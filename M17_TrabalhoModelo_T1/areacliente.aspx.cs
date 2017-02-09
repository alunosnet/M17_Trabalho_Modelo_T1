using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace M17_TrabalhoModelo_T1
{
    public partial class areacliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["perfil"] == null)
                Response.Redirect("index.aspx");
            //verificar se é a primeira vez que a página é pedida
            if (!IsPostBack)
            {
                divDevolver.Visible = false;
                divEmprestimo.Visible = false;
                divHistorico.Visible = false;
            }
            //eventos
            gvEmprestar.RowCommand += new GridViewCommandEventHandler(gvEmprestar_RowCommand);
            gvDevolver.RowCommand += new GridViewCommandEventHandler(gvDevolver_RowCommand);
        }

        private void gvEmprestar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int linha = int.Parse(e.CommandArgument as string);
            int id = int.Parse(gvEmprestar.Rows[linha].Cells[1].Text);
            int idUtilizador = int.Parse(Session["id"].ToString());
            if (e.CommandName == "emprestar")
            {
                DateTime data = DateTime.Now.AddDays(7);
                BaseDados.Instance.adicionarEmprestimo(id, idUtilizador, data);
                atualizaGrelhaEmprestimo();
            }
        }

        private void gvDevolver_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int linha = int.Parse(e.CommandArgument as string);
            int id = int.Parse(gvDevolver.Rows[linha].Cells[1].Text);
            if (e.CommandName == "devolver")
            {
                BaseDados.Instance.concluirEmprestimo(id);
                Response.CacheControl = "no-cache";
                atualizaGrelhaDevolve();
            }
        }

        protected void btEmprestimo_Click(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            divEmprestimo.Visible = true;
            divDevolver.Visible = false;
            divHistorico.Visible = false;
            btEmprestimo.CssClass = "btn btn-info active";
            btDevolve.CssClass = "btn btn-info";
            btHistorico.CssClass = "btn btn-info";
            atualizaGrelhaEmprestimo();
        }
        protected void btHistorico_Click(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            divEmprestimo.Visible = false;
            divDevolver.Visible = false;
            divHistorico.Visible = true;
            btEmprestimo.CssClass = "btn btn-info";
            btDevolve.CssClass = "btn btn-info";
            btHistorico.CssClass = "btn btn-info active";
            atualizaGrelhaHistorico();
        }
        protected void btDevolve_Click(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            divEmprestimo.Visible = false;
            divDevolver.Visible = true;
            divHistorico.Visible = false;
            btEmprestimo.CssClass = "btn btn-info";
            btDevolve.CssClass = "btn btn-info active";
            btHistorico.CssClass = "btn btn-info";
            atualizaGrelhaDevolve();
        }
        private void atualizaGrelhaEmprestimo()
        {
            gvEmprestar.Columns.Clear();
            gvEmprestar.DataSource = null;
            gvEmprestar.DataBind();

            gvEmprestar.DataSource = BaseDados.Instance.listaLivrosDisponiveis();

            //botão emprestar
            ButtonField btEmprestar = new ButtonField();
            btEmprestar.HeaderText = "Fazer empréstimo";
            btEmprestar.Text = "Emprestar";
            btEmprestar.ButtonType = ButtonType.Button;
            btEmprestar.CommandName = "emprestar";
            gvEmprestar.Columns.Add(btEmprestar);

            gvEmprestar.DataBind();
        }
        private void atualizaGrelhaDevolve()
        {
            gvDevolver.Columns.Clear();
            gvDevolver.DataSource = null;
            gvDevolver.DataBind();

            int idUtilizador = int.Parse(Session["id"].ToString());
            gvDevolver.DataSource = BaseDados.Instance.listaTodosEmprestimosPorConcluirComNomes(idUtilizador);

            //botão devolver
            ButtonField btDevolver = new ButtonField();
            btDevolver.HeaderText = "Devolver";
            btDevolver.Text = "Devolver";
            btDevolver.ButtonType = ButtonType.Button;
            btDevolver.CommandName = "devolver";
            gvDevolver.Columns.Add(btDevolver);

            gvDevolver.DataBind();
        }
        private void atualizaGrelhaHistorico()
        {
            gvHistorico.Columns.Clear();

            int idUtilizador = int.Parse(Session["id"].ToString());
            gvHistorico.DataSource = BaseDados.Instance.listaTodosEmprestimosComNomes(idUtilizador);
            gvHistorico.DataBind();
        }
    }
}