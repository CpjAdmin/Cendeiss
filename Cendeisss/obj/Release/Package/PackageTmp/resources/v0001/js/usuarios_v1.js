var tabla, data;
var objTabla = $('#tbl_usuarios');
var paginaURL = 'Usuarios.aspx';
var nombreReporte = 'Lista de Usuarios CENDEISSS';
var height;

var inicio;

var MensajeAlerta;
var tipoMsj;

var cod_proceso;
var animacion;

// *** Iniciar Acciones
$(document).ready(function () {

    ////*** Iniciar Dialogo de Progreso
    dialogoProgreso.show('Cargando ' + '<span class="glyphicon glyphicon-hourglass"></span>');
    animacion = dialogoProgreso.animate();

    setTimeout(function () {

        iniciarDataTable();
        AjaxCargarRegistros();

    }, 1000); //Cargar despues de 1 Seg para poder leer el $(document).height()
});

// *** Evento click 
$(document).on('click', '.btn-editar', function (e) {

    //Evitar el PostBack
    e.preventDefault();

    $("#ModalTxtUsuario").attr("readonly", true); 

    var fila = $(this).parent().parent()[0];
    data = objTabla.fnGetData(fila);

    //Llenar el Modal de Modificación de Usuarios
    llenarDataModal();

    cod_proceso = 'U';


});

// *** Evento click
$(document).on('click', '#btnRegistrar', function (e) {

    //Evitar el PostBack
    e.preventDefault();

    $("#modalTitulo").text("REGISTRAR USUARIO");
    $("#ModalTxtUsuario").attr("readonly", false); 
    $('#modal_actualizar').modal('show');

    cod_proceso = 'C';

});

// *** Limpiar Modal al Ocultarlo
$(document).on('hidden.bs.modal', '#modal_actualizar', function (e) {
    //Limpiar Modal
    limpiarDataModal();
});

function limpiarDataModal() {

    $("#modalTitulo").text();
    $("#ModalTxtUsuario").val("");
    $("#ModalTxtNombre").val("");
    $('#ModalDdlRol').val("1");
    $("#ModalTxtEmail").val("");
    $('#ModalDdlEstado').val("S");
    $("#ModalTxtClave").val("");
}

// *** verificarInputVacios
function verificarInputVacios() {

    var TxtUsuario = $("#ModalTxtUsuario").val();
    var TxtNombre = $("#ModalTxtNombre").val();
    var TxtEmail = $("#ModalTxtEmail").val();
    var TxtClave = $("#ModalTxtClave").val();

    if (TxtUsuario === "") {
        $("#ModalTxtUsuario").focus();
    } else if (TxtNombre === "") {
        $("#ModalTxtNombre").focus();
    } else if (TxtEmail === "") {
        $("#ModalTxtEmail").focus();
    } else if (TxtClave === "") {
        $("#ModalTxtClave").focus();
    } else {
        return false;
    }
}

// Btn Cerrar Modal 
$("#btnModalProcesar").click(function (e) {

    //Evitar el PostBack
    e.preventDefault();

    var vacios = verificarInputVacios();

    if (vacios === false) {

        ProcesarDataAjax(cod_proceso); 

        //Esconder Modal
        $('#modal_actualizar').modal('hide');

    } 
});

// Btn Cerrar Modal 
$("#btnModalCancelar").click(function (e) {
    e.preventDefault();
    cerrarModalActualizar();
});

//Función Cerrar Modal Actualizar
function cerrarModalActualizar() {
    $("#modal_actualizar").modal('hide');
}

// *** Evento click
$(document).on('click', '#btnRefrescar', function (e) {

    //Evitar el PostBack
    e.preventDefault();

    AjaxCargarRegistros();
});

// Iniciar Tabla
function iniciarDataTable() {

    // inicio = new Date().getTime();
    tabla = objTabla.DataTable({
        //data: filas,
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: "EXCEL",
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5]
                },
                title: function () {
                    return nombreReporte;
                },
                sheetName: 'Usuarios'
            }]
        ,
        language: {
            "url": "resources/components/datatables-buttons/json/spanish.json"
        }
        , "fnDrawCallback": function () {
            var tbl = objTabla.DataTable();
            if (tbl.data().length === 0)
                tbl.buttons('.dt-button').disable();
            else
                tbl.buttons('.dt-button').enable();
        },

        "aaSorting": [[1, 'asc']],
        'bDestroy': true,
        "bDeferRender": true,
        "paging": false,
        "scrollY": $(document).height() - 390 + "px",//390
        "scrollX": true,
        "scrollCollapse": true,
        "scroller": true,
        "bSort": true,
        "autoWidth": false,

        "responsive": true,
        "aoColumns": [
            { "sClass": "text-bold" },
            null,
            null,
            null,
            null,
            null,
            { "bSortable": false, "sClass": "center" }
        ]
    });
}
// *** Función Editar
function fnEditar(rol, id) {

    if (rol === "ADMINISTRADOR") {

        return '<button type="button" id="' + id + '" class="btn btn-xs btn-primary btn-editar btn_datatable" data-target="#modal_actualizar" data-toggle="modal"><i class="glyphicon glyphicon-pencil" aria-hidden="true"></i></button>';
    }
    else {
        return '';
    }
}

function agregarFilasDT(data) {


    // Limpiar Tabla despues de cualquier cambio a los Usuarios
    objTabla.dataTable().fnClearTable();

    var filas = [];

    for (var i = 0; i < data.length; i++) {

        filas.push(
                 [data[i].id
                , data[i].nombre
                , data[i].rol
                , data[i].email
                , spanEstado(data[i].estado)
                , data[i].f_ult_ingreso
                , fnEditar(rol, data[i].id) 
            ]);
    }

    objTabla.dataTable().fnAddData(filas, true);

    // Función Condicional de Estado
    function spanEstado(estado) {

        if (estado === "Activo") {
            return '<span class="label label-success" style="font-size:9px;">' + estado + '</span>';
        }
        else {
            return '<span class="label label-danger"  style="font-size:9px;">' + estado + '</span>';
        }
    }

    //*** Dibujar la Tabla
    //tabla.draw();
}

// *** AjaxCargarRegistros
function AjaxCargarRegistros() {

    $.ajax({
        type: 'POST',
        url: 'Usuarios.aspx' + '/CargarRegistros',
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {

            MensajeAlerta = "Error: " + xhr.status + "<br>" + xhr.responseJSON.Message, "\n" + thrownError;

            mensajeError(MensajeAlerta, paginaURL);

            setTimeout(function () {
                window.location = "./Login.aspx";
            }, 5000);

            //*** Detener dialogoProgreso 
            if (animacion !== undefined) {
                setTimeout(function () {
                    dialogoProgreso.hide();
                    dialogoProgreso.stopAnimate(animacion);
                }, 500);
            }

        },
        success: function (data) {

            if (data.d.length > 0) {

                agregarFilasDT(data.d);

            } else {

                MsjPersonalizado = 'No existen usuarios en la base de datos!';
                tipoMsj = 'info';

                Alerta('mensajesAlerta', MsjPersonalizado, tipoMsj, 2000);
            }
        }
    }).done(function () {
        //*** Detener dialogoProgreso 
        if (animacion !== undefined) {
            setTimeout(function () {
                dialogoProgreso.hide();
                dialogoProgreso.stopAnimate(animacion);
            }, 500);
        }
    });
}

function ProcesarDataAjax(cod_proceso) {
    // JSON.stringify = Convertir a Cadena
    var obj = JSON.stringify({
                                  p_usuario: $("#ModalTxtUsuario").val()
                                , p_nombre: $("#ModalTxtNombre").val() 
                                , p_cod_rol: $("#ModalDdlRol").val()
                                , p_email: $("#ModalTxtEmail").val()
                                , p_clave: $("#ModalTxtClave").val()
                                , p_i_estado: $("#ModalDdlEstado").val()
                                , p_proceso: cod_proceso
                                , p_id: login
                            });

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
            }, 5000);
        },
        success: function (response) {

            if (response.d) {
                MensajeAlerta = 'Usuario procesado correctamente.';
                Alerta('mensajesAlerta', MensajeAlerta, 'success', 2000);

                //Refrescar
                AjaxCargarRegistros();
            } else {
                MensajeAlerta = 'No se pudo procesar el Usuario.';
                Alerta('mensajesAlerta', MensajeAlerta, 'danger', 2000);
            }
        }
    });
}

// Cargar datos en el Modal Actualizar
function llenarDataModal() {

    $("#modalTitulo").text("ACTUALIZAR USUARIO");
    $("#ModalTxtUsuario").val(data[0]);
    $("#ModalTxtNombre").val(data[1]);
    $('#ModalDdlRol').val($("#ModalDdlRol option:contains('" + data[2] + "')").val());
    $("#ModalTxtEmail").val(data[3]);
    $('#ModalDdlEstado').val($("#ModalDdlEstado option:contains('" + $.parseHTML(data[4])[0].innerHTML + "')").val());
    $("#ModalTxtClave").val("        ");
}