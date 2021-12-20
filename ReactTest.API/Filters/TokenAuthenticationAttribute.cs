using System;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ReactTest.API.Filters
{
	public class TokenAuthenticationAttribute : ActionFilterAttribute
	{
		public const string SecretKey = "SECRET";
		
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (actionContext.Request.RequestUri.AbsolutePath.ToLower().StartsWith("/api/auth/"))
			{
				return;
			}

			var handler = new JsonWebTokenHandler();
			string tokenString = null;

			// 토큰 값을 가져옵니다.
			if (actionContext.Request.Headers.Authorization != null)
			{
				tokenString = actionContext.Request.Headers.Authorization.ToString();
			}

			// 토큰이 없거나, 확인할 수 없는 경우에는 UnauthorizedAccessException을 발생시킵니다.
			if (tokenString == null || !handler.CanValidateToken)
			{
				throw new UnauthorizedAccessException();
			}
			
			// 토큰을 검사합니다.
			var validationResult = handler.ValidateToken(tokenString, new TokenValidationParameters()
			{
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey))
			});

			// 올바르지 않은 경우, 예외를 발생시킵니다.
			if (!validationResult.IsValid)
			{
				if (validationResult.Exception != null)
				{
					throw validationResult.Exception;
				}

				throw new UnauthorizedAccessException();
			}
			
			base.OnActionExecuting(actionContext);
		}
	}
}