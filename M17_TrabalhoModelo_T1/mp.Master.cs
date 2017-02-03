using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_TrabalhoModelo_T1
{
    public partial class mp : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["avisoT1"] as HttpCookie;
            if (cookie != null)
                div_aviso.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Guid g = Guid.NewGuid();

            HttpCookie cookie = new HttpCookie("avisoT1", g.ToString());
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            div_aviso.Visible = false;
        }

    }
}