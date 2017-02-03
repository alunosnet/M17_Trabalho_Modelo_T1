<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="areaadmin.aspx.cs" Inherits="M17_TrabalhoModelo_T1.areaadmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Área admin</h1>
    <div class="btn-group">
        <asp:Button runat="server" ID="btLivros" Text="Gerir Livros" CssClass="btn btn-info" OnClick="btLivros_Click" />
        <asp:Button runat="server" ID="btUtilizador" Text="Gerir Utilizadores" CssClass="btn btn-info" />
        <asp:Button runat="server" ID="btEmprestimos" Text="Gerir Empréstimos" CssClass="btn btn-info" />
    </div>
    <div id="divLivros" runat="server">
        <h2>Livros</h2>
        <asp:GridView runat="server" ID="gvLivros" CssClass="table table-responsive"></asp:GridView>
        <h3>Adicionar</h3>
        <div class="form-group">
            <label for="tbNomeLivro">Nome</label>
            <asp:TextBox runat="server" ID="tbNomeLivro" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="tbAno">Ano</label>
            <asp:TextBox runat="server" ID="tbAno" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="tbData">Data Aquisição</label>
            <asp:TextBox runat="server" ID="tbData" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="tbPreco">Preço</label>
            <asp:TextBox runat="server" ID="tbPreco" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="fuCapa">Capa</label>
            <asp:FileUpload runat="server" ID="fuCapa" CssClass="form-control" />
        </div>
        <asp:Button runat="server" ID="btAdicionarLivro" Text="Adicionar" CssClass="btn btn-info" OnClick="btAdicionarLivro_Click" />
        <asp:Label runat="server" ID="lbErroLivro"></asp:Label>
    </div>
    <div id="divUtilizadores" runat="server"></div>
    <div id="divEmprestimos" runat="server"></div>
</asp:Content>
