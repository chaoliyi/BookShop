using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.BLL

{
    public partial class UserManager
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BookShop.Model.User model, out string msg)
        {
            int count = -1;
            if (ValidateUserName(model.LoginId))
            {
                msg = "用户名已存在";
            }
            else
            {
                count =dal.Add(model);
                msg = "注册成功";
            }
            return count;
        }
        /// <summary>
        /// 完成用户名检查
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ValidateUserName(string userName)
        {
            return dal.GetModel(userName) != null ? true : false;
        }

        /// <summary>
        /// 根据邮箱找信息
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public bool CheckUserMail(string mail)
        {
            return dal.CheckUserMail(mail) > 0 ? true : false;
        }
        /// <summary>
        /// 完成用户登录信息的校验
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="msg"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool CheckUserInfo(string userName, string userPwd, out string msg, out Model.User userInfo) {
            bool isSucess = false;
            userInfo=dal.GetModel(userName);
            if (userInfo!=null)
            {
                if (userInfo.LoginPwd==userPwd)
                {
                    msg = "登陆成功";
                    isSucess = true;
                }
                else
                {
                    msg = "用户名或密码错误";
                }
            }
            else
            {
                msg = "用户名不存在";
            }
            return isSucess;
        }
        /// <summary>
        /// 根据用户名查找用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Model.User GetModel(string userName)
        {            
            return dal.GetModel(userName); 
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool FindPassword(Model.User userInfo)
        {
            #region 系统产生新的密码
            /*系统产生一个新的密码，然后更新数据库，再将新密码发送到用户的邮箱*/
            string newPwd = Guid.NewGuid().ToString().Substring(0, 8);
            userInfo.LoginPwd = newPwd;
            dal.Update(userInfo);

            MailMessage msg = new MailMessage();

            msg.To.Add("woaisln1314@163.com");//收件人地址 
            //msg.CC.Add("77577053@qq.com");//抄送人地址 

            msg.From = new MailAddress("77577053@qq.com", "Gc");//发件人邮箱，名称 

            msg.Subject = "超利益科技-用户密码修改";//邮件标题 
            msg.SubjectEncoding = Encoding.UTF8;//标题格式为UTF8 

            StringBuilder sb = new StringBuilder();
            sb.Append("用户名：" + userInfo.LoginId);
            sb.Append("新密码："+newPwd);
            msg.Body = sb.ToString();//邮件内容 
            msg.BodyEncoding = Encoding.UTF8;//内容格式为UTF8 

            SmtpClient client = new SmtpClient();

            client.Host = "smtp.qq.com";//SMTP服务器地址 
            client.Port = 587;//SMTP端口，QQ邮箱填写587 

            client.EnableSsl = true;//启用SSL加密 
            //发件人邮箱账号，授权码(注意此处，是授权码你需要到qq邮箱里点设置开启Smtp服务，然后会提示你第三方登录时密码处填写授权码)
            client.Credentials = new System.Net.NetworkCredential("77577053@qq.com", "kywciazvrjhdbjhj");

            try
            {
                client.Send(msg);//发送邮件
            }
            catch (Exception)
            {
                return false;
            }
            return true;
            #endregion
        }
    }
}
