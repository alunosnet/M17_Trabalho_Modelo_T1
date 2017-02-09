using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_TrabalhoModelo_T1
{
    public partial class recuperarPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btPassword_Click(object sender, EventArgs e)
        {
            try
            {
                //guid
                string guid = Server.UrlDecode(Request["id"].ToString());
                //atualizar a password
                string novaPassword = tbPassword.Text;
                if (novaPassword == String.Empty)
                    throw new Exception("");
                BaseDados.Instance.atualizarPassword(guid, novaPassword);
                Response.Redirect("index.aspx");
            }catch(Exception erro)
            {
                Response.Redirect("index.aspx");
            }
        }
    }
}