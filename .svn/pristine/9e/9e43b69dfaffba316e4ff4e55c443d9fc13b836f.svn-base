
$(document).ready(function () {
    var idReporte = $('#txtIdReporte').val();
    $('#txtIdReporte').hide();
    $('#txtIdUsuario').hide();    
    //Función para obtener los sistemas solicitados por un grupo.
    obtenerSistemasSolicitados(idReporte);
    //Función para saber si la base de EMP existe y en que estatus se quedo de ejecución
    existeEstatusDBEMP(idReporte);
    $("#dlogHistorial").dialog({
        modal: true,
        autoOpen: false,
        height: 600,
        width: 950,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            $('#divTblHistorial').empty();
        },
        open: function (event, ui) {
            verHistorialBDEjecucion();
        }
    });
    $("#dlogSGI").dialog({
        modal: true,
        autoOpen: false,
        height: 170,
        width: 300,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            $('#txtRutaSGI').empty();
        },
        open: function (event, ui) {
            $('#txtRutaSGI').focus();
        }
    });
    //Cerrar Dialog del Detalle del Reporte
    $('#btnCerrarDlogHistorial').click(function (e) {
        e.preventDefault();
        $('#dlogHistorial').dialog("close");
    });
    $('#btnCerrarDlogSGI').click(function (e) {
        e.preventDefault();
        $('#dlogSGI').dialog("close");
    });
    $('#btnVerHistorial').click(function () {
        $("#dlogHistorial").dialog("open");
    });
    $('#btnTerminar').click(function () {
        $("#dlogSGI").dialog("open");
    });
    $('#btnGuardarSGI').click(function (e) {
        e.preventDefault();
        guardarSGI();
    });
});

//Funciones Propias del Traking para avanzar a la sigiente tarea o a la anterior
function nextTab(elem) {
    $(elem).next().find('a[data-toggle="tab"]').click();
}
function prevTab(elem) {
    $(elem).prev().find('a[data-toggle="tab"]').click();
}
//Obtener el Nombre del Grupo y los Sistemas que Solicito
function obtenerSistemasSolicitados(idReporte) {    
    block();
    $.ajax({
        url: "Sistemas.aspx/obtenerSistemasSolicitados",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var contador = 0;
            var botonGenerarBD = '';
            var botonCambiarEsquemas = '';
            var botonLimpiarBD = '';
            //Pintar los cuadros de los Sistemas solicitados
            $.each(data.d, function (i, dato) {
                contador++;
                if (contador==1) {
                    $(".lblGrupo").text(dato.nomGrupo);
                    $("#txtIdGrupo").val(dato.idERPGrupo);
                    $("#txtNomGrupo").val(dato.nomGrupo);                    
                }
                $("#mainbox").append("<div class='card'>" + "" + dato.imagen + "" + "<p class='h1-card' style='text-align: center;'>" + dato.nomSistema + "</p>" + "<input type='button' value='GENERAR' id='btnVerTraking"+dato.idSistema+"' class='btn azul width100 shadow2 borde-blanco center btnVerTraking hide' onClick='javascript:verTrakingPorSistema(" + dato.idSistema + ")'/><input type='hidden' value='" + dato.idSistema + "'>" + "</div>");
            });
            //Pintar el trakin para cada sistema
            var idsSistemas = '';
            $.each(data.d, function (i, dato) {                
                idsSistemas += dato.idSistema + ',';                
                if (dato.idSistema==10) {
                    botonGenerarBD = "<button type='button' class='btn verde next-step loading-btn-botonGenerarBD" + dato.idSistema + "' data-loading-text='Ejecución en proceso...' onclick='generarBDSGC(" + dato.idSistema + ");' id='btnGenerarBDSGC" + dato.idSistema + "'>GENERAR</button>";
                    botonCambiarEsquemas = "<button type='button' class='btn verde next-step loading-btn-botonCambiarEsquemas" + dato.idSistema + "' data-loading-text='Ejecución en proceso...' onclick='cambiarEsquemasBDSGC(" + dato.idSistema + ")' id='btnCambiarEsquemasBDSGC" + dato.idSistema + "'>GENERAR</button>";
                    botonLimpiarBD = "<button type='button' class='btn verde next-step loading-btn-botonLimpiarBD" + dato.idSistema + "' data-loading-text='Ejecución en proceso...' onclick='limpiarBDSGC(" + dato.idSistema + ")' id='btnLimpiarBDSGC" + dato.idSistema + "'>GENERAR</button>";
                } else if (dato.idSistema==1) {
                    botonGenerarBD = "<button type='button' class='btn verde next-step loading-btn-botonGenerarBD" + dato.idSistema + "' data-loading-text='Ejecución en proceso...' onclick='generarBDSGSOX(" + dato.idSistema + ");' id='btnGenerarBDSGSOX" + dato.idSistema + "'>GENERAR</button>";
                    botonCambiarEsquemas = "<button type='button' class='btn verde next-step loading-btn-botonCambiarEsquemas"+dato.idSistema+"' data-loading-text='Ejecución en proceso...' onclick='cambiarEsquemasBDSGSOX(" + dato.idSistema + ")' id='btnCambiarEsquemasBDSGSOX" + dato.idSistema + "'>GENERAR</button>";
                    botonLimpiarBD = "<button type='button' class='btn verde next-step loading-btn-botonLimpiarBD"+dato.idSistema+"' data-loading-text='Ejecución en proceso...' onclick='limpiarBDSGSOX(" + dato.idSistema + ")' id='btnLimpiarBDSGSOX" + dato.idSistema + "'>GENERAR</button>";
                } else if (dato.idSistema==2) {
                    botonGenerarBD = "<button type='button' class='btn verde next-step loading-btn-botonGenerarBD" + dato.idSistema + "' data-loading-text='Ejecución en proceso...' onclick='generarBDSGCE(" + dato.idSistema + ");' id='btnGenerarBDSGCE" + dato.idSistema + "'>GENERAR</button>";
                    botonCambiarEsquemas = "<button type='button' class='btn verde next-step loading-btn-botonCambiarEsquemas"+dato.idSistema+"' data-loading-text='Ejecución en proceso...' onclick='cambiarEsquemasBDSGCE(" + dato.idSistema + ")' id='btnCambiarEsquemasBDSGCE" + dato.idSistema + "'>GENERAR</button>";
                    botonLimpiarBD = "<button type='button' class='btn verde next-step loading-btn-botonLimpiarBD"+dato.idSistema+"' data-loading-text='Ejecución en proceso...' onclick='limpiarBDSGCE(" + dato.idSistema + ")' id='btnLimpiarBDSGCE" + dato.idSistema + "'>GENERAR</button>";
                } else if (dato.idSistema==3) {
                    botonGenerarBD = "<button type='button' class='btn verde next-step loading-btn-botonGenerarBD"+dato.idSistema+"' data-loading-text='Ejecución en proceso...' onclick='generarBDSGRO(" + dato.idSistema + ");' id='btnGenerarBDSGRO" + dato.idSistema + "'>GENERAR</button>";
                    botonCambiarEsquemas = "<button type='button' class='btn verde next-step loading-btn-botonCambiarEsquemas"+dato.idSistema+"' data-loading-text='Ejecución en proceso...' onclick='cambiarEsquemasBDSGRO(" + dato.idSistema + ")' id='btnCambiarEsquemasBDSGRO" + dato.idSistema + "'>GENERAR</button>";
                    botonLimpiarBD = "<button type='button' class='btn verde next-step loading-btn-botonLimpiarBD"+dato.idSistema+"' data-loading-text='Ejecución en proceso...' onclick='limpiarBDSGRO(" + dato.idSistema + ")' id='btnLimpiarBDSGRO" + dato.idSistema + "'>GENERAR</button>";
                } else if (dato.idSistema == 4) {
                    botonGenerarBD = "<button type='button' class='btn verde next-step loading-btn-botonGenerarBD"+dato.idSistema+"' data-loading-text='Ejecución en proceso...' onclick='generarBDSGOV(" + dato.idSistema + ");' id='btnGenerarBDSGOV" + dato.idSistema + "'>GENERAR</button>";
                    botonCambiarEsquemas = "<button type='button' class='btn verde next-step loading-btn-botonCambiarEsquemas"+dato.idSistema+"' data-loading-text='Ejecución en proceso...' onclick='cambiarEsquemasBDSGOV(" + dato.idSistema + ")' id='btnCambiarEsquemasBDSGOV" + dato.idSistema + "'>GENERAR</button>";
                    botonLimpiarBD = "<button type='button' class='btn verde next-step loading-btn-botonLimpiarBD"+dato.idSistema+"' data-loading-text='Ejecución en proceso...' onclick='limpiarBDSGOV(" + dato.idSistema + ")' id='btnLimpiarBDSGOV" + dato.idSistema + "'>GENERAR</button>";
                } else if (dato.idSistema == 5) {
                    botonGenerarBD = "<button type='button' class='btn verde next-step loading-btn-botonGenerarBD"+dato.idSistema+"' data-loading-text='Ejecución en proceso...' onclick='generarBDSGICE(" + dato.idSistema + ");' id='btnGenerarBDSGICE" + dato.idSistema + "'>GENERAR</button>";
                    botonCambiarEsquemas = "<button type='button' class='btn verde next-step loading-btn-botonCambiarEsquemas" + dato.idSistema + "' data-loading-text='Ejecución en proceso...' onclick='cambiarEsquemasBDSGICE(" + dato.idSistema + ")' id='btnCambiarEsquemasBDSGICE" + dato.idSistema + "'>GENERAR</button>";
                    botonLimpiarBD = "<button type='button' class='btn verde next-step loading-btn-botonLimpiarBD" + dato.idSistema + "' data-loading-text='Ejecución en proceso...' onclick='limpiarBDSGICE(" + dato.idSistema + ")' id='btnLimpiarBDSGICE" + dato.idSistema + "'>GENERAR</button>";
                }                
                $("#divTraking").append("<div class='row traking hide' id='trakingSistema" + dato.idSistema + "'>" +
                                        "<section>"+
                                            "<div class='wizard'>"+
                                                "<div class='wizard-inner'>"+
                                                    "<div class='connecting-line'></div>"+
                                                    "<ul class='nav nav-tabs' role='tablist'>"+
                                                        "<div class='form-tit round-border3'>" +
                                                            "<div class='clear width98 center'>"+
					                                            "<div class='left width50'>"+
						                                            "<h3 class='left'>Generar el " + dato.nomSistema + "</h3>" +
					                                            "</div>"+
					                                            "<div class='left width50 divIconUpdated" + dato.idSistema + "'>" +						                                            
                                                                    "<span class='glyphicon glyphicon-refresh glyphicon-refresh-animate icon-update hide iconUpdate" + dato.idSistema + " style='color: green;' title='Ejecutando...'></span>" +
					                                           "</div>	"+		
				                                            "</div>"+
                                                            
                                                            //"<div class='right'><span id='icon-25' onclick='javascript:cerrarTraking(" + dato.idSistema + ")' class='verde cerrar cursor-link divTraking" + dato.idSistema + "'></span></div>"+
                                                        "</div>"+
                                                        "<li role='presentation' class='presentation1" + dato.idSistema + " active'><a href='#step1" + dato.idSistema + "' class='title-change1" + dato.idSistema + "' data-toggle='tab' aria-controls='step1" + dato.idSistema + "' role='tab' title='Crear Base de Datos y Objetos'><span class='round-tab estatus1" + dato.idSistema + "'><i class='glyphicon glyphicon-hdd' id='intermitente1"+dato.idSistema+"'></i></span></a></li>" +
                                                        "<li role='presentation' class='presentation2" + dato.idSistema + " disabled'><a href='#step2" + dato.idSistema + "' class='title-change2" + dato.idSistema + "' data-toggle='tab' aria-controls='step2" + dato.idSistema + "' role='tab' title='Cambiar Esquemas'><span class='round-tab estatus2" + dato.idSistema + "'><i class='glyphicon glyphicon-wrench' id='intermitente2"+dato.idSistema+"'></i></span></a></li>" +
                                                        "<li role='presentation' class='presentation3" + dato.idSistema + " disabled'><a href='#step3" + dato.idSistema + "' class='title-change3" + dato.idSistema + "' data-toggle='tab' aria-controls='step3" + dato.idSistema + "' role='tab' title='Limpiar Base de Datos'><span class='round-tab estatus3" + dato.idSistema + "'><i class='glyphicon glyphicon-trash' id='intermitente3"+dato.idSistema+"'></i></span></a></li>" +
                                                        "<li role='presentation' class='presentation4" + dato.idSistema + " disabled'><a href='#step4" + dato.idSistema + "' class='title-change4" + dato.idSistema + "' data-toggle='tab' aria-controls='step4" + dato.idSistema + "' role='tab' title='URL Sistema'><span class='round-tab estatus4" + dato.idSistema + "'><i class='glyphicon glyphicon-ok' id='intermitente4"+dato.idSistema+"'></i></span></a></li>" +
                                                    "</ul>"+
                                                "</div>"+
                                                "<form role='form'>"+
                                                    "<div class='tab-content'>"+
                                                        "<div class='tab-pane active' role='tabpanel' id='step1" + dato.idSistema + "'>" +
                                                            "<h3>Información:</h3>"+
                                                            "<h4>Generar la Base y Objetos SQl para el "+dato.nomSistema+"</h4>"+
                                                            "<ul class='list-inline pull-right'>"+
                                                                "<li>"+botonGenerarBD+"</li>"+
                                                            "</ul>"+
                                                        "</div>"+
                                                        "<div class='tab-pane' role='tabpanel' id='step2" + dato.idSistema + "'>" +
                                                            "<h3>Información:</h3>"+
                                                            "<h4>CAMBIAR EL ESQUEMA DE LA BASE DE DATOS CON EL NUEVO NOMBRE DEL GRUPO.</h4>" +
                                                            "<ul class='list-inline pull-right'>" +
                                                                "<li>" + botonCambiarEsquemas + "</li>" +
                                                            "</ul>"+
                                                        "</div>"+
                                                        "<div class='tab-pane' role='tabpanel' id='step3" + dato.idSistema + "'>" +
                                                            "<h3>Información:</h3>"+
                                                            "<h4>LIMPIAR LA BASE DE DATOS PARA SU NUEVA CONFIGURACIÓN.</h4>" +
                                                            "<ul class='list-inline pull-right'>"+
                                                                "<li>" + botonLimpiarBD + "</li>" +
                                                            "</ul>"+
                                                        "</div>" +
                                                        "<div class='tab-pane' role='tabpanel' id='step4" + dato.idSistema + "'>" +
                                                            "<h3>Información:</h3>" +
                                                            "<h4>GENERAR URL DEL SISTEMA "+dato.nomSistema+" PARA EL GRUPO "+dato.nomGrupo+".</h4>" +
                                                            "<label id='lblURL"+dato.idSistema+"'>URL:</label>" +
                                                            "<input type='text' id='txtURL"+dato.idSistema+"'>" +
                                                            "<ul class='list-inline pull-right'>" +
                                                                "<li>" +
                                                                    "<button type='button' class='btn verde next-step loading-btn btnUrl"+dato.idSistema+"' onclick='guardarURL(" + dato.idSistema + ")'  data-loading-text='Ejecucion en proceso...' id='btnGenerarSGCI" + dato.idSistema + "'>GENERAR</button>" +
                                                                "</li>" +
                                                            "</ul>" +
                                                        "</div>" +
                                                        "<div class='clearfix'></div>"+
                                                    "</div>" +
                                                "</form>"+
                                            "</div>"+
                                        "</section>"+
                                    "</div>");
                
            });
            $('#txtIdsSistemas').val(idsSistemas.slice(0, -1));//Colocar ids de los sistemas solicitados en una caja de texto oculta
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en obtenerSistemasSolicitados.");
            unBlock();
        }
    });
}
//Saber si el Grupo ya tiene BDEMP (No esta en uso, solo verifica si existe la base creada)
function existeDbEmpresa(idReporte) {
    block();
    $.ajax({
        url: "Sistemas.aspx/existeDbEmpresa",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == '') {
                $('.btnVerTraking').hide();
                $("#divTrakingEmp").append("<div class='row traking' id='trakingDBEMP'>" +
                                        "<section>" +
                                            "<div class='wizard'>" +
                                                "<div class='wizard-inner'>" +
                                                    "<div class='connecting-line'></div>" +
                                                    "<ul class='nav nav-tabs' role='tablist'>" +
                                                        "<div class='form-tit round-border3'>" +
                                                            "<div class='left'>" +
                                                                "<h3 class='left'>Generar Base de Datos de Empresa</h3>" +                                                                
                                                            "</div>" +
                                                            //"<div class='right'><span id='icon-25' onclick='javascript:cerrarTraking()' class='verde cerrar cursor-link divTraking'></span></div>" +
                                                        "</div>" +
                                                        "<li role='presentation' class='active'><a href='#step1' data-toggle='tab' aria-controls='step1' role='tab' title='Crear Base de Datos y Objetos'><span class='round-tab estatus1'><i class='glyphicon glyphicon-eye-open' id='intermitente1'></i></span></a></li>" +
                                                        "<li role='presentation' class='disabled'><a href='#step2' data-toggle='tab' aria-controls='step2' role='tab' title='Cambiar Esquemas'><span class='round-tab estatus2'><i class='glyphicon glyphicon-wrench' id='intermitente2'></i></span></a></li>" +
                                                        "<li role='presentation' class='disabled'><a href='#complete' data-toggle='tab' aria-controls='complete' role='tab' title='Limpiar Base de Datos'><span class='round-tab estatus3'><i class='glyphicon glyphicon-ok' id='intermitente3'></i></span></a></li>" +
                                                    "</ul>" +
                                                "</div>" +
                                                "<form role='form'>" +
                                                    "<div class='tab-content'>" +
                                                        "<div class='tab-pane active' role='tabpanel' id='step1'>" +
                                                            "<h3>Información:</h3>" +
                                                            "<h4>Generar la Base y Objetos SQl para la Base de Datos de Empresa</h4></br>" +
                                                            "<h3 class='rGenerarBDEmpresas hide'>Resultados de Ejecución:</h3>" +
                                                            "<h4 id='rGenerarBDEmpresas'></h4>" +
                                                            "<ul class='list-inline pull-right'>" +
                                                                "<li>" +
                                                                    "<button type='button' class='btn verde next-step' onclick='generarBDEmpresa();' id='btnGenerarBDEMP'>GENERAR</button>"+
                                                                "</li>" +
                                                                "<li>" +
                                                                    "<button type='button' class='btn verde prev-step hide' id=''>ATRÁS</button>" +
                                                                "</li>" +
                                                            "</ul>" +
                                                        "</div>" +
                                                        "<div class='tab-pane' role='tabpanel' id='step2'>" +
                                                            "<h3>Información:</h3>" +
                                                            "<h4>Cambiar el esquema de la base de datos con el nombre del grupo.</h4></br>" +
                                                            "<h3 class='rEsquemasBDEmpresas hide'>Resultados de Ejecución:</h3>" +
                                                            "<h4 id='rEsquemasBDEmpresas'></h4>" +
                                                            "<ul class='list-inline pull-right'>" +
                                                                "<li>" +
                                                                    "<button type='button' class='btn verde next-step' onclick='cambiarEsquemasBDEmpresa()' id='btnCambiarEsquemaBDEMP'>GENERAR</button></li>" +
                                                            "</ul>" +
                                                        "</div>" +
                                                        "<div class='tab-pane' role='tabpanel' id='complete'>" +
                                                            "<h3>Información:</h3>" +
                                                            "<h4>Limpiar la base de datos para su nueva configuración</h4>" +
                                                            "<h3 class='rLimpiarBDEMP hide'>Resultados de Ejecución:</h3>" +
                                                            "<h4 id='rLimpiarBDEMP'></h4>" +
                                                            "<ul class='list-inline pull-right'>" +
                                                                "<li>" +
                                                                    "<button type='button' class='btn verde next-step' onclick='limpiarBDEmpresa();' id='btnLimpiarBDEMP'>GENERAR</button></li>" +
                                                            "</ul>" +
                                                        "</div>" +
                                                        "<div class='clearfix'></div>" +
                                                    "</div>" +
                                                "</form>" +
                                            "</div>" +
                                        "</section>" +
                                    "</div>");
                unBlock();
            } else {
                $('.btnVerTraking').removeClass('hide');
                unBlock();
            }
            /*EVENTOS PARA QUE FUNCIONE EL TRAKING*/
            //Initialize tooltips
            $('.nav-tabs > li a[title]').tooltip();
            //Wizard
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                var $target = $(e.target);
                if ($target.parent().hasClass('disabled')) {
                    return false;
                }
            });
            //$(".next-step").click(function (e) {
            //    var $active = $('.wizard .nav-tabs li.active');
            //    $active.next().removeClass('disabled');
            //    nextTab($active);
            //});
            $(".prev-step").click(function (e) {
                var $active = $('.wizard .nav-tabs li.active');
                prevTab($active);
            });
            /*FIN EVENTOS PARA QUE FUNCIONE EL TRAKING*/
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en existeDbEmpresa.");
            unBlock();
        }
    });
}
//Saber si existe la base de datos y el estatus en el que se quedo la generación de la nueva BD
function existeEstatusDBEMP(idReporte) {
    block();
    $.ajax({
        url: "Sistemas.aspx/existeEstatusDBEMP",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var contador = 0;
            var conocerSistemas = 0;

            jQuery.each(data.d, function (i, dato) {
                conocerSistemas++;
                contador++;
                //alert(dato.existe + ', ' + dato.idReporte + ', ' + dato.idERPGrupo + ', ' + dato.nomGrupo + ', ' + dato.idAccion + ', ' + dato.idEstatus);
                if (dato.existe != null && conocerSistemas > 2 && dato.idEstatus==5 && contador==1) {//Comprueba si solo tiene un estatus en view_ExisteBDEMP
                    //alert('1');
                    //alert(dato.existe + ', ' + dato.idReporte + ', ' + dato.idERPGrupo + ', ' + dato.nomGrupo + ', ' + dato.idAccion + ', ' + dato.idEstatus);
                    $('#trakingDBEMP').hide();
                    $('.btnVerTraking ').removeClass('hide');
                } else if (dato.existe == null && conocerSistemas < 2) {
                    //alert('2');
                    if (contador == 1) {
                        $("#divTrakingEmp").append("<div class='row traking' id='trakingDBEMP'>" +
                                                "<section>" +
                                                    "<div class='wizard'>" +
                                                        "<div class='wizard-inner'>" +
                                                            "<div class='connecting-line'></div>" +
                                                            "<ul class='nav nav-tabs' role='tablist'>" +
                                                                "<div class='form-tit round-border3'>" +
                                                                    "<div class='clear width98 center'>" +
                                                                        "<div class='left width50'>" +
                                                                            "<h3 class='left'>Generar Base de Datos de Empresa</h3>" +
                                                                        "</div>" +
                                                                        "<div class='left width50'>" +
                                                                            "<span class='glyphicon glyphicon-refresh glyphicon-refresh-animate icon-update hide iconUpdate' style='color: green;' title='Ejecutando...'></span>" +
                                                                        "</div>	" +
                                                                    "</div>" +
                                                                "</div>" +
                                                                "<li role='presentation' class='active presentation1 emp'><a href='#step1' data-toggle='tab' aria-controls='step1' class='title-change1' role='tab' title='Crear Base de Datos y Objetos'><span class='round-tab estatus1'><i class='glyphicon glyphicon-hdd' id='intermitente1'></i></span></a></li>" +
                                                                "<li role='presentation' class='disabled presentation2 emp'><a href='#step2' data-toggle='tab' aria-controls='step2' class='title-change2' role='tab' title='Cambiar Esquemas'><span class='round-tab estatus2'><i class='glyphicon glyphicon-wrench' id='intermitente2'></i></span></a></li>" +
                                                                "<li role='presentation' class='disabled presentation3 emp'><a href='#complete' data-toggle='tab' aria-controls='complete' class='title-change3' role='tab' title='Limpiar Base de Datos'><span class='round-tab estatus3'><i class='glyphicon glyphicon-trash' id='intermitente3'></i></span></a></li>" +
                                                                "<li role='presentation' class='disabled presentation4 emp'><a href='#step3' data-toggle='tab' aria-controls='complete' class='title-change4' role='tab' title='Configuración Inicial'><span class='round-tab estatus4'><i class='glyphicon glyphicon-tasks' id='intermitente4'></i></span></a></li>" +
                                                            "</ul>" +
                                                        "</div>" +
                                                        "<form role='form'>" +
                                                            "<div class='tab-content'>" +
                                                                "<div class='tab-pane active' role='tabpanel' id='step1'>" +
                                                                    "<h3>Información:</h3>" +
                                                                    "<h4>Generar la Base y Objetos SQl para la Base de Datos de Empresa</h4></br>" +
                                                                    "<h3 class='rGenerarBDEmpresas hide'>Resultados de Ejecución:</h3>" +
                                                                    "<h4 id='rGenerarBDEmpresas'></h4>" +
                                                                    "<ul class='list-inline pull-right'>" +
                                                                        "<li>" +
                                                                            "<button type='button' class='btn verde next-step loading-btn-generarBDEMP' data-loading-text='Ejecución en proceso...' onclick='generarBDEmpresa();' id='btnGenerarBDEMP'>GENERAR</button>" +
                                                                        "</li>" +
                                                                        "<li>" +
                                                                            "<button type='button' class='btn verde prev-step hide' id=''>ATRÁS</button>" +
                                                                        "</li>" +
                                                                    "</ul>" +
                                                                "</div>" +
                                                                "<div class='tab-pane' role='tabpanel' id='step2'>" +
                                                                    "<h3>Información:</h3>" +
                                                                    "<h4>Cambiar el esquema de la base de datos con el nombre del grupo.</h4></br>" +
                                                                    "<h3 class='rEsquemasBDEmpresas hide'>Resultados de Ejecución:</h3>" +
                                                                    "<h4 id='rEsquemasBDEmpresas'></h4>" +
                                                                    "<ul class='list-inline pull-right'>" +
                                                                        "<li>" +
                                                                            "<button type='button' class='btn verde next-step loading-btn-esquemasBDEMP' data-loading-text='Ejecución en proceso...' onclick='cambiarEsquemasBDEmpresa()' id='btnCambiarEsquemaBDEMP'>GENERAR</button>" +
                                                                        "</li>" +
                                                                    "</ul>" +
                                                                "</div>" +
                                                                "<div class='tab-pane' role='tabpanel' id='complete'>" +
                                                                    "<h3>Información:</h3>" +
                                                                    "<h4>Limpiar la base de datos para su nueva configuración</h4>" +
                                                                    "<h3 class='rLimpiarBDEMP hide'>Resultados de Ejecución:</h3>" +
                                                                    "<h4 id='rLimpiarBDEMP'></h4>" +
                                                                    "<ul class='list-inline pull-right'>" +
                                                                        "<li>" +
                                                                            "<button type='button' class='btn verde next-step loading-btn-limpiarBDEMP' data-loading-text='Ejecución en proceso...' onclick='limpiarBDEmpresa();' id='btnLimpiarBDEMP'>GENERAR</button>" +
                                                                        "</li>" +
                                                                    "</ul>" +
                                                                "</div>" +
                                                                "<div class='tab-pane' role='tabpanel' id='step3'>" +
                                                                    "<h3>Información:</h3>" +
                                                                    "<h4>Iniciar la configuración inicial de base de datos de Empresa</h4>" +
                                                                    "<h3 class='rConfInicial hide'>Resultados de Ejecución:</h3>" +
                                                                    "<h4 id='rConfInicial'></h4>" +
                                                                    "<ul class='list-inline pull-right'>" +
                                                                        "<li>" +
                                                                            "<button type='button' class='btn verde next-step loading-btn-confIniBDEMP' data-loading-text='Ejecución en proceso...' onclick='configuracionInicial();' id='btnConfIniBDEMP'>GENERAR</button>" +
                                                                        "</li>" +
                                                                    "</ul>" +
                                                                "</div>" +
                                                                "<div class='clearfix'></div>" +
                                                            "</div>" +
                                                        "</form>" +
                                                    "</div>" +
                                                "</section>" +
                                            "</div>");
                    }
                    $('#trakingDBEMP').show();
                } else if (dato.existe == null && conocerSistemas > 1) {//Si no tiene BDEMP y no tienen ningun sistema generado
                    //alert('3');
                    $('.btnVerTraking ').removeClass('hide');
                    $.each(data.d, function (i, dato) {
                        contador++;
                        if (contador == 1) {//If contador
                            //console.log('2: ' + contador);
                            // alert(dato.existe + ', ' + dato.idReporte + ', ' + dato.idERPGrupo + ', ' + dato.nomGrupo + ', ' + dato.idAccion + ', ' + dato.idEstatus);
                            $("#divTrakingEmp").append("<div class='row traking' id='trakingDBEMP'>" +
                                                    "<section>" +
                                                        "<div class='wizard'>" +
                                                            "<div class='wizard-inner'>" +
                                                                "<div class='connecting-line'></div>" +
                                                                "<ul class='nav nav-tabs' role='tablist'>" +
                                                                    "<div class='form-tit round-border3'>" +
                                                                        "<div class='clear width98 center'>" +
                                                                            "<div class='left width50'>" +
                                                                                "<h3 class='left'>Generar Base de Datos de Empresa</h3>" +
                                                                            "</div>" +
                                                                            "<div class='left width50'>" +
                                                                                "<span class='glyphicon glyphicon-refresh glyphicon-refresh-animate icon-update hide iconUpdate' style='color: green;' title='Ejecutando...'></span>" +
                                                                            "</div>	" +
                                                                        "</div>" +
                                                                    "</div>" +
                                                                    "<li role='presentation' class='active presentation1 emp'><a href='#step1' data-toggle='tab' aria-controls='step1' class='title-change1' role='tab' title='Crear Base de Datos y Objetos'><span class='round-tab estatus1'><i class='glyphicon glyphicon-hdd' id='intermitente1'></i></span></a></li>" +
                                                                    "<li role='presentation' class='disabled presentation2 emp'><a href='#step2' data-toggle='tab' aria-controls='step2' class='title-change2' role='tab' title='Cambiar Esquemas'><span class='round-tab estatus2'><i class='glyphicon glyphicon-wrench' id='intermitente2'></i></span></a></li>" +
                                                                    "<li role='presentation' class='disabled presentation3 emp'><a href='#complete' data-toggle='tab' aria-controls='complete' class='title-change3' role='tab' title='Limpiar Base de Datos'><span class='round-tab estatus3'><i class='glyphicon glyphicon-trash' id='intermitente3'></i></span></a></li>" +
                                                                    "<li role='presentation' class='disabled presentation4 emp'><a href='#step3' data-toggle='tab' aria-controls='complete' class='title-change4' role='tab' title='Configuración Inicial'><span class='round-tab estatus4'><i class='glyphicon glyphicon-tasks' id='intermitente4'></i></span></a></li>" +
                                                                "</ul>" +
                                                            "</div>" +
                                                            "<form role='form'>" +
                                                                "<div class='tab-content'>" +
                                                                    "<div class='tab-pane active' role='tabpanel' id='step1'>" +
                                                                        "<h3>Información:</h3>" +
                                                                        "<h4>Generar la Base y Objetos SQl para la Base de Datos de Empresa</h4></br>" +
                                                                        "<h3 class='rGenerarBDEmpresas hide'>Resultados de Ejecución:</h3>" +
                                                                        "<h4 id='rGenerarBDEmpresas'></h4>" +
                                                                        "<ul class='list-inline pull-right'>" +
                                                                            "<li>" +
                                                                                "<button type='button' class='btn verde next-step loading-btn-generarBDEMP' data-loading-text='Ejecución en proceso...' onclick='generarBDEmpresa();' id='btnGenerarBDEMP'>GENERAR</button>" +
                                                                            "</li>" +
                                                                            "<li>" +
                                                                                "<button type='button' class='btn verde prev-step hide' id=''>ATRÁS</button>" +
                                                                            "</li>" +
                                                                        "</ul>" +
                                                                    "</div>" +
                                                                    "<div class='tab-pane' role='tabpanel' id='step2'>" +
                                                                        "<h3>Información:</h3>" +
                                                                        "<h4>Cambiar el esquema de la base de datos con el nombre del grupo.</h4></br>" +
                                                                        "<h3 class='rEsquemasBDEmpresas hide'>Resultados de Ejecución:</h3>" +
                                                                        "<h4 id='rEsquemasBDEmpresas'></h4>" +
                                                                        "<ul class='list-inline pull-right'>" +
                                                                            "<li>" +
                                                                                "<button type='button' class='btn verde next-step loading-btn-esquemasBDEMP' data-loading-text='Ejecución en proceso...' onclick='cambiarEsquemasBDEmpresa()' id='btnCambiarEsquemaBDEMP'>GENERAR</button>" +
                                                                            "</li>" +
                                                                        "</ul>" +
                                                                    "</div>" +
                                                                    "<div class='tab-pane' role='tabpanel' id='complete'>" +
                                                                        "<h3>Información:</h3>" +
                                                                        "<h4>Limpiar la base de datos para su nueva configuración</h4>" +
                                                                        "<h3 class='rLimpiarBDEMP hide'>Resultados de Ejecución:</h3>" +
                                                                        "<h4 id='rLimpiarBDEMP'></h4>" +
                                                                        "<ul class='list-inline pull-right'>" +
                                                                            "<li>" +
                                                                                "<button type='button' class='btn verde next-step loading-btn-limpiarBDEMP' data-loading-text='Ejecución en proceso...' onclick='limpiarBDEmpresa();' id='btnLimpiarBDEMP'>GENERAR</button>" +
                                                                            "</li>" +
                                                                        "</ul>" +
                                                                    "</div>" +
                                                                    "<div class='tab-pane' role='tabpanel' id='step3'>" +
                                                                        "<h3>Información:</h3>" +
                                                                        "<h4>Iniciar la configuración inicial de base de datos de Empresa</h4>" +
                                                                        "<h3 class='rConfInicial hide'>Resultados de Ejecución:</h3>" +
                                                                        "<h4 id='rConfInicial'></h4>" +
                                                                        "<ul class='list-inline pull-right'>" +
                                                                            "<li>" +
                                                                                "<button type='button' class='btn verde next-step loading-btn-confIniBDEMP' data-loading-text='Ejecución en proceso...' onclick='configuracionInicial();' id='btnConfIniBDEMP'>GENERAR</button>" +
                                                                            "</li>" +
                                                                        "</ul>" +
                                                                    "</div>" +
                                                                    "<div class='clearfix'></div>" +
                                                                "</div>" +
                                                            "</form>" +
                                                        "</div>" +
                                                    "</section>" +
                                                "</div>");
                        }//Fin if comntador
                    });


                
                } else if (dato.existe !== null && conocerSistemas < 2 && dato.idEstatus == 5) {
                    $('.btnVerTraking ').removeClass('hide');
                    //alert('4');
                } else if (dato.existe != null && conocerSistemas > 1 && dato.idEstatus != 4) {
                    //console.log(conocerSistemas);
                    //$('.btnVerTraking ').removeClass('hide');
                    //alert('5');
                    //alert(dato.existe + ', ' + dato.idReporte + ', ' + dato.idERPGrupo + ', ' + dato.nomGrupo + ', ' + dato.idAccion + ', ' + dato.idEstatus);
                    if (contador == 2) {
                    // console.log('3: ' + contador);
                    $("#divTrakingEmp").append("<div class='row traking' id='trakingDBEMP'>" +
                                            "<section>" +
                                                "<div class='wizard'>" +
                                                    "<div class='wizard-inner'>" +
                                                        "<div class='connecting-line'></div>" +
                                                        "<ul class='nav nav-tabs' role='tablist'>" +
                                                            "<div class='form-tit round-border3'>" +
                                                                "<div class='clear width98 center'>" +
                                                                    "<div class='left width50'>" +
                                                                        "<h3 class='left'>Generar Base de Datos de Empresa</h3>" +
                                                                    "</div>" +
                                                                    "<div class='left width50'>" +
                                                                        "<span class='glyphicon glyphicon-refresh glyphicon-refresh-animate icon-update hide iconUpdate' style='color: green;' title='Ejecutando...'></span>" +
                                                                    "</div>	" +
                                                                "</div>" +
                                                            "</div>" +
                                                            "<li role='presentation' class='active presentation1 emp'><a href='#step1' data-toggle='tab' aria-controls='step1' class='title-change1' role='tab' title='Crear Base de Datos y Objetos'><span class='round-tab estatus1'><i class='glyphicon glyphicon-hdd' id='intermitente1'></i></span></a></li>" +
                                                            "<li role='presentation' class='disabled presentation2 emp'><a href='#step2' data-toggle='tab' aria-controls='step2' class='title-change2' role='tab' title='Cambiar Esquemas'><span class='round-tab estatus2'><i class='glyphicon glyphicon-wrench' id='intermitente2'></i></span></a></li>" +
                                                            "<li role='presentation' class='disabled presentation3 emp'><a href='#complete' data-toggle='tab' aria-controls='complete' class='title-change3' role='tab' title='Limpiar Base de Datos'><span class='round-tab estatus3'><i class='glyphicon glyphicon-trash' id='intermitente3'></i></span></a></li>" +
                                                            "<li role='presentation' class='disabled presentation4 emp'><a href='#step3' data-toggle='tab' aria-controls='complete' class='title-change4' role='tab' title='Configuración Inicial'><span class='round-tab estatus4'><i class='glyphicon glyphicon-tasks' id='intermitente4'></i></span></a></li>" +
                                                        "</ul>" +
                                                    "</div>" +
                                                    "<form role='form'>" +
                                                        "<div class='tab-content'>" +
                                                            "<div class='tab-pane active' role='tabpanel' id='step1'>" +
                                                                "<h3>Información:</h3>" +
                                                                "<h4>Generar la Base y Objetos SQl para la Base de Datos de Empresa</h4></br>" +
                                                                "<h3 class='rGenerarBDEmpresas hide'>Resultados de Ejecución:</h3>" +
                                                                "<h4 id='rGenerarBDEmpresas'></h4>" +
                                                                "<ul class='list-inline pull-right'>" +
                                                                    "<li>" +
                                                                        "<button type='button' class='btn verde next-step loading-btn-generarBDEMP' data-loading-text='Ejecución en proceso...' onclick='generarBDEmpresa();' id='btnGenerarBDEMP'>GENERAR</button>" +
                                                                    "</li>" +
                                                                    "<li>" +
                                                                        "<button type='button' class='btn verde prev-step hide' id=''>ATRÁS</button>" +
                                                                    "</li>" +
                                                                "</ul>" +
                                                            "</div>" +
                                                            "<div class='tab-pane' role='tabpanel' id='step2'>" +
                                                                "<h3>Información:</h3>" +
                                                                "<h4>Cambiar el esquema de la base de datos con el nombre del grupo.</h4></br>" +
                                                                "<h3 class='rEsquemasBDEmpresas hide'>Resultados de Ejecución:</h3>" +
                                                                "<h4 id='rEsquemasBDEmpresas'></h4>" +
                                                                "<ul class='list-inline pull-right'>" +
                                                                    "<li>" +
                                                                        "<button type='button' class='btn verde next-step loading-btn-esquemasBDEMP' data-loading-text='Ejecución en proceso...' onclick='cambiarEsquemasBDEmpresa()' id='btnCambiarEsquemaBDEMP'>GENERAR</button>" +
                                                                    "</li>" +
                                                                "</ul>" +
                                                            "</div>" +
                                                            "<div class='tab-pane' role='tabpanel' id='complete'>" +
                                                                "<h3>Información:</h3>" +
                                                                "<h4>Limpiar la base de datos para su nueva configuración</h4>" +
                                                                "<h3 class='rLimpiarBDEMP hide'>Resultados de Ejecución:</h3>" +
                                                                "<h4 id='rLimpiarBDEMP'></h4>" +
                                                                "<ul class='list-inline pull-right'>" +
                                                                    "<li>" +
                                                                        "<button type='button' class='btn verde next-step loading-btn-limpiarBDEMP' data-loading-text='Ejecución en proceso...' onclick='limpiarBDEmpresa();' id='btnLimpiarBDEMP'>GENERAR</button>" +
                                                                    "</li>" +
                                                                "</ul>" +
                                                            "</div>" +
                                                            "<div class='tab-pane' role='tabpanel' id='step3'>" +
                                                                "<h3>Información:</h3>" +
                                                                "<h4>Iniciar la configuración inicial de base de datos de Empresa</h4>" +
                                                                "<h3 class='rConfInicial hide'>Resultados de Ejecución:</h3>" +
                                                                "<h4 id='rConfInicial'></h4>" +
                                                                "<ul class='list-inline pull-right'>" +
                                                                    "<li>" +
                                                                        "<button type='button' class='btn verde next-step loading-btn-confIniBDEMP' data-loading-text='Ejecución en proceso...' onclick='configuracionInicial();' id='btnConfIniBDEMP'>GENERAR</button>" +
                                                                    "</li>" +
                                                                "</ul>" +
                                                            "</div>" +
                                                            "<div class='clearfix'></div>" +
                                                        "</div>" +
                                                    "</form>" +
                                                "</div>" +
                                            "</section>" +
                                        "</div>");   
                    }     
                }
            
            });////////fin primer each
            $.each(data.d, function (i, dato) {
                ///OBTENER LOS ESTATUS
                //La primer acción esta o no terminada (generar base de datos)
                if (dato.idEstatus == 2 && dato.idAccion == 1) {
                    $('.estatus1').css('border', '2px solid #30bf54');
                    $('.presentation2').removeClass('disabled');//Habilita la tarea que sigue
                    $('.presentation1').removeClass('active');//Quita el active a la primera
                    $('.presentation2').addClass('active');//Agregale active a la segunda
                    $('#step1').removeClass('active');//Quita el active a la primera
                    $('#step2').addClass('active');//Agregale active a la segunda
                    $('#btnGenerarBDEMP').addClass('hide');
                    $('#intermitente1').removeClass('parpadea');

                } else if (dato.idEstatus == 3 && dato.idAccion == 1) {
                    $('.estatus1').css('border', '2px solid #bd0400');
                    $('#intermitente1').removeClass('parpadea');
                } else if (dato.idEstatus == 1 && dato.idAccion == 1) {
                    $('.estatus1').css('border', '2px solid #b7b224');
                    $('.title-change1').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera
                    $('#intermitente1').addClass('parpadea');
                }
                //La segunda acción esta o no terminada (cambiar esquemas)
                if (dato.idEstatus == 2 && dato.idAccion == 2) {
                    $('.estatus2').css('border', '2px solid #30bf54');
                    $('.presentation3').removeClass('disabled');//Habilita la tarea que sigue
                    $('.presentation2').removeClass('active');//Quita active a la segunda
                    $('.presentation3').addClass('active');//Agregale active a la tercera
                    $('#step2').removeClass('active');//Quita el active a la primera
                    $('#complete').addClass('active');//Agregale active a la segunda
                    $('#btnCambiarEsquemaBDEMP').addClass('hide');
                    $('#intermitente2').removeClass('parpadea');
                } else if (dato.idEstatus == 3 && dato.idAccion == 2) {
                    $('.estatus2').css('border', '2px solid #bd0400');
                    $('#intermitente2').removeClass('parpadea');
                } else if (dato.idEstatus == 1 && dato.idAccion == 2) {
                    $('.estatus2').css('border', '2px solid #b7b224');
                    $('.title-change2').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Cambiar Esquemas');//Quita el active a la primera
                    $('#intermitente2').addClass('parpadea');
                }
                //La tercera acción esta o no terminada (limpiar base de datos)
                if (dato.idEstatus == 2 && dato.idAccion == 3) {
                    $('.estatus3').css('border', '2px solid #30bf54');
                    $('#btnLimpiarBDEMP').addClass('hide');//Ocultar el botón paara limpia BDEMP
                    //$('.btnVerTraking ').removeClass('hide');//Mostrar los botones para generar los demás tracking
                    $('.presentation4').removeClass('disabled');//Habilita la tarea que sigue
                    $('.presentation3').removeClass('active');//Quita active a la segunda
                    $('.presentation4').addClass('active');//Agregale active a la tercera
                    $('#complete').removeClass('active');//Quita el active a la primera
                    $('#step3').addClass('active');//Agregale active a la segunda
                    $('#intermitente3').removeClass('parpadea');
                } else if (dato.idEstatus == 3 && dato.idAccion == 3) {
                    $('.estatus3').css('border', '2px solid #bd0400');
                    $('#intermitente3').removeClass('parpadea');
                } else if (dato.idEstatus == 1 && dato.idAccion == 3) {
                    $('.estatus3').css('border', '2px solid #b7b224');
                    $('.title-change3').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera
                    $('.btnVerTraking ').removeClass('hide');
                    $('#intermitente3').addClass('parpadea');
                    $('.loading-btn-limpiarBDEMP').button('loading');//Mostrar moton con animacion de ejecución
                }
                //La cuarta acción esta o no terminada (configuración inicial)
                if (dato.idEstatus == 2 && dato.idAccion == 6) {
                    $('#divTrakingEmp').hide(); //Ocultar el tracking de empresa si ya esta terminada correctamente
                    $('.estatus4').css('border', '2px solid #30bf54');
                    $('#btnConfIniBDEMP').addClass('hide');//Ocultar el botón paara limpia BDEMP
                    $('#intermitente4').removeClass('parpadea');
                    $('.btnVerTraking ').removeClass('hide');//Mostrar los botones para generar los demás tracking
                } else if (dato.idEstatus == 3 && dato.idAccion == 6) {
                    $('.estatus4').css('border', '2px solid #bd0400');
                    $('#intermitente4').removeClass('parpadea');
                } else if (dato.idEstatus == 1 && dato.idAccion == 6) {
                    $('.estatus4').css('border', '2px solid #b7b224');
                    $('.title-change4').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Configuración Inicial');//Quita el active a la primera
                    $('.btnVerTraking ').removeClass('hide');
                    $('#intermitente4').addClass('parpadea');
                    $('.loading-btn-confIniBDEMP').button('loading');//Mostrar moton con animacion de ejecución
                }
            });
            if (data.d == '') {
                //alert('4');
                $('.btnVerTraking ').removeClass('hide');
            }else{
 
            }
            /*EVENTOS PARA QUE FUNCIONE EL TRAKING*/
            //Initialize tooltips
            $('.nav-tabs > li a[title]').tooltip();
            //Wizard
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                var $target = $(e.target);
                if ($target.parent().hasClass('disabled')) {
                    return false;
                }
            });
            //$(".next-step").click(function (e) {
            //    var $active = $('.wizard .nav-tabs li.active.emp');
            //    $active.next().removeClass('disabled');
            //    nextTab($active);
            //});
            $(".prev-step").click(function (e) {
                var $active = $('.wizard .nav-tabs li.active.emp');
                prevTab($active);
            });
            /*FIN EVENTOS PARA QUE FUNCIONE EL TRAKING*/
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en existeEstatusDBEMP.");
            unBlock();
        }
    });
}
//Saber si existe la base de datos y el estatus en el que se quedo la generación de la nueva BD SGC
function existeEstatusDBSGC(idReporte,idSistema) {
    block();
    $.ajax({
        url: "Sistemas.aspx/existeEstatusDBSGC",
        type: "POST",
        data: "{idReporte:" + idReporte + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == '') {
               
            } else {
                $.each(data.d, function (i, dato) {
                    //alert('SGC: '+dato.existe + ', ' + dato.idReporte + ', ' + dato.idERPGrupo + ', ' + dato.nomGrupo + ', ' + dato.idAccion + ', ' + dato.idEstatus);                
                    //La primer acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 1) {
                        $('.estatus1'+idSistema+'').css('border', '2px solid #30bf54');
                        $('.presentation2'+idSistema+'').removeClass('disabled');//Habilita la tarea que sigue
                        $('.presentation1'+idSistema+'').removeClass('active');//Quita el active a la primera
                        $('.presentation2'+idSistema+'').addClass('active');//Agregale active a la segunda
                        $('#step1'+idSistema+'').removeClass('active');//Quita el active a la primera
                        $('#step2'+idSistema+'').addClass('active');//Agregale active a la segunda
                        $('#btnGenerarBDSGC' + idSistema + '').addClass('hide');
                        $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! Crear Base de Datos y Objetos');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 1) {
                        $('.estatus1' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Crear Base de Datos y Objetos');//Quita el active a la primera
                    } else if (dato.idEstatus == 1 && dato.idAccion == 1) {
                        $('.estatus1' + idSistema + '').css('border', '2px solid #b7b224');
                        $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Crear Base de Datos y Objeto');//Quita el active a la primera
                        $('#intermitente1' + idSistema + '').addClass('parpadea');
                    }
                    //La segunda acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 2) {
                        $('.estatus2' + idSistema + '').css('border', '2px solid #30bf54');
                        $('.presentation3' + idSistema + '').removeClass('disabled');//Habilita la tarea que sigue
                        $('.presentation2' + idSistema + '').removeClass('active');//Quita active a la segunda
                        $('.presentation3' + idSistema + '').addClass('active');//Agregale active a la tercera
                        $('#step2' + idSistema + '').removeClass('active');//Quita el active a la primera
                        $('#step3' + idSistema + '').addClass('active');//Agregale active a la segunda
                        $('#btnCambiarEsquemasBDSGC' + idSistema + '').addClass('hide');
                        $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! Cambiar Esquemas');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 2) {
                        $('.estatus2' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Cambiar Esquemas');//Quita el active a la primera
                    } else if (dato.idEstatus == 1 && dato.idAccion == 2) {
                        $('.estatus2' + idSistema + '').css('border', '2px solid #b7b224');
                        $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Cambiar Esquemas');//Quita el active a la primera
                        $('#intermitente2' + idSistema + '').addClass('parpadea');
                    }
                    //La tercera acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #30bf54');                        
                        $('#btnLimpiarBDSGC' + idSistema + '').addClass('hide');//Ocultar el botón paara limpia BDEMP
                        $('.btnVerTraking').removeClass('hide');//Mostrar los botones para generar los demás tracking
                        $('#divTrakingEmp').hide(); //Ocultar el tracking de empresa si ya esta terminada correctamente   
                        $('.presentation3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('.presentation4' + idSistema + '').removeClass('disabled');//Agregale active a la cuarta
                        $('.presentation4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('#step3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('#step4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! Limpiar Base de Datos');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Limpiar Base de Datos');//Quita el active a la primera
                    }else if (dato.idEstatus == 1 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #b7b224');
                        $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera
                        $('.loading-btn-botonLimpiarBD' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
                        $('#intermitente3' + idSistema + '').addClass('parpadea');
                    }
                    //La cuarta acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 4) {
                        $('.estatus4' + idSistema + '').css('border', '2px solid #30bf54');
                        $('.btnUrl' + idSistema + '').addClass('hide');//Ocultar el botón paara limpia BDEMP
                        $('#lblURL' + idSistema + '').addClass('hide');//Ocultar lbl url
                        $('#txtURL' + idSistema + '').addClass('hide');//Ocultar txt url
                       
                        $('.presentation3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('.presentation4' + idSistema + '').removeClass('disabled');//Agregale active a la cuarta
                        $('.presentation4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('#step3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('#step4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('.title-change4' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! URL guardada');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change4' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! No se guardo la URL');//Quita el active a la primera
                    }
                    unBlock();

                });
            }
            /*EVENTOS PARA QUE FUNCIONE EL TRAKING*/
            //Initialize tooltips
            $('.nav-tabs > li a[title]').tooltip();
            //Wizard
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                var $target = $(e.target);
                if ($target.parent().hasClass('disabled')) {
                    return false;
                }
            });
            /*FIN EVENTOS PARA QUE FUNCIONE EL TRAKING*/
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en existeEstatusDBSGC.");
            unBlock();
        }
    });
}
//Saber si existe la base de datos y el estatus en el que se quedo la generación de la nueva BD SGRO
function existeEstatusDBSGRO(idReporte, idSistema) {
    block();
    $.ajax({
        url: "Sistemas.aspx/existeEstatusDBSGRO",
        type: "POST",
        data: "{idReporte:" + idReporte + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == '') {

            } else {
                $.each(data.d, function (i, dato) {
                    //alert('SGC: '+dato.existe + ', ' + dato.idReporte + ', ' + dato.idERPGrupo + ', ' + dato.nomGrupo + ', ' + dato.idAccion + ', ' + dato.idEstatus);                
                    //La primer acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 1) {
                        $('.estatus1' + idSistema + '').css('border', '2px solid #30bf54');
                        $('.presentation2' + idSistema + '').removeClass('disabled');//Habilita la tarea que sigue
                        $('.presentation1' + idSistema + '').removeClass('active');//Quita el active a la primera
                        $('.presentation2' + idSistema + '').addClass('active');//Agregale active a la segunda
                        $('#step1' + idSistema + '').removeClass('active');//Quita el active a la primera
                        $('#step2' + idSistema + '').addClass('active');//Agregale active a la segunda
                        $('#btnGenerarBDSGRO' + idSistema + '').addClass('hide');
                        $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! Crear Base de Datos y Objetos');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 1) {
                        $('.estatus1' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Crear Base de Datos y Objetos');//Quita el active a la primera
                    } else if (dato.idEstatus == 1 && dato.idAccion == 1) {
                        $('.estatus1' + idSistema + '').css('border', '2px solid #b7b224');
                        $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Crear Base de Datos y Objetos');//Quita el active a la primera
                        $('#intermitente1' + idSistema + '').addClass('parpadea');
                    }
                    //La segunda acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 2) {
                        $('.estatus2' + idSistema + '').css('border', '2px solid #30bf54');
                        $('.presentation3' + idSistema + '').removeClass('disabled');//Habilita la tarea que sigue
                        $('.presentation2' + idSistema + '').removeClass('active');//Quita active a la segunda
                        $('.presentation3' + idSistema + '').addClass('active');//Agregale active a la tercera
                        $('#step2' + idSistema + '').removeClass('active');//Quita el active a la primera
                        $('#step3' + idSistema + '').addClass('active');//Agregale active a la segunda
                        $('#btnCambiarEsquemasBDSGRO' + idSistema + '').addClass('hide');
                        $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! Cambiar Esquemas');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 2) {
                        $('.estatus2' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Cambiar Esquemas');//Quita el active a la primera
                    } else if (dato.idEstatus == 1 && dato.idAccion == 2) {
                        $('.estatus2' + idSistema + '').css('border', '2px solid #b7b224');
                        $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Cambiar Esquemas');//Quita el active a la primera
                        $('#intermitente2' + idSistema + '').addClass('parpadea');
                    }
                    //La tercera acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #30bf54');
                        $('#btnLimpiarBDSGRO' + idSistema + '').addClass('hide');//Ocultar el botón paara limpia BDEMP
                        $('.btnVerTraking').removeClass('hide');//Mostrar los botones para generar los demás tracking
                        $('#divTrakingEmp').hide(); //Ocultar el tracking de empresa si ya esta terminada correctamente   
                        $('.presentation3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('.presentation4' + idSistema + '').removeClass('disabled');//Agregale active a la cuarta
                        $('.presentation4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('#step3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('#step4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! Limpiar Base de Datos');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Limpiar Base de Datos');//Quita el active a la primera
                    } else if (dato.idEstatus == 1 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #b7b224');
                        $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera
                        $('#intermitente3' + idSistema + '').addClass('parpadea');
                        $('.loading-btn-botonLimpiarBD' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
                    }

                    //La cuarta acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 4) {
                        $('.estatus4' + idSistema + '').css('border', '2px solid #30bf54');
                        $('.btnUrl' + idSistema + '').addClass('hide');//Ocultar el botón paara limpia BDEMP
                        $('#lblURL' + idSistema + '').addClass('hide');//Ocultar lbl url
                        $('#txtURL' + idSistema + '').addClass('hide');//Ocultar txt url
                        $('.presentation3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('.presentation4' + idSistema + '').removeClass('disabled');//Agregale active a la cuarta
                        $('.presentation4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('#step3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('#step4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('.title-change4' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! URL guardada');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change4' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! No se guardo la URL');//Quita el active a la primera
                    }
                    unBlock();

                });
            }
            /*EVENTOS PARA QUE FUNCIONE EL TRAKING*/
            //Initialize tooltips
            $('.nav-tabs > li a[title]').tooltip();
            //Wizard
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                var $target = $(e.target);
                if ($target.parent().hasClass('disabled')) {
                    return false;
                }
            });
            /*FIN EVENTOS PARA QUE FUNCIONE EL TRAKING*/
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en existeEstatusDBSGRO.");
            unBlock();
        }
    });
}
//Saber si existe la base de datos y el estatus en el que se quedo la generación de la nueva BD SGCE
function existeEstatusDBSGCE(idReporte, idSistema) {
    block();
    $.ajax({
        url: "Sistemas.aspx/existeEstatusDBSGCE",
        type: "POST",
        data: "{idReporte:" + idReporte + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == '') {

            } else {
                $.each(data.d, function (i, dato) {
                    //alert('SGC: '+dato.existe + ', ' + dato.idReporte + ', ' + dato.idERPGrupo + ', ' + dato.nomGrupo + ', ' + dato.idAccion + ', ' + dato.idEstatus);                
                    //La primer acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 1) {
                        $('.estatus1' + idSistema + '').css('border', '2px solid #30bf54');
                        $('.presentation2' + idSistema + '').removeClass('disabled');//Habilita la tarea que sigue
                        $('.presentation1' + idSistema + '').removeClass('active');//Quita el active a la primera
                        $('.presentation2' + idSistema + '').addClass('active');//Agregale active a la segunda
                        $('#step1' + idSistema + '').removeClass('active');//Quita el active a la primera
                        $('#step2' + idSistema + '').addClass('active');//Agregale active a la segunda
                        $('#btnGenerarBDSGRO' + idSistema + '').addClass('hide');
                        $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! Crear Base de Datos y Objetos');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 1) {
                        $('.estatus1' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Crear Base de Datos y Objetos');//Quita el active a la primera
                    } else if (dato.idEstatus == 1 && dato.idAccion == 1) {
                        $('.estatus1' + idSistema + '').css('border', '2px solid #b7b224');
                        $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Crear Base de Datos y Objetos');//Quita el active a la primera
                        $('#intermitente1' + idSistema + '').addClass('parpadea');
                    }
                    //La segunda acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 2) {
                        $('.estatus2' + idSistema + '').css('border', '2px solid #30bf54');
                        $('.presentation3' + idSistema + '').removeClass('disabled');//Habilita la tarea que sigue
                        $('.presentation2' + idSistema + '').removeClass('active');//Quita active a la segunda
                        $('.presentation3' + idSistema + '').addClass('active');//Agregale active a la tercera
                        $('#step2' + idSistema + '').removeClass('active');//Quita el active a la primera
                        $('#step3' + idSistema + '').addClass('active');//Agregale active a la segunda
                        $('#btnCambiarEsquemasBDSGRO' + idSistema + '').addClass('hide');
                        $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! Cambiar Esquemas');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 2) {
                        $('.estatus2' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Cambiar Esquemas');//Quita el active a la primera
                    } else if (dato.idEstatus == 1 && dato.idAccion == 2) {
                        $('.estatus2' + idSistema + '').css('border', '2px solid #b7b224');
                        $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Cambiar Esquemas');//Quita el active a la primera
                        $('#intermitente2' + idSistema + '').addClass('parpadea');
                    }
                    //La tercera acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #30bf54');
                        $('#btnLimpiarBDSGRO' + idSistema + '').addClass('hide');//Ocultar el botón paara limpia BDEMP
                        $('.btnVerTraking').removeClass('hide');//Mostrar los botones para generar los demás tracking
                        $('#divTrakingEmp').hide(); //Ocultar el tracking de empresa si ya esta terminada correctamente   
                        $('.presentation3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('.presentation4' + idSistema + '').removeClass('disabled');//Agregale active a la cuarta
                        $('.presentation4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('#step3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('#step4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! Limpiar Base de Datos');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Limpiar Base de Datos');//Quita el active a la primera
                    } else if (dato.idEstatus == 1 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #b7b224');
                        $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera
                        $('#intermitente3' + idSistema + '').addClass('parpadea');
                        $('.loading-btn-botonLimpiarBD' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
                    }

                    //La cuarta acción esta o no terminada
                    if (dato.idEstatus == 2 && dato.idAccion == 4) {
                        $('.estatus4' + idSistema + '').css('border', '2px solid #30bf54');
                        $('.btnUrl' + idSistema + '').addClass('hide');//Ocultar el botón paara limpia BDEMP
                        $('#lblURL' + idSistema + '').addClass('hide');//Ocultar lbl url
                        $('#txtURL' + idSistema + '').addClass('hide');//Ocultar txt url
                        $('.presentation3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('.presentation4' + idSistema + '').removeClass('disabled');//Agregale active a la cuarta
                        $('.presentation4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('#step3' + idSistema + '').removeClass('active');//Quita el active a la tercera
                        $('#step4' + idSistema + '').addClass('active');//Agregale active a la cuarta
                        $('.title-change4' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea Terminada! URL guardada');//Quita el active a la primera
                    } else if (dato.idEstatus == 3 && dato.idAccion == 3) {
                        $('.estatus3' + idSistema + '').css('border', '2px solid #bd0400');
                        $('.title-change4' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! No se guardo la URL');//Quita el active a la primera
                    }
                    unBlock();

                });
            }
            /*EVENTOS PARA QUE FUNCIONE EL TRAKING*/
            //Initialize tooltips
            $('.nav-tabs > li a[title]').tooltip();
            //Wizard
            $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {
                var $target = $(e.target);
                if ($target.parent().hasClass('disabled')) {
                    return false;
                }
            });
            /*FIN EVENTOS PARA QUE FUNCIONE EL TRAKING*/
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en existeEstatusDBSGRO.");
            unBlock();
        }
    });
}
//Generar .BAK de Empresa DANA
function generarBDEmpresa() {
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    var idSistema = 0;
    $('.title-change1').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Generar Base de datos');//Quita el active a la primera
    $('.estatus1').css('border', '2px solid #b7b224');
    //Boton efecto loading
    $('.loading-btn-generarBDEMP').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate').removeClass('hide');//Mostrar icno de ejecutandose        
    $('#intermitente1').addClass('parpadea');
    //block();
    $.ajax({
        url: "Sistemas.aspx/generarBDEmpresa",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.rGenerarBDEmpresas').removeClass('hide');
                $('#rGenerarBDEmpresas').append(data.d.substring(0, mensaje - 1));
                $('.estatus1').css('border', '2px solid #30bf54');
                $('#btnGenerarBDEMP').hide();
                //Activar el sigiente paso
                var $active = $('.wizard .nav-tabs li.active.emp');
                $active.next().removeClass('disabled');
                nextTab($active);
                //Fin cambiar al segundo paso                
                $('.loading-btn-generarBDEMP').button('loading');//Mostrar moton con animacion de ejecución     
                $('.iconUpdate').addClass('hide');//Mostrar icno de ejecutandose
                $('.estatus1').css('border', '2px solid #30bf54');
                $('.title-change1').removeAttr('data-original-title').attr('data-original-title', '¡Tarea terminada! Generar Base de Datos');//Quita el active a la primera               
                $('#intermitente1').removeClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.rGenerarBDEmpresas').removeClass('hide');
                $('#rGenerarBDEmpresas').append('Error al generar la base de datos de Empresa');
                $('.estatus1').css('border', '2px solid #bd0400');
                $('.prev-step').click();
                $('.iconUpdate').addClass('hide');//Mostrar icno de ejecutandose
                $('.title-change1').removeAttr('data-original-title').attr('data-original-title', '¡Error! Generar Base de Datos');//Quita el active a la primera
                $('.loading-btn-botonGenerarBD').button('reset');
                $('#intermitente1').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en generarBDEmpresa.");
            $('.loading-btn-botonGenerarBD').button('reset');
            $('#intermitente1').removeClass('parpadea');
            unBlock();
        }
    });
}
//Generar .BAK del Sistema SGC
function generarBDSGC(idSistema) {    
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-botonGenerarBD' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate' + idSistema + '').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change1'+idSistema+'').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Generar Base de Datos');//Quita el active a la primera
    $('.estatus1' + idSistema + '').css('border', '2px solid #b7b224');
    $('#intermitente1' + idSistema + '').addClass('parpadea');
    //block();
    $.ajax({
        url: "Sistemas.aspx/generarBDSGC",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea terminada! Generar Base de Datos');//Quita el active a la primera
                $('.estatus1'+idSistema+'').css('border', '2px solid #30bf54');
                $('#btnGenerarBD' + idSistema + '').hide();
                //Cambiar al segundo paso
                var $active = $('.wizard .nav-tabs li.presentation1' + idSistema + '');
                $active.next().removeClass('disabled');
                nextTab($active);
                //Fin cambiar al segundo paso                
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icono de ejecutandose
                $('#intermitente1' + idSistema + '').removeClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Generar Base de Datos');//Quita el active a la primera
                $('.estatus1' + idSistema + '').css('border', '2px solid #bd0400');
                $('.prev-step' + idSistema + '').click();
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('.loading-btn-botonGenerarBD' + idSistema + '').button('reset');
                $('#intermitente1' + idSistema + '').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en generarBDSGC.");
            $('.loading-btn-botonGenerarBD' + idSistema + '').button('reset');
            $('#intermitente1' + idSistema + '').removeClass('parpadea');
            unBlock();
        }
    });
}
//Generar .BAK del Sistema SGRO
function generarBDSGRO(idSistema) {
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-botonGenerarBD' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate' + idSistema + '').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Generar Base de Datos');//Quita el active a la primera
    $('.estatus1' + idSistema + '').css('border', '2px solid #b7b224');
    $('#intermitente1' + idSistema + '').addClass('parpadea');
    //block();
    $.ajax({
        url: "Sistemas.aspx/generarBDSGRO",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea terminada! Generar Base de Datos');//Quita el active a la primera
                $('.estatus1' + idSistema + '').css('border', '2px solid #30bf54');
                $('#btnGenerarBD' + idSistema + '').hide();
                //Cambiar al segundo paso
                var $active = $('.wizard .nav-tabs li.presentation1' + idSistema + '');
                $active.next().removeClass('disabled');
                nextTab($active);
                //Fin cambiar al segundo paso                
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('#intermitente1' + idSistema + '').removeClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Generar Base de Datos');//Quita el active a la primera
                $('.estatus1' + idSistema + '').css('border', '2px solid #bd0400');
                $('.prev-step' + idSistema + '').click();
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('.loading-btn-botonGenerarBD' + idSistema + '').button('reset');
                $('#intermitente1' + idSistema + '').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en generarBDSGRO.");
            $('.loading-btn-botonGenerarBD' + idSistema + '').button('reset');
            $('#intermitente1' + idSistema + '').removeClass('parpadea');
            unBlock();
        }
    });
}
//Generar .BAK del Sistema SGCE
function generarBDSGCE(idSistema) {
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-botonGenerarBD' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate' + idSistema + '').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Generar Base de Datos');//Quita el active a la primera
    $('.estatus1' + idSistema + '').css('border', '2px solid #b7b224');
    $('#intermitente1' + idSistema + '').addClass('parpadea');
    //block();
    $.ajax({
        url: "Sistemas.aspx/generarBDSGCE",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea terminada! Generar Base de Datos');//Quita el active a la primera
                $('.estatus1' + idSistema + '').css('border', '2px solid #30bf54');
                $('#btnGenerarBD' + idSistema + '').hide();
                //Cambiar al segundo paso
                var $active = $('.wizard .nav-tabs li.presentation1' + idSistema + '');
                $active.next().removeClass('disabled');
                nextTab($active);
                //Fin cambiar al segundo paso                
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('#intermitente1' + idSistema + '').removeClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.title-change1' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Generar Base de Datos');//Quita el active a la primera
                $('.estatus1' + idSistema + '').css('border', '2px solid #bd0400');
                $('.prev-step' + idSistema + '').click();
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('.loading-btn-botonGenerarBD' + idSistema + '').button('reset');
                $('#intermitente1' + idSistema + '').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en generarBDSGCE.");
            $('.loading-btn-botonGenerarBD' + idSistema + '').button('reset');
            $('#intermitente1' + idSistema + '').removeClass('parpadea');
            unBlock();
        }
    });
}
//Cambiar los esquemas de DANA a los del nuevo Grupo
function cambiarEsquemasBDEmpresa() {
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-esquemasBDEMP').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change2').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Cambiar Esquemas');//Quita el active a la primera
    $('.estatus2').css('border', '2px solid #b7b224');
    $('#intermitente2').addClass('parpadea');
    //var idSistema = $('#idSistema').val();
    //block();
    $.ajax({
        url: "Sistemas.aspx/cambiarEsquemasBDEmpresa",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.rEsquemasBDEmpresas').removeClass('hide');
                $('#rEsquemasBDEmpresas').append(data.d.substring(0, mensaje - 1));
                $('.estatus2').css('border', '2px solid #30bf54');
                $('#btnCambiarEsquemaBDEMP').hide();
                var $active = $('.wizard .nav-tabs li.active.emp');
                $active.next().removeClass('disabled');
                nextTab($active);
                //Fin cambiar al segundo paso                
                $('.iconUpdate').addClass('hide');//Mostrar icno de ejecutandose
                $('.title-change2').removeAttr('data-original-title').attr('data-original-title', '¡Tarea terminada! Cambiar Esquemas');//Quita el active a la primera
                $('#intermitente2').removeClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.rEsquemasBDEmpresas').removeClass('hide');
                $('#rEsquemasBDEmpresas').append('Error al cambiar los Esquemas de la base de datos de Empresa.');
                $('.estatus2').css('border', '2px solid #bd0400');
                $('.prev-step').click();
                $('.iconUpdate').addClass('hide');//Mostrar icno de ejecutandose
                $('.title-change2').removeAttr('data-original-title').attr('data-original-title', '¡Error! Cambiar Esquemas');//Quita el active a la primera
                $('.loading-btn-esquemasBDEMP').button('reset');
                $('#intermitente2').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en cambiarEsquemasBDEmpresa.");
            $('.loading-btn-esquemasBDEMP').button('reset');
            unBlock();
        }
    });
}
//Cambiar los esquemas del Sistema SGC
function cambiarEsquemasBDSGC(idSistema) {
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-botonCambiarEsquemas' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate' + idSistema + '').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Cambiar Esquemas');//Quita el active a la primera
    $('.estatus2' + idSistema + '').css('border', '2px solid #b7b224');
    $('#intermitente2' + idSistema + '').addClass('parpadea');
    //var idSistema = $('#idSistema').val();
    //block();
    $.ajax({
        url: "Sistemas.aspx/cambiarEsquemasBDSGC",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea terminada! Cambiar Esquemas');//Quita el active a la primera
                $('.estatus2'+idSistema+'').css('border', '2px solid #30bf54');
                $('#btnCambiarEsquemaBD' + idSistema + '').hide();
                //Cambiar al sig paso
                var $active = $('.wizard .nav-tabs li.presentation2' + idSistema + '');
                $active.next().removeClass('disabled');
                nextTab($active);
                //Fin cambiar al sig paso                
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('#intermitente2' + idSistema + '').removeClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Cambiar Esquemas');//Quita el active a la primera
                $('.estatus2' + idSistema + '').css('border', '2px solid #bd0400');
                $('.prev-step').click();
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('.loading-btn-botonCambiarEsquemas' + idSistema + '').button('reset');
                $('#intermitente2' + idSistema + '').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en cambiarEsquemasBDSGC.");
            $('.loading-btn-botonCambiarEsquemas' + idSistema + '').button('reset');
            $('#intermitente2' + idSistema + '').removeClass('parpadea');
            unBlock();
        }
    });
}
//Cambiar los esquemas del Sistema SGRO
function cambiarEsquemasBDSGRO(idSistema) {
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-botonCambiarEsquemas' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate' + idSistema + '').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Cambiar Esquemas');//Quita el active a la primera
    $('.estatus2' + idSistema + '').css('border', '2px solid #b7b224');
    $('#intermitente2' + idSistema + '').addClass('parpadea');
    //var idSistema = $('#idSistema').val();
    //block();
    $.ajax({
        url: "Sistemas.aspx/cambiarEsquemasBDSGRO",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea terminada! Cambiar Esquemas');//Quita el active a la primera
                $('.estatus2' + idSistema + '').css('border', '2px solid #30bf54');
                $('#btnCambiarEsquemaBD' + idSistema + '').hide();
                //Cambiar al sig paso
                var $active = $('.wizard .nav-tabs li.presentation2' + idSistema + '');
                $active.next().removeClass('disabled');
                nextTab($active);
                //Fin cambiar al sig paso                
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('#intermitente2' + idSistema + '').removeClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Cambiar Esquemas');//Quita el active a la primera                
                $('.estatus2' + idSistema + '').css('border', '2px solid #bd0400');
                $('.prev-step').click();
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('.loading-btn-botonCambiarEsquemas' + idSistema + '').button('reset');
                $('#intermitente2' + idSistema + '').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en cambiarEsquemasBDSGRO.");
            $('.loading-btn-botonCambiarEsquemas' + idSistema + '').button('reset');
            $('#intermitente2' + idSistema + '').removeClass('parpadea');
            unBlock();
        }
    });
}
//Cambiar los esquemas del Sistema SGRO
function cambiarEsquemasBDSGCE(idSistema) {
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-botonCambiarEsquemas' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate' + idSistema + '').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Cambiar Esquemas');//Quita el active a la primera
    $('.estatus2' + idSistema + '').css('border', '2px solid #b7b224');
    $('#intermitente2' + idSistema + '').addClass('parpadea');
    //var idSistema = $('#idSistema').val();
    //block();
    $.ajax({
        url: "Sistemas.aspx/cambiarEsquemasBDSGCE",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea terminada! Cambiar Esquemas');//Quita el active a la primera
                $('.estatus2' + idSistema + '').css('border', '2px solid #30bf54');
                $('#btnCambiarEsquemaBD' + idSistema + '').hide();
                //Cambiar al sig paso
                var $active = $('.wizard .nav-tabs li.presentation2' + idSistema + '');
                $active.next().removeClass('disabled');
                nextTab($active);
                //Fin cambiar al sig paso                
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('#intermitente2' + idSistema + '').removeClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.title-change2' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Cambiar Esquemas');//Quita el active a la primera                
                $('.estatus2' + idSistema + '').css('border', '2px solid #bd0400');
                $('.prev-step').click();
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('.loading-btn-botonCambiarEsquemas' + idSistema + '').button('reset');
                $('#intermitente2' + idSistema + '').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en cambiarEsquemasBDSGCE.");
            $('.loading-btn-botonCambiarEsquemas' + idSistema + '').button('reset');
            $('#intermitente2' + idSistema + '').removeClass('parpadea');
            unBlock();
        }
    });
}
//Limpiar Tablas de BDEMP para la nueva configuración
function limpiarBDEmpresa() {
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-limpiarBDEMP').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change3').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera
    $('.estatus3').css('border', '2px solid #b7b224');
    $('#intermitente3').addClass('parpadea');
    //block();
    $.ajax({
        url: "Sistemas.aspx/limpiarBDEmpresa",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.rLimpiarBDEMP').removeClass('hide');
                $('#rLimpiarBDEMP').append(data.d.substring(0, mensaje - 1));
                $('.estatus3').css('border', '2px solid #b7b224');                
                $('#btnLimpiarBDEMP').hide();
                //var $active = $('.wizard .nav-tabs li.active.emp');
                //$active.next().removeClass('disabled');
                //nextTab($active);
                //Fin cambiar al segundo paso                
                $('.iconUpdate').addClass('hide');//Mostrar icono de ejecutandose
                $('#intermitente3').addClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.rLimpiarBDEMP').removeClass('hide');
                $('#rLimpiarBDEMP').append('Error al limpiar la base de datos de Empresa');
                $('.estatus3').css('border', '2px solid #bd0400');
                $('.prev-step').click();
                $('.iconUpdate').addClass('hide');//Mostrar icono de ejecutandose
                $('.loading-btn-botonLimpiarBD').button('reset');
                $('#intermitente3').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en limpiarBDEmpresa.");
            $('.loading-btn-botonLimpiarBD').button('reset');
            unBlock();
        }
    });
}
//Limpiar Tablas del Sistema SGC
function limpiarBDSGC(idSistema) {
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-botonLimpiarBD' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate' + idSistema + '').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera
    $('.estatus3' + idSistema + '').css('border', '2px solid #b7b224');
    $('#intermitente3' + idSistema + '').addClass('parpadea');
    //block();
    $.ajax({
        url: "Sistemas.aspx/limpiarBDSGC",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera
                $('.estatus3' + idSistema + '').css('border', '2px solid #b7b224');
                $('#btnLimpiarBDSGC' + idSistema + '').hide();
                //Cambiar al sig paso
                //var $active = $('.wizard .nav-tabs li.presentation3' + idSistema + '');
                //$active.next().removeClass('disabled');
                //nextTab($active);
                //Fin cambiar al sig paso
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('#intermitente3' + idSistema + '').addClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Limpiar Base de Datos');//Quita el active a la primera
                $('.estatus3' + idSistema + '').css('border', '2px solid #bd0400');
                $('.prev-step').click();
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('.loading-btn-botonLimpiarBD' + idSistema + '').button('reset');
                $('#intermitente3' + idSistema + '').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en limpiarBDSGC.");
            $('.loading-btn-botonLimpiarBD' + idSistema + '').button('reset');
            $('#intermitente3' + idSistema + '').removeClass('parpadea');
            unBlock();
        }
    });
}
//Limpiar Tablas del Sistema SGRO
function limpiarBDSGRO(idSistema) {
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-botonLimpiarBD' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate' + idSistema + '').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera                
    $('.estatus3' + idSistema + '').css('border', '2px solid #b7b224');    
    $('#intermitente3' + idSistema + '').addClass('parpadea');
    //block();
    $.ajax({
        url: "Sistemas.aspx/limpiarBDSGRO",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera                
                $('.estatus3' + idSistema + '').css('border', '2px solid #b7b224');
                //$('span.round-tab:first').css('border', '2px solid #30bf54');
                $('#btnLimpiarBDSGRO' + idSistema + '').hide();
                //Cambiar al sig paso
                //var $active = $('.wizard .nav-tabs li.presentation3' + idSistema + '');
                //$active.next().removeClass('disabled');
                //nextTab($active);
                //Fin cambiar al sig paso
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('#intermitente3' + idSistema + '').addClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Limpiar Base de Datos');//Quita el active a la primera                
                $('.estatus3' + idSistema + '').css('border', '2px solid #bd0400');
                $('.prev-step').click();
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('.loading-btn-botonLimpiarBD' + idSistema + '').button('reset');
                $('#intermitente3' + idSistema + '').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en obtenerSistemasSolicitados.");
            $('.loading-btn-botonLimpiarBD' + idSistema + '').button('reset');
            $('#intermitente3' + idSistema + '').removeClass('parpadea');
            unBlock();
        }
    });
}
//Limpiar Tablas del Sistema SGRO
function limpiarBDSGCE(idSistema) {
    var nomGrupo = $('#txtNomGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-botonLimpiarBD' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate' + idSistema + '').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera                
    $('.estatus3' + idSistema + '').css('border', '2px solid #b7b224');
    $('#intermitente3' + idSistema + '').addClass('parpadea');
    //block();
    $.ajax({
        url: "Sistemas.aspx/limpiarBDSGCE",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idReporte:" + idReporte + ",idUsuario:" + idUsuario + ",idSistema:" + idSistema + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Limpiar Base de Datos');//Quita el active a la primera                
                $('.estatus3' + idSistema + '').css('border', '2px solid #b7b224');
                //$('span.round-tab:first').css('border', '2px solid #30bf54');
                $('#btnLimpiarBDSGCE' + idSistema + '').hide();
                //Cambiar al sig paso
                //var $active = $('.wizard .nav-tabs li.presentation3' + idSistema + '');
                //$active.next().removeClass('disabled');
                //nextTab($active);
                //Fin cambiar al sig paso
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('#intermitente3' + idSistema + '').addClass('parpadea');
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.title-change3' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! Limpiar Base de Datos');//Quita el active a la primera                
                $('.estatus3' + idSistema + '').css('border', '2px solid #bd0400');
                $('.prev-step').click();
                $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                $('.loading-btn-botonLimpiarBD' + idSistema + '').button('reset');
                $('#intermitente3' + idSistema + '').removeClass('parpadea');
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en limpiarBDSGCE.");
            $('.loading-btn-botonLimpiarBD' + idSistema + '').button('reset');
            $('#intermitente3' + idSistema + '').removeClass('parpadea');
            unBlock();
        }
    });
}
//Guardar URL del sistema generado
function guardarURL(idSistema) {
    var idGrupo = $('#txtIdGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var url = $('#txtURL' + idSistema + '').val();
    var idUsuario = $('#txtIdUsuario').val();
    $('.loading-btn-botonURL' + idSistema + '').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate' + idSistema + '').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change4' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! URL Sistema');//Quita el active a la primera                
    $('#intermitente3' + idSistema + '').removeClass('parpadea');
    $('#intermitente4' + idSistema + '').addClass('parpadea');
    //block();
    if ($.trim( $('#txtURL' + idSistema + '').val()) !== '') {
        $.ajax({
            url: "Sistemas.aspx/guardarURL",
            type: "POST",
            data: "{idGrupo:" + idGrupo + ",idReporte:" + idReporte + ",idSistema:" + idSistema + ",url:'" + url + "',idUsuario:" + idUsuario + "}",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                $('.estatus4' + idSistema + '').css('border', '2px solid #b7b224');
                if (data.d == true) {
                    alertify.success("<span id='icon-25' class='success blanco'></span>Se guardo correctamente la URL.");
                    $('.title-change4' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Tarea terminada! URL Sistema');//Quita el active a la primera                
                    $('.estatus4' + idSistema + '').css('border', '2px solid #30bf54');
                    $('.btnUrl' + idSistema + '').addClass('hide');
                    $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                    $('#lblURL' + idSistema + '').addClass('hide');//Ocultar lbl url
                    $('#txtURL' + idSistema + '').addClass('hide');//Ocultar txt url
                    $('#intermitente4' + idSistema + '').removeClass('parpadea');
                } else {
                    $('.estatus4' + idSistema + '').css('border', '2px solid #ff3632');
                    $('.prev-step').click();
                    alertify.error("<span id='icon-25' class='warning blanco'></span>Error al guardar la URL.");
                    $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
                    $('.title-change4' + idSistema + '').removeAttr('data-original-title').attr('data-original-title', '¡Error! URL Sistema');//Quita el active a la primera                
                    $('.estatus4' + idSistema + '').css('border', '2px solid #bd0400');
                    $('#intermitente4' + idSistema + '').removeClass('parpadea');
                }
                unBlock();
            },
            error: function (xhr, status, error) {
                alertify.error("<span id='icon-25' class='warning blanco'></span>Error en guardarURL.");
                $('#intermitente4' + idSistema + '').removeClass('parpadea');
                unBlock();
            }
        });
    } else {
        $('#txtURL' + idSistema + '').attr('placeholder', 'Ingresa la URL').focus();
        $('.iconUpdate' + idSistema + '').addClass('hide');//Mostrar icno de ejecutandose
    }
   
}
//Clic para mostrar el traking del sistema seleccionado
function verTrakingPorSistema(idSistema) {
    var idReporte = $('#txtIdReporte').val();
    $('.btnVerTraking').removeClass("verde");
    $('.btnVerTraking').addClass("azul");
        
    $('#btnVerTraking' + idSistema + '').removeClass('azul');
    $('#btnVerTraking' + idSistema + '').addClass('verde');

    $('.traking').each(function () {
        $('.traking').addClass('hide');
        $('#trakingSistema' + idSistema + '').removeClass('hide');
    });
    existeEstatusDBSGC(idReporte, idSistema)//Verifica los estatus en los cuales se quedo la generación de la BDSC
    existeEstatusDBSGRO(idReporte, idSistema);//Verifica los estatus en los cuales se quedo la generación de la BDRO
    existeEstatusDBSGCE(idReporte, idSistema);//Verifica los estatus en los cuales se quedo la generación de la BDCE
    //$('#trakingSistema'+idSistema+'').show('slow');
}
//Cerrar los Traking
function cerrarTraking(idSistema) {
    $('#trakingSistema' + idSistema + '').hide('slow');
}
//Ver el historial de ejeciciones de las bases de datos
function verHistorialBDEjecucion() {
    var idReporte = $('#txtIdReporte').val();
    block();
    $.ajax({
        url: "Sistemas.aspx/verHistorialBDEjecucion",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            $('#divTblHistorial').append(data.d);
            $("#tblHistorial").dataTable({
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "aoColumnDefs": [
                       { "sClass": "txt-left", "aTargets": [1,2, 3, 4] }
                ],
                "pageLength": 5
            });
            $("#tblHistorial").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en verHistorialBDEjecucion.");
            unBlock();
        }
    });
}
//Guardar URL SGI
function guardarSGI() {
    var idGrupo = $('#txtIdGrupo').val();
    var url = $('#txtRutaSGI').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    if ($.trim($('#txtRutaSGI').val()) !== '') {
        block();
        $.ajax({
            url: "Sistemas.aspx/guardarSGI",
            type: "POST",
            data: "{idGrupo:" + idGrupo + ",idReporte:" + idReporte + ", url:'" + url + "',idUsuario:" + idUsuario + "}",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                if (data.d == true) {
                    alertify.success("<span id='icon-25' class='success blanco'></span>URL SGI guardada correctamente.");
                    $("#dlogSGI").dialog("close");
                    unBlock();
                } else if (data.d == false) {
                    alertify.error("<span id='icon-25' class='warning blanco'></span>Error al guardar URL SGI.");
                    unBlock();
                }

                unBlock();
            },
            error: function (xhr, status, error) {
                alertify.error("<span id='icon-25' class='warning blanco'></span>Error en guardarSGI.");
                unBlock();
            }
        });
    } else {
        $('#txtRutaSGI').focus();
    }

}
// Cargar Configuación inicial para Empresa
function configuracionInicial() {
    $('.loading-btn-confIniBDEMP').button('loading');//Mostrar moton con animacion de ejecución
    $('.iconUpdate').removeClass('hide');//Mostrar icno de ejecutandose
    $('.title-change4').removeAttr('data-original-title').attr('data-original-title', '¡En proceso! Configuración Inicial');//Quita el active a la primera                
    $('#intermitente3').removeClass('parpadea');
    $('#intermitente4').addClass('parpadea');
    $('.estatus4').css('border', '2px solid #b7b224');
    var lstSistemas = $('#txtIdsSistemas').val();//Colocar ids de los sistemas solicitados en una caja de texto oculta
    var nomGrupo = $('#txtNomGrupo').val();
    var idGrupo = $('#txtIdGrupo').val();
    var idReporte = $('#txtIdReporte').val();
    var idUsuario = $('#txtIdUsuario').val();
    $.ajax({
        url: "Sistemas.aspx/configuracionInicial",
        type: "POST",
        data: "{nomGrupo:'" + nomGrupo + "',idGrupo:" + idGrupo + ", idReporte:" + idReporte + ",idUsuario:" + idUsuario + ",lstSistemas:'" + lstSistemas + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('.btnVerTraking ').removeClass('hide');
                $('.estatus4').css('border', '2px solid #30bf54');
                $('#intermitente4').removeClass('parpadea');
                $('.title-change4').removeAttr('data-original-title').attr('data-original-title', '¡Tarea terminada! Configuración Inicial');//Quita el active a la primera                
                $('.iconUpdate').addClass('hide');//Mostrar icno de ejecutandose
                $('.loading-btn-confIniBDEMP').hide();
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, data.d.indexOf('°Error') - 1));
                $('.estatus4').css('border', '2px solid #bd0400');
                $('#intermitente4').removeClass('parpadea');
                $('.title-change4').removeAttr('data-original-title').attr('data-original-title', '¡Error! Configuración Inicial');//Quita el active a la primera                
                $('.loading-btn-confIniBDEMP').button('reset');
                $('.iconUpdate').addClass('hide');//Mostrar icno de ejecutandose
            }            
            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en configuracionInicial.");
            $('#intermitente4').removeClass('parpadea');
            $('.title-change4').removeAttr('data-original-title').attr('data-original-title', '¡Error! Configuración Inicial');//Quita el active a la primera                
            $('.loading-btn-confIniBDEMP').button('reset');
            $('.iconUpdate').addClass('hide');//Mostrar icno de ejecutandose
            unBlock();
        }
    });
}