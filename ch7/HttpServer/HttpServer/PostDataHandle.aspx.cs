using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HttpServer
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            try
            {
                // Write back the request body
                Response.Write("你post的内容是:");

                using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.InputStream))
                {
                    string body = reader.ReadToEnd();
                    Response.Write(body);
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