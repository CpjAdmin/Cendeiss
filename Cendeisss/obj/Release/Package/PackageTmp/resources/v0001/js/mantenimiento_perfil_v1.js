var tabla, data;
var paginaURL = 'MantenimientoPerfil.aspx';
var height;
var inicio;
var MensajeAlerta;
var tipoMsj;

$(document).ready(function () {

    $('#txtId').prop("disabled", true);
    $('#txtPerfilRol').prop("disabled", true);

    $(":input").inputmask();

    $('#fechaNacimiento').datepicker({
        format: "yyyy-mm-dd",
        showButtonPanel: true,
        orientation: "bottom",
        autoclose: true,
        weekStart: 1,
        startDate: '1900/01/01'
    });

    // Cambio de Clave CENDEISSS
    $("#btnCambiarClave").click(function (e) {

        e.preventDefault();

        sistema = $(this).attr('name');

        //Llenar el Modal
        llenarDataModalCambioClave();
    });

    // Actualizar Clave
    $("#btnModalClaveActualizar").click(function (e) {

        e.preventDefault();

        if (clavesIguales) {

            //Actualizar Clave
            actualizarClaveAjax(sistema);

            //Cerrar Modal
            cerrarModalClave();
        }
    });

    // Btn Cerrar Modal 
    $("#btnModalClaveCancelar").click(function (e) {

        e.preventDefault();
        cerrarModalClave();
    });

    // FN Modal Hideen
    modalCambiarClave.on('hidden.bs.modal', function (e) {
        //Borrar Modal
        $('#spanError').remove();
        //Borrar Campos modalCambiarClave
        modalCambiarClave.find("input[type='password']").val("");
    });

    // FN Modal Hideen
    modalCambiarClave.on('shown.bs.modal', function (e) {
        //Enfocar
        $('#ModalTxtClave1').focus();

    });
});

//Cambio de Clave
// FN Llenar Modal Cambio Clave
function llenarDataModalCambioClave() {

    var titulo = "";
    var icono = "";
    // IF Control de Contenido Modal
    if (sistema === "CENDEISSS") {

        icono = "usuario.png";
        titulo = "<strong>CAMBIO DE CONTRASEÑA </strong><ul class='nav navbar-nav'><li class='dropdown user user-menu'><img src='resources/v0001/img/" + icono + "' class='user-image'></li></ul>";
    }

    //Datos Modal
    $("#modalTituloCambiarClave").html(titulo);
    $('#ModalTxtClave1').focus();

    // Verificar Claves
    verificarClaves();
}

// FN verificar Contraseñas
function verificarClaves() {

    var clave1 = $('#ModalTxtClave1');
    var clave2 = $('#ModalTxtClave2');

    var confirmacion = "Contraseñas SI coinciden";
    var negacion = "Contraseñas NO coinciden";

    var longitud = "Longitud mínima, 6 carácteres.";
    var vacio = "Contraseña NO puede estar vacía";

    if ($('#spanError').length > 0) {
        //Si existe el spanError se oculta
        $('#spanError').hide();
    } else {
        // Creo el elemento spanError despues de clave2
        var spanError = $('<span class="spanError" id="spanError"></span>').insertAfter(clave2);
    }

    // FN Comparar las contraseñas
    function coincidePassword() {
        var valor1 = clave1.val();
        var valor2 = clave2.val();

        // Muestra el span y remueve las clases
        spanError.show().removeClass('negacion confirmacion');

        // Condiciones
        if (valor1 !== valor2) {
            //Incorrecto
            spanError.text(negacion).addClass('negacion');
            clavesIguales = false;
        }
        if (valor1.length === 0 || valor1 === "") {
            //Incorrecto
            spanError.text(vacio).addClass('negacion');
            clavesIguales = false;
        }
        if (valor1.length < 6) {
            //Incorrecto
            spanError.text(longitud).addClass('negacion');
            clavesIguales = false;
        }
        if (valor1.length !== 0 && valor1.length >= 6 && valor1 === valor2) {
            //Confirmacion
            spanError.text(confirmacion).removeClass("negacion").addClass('confirmacion');
            clavesIguales = true;
        }
    }

    // Ejecuto la función al soltar la tecla
    clave2.keyup(function () {
        coincidePassword();
    });

    // Ejecuto la función al soltar la tecla
    clave1.keyup(function () {
        if (clave2.val().length >= 6) {
            coincidePassword();
        }
    });
}

// FN Cerrar Modal
function cerrarModalClave() {

    modalCambiarClave.modal('hide');
}
//******************************************************************************** FUNCIONES (FIN)

//******************************************************************************** AJAX (INICIO)
//*** Ajax - Actualizar Clave
function actualizarClaveAjax(sistema) {

    // JSON.stringify
    var obj = JSON.stringify({ clave: $("#ModalTxtClave1").val(), sistema: sistema, id: login });

    $.ajax({
        type: "POST",
        url: paginaURL + "/ActualizarClave",
        data: obj,
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {

            MensajeAlerta = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
            mensajeError(MensajeAlerta, "Inicio.aspx");
        },
        success: function (response) {
            if (response.d) {
                // Mensaje
                MensajeAlerta = 'Contraseña de <strong> ' + sistema + ' </strong> actualizada correctamente!';
                Alerta('mensajesAlerta', MensajeAlerta, 'success', 3000);

            } else {
                // Mensaje
                MensajeAlerta = 'Contraseña de <strong> ' + sistema + ' </strong>  no se actualizó!';
                Alerta('mensajesAlerta', MensajeAlerta, 'danger', 3000);
            }
        }
    });
}

// 1   - Cargar Cantones
function CargarCantones() {

    $("#ddlCanton").attr("disabled", "disabled");
    $('#ddlDistrito').empty().append('<option selected="selected" value="">Seleccione un valor</option>');
    $("#ddlDistrito").attr("disabled", "disabled");

    if ($('#ddlProvincia').val() === "") {
        $('#ddlCanton').empty().append('<option selected="selected" value="">Seleccione un valor</option>');
        $('#ddlDistrito').empty().append('<option selected="selected" value="">Seleccione un valor</option>');
    }
    else {

        $('#ddlCanton').empty().append('<option selected="selected" value="">Cargando...</option>');
        // Guardar parámetros y usar Libreria de JSON
        var parametros = new Object();
        parametros.provinciaId = $("#ddlProvincia").val();
        //Convierte a cadena JSON
        parametros = JSON.stringify(parametros);

        $.ajax({
            type: "POST",
            url: paginaURL + '/CargarCantones',
            data: parametros,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: IniciarCargaCantones,
            error: function (xhr, ajaxOptions, thrownError) {

                MensajeAlerta = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
                mensajeError(MensajeAlerta, paginaURL);
            }
        });
    }
}

// 1.1 - Cargar Cantones
function IniciarCargaCantones(response) {
    LlenarControl(response.d, $("#ddlCanton"));
}

// 2   -  Carga Distritos
function CargarDistritos() {
    if ($('#ddlCanton').val() === "") {
        $('#ddlDistrito').empty().append('<option selected="selected" value="">Seleccione un valor</option>');
    }
    else {
        $('#ddlDistrito').empty().append('<option selected="selected" value="">Cargando...</option>');
        // Guardar parámetros y usar Libreria de JSON
        var parametros = new Object();
        parametros.provinciaId = $("#ddlProvincia").val();
        parametros.cantonId = $("#ddlCanton").val();
        //Convierte a cadena JSON
        parametros = JSON.stringify(parametros);
        //console.log(parametros);
        $.ajax({
            type: "POST",
            url: paginaURL + '/CargarDistritos',
            data: parametros,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: IniciarCargaDistritos,
            error: function (xhr, ajaxOptions, thrownError) {

                MensajeAlerta = xhr.status + "\n" + xhr.responseText, "\n" + thrownError;
                mensajeError(MensajeAlerta, paginaURL);
            }
        });
    }
}

// 2.1   -  Carga Distritos
function IniciarCargaDistritos(response) {
    LlenarControl(response.d, $("#ddlDistrito"));
}


// Llenar LlenarControl
function LlenarControl(list, control) {

    if (list.length > 0) {
        control.removeAttr("disabled");
        control.empty().append('<option selected="selected" value="">Seleccione un valor</option>');

        //Canton
        if (control.attr('id') === "ddlCanton") {
            $.each(list, function () {
                control.append($("<option></option>").val(this['cod_canton']).html(this['nombre']));
            });
        }
        //Distrito
        else if (control.attr('id') === "ddlDistrito") {
            $.each(list, function () {
                control.append($("<option></option>").val(this['cod_distrito']).html(this['nombre']));
            });
        }

    }
    else {
        control.empty().append('<option selected="selected" value="">No existen registros!<option>');
    }
}

// *** Evento click
$(document).on('click', '#btnGuardarPerfil', function (e) {

    if (!$("#txtNombre")[0].checkValidity()) {
        return true;
    } else if (!$("#txtCorreoPrin")[0].checkValidity()) {
        return true;
    } else if (!$("#txtCorreoSec")[0].checkValidity()) {
        return true;
    } else if (!$("#txtTelHabitacion")[0].checkValidity()) {
        return true;
    } else if (!$("#txtTelCelular")[0].checkValidity()) {
        return true;
    } else if (!$("#txtTelTrabajo")[0].checkValidity()) {
        return true;
    } else if (!$("#fechaNacimiento")[0].checkValidity()) {
        return true;
    } else if (!$("#ddlProvincia")[0].checkValidity()) {
        return true;
    } else if (!$("#ddlCanton")[0].checkValidity()) {
        return true;
    } else if (!$("#ddlDistrito")[0].checkValidity()) {
        return true;
    } else {
        e.preventDefault();
        ProcesarPerfil();
    }
});

function ProcesarPerfil() {

    var parametros = new Object();

    parametros.p_id = $("#Id").val();
    parametros.p_nombre = $("#txtNombre").val();
    parametros.p_email = $("#txtCorreoPrin").val();
    parametros.p_email2 = $("#txtCorreoSec").val();
    parametros.p_tel_habitacion = $("#txtTelHabitacion").val();
    parametros.p_tel_celular = $("#txtTelCelular").val();
    parametros.p_tel_trabajo = $("#txtTelTrabajo").val();
    parametros.p_direccion = $("#txtOtrasS").val();

    var prov = $("#ddlProvincia").val() === "" ? "008" : $("#ddlProvincia").val();
    var can = $("#ddlCanton").val() === "" ? "" : $("#ddlCanton").val();
    var dis = $("#ddlDistrito").val() === "" ? "" : $("#ddlDistrito").val();

    parametros.p_ubicacion_geografica = prov + can + dis;
    parametros.p_f_nacimiento = $("#fechaNacimiento").val();

    var obj = JSON.stringify(parametros);

    $.ajax({
        type: "POST",
        url: paginaURL + "/ProcesarUsuario",
        data: obj,
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {

            MensajeAlerta = "Error: " + xhr.status + "<br>" + xhr.responseJSON.Message, "\n" + thrownError;

            mensajeError(MensajeAlerta, paginaURL);

            setTimeout(function () {
                window.location = "./Login.aspx";
            }, 4000);
        },
        success: function (response) {

            if (response.d) {
                MensajeAlerta = 'Perfil actualizado correctamente.';
                Alerta('mensajesAlerta', MensajeAlerta, 'success', 3000);

                setTimeout(function () {
                    window.location = "./Inicio.aspx";
                }, 4000);

            } else {
                MensajeAlerta = 'No se pudo actualizar el perfil.';
                Alerta('mensajesAlerta', MensajeAlerta, 'danger', 3000);
            }
        }
    });
}