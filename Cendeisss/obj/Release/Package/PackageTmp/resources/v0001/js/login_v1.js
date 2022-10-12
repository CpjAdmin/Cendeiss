//************************************************** Recuperar Contraseña
var paginaUrl = 'Login.aspx';
var usuarioDigitado;

$(document).ready(function () {

    //*** tooltip
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-toggle="tooltip"]').tooltip().off("focusin focusout");

    $('.InputUser').bind('change keyup paste', function () {
        soloLetrasNumeros($(this));
    });

    //Agregado por Carlos Fonseca SOL271275 24/05/2019
    //Se agrega un metodo para generar un clave random y el metodo de enviar correo.
    $(document).on('click', '#btnEnviar', function () {
        usuarioDigitado = $("#ModalTxtUsuario").val();
        if (!usuarioDigitado) {
            mensajeAlerta = 'Ingrese un usuario valido!';
            titulo = 'Usuario Incorrecto';
            //Mostar Mensaje
            mensajeError(mensajeAlerta, titulo);
            return false;
        } else {
            recuperacionClaveUsuario(usuarioDigitado);
            $('#modal_enviarClave').modal('hide');
        }
    });

    $(document).on('click', '#passModal', function () {
        $("#ModalTxtUsuario").val('');
    });

    //FIN Agregado por Carlos Fonseca SOL271275 24/05/2019
});

//Agregado por Carlos Fonseca SOL271275 24/05/2019
function recuperacionClaveUsuario(usuarioDigitado) {

    // Guardar parámetros y usar Libreria de JSON
    var parametros = new Object();

    // Parametros Default
    parametros.id = usuarioDigitado;

    data = JSON.stringify(parametros);

    $.ajax({
        type: 'POST',
        url: paginaUrl + '/EnviarClaveEmail',
        data: data,
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {

            mensajeAlerta = xhr.statusText + '\n' + xhr.responseText + '\n' + thrownError;
            titulo = 'Recuperar Contraseña';

            //Mostar Mensaje
            mensajeError(mensajeAlerta, titulo);
        },
        success: function (data) {

            if (data.d.Error === "0") {
                MensajeAlerta = 'Contraseña enviada correctamente!. Recuerde cambiarla al ingresar al sitio.';
                titulo = 'Envio Exitoso!';

                //Mostar Mensaje
                mensajeConfirm(MensajeAlerta, titulo, '', '', '');

            } else {
                mensajeAlerta = data.d.Mensaje;
                titulo = 'Recuperar Contraseña';

                //Mostar Mensaje
                mensajeError(mensajeAlerta, titulo);
            }
        }
    });
}
//FIN Agregado por Carlos Fonseca SOL271275 24/05/2019


