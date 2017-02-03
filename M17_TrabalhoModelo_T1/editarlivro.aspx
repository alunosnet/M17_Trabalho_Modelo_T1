<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="editarlivro.aspx.cs" Inherits="M17_TrabalhoModelo_T1.editarlivro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Editar Livro</h1>
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
        <asp:Image ID="imgCapa" runat="server" Width="100" /><br />
        <div class="form-group">
            <label for="fuCapa">Capa</label>
            <asp:FileUpload runat="server" ID="fuCapa" CssClass="form-control" />
        </div>
        <asp:Button runat="server" ID="btAtualizarLivro" Text="Atualizar" CssClass="btn btn-success" OnClick="btAtualizarLivro_Click" />
        <asp:Button runat="server" ID="btVoltar" Text="Voltar" CssClass="btn btn-info" OnClick="btVoltar_Click" />
        <asp:Label runat="server" ID="lbErroLivro"></asp:Label>
</asp:Content>
