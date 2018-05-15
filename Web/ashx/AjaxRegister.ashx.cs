using BookShop.Web.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// AjaxRegister 的摘要说明
    /// </summary>
    public class AjaxRegister : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        BLL.UserManager userManager = new BLL.UserManager();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (CheckUserName(context.Request["txtName"]))
            {
                context.Response.Write("用户名已存在");
                return;
            }
            if (CheckEmail(context.Request["txtEmail"]))
            {
                context.Response.Write("邮箱已存在");
                return;
            }
            if (!CheckValidateCode(context.Request["txtCode"]))
            {
                context.Response.Write("验证码错误");
                return;
            }
            //添加用户
            AddUserInfo(context);
        }

        //服务端-校验用户名
        private bool CheckUserName(string name)
        {
            bool exist = false;
            if (userManager.ValidateUserName(name))
            {
                exist = true;
            }
            return exist;
        }
        //服务端-校验邮箱
        private bool CheckEmail(string email)
        {
            bool exist = false;
            if (userManager.CheckUserMail(email))
            {
                exist = true;
            }
            return exist;
        }

        //服务端-校验验证码
        private bool CheckValidateCode(string vCode)
        {
            bool isRight = false;
            if (HttpContext.Current.Session["vCode"].ToString().Equals(vCode, StringComparison.InvariantCultureIgnoreCase))
            {
                isRight = true;
                HttpContext.Current.Session["vCode"] = null;
            }
            return isRight;
        }

       

        private void AddUserInfo(HttpContext context)
        {
            string msg = string.Empty;
            Model.User userInfo = new Model.User();
            userInfo.Address = context.Request["txtAddress"];
            userInfo.LoginId = context.Request["txtName"];
            userInfo.LoginPwd = context.Request["txtPwd"];
            userInfo.Mail = context.Request["txtEmail"];
            userInfo.Name = context.Request["txtRealName"];
            userInfo.Phone = context.Request["txtPhone"];

            userInfo.UserState.Id = Convert.ToInt32(UserStateEnum.NormalState);

            BLL.UserManager userInfoBll = new BLL.UserManager();
            if (userInfoBll.Add(userInfo, out msg) > 0)
            {
                //跳转后实现自动登录
                context.Session["userInfo"] = userInfo;
                context.Response.Write("ok");
            }
            else
            {
                context.Response.Write(msg);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}