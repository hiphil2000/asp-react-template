using System.Web.Http;
using ReactTest.API.Models.Auth;

namespace ReactTest.API.Libraries
{
	/// <summary>
	/// JWT 관련 기능의 확장 메서드 클래스입니다.
	/// </summary>
	public static class JwtExtensions
	{
		/// <summary>
		/// 현재 요청의 JwtPayload를 조회합니다.
		/// </summary>
		/// <param name="controller">요청 컨트롤러 인스턴스</param>
		/// <returns></returns>
		public static JwtPayload GetJwtPayload(this ApiController controller)
		{
			return JwtHelper.DecodeToken(controller.Request.Headers.Authorization.ToString());
		}
	}
}