<%@ Page Title="" Language="C#" MasterPageFile="~/MasterCC.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Cendeisss.Usuarios" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="resources/components/datatables-buttons/buttons.dataTables.min.css">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <section class="content-header">
            <h1>Mantenimiento de Usuarios
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
                Este módulo permite crear, modificar e inactivar los usuarios de la aplicación <b>CENDEISSS</b>.
            </div>
            <div class="box-footer">
                <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-primary" Text="Registrar Usuario" />
                <button id="btnRefrescar" class="btn btn-danger pull-right">Refrescar</button>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="box box-primary">
                    <div class="tab-pane active">
                        <div class="box">
                            <div class="box-body table-responsive">
                                <table id="tbl_usuarios" class="table nowrap table-bordered table-hover">
                                    <thead>
                                        <tr>
                                            <th title="USUARIO">USUARIO</th>
                                            <th title="NOMBRE">NOMBRE</th>
                                            <th title="ROL">ROL</th>
                                            <th title="EMAIL">EMAIL</th>
                                            <th title="ESTADO">ESTADO</th>
                                            <th title="ULTIMO INGRESO">ULT. INGRESO</th>
                                            <th title="EDITAR">*</th>
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
    <div class="modal fade" id="modal_actualizar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document" style="width: 340px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title negrita" id="modalTitulo"></h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>USUARIO</label>
                        <input id="ModalTxtUsuario" class="form-control InputUser" readonly oninput="this.value = this.value.toUpperCase()"/>
                    </div>
                    <div class="form-group">
                        <label>NOMBRE</label>
                        <input id="ModalTxtNombre" class="form-control" oninput="this.value = this.value.toUpperCase()" />
                    </div>
                    <div class="form-group">
                        <label>ROL</label>
                        <asp:DropDownList ID="ModalDdlRol" runat="server" CssClass="form-control">
                            <asp:ListItem Text="USUARIO" Value="1" />
                            <asp:ListItem Text="USUARIO AVANZADO" Value="3" />
                            <asp:ListItem Text="ADMINISTRADOR" Value="2" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>EMAIL</label>
                        <input id="ModalTxtEmail" class="form-control" type="email"/>
                    </div>
                    <div class="form-group">
                        <label>CLAVE</label>
                          <input id="ModalTxtClave" class="form-control" type="Password" />
                    </div>
                    <div class="form-group">
                        <label>ESTADO</label>
                        <asp:DropDownList ID="ModalDdlEstado" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Activo" Value="S" />
                            <asp:ListItem Text="Inactivo" Value="N" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="center">
                        <table>
                            <tr>
                                <td>
                                    <button runat="server" type="button" class="btn btn-primary" id="btnModalProcesar">Procesar</button>
                                </td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td>
                                    <button type="button" class="btn btn-danger" id="btnModalCancelar">Cancelar</button>
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

    <script src="resources/components/datatables-buttons/dataTables.buttons.min.js"></script>
    <script src="resources/components/datatables-buttons/buttons.flash.min.js"></script>
    <script src="resources/components/datatables-buttons/jszip.min.js"></script>
    <script src="resources/components/datatables-buttons/pdfmake.min.js"></script>
    <script src="resources/components/datatables-buttons/vfs_fonts.js"></script>
    <script src="resources/components/datatables-buttons/buttons.html5_1.0.3.min.js"></script>

    <script src="resources/v0001/js/barra_progreso.min.js"></script>
    <script src="resources/v0001/js/usuarios_v1.min.js"></script>

</asp:Content>
