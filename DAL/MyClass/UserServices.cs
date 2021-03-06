﻿using Maticsoft.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.DAL
{
   public partial class UserServices
    {
       /// <summary>
       /// 根据用户名查询用户
       /// </summary>
       /// <param name="userName"></param>
       /// <returns></returns>
       public Model.User GetModel(string userName)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append("select  top 1 Id,LoginId,LoginPwd,Name,Address,Phone,Mail,UserStateId from Users ");
           strSql.Append(" where LoginId=@LoginId ");
           SqlParameter[] parameters = {
					new SqlParameter("@LoginId", SqlDbType.NVarChar,50)};
           parameters[0].Value = userName;

           BookShop.Model.User model = new BookShop.Model.User();
           DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
           if (ds.Tables[0].Rows.Count > 0)
           {
               if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
               {
                   model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
               }
               model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
               model.LoginPwd = ds.Tables[0].Rows[0]["LoginPwd"].ToString();
               model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
               model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
               model.Phone = ds.Tables[0].Rows[0]["Phone"].ToString();
               model.Mail = ds.Tables[0].Rows[0]["Mail"].ToString();

               if (ds.Tables[0].Rows[0]["UserStateId"].ToString() != "")
               {
                   int UserStateId = int.Parse(ds.Tables[0].Rows[0]["UserStateId"].ToString());
                   model.UserState = userStateServices.GetModel(UserStateId);
               }
               return model;
           }
           else
           {
               return null;
           }
       }
       /// <summary>
       /// 根据邮箱找信息
       /// </summary>
       /// <param name="mail"></param>
       /// <returns></returns>
       public int CheckUserMail(string mail)
       {
           string sql = "select count(*) from Users where Mail=@Mail";
           SqlParameter[] parameters = {
					new SqlParameter("@Mail", SqlDbType.NVarChar,50)};
           parameters[0].Value = mail;
          return Convert.ToInt32(DbHelperSQL.GetSingle(sql, parameters));
       }

    }
}
