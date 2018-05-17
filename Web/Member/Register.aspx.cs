using BookShop.Web.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web.Member
{
    public partial class Register : System.Web.UI.Page
    {
        public string ReturnUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Common.WebCommon.CheckValidateCode(Request["txtCode"]))//完成验证码校验
                {
                    AddUserInfo();
                }
            }
            else
            {
                this.ReturnUrl = Request["returnUrl"];
            }
        }

        #region 完成用户注册
        private void AddUserInfo()
        {
            string msg = string.Empty;
            Model.User userInfo = new Model.User();
            userInfo.Address = Request["txtAddress"];
            userInfo.LoginId = Request["txtName"];
            userInfo.LoginPwd = Request["txtPwd"];
            userInfo.Mail = Request["txtEmail"];
            userInfo.Name = Request["txtRealName"];
            userInfo.Phone = Request["txtPhone"];

            userInfo.UserState.Id = Convert.ToInt32(UserStateEnum.NormalState);

            BLL.UserManager userInfoBll = new BLL.UserManager();
            if (userInfoBll.Add(userInfo, out msg) > 0)
            {
                //跳转后实现自动登录
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
                Response.Redirect("/ShowMsg.aspx?msg=" + msg + "&txt=首页&url=/Default.aspx");
            }
        }
        #endregion       
    }
}