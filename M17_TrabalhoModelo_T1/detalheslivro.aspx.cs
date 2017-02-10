using System;
using System.Data;
using System.Web;

namespace M17_TrabalhoModelo_T1
{
    public partial class detalheslivro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(Request["id"]);
                DataTable dados = BaseDados.Instance.devolveDadosLivro(id);
                lbNome.Text = dados.Rows[0]["nome"].ToString();
                lbAno.Text = dados.Rows[0]["ano"].ToString();
                lbPreco.Text = String.Format(" | {0:C}", Decimal.Parse(dados.Rows[0]["preco"].ToString()));
                string ficheiro = @"~\Imagens\" + dados.Rows[0]["nlivro"].ToString() + ".jpg";
                imgCapa.ImageUrl = ficheiro;
                imgCapa.Width = 200;
                //criar cookie com o id do livro
                HttpCookie cookie = new HttpCookie("ultimolivro", id.ToString());
                cookie.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(cookie);
            }
            catch
            {
                Response.Redirect("index.aspx");
            }
        }
    }
}