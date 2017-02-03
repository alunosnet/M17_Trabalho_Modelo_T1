using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_TrabalhoModelo_T1
{
    public partial class removerlivro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //verificar se é admin
            if (Session["perfil"] == null || !Session["perfil"].Equals("0"))
                Response.Redirect("index.aspx");
            //mostrar os dados do livro
            if (!IsPostBack)
            {
                try
                {
                    int nlivro = int.Parse(Request["nlivro"].ToString());
                    DataTable dados = BaseDados.Instance.devolveDadosLivro(nlivro);
                    if (dados == null || dados.Rows.Count == 0)
                        throw new Exception("Erro");
                    lbNome.Text = dados.Rows[0]["nome"].ToString();
                    lbAno.Text = dados.Rows[0]["ano"].ToString();
                    lbData.Text = DateTime.Parse(dados.Rows[0]["data_aquisicao"].ToString()).ToShortDateString();
                    lbPreco.Text = String.Format("{0:C}", Decimal.Parse(dados.Rows[0]["preco"].ToString()));
                    lbEstado.Text = dados.Rows[0]["estado"].ToString();
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

        protected void btEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int nlivro = int.Parse(Request["nlivro"].ToString());
                BaseDados.Instance.removerLivro(nlivro);
                //apagar ficheiro
                string ficheiro = Server.MapPath(@"~\Imagens\" + nlivro + ".jpg");
                File.Delete(ficheiro);
            }
            catch
            {

            }
            Response.Redirect("areaadmin.aspx");
        }

        protected void btVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("areaadmin.aspx");
        }
    }
}