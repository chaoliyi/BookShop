using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.Member
{
    public partial class Login : System.Web.UI.Page
    {
        public string LoginMessage { get; set; }
        public string ReturnUrl { get; set; }
        BLL.UserManager userBll = new BLL.UserManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //校验用户信息
                UserLogin();
            }
            else
            {
                this.ReturnUrl = Request["returnUrl"];
                /*校验cookie中的值*/
                CheckCookiesInfo();
            }
        }        
        private void CheckCookiesInfo()
        {
            if (Request.Cookies["userName"]!=null && Request.Cookies["userPwd"]!=null)
            {
                string userName = Request.Cookies["userName"].Value;
                string userPwd = Request.Cookies["userPwd"].Value;
                Model.User userInfo = userBll.GetModel(userName);
                if (userInfo!=null)
                {
                    if (userPwd==Common.WebCommon.StringConvertToMD5(Common.WebCommon.StringConvertToMD5(userInfo.LoginPwd)))
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
                        /*如果cookie有值，但密码错误，则清空cookie*/
                        Response.Cookies["userName"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["userName"].Expires = DateTime.Now.AddDays(-1);
                    }
                }
            }
        }

        private void UserLogin()
        {
            string userName = Request["txtLoginID"];
            string userPwd = Request["txtLoginPwd"];
            string msg = string.Empty;
            Model.User userInfo = new Model.User();
           
            if (userBll.CheckUserInfo(userName, userPwd, out msg, out userInfo))
            {
                Session["userInfo"] = userInfo;
                /*自动登录 写入cookie*/
                if (!string.IsNullOrEmpty(Request["chkAutoLogin"]))
                {
                    HttpCookie cookieUserName = new HttpCookie("userName",userName);
                    HttpCookie cookieUserPwd = new HttpCookie("userPwd", Common.WebCommon.StringConvertToMD5(Common.WebCommon.StringConvertToMD5(userPwd)));
                    cookieUserName.Expires=DateTime.Now.AddDays(7);
                    cookieUserPwd.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookieUserName);
                    Response.Cookies.Add(cookieUserPwd);

                }
                /*判断跳转页面*/
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