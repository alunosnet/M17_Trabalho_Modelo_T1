<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="removerlivro.aspx.cs" Inherits="M17_TrabalhoModelo_T1.removerlivro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Remover livro</h1>
    Nome:<asp:Label ID="lbNome" runat="server" /><br />
    Ano:<asp:Label ID="lbAno" runat="server" /><br />
    Data Aquisição:<asp:Label ID="lbData" runat="server" /><br />
    Preço:<asp:Label ID="lbPreco" runat="server" /><br />
    Estado:<asp:Label ID="lbEstado" runat="server" /><br />
    <asp:Image ID="imgCapa" runat="server" Width="100" /><br />
    <asp:Button CssClass="btn btn-danger" ID="btEliminar" runat="server" Text="Eliminar" OnClick="btEliminar_Click" />
    <asp:Button CssClass="btn btn-info" ID="btVoltar" runat="server" Text="Voltar" OnClick="btVoltar_Click" />
</asp:Content>
