using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_TrabalhoModelo_T1
{
    public partial class areaadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["perfil"] == null || !Session["perfil"].Equals("0"))
                Response.Redirect("index.aspx");
            //esconder divs
            if (!IsPostBack)
            {
                divLivros.Visible = false;
                divUtilizadores.Visible = false;
                divEmprestimos.Visible = false;
            }

        }
#region Livros
        protected void btLivros_Click(object sender, EventArgs e)
        {
            divLivros.Visible = true;
            divUtilizadores.Visible = false;
            divEmprestimos.Visible = false;
            Response.CacheControl = "no-cache";
            btLivros.CssClass = "btn btn-info active";
            btUtilizador.CssClass = "btn btn-info";
            btEmprestimos.CssClass = "btn btn-info";
            atualizaGrelhaLivros();
        }
        protected void btAdicionarLivro_Click(object sender, EventArgs e)
        {
            try
            {
                string nomeLivro = tbNomeLivro.Text;
                int ano = int.Parse(tbAno.Text);
                DateTime data = DateTime.Parse(tbData.Text);
                decimal preco = Decimal.Parse(tbPreco.Text);
                if (nomeLivro == String.Empty)
                    throw new Exception("Indique o nome do livro");
                if (ano < 0 || ano > DateTime.Now.Year)
                    throw new Exception("O ano indicado não é válido.");
                if (preco < 0) throw new Exception("O preço não pode ser inferior a zero");
                //verificar se existe imagem
                if (fuCapa.HasFile == false) throw new Exception("Tem que indicar uma capa");
                int id = BaseDados.Instance.adicionarLivro(nomeLivro, ano, data, preco);
                //guardar imagem
                if(fuCapa.PostedFile.ContentLength>0 &&
                    fuCapa.PostedFile.ContentType == "image/jpeg")
                {
                    string ficheiro=Server.MapPath(@"~\Imagens\");
                    ficheiro += id + ".jpg";
                    fuCapa.SaveAs(ficheiro);
                }
                
                tbNomeLivro.Text = "";
                tbAno.Text = "";
                tbData.Text = "";
                tbPreco.Text = "";
                atualizaGrelhaLivros();
            }catch(Exception erro)
            {
                lbErroLivro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErroLivro.CssClass = "alert alert-danger";
            }
        }

        private void atualizaGrelhaLivros()
        {
            //limpar a gridview
            gvLivros.Columns.Clear();
            gvLivros.DataSource = null;
            gvLivros.DataBind();

            //consulta à bd
            DataTable dados = BaseDados.Instance.listaLivros();
            if (dados == null || dados.Rows.Count == 0) return;

            //adicionar colunas remover/editar
            DataColumn dcRemover = new DataColumn();
            dcRemover.ColumnName = "Remover";
            dcRemover.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcRemover);

            DataColumn dcEditar = new DataColumn();
            dcEditar.ColumnName = "Editar";
            dcEditar.DataType = Type.GetType("System.String");
            dados.Columns.Add(dcEditar);

            //configurar os campos da grid
            gvLivros.DataSource = dados;
            gvLivros.AutoGenerateColumns = false;
            //coluna remover
            HyperLinkField hlRemover = new HyperLinkField();
            hlRemover.HeaderText = "Remover";
            hlRemover.DataTextField = "Remover";
            hlRemover.Text = "Remover livro";
            hlRemover.DataNavigateUrlFormatString = "removerlivro.aspx?nlivro={0}";
            hlRemover.DataNavigateUrlFields = new string[] { "nlivro" };
            gvLivros.Columns.Add(hlRemover);
            //coluna editar
            HyperLinkField hlEditar = new HyperLinkField();
            hlEditar.HeaderText = "Editar";
            hlEditar.DataTextField = "Editar";
            hlEditar.Text = "Editar livro";
            hlEditar.DataNavigateUrlFormatString = "editarlivro.aspx?nlivro={0}";
            hlEditar.DataNavigateUrlFields = new string[] { "nlivro" };
            gvLivros.Columns.Add(hlEditar);
            //nlivro
            BoundField bfNLivro = new BoundField();
            bfNLivro.DataField = "nlivro";
            bfNLivro.HeaderText = "NºLivro";
            gvLivros.Columns.Add(bfNLivro);
            //nome
            BoundField bfNome = new BoundField();
            bfNome.DataField = "nome";
            bfNome.HeaderText = "Nome";
            gvLivros.Columns.Add(bfNome);
            //ano
            BoundField bfAno = new BoundField();
            bfAno.DataField = "ano";
            bfAno.HeaderText = "Ano";
            gvLivros.Columns.Add(bfAno);
            //data aquisição
            BoundField bfData = new BoundField();
            bfData.DataField = "data_aquisicao";
            bfData.HeaderText = "Data";
            gvLivros.Columns.Add(bfData);
            //preço
            BoundField bfPreco = new BoundField();
            bfPreco.DataField = "preco";
            bfPreco.HeaderText = "Preço";
            gvLivros.Columns.Add(bfPreco);
            //estado
            BoundField bfEstado = new BoundField();
            bfEstado.DataField = "estado";
            bfEstado.HeaderText = "Estado";
            gvLivros.Columns.Add(bfEstado);
            //capa
            ImageField ifCapa = new ImageField();
            ifCapa.HeaderText = "Capa";
            ifCapa.DataImageUrlFormatString = "~/Imagens/{0}.jpg";
            ifCapa.DataImageUrlField = "nlivro";
            ifCapa.ControlStyle.Width = 100;
            gvLivros.Columns.Add(ifCapa);

            //databind
            gvLivros.DataBind();
            
        }
        #endregion


    }
}