$(document).ready(function () {
    $('#divAutocompleteResponsable').hide();
    $('#divAutocompleteApoyo').hide();
    //Ocultar la sección de Filtros de búsqueda
    $('#btnGenerarPdfReportesAsignados').hide();
    $('#btnGenerarPdfReportesSinAsignar').hide();
    $("#divTblLog").hide();
    //OCultar boton para generar reportes
    $('#btnGenerarReporte').hide();
    //Ocultar gráficas al inicio.
    $('#container').css('display', 'none');
    //Ocultar el div que contiene a la gráfica
    $('#divGrafica').hide();
    //Ocultar el boton para ocultar el historial del reporte
    $("#btnTblLogReporteOcultar").hide();
    //Función que ayuda a generar la imgagen de la gráfica a PDF
    (function (H) {
        H.Chart.prototype.createCanvas = function (divId) {
            var svg = this.getSVG(),
                width = parseInt(svg.match(/width="([0-9]+)"/)[1]),
                height = parseInt(svg.match(/height="([0-9]+)"/)[1]),
                canvas = document.createElement('canvas');

            canvas.setAttribute('width', width);
            canvas.setAttribute('height', height);

            if (canvas.getContext && canvas.getContext('2d')) {

                canvg(canvas, svg);

                return canvas.toDataURL("image/jpeg");

            }
            else {
                alert("Your browser doesn't support this feature, please use a modern browser");
                return false;
            }

        }
    }(Highcharts));    
    //Saber si el Checkbox de "Asignados" y "Sin Asignar" estan seleciconados para que muestre el botón para Generar Reporte        
    $('#chkAsignados').click(function () {
        var chkAsignados = $('#chkAsignados').is(':checked');
        if (chkAsignados == true) {
            $('#btnGenerarPdfReportesAsignados').show();
        } else {
            $('#btnGenerarPdfReportesAsignados').hide();
        }
    });
    $('#chkSinAsignar').click(function () {
        var chkSinAsignar = $('#chkSinAsignar').is(':checked');
        if (chkSinAsignar == true) {
            $('#btnGenerarPdfReportesSinAsignar').show();
        } else {
            $('#btnGenerarPdfReportesSinAsignar').hide();
        }
    });
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
    //Dialog para la lista de Responsables y Apoyo
    $("#dlogLstApoyo").dialog({
        modal: true,
        autoOpen: false,
        height: 800,
        width: 900,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            //Funciones a ejecutar al cerrar el dialog del detalle de reporte
            $('#txtIdApoyo').val('');
            $('#txtApoyo').val('');
            $('#txtIdResponsable').val('');
            $('#txtResponsable').val('');
        },
        open: function (event, ui) {                            
            cargarPersonalApoyo("#txtIdApoy", "#txtApoy", "obtenerPersonalApoyo");
            cargarPersonalResponsable("#txtIdResponsable", "#txtResponsable", "obtenerPersonalApoyo"); 
        }
    });
    //Dialog para verificar la eliminacion de reporte
    $("#dlogEliminarReporte").dialog({
        modal: true,
        autoOpen: false,
        height: 290,
        width: 450,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            $('#txtJustifi').val('');
            $('#txtJustifi').removeAttr('placeholder');
            $('#alerta').addClass('hide');
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
    });
   //Cerrar Dialog de la lista y agregado de responsables y apoyo
    $('#btnCerrarDlogResponsables').click(function (e) {
        e.preventDefault();
        $('#dlogLstApoyo').dialog("close");
    });
    //Cerrar Dialog para preguntar si generará el PDF
    $('#btnCerrarDlogPDF').click(function (e) {
        e.preventDefault();
        $('#dlogPDF').dialog("close");
    });
    //Clic en SI para generar el PDF
    $('#btnGenerarPDF').click(function () {
        generarGraficaPDF();
        $('#dlogPDF').dialog("close");
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
            var idAsignado   = 0;
            var idSinAsignar = 0;
            var idPorValidar = 0;
            var idTerminado =  0;
            var idGrupo     =  0;

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
            if ($('#chkTipoReporteFallo').prop('checked')) {
                idFallo = $('#chkTipoReporteFallo').val();
            } else {
                idFallo;
            }

            var idFuncionalidad = 0;
            if ($('#chkTipoReporteFuncionalidad').prop('checked')) {
                idFuncionalidad = $('#chkTipoReporteFuncionalidad').val();
            } else {
                idFuncionalidad;
            }

            var idERP = 0;
            if ($('#chkTipoReporteErp').prop('checked')) {
                idERP = $('#chkTipoReporteErp').val();
            } else {
                idERP;
            }
            //alert('idAsignado: ' + idAsignado + ', idSinAsignar: ' + idSinAsignar + ',idPorValidar: '+idPorValidar+', idTerminado: '+idTerminado+', folio: ' + folio + ', fechaInicio: ' + fechaInicio + ', fechaFin: ' + fechaFin + ', idFallo: ' + idFallo + ', idFuncionalidad: ' + idFuncionalidad + ', idERP: ' + idERP);
            getTblReportesFiltrados(idAsignado, idSinAsignar,idPorValidar,idTerminado, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP, idGrupo);
            generarGraficasPorFiltros(idAsignado, idSinAsignar, idPorValidar, idTerminado, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP,idGrupo);
            //generarGraficasDinamicas(idAsignado, idSinAsignar, idPorValidar, idTerminado, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP);
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
            $('#btnGenerarPdfReportesAsignados').show();
        } else {
            $('#chkAsignados').prop("checked", true);
            $('#btnGenerarPdfReportesAsignados').show();
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
        
        var folio='';
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

        var idFallo= 0;
        if ($('#chkTipoReporteFalloAsig').prop('checked')) {
            idFallo = $('#chkTipoReporteFalloAsig').val();
        } else {
            idFallo;
        }

        var idFuncionalidad = 0;
        if ($('#chkTipoReporteFuncionalidadAsig').prop('checked')) {
            idFuncionalidad = $('#chkTipoReporteFuncionalidadAsig').val();
        } else {
            idFuncionalidad;
        }

        var idERP= 0;
        if ($('#chkTipoReporteErpdAsig').prop('checked')) {
            idERP = $('#chkTipoReporteErpdAsig').val();
        } else {
            idERP;
        }

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
        if ($('#chkTipoReporteFalloSinAsig').prop('checked')) {
            idFallo = $('#chkTipoReporteFalloSinAsig').val();
        } else {
            idFallo;
        }

        var idFuncionalidad = 0;
        if ($('#chkTipoReporteFuncionalidadSinAsig').prop('checked')) {
            idFuncionalidad = $('#chkTipoReporteFuncionalidadSinAsig').val();
        } else {
            idFuncionalidad;
        }

        var idERP = 0;
        if ($('#chkTipoReporteErpSinAsig').prop('checked')) {
            idERP = $('#chkTipoReporteErpSinAsig').val();
        } else {
            idERP;
        }

        //alert('idAsignado: ' + idAsignado + ', idSinAsignar: ' + idSinAsignar + ', folio: ' + folio + ', fechaInicio: ' + fechaInicio + ', fechaFin: ' + fechaFin + ', idFallo: ' + idFallo + ', idFuncionalidad: ' + idFuncionalidad + ', idERP: ' + idERP);

        getTblReportesSinAsignarFiltrado(idAsignado, idSinAsignar, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP);

    });
    //Generar PDF de Reportes Asignados segun filtros
    $('#btnGenerarPdfReportesAsignados').click(function (e) {
        e.preventDefault();
        var idSinAsignar = 0;
        var idAsignado = 0;
        if ($('#chkAsignados').val() == 2) {
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
        if ($('#chkTipoReporteFalloAsig').prop('checked')) {
            idFallo = $('#chkTipoReporteFalloAsig').val();
        } else {
            idFallo;
        }

        var idFuncionalidad = 0;
        if ($('#chkTipoReporteFuncionalidadAsig').prop('checked')) {
            idFuncionalidad = $('#chkTipoReporteFuncionalidadAsig').val();
        } else {
            idFuncionalidad;
        }

        var idERP = 0;
        if ($('#chkTipoReporteErpdAsig').prop('checked')) {
            idERP = $('#chkTipoReporteErpdAsig').val();
        } else {
            idERP;
        }

        reportesAsignadosPDFWeb(idAsignado, idSinAsignar, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP);

    });
    //Generar PDF de Reportes Asignados segun filtros
    $('#btnGenerarPdfReportesSinAsignar').click(function (e) {
        e.preventDefault();
        var idSinAsignar = 0;
        var idAsignado = 0;
        if ($('#chkSinAsignar').val() == 1) {
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
        if ($('#chkTipoReporteFalloSinAsig').prop('checked')) {
            idFallo = $('#chkTipoReporteFalloSinAsig').val();
        } else {
            idFallo;
        }

        var idFuncionalidad = 0;
        if ($('#chkTipoReporteFuncionalidadSinAsig').prop('checked')) {
            idFuncionalidad = $('#chkTipoReporteFuncionalidadSinAsig').val();
        } else {
            idFuncionalidad;
        }

        var idERP = 0;
        if ($('#chkTipoReporteErpSinAsig').prop('checked')) {
            idERP = $('#chkTipoReporteErpSinAsig').val();
        } else {
            idERP;
        }

        reportesSinAsignarPDFWeb(idAsignado, idSinAsignar, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP);

    });
    //Generar PDF General
    $('#btnGenerarPdfGeneral').click(function () {
        /////////////ASIGNADOS/////////////////
        var idAsignadoA = 0;
        if ($('#chkAsignados').is(':checked')) {
            idAsignadoA = $('#chkAsignados').val();
        } else {
            idAsignadoA;
        }
        var folioA = '';
        if ($('#txtIdFolioAsig').val() !== '') {
            folioA = $('#txtIdFolioAsig').val();
        } else {
            folioA;
        }

        var fechaInicioA = '';
        if ($('#txtFechaInicioAsig').val() !== '') {
            fechaInicioA = $('#txtFechaInicioAsig').val();
        } else {
            fechaInicioA;
        }

        var fechaFinA = '';
        if ($('#txtFechaFinAsig').val() !== '') {
            fechaFinA = $('#txtFechaFinAsig').val();
        } else {
            fechaFinA;
        }

        var idFalloA = 0;
        if ($('#chkTipoReporteFalloAsig').prop('checked')) {
            idFalloA = $('#chkTipoReporteFalloAsig').val();
        } else {
            idFalloA;
        }

        var idFuncionalidadA = 0;
        if ($('#chkTipoReporteFuncionalidadAsig').prop('checked')) {
            idFuncionalidadA = $('#chkTipoReporteFuncionalidadAsig').val();
        } else {
            idFuncionalidadA;
        }

        var idERPA = 0;
        if ($('#chkTipoReporteErpdAsig').prop('checked')) {
            idERPA = $('#chkTipoReporteErpdAsig').val();
        } else {
            idERPA;
        }
        ////////////FIN ASIGNADOS/////////////////

        ///////////SIN ASIGNAR////////////////////
        var idSinAsignarS = 0;
        if ($('#chkSinAsignar').is(':checked')) {
            idSinAsignarS = $('#chkSinAsignar').val();
        } else {
            idSinAsignarS;
        }
        var folioS = '';
        if ($('#txtIdFolioSinAsig').val() !== '') {
            folioS = $('#txtIdFolioSinAsig').val();
        } else {
            folioS;
        }

        var fechaInicioS = '';
        if ($('#txtFechaInicioSinAsig').val() !== '') {
            fechaInicioS = $('#txtFechaInicioSinAsig').val();
        } else {
            fechaInicioS;
        }

        var fechaFinS = '';
        if ($('#txtFechaFinSinAsig').val() !== '') {
            fechaFinS = $('#txtFechaFinSinAsig').val();
        } else {
            fechaFinS;
        }

        var idFalloS = 0;
        if ($('#chkTipoReporteFalloSinAsig').prop('checked')) {
            idFalloS = $('#chkTipoReporteFalloSinAsig').val();
        } else {
            idFalloS;
        }

        var idFuncionalidadS = 0;
        if ($('#chkTipoReporteFuncionalidadSinAsig').prop('checked')) {
            idFuncionalidadS = $('#chkTipoReporteFuncionalidadSinAsig').val();
        } else {
            idFuncionalidadS;
        }

        var idERPS = 0;
        if ($('#chkTipoReporteErpSinAsig').prop('checked')) {
            idERPS = $('#chkTipoReporteErpSinAsig').val();
        } else {
            idERPS;
        }
        ////////////FIN SIN ASIGNAR///////////////
        alert(idAsignadoA + ' ' + folioA + ' ' + fechaInicioA + ' ' + fechaFinA + ' ' + idFalloA + ' ' + idFuncionalidadA + ' ' + idERPA + ' ' + idSinAsignarS + ' ' + folioS + ' ' + fechaInicioS + ' ' + fechaFinS + ' ' + idFalloS + ' ' + idFuncionalidadS + ' ' + idERPS);
        generarPdfGeneral(idAsignadoA, folioA, fechaInicioA, fechaFinA, idFalloA, idFuncionalidadA, idERPA, idSinAsignarS, folioS, fechaInicioS, fechaFinS, idFalloS, idFuncionalidadS, idERPS);
    });
    //Generar PDF con las tablas filtradas y la gráfica
    $('.btnGenerarPDF').click(function () {
        generarGraficaPDF();
    });
    $('.btnGenerarPDF').hide();
    //Al dar clic en "Ver Historial" este mostrara la tabla de las acciones del reporte
    //$("#btnTblLogReporte").click(function () {
    //    $('#divDetalle').hide();
    //    $("#divTblLog").show();
    //    detalleAccionesReporte();
    //    $("#btnTblLogReporte").hide();
    //    $("#btnTblLogReporteOcultar").show();
    //});
    //Al dar clic en ocultar Historial este ocultara la tabla y mostrará el boton de "Ver Historial"
    $("#btnTblLogReporteOcultar").click(function () {
        $("#divTblLog").empty();
        $("#btnTblLogReporteOcultar").hide();
        $("#btnTblLogReporte").show();
        $('#divDetalle').show();
    });
    //Cambiar de clor el icono de PDF cada que s ecoloncan ensima o fuera
    $('.btnGenerarPDF').mouseover(function () {
        $(this).removeClass('azul').addClass('verde');
    });
    $(".btnGenerarPDF").mouseout(function () {
        $(this).removeClass('verde').addClass('azul');
    });
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

    //Eliminar Reporte
    $('#btnSi').click(function () {
        var justifi = $('#txtJustifi').val();
        var idReporte = $('#valReporte').val();
        if ($.trim($('#txtJustifi').val()) !== '') {
            block();
            $.ajax({
                url: "ConsultarReportes.aspx/eliminarReporte",
                type: "POST",
                data: "{idReporte:" + idReporte + "}",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    if (data.d == true) {
                        unBlock();
                        alertify.success("<span id='icon-25' class='success blanco'></span>Se eliminó correctamente el Reporte.");
                        $('#' + idReporte + '').closest('tr').remove().fadeOut(1000);
                        $("#dlogEliminarReporte").dialog("close").fadeIn();
                        guardarJustificacion(idReporte, justifi);
                        //$('#divTblReportes').empty();
                        //getTblReportes();
                    } else {
                        unBlock();
                        alertify.error("<span id='icon-25' class='warning blanco'></span>No se eliminó el Reporte.");
                    }
                },
                error: function (xhr, status, error) {
                    unBlock();
                    alertify.error("<span id='icon-25' class='warning blanco'></span>Error en eliminarReporteSinAsignar");
                }
            });
        } else {

            $('#txtJustifi').attr('placeholder', 'Escribe la Justificación');
            $('#alerta').removeClass('hide');
        }
    });
    $('#btnNo').click(function () {
        $("#dlogEliminarReporte").dialog("close").fadeIn();
    });
    //Mostral al inicio todos los reportes
    //getTblReportes();    
});

//Traer todos los reportes (GENERAL)
function getTblReportes() {
    block();
    $.ajax({
        url: "ConsultarReportes.aspx/getTblReportes",
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
//Eliminar Reporte (GENERAL)
function eliminarReporte(idReporte) {
    $("#dlogEliminarReporte").dialog("open");
    $('#valReporte').val(idReporte);
}
//Consultar Reporte (GENERAL)
function consultarReporte(idReporte) {
    block();
    obtenerResponsableNuevo(idReporte);//Agrega en un label blanco el responsable
    
    $('#divDetalle').show();
    $('#dlogReporte').dialog("open");
    $.ajax({
        url: "ConsultarReportes.aspx/consultarReporte",
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
            
            $('#iconPersonal').append('<span id="icon-25" class="usuarios azul lblResponsables' + data.d[0] + '" '+
                                            'title="Asignar Apoyo para realizar esta Tarea" '+
                                            'onclick="tblPersonalApoyo(' + data.d[0] + ');'+
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
            detalleAccionesReporte();
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
        url: "ConsultarReportes.aspx/obtenerResponsableNuevo",
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
        url: "ConsultarReportes.aspx/obtenerApoyoNuevo",
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
        url: "ConsultarReportes.aspx/tblPersonalApoyo",
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
        url: "ConsultarReportes.aspx/tblPersonalResponsable",
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
    if ($.trim(idResponsable)=='') {
        $('#txtApoyo').focus();
    } else {
        $.ajax({
            url: "ConsultarReportes.aspx/guardarPersonaApoyo",
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
        url: "ConsultarReportes.aspx/eliminarPersonaApoyo",
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
            url: "ConsultarReportes.aspx/guardarPersonaResponsable",
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
        url: "ConsultarReportes.aspx/eliminarPersonaResponsable",
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
    if ($.trim($('#txtIdResponsabl').val()) !=='') {
        block();
        $.ajax({
            url: "ConsultarReportes.aspx/editarPersonaResponsable",
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
            url: "ConsultarReportes.aspx/guardarPersonaApoyo",
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
        url: "ConsultarReportes.aspx/getTblReportesFiltrados",
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
                $('#divGrafica').hide();
                $('.btnGenerarPDF').hide();
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
//Graficas Dinamicas (GENERAL)
function generarGraficasPorFiltros(idAsignado, idSinAsignar, idPorValidar, idTerminado, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP, idERPGrupo) {    
    block();
    $.ajax({
        url: "ConsultarReportes.aspx/generarGraficas",
        type: "POST",
        data: "{idAsignado:" + idAsignado + ", idSinAsignar:" + idSinAsignar + ", idPorValidar:" + idPorValidar + ",idTerminado:" + idTerminado + ", folio:'" + folio + "', fechaInicio:'" + fechaInicio + "', fechaFin:'" + fechaFin + "', idFallo:" + idFallo + ", idFuncionalidad:" + idFuncionalidad + ", idERP:" + idERP + ", idERPGrupo:" + idERPGrupo + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d.length !==0) {
                var datos = [];// Array para agregar los Estatus de Reporte y la cantidad de estos mismos.
                for (var i = 0; i < data.d.length; i += 2) {
                    if (data.d[i] == 1) {
                        datos.push([
                            'Sin Asignar',
                            parseInt(data.d[i + 1])
                        ]);
                        datos + ',';
                    }
                    if (data.d[i] == 2) {
                        datos.push([
                            'Asignado',
                            parseInt(data.d[i + 1])
                        ]);
                        datos + ',';
                    }
                    if (data.d[i] == 3) {
                        datos.push([
                            'Por Validar',
                            parseInt(data.d[i + 1])
                        ]);
                        datos + ',';
                    }
                    if (data.d[i] == 5) {
                        datos.push([
                            'Terminado',
                            parseInt(data.d[i + 1])
                        ]);
                        datos + ',';
                    }
                }

                $('#divGrafica').show();
                var Asignado    = "";    var Fallo          = "";
                var SinAsignar  = "";    var Funcionalidad  = "";
                var PorValidar  = "";    var ERP            = "";
                var Terminado   = "";    var idGrup     = "";
                var filtros     = "";
                if (Asignado == "" && SinAsignar == "" && PorValidar == "" && Terminado == "" && Fallo == "" && Funcionalidad == "" && ERP == "" && idERPGrupo == "") {
                    filtros += 'Todos los filtros';
                    filtros += ',';
                }
                if (idERPGrupo > 0) {
                    filtros += 'Grupo';
                    filtros += ',';
                }
                if (idAsignado == 2) {
                    filtros += 'Asignados';
                    filtros += ',';
                }
                if (idSinAsignar == 1) {
                    filtros += 'Sin Asignar';
                    filtros += ',';
                }
                if (idPorValidar == 3) {
                    filtros += 'Por Validar';
                    filtros += ',';
                }
                if (idTerminado == 5) {
                    filtros += 'Terminados';
                    filtros += ',';
                }
                if (idFallo == 1) {
                    filtros += 'Fallos';
                    filtros += ',';
                }
                if (idFuncionalidad == 2) {
                    filtros += 'Funcionalidad';
                    filtros += ',';
                }
                if (idERP == 3) {
                    filtros += 'ERP';
                    filtros += ',';
                }

                pintarGrafica(datos, filtros.slice(0, -1));
                //generarGraficaPDF();
                unBlock();
            } else {
                $('#divGrafica').hide();
            }
            
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en generarGraficas.");
            unBlock();
        }
    });
}
//Función que pinta la grafica con los datos enviandos
function pintarGrafica(arrayDatos,filtros) {
    $('#container').css('display', 'block');
    // Create the chart
    $('#container').highcharts({
        allowDecimals: false,
        exporting: {
            chartOptions: { // specific options for the exported image
                plotOptions: {
                    series: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                }
            },
            scale: 3,
            fallbackToExportServer: false
        },
        chart: {
            type: 'column',
        },
        title: {
            text: 'Estatus de los Reportes'
        },
        subtitle: {
            //text: 'Információn para visualizar cuántos Reportes hay en determinados Estatus (Asignados/Creado, Sin Asignar, Por Validar, Terminados)'
            text: 'Información devuelta por: '+filtros
        },
        xAxis: {
            type: 'category',
            labels: {
                rotation: 0,
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif'
                }
            }

        },
        yAxis: {
            title: {
                text: 'Cantidad'
            }

        },
        legend: {
            enabled: false
        },
        plotOptions: {
            series: {
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    format: '{point.y:.0f}'
                }
            }
        }, series: [{
            name: 'Descripción:',
            colorByPoint: true,
            data: arrayDatos,
        }],
        tooltip: {
            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.0f} </b> en Total<br/>'
        }
    });
}
//**EXPORTAR GRAFICA (PDF)
function generarGraficaPDF() {
    $('td.ocultar').addClass('hide');
    $('th.ocultar').addClass('hide');
    $('#tblReportes').dataTable().fnDestroy();
    //var doc = new jsPDF();
    var heightDiv = $('#divTblReportes').height();
   
    var doc = new jsPDF('l', 'pt');
    doc.setFontSize(20);
    doc.text(35, 25, "Resultados");
    //Print Table    
    var elem = document.getElementById("tblReportes");
    var res = doc.autoTableHtmlToJson(elem);
    doc.autoTable(res.columns, res.data);
    //End Print Table
    doc.addPage();
    // chart height defined here so each chart can be palced
    // in a different position
    var chartHeight = 400;//alto de la imagen de gráfia

    // All units are in the set measurement for the document
    // This can be changed to "pt" (points), "mm" (Default), "cm", "in"
    doc.setFontSize(20);
    doc.text(35, 25, "Gráfica de resultados");

    //loop through each chart
    $('.grafica').each(function (index) {
        var imageData = $(this).highcharts().createCanvas();
        // add image to doc, if you have lots of charts,
        // you will need to check if you have gone bigger 
        // than a page and do doc.addPage() before adding 
        // another image.

        /**
        * addImage(imagedata, type, x, y, width, height)
        */
        doc.addImage(imageData, 'JPEG', 100, 50, 650, chartHeight);
    });

    //doc.output("dataurlnewwindow");//Abrir PDF en una ventana aparte
    //save with name
    doc.save('Reporte.pdf');
    $('td.ocultar').removeClass('hide');
    $('th.ocultar').removeClass('hide');
    $("#tblReportes").dataTable({
        "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
        "stripeClasses": ['gray', 'white'],
        order: [[7, 'asc']],
        "aoColumnDefs": [
                       { "sClass": "txt-left", "aTargets": [1, 3,4,5] }
        ],
    });
    $("#tblReportes").on('draw.dt', function () {
        $("#grid-head1").addClass("round-border2");
    });
}
//Función con la cual se genera la tabla en la que se encuentra el log de las acciones que se ha realizado con cada uno de los reportes
//Y ver el estatus en el que se encuentra el reporte
function detalleAccionesReporte() {
    block();
    var idReporte = $("#txtIdReporte").val();
    
    $.ajax({
        url: "ConsultarReportes.aspx/detalleAccionesReporte",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            $("#divTblLog").empty();
            $("#divTblLog").show();
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
//Guardar la justifiación al dar clic en eliminar reporte
function guardarJustificacion(idReporte, justifi) {
    block();
    $.ajax({
        url: "ConsultarReportes.aspx/guardarJustificacion",
        type: "POST",
        data: "{idReporte:" + idReporte + ", justifi:'" + justifi + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == true) {
                //alertify.success("<span id='icon-25' class='success blanco'></span>Se Guardo la justificación.");                
            } else {
                alertify.error("<span id='icon-25' class='warning blanco'></span>No se Guardo la justificación.");
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en guardarJustificacion.");
            unBlock();
        }
    });
}
//Cargar los Grupos en un autocomplete
function cargarGrupos(idObjeto, nombre, metodo) {
    var idGrupo = $('#txtIdGrupo').val();
    $('#txtNomGrupo').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "ConsultarReportes.aspx/obtenerGrupos",
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

function setClickableTooltip(target, content){
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
                url: "ConsultarReportes.aspx/" + metodo + "",
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
                url: "ConsultarReportes.aspx/" + metodo + "",
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
                url: "ConsultarReportes.aspx/obtenerFoliosReportesAsignados",
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
                url: "ConsultarReportes.aspx/obtenerFoliosReportesSinAsignar",
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
                url: "ConsultarReportes.aspx/obtenerFoliosReportes",
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
