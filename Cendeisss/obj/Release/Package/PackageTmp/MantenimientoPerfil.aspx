<%@ Page Title="" Language="C#" MasterPageFile="~/MasterCC.Master" AutoEventWireup="true" CodeBehind="MantenimientoPerfil.aspx.cs" Inherits="Cendeisss.MantenimientoPerfil" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="resources/components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css">
    <link rel="stylesheet" href="resources/v0001/css/mantenimiento_perfil_v1.1.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <section class="content-header">
            <h1>Perfil de Usuario
          <small>CENDEISSS</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="Inicio.aspx"><i class="fa fa-home"></i>Inicio</a></li>
                <li><a href="Login.aspx"><i class="fa fa-close"></i>Salir</a></li>
            </ol>
        </section>
        <div class="box box-primary" style="margin-top: 10px; border-left: 1px solid; border-right: 1px solid; border-bottom: 1px solid;">
            <div class="box-body">
                <hr>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Identificación:</label>
                            <input id="txtId" runat="server" type="text" class="form-control">
                        </div>
                        <!-- /.form-group -->
                        <div class="form-group">
                            <label>Nombre:</label>
                            <input id="txtNombre" runat="server" type="text" maxLength="50" required pattern=".{15,50}" title="Longitud de 15 a 50 caracteres" class="form-control" style="text-transform: uppercase;" onkeyup="javascript:this.value=this.value.toUpperCase();">
                        </div>
                        <!-- /.form-group -->
                        <div class="form-group">
                            <label>Rol de Usuario:</label>
                            <input id="txtPerfilRol" runat="server" type="text" class="form-control">
                        </div>
                        <!-- /.form-group -->
                        <div class="form-group">
                            <label>Teléfono Habitación:</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-phone"></i>
                                </div>
                                <input type="text" runat="server" maxLength="8" required pattern="[0-9]{8,8}" title="Longitud de 8 numeros, Ejemplo: 22509090" id="txtTelHabitacion" class="form-control">
                            </div>
                        </div>
                        <!-- /.form-group -->
                        <div class="form-group">
                            <label>Teléfono Celular:</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-phone"></i>
                                </div>
                                <input type="text" runat="server" maxLength="8" required pattern="[0-9]{8,8}" title="Longitud de 8 numeros, Ejemplo: 22509090" id="txtTelCelular" class="form-control">
                            </div>
                        </div>
                        <!-- /.form-group -->
                        <div class="form-group">
                            <label>Teléfono Trabajo:</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-phone"></i>
                                </div>
                                <input type="text" runat="server" maxLength="8" required pattern="[0-9]{8,8}" title="Longitud de 8 numeros, Ejemplo: 22509090" id="txtTelTrabajo" class="form-control">
                            </div>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <!-- /.col -->

                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Fecha Nacimiento:</label>
                            <div class="input-group date">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input runat="server" type="text" required class="form-control pull-right" id="fechaNacimiento">
                            </div>
                        </div>
                        <div runat="server" id="correoP" class="form-group">
                            <label>Correo Principal:</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                <input runat="server" id="txtCorreoPrin" type="email" required class="form-control" placeholder="Email Primario">
                            </div>
                        </div>
                        <!-- /.form-group -->
                        <div runat="server" id="correoS" class="form-group">
                            <label>Correo Secundario:</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                <input runat="server" id="txtCorreoSec" type="email" required class="form-control" placeholder="Email Secundario">
                            </div>
                        </div>
                        <!-- /.form-group -->
                        <div class="form-group">
                            <label>PROVINCIA</label>
                            <asp:DropDownList ID="ddlProvincia" required="required" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                onchange="CargarCantones();">
                                <asp:ListItem Text="Seleccione un valor" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                        <div class="form-group">
                            <label>CANTÓN</label>
                            <asp:DropDownList ID="ddlCanton" required="required" runat="server" CssClass="form-control"
                                onchange="CargarDistritos();">
                                <asp:ListItem Text="Seleccione un valor" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                        <div class="form-group">
                            <label>DISTRITO</label>
                            <asp:DropDownList ID="ddlDistrito" required="required" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Seleccione un valor" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /.form-group -->
                    </div>
                    <!-- /.col -->
                    <div class="col-md-12">
                        <div class="form-group">
                            <label>Dirección Detallada:</label>
                            <textarea runat="server" id="txtOtrasS" class="form-control" rows="4" maxlength="200" placeholder="Otrase señas de la dirección..."></textarea>
                        </div>
                    </div>
                    <!-- /.col -->
                </div>
            </div>
        </div>
        <div class="box-footer">
            <button id="btnGuardarPerfil" class="btn btn-primary">GUARDAR PERFIL</button>
            <button id="btnCambiarClave" class="btn btn-danger pull-right" data-target="#modal_cambiarClave" data-toggle="modal" name="CENDEISSS">CAMBIAR CONTRASEÑA</button>
        </div>
    </div>
    <%--*********************************************************************** CAMBIAR CLAVE--%>
    <div class="modal fade modal-centrado" id="modal_cambiarClave" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document" style="width: 350px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" tabindex="-1" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h5 class="modal-title text-danger" id="modalTituloCambiarClave"></h5>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="text-black">Contraseña Nueva:</label>
                        <input id="ModalTxtClave1" type="password" class="form-control" maxlength="10" placeholder="Escriba la contraseña" onkeydown="BlodMayus(event,id)"
                            data-toggle="tooltip" data-placement="bottom" data-trigger="manual" title="Bloq Mayús Activa" />
                    </div>
                    <div class="form-group" style="padding-top: 30px;">
                        <label class="text-black">Confirmar Contraseña:</label>
                        <input id="ModalTxtClave2" type="password" class="form-control" maxlength="10" placeholder="Vuelva a escribir la contraseña" onkeydown="BlodMayus(event,id)"
                            data-toggle="tooltip" data-placement="bottom" data-trigger="manual" title="Bloq Mayús Activa" />
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="center">
                        <table>
                            <tr>
                                <td>
                                    <button type="button" class="btn btn-primary" id="btnModalClaveActualizar">Actualizar</button>
                                </td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <button type="button" class="btn btn-danger" id="btnModalClaveCancelar">Cancelar</button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script src="resources/components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script src="resources/components/inputmask/dist/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <script src="resources/v0001/js/mantenimiento_perfil_v1.min.js" type="text/javascript"></script>
</asp:Content>
