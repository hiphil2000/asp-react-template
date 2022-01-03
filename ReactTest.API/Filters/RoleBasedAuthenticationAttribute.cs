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
	public class RoleBasedAuthenticationAttribute : TokenAuthenticationAttribute
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
			base.OnActionExecuting(actionContext);

			var request = actionContext.Request;
			var access = JwtHelper.GetTokenCookie(JwtHelper.Constants.AccessToken, request); 
			var refresh = JwtHelper.GetTokenCookie(JwtHelper.Constants.RefreshToken, request);

			// 권한이 올바르지 않은 경우: 권한 없음
			if (!(IsValidRole(access) || IsValidRole(refresh)))
			{
				HandleUnauthorized(actionContext);
			}
			
			base.OnActionExecuting(actionContext);
		}

		/// <summary>
		/// 토큰의 권한이 현재 대상에 올바른지 확인합니다.
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		private bool IsValidRole(string token)
		{
			if (token == null)
			{
				return false;
			}
			
			var decoded = JwtHelper.DecodeToken(token);

			return IsWhiteList ? PermitList.Contains(decoded.Role) : !DenyList.Contains(decoded.Role);
		}
	}
}