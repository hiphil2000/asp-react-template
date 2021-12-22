using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ReactTest.API.Libraries;
using ReactTest.API.Models;

namespace ReactTest.API.Filters
{
	/// <summary>
	/// 역할 기반 접근 제어를 위한 Attribute입니다.
	/// 기본 모드는 WhiteList입니다.
	/// </summary>
	public class RoleBasedAuthenticationAttribute : AuthorizationFilterAttribute
	{
		/// <summary>
		/// WhiteList 여부입니다.
		/// False인 경우, BlackList 방식으로 권한제어를 합니다.
		/// </summary>
		public bool IsWhiteList { get; set; } = true;
		
		/// <summary>
		/// 허용할 권한 목록
		/// </summary>
		public Role[] PermitList { get; set; }
		
		/// <summary>
		/// 불허할 권한 목록
		/// </summary>
		public Role[] DenyList { get; set; }

		/// <summary>
		/// 인증 처리 메서드입니다.
		/// </summary>
		/// <param name="actionContext"></param>
		public override void OnAuthorization(HttpActionContext actionContext)
		{
			// 요청 컨텍스트
			var request = actionContext.Request;

			// 만약 요청 헤더에 토큰이 존재하지 않으면 권한 없음 처리 합니다.
			if (request.Headers.Authorization == null)
			{
				HandleUnauthorized(actionContext);
			}
			else
			{
				// 토큰의 Payload를 조회합니다.
				var payload = JwtHelper.DecodeToken(request.Headers.Authorization.ToString());

				// WhiteList인데 PermitList에 없거나,
				// BlackList인데 DenyList에 있는 경우, 권한 없음 처리합니다.
				if ((IsWhiteList && !PermitList.Contains(payload.Role)) ||
				    (!IsWhiteList && DenyList.Contains(payload.Role)))
				{
					HandleUnauthorized(actionContext);
				}
			}
		}

		/// <summary>
		/// 요청에 대해 권한 없음 처리를 합니다.
		/// </summary>
		/// <param name="context">요청의 액션 컨텍스트</param>
		private void HandleUnauthorized(HttpActionContext context)
		{
			 context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
		}
	}
}