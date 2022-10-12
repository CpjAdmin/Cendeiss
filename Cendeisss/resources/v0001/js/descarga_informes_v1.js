
var tabla, data;
var objTabla = $('#tbl_informes');
var paginaURL = 'DescargaInformes.aspx';

var inicio;

var MensajeAlerta;
var tipoMsj;

// *** Iniciar Acciones
$(document).ready(function () {

    setTimeout(function () {
        // Iniciar Tabla
        AjaxCargarArchivos();
    }, 100); //Cargar despues de 1 Seg para poder leer el $(document).height()
});

// *** Evento click 
$(document).on('click', '.btn-descargar', function (e) {

    //Evitar el PostBack
    e.preventDefault();

    var nombreInforme;
      
    nombreInforme = e.currentTarget.id;  

    var archivoNombre = $("#archivoNombre");

    archivoNombre.val(nombreInforme);

    $("#archivoMaster").click();

});

// *** Evento click
$(document).on('click', '#btnRefrescar', function (e) {

    //Evitar el PostBack
    e.preventDefault();

    AjaxCargarArchivos();

    $("#lbEstado").text('');
});


function agregarFilasDT(data) {

    var filas = [];

    //console.log(data);

    for (var i = 0; i < data.length; i++) {

        filas.push([  data[i][0]
                    , data[i][1]
                    , data[i][2]
                    , data[i][3]
                    , data[i][4]
                    , "<button type='button' id='" + data[i][0] + "' class='btn btn-xs btn-success btn-descargar btn_datatable' style='margin-top: 2px;margin-bottom: 2px;'><i class='glyphicon glyphicon-download-alt' aria-hidden='true'></i></button>"
                  ]);
    }

    objTabla.DataTable({
        data: filas,
        dom: 'Bfrtip',
        language: {
            "url": "resources/components/datatables-buttons/json/spanish.json"
        },
        "aaSorting": [[2, 'desc']],
        'bDestroy': true,
        "bDeferRender": true,
        "paging": false,
        "scrollY": $(document).height() - 405 + "px",
        "scrollX": true,
        "scrollCollapse": true,
        "scroller": true,
        "bSort": true,
        "autoWidth": false,
        "responsive": true,

        "aoColumns": [
            null,
            { "sClass": "text-bold" },
            null,
            null,
            { "bSortable": false, render: $.fn.dataTable.render.number('.', ',', 2, ''), "sClass": "text-right" },
            { "bSortable": false, "sClass": "center" }
        ]
    });

    //*** Titulo de la Tabla
    $("#textoCierre").text('Lista de Informes - Total: ( ' + data.length + ' )');
}

// *** AjaxCargarArchivos
function AjaxCargarArchivos() {

    $.ajax({
        type: 'POST',
        url: 'DescargaInformes.aspx' + '/CargarArchivosInformes',
        contentType: 'application/json; charset=utf-8',
        error: function (xhr, ajaxOptions, thrownError) {

            MensajeAlerta = "Error: " + xhr.status + "<br>" + xhr.responseJSON.Message, "\n" + thrownError;

            mensajeError(MensajeAlerta, paginaURL);

            setTimeout(function () {
                window.location = "./Login.aspx";
            }, 5000);
        },
        success: function (data) {

            //console.log(data.d);

            if (data.d.length > 0) {

                //Agregar Filas
                agregarFilasDT(data.d);

            } else {

                MsjPersonalizado = 'No existen archivos en el Directorio!';
                tipoMsj = 'info';

                Alerta('mensajesAlerta', MsjPersonalizado, tipoMsj, 2000);
            }
        }
    });
}