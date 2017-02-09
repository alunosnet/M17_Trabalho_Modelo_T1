using System;
using System.Collections.Generic;
using System.Configuration;
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
            try
            {
                if (tbEmail.Text == String.Empty)
                    throw new Exception("Tem de indicar um email");
                //verificar se o email existe
                string email = tbEmail.Text;
                DataTable dados = BaseDados.Instance.devolveDadosUtilizador(email);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("");

                //GUID
                Guid g = Guid.NewGuid();

                //guardar guid na bd
                BaseDados.Instance.recuperarPassword(email, g.ToString());
                //enviar email com guid
                string mensagem = "Clique no link para recuperar a sua password.\n";
                mensagem += "<a href='http://" + Request.Url.Authority + "/recuperarPassword.aspx?id=";
                mensagem += Server.UrlEncode(g.ToString()) + "'>Clique aqui</a>";
                string senha = ConfigurationManager.AppSettings["senha"].ToString();
                Helper.enviarMail("alunosnet@gmail.com", senha, email, "Recuperação de password", mensagem);
                lbErro.Text = "Foi enviado um email.";
                lbErro.CssClass = "alert alert-success";
            }
            catch(Exception erro)
            {
                lbErro.Text = erro.Message;
                if (erro.Message != String.Empty)
                    lbErro.CssClass = "alert alert-danger";
            }
        }
    }
}