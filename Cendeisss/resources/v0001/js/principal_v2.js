//************************************
var rol = $("#Rol").val();
var login = $("#Id").val();
var nombre = $("#Nombre").val();
//************************************
var clavesIguales;
var modalCambiarClave = $("#modal_cambiarClave");
var sistema = "";

var usuarioDigitado;

// ****************************************************************************************************************** ( INICIO CAMBIO CLAVE )
//******************************************************************************** FUNCIONES (INICIO)

// FN Salir
function salir() {
    window.close();
    return false;
}

// Cargar Menú de Mantenimiento
$(document).ready(function () {

    $(document).click(function (event) {
        var clickover = $(event.target);
        var _opened = $("#navbar-collapse").hasClass("in");
        if (_opened === true && !clickover.hasClass("navbar-toggle")) {
            $("button.navbar-toggle").click();
        }
    });

    //Menú de Mantenimiento
    menuMantenimiento();

    //Eventos
    $("#optEstadoCuenta").click(function (e) {

        e.preventDefault();

        if (rol !== "USUARIO") {

            procesoEstadoCuenta();

        } else {

            usuarioDigitado = $("#Id").val();
            procesarSolicitud(usuarioDigitado);
        }
    });

});

function menuMantenimiento() {

    if (rol === "ADMINISTRADOR") {

        // Contenido solo para administradores

    } else {

        $("#menuMantenimiento").remove();
        //$("#menuAuditoria").remove();
        //$("#menuMantenimiento").remove();
    }
}

$(document).on('click', '.btn-pendiente', function (e) {

    //Evitar el PostBack
    e.preventDefault();

    var MensajeAlerta = 'No tiene permisos para ingresar a este módulo!';
    var Titulo = e.currentTarget.id;
    mensajeConfirm(MensajeAlerta, Titulo, '', 'red', 'fa-lock');

});

function procesoEstadoCuenta() {

    //Estado de Cuenta 
    $.confirm({

        theme: 'material',
        title: 'ESTADO DE CUENTA',
        content: '' +
            '<form action="" class="formName">' +
            '<div class="form-group">' +
            '<label>Generar estado de cuenta del asociado</label>' +
            '<input type="text" id="usuarioIngresado" placeholder="CÉDULA DEL ASOCIADO" minlength="9" class="login InputUser form-control" required/>' +
            '</div>' +
            '</form>',
        columnClass: 'small',
        icon: 'fa fa-users',
        type: 'blue',
        draggable: true,
        dragWindowGap: 0,
        animation: 'top', /*--cambiar*/
        closeAnimation: 'scale',
        animationBounce: 2.5,
        animationSpeed: 600,
        escapeKey: 'cancelar',
        backgroundDismiss: false,

        //Botones
        buttons: {
            formSubmit: {
                text: 'GENERAR',
                btnClass: 'btn-primary',

                //Acción del Botón
                action: function () {

                    usuarioDigitado = $("#usuarioIngresado").val();

                    if (!usuarioDigitado) {

                        mensajeAlerta = 'Ingrese una identificación valida!';
                        titulo = 'Usuario Incorrecto!';

                        //Mostar Mensaje
                        mensaje('material', mensajeAlerta, titulo, 'fa fa-user-times', 'blue', 'medium');

                        return false;
                    } else {

                        procesarSolicitud(usuarioDigitado);
                    }
                }
            },
            //Cerrar
            cancelar: {
                text: 'Cerrar',
                btnClass: 'btn-red'
            }
        },
        onContentReady: function () {
            // Enlazar a eventos
            var jc = this;

            this.$content.find('form').on('submit', function (e) {
                // Si el usuario envía el formulario presionando Enter en el campo.
                e.preventDefault();
                // Referencia el botón e inicia evento clic
                jc.$$formSubmit.trigger('click');
            });

            $('.InputUser').bind('change keyup paste', function () {
                soloNumeros($(this));
            });
        }
    });
}

function procesarSolicitud(cedula) {
    
    var pdfMov = $("#pdfMov");
    pdfMov.val(cedula);

    $("#genEstadoCuenta").click();
}