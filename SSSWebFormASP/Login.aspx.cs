using Antlr.Runtime.Misc;
using SSSWebFormASP.Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSWebFormASP
{
    public partial class Login : System.Web.UI.Page
    {
        Database func = new Database();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            string username = txt_username.Value;
            string password = txt_password.Value;

            string qry = $@"SELECT [Username]
                                  ,[Password]
                              FROM [dbo].[Employee]
                              WHERE [Username] = '{username}' AND [Password] = '{password}'";


            DataTable dt = func.ConnectDatabase_Return(qry);

            if (dt.Rows.Count > 0)
            {

            }
            else
            {
                dt.Clear();
            }
        }
    }
}