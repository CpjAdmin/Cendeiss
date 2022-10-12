
var tabla, data;
var objTabla = $('#tbl_saldos');
var paginaURL = 'ConsultaSaldos.aspx';

var inicio;

var MensajeAlerta;
var tipoMsj;
var tituloReporte = 'COOPECAJA RL - Consulta de Saldos';
var nombreReporte = 'COOPECAJA RL -  ';

var animacion;

// *** Iniciar Acciones
$(document).ready(function () {

    $('#datepicker').datepicker({
        format: "mm-yyyy",
        showButtonPanel: true,
        viewMode: "months",
        minViewMode: "months",
        orientation: "bottom",
        autoclose: true
    });

    // ************************ Iniciar Tabla

    // Iniciar Tabla
    //iniciarDataTable();

});

// *** Iniciar Tabla 
//function iniciarDataTable() {

//    tabla = objTabla.DataTable({
//        data: filas,
//        //"pageLength": 50,
//        dom: 'Bfrtip',
//        buttons: [{
//            extend: "copy",
//            text: "Copiar"
//        }
//            , {
//            extend: 'csvHtml5',
//                title: tituloReporte,
//                messageTop: function () {
//                    return nombreReporte + $("#textoCierre").text();
//                }
//        }
//            , {
//            extend: 'excelHtml5',
//                title: tituloReporte,
//                messageTop: function () {
//                    return nombreReporte + $("#textoCierre").text();
//                }
//        }
//            , {
//            extend: 'print',
//            text: "Imprimir"
//        }]
//        ,
//        language: {
//            "url": "resources/components/datatables-buttons/json/spanish.json",
//            buttons: {
//                copyTitle: 'Añadido al portapapeles',
//                copyKeys: 'Presione <i> ctrl </ i> o <i> \ u2318 </ i> + <i> C </ i> para copiar los datos de la tabla al portapapeles. <br> <br> Para cancelar, haga clic en este mensaje o presione Esc.',
//                copySuccess: {
//                    _: '%d lineas copiadas',
//                    1: '1 línea copiada'
//                }
//            }
//        },
//        "fnDrawCallback": function () {
//            var tbl = objTabla.DataTable();
//            if (tbl.data().length === 0)
//                tbl.buttons('.dt-button').disable();
//            else
//                tbl.buttons('.dt-button').enable();


//        },
//        "aaSorting": [[0, 'desc']],
//        'bDestroy': true,
//        "bDeferRender": true,
//        "paging": false,
//        "scrollY": $(document).height() - 465 + "px",
//        "scrollCollapse": true,
//        "scroller": true,

//        "bSort": true,
//        "autoWidth": false,
//        "responsive": true,

//        "aoColumns": [
//            { "sWidth": "80px", "sClass": "textoCentrado" },
//            null,
//            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
//            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
//            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
//            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
//            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
//            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
//            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
//            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
//            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" }
//        ]
//    });
//}

function agregarFilasDT(data) {

    // Limpiar Tabla despues de cualquier cambio 
    //objTabla.dataTable().fnClearTable();

    var filas = [];

    for (var i = 0; i < data.length; i++) {

        filas.push([data[i].cedula, data[i].nombre, data[i].saldo_ant_ap, data[i].aportes, data[i].debitos_ap, data[i].saldo_act_ap, data[i].saldo_ant_rend, data[i].rendimientos, data[i].debitos_rend, data[i].saldo_act_rend, data[i].total]);

        //var filas = new Array();

        //filas[0] = data[i].cedula;
        //filas[1] = data[i].nombre;
        //filas[2] = data[i].saldo_ant_ap;
        //filas[3] = data[i].aportes;
        //filas[4] = data[i].debitos_ap;
        //filas[5] = data[i].saldo_act_ap;
        //filas[6] = data[i].saldo_ant_rend;
        //filas[7] = data[i].rendimientos;
        //filas[8] = data[i].debitos_rend;
        //filas[9] = data[i].saldo_act_rend;
        //filas[10] = data[i].total;

        //objTabla.dataTable().fnAddData(filas, false);
    }

    objTabla.DataTable({
        data: filas,
        "pageLength": 100,
        "lengthMenu": [[100, 500, 2000,-1], ['100 Registros', '500 Registros', '2000 Registros','Todos']],
        dom: 'Bfrtip',
        buttons: [{
            extend: 'pageLength'
        }, {
            extend: "copy",
            text: "Copiar"
        }
            , {
            extend: 'csvHtml5',
            title: function () {
                return nombreReporte + $("#textoCierre").text();
            }
        }
            , {
            extend: 'excelHtml5',
            title: function () {
                return nombreReporte + $("#textoCierre").text();
            },
            sheetName: 'Saldos CENDEISSS'
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
        "scrollY": $(document).height() - 410 + "px", // 510

        "scrollCollapse": true,
        "scroller": true,
        //"bSort": true,
        "autoWidth": false,

        "responsive": {
            details: {
                display: $.fn.dataTable.Responsive.display.modal({
                    header: function (row) {
                        var data = row.data();
                        return '<h3>CÉDULA: <b>' + data[0] + '<b></br> <b class="text-danger" >' + data[1] + '</b></h3>';
                    }
                }),
                renderer: $.fn.dataTable.Responsive.renderer.tableAll()
            }
        },

        "aoColumns": [
            { "sClass": "textoCentrado"},
            null,
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" },
            { "bSortable": false, render: $.fn.dataTable.render.number(',', '.', 2, ''), "sClass": "text-right" }
        ]
    });

    //*** Dibujar la Tabla
    //tabla.draw();

    var textoAdicional;

    if ($('#txtUsuario').val() === '') {
        textoAdicional = '';
    } else {
        textoAdicional = '';
    }

    //*** Titulo de la Tabla
    $("#textoCierre").text('Saldos al ' + data[0].periodo + textoAdicional);
}

$('#txtUsuario').on('input', function () {
    this.value = this.value.replace(/[^0-9]/g, '');
});

// *** Evento click  Agregar Reporte 
$(document).on('click', '#btnEjecutarConsulta', function (e) {

    //Evitar el PostBack
    e.preventDefault();

    if ($('#datepicker').val() !== '') {

        var usuario;

        if (!$('#txtUsuario').length) {

            usuario = login;

        } else {

            usuario = $('#txtUsuario').val();

            // Barra de Progreso
            if (usuario === '') {

                //*** Iniciar Dialogo de Progreso
                dialogoProgreso.show('Cargando ' + '<span class="glyphicon glyphicon-hourglass"></span>');
                //*** Animación -  cambio de mensaje cada 2 segundos + inicio después del retraso de 1 segundo
                animacion = dialogoProgreso.animate();
            }
        }

        var date = $('#datepicker').datepicker('getDate');
        var mes = date.getMonth() + 1;
        var ano = date.getFullYear();

        var parametros = new Object();

        parametros.usuario = usuario;
        parametros.mes = mes;
        parametros.ano = ano;
        parametros.id = login;

        //console.log(parametros);

        AjaxCargarSaldos(parametros);

    } else {

        //MsjPersonalizado = 'Seleccione un Periodo de consulta!';
        //tipoMsj = 'info';
        //Alerta('mensajesAlerta', MsjPersonalizado, tipoMsj, 2000);

        $('#datepicker').focus();
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

            $("#textoCierre").text('');

        } else {

            MsjPersonalizado = 'La tabla de datos está vacia!';
            tipoMsj = 'warning';

            Alerta('mensajesAlerta', MsjPersonalizado, tipoMsj, 2000);
        }
    }
});


// *** AjaxCargarSaldos 
function AjaxCargarSaldos(parametros) {

    // Convierte parametros a cadena JSON
    parametros = JSON.stringify(parametros);

    $.ajax({
        type: 'POST',
        url: 'ConsultaSaldos.aspx' + '/CargarSaldos',
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


