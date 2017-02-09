<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="areacliente.aspx.cs" Inherits="M17_TrabalhoModelo_T1.areacliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Área leitor</h1>
    <div class="btn-group">
        <asp:Button runat="server" ID="btEmprestimo" Text="Empréstimo" CssClass="btn btn-info" OnClick="btEmprestimo_Click" />
        <asp:Button runat="server" ID="btDevolve" Text="Devolver" CssClass="btn btn-info" OnClick="btDevolve_Click" />
        <asp:Button runat="server" ID="btHistorico" Text="Histórico" CssClass="btn btn-info" OnClick="btHistorico_Click" />
    </div>
    <div id="divEmprestimo" runat="server">
        <h2>Livros a emprestar</h2>
        <asp:GridView runat="server" ID="gvEmprestar" CssClass="table table-responsive"></asp:GridView>
    </div>
    <div id="divDevolver" runat="server">
        <h2>Livros emprestados</h2>
        <asp:GridView runat="server" ID="gvDevolver" CssClass="table table-responsive"></asp:GridView>
    </div>
    <div id="divHistorico" runat="server">
        <h2>Histórico</h2>
        <asp:GridView runat="server" ID="gvHistorico" CssClass="table table-responsive"></asp:GridView>
    </div>

</asp:Content>
