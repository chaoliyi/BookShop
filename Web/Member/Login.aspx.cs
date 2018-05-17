using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.Member
{
    public partial class Login: System.Web.UI.Page
    {
        public string LoginMessage { get; set; }
        public string ReturnUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                UserLogin();
            }
            else
            {
                this.ReturnUrl = Request["returnUrl"];
            }
        }

        private void UserLogin()
        {
            string userName = Request["txtLoginID"];
            string userPwd = Request["txtLoginPwd"];
            string msg = string.Empty;
            Model.User userInfo = new Model.User();

            BLL.UserManager userBll = new BLL.UserManager();
            if(userBll.CheckUserInfo(userName,userPwd,out msg,out userInfo))
            {
                Session["userInfo"] = userInfo;
                if (string.IsNullOrEmpty(Request["hiddenReturnUrl"]))
                {
                    Response.Redirect("/Default.aspx");
                }
                else
                {
                    Response.Redirect(Request["hiddenReturnUrl"]);
                }
                
            }
            else
            {
                this.LoginMessage = msg;
            }
        }
    }
}