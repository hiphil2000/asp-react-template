using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.IdentityModel.Tokens;
using ReactTest.API.Libraries;
using ReactTest.API.Models;
using ReactTest.API.Models.Auth;
using ReactTest.API.Models.Enums;

namespace ReactTest.API.Filters
{
	/// <summary>
	/// 역할 기반 접근 제어를 위한 Attribute입니다.
	/// 기본 모드는 WhiteList입니다.
	/// </summary>
	public class RoleBasedAuthenticationAttribute : ActionFilterAttribute, IActionFilter
	{
		/// <summary>
		/// WhiteList 여부입니다.
		/// False인 경우, BlackList 방식으로 권한제어를 합니다.
		/// </summary>
		public bool IsWhiteList { get; set; } = true;

		/// <summary>
		/// 허용할 권한 목록
		/// </summary>
		public List<Role> PermitList { get; set; } = new List<Role>();

		/// <summary>
		/// 불허할 권한 목록
		/// </summary>
		public List<Role> DenyList { get; set; } = new List<Role>();

		/// <summary>
		/// 액션 실행 전 권한이 없는 요청을 차단합니다.
		/// </summary>
		/// <param name="actionContext"></param>
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			// 권한이 필요 없는 경우에는 진행하지 않습니다.
			if (IsAllowAnonymous(actionContext))
			{
				return;
			}
			
			// 요청 컨텍스트
			var request = actionContext.Request;
			var accessTokenCookie = request.Headers.GetCookies(JwtHelper.Constants.AccessToken)?.FirstOrDefault();
			var refreshTokenCookie = request.Headers.GetCookies(JwtHelper.Constants.RefreshToken)?.FirstOrDefault();

			var accessToken = accessTokenCookie?[JwtHelper.Constants.AccessToken].Value;
			var refreshToken = refreshTokenCookie?[JwtHelper.Constants.RefreshToken].Value;
			
			// 둘 다 유효하지 않은 경우: 권한 없음
			if (!(IsValidToken(accessToken) || IsValidToken(refreshToken)))
			{
				// 모두 유효하지 않으므로, 권한 없음 처리합니다.
				HandleUnauthorized(actionContext);
			}
			
			base.OnActionExecuting(actionContext);
		}

		/// <summary>
		/// 요청이 완료된 후, 토큰 재발급 절차를 밟습니다.
		/// </summary>
		/// <param name="actionExecutedContext"></param>
		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
			var actionContext = actionExecutedContext.ActionContext;
			
			// 권한이 필요 없는 경우에는 진행하지 않습니다.
			if (IsAllowAnonymous(actionContext))
			{
				return;
			}
			
			// 요청 컨텍스트
			var request = actionContext.Request;

			// 토큰의 유효 여부를 확인하고, 재발급을 진행합니다.
			var accessToken = JwtHelper.GetTokenCookie(JwtHelper.Constants.AccessToken, request);
			var refreshToken = JwtHelper.GetTokenCookie(JwtHelper.Constants.RefreshToken, request);

			// 토큰 재발급
			JwtHelper.ReissueToken(accessToken, refreshToken, actionContext.Response);
			
			base.OnActionExecuted(actionExecutedContext);
		}

		/// <summary>
		/// 요청에 대해 권한 없음 처리를 합니다.
		/// </summary>
		/// <param name="context">요청의 액션 컨텍스트</param>
		private void HandleUnauthorized(HttpActionContext context)
		{
			 context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
		}

		/// <summary>
		/// 대상 액션의 AllowAnonymous Attribute 적용 여부를 확인합니다.
		/// </summary>
		/// <param name="actionContext">요청의 액션 컨텍스트</param>
		/// <returns></returns>
		private static bool IsAllowAnonymous(HttpActionContext actionContext)
		{
			return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
			       actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>()
				       .Any();
		}

		private bool IsValidToken(string tokenString)
		{
			if (tokenString.IsNullOrEmpty())
			{
				return false;
			}
			
			var decoded = JwtHelper.DecodeToken(tokenString);
			
			return decoded != null ||
			       JwtHelper.IsUsingToken(decoded.JwtId.ToString()) ||
			       (IsWhiteList && PermitList.Contains(decoded.Role)) ||
			       (!IsWhiteList && !DenyList.Contains(decoded.Role));
		}
	}
}