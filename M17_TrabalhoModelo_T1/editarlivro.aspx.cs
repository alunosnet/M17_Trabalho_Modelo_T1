using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_TrabalhoModelo_T1
{
    public partial class editarlivro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //verificar sessão
            if (Session["perfil"] == null || !Session["perfil"].Equals("0"))
                Response.Redirect("index.aspx");
            //dados do livro
            if (!IsPostBack)
            {
                try
                {
                    int nlivro = int.Parse(Request["nlivro"].ToString());
                    DataTable dados = BaseDados.Instance.devolveDadosLivro(nlivro);
                    if (dados == null || dados.Rows.Count == 0)
                        throw new Exception("Erro");
                    tbNomeLivro.Text = dados.Rows[0]["nome"].ToString();
                    tbAno.Text = dados.Rows[0]["ano"].ToString();
                    tbData.Text = DateTime.Parse(dados.Rows[0]["data_aquisicao"].ToString()).ToShortDateString();
                    tbPreco.Text = String.Format("{0:C}", Decimal.Parse(dados.Rows[0]["preco"].ToString()));
                    
                    //capa
                    string ficheiro = @"~\Imagens\" + dados.Rows[0]["nlivro"].ToString() + ".jpg";
                    imgCapa.ImageUrl = ficheiro;
                    imgCapa.Width = 100;
                }
                catch
                {
                    Response.Redirect("areaadmin.aspx");
                }
            }
        }

        protected void btAtualizarLivro_Click(object sender, EventArgs e)
        {
            try
            {
                int nlivro = int.Parse(Request["nlivro"].ToString());
                string nome = tbNomeLivro.Text;
                int ano = int.Parse(tbAno.Text);
                DateTime data = DateTime.Parse(tbData.Text);
                decimal preco = Decimal.Parse(tbPreco.Text);
                BaseDados.Instance.atualizaLivro(nlivro, nome, ano, data, preco);
                if (fuCapa.HasFile)
                {
                    if(fuCapa.PostedFile.ContentLength>0 && fuCapa.PostedFile.ContentType == "image/jpeg")
                    {
                        string ficheiro = Server.MapPath(@"~\Imagens\" + nlivro + ".jpg");
                        fuCapa.SaveAs(ficheiro);
                    }
                }
              //  Response.CacheControl = "no-cache";
                Response.Redirect("areaadmin.aspx");
            }
            catch (Exception erro)
            {
                lbErroLivro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErroLivro.CssClass = "alert alert-danger";
            }
        }

        protected void btVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("areaadmin.aspx");
        }
    }
}