<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="M17_TrabalhoModelo_T1.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Trabalho M17</h1>
    <div id="divLogin" runat="server" class="pull-right col-md-3 table-bordered text-center">
        Email:<asp:TextBox runat="server" ID="tbEmail" CssClass="form-control"></asp:TextBox>
        Password:<asp:TextBox runat="server" ID="tbPassword" CssClass="form-control" TextMode="Password"></asp:TextBox>
        <asp:Button ID="btLogin" runat="server" Text="Login" CssClass="btn btn-info" OnClick="btLogin_Click" />
        <asp:Button ID="btRecuperar" runat="server" Text="Recuperar Password" CssClass="btn btn-danger" OnClick="btRecuperar_Click" />
        <asp:Label ID="lbErro" runat="server"></asp:Label>
    </div>
    <div class="pull-left col-md-4 col-sm-4 input-group">
        <asp:TextBox  runat="server" ID="tbPesquisa" CssClass="form-control" />
        <span class="input-group-btn">
            <asp:Button Text="Pesquisar" ID="btPesquisa" runat="server" CssClass="btn btn-info" OnClick="btPesquisa_Click" />
        </span>
    </div>
    <div id="divLivros" runat="server" class="pull-left col-md-9"></div>
</asp:Content>
