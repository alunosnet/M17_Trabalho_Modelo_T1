using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_TrabalhoModelo_T1
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Session["id"].ToString());
                BaseDados.Instance.executaComando("UPDATE utilizadores SET online=0 WHERE id=" + id);
                Session.Clear();
            }
            catch
            {

            }
            Response.Redirect("index.aspx");
        }
    }
}