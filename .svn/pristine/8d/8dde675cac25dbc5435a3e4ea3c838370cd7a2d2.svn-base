var isVerTodos = false;
$(document).ready(function () {
    isVerTodos = false;
    $('#divAutocompleteResponsable').hide();
    $('#divAutocompleteApoyo').hide();
    $("#divTblLog").hide();
    //Ocultar el boton para ocultar el historial del reporte
    $("#btnTblLogReporteOcultar").hide();      
    //DatesPickers para filtros por fecha
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '<Ant',
        nextText: 'Sig>',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'yy-mm-dd',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);
    //Dates Pickers (GENERAL)
    $("#txtFechaFin").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });
    $("#txtFechaInicio").datepicker({
        onClose: function (selectedDate) {
            $("#txtFechaFin").datepicker("option", "minDate", selectedDate);
        },
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });
    //Clic en icono de calendario (GENERAL)
    $('.calendarioInicio').click(function () {
        mostrarDatepickerFechaInicio();
    });
    $('.calendarioFin').click(function (e) {
        e.preventDefault();
        mostrarDatepickerFechaFin();
    });
    //Iniciar Dialog al ver del detalle de reportes sin asignar
    //Dialog para el datelle del Reporte
    $("#dlogReporte").dialog({
        modal: true,
        autoOpen: false,
        height: 600,
        width: 900,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            //Funciones a ejecutar al cerrar el dialog del detalle de reporte
            $('#btnTblLogReporte').show();
            $('#btnTblLogReporteOcultar').hide();
            $('#divTblLog').empty();
        },
        open: function (event, ui) {

        }
    });
    
    //Obtener los Folios de los Reportes (GENERAÑ)
    obtenerFoliosReportes("#txtIdFolio", "#txtFolio", "obtenerFoliosReportes");
    //Obtener los grupos de empresas
    cargarGrupos("#txtIdGrupo", "#txtNomFolio", "obtenerGrupos");
    //Cerrar Dialog del Detalle del Reporte
    $('#btnCerrarDlogDetalleReporte').click(function (e) {
        e.preventDefault();
        $('#dlogReporte').dialog("close");
        ocultarLog();
    });
    //Cerrar Dialog de la lista y agregado de responsables y apoyo
    $('#btnCerrarDlogResponsables').click(function (e) {
        e.preventDefault();
        $('#dlogLstApoyo').dialog("close");
    });
    
    //Mostrar todos los registros de Reportes (GENERAL)
    $('#btnVerTodos').click(function (e) {
        e.preventDefault();
        $('.validar:checked').each(
            function () {
                $(this).prop('checked', false); //solo los del objeto #diasHabilitados
            }
        );
        $('#txtFechaInicio').val('');
        $('#txtFechaFin').val('');
        $('#txtFolio').val('');
        $('.btnGenerarPDF').hide();
        $('#divGrafica').hide();
        $('#divTblReportes').empty();
        isVerTodos = true;
        getTblReportes();
    });
    //Limpiar todos los campos con informacion
    $('.btnBorrar').click(function (e) {
        e.preventDefault();
        $('.validar:checked').each(
            function () {
                $(this).prop('checked', false); //solo los del objeto #diasHabilitados
            }
        );
        $('#txtFechaInicio').val('');
        $('#txtNomGrupo').val('');
        $('#txtIdGrupo').val('');
        $('#txtFechaFin').val('');
        $('#txtFolio').val('');
        $('.btnGenerarPDF').hide();
        $('#divGrafica').hide();
    });
    //Validar que seleccione al menos un Estatus de Reporte
    $('#btnBuscar').click(function () {
        var idAsignado = 0;
        var idSinAsignar = 0;
        var idPorValidar = 0;
        var idTerminado = 0;
        var idGrupo = 0;

        if ($('#chkSinAsignar').prop('checked')) {
            idSinAsignar = $('#chkSinAsignar').val();
        } else {
            idSinAsignar;
        }

        if ($('#chkAsignados').prop('checked')) {
            idAsignado = $('#chkAsignados').val();
        } else {
            idAsignado;
        }

        if ($('#chkPorValidar').prop('checked')) {
            idPorValidar = $('#chkPorValidar').val();
        } else {
            idPorValidar;
        }

        if ($('#chkTerminados').prop('checked')) {
            idTerminado = $('#chkTerminados').val();
        } else {
            idTerminado;
        }

        var folio = '';
        if ($('#txtIdFolio').val() !== '') {
            folio = $('#txtIdFolio').val();
        } else {
            folio;
        }

        var fechaInicio = '';
        if ($('#txtFechaInicio').val() !== '') {
            fechaInicio = $('#txtFechaInicio').val();
        } else {
            fechaInicio;
        }

        if ($('#txtIdGrupo').val() !== '') {
            idGrupo = $('#txtIdGrupo').val();
        } else {
            idGrupo;
        }

        var fechaFin = '';
        if ($('#txtFechaFin').val() !== '') {
            fechaFin = $('#txtFechaFin').val();
        } else {
            fechaFin;
        }

        var idFallo = 0;
        var idFuncionalidad = 0;
        var idERP = 0;
        isVerTodos = false;
        //alert('idAsignado: ' + idAsignado + ', idSinAsignar: ' + idSinAsignar + ',idPorValidar: '+idPorValidar+', idTerminado: '+idTerminado+', folio: ' + folio + ', fechaInicio: ' + fechaInicio + ', fechaFin: ' + fechaFin + ', idFallo: ' + idFallo + ', idFuncionalidad: ' + idFuncionalidad + ', idERP: ' + idERP);
        getTblReportesFiltrados(idAsignado, idSinAsignar, idPorValidar, idTerminado, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP, idGrupo);
    });

    //Seleccionar chkAsignados cuando delecciona alguno de sus filtros de Reportes Asignados
    $('#btnBuscarAsignados').click(function () {
        var si = 'si';
        $('.validarAsignados').each(function () {
            if ($(this).val() == '' || $.trim($(this).val()) == ' ' || $(this).val() == 0) {
                si += 'no';
            }
        });
        if (si == 'si') {
            $('#chkAsignados').prop("checked", true);
            
        } else {
            $('#chkAsignados').prop("checked", true);
            
        }
    });
    //Seleccionar chkAsignados cuando delecciona alguno de sus filtros de Reportes Sin Asignar
    $('#btnBuscarSinAsignar').click(function () {
        var si = 'si';
        $('.validarSinAsignar').each(function () {
            if ($(this).val() == '' || $.trim($(this).val()) == ' ' || $(this).val() == 0) {
                si += 'no';
            }
        });
        if (si == 'si') {
            $('#chkSinAsignar').prop("checked", true);
            $('#btnGenerarPdfReportesSinAsignar').show();
        } else {
            $('#chkSinAsignar').prop("checked", true);
            $('#btnGenerarPdfReportesSinAsignar').show();
        }
    });
    //Hacer filto de Reportes Asignados
    $('#btnBuscarAsignados').click(function () {
        var idAsignado = 0;
        var idSinAsignar = 0;
        if ($('#chkAsignados').prop('checked')) {
            idAsignado = $('#chkAsignados').val();
        } else {
            idAsignado;
        }

        var folio = '';
        if ($('#txtIdFolioAsig').val() !== '') {
            folio = $('#txtIdFolioAsig').val();
        } else {
            folio;
        }

        var fechaInicio = '';
        if ($('#txtFechaInicioAsig').val() !== '') {
            fechaInicio = $('#txtFechaInicioAsig').val();
        } else {
            fechaInicio;
        }

        var fechaFin = '';
        if ($('#txtFechaFinAsig').val() !== '') {
            fechaFin = $('#txtFechaFinAsig').val();
        } else {
            fechaFin;
        }

        var idFallo = 0;
        var idFuncionalidad = 0;
        var idERP = 0;

        //alert('idAsignado: ' + idAsignado + ', idSinAsignar: ' + idSinAsignar + ', folio: ' + folio + ', fechaInicio: ' + fechaInicio + ', fechaFin: ' + fechaFin + ', idFallo: ' + idFallo + ', idFuncionalidad: ' + idFuncionalidad + ', idERP: ' + idERP);

        getTblReportesAsignadosFiltrado(idAsignado, idSinAsignar, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP);

    });
    //Hacer filto de Reportes Sin Asignar
    $('#btnBuscarSinAsignar').click(function () {
        var idAsignado = 0;
        var idSinAsignar = 0;
        if ($('#chkSinAsignar').prop('checked')) {
            idSinAsignar = $('#chkSinAsignar').val();
        } else {
            idSinAsignar;
        }

        var folio = '';
        if ($('#txtIdFolioSinAsig').val() !== '') {
            folio = $('#txtIdFolioSinAsig').val();
        } else {
            folio;
        }

        var fechaInicio = '';
        if ($('#txtFechaInicioSinAsig').val() !== '') {
            fechaInicio = $('#txtFechaInicioSinAsig').val();
        } else {
            fechaInicio;
        }

        var fechaFin = '';
        if ($('#txtFechaFinSinAsig').val() !== '') {
            fechaFin = $('#txtFechaFinSinAsig').val();
        } else {
            fechaFin;
        }

        var idFallo = 0;
        var idFuncionalidad = 0;
        var idERP = 0;

        //alert('idAsignado: ' + idAsignado + ', idSinAsignar: ' + idSinAsignar + ', folio: ' + folio + ', fechaInicio: ' + fechaInicio + ', fechaFin: ' + fechaFin + ', idFallo: ' + idFallo + ', idFuncionalidad: ' + idFuncionalidad + ', idERP: ' + idERP);

        getTblReportesSinAsignarFiltrado(idAsignado, idSinAsignar, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP);

    });
    
    //Al dar clic en ocultar Historial este ocultara la tabla y mostrará el boton de "Ver Historial"
    $("#btnTblLogReporteOcultar").click(function () {
        $("#divTblLog").empty();
        $("#btnTblLogReporteOcultar").hide();
        $("#btnTblLogReporte").show();
        $('#divDetalle').show();
    });
    //Cambiar de color el icono del boton cada que se coloncan encima o fuera
    $(".btnBorrar").mouseover(function () {
        $(this).removeClass('azul').addClass('verde');
    });
    $(".btnBorrar").mouseout(function () {
        $(this).removeClass('verde').addClass('azul');
    });
    $(".calendarioInicio").mouseover(function () {
        $(this).removeClass('azul').addClass('verde');
    });
    $(".calendarioInicio").mouseout(function () {
        $(this).removeClass('verde').addClass('azul');
    });
    $(".calendarioFin").mouseover(function () {
        $(this).removeClass('azul').addClass('verde');
    });
    $(".calendarioFin").mouseout(function () {
        $(this).removeClass('verde').addClass('azul');
    });
  
});

//Traer todos los reportes (GENERAL)
function getTblReportes() {
    block();
    $.ajax({
        url: "ModificarReportes.aspx/getTblReportes",
        type: "POST",
        data: "{}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();

            $('#divTblReportes').append(data.d);
            $("#tblReportes").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                order: [[7, 'asc']],
                "aoColumnDefs": [
                    { "sClass": "txt-left", "aTargets": [1, 3, 4, 5] }
                ],
            });
            $("#tblReportes").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en getTblReportes.");
        }
    });
}
//Al dar clic en el icono de calendario Reportes (GENERAL)
function mostrarDatepickerFechaFin() {
    $("#txtFechaFin").datepicker("show");
}
function mostrarDatepickerFechaInicio() {
    $("#txtFechaInicio").datepicker("show");
}

//Consultar Reporte (GENERAL)
function consultarReporte(idReporte) {
    block();
    obtenerResponsableNuevo(idReporte);//Agrega en un label blanco el responsable

    $('#divDetalle').show();
    $('#dlogReporte').dialog("open");
    $.ajax({
        url: "ModificarReportes.aspx/consultarReporte",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d[5] != "5") {
                obtenerApoyoNuevo(idReporte);//Agrega en un label blanco el responsable                
                $('#lblApoyo').removeClass("hide");
                $('#lblCalificacion').addClass("hide");
                $('#divCalificacion').addClass("hide");
                $('#apoyo').removeClass("hide");

            } else if (data.d[5] == "5") {
                $('#lblApoyo').addClass("hide");
                $('#divCalificacion').removeClass("hide");
                $('#divCalificacion').empty().append("<label id='lblCalificacion' class='txt-verde frm'>Calificación: </label>" + data.d[6] + "");
                $('#apoyo').addClass("hide");
            }

            $('#txtIdReporte').val(parseInt(data.d[0]));
            $('#lblAsunto').text(data.d[1]);
            $('#lblDescripcion').text(data.d[2]);
            $('#lblFechaReporte').text(data.d[3]);
            $('#lblFechaPropuesta').text(data.d[4]);
            $('#iconPersonal').empty();

            $('#iconPersonal').append('<span id="icon-25" class="usuarios azul lblResponsables' + data.d[0] + '" ' +
                'title="Asignar Apoyo para realizar esta Tarea" ' +
                'onclick="tblPersonalApoyo(' + data.d[0] + ');' +
                'tblPersonalResponsable(' + data.d[0] + ');"></span>');

            $('.lblResponsables' + data.d[0]).attr('data-id', parseInt(data.d[0]));
            $('#tablaDetalleReporte').empty();
            $('#tablaDetalleReporte').append('<div id="divTblApoyo' + data.d[0] + '"></div>');
            $('#inpAutocomplete').empty();
            $('#inpAutocompleteResponsable').empty();
            if (data.d[5] != 5) {
                $('#inpAutocomplete').append('<input type="text" id="txtApoyo" class="inputP50" placeholder="Escriba el Nombre"/><input type="hidden" id="txtIdApoyo"/><a onclick="javascript:"><span title="Agregar" id="icon-25" class="verde agregar agregar-apoyo" onclick="javascript:guardarPersonaApoyo(' + data.d[0] + ')"></span></a>');
                $('#inpAutocompleteResponsable').append('<input type="text" id="txtResponsable" class="inputP50" placeholder="Escriba el Nombre"/><input type="hidden" id="txtIdResponsable"/><a onclick="javascript:"><span title="Agregar" id="icon-25" class="verde agregar agregar-apoyo" onclick="javascript:guardarPersonaResponsable(' + data.d[0] + ')"></span></a>');
            }
            if (data.d[5] == 1) {
                $('.usuarios').hide();
                $('#lblPerosnalApoy').hide();
            } else {
                $('.usuarios').show();
                $('#lblPerosnalApoy').show();
            }
            ocultarLog();
            getListReporte(parseInt(data.d[7]), parseInt(data.d[8]), parseInt(data.d[9]), parseInt(data.d[10])); //obterner los list de Prioridad, TipoIncidencia, Grupos y Sistemas
            cargarPersonalApoyo("#txtIdApoy", "#txtApoy", "obtenerPersonalApoyo");
            cargarPersonalResponsable("#txtIdResponsabl", "#txtResponsabl", "obtenerPersonalApoyo");//Carga los responsables en autocomplete
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en consultarReporte.");
        }
    });
}

//****************************Obtener Responsables en tooltip obtenerResponsableNuevo
function obtenerResponsableNuevo(idReporte) {
    $.ajax({
        url: "ModificarReportes.aspx/obtenerResponsableNuevo",
        type: "POST",
        data: "{idReporte:'" + idReporte + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            $('#responsable').empty();
            $('#responsable').append(data.d);
            $('.btnModificarRes').click(function () {
                $('#divAutocompleteResponsable').show();
            });
            $('#btnAddApoyo').click(function () {
                $('#divAutocompleteApoyo').show();

            });
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>rror en tblPersonalApoyo.");
        }
    });
}
function obtenerApoyoNuevo(idReporte) {
    $.ajax({
        url: "ModificarReportes.aspx/obtenerApoyoNuevo",
        type: "POST",
        data: "{idReporte:'" + idReporte + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == "<label>No existe personal de apoyo <span id='icon-25' class='verde agregar btnAddApoyo' title='Agregar Apoyo'></span></label>") {
                $('.btnAddApoyo').hide();
            } else {
                $('.btnAddApoyo').show();
            }
            $('#apoyo').empty();
            $('#apoyo').append(data.d);
            $('.btnModificarApoyo').click(function () {
                $('#divAutocompleteResponsable').show();

            });
            $('.btnAddApoyo').click(function () {
                $('.btnAddApoyo').hide();
                $('#divAutocompleteApoyo').show();
            });
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>rror en tblPersonalApoyo.");
        }
    });
}

//Función para obtener las personas de Apoyo para el reporte
function tblPersonalApoyo(idReporte) {
    block();
    $('#dlogLstApoyo').dialog('open');
    $.ajax({
        url: "ModificarReportes.aspx/tblPersonalApoyo",
        type: "POST",
        data: "{idReporte:'" + idReporte + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            $('#divTblApoyo' + idReporte).html(data.d);
            $("#tblApoyo").dataTable({
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white']
            });
            $("#tblApoyo").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>rror en tblPersonalApoyo.");
            unBlock();
        }
    });
}
//Función para obtener las personas de Apoyo para el reporte
function tblPersonalResponsable(idReporte) {
    block();
    $.ajax({
        url: "ModificarrReportes.aspx/tblPersonalResponsable",
        type: "POST",
        data: "{idReporte:'" + idReporte + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            $('#divTblResponsables').empty();
            $('#divTblResponsables').append(data.d);
            $("#tblResponsables").dataTable({
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white']
            });
            $("#tblResponsables").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>rror en tblPersonalResponsable.");
            unBlock();
        }
    });
}
//Guardar a las personas que van a estar como Apoyo en un Reporte
function guardarPersonaApoyo(idReporte) {
    block();
    var idResponsable = $('#txtIdApoyo').val();
    if ($.trim(idResponsable) == '') {
        $('#txtApoyo').focus();
    } else {
        $.ajax({
            url: "ModificarReportes.aspx/guardarPersonaApoyo",
            type: "POST",
            data: "{idReporte:" + idReporte + ", idResponsable:" + idResponsable + "}",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                if (data.d == true) {
                    alertify.success("<span id='icon-25' class='success blanco'></span>Se Guardo Correctamente la Información.");
                    $('#tblApoyo').empty();
                    tblPersonalApoyo(idReporte);
                    //Al guardar la persona de apoyo limpiar el Autocomplete
                    $('#txtIdApoyo').val('');
                    $('#txtApoyo').val('');
                } else {
                    alertify.error("<span id='icon-25' class='warning blanco'></span>No se Guardo la Información.");
                }
                unBlock();
            },
            error: function (xhr, status, error) {
                alertify.error("<span id='icon-25' class='warning blanco'></span>Error en agregarPersonaApoyo.");
                unBlock();
            }
        });
    }


}
//Eliminar a las personas que están como Apoyo en un Reporte
function eliminarPersonaApoyo(idReporte, idResponsable) {
    block();
    $.ajax({
        url: "ModificarReportes.aspx/eliminarPersonaApoyo",
        type: "POST",
        data: "{idReporte:" + idReporte + ", idResponsable:" + idResponsable + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == true) {
                alertify.success("<span id='icon-25' class='success blanco'></span>Se eliminó correctamente la Información.");
                //$('#tblApoyo').empty();
                //tblPersonalApoyo(idReporte);
                obtenerApoyoNuevo(idReporte);
                $('#divAutocompleteApoyo').hide();
            } else {
                alertify.error("<span id='icon-25' class='warning blanco'></span>No se eliminó la Información.");
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en eliminarPersonaApoyo.");
            unBlock();
        }
    });
}
//Guardar a las personas que van a estar como Responsable en un Reporte
function guardarPersonaResponsable(idReporte) {
    block();
    var idResponsable = $('#txtIdResponsable').val();
    if ($.trim(idResponsable) == '') {
        $('#txtResponsable').focus();
    } else {
        $.ajax({
            url: "ModificarReportes.aspx/guardarPersonaResponsable",
            type: "POST",
            data: "{idReporte:" + idReporte + ", idResponsable:" + idResponsable + "}",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                if (data.d == true) {
                    alertify.success("<span id='icon-25' class='success blanco'></span>Se Guardo Correctamente la Información.");
                    $('#divTblResponsables').empty();
                    tblPersonalResponsable(idReporte);
                    //Al guardar la persona Responsable limpiar el Autocomplete
                    $('#txtIdResponsable').val('');
                    $('#txtResponsable').val('');
                } else {
                    alertify.error("<span id='icon-25' class='warning blanco'></span>No se Guardo la Información.");
                }
                unBlock();
            },
            error: function (xhr, status, error) {
                alertify.error("<span id='icon-25' class='warning blanco'></span>Error en guardarPersonaResponsable.");
                unBlock();
            }
        });
    }
}
//Eliminar a las personas que están como Apoyo en un Reporte
function eliminarPersonaResponsable(idReporte, idResponsable) {
    block();
    $.ajax({
        url: "ModificarReportes.aspx/eliminarPersonaResponsable",
        type: "POST",
        data: "{idReporte:" + idReporte + ", idResponsable:" + idResponsable + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == true) {
                alertify.success("<span id='icon-25' class='success blanco'></span>Se eliminó correctamente la Información.");
                $('#divTblResponsables').empty();
                tblPersonalResponsable(idReporte);
            } else {
                alertify.error("<span id='icon-25' class='warning blanco'></span>No se eliminó la Información.");
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en eliminarPersonaResponsable.");
            unBlock();
        }
    });
}

//*******************************Editar Reponsable
function editarPersonaResponsable() {
    var idReporte = $('#txtIdReporte').val();
    var idResponsable = $('#txtIdResponsabl').val();
    if ($.trim($('#txtIdResponsabl').val()) !== '') {
        block();
        $.ajax({
            url: "ModificarReportes.aspx/editarPersonaResponsable",
            type: "POST",
            data: "{idReporte:" + idReporte + ", idResponsable:" + idResponsable + "}",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                if (data.d == true) {
                    alertify.success("<span id='icon-25' class='success blanco'></span>Se modificó correctamente la Información.");
                    obtenerResponsableNuevo(idReporte);
                    //obtenerApoyoNuevo(idReporte);
                    $('#txtResponsabl').val('');
                    $('#txtIdResponsabl').val('');
                    $('#divAutocompleteResponsable').hide();
                } else {
                    alertify.error("<span id='icon-25' class='warning blanco'></span>No se modificó la Información.");
                }
                unBlock();
            },
            error: function (xhr, status, error) {
                alertify.error("<span id='icon-25' class='warning blanco'></span>Error en editarPersonaResponsable.");
                unBlock();
            }
        });

    } else {
        $('#txtResponsabl').focus();
    }

}
//Guardar a las personas que van a estar como Apoyo en un Reporte
function editarPersonaApoyo() {

    var idReporte = $('#txtIdReporte').val();
    var idResponsable = $('#txtIdApoy').val();
    if ($.trim(idResponsable) == '') {
        $('#txtApoy').focus();
    } else {
        block();
        $.ajax({
            url: "ModificarReportes.aspx/guardarPersonaApoyo",
            type: "POST",
            data: "{idReporte:" + idReporte + ", idResponsable:" + idResponsable + "}",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                if (data.d == true) {
                    alertify.success("<span id='icon-25' class='success blanco'></span>Se Guardo Correctamente la Información.");
                    obtenerApoyoNuevo(idReporte);
                    $('#txtApoy').val('');
                    $('#txtIdApoy').val('');
                    $('#divAutocompleteApoyo').hide();
                } else {
                    alertify.error("<span id='icon-25' class='warning blanco'></span>No se Guardo la Información.");
                }
                unBlock();
            },
            error: function (xhr, status, error) {
                alertify.error("<span id='icon-25' class='warning blanco'></span>Error en agregarPersonaApoyo.");
                unBlock();
            }
        });
    }


}
//Obtener los Reportes Asignados según los filtros especificado
function getTblReportesFiltrados(idAsignado, idSinAsignar, idPorValidar, idTerminado, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP, idGrupo) {
    block();
    $.ajax({
        url: "ModificarReportes.aspx/getTblReportesFiltrados",
        type: "POST",
        data: "{idAsignado:" + idAsignado + ", idSinAsignar:" + idSinAsignar + ", idPorValidar:" + idPorValidar + ",idTerminado:" + idTerminado + ", folio:'" + folio + "', fechaInicio:'" + fechaInicio + "', fechaFin:'" + fechaFin + "', idFallo:" + idFallo + ", idFuncionalidad:" + idFuncionalidad + ", idERP:" + idERP + ", idGrupo:" + idGrupo + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            //alert(data.d.indexOf("°"));
            if (data.d.indexOf("°") == -1) {

                //alertify.success(data.d.substring(0, data.d.indexOf("°")));
                //alertify.success("<span id='icon-25' class='success blanco'></span>" + data.d.substring(0, data.d.indexOf("°")) + "");
                $('.btnGenerarPDF').show();
                $('#divTblReportes').empty();
                $('#divTblReportes').append(data.d);
                $("#tblReportes").dataTable({
                    "pageLength": 50,
                    "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                    "stripeClasses": ['gray', 'white'],
                    order: [[7, 'asc']],
                    "aoColumnDefs": [
                        { "sClass": "txt-left", "aTargets": [1, 3, 4, 5] }
                    ],
                });
                $("#tblReportes").on('draw.dt', function () {
                    $("#grid-head1").addClass("round-border2");
                });
            } else {
                $('#divTblReportes').empty();
                $('#divTblReportes').append(data.d.substring(0, data.d.indexOf("°")));

            }


            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en getTblReportesAsignadosFiltrado.");
            unBlock();
        }
    });
}

//Función con la cual se genera la tabla en la que se encuentra el log de las acciones que se ha realizado con cada uno de los reportes
//Y ver el estatus en el que se encuentra el reporte
function detalleAccionesReporte() {
    block();
    var idReporte = $("#txtIdReporte").val();

        $.ajax({
            url: "ModificarReportes.aspx/detalleAccionesReporte",
            type: "POST",
            data: "{idReporte:" + idReporte + "}",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                unBlock();
                $('#lblLog').show()
                $("#divTblLog").empty();
                $("#divTblLog").show();
                $("#btnOcultarLog").show();
                $("#btnMostrarLog").hide();
                $("#divTblLog").append(data.d);
                $("#tblDetalleAccionesR").dataTable({
                    "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                    "stripeClasses": ['gray', 'white'],
                    order: [[3, 'asc']],
                    "pageLength": 3
                });

            },
            error: function (xhr, status, error) {
                unBlock();
                alert(error);
            }
        });

    
}

//Funcion para ocultar el log del detalle
function ocultarLog() {
    $('#lblLog').hide()
    $("#divTblLog").empty();
    $("#divTblLog").hide();
    $("#btnOcultarLog").hide();
    $("#btnMostrarLog").show();
};

//Cargar los Grupos en un autocomplete
function cargarGrupos(idObjeto, nombre, metodo) {
    var idGrupo = $('#txtIdGrupo').val();
    $('#txtNomGrupo').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "ModificarReportes.aspx/obtenerGrupos",
                data: "{ term: '" + request.term + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            value: item.Nombre,
                            ID: item.Id,
                            label: item.Nombre
                        }
                        alert(item.Id);
                        alert(item.Nombre);
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error en cargarGrupos');
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            var value = ui.item.value;
            var id = $(ui.item).attr("ID");
            $(idObjeto).val(id); // Valor de id oculto en la vista      
            //obtenerTipoTarea();
        },
        change: function (event, ui) {
            if (ui.item == null || ui.item == undefined) {
                $(idObjeto).val("");
                $(nombre).val("");
            }
        }
    });
}

function setClickableTooltip(target, content) {
    $(target).tooltip({
        show: null, // show immediately 
        position: { my: "right top", at: "left top" },
        content: content, //from params
        hide: { effect: "" }, //fadeOut
        close: function (event, ui) {
            ui.tooltip.hover(
                function () {
                    $(this).stop(true).fadeTo(400, 1);
                },
                function () {
                    $(this).fadeOut("600", function () {
                        $(this).remove();
                    });
                }
            );
        }
    });
}

function cargarPersonalApoyo(idObjeto, nombre, metodo) {
    var idResponsable = $('#txtIdApoy').val();
    var idReporte = $('#txtIdReporte').val();
    $(nombre).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "ModificarReportes.aspx/" + metodo + "",
                data: "{ term: '" + request.term + "', idReporte: " + idReporte + "  }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            value: item.Nombre,
                            ID: item.Id,
                            label: item.Nombre
                        }
                        alert(item.Id);
                        alert(item.Nombre);
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error en cargarPersonalApoyo');
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            var value = ui.item.value;
            var id = $(ui.item).attr("ID");
            $(idObjeto).val(id); // Valor de id oculto en la vista      
            //obtenerTipoTarea();
        },
        change: function (event, ui) {
            if (ui.item == null || ui.item == undefined) {
                $(idObjeto).val("");
                $(nombre).val("");
            }
        }
    });
}

function cargarPersonalResponsable(idObjeto, nombre, metodo) {
    var idResponsable = $('#txtIdResponsable').val();
    var idReporte = $('#txtIdReporte').val();
    $(nombre).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "ModificarrReportes.aspx/" + metodo + "",
                data: "{ term: '" + request.term + "', idReporte: " + idReporte + "  }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            value: item.Nombre,
                            ID: item.Id,
                            label: item.Nombre
                        }
                        alert(item.Id);
                        alert(item.Nombre);
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error en cargarPersonalResponsableee');
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            var value = ui.item.value;
            var id = $(ui.item).attr("ID");
            $(idObjeto).val(id); // Valor de id oculto en la vista      
            //obtenerTipoTarea();
        },
        change: function (event, ui) {
            if (ui.item == null || ui.item == undefined) {
                $(idObjeto).val("");
                $(nombre).val("");
            }
        }
    });
}

function obtenerFoliosReportesAsignados(idObjeto, nombre, metodo) {
    var idFolio = $('#txtIdFolioAsig').val();
    $('#txtFolioAsig').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "ModificarReportes.aspx/obtenerFoliosReportesAsignados",
                data: "{ term: '" + request.term + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            value: item.Nombre,
                            ID: item.Id,
                            label: item.Nombre
                        }
                        alert(item.Id);
                        alert(item.Nombre);
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error en obtenerFoliosReportesAsignados');
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            var value = ui.item.value;
            var id = $(ui.item).attr("ID");
            $(idObjeto).val(id); // Valor de id oculto en la vista      
            //obtenerTipoTarea();
        },
        change: function (event, ui) {
            if (ui.item == null || ui.item == undefined) {
                $(idObjeto).val("");
                $(nombre).val("");
            }
        }
    });
}

function obtenerFoliosReportesSinAsignar(idObjeto, nombre, metodo) {
    var idFolio = $('#txtIdFolioSinAsig').val();
    $('#txtFolioSinAsig').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "ModificarReportes.aspx/obtenerFoliosReportesSinAsignar",
                data: "{ term: '" + request.term + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            value: item.Nombre,
                            ID: item.Id,
                            label: item.Nombre
                        }
                        alert(item.Id);
                        alert(item.Nombre);
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error en obtenerFoliosReportesSinAsignar');
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            var value = ui.item.value;
            var id = $(ui.item).attr("ID");
            $(idObjeto).val(id); // Valor de id oculto en la vista      
            //obtenerTipoTarea();
        },
        change: function (event, ui) {
            if (ui.item == null || ui.item == undefined) {
                $(idObjeto).val("");
                $(nombre).val("");
            }
        }
    });
}

//Autocomplete Folios (GENERAL)
function obtenerFoliosReportes(idObjeto, nombre, metodo) {
    var idFolio = $('#txtIdFolio').val();
    $('#txtFolio').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "ModificarReportes.aspx/obtenerFoliosReportes",
                data: "{ term: '" + request.term + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            value: item.Nombre,
                            ID: item.Id,
                            label: item.Nombre
                        }
                        alert(item.Id);
                        alert(item.Nombre);
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error en obtenerFoliosReportes');
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            var value = ui.item.value;
            var id = $(ui.item).attr("ID");
            $(idObjeto).val(id); // Valor de id oculto en la vista      
            //obtenerTipoTarea();
        },
        change: function (event, ui) {
            if (ui.item == null || ui.item == undefined) {
                $(idObjeto).val("");
                $(nombre).val("");
            }
        }
    });
}

//Funcion para llenar los list del detalle de reporte
function getListReporte(idPrioridad, idTipoIncidencia,  idGrupo,  idSistema) {
    block();
    $("#divPrioridad").html("");
    $("#divTipoIncidencia").html("");
    $("#divGrupo").html("");
    $("#divArea").html("");
    $.ajax({
        url: "ModificarReportes.aspx/getDdlReporte",
        type: "POST",
        data: "{idPrioridad: " + idPrioridad + ", idTipoIncidencia : " + idTipoIncidencia + " ,idGrupo :" + idGrupo + " ,idSistema : " + idSistema + " } ",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            $("#divPrioridad").html(data.d[0]);
            $("#divTipoIncidencia").html(data.d[1]);
            $("#divGrupo").html(data.d[2]);
            $("#divArea").html(data.d[3]);
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Función con la cual se cargan los sistemas asociados al grupo seleccionado por el usuario
function ddlSistemas() {
    block();
    $('#ddlSistemaG').empty();
    $("#ddlSistemaG").removeAttr("disabled");
    var idGrupo = $("#ddlERPGrupo").val();;
    $.ajax({
        url: "ModificarReportes.aspx/getDdlSistemasGrupo",
        type: "POST",
        dataType: "JSON",
        data: "{idGrupo:" + idGrupo + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#divArea").html(data.d);
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Botón por medio del cual se realizan las validaciones de los campos para agregar un reporte, además de invocar la función para
//agregar reportes
function validarReporte() {
    var bandera = true;

    var tipoIncidencia = parseInt($("#ddlTipoIncidencia").val()) ;
    var tiposExcluidos = [5, 6, 7, 8, 9, 10, 11, 12, 13]

    if (tiposExcluidos.indexOf(tipoIncidencia) === -1) {
        idSistema = $("#ddlSistemaG").val();
        idERPGrupo = $("#ddlERPGrupo").val();
        $(".validaCombo").each(function () {
            if ($(this).val() == null) {
                $(this).focus().after("<span class='requerido-campos'>Seleccione una opción</span>");
                setTimeout(function () {
                    $(".requerido-campos").fadeOut('slow');
                }, 3000);
                bandera = false;
            }
        });
    } else {
        idSistema = 0;
        idERPGrupo = $("#ddlERPGrupo").val();
        $(".validaCombo").each(function () {
            // Verifica si el elemento actual es #ddlSistemaG y si lo es, omite la validación
            if ($(this).attr("id") !== "ddlSistemaG") {
                if ($(this).val() == null) {
                    $(this).focus().after("<span class='requerido-campos'>Seleccione una opción</span>");
                    setTimeout(function () {
                        $(".requerido-campos").fadeOut('slow');
                    }, 3000);
                    bandera = false;
                }
            }
        });
    }
    



    console.log("Reporte generado.");
    if (bandera) {
        modificarReporte();
    }
}


//Función para agregar un nuevo reporte
function modificarReporte() {
    block();
    var idReporte = $("#txtIdReporte").val();
    var idPrioridad = $("#ddlPrioridad").val();
    var idTipoReporte = $("#ddlTipoReporte").val();
    var idERPGrupo = 0;
    var idSistema = 0;

    var tipoIncidencia = Number(idTipoReporte);

    if (tipoIncidencia < 5 || tipoIncidencia == 14 || tipoIncidencia == 15 || tipoIncidencia == 16 || tipoIncidencia == 17 || tipoIncidencia == 18 || tipoIncidencia == 19 || tipoIncidencia == 20 || tipoIncidencia == 21 || tipoIncidencia == 22) {
        idSistema = $("#ddlSistemaG").val();
        idERPGrupo = $("#ddlERPGrupo").val();
    } else {
        idERPGrupo = $("#ddlERPGrupo").val();
        idSistema = 0;
    }

    if (idTipoReporte == 1 || idTipoReporte == 2 || idTipoReporte == 3 || idTipoReporte == 4 || idTipoReporte == 14 || idTipoReporte == 15 || idTipoReporte == 16 || idTipoReporte == 17 || idTipoReporte == 18 || idTipoReporte == 19 || idTipoReporte == 20 || idTipoReporte == 21 || idTipoReporte == 22) {
        idERPGrupo = $("#ddlERPGrupo").val();
        idSistema = $("#ddlSistemaG").val();
       
    }
    else if (idTipoReporte == 5 || idTipoReporte == 6 || idTipoReporte == 7 || idTipoReporte == 8 || idTipoReporte == 9) {
        idERPGrupo = $("#ddlERPGrupo").val();
        idSistema = 0;
    } else if (idTipoReporte == 25) {
        idERPGrupo = $("#ddlERPGrupo").val();
        idSistema = $("#ddlSistemaG").val();
    }
    console.log("{ idReporte:" + idReporte + ", idTipoReporte:" + idTipoReporte + ", idPrioridad: " + idPrioridad + ", idSistema:" + idSistema + ", idERPGrupo:" + idERPGrupo);
    $.ajax({
        url: "ModificarReportes.aspx/modificarReporte",
        type: "POST",
        data: "{ idReporte:" + idReporte + ", idTipoReporte:" + idTipoReporte + ", idPrioridad: " + idPrioridad + ", idSistema:" + idSistema + ", idERPGrupo:" + idERPGrupo + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            if (data.d == true) {
                alertify.success("<span id='icon-25' class='success blanco' ></span>Se modificó correctamente la incidencia.");
                if (isVerTodos) {
                    $('#btnVerTodos').click();
                } else {
                    $('#btnBuscar').click();
                }
                
            } else {
                alertify.error("<span id='icon-25' class='error blanco'></span>No se modificó la incidencia");
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);

        }
    });
    $('#dlogReporte').dialog("close");
    ocultarLog();
}