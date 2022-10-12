function Alerta(id, mensaje, tipo, duracionSegundos) {

    $("#" + id + "").html('<div class="alert alert-' + tipo + ' fade in alert-dismissible">' +
        '<button class="close" aria-hidden="true" type="button" data-dismiss="alert">×</button>' +
        '<strong>' + 'ATENCIÓN' + '!</strong> ' + mensaje + '</div > ');

    window.setTimeout(function () {
        $(".alert").fadeTo(500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, duracionSegundos);

    $('.alertp .close').on("click", function (e) {
        $(this).parent().fadeTo(duracionSegundos, 0).slideUp(500);
    });
}

function mensajeError(mensaje, titulo) {

    if (mensaje.length <= 100) {
        columnClass = 'small';
    } else if (mensaje.length > 100 && mensaje.length <= 500) {
        columnClass = 'medium';
    }  else {
        columnClass = 'large';
    }

    $.confirm({
        theme: 'material',
        title: titulo,
        content: mensaje,
        columnClass: columnClass,
        icon: 'fa fa-warning',
        type: 'blue',
        draggable: true,
        dragWindowGap: 0,
        animation: 'scale',
        closeAnimation: 'zoom',
        animationBounce: 2.5,
        escapeKey: 'cerrar',
        backgroundDismiss: false,

        //Botones del Form principal
        buttons: {
            cerrar: {
                text: 'Cerrar',
                btnClass: 'btn-red'
            }
        }
    });
}
function mensajeConfirm(mensaje, titulo, theme, type, icon) {

    if (mensaje.length <= 100) {
        columnClass = 'small';
    } else if (mensaje.length > 100 && mensaje.length <= 500) {
        columnClass = 'medium';
    } else {
        columnClass = 'large';
    }

    var tipo  = type  === '' ? 'green' : type;
    var icono = icon  === '' ? 'fa-check-circle' : icon;
    var tema  = theme === '' ? 'material' : theme;

    $.confirm({
        theme: tema,
        title: titulo,
        content: mensaje,
        columnClass: columnClass,
        icon: 'fa ' + icono,
        type: tipo, //red,green,blue
        draggable: true,
        dragWindowGap: 0,
        animation: 'scale',
        closeAnimation: 'zoom',
        animationBounce: 2.5,
        escapeKey: 'cerrar',
        backgroundDismiss: false,

        //Botones del Form principal
        buttons: {
            cerrar: {
                text: 'Cerrar',
                btnClass: 'btn-red'
            }
        }
    });
}

//*** Mayusculas
function BlodMayus(e, idElement) {

    var elemento = $('#' + idElement + '');

    var CapsLock = event.getModifierState && event.getModifierState('CapsLock');

    if (CapsLock) {

        elemento.tooltip('show');

        setTimeout(function () {
            elemento.tooltip('hide');
        }, 3000);

    } else {
        elemento.tooltip('hide');
    }
}

//Solo Letras y Numeros
function soloLetrasNumeros(key) {
    var tecla = key;
    tecla.val(tecla.val().replace(/[^a-zA-Z0-9]/g, ''));
}

function soloNumeros(key) {
    var tecla = key;
    tecla.val(tecla.val().replace(/[^0-9]/g, ''));
}

//Bloquero Inspect Element
$(document).keydown(function (e) {
    if (e.which === 123) {
        return false;
    }
});

//$(document).bind("contextmenu", function (e) {
//    e.preventDefault();
//});


