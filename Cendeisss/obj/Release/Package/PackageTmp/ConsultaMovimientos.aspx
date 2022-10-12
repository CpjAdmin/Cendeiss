<%@ Page Title="" Language="C#" MasterPageFile="~/MasterCC.Master" AutoEventWireup="true" CodeBehind="ConsultaMovimientos.aspx.cs" Inherits="Cendeisss.ConsultaMovimientos" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="resources/components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css">
    <link rel="stylesheet" href="resources/components/datatables-buttons/buttons.dataTables.min.css">

    <link rel="stylesheet" href="resources/v0001/css/movimientos_v1.1.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <section class="content-header">
            <h1>Consulta de Movimientos
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
                    <span class="label label-danger">Parametros</span>
                </div>
            </div>
            <div class="box-body">
                Este módulo permite consultar los movimientos en un rango de fechas determinado, presionando <b>Consultar</b>.
                <p class="text-danger">Para consultas masivas, rango permitido es de 1 Mes.</p>
                <hr>
                <div class="row">
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label>Fecha Inicial:</label>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right datepicker" id="fechaInicial" readonly="readonly">
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-2">
                        <div class="form-group">
                            <label>Fecha Final:</label>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right datepicker" id="fechaFinal" readonly="readonly">
                            </div>
                        </div>
                    </div>
                     <div runat="server" id="optConsultaMovimientosUsuario" class="col-xs-2">
                        <div class="form-group">
                            <label>Asociado:</label>
                            <input id="txtUsuario" type="text" class="form-control" placeholder="Cédula...">
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-footer">
                <button id="btnEjecutarConsulta" class="btn btn-primary">Consultar</button>
                <button type="submit" id="btnRefrescar" style="border-color: black;" class="btn btn-default box-danger">Refrescar</button>
                <%--<p id="textoCierre" style="display: inline-block; padding-left: 20px; color: darkred;"></p>--%>
                <button id="btnLimpiar" class="btn btn-danger pull-right">Limpiar Tabla</button>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="box box-primary">
                    <div class="tab-pane active">
                        <div class="box">
                            <div class="box-body table-responsive">
                                <table id="tbl_movimientos" class="table nowrap table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th title="CEDULA">CEDULA</th>
                                            <th title="NOMBRE">NOMBRE</th>
                                            <th title="APORTES">APORTES</th>
                                            <th title="RENDIMIENTOS">RENDIMIENTOS</th>
                                            <th title="TIPO">TIPO</th>
                                            <th title="DESCRIPCION">DESCRIPCION</th>
                                            <th title="DOCUMENTO">DOCUMENTO</th>
                                            <th title="FECHA">FECHA</th>
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

    <script src="resources/components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script src="resources/components/datatables-buttons/dataTables.buttons.min.js"></script>
    <script src="resources/components/datatables-buttons/buttons.flash.min.js"></script>
    <script src="resources/components/datatables-buttons/jszip.min.js"></script>
    <script src="resources/components/datatables-buttons/pdfmake.min.js"></script>
    <script src="resources/components/datatables-buttons/vfs_fonts.js"></script>
    <script src="resources/components/datatables-buttons/buttons.html5_1.0.3.min.js"></script>
    <script src="resources/components/datatables-buttons/buttons.print.min.js"></script>

    <script src="resources/v0001/js/barra_progreso.min.js"></script>
    <script src="resources/v0001/js/movimientos_v1.min.js" type="text/javascript"></script>

</asp:Content>
