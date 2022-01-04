using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMSWeb.Controllers.Database
{
    /// <summary>
    /// 데이터에 접근할 공급자 목록입니다.
    /// </summary>
    public enum Providers
    {
        SqlClient,
        OleDb,
        Odbc
    }
}