using System.Data;

namespace IMSWeb.Controllers.Database
{
	public static class DbExtensions
	{
		public static bool GetDbBool(this DataRow dr, string column)
		{
			return dr[column].ToString().Equals("Y");
		}
	}
}