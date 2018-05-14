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