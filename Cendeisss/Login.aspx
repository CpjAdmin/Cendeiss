<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Cendeisss.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link rel="shorcut icon" type="image/x-icon" href="resources/v0001/img/favicon.ico" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <title>CENDEISSS - Login</title>
    <link rel="stylesheet" href="resources/fonts/font-awesome-4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="resources/v0001/css/login_v1.min.css" />
    <link rel="stylesheet" href="resources/components/bootstrap/dist/css/bootstrap.min.css" type="text/css">
    <link rel="stylesheet" href="resources/plugins/jquery-confirm/jquery-confirm-master/dist/jquery-confirm.min.css">
    <link rel="stylesheet" href="resources/dist/css/AdminLTE.min.css" type="text/css">

    <script src="resources/components/jquery/dist/jquery.min.js" type="text/javascript"></script>
    <script src="resources/components/bootstrap/dist/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="resources/plugins/jquery-confirm/jquery-confirm-master/dist/jquery-confirm.min.js"></script>
    <script src="resources/v0001/js/publico_v1.min.js" type="text/javascript"></script>
    <script src="resources/v0001/js/login_v1.min.js" type="text/javascript"></script>

</head>
<body>
    <div class="main">
        <div class="containerLogin">
            <div class="">
                <div style='position: fixed; top: -100px; left: 50%;'>
                    <div id="mensajeAlerta"></div>
                </div>
                <div id="login">
                    <form id="formLogin" autocomplete="off" runat="server">
                        <div>
                            <div class="logo">
                                <img id="logo" class="img-fluid" src="resources/v0001/img/logo_cendeisss.png" alt="CENDEISSS" />
                                <div class="clearfix"></div>
                            </div>
                            <fieldset class="clearfix">
                                <p>
                                    <span class="fa fa-user"></span>
                                    <input id="usuario" type="text" runat="server" placeholder="Usuario" class="InputUser" required="required" oninput="this.value = this.value.toUpperCase()" />
                                </p>
                                <p>
                                    <span class="fa fa-lock"></span>
                                    <input type="password" id="password" name="con" runat="server" class="form-control input-block-level password"
                                        data-toggle="tooltip" data-placement="bottom" data-trigger="manual" title="Bloq Mayús Activa"
                                        placeholder="Contrase&ntilde;a" onkeydown="BlodMayus(event,id)" autocomplete="off" required>
                                </p>
                                <div style="padding-top: 25px;">
                                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click" />
                                </div>

                                <%--Modificado por Carlos Fonseca SOL271275 24/05/2019
                                Se agregar una etiqueta para llamar al modal que enviará el correo con la contraseña nueva.--%>
                                <div style="padding-top: 25px;">
                                    <p id="passModal" style="color: whitesmoke; font-weight: bold; text-align: center; cursor: pointer;" data-target="#modal_enviarClave" data-toggle="modal">Olvidó su Contraseña</p>
                                </div>
                                <%--FIN Modificado por Carlos Fonseca SOL271275 24/05/2019--%>
                            </fieldset>
                            <div class="clearfix"></div>
                        </div>
                    </form>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <%--Modificado por Carlos Fonseca SOL271275 24/05/2019
        Se agrega un modal para ingresar el usuario para enviarle el correo.--%>
        <div class="modal fade modal-centrado" id="modal_enviarClave" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document" style="width: 350px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" tabindex="-1" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h5 class="modal-title text-danger" id="modalTitulo" style="text-align: center;"><strong>RECUPERAR SU CONTRASEÑA</strong><ul class="nav navbar-nav">
                            <li class="dropdown user user-menu">
                                <img src="resources/v0001/img/usuario.png" class="user-image">
                            </li>
                        </ul></h5>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label class="text-black">Digite su usuario:</label>
                            <input id="ModalTxtUsuario" type="text" class="form-control InputUser" maxlength="20" placeholder="Número de cédula..." onkeydown="BlodMayus(event,id)"
                                data-toggle="tooltip" data-placement="bottom" data-trigger="manual" title="Bloq Mayús Activa" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="center">
                            <table>
                                <tr>
                                    <td>
                                        <button type="button" class="btn btn-primary" id="btnEnviar">Enviar</button>
                                    </td>
                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    <td>
                                        <button type="button" class="btn btn-danger" id="btnCancelar" data-dismiss="modal">Cancelar</button>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--Modificado por Carlos Fonseca SOL271275 24/05/2019 --%>
    </div>
</body>
</html>
