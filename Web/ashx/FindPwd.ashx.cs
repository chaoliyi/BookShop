using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// FindPwd 的摘要说明
    /// </summary>
    public class FindPwd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userName = context.Request["userName"];
            string userEmail = context.Request["userEmail"];

            if (Common.WebCommon.StringIsNullOrEmpty(userName,userEmail))
            {
                context.Response.Write("数据提交出错，请重试！");
            }
            else
            {
                BLL.UserManager userBll = new BLL.UserManager();
                Model.User userInfo=userBll.GetModel(userName);
                if (userInfo!=null)
                {
                    if (userEmail==userInfo.Mail)
                    {
                        //找回用户密码
                        if (userBll.FindPassword(userInfo))
                        {
                            context.Response.Write("新密码已发送到您的邮箱！");
                        }
                    }
                    else
                    {
                        context.Response.Write("邮箱错误！");
                    }
                }
                else
                {
                    context.Response.Write("用户名不存在！");
                }
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