
var tabla, data;
var objTabla = $('#tbl_movimientos');
var paginaURL = 'ConsultaMovimientos.aspx';

var inicio;

var MensajeAlerta;
var tipoMsj;
var tituloReporte = 'COOPECAJA RL - Consulta de Movimientos';
var nombreReporte = 'COOPECAJA RL - Desde ';

var animacion;
var fechaA;
var fechaB;

// *** Iniciar Acciones
$(document).ready(function () {

    //$('.datepicker').datepicker({
    //    format: "dd-mm-yyyy",
    //    showButtonPanel: true ,
    //    orientation: "bottom",
    //    autoclose: true,
    //});

    var startDate = new Date('01/01/2018');
    var FromEndDate = new Date();
    var ToEndDate = new Date();

    ToEndDate.setDate(ToEndDate.getDate() + 365);

    $('#fechaInicial').datepicker({
        format: "dd-mm-yyyy",
        showButtonPanel: true,
        orientation: "bottom",
        autoclose: true,
        weekStart: 1,
        startDate: '01/01/2018',
        endDate: FromEndDate
    }).on('changeDate', function (selected) {

        var usuario = $('#txtUsuario').val();

        startDate = new Date(selected.date.valueOf());
        startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
        $('#fechaFinal').datepicker('setStartDate', startDate);

        $('#txtUsuario').prop('readonly', true);

        if (usuario === '') {
            $('#txtUsuario').prop('placeholder', 'Todos');

            // Fecha Final
            ToEndDate = new Date(startDate.getFullYear(), startDate.getMonth() + 1, 0);
            $('#fechaFinal').datepicker('setEndDate', ToEndDate);
        }

    });

    $('#fechaFinal')
        .datepicker({
            weekStart: 1,
            format: "dd-mm-yyyy",
            showButtonPanel: true,
            orientation: "bottom",
            autoclose: true,
            startDate: startDate,
            endDate: ToEndDate
        }).on('changeDate', function (selected) {

            var usuario = $('#txtUsuario').val();

            FromEndDate = new Date(selected.date.valueOf());
            FromEndDate.setDate(FromEndDate.getDate(new Date(selected.date.valueOf())));
            $('#fechaInicial').datepicker('setEndDate', FromEndDate);

            if (usuario === '') {
                // Fecha Inicial
                startDate = new Date(FromEndDate.getFullYear(), FromEndDate.getMonth(), 1);
                $('#fechaInicial').datepicker('setStartDate', startDate);
            }
        });
});

// ************************ Iniciar Tabla

// Iniciar Tabla
//iniciarDataTable();

// *** Iniciar Tabla 
/*
function iniciarDataTable() {

    tabla = objTabla.DataTable({
        dom: 'Bfrtip',
        buttons: [{
            extend: "copy",
            text: "Copiar"
        }
            , {
            extend: 'csvHtml5',
            title: tituloReporte,
            messageTop: function () {
                return nombreReporte + $("#textoCierre").text();
            }
        }
            , {
            extend: 'excelHtml5',
            title: tituloReporte,
            messageTop: function () {
                return nombreReporte + $("#textoCierre").text();
            }
        }
            , {
            extend: 'print',
            text: "Imprimir"
        }]
        ,
        language: {
            "url": "resources/components/datatables-buttons/json/spanish.json",
            buttons: {
                copyTitle: 'Añadido al portapapeles',
                copyKeys: 'Presione <i> ctrl </ i> o <i> \ u2318 </ i> + <i> C </ i> para copiar los datos de la tabla al portapapeles. <br> <br> Para cancelar, haga clic en este mensaje o presione Esc.',
                copySuccess: {
                    _: '%d lineas copiadas',
                    1: '1 línea copiada'
                }
            }
        },
        "fnDrawCallback": function () {
            var tbl = objTabla.DataTable();
            if (tbl.data().length === 0)
                tbl.buttons('.dt-button').disable();
            else
                tbl.buttons('.dt-button').enable();


        },
        "aaSorting": [[0, 'desc']],
        'bDestroy': true,
        "bDeferRender": true,
        "paging": false,
        "scrollY": $(document).height() - 625 + "px", //30vh

        "scrollCollapse": true,
        "bSort": true,
        "autoWidth": false,
        "responsive": true,
        "aoColumns": [
            { "sWidth": "80px", "sClass": "textoCentrado" },
            null,
            null,
            null,
            null,
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, '₡'), "sClass": "text-right" },
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, '₡'), "sClass": "text-right" },
            { "sClass": "center" }
        ]
    });
}
*/

function agregarFilasDT(data) {

    //Tomar el tiempo de Ejecución
    //inicio = new Date().getTime();

    // Limpiar Tabla despues de cualquier cambio 
    //objTabla.dataTable().fnClearTable();

    var filas = [];

    for (var i = 0; i < data.length; i++) {

        filas.push([data[i].cedula, data[i].nombre, data[i].aportes, data[i].rendimientos, data[i].tipo, data[i].descripcion, data[i].documento, data[i].fecha]);
    }

    tabla = objTabla.DataTable({
        data: filas,
        "pageLength": 500,
        "lengthMenu": [[500, 1000, 2000], ['500 Registros', '1000 Registros', '2000 Registros']],
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pageLength'
            },
            {
                extend: 'csvHtml5',
                action: function (e, dt, node, config) {

                    dialogoProgreso.show('Exportando archivo CSV, espere un momento' + '<span class="glyphicon glyphicon-hourglass"></span>');

                    var that = this;
                    setTimeout(function () {
                        $.fn.DataTable.ext.buttons.csvHtml5.action.call(that, e, dt, node, config);
                        dialogoProgreso.hide();
                    }, 1000);
                },
                title: function () {
                    return nombreReporte + $.datepicker.formatDate("dd-mm-yy", fechaA) + ' Al ' + $.datepicker.formatDate("dd-mm-yy", fechaB);
                }
                //message: function () {
                //    return nombreReporte + $("#textoCierre").text();
                //}
            }
            , {
                extend: 'excelHtml5',
                action: function (e, dt, node, config) {

                    dialogoProgreso.show('Exportando archivo EXCEL, espere un momento' + '<span class="glyphicon glyphicon-hourglass"></span>');

                    var that = this;
                    setTimeout(function () {
                        $.fn.DataTable.ext.buttons.excelHtml5.action.call(that, e, dt, node, config);
                        dialogoProgreso.hide();
                    }, 1000);
                },
                title: function () {
                    return nombreReporte + $.datepicker.formatDate("dd-mm-yy", fechaA) + ' Al ' + $.datepicker.formatDate("dd-mm-yy", fechaB);
                },
                sheetName: 'Movimientos CENDEISSS'
            }]
        ,
        language: {
            "url": "resources/components/datatables-buttons/json/spanish.json",
            buttons: {
                copyTitle: 'Añadido al portapapeles',
                copyKeys: 'Presione <i> ctrl </ i> o <i> \ u2318 </ i> + <i> C </ i> para copiar los datos de la tabla al portapapeles. <br> <br> Para cancelar, haga clic en este mensaje o presione Esc.',
                copySuccess: {
                    _: '%d lineas copiadas',
                    1: '1 línea copiada'
                },
                pageLength: {
                    _: "Mostrar %d Filas",
                    '-1': "Ver todo"
                }
            }
        },
        "fnDrawCallback": function () {
            var tbl = objTabla.DataTable();
            if (tbl.data().length === 0)
                tbl.buttons('.dt-button').disable();
            else
                tbl.buttons('.dt-button').enable();
        },
        //"aaSorting": [[1, 'asc']],
        'bDestroy': true,
        "bDeferRender": true,
        "paging": true,
        "scrollY": $(document).height() - 427 + "px", //527
        "scrollX":true,
        "scrollCollapse": true,
        "scroller": true,
        //"bSort": true,
        "autoWidth": false,

        "responsive": true,
        "aoColumns": [
            { "sClass": "textoCentrado"},
            null,
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
            { "sClass": "center" },
            null,
            null,
            { "sClass": "center" }
        ]
    });

    //*** Dibujar la Tabla
    tabla.draw();
}

$('#txtUsuario').on('input', function () {
    this.value = this.value.replace(/[^0-9]/g, '');
});

// *** Evento click  Agregar Reporte 
$(document).on('click', '#btnEjecutarConsulta', function (e) {

    //Evitar el PostBack
    e.preventDefault();

    if ($('#fechaInicial').val() === '') {
        $('#fechaInicial').focus();
    } else if ($('#fechaFinal').val() === '') {
        $('#fechaFinal').focus();
    } else {

        var usuario;

        if (!$('#txtUsuario').length) {

            usuario = login;

        } else {

            usuario = $('#txtUsuario').val();
        }

        // Barra de Progreso
        if (usuario === '') {

            $('#txtUsuario').focus();
            ////*** Iniciar Dialogo de Progreso
            dialogoProgreso.show('Cargando ' + '<span class="glyphicon glyphicon-hourglass"></span>');
            animacion = dialogoProgreso.animate();

        }
        //else {

        fechaA = $('#fechaInicial').datepicker('getDate');
        fechaB = $('#fechaFinal').datepicker('getDate');

        var parametros = new Object();

        parametros.usuario = usuario;
        parametros.f_ini = fechaA;
        parametros.f_fin = fechaB;
        parametros.id = login;

        AjaxCargarMovimientos(parametros);
        //}
    }
});




// *** Evento click  Limpiar
$(document).on('click', '#btnLimpiar', function (e) {

    //Evitar el PostBack
    e.preventDefault();

    if (!$('.dt-button').length) {

        MsjPersonalizado = 'La tabla de datos está vacia!';
        tipoMsj = 'warning';

        Alerta('mensajesAlerta', MsjPersonalizado, tipoMsj, 2000);

    } else {
        //Total de Registros en la Tabla
        var totalRegistros = objTabla.DataTable().rows().count();

        if (totalRegistros > 0) {

            objTabla.dataTable().fnClearTable();

        } else {

            MsjPersonalizado = 'La tabla de datos está vacia!';
            tipoMsj = 'warning';

            Alerta('mensajesAlerta', MsjPersonalizado, tipoMsj, 2000);
        }
    }
});


// *** AjaxCargarMovimientos 
function AjaxCargarMovimientos(parametros) {

    // Convierte parametros a cadena JSON
    parametros = JSON.stringify(parametros);

    $.ajax({
        type: 'POST',
        url: 'ConsultaMovimientos.aspx' + '/CargarMovimientos',
        data: parametros,
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

            //console.log(data.d);

            if (data.d.length > 0) {

                //Agregar Filas
                agregarFilasDT(data.d);

            } else {

                MsjPersonalizado = 'No existen registros para el Periodo consultado!';
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


