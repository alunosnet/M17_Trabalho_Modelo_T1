<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="areaadmin.aspx.cs" Inherits="M17_TrabalhoModelo_T1.areaadmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Área admin</h1>
    <div class="btn-group">
        <asp:Button runat="server" ID="btLivros" Text="Gerir Livros" CssClass="btn btn-info" OnClick="btLivros_Click" />
        <asp:Button runat="server" ID="btUtilizador" Text="Gerir Utilizadores" CssClass="btn btn-info" OnClick="btUtilizador_Click" />
        <asp:Button runat="server" ID="btEmprestimos" Text="Gerir Empréstimos" CssClass="btn btn-info" OnClick="btEmprestimos_Click" />
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
    <div id="divUtilizadores" runat="server">
        <h2>Utilizadores</h2>
        <asp:GridView runat="server" ID="gvUtilizadores" CssClass="table table-responsive"></asp:GridView>
        <h3>Adicionar</h3>
        <div class="form-group">
            <label for="tbEmail">Email</label>
            <asp:TextBox runat="server" ID="tbEmail" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="tbNomeUtilizador">Nome</label>
            <asp:TextBox runat="server" ID="tbNomeUtilizador" CssClass="form-control"></asp:TextBox>
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
            <label for="tbPassword">Password</label>
            <asp:TextBox runat="server" ID="tbPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="cbEstado">Estado</label>
            <asp:CheckBox runat="server" ID="cbEstado" CssClass="form-control"></asp:CheckBox>
        </div>
        <div class="form-group">
            <label for="ddPerfil">Perfil</label>
            <asp:DropDownList runat="server" ID="ddPerfil" CssClass="form-control">
                <asp:ListItem Value="0">Administrador</asp:ListItem>
                <asp:ListItem Value="1" Selected="True">Leitor</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Button OnClick="btAdicionarUtilizador_Click" runat="server" ID="btAdicionarUtilizador" Text="Adicionar" CssClass="btn btn-success" />
        <asp:Label runat="server" ID="lbErroUtilizador"></asp:Label>
    </div>
    <div id="divEmprestimos" runat="server">
        <h2>Empréstimo</h2>
        <asp:CheckBox runat="server" ID="cbEmprestimosPorConcluir" />
        <asp:GridView runat="server" ID="gvEmprestimos"></asp:GridView>
        <h3>Adicionar</h3>
        <div class="form-group">
            <label for="ddLivro">Livro:</label>
            <asp:DropDownList runat="server" ID="ddLivro" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="ddUtilizador">Leitor:</label>
            <asp:DropDownList runat="server" ID="ddUtilizador" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="clData">Data de Devolução:</label>
            <asp:Calendar runat="server" ID="clData"></asp:Calendar>
        </div>
        <asp:Button runat="server" ID="btAdicionarEmprestimo" Text="Adicionar" CssClass="btn btn-success" OnClick="btAdicionarEmprestimo_Click" />
        <asp:Label runat="server" ID="lbErroEmprestimo"></asp:Label>
    </div>
</asp:Content>
