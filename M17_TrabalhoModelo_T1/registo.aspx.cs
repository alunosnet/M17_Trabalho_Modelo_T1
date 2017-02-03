using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_TrabalhoModelo_T1
{
    public partial class registo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btRegistar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = tbEmail.Text;
                string nome = tbNome.Text;
                string morada = tbMorada.Text;
                string nif = tbNIF.Text;
                string password = tbPassword.Text;
                if (email == String.Empty)
                    throw new Exception("Tem de preencher o email.");
                if (nome == String.Empty)
                    throw new Exception("Tem de indicar um nome.");
                if (nome.Length < 3)
                    throw new Exception("O nome indicado é muito pequeno.");
                if (password.Length < 3)
                    throw new Exception("A password é muito pequena.");
                //validar o recaptcha
                var encodedResponse = Request.Form["g-Recaptcha-Response"];
                var eValido = ReCaptcha.Validate(encodedResponse);
                if (!eValido)
                    throw new Exception("Tem de provar que não é um robot...");
                //registar
                BaseDados.Instance.registarUtilizador(email, nome, morada, nif, password);
                //TODO: enviar email
                //redirecionar para index
                Response.Redirect("index.aspx");
            }catch(Exception erro)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErro.CssClass = "alert alert-danger";
            }
        }
    }
}