using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HttpServer
{
    public partial class getUserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            try
            {
                if (Request.QueryString.Count > 0)
                {
                    foreach (String key in Request.QueryString.AllKeys)
                    {
                        if (Request.QueryString[key].Equals("001"))
                        {
                            Response.Write("破船的基本信息如下:\n 博客：http://beyondvincent.com\n新浪微博：www.weibo.com/beyondvincent");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.Write(ex.ToString());
            }
        }
    }
}