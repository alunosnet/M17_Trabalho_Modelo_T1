using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
                divConsultas.Visible = false;
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

            //grelha dos empréstimos
            gvEmprestimos.RowCommand += new GridViewCommandEventHandler(gvEmprestimos_RowCommand);
        }

        private void gvEmprestimos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int linha = int.Parse(e.CommandArgument as string);
            int id = int.Parse(gvEmprestimos.Rows[linha].Cells[2].Text);
            if (e.CommandName == "receber")
            {
                BaseDados.Instance.concluirEmprestimo(id);
                atualizaGrelhaEmprestimos();
                atualizaDDLivros();
            }
            if (e.CommandName == "email")
            {
                DataTable dadosEmprestimo = BaseDados.Instance.devolveDadosEmprestimo(id);
                int idUtilizador = int.Parse(dadosEmprestimo.Rows[0]["idutilizador"].ToString());
                DataTable dadosUtilizador = BaseDados.Instance.devolveDadosUtilizador(idUtilizador);
                string email = dadosUtilizador.Rows[0]["email"].ToString();
                string password = ConfigurationManager.AppSettings["senha"].ToString();
                Helper.enviarMail("alunosnet@gmail.com", password, email, "Livro emprestado",
                    "Caro leitor deve devolver o livro que tem emprestado");
            }
        }

        #region Livros
        private void gvLivros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLivros.PageIndex = e.NewPageIndex;
            atualizaGrelhaLivros();
        }
        protected void btLivros_Click(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            divLivros.Visible = true;
            divUtilizadores.Visible = false;
            divEmprestimos.Visible = false;
            divConsultas.Visible = false;
            btLivros.CssClass = "btn btn-info active";
            btUtilizador.CssClass = "btn btn-info";
            btEmprestimos.CssClass = "btn btn-info";
            btConsultas.CssClass = "btn btn-info";
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
            int rand = new Random().Next(999999999);
            ifCapa.DataImageUrlFormatString = "~/Imagens/{0}.jpg?"+rand;
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
            Response.CacheControl = "no-cache";
            divLivros.Visible = false;
            divUtilizadores.Visible = true;
            divEmprestimos.Visible = false;
            divConsultas.Visible = false;
            btLivros.CssClass = "btn btn-info";
            btUtilizador.CssClass = "btn btn-info active";
            btEmprestimos.CssClass = "btn btn-info";
            btConsultas.CssClass = "btn btn-info";
            atualizaGrelhaUtilizadores();
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
                Response.Redirect("historicoLeitor.aspx?id=" + id);
            }
        }

        private void gvUtilizadores_RowCreated(object sender, GridViewRowEventArgs e)
        {
            foreach (TableCell coluna in e.Row.Cells)
            {
                if (coluna.Text != "" && coluna.Text != "&nbsp;" && coluna.Text != "Ativar/Desativar"
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
        #region empréstimos
        protected void btEmprestimos_Click(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            divLivros.Visible = false;
            divUtilizadores.Visible = false;
            divConsultas.Visible = false;
            divEmprestimos.Visible = true;
            btLivros.CssClass = "btn btn-info";
            btUtilizador.CssClass = "btn btn-info";
            btEmprestimos.CssClass = "btn btn-info active";
            btConsultas.CssClass = "btn btn-info";
            atualizaGrelhaEmprestimos();
            atualizaDDLivros();
            atualizaDDLeitores();
        }

        private void atualizaDDLeitores()
        {
            ddUtilizador.Items.Clear();

            DataTable dados = BaseDados.Instance.listaUtilizadoresDisponiveis();
            foreach(DataRow leitor in dados.Rows)
            {
                ddUtilizador.Items.Add(new ListItem(leitor[2].ToString(),
                    leitor[0].ToString()));
            }
        }

        private void atualizaDDLivros()
        {
            ddLivro.Items.Clear();
            DataTable dados = BaseDados.Instance.listaLivrosDisponiveis();
            foreach(DataRow livro in dados.Rows)
            {
                ddLivro.Items.Add(new ListItem(livro[1].ToString(),
                    livro[0].ToString()));
            }
        }

        private void atualizaGrelhaEmprestimos()
        {
            gvEmprestimos.Columns.Clear();
            gvEmprestimos.DataSource = null;
            gvEmprestimos.DataBind();

            DataTable dados;

            if (cbEmprestimosPorConcluir.Checked)
                dados = BaseDados.Instance.listaTodosEmprestimosPorConcluirComNomes();
            else
                dados = BaseDados.Instance.listaTodosEmprestimosComNomes();
            if (dados == null || dados.Rows.Count == 0) return;

            //receber livro
            ButtonField bfReceberLivro = new ButtonField();
            bfReceberLivro.HeaderText = "Receber livro";
            bfReceberLivro.Text = "Receber";
            bfReceberLivro.ButtonType = ButtonType.Button;
            bfReceberLivro.CommandName = "receber";
            gvEmprestimos.Columns.Add(bfReceberLivro);

            //enviar email
            ButtonField bfEnviarEmail = new ButtonField();
            bfEnviarEmail.HeaderText = "Enviar email";
            bfEnviarEmail.Text = "Email";
            bfEnviarEmail.ButtonType = ButtonType.Button;
            bfEnviarEmail.CommandName = "email";
            gvEmprestimos.Columns.Add(bfEnviarEmail);

            gvEmprestimos.DataSource = dados;
            gvEmprestimos.AutoGenerateColumns = true;
            gvEmprestimos.DataBind();
        }

        protected void btAdicionarEmprestimo_Click(object sender, EventArgs e)
        {
            try
            {
                int idLeitor = int.Parse(ddUtilizador.SelectedValue);
                int idLivro = int.Parse(ddLivro.SelectedValue);
                DateTime data = clData.SelectedDate;
                BaseDados.Instance.adicionarEmprestimo(idLivro, idLeitor, data);
                atualizaGrelhaEmprestimos();
                atualizaDDLivros();
            }catch(Exception erro)
            {
                lbErroEmprestimo.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErroEmprestimo.CssClass = "alert alert-danger";
            }
        }
        #endregion

        protected void cbEmprestimosPorConcluir_CheckedChanged(object sender, EventArgs e)
        {
            atualizaGrelhaEmprestimos();
        }

        protected void btConsultas_Click(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            divLivros.Visible = false;
            divUtilizadores.Visible = false;
            divEmprestimos.Visible = false;
            divConsultas.Visible = true;
            btLivros.CssClass = "btn btn-info";
            btUtilizador.CssClass = "btn btn-info";
            btEmprestimos.CssClass = "btn btn-info";
            btConsultas.CssClass = "btn btn-info active";
        }

        protected void ddEscolhaConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            atualizaGrelhaConsultas();
        }

        private void atualizaGrelhaConsultas()
        {
            gvConsultas.Columns.Clear();
            int iconsulta = int.Parse(ddEscolhaConsulta.SelectedValue);
            DataTable dados;
            string sql = "";
            switch (iconsulta)
            {
                case 1: sql = @"SELECT utilizadores.nome,count(*) as n_emprestimos
                                FROM utilizadores inner join emprestimos
                                ON utilizadores.id=emprestimos.idutilizador
                                GROUP by emprestimos.idutilizador,nome
                                ORDER by n_emprestimos DESC";
                    break;
                case 2: sql = @"SELECT nome,count(*) as n_emprestimos
                                FROM livros inner join emprestimos
                                ON livros.nlivro=emprestimos.nlivro
                                GROUP BY emprestimos.nlivro,nome
                                ORDER BY n_emprestimos DESC";
                    break;
                case 3: sql = @"SELECT nemprestimo,livros.nome as nome_livro,
                            utilizadores.nome as nome_leitor,data_devolve 
                            FROM emprestimos inner join livros on 
                            emprestimos.nlivro=livros.nlivro
                            inner join utilizadores on emprestimos.idutilizador=utilizadores.id
                            WHERE data_devolve<getdate() AND emprestimos.estado=1";
                    break;
                case 4: sql = @"SELECT email,nome FROM utilizadores WHERE online=1";
                    break;
            }
            dados = BaseDados.Instance.devolveConsulta(sql);
            gvConsultas.DataSource = dados;
            gvConsultas.DataBind();
        }
    }
}