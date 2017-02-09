<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="historicoLeitor.aspx.cs" Inherits="M17_TrabalhoModelo_T1.historicoLeitor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="areaadmin.aspx">Voltar</a>
    <asp:GridView runat="server" ID="gvHistorico" CssClass="table table-responsive"></asp:GridView>
</asp:Content>
