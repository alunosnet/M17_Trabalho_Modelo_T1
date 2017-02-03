<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="registo.aspx.cs" Inherits="M17_TrabalhoModelo_T1.registo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://www.google.com/recaptcha/api.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-group">
        <label for="tbEmail">Email</label>
        <asp:TextBox runat="server" ID="tbEmail"  CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="tbNome">Nome</label>
        <asp:TextBox runat="server" ID="tbNome" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="tbMorada">Morada</label>
        <asp:TextBox runat="server" ID="tbMorada" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="tbNIF">NIF</label>
        <asp:TextBox runat="server" ID="tbNIF" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="tbPassword">Palavra Passe:</label>
        <asp:TextBox TextMode="Password" runat="server" ID="tbPassword" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="g-recaptcha" data-sitekey="6Lc1vvoSAAAAAFjyIsG88_b-SoYcW5n89amtzucB"></div>
    <asp:Label ID="lbErro" runat="server" /><br />
    <asp:Button ID="btRegistar" runat="server" Text="Registar" CssClass="btn btn-info" OnClick="btRegistar_Click" />
</asp:Content>
