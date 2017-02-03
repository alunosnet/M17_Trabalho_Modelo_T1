using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_TrabalhoModelo_T1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] != null)
                divLogin.Visible = false;
            //TODO: listar os livros disponíveis
        }
        //login
        protected void btLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = tbEmail.Text;
                string password = tbPassword.Text;
                DataTable dados = BaseDados.Instance.verificarLogin(email, password);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("Login falhou.");
                Session["nome"] = dados.Rows[0]["nome"].ToString();
                Session["perfil"] = dados.Rows[0]["perfil"].ToString();
                Session["id"] = dados.Rows[0]["id"].ToString();
                if (Session["perfil"].Equals("0"))
                    Response.Redirect("areaadmin.aspx");
                else
                    Response.Redirect("areacliente.aspx");
            }catch(Exception erro)
            {
                lbErro.Text = erro.Message;
                lbErro.CssClass = "alert alert-danger";
            }
        }
        //recuperar
        protected void btRecuperar_Click(object sender, EventArgs e)
        {

        }
    }
}