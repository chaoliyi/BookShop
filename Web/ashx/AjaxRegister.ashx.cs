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
    public class AjaxRegister : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (Common.WebCommon.CheckValidateCode(context.Request["txtCode"]))//服务端校验验证码
            {
                AddUserInfo(context);
            }
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