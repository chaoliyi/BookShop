using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class ShowMsg : System.Web.UI.Page
    {
        public string ErrorMessage { get; set; }
        public string LinkTitle { get; set; }
        public string LinkUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ErrorMessage = string.IsNullOrEmpty(Request["msg"]) ? "暂无信息" : Request["msg"];
            this.LinkTitle = string.IsNullOrEmpty(Request["txt"]) ? "首页" : Request["txt"];
            this.LinkUrl = string.IsNullOrEmpty(Request["url"]) ? "/Default.aspx" : Request["url"];
        }
    }
}