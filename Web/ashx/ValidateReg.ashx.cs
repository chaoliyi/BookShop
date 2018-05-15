using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// ValidateReg 的摘要说明
    /// </summary>
    public class ValidateReg : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        BLL.UserManager userManager = new BLL.UserManager();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action = context.Request["action"];
            if (action== "validateEmail")
            {
                CheckUserEmail(context);
            }
            else if(action=="validateCode")
            {
                CheckUserCode(context);
            }
            else if (action=="validateName")
            {
                CheckUserName(context);
            }
            else
            {
                context.Response.Write("参数错误!");
            }
            
            
        }
        //校验用户名
        private void CheckUserName(HttpContext context)
        {
            string userName = context.Request["userName"];
            if (userManager.ValidateUserName(userName))
            {
                context.Response.Write("用户名已存在");
            }
            else
            {
                context.Response.Write("用户名可用");
            }
        }

        //校验验证码
        private void CheckUserCode(HttpContext context)
        {
            string userVCode = context.Request["userVCode"];
            string sysVCode = context.Session["vCode"].ToString();
            if (sysVCode!=null)
            {
                if (sysVCode.Equals(userVCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Response.Write("验证码正确");
                }
                else
                {
                    context.Response.Write("验证码错误");
                }
            }
            else
            {
                context.Response.Write("生成验证码出错");
            }
        }
        //校验邮箱
        private void CheckUserEmail(HttpContext context)
        {
            string userEmail = context.Request["userEmail"];
            if (userManager.CheckUserMail(userEmail))
            {
                context.Response.Write("邮箱已经存在");
            }
            else
            {
                context.Response.Write("邮箱可以注册");
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