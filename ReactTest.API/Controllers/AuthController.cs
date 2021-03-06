using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using IMSWeb.Controllers.Database;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ReactTest.API.Filters;
using ReactTest.API.Libraries;
using ReactTest.API.Models;
using ReactTest.API.Models.Auth;
using ReactTest.API.Models.Enums;
using ReactTest.API.Models.Responses;

namespace ReactTest.API.Controllers
{
	public class AuthController : ApiController
	{
		private static readonly UserModel[] UserList = new[]
		{
			new UserModel()
				{ UserNo = 1, Role = Role.Admin, UserId = "admin", UserPassword = "password1", UserName = "관리자" },
			new UserModel()
				{ UserNo = 2, Role = Role.User, UserId = "user1", UserPassword = "password3", UserName = "사용자1" },
			new UserModel()
				{ UserNo = 3, Role = Role.User, UserId = "user2", UserPassword = "password2", UserName = "사용자2" },
		};

		/// <summary>
		/// 요청에 따라 로그인을 진행합니다.
		/// 성공 시 토큰이 반환됩니다.
		/// </summary>
		/// <param name="payload">로그인 Payload</param>
		/// <returns></returns>
		[HttpPost]
		[ActionName("Login")]
		public HttpResponseMessage Login([FromBody] LoginPayload payload)
		{
			TokenModel token = null;
			UserModel user = null;
			string message = null;

			// 토큰을 생성합니다. 실패 시 오류 메시지를 전달합니다.
			try
			{
				user = ProcessLogin(payload.UserId, payload.Password);
				if (user != null)
				{
					token = new TokenModel()
					{
						AccessToken = JwtHelper.CreateAccessToken(user.UserNo),
						RefreshToken = JwtHelper.CreateRefreshToken(user.UserNo),
					};
				}
				else
				{
					message = "mismatch ID or Password.";
				}
			}
			catch (Exception e)
			{
				message = e.Message;
			}

			// 응답 메시지 생성
			var response = new HttpResponseMessage();
			response.Content = new StringContent(
				JsonConvert.SerializeObject(new AuthenticationResult()
				{
					Success = token != null,
					Token = token,
					User = user,
					Message = message
				}, GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings)
			);
			response.StatusCode = token != null ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
			
			// 인증에 성공한 경우, 쿠키도 생성합니다.
			if (token != null)
			{
				response.Headers.AddCookies(JwtHelper.CreateTokenCookies(token.AccessToken, token.RefreshToken));
			}
			
			return response;
		}

		[HttpPost]
		[ActionName("Logout")]
		public HttpResponseMessage Logout()
		{
			var response = new HttpResponseMessage();
			
			var accessToken = this.GetJwtToken(JwtHelper.Constants.AccessToken);
			var refreshToken = this.GetJwtToken(JwtHelper.Constants.RefreshToken);

			// 토큰이 아예 없는 경우: 무시
			if (accessToken.IsNullOrEmpty() && refreshToken.IsNullOrEmpty())
			{
				return response;
			}
			
			// DB에서 쿠키를 사용안함 처리합니다.
			RemoveToken(accessToken);
			RemoveToken(refreshToken);

			// 만료된 쿠키를 추가하여 쿠키를 제거합니다.
			var cookies = JwtHelper
				.CreateTokenCookies(accessToken, refreshToken)
				.Select(x =>
				{
					x.Expires = DateTimeOffset.Now.AddDays(-1);
					return x;
				});
			response.Headers.AddCookies(cookies);

			return response;
		}

		private void RemoveToken(string token)
		{
			var decoded = JwtHelper.DecodeToken(token);
			if (decoded == null)
			{
				return;
			}

			using (var db = new SqlService())
			{
				db.AddParameter("@TokenId", DbType.Guid, decoded.JwtId);
				_ = db.ExecuteQuery("uSP_RemoveToken", commandType: CommandType.StoredProcedure);
			}
		}

		/// <summary>
		/// 현재 사용자 정보를 조회합니다.
		/// 토큰이 올바르지 않거나, 정보가 없다면 null이 반환됩니다.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ActionName("CurrentUser")]
		[TokenAuthentication]
		public UserModel GetCurrentUser()
		{
			var payload = this.GetJwtPayload();
			return UserList.FirstOrDefault(u => u.UserNo == payload.Issuer);
		}

		[HttpPost]
		[ActionName("ValidateToken")]
		[RoleBasedAuthentication]
		public JwtPayload ValidateToken()
		{
			return this.GetJwtPayload();
		}

		/// <summary>
		/// 로그인을 처리합니다.
		/// 로그인 정보가 없다면 null을 반환합니다.
		/// (임시 로직)
		/// </summary>
		/// <param name="userId">사용자 ID</param>
		/// <param name="password">사용자 비밀번호</param>
		/// <returns></returns>
		private UserModel ProcessLogin(string userId, string password)
		{
			return UserList
				.FirstOrDefault(u =>
					u.UserId.Equals(userId) &&
					u.UserPassword.Equals(password));
		}
	}
}