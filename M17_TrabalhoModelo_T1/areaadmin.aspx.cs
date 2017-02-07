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

            ///////////////////////////////////////////////////EVENTOS
            //paginação da grelha dos livros
            gvLivros.PageSize = 5;
            gvLivros.PageIndexChanging += new GridViewPageEventHandler(gvLivros_PageIndexChanging);

            //grelha dos utilizadores
            gvUtilizadores.RowDeleting += new GridViewDeleteEventHandler(gvUtilizadores_RowDeleting);
            gvUtilizadores.RowEditing += new GridViewEditEventHandler(gvUtilizadores_RowEditing);
            gvUtilizadores.RowCancelingEdit += new GridViewCancelEditEventHandler(gvUtilizadores_RowCancelingEdit);
            gvUtilizadores.RowUpdating += new GridViewUpdateEventHandler(gvUtilizadores_RowUpdating);
            gvUtilizadores.RowCreated += new GridViewRowEventHandler(gvUtilizadores_RowCreated);
            gvUtilizadores.RowCommand += new GridViewCommandEventHandler(gvUtilizadores_RowCommand);
        }

        private void gvUtilizadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int linha = int.Parse(e.CommandArgument as string);
            int id = int.Parse(gvUtilizadores.Rows[linha].Cells[3].Text);
            if (e.CommandName == "estado")
            {
                BaseDados.Instance.ativarDesativarUtilizador(id);
                atualizaGrelhaUtilizadores();
            }
            if (e.CommandName == "histórico")
            {
                //TODO: mostrar histórico do leitor
            }
        }

        private void gvUtilizadores_RowCreated(object sender, GridViewRowEventArgs e)
        {
            foreach(TableCell coluna in e.Row.Cells)
            {
                if(coluna.Text!="" && coluna.Text!="&nbsp;" && coluna.Text!="Ativar/Desativar"
                    && coluna.Text != "Histórico")
                {
                    BoundField campo = (BoundField)((DataControlFieldCell)coluna).ContainingField;
                    if (campo.DataField == "id" || campo.DataField == "estado" || campo.DataField == "perfil")
                        campo.ReadOnly = true;
                }
            }
        }

        private void gvUtilizadores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int linha = e.RowIndex;
            int id = int.Parse(gvUtilizadores.Rows[linha].Cells[3].Text);
            string email = ((TextBox)gvUtilizadores.Rows[linha].Cells[3].Controls[0]).Text;
            string nome = ((TextBox)gvUtilizadores.Rows[linha].Cells[4].Controls[0]).Text;
            string morada = ((TextBox)gvUtilizadores.Rows[linha].Cells[5].Controls[0]).Text;
            string nif = ((TextBox)gvUtilizadores.Rows[linha].Cells[6].Controls[0]).Text;
            //validar dados
            //atualizar a bd
            BaseDados.Instance.atualizarUtilizador(id, nome, email, morada, nif);
            gvUtilizadores.EditIndex = -1;
            atualizaGrelhaUtilizadores();
        }

        private void gvUtilizadores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUtilizadores.EditIndex = -1;
            atualizaGrelhaUtilizadores();
        }

        private void gvUtilizadores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int linha = e.NewEditIndex;
            gvUtilizadores.EditIndex = linha;
            atualizaGrelhaUtilizadores();
        }

        private void gvUtilizadores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //eliminar utilizador
            int linha = e.RowIndex;
            string id = gvUtilizadores.Rows[linha].Cells[3].Text;
            BaseDados.Instance.removerUtilizador(int.Parse(id));
            atualizaGrelhaUtilizadores();
        }

        #region Livros
        private void gvLivros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLivros.PageIndex = e.NewPageIndex;
            atualizaGrelhaLivros();
        }
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
                if (fuCapa.PostedFile.ContentLength > 0 &&
                    fuCapa.PostedFile.ContentType == "image/jpeg")
                {
                    string ficheiro = Server.MapPath(@"~\Imagens\");
                    ficheiro += id + ".jpg";
                    fuCapa.SaveAs(ficheiro);
                }

                tbNomeLivro.Text = "";
                tbAno.Text = "";
                tbData.Text = "";
                tbPreco.Text = "";
                atualizaGrelhaLivros();
            }
            catch (Exception erro)
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
        #region utilizadores
        protected void btAdicionarUtilizador_Click(object sender, EventArgs e)
        {
            try
            {
                string email = tbEmail.Text;
                if (email.Contains("@") == false)
                    throw new Exception("O email indicado não é válido");
                string nome = tbNomeUtilizador.Text;
                string morada = tbMorada.Text;
                string nif = tbNIF.Text;
                string password = tbPassword.Text;
                int perfil = int.Parse(ddPerfil.SelectedValue);
                int estado = cbEstado.Checked == true ? 1 : 0;
                //guardar na bd
                BaseDados.Instance.registarUtilizador(email, nome, morada, nif, password, estado, perfil);
                //atualizar grelha
                atualizaGrelhaUtilizadores();
                //limpar o form
                tbEmail.Text = "";
                tbNomeUtilizador.Text = "";
                tbMorada.Text = "";
                tbNIF.Text="";
            }catch(Exception erro)
            {
                lbErroUtilizador.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErroUtilizador.CssClass = "alert alert-danger";
            }
        }

        protected void btUtilizador_Click(object sender, EventArgs e)
        {
            divLivros.Visible = false;
            divUtilizadores.Visible = true;
            divEmprestimos.Visible = false;
            Response.CacheControl = "no-cache";
            btLivros.CssClass = "btn btn-info";
            btUtilizador.CssClass = "btn btn-info active";
            btEmprestimos.CssClass = "btn btn-info";
            atualizaGrelhaUtilizadores();
        }

        private void atualizaGrelhaUtilizadores()
        {
            gvUtilizadores.Columns.Clear();
            gvUtilizadores.DataSource = null;
            gvUtilizadores.DataBind();

            DataTable dados = BaseDados.Instance.listaUtilizadoresDisponiveis();
            if (dados == null || dados.Rows.Count == 0) return;

            gvUtilizadores.DataSource = dados;
            gvUtilizadores.AutoGenerateColumns = true;
            gvUtilizadores.AutoGenerateEditButton = true;
            gvUtilizadores.AutoGenerateDeleteButton = true;

            //coluna para ativar/desativar
            ButtonField btEstado = new ButtonField();
            btEstado.HeaderText = "Ativar/Desativar";
            btEstado.Text = "Ativar/Desativar";
            btEstado.ButtonType = ButtonType.Button;
            btEstado.CommandName = "estado";
            gvUtilizadores.Columns.Add(btEstado);

            //coluna para ver histórico
            ButtonField btHistorico = new ButtonField();
            btHistorico.HeaderText = "Histórico";
            btHistorico.Text = "Histórico";
            btHistorico.ButtonType = ButtonType.Button;
            btHistorico.CommandName = "histórico";
            gvUtilizadores.Columns.Add(btHistorico);

            gvUtilizadores.DataBind();
        }
        #endregion

        protected void btEmprestimos_Click(object sender, EventArgs e)
        {
            divLivros.Visible = false;
            divUtilizadores.Visible = false;
            divEmprestimos.Visible = true;
            Response.CacheControl = "no-cache";
            btLivros.CssClass = "btn btn-info";
            btUtilizador.CssClass = "btn btn-info";
            btEmprestimos.CssClass = "btn btn-info active";
            atualizaGrelhaEmprestimos();
            atualizaDDLivros();
            atualizaDDLeitores();
        }

        private void atualizaDDLeitores()
        {
            
        }

        private void atualizaDDLivros()
        {
            throw new NotImplementedException();
        }

        private void atualizaGrelhaEmprestimos()
        {
            
        }

        protected void btAdicionarEmprestimo_Click(object sender, EventArgs e)
        {

        }
    }
}