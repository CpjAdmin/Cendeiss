<%@ Page Title="" Language="C#" MasterPageFile="~/MasterCC.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Cendeisss.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="resources/v0001/css/inicio.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <section class="content-header">
            <h1>Inicio
          <small><b>ÚLTIMO INGRESO:</b>&nbsp;&nbsp;<%= Session["UltimoIngreso"] %></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="Login.aspx"><i class="fa fa-close"></i>Salir</a></li>
            </ol>
        </section>
    </div>
</asp:Content>
