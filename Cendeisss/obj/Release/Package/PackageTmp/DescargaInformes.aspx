<%@ Page Title="" Language="C#" MasterPageFile="~/MasterCC.Master" AutoEventWireup="true" CodeBehind="DescargaInformes.aspx.cs" Inherits="Cendeisss.DescargaInformes" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <section class="content-header">
            <h1>Carga de Informes
          <small>CENDEISSS</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="Inicio.aspx"><i class="fa fa-home"></i>Inicio</a></li>
                <li><a href="Login.aspx"><i class="fa fa-close"></i>Salir</a></li>
            </ol>
        </section>
        <div class="box box-primary" style="margin-top: 10px; border-left: 1px solid; border-right: 1px solid; border-bottom: 1px solid;">
            <div class="box-header with-border">
                <h3 class="box-title">Descripción</h3>
                <div class="box-tools pull-right">
                    <span class="label label-danger">Informes</span>
                </div>
            </div>
            <div class="box-body">
                Este módulo permite cargar y descargar informes en formato <b>XLS/XLSX , DOC/DOCX y PDF</b>.
                <div class="row">
                    <div class="col-xs-5">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lbEstado" CssClass="text-danger text-bold" Text="" />
                            <asp:FileUpload ID="FileUploadControl" Width="450px" size="100" text="Cargar Archivo" runat="server" accept="application/vnd.ms-excel, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/msword,application/pdf"/>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-footer">
                <asp:Button ID="btnGuardarArchivo" runat="server" CssClass="btn btn-primary" Text="Cargar Archivo" OnClick="btnGuardarInforme_Click" />
                <p id="textoCierre" style="display: inline-block; padding-left: 20px; color: darkred;"></p>
                <button id="btnRefrescar" class="btn btn-danger pull-right">Refrescar</button>
            </div>
        </div>
        <asp:Button Style="display: none" ID="archivoMaster" runat="server" OnClick="btnDescargarInforme_Click" />
        <input id="archivoNombre" type="hidden" runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="box box-primary">
                    <div class="tab-pane active">
                        <div class="box">
                            <div class="box-body table-responsive">
                                <table id="tbl_informes" class="table nowrap table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th>NOMBRE DE ARCHIVO</th>
                                            <th>TIPO</th>
                                            <th>FECHA CREACIÓN</th>
                                            <th>FECHA MODIFICACIÓN</th>
                                            <th>TAMAÑO (KILOBYTES)</th>
                                            <th>DESCARGAR</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbl_body_table">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">

    <script src="resources/v0001/js/descarga_informes_v1.min.js"></script>

</asp:Content>
