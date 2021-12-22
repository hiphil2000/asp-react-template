using System;
using System.Linq;
using System.Web.Http;
using ReactTest.API.Libraries;
using ReactTest.API.Models;
using ReactTest.API.Models.Auth;
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
		public AuthenticationResult Login([FromBody] LoginPayload payload)
		{
			string token = null;
			string message = null;

			// 토큰을 생성합니다. 실패 시 오류 메시지를 전달합니다.
			try
			{
				var userData = ProcessLogin(payload.UserId, payload.Password);
				if (userData != null)
				{
					token = JwtHelper.CreateToken(userData);
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

			return new AuthenticationResult()
			{
				Success = token != null,
				Token = token,
				Message = message
			};
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