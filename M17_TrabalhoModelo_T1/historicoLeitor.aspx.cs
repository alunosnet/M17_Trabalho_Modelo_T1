using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_TrabalhoModelo_T1
{
    public partial class historicoLeitor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["perfil"]==null || !Session["perfil"].Equals("0"))
            {
                Response.Redirect("index.aspx");
            }
            try
            {
                int id = int.Parse(Request["id"].ToString());
                gvHistorico.DataSource = BaseDados.Instance.listaTodosEmprestimosComNomes(id);
                gvHistorico.DataBind();
            }catch
            {
                Response.Redirect("areaadmin.aspx");
            }
        }
    }
}