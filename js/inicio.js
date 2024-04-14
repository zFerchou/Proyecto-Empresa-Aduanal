var txtidUser;
var bAvance;
$(document).ready(function () {
    
    //invocar función para obtener los pendientes.
    start();
    txtidUser = $("#txtidUser").val();
    //Dar estilo a los title de los elementos.
    $(document).tooltip();

    //boton que invoca la funcion para abrir dialog de formulario
    //para insertar la respuesta validado del reporte
    //se construye uploadify 
    $('#btnTerminarReporte').click(function (e) {
        $("#calif").empty();
        var idReporte = $("#txtid").val();
        $("#subir").empty();
        $("#subir").append('<input type="file" name="flSubirEvidencia' + idReporte + '" id="flSubirEvidencia' + idReporte + '" />');
        subir(idReporte);
        $("#consulta").toggle("slow");
        $("#termino").toggle("slow");
        $('#btnTerminarReporte').hide();
        $('#btnRechazarReporte').hide();
        $('#cerrar').hide();
        $(".calificacion").jRating({
            step: false,
            length: 5
        });
    });

    //boton terminar, se valida los campos requeridos
    //se invoca la funcion de insertar respuesta reporte finalizado,
    //subir archivo (imagen) a servidor
    $('#btnAceptarTermino').click(function (e) {
        var id = $("#txtid").val();
        var nombreArchivo = $("#txtnombreArchivo").val();
        var comentario = $("#txtComentario").val();
        var cal = $("#cal").val();
        $("#accion").val(5);
        $("#alertas").remove();
        $("#alertaC").remove();
        if ((nombreArchivo == '' && $.trim(comentario) == '') || cal == '') {
            if (cal == '') {
                $('#calif').focus().after("<span id='alertaC' class='requerido'>Seleccione una Calificacion</span>");
                setTimeout(function () {
                    $('#alertaC').fadeOut('slow');
                }, 3000);
            }
            if (nombreArchivo == '' && $.trim(comentario) == '') {
                $('#txtComentario').focus().after("<span id='alertas' class='requeridoComentarioV'>Introduce un comentario o adjunta un archivo</span>");
                setTimeout(function () {
                    $('#alertas').fadeOut('slow');
                }, 3000);
            }
        } else {
            if (nombreArchivo != '') {
                block();
                $("#flSubirEvidencia" + id).uploadify('upload');
            } else {
                insertaComentario(id, comentario, nombreArchivo, cal);
            }
        }
    });

    //boton que invoca la funcion para abrir el formulario para rechazar el reporte
    $('#btnRechazarReporte').click(function (e) {
        var idReporte = $("#txtid").val();
        $("#subir").empty();
        $("#subirRechazo").empty();
        $("#subirRechazo").append('<input type="file" name="flSubirEvidencia' + idReporte + '" id="flSubirEvidencia' + idReporte + '" />');
        subir(idReporte);
        $("#rechazar").toggle("slow");
        $("#consulta").toggle("slow");
        $('#btnTerminarReporte').hide();
        $('#btnRechazarReporte').hide();
        $('#btnAvance').hide();
        $('#cerrar').hide();
    });

    //boton que invoca la funcion para insertar el reporte terminado correctamente
    //validacion de campos necesarios para la insercion
    $('#btnAceptarRechazo').click(function (e) {
        var id = $("#txtid").val();
        var comentario = $("#txtComentarioRechazo").val();
        var nombreArchivo = $("#txtnombreArchivo").val();
        $("#alertas").remove();
        $("#accion").val(4);
        if ($.trim(comentario) == '' && nombreArchivo == '') {
            $('#txtComentarioRechazo').focus().after("<span id='alertas' class='requeridoComentario'>Introduce un comentario o adjunta un archivo.</span>");
            setTimeout(function () {
                $('#alertas').fadeOut('slow');
            }, 3000);
        } else {
            if (nombreArchivo != '') {
                block();
                $('#flSubirEvidencia' + id).uploadify('upload');
            } else {
                insertaComentarioRechazo(id, comentario, nombreArchivo);
            }
        }
    });

    //boton para cerrar dialog de rechazar reporte
    $('#btnCancelarRechazo').click(function (e) {
        $("#rechazar").hide();
        $("#btnTerminarReporte").show();
        $("#btnRechazarReporte").show();
        $("#consulta").toggle("slow");
        $("#cerrar").show();
    });
    //boton para cerrar dialog de validar reporte.
    $('#btnCancelarTermino').click(function (e) {
        $("#termino").hide();
        $("#btnTerminarReporte").show();
        $("#btnRechazarReporte").show();
        $("#consulta").toggle("slow");
        $("#cerrar").show();
        $("#cal").val('');
    });

    //Boton para cancelar el envio de la incidencia
    $('#btnCancelarEnvio').click(function (e) {
        $("#responder").hide();
        $("#btnResponderReporte").show();
        $("#consulta").toggle("slow");
        $("#cerrar").show();
        $("#boton").show();
        $("#btnEnviarModificar").show();
        if (bAvance == 1) {
            $("#btnAvance").show();
        }
    });

    //boton para cerrar dialog de detalle de reporte
    $('#btnCerrarDlogDetalleReporte').click(function (e) {
        cerrarDialog("#dlogReporte");
    });

    //boton para abrir dialog parea responder un reporte tabla asignados(proceso)
    $('#btnResponderReporte').click(function (e) {
        var idReporte = $("#txtid").val();
        $("#subirRespuesta").empty();
        $("#subirRespuesta").append('<input type="file" name="flSubirEvidencia' + idReporte + '" id="flSubirEvidencia' + idReporte + '" />');
        subir(idReporte);
        $("#consulta").toggle("slow");
        $("#responder").toggle("slow");
        $("#cerrar").hide();
        $("#btnResponderReporte").hide();
        $("#boton").hide();
        $("#btnEnviarModificar").hide();
        $("#btnAvance").hide();
    });

    $('#btnPausarReanudar').click(function (e) {
        var idReporte = $("#txtid").val();
        $("#subirRespuesta").empty();
        //$("#subirRespuesta").append('<input type="file" name="flSubirEvidencia' + idReporte + '" id="flSubirEvidencia' + idReporte + '" />');
        //subir(idReporte);
        $("#consulta").toggle("slow");
        $("#responder").toggle("slow");
        $("#cerrar").hide();
        $("#btnResponderReporte").hide();
        $("#boton").hide();
        $("#btnEnviarModificar").hide();
        $("#btnAvance").hide();
    });

    //boton para aceptar envio de respuesta de reporte
    $('#btnAceptarEnvio').click(function (e) {
        var idReporte = $("#txtid").val();
        var comentario = $("#txtComentarioEnvio").val();
        var nombreArchivo = $("#txtnombreArchivo").val();
        var urlERP = $("#txtUrl").val();
        $("#alertaComentarioEnvio").remove();
        $("#alertaUrl").remove();
        $("#accion").val(3);
        if ($("#tipoReporte").val() == 3) {
            if (typeof $("#txtUrl").val() == "undefined") {
                if ($.trim(comentario) == '' && nombreArchivo == '') {
                    $('#txtComentarioEnvio').focus().after("<span id='alertaComentarioEnvio' class='requeridoComentario'>Introduce un comentario o adjunta un archivo</span>");
                    setTimeout(function () {
                        $('#alertaComentarioEnvio').fadeOut('slow');
                    }, 3000);
                } else {
                    if (nombreArchivo != '') {
                        block();
                        $('#flSubirEvidencia' + idReporte).uploadify('upload');
                    } else if (nombreArchivo == '' && $.trim(comentario) != '') {
                        insertaComentarioRespuesta(idReporte, comentario, nombreArchivo, "");
                    }
                }
            } else if (typeof $("#txtUrl").val() != "undefined") {
                if ($.trim(comentario) == '' && nombreArchivo == '' && $.trim(urlERP) == '') {
                    $('#txtComentarioEnvio').focus().after("<span id='alertaComentarioEnvio' class='requeridoComentario'>Introduce un comentario o adjunta un archivo</span>");
                    setTimeout(function () {
                        $('#alertaComentarioEnvio').fadeOut('slow');
                    }, 3000);
                    $("#txtUrl").focus().after("<span id='alertaUrl' class='errorURL'>Introduce la Url.</span>");
                    setTimeout(function () {
                        $('#alertaUrl').fadeOut('slow');
                    }, 3000);
                } else {
                    if ($.trim(urlERP) == '') {
                        $("#txtUrl").focus().after("<span id='alertaUrl' class='errorURL'>Introduce la Url.</span>");
                        setTimeout(function () {
                            $('#alertaUrl').fadeOut('slow');
                        }, 3000);
                    }
                    if (nombreArchivo != '' && $.trim(urlERP) != '') {
                        block();
                        $('#flSubirEvidencia' + idReporte).uploadify('upload');
                    } else if ($.trim(comentario) != '' && $.trim(urlERP) != '') {
                        insertaComentarioRespuesta(idReporte, comentario, nombreArchivo, urlERP);
                    }
                }
            }
        } else {
            if ($.trim(comentario) == '' && nombreArchivo == '') {
                $('#txtComentarioEnvio').focus().after("<span id='alertaComentarioEnvio' class='requeridoComentario'>Introduce un comentario o adjunta un archivo</span>");
                setTimeout(function () {
                    $('#alertaComentarioEnvio').fadeOut('slow');
                }, 3000);
            } else {
                if (nombreArchivo != '') {
                    block();
                    $('#flSubirEvidencia' + idReporte).uploadify('upload');
                } else if (nombreArchivo == '') {
                    insertaComentarioRespuesta(idReporte, comentario, nombreArchivo, "");
                }
            }
        }
    });

    //boton que asigna al usuario responsable 
    $('#btnAsignarReporte').click(function (e) {
        var idReporte = $("#txtid").val();
        var fechaTermino = $("#fechaT").val();
        var fechaTerminoD = $("#txtFechaTermino").val();
        var fechat = new Date(fechaTermino);
        var fechatd = new Date(fechaTerminoD);
        if (fechaTerminoD != '') {
            var fecha1 = new Date(fechaTermino);
            var fecha2 = new Date(fechaTerminoD);
            var diasDif = fecha2.getTime() - fecha1.getTime();
            var dias = Math.round(diasDif / (1000 * 60 * 60 * 24));
            if (dias >= 1) {
                $(".icon").removeClass("informacion");
                $(".icon").removeClass("azul");
                $(".icon").addClass("warning");
                $(".icon").addClass("rojo");
            } else {
                $(".icon").removeClass("warning");
                $(".icon").removeClass("rojo");
                $(".icon").addClass("informacion");
                $(".icon").addClass("azul");
            }
            $("#fpt").html('<label class="txt-verde lbl">Propuesta Termino:</label><br /><label  style="padding-left:5px;">' + fechaTermino + '</label>');
            $("#ft").html('<label class="txt-verde lbl">Termino:</label><br /><label  style="padding-left:5px;">' + fechaTerminoD + '</label>');

            $("#texta").html('<br /><label for="tex">Escribe un comentario: </labe><textarea placeholder="Escribe un comentario.." id="tex" class="input300"  rows="4" cols="5" ></textarea>');

            $("#dlogConfirmarAsig").dialog({ modal: true, height: 370, width: 400, resizable: true, draggable: false, show: "clip", hide: "clip",
                close: function (event, ui) {
                }
            });
        } else {
            $('#txtFechaTermino').focus();
        }
    });

    //boton para asignar reporte
    $('#btnAceptarAsignacion').click(function (e) {
        var idReporte = $("#txtid").val();
        var fechaTerminoD = $("#txtFechaTermino").val();
        var comentarioAsignacion = $("#tex").val();
        var personalApoyo = $("#autoC").val();
        if ($("#tipoReporte").val() == 3) {
            if ($.trim($("#tex").val()) != '') {
                asignarReporte(idReporte, fechaTerminoD, comentarioAsignacion, personalApoyo);
            }
            else {
                $('#alertas').remove();
                $('#tex').focus().after("<span id='alertas' class='errorComentario'>Escribe un comentario</span>");
                setTimeout(function () {
                    $('#alertas').fadeOut('slow');
                }, 3000);
            }
        } else {
            asignarReporte(idReporte, fechaTerminoD, comentarioAsignacion, personalApoyo);
        }
    });

    $('#btnAceptarPausar').click(function (e) {
        var idReporte = $("#txtid").val();
        var comentario = $("#textaComentarioPausar").val();
        if ($.trim($("#textaComentarioPausar").val()) != '') {
            console.info("Reporte: " + idReporte + " Comentario: " + comentario + " PAUSADO");
            aceptarPausarReanudar(idReporte, comentario);
        }
        else {
            $('#alertas').remove();
            $('#textaComentarioPausar').focus().after("<span id='alertas' class='errorComentario'>Escribe un comentario</span>");
            setTimeout(function () {
                $('#alertas').fadeOut('slow');
            }, 3000);
        }
    });

    $('#btnAceptarCancelacion').click(function (e) {
        var id = $("#txtid").val();
        var comentario = $("#txtComentarioCancelarIncidencia").val();
        var nombreArchivo = $("#txtnombreArchivo").val();
        $("#alertas").remove();
        $("#accion").val(9);
        if ($.trim(comentario) == '' && nombreArchivo == '') {
            $('#txtComentarioCancelarIncidencia').focus().after("<span id='alertas' class='requeridoComentario'>Introduce un comentario o adjunta un archivo.</span>");
            setTimeout(function () {
                $('#alertas').fadeOut('slow');
            }, 3000);
        } else {
            if (nombreArchivo != '') {
                block();
                $('#flSubirEvidencia' + id).uploadify('upload');
            } else {
                cancelarIncidencia(id, comentario, nombreArchivo);
            }
        }
    });

    $("#btnAceptarEnviarM").click(function () {
        var idReporte = $("#txtid").val();
        var comentario = $("#txtComentarioCancelarIncidencia").val();
        var nombreArchivo = $("#txtnombreArchivo").val();
        $('#accion').val(7)
        $("#alertas").remove();
        if ($.trim(comentario) == '' && nombreArchivo == '') {
            $('#txtComentarioCancelarIncidencia').focus().after("<span id='alertas' class='requeridoComentario'>Introduce un comentario o adjunta un archivo.</span>");
            setTimeout(function () {
                $('#alertas').fadeOut('slow');
            }, 3000);
        } else {
            if (nombreArchivo != '') {
                block();
                $('#flSubirEvidencia' + idReporte).uploadify('upload');
            } else {
                enviarIncidenciaModificar(comentario, idReporte, nombreArchivo);
            }
        }

    });

    $('#btnAvance').click(function (e) {
        var idReporte = $("#txtid").val();
        $("#subirAvance").empty();
        $("#subirAvance").append('<input type="file" name="UpSubirAvance' + idReporte + '" id="UpSubirAvance' + idReporte + '" />');
        subirAvance(idReporte);
        $("#consulta").toggle("slow");
        $("#avance").toggle("slow");
        $("#cerrar").hide();
        $("#btnResponderReporte").hide();
        $("#boton").hide();
        $("#btnEnviarModificar").hide();
        $("#btnAvance").hide();
        $("#lblPausarReanudar").hide();
        var folio = document.getElementById('folio').innerHTML;
        $("#afolio").html(folio);

    });

    $('#btnAceptarAvance').click(function (e) {
        var idReporte = $("#txtid").val();
        var comentario = $("#txtComentarioAvance").val();
        var nombreArchivo = $("#txtnombreArchivo").val();
        //$('#accion').val(7);
        $("#alertas").remove();
        if ($.trim(comentario) == '' && nombreArchivo == '') {
            $('#txtComentarioAvance').focus().after("<span id='alertas' class='requeridoComentario'>Introduce un comentario o adjunta un archivo.</span>");
            setTimeout(function () {
                $('#alertas').fadeOut('slow');
            }, 3000);
        } else {
            if (nombreArchivo != '') {
                block();
                $('#UpSubirAvance' + idReporte).uploadify('upload');
            } else {
                enviarAvance(comentario, idReporte, nombreArchivo);
            }
        }
    });

});

//Función con la cual se genera la tabla en la que se encuentra el log de las acciones que se ha realizado con cada uno de los reportes
//Y ver el estatus en el que se encuentra el reporte
function detalleAccionesReporte() {
    block();
    var idReporte = $("#txtIdReporte").val();
    $("#detalleRep").hide();
    $("#lblTablaDetalleR").show();
    $("#btnOcultarDetalle").show();
    $("#btnMostrarDetalle").hide();
    $.ajax({
        url: "Inicio.aspx/detalleAccionesReporte",
        type: "POST",
        data: "{idReporte:" + idReporte + ", idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTablaDetalleR").html(data.d);
            $("#tblDetalleAccionesR").dataTable({
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3] },
                    { width: "17%", targets: [0] },
                    { width: "17%", targets: [1] },
                    { width: "46%", targets: [2] },
                    { width: "20%", targets: [3] },
                ],
            });
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}


//Función para ocultar la tabla en la que se encuentra el log de las acciones de cada reporte y mostrar el detalle general del mismo
function ocultarDetalleAcciones() {
    $("#detalleRep").show();
    $("#lblTablaDetalleR").hide();
    $("#btnMostrarDetalle").show();
    $("#btnOcultarDetalle").hide();
}

//funcion para agregar las etiquetas de "SinAsignar","Asignado","PorValidar"
function start() {
    block();
    $.ajax({
        url: "Inicio.aspx/start",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblPendiente").html(data.d[0]);
            $("#lblEtiquetas").html(data.d[1]);
            $('.pdtes').click(function () {
                $('.pdtes').removeClass('divSelected');
                $(this).addClass('divSelected');
            });
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//funcion para cerrar los dialog
function cerrarDialogAsignado() {
    cerrarDialog("#dlogReporte");
}

//funcion para obtener los reportes sin asignacion 
//(que no hayan sido asignados a un responsable)
function getReportesSinAsignar() {
    block();
    $.ajax({
        url: "Inicio.aspx/getReportesSinAsignar",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").show();
            $("#lblTabla").html(data.d);
            $("#tblReporteSinAsignar").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "order": [[4, 'desc']],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "8%", targets: [0, 3] },
                    { width: "10%", targets: [1, 2, 4, 7] },
                    { width: "33%", targets: [5] },
                    { width: "7%", targets: [6] },
                    { width: "4%", targets: [8] },
                ],
            });
            $("#tblReporteSinAsignar").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });

            location.href = "#spanActive";
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

function getReportesSinAsignarSprint() {
    block();
    $.ajax({
        url: "Inicio.aspx/getReportesSinAsignarSprint",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").show();
            $("#lblTabla").html(data.d);
            $("#tblReporteSinAsignar").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "order": [[4, 'desc']],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "8%", targets: [0, 3] },
                    { width: "10%", targets: [1, 2, 4, 7] },
                    { width: "33%", targets: [5] },
                    { width: "7%", targets: [6] },
                    { width: "4%", targets: [8] },
                ],
            });
            $("#tblReporteSinAsignar").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });

            location.href = "#spanActive";
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

function getReportesSinAsignarHistorico() {
    block();
    $.ajax({
        url: "Inicio.aspx/getReportesSinAsignarHistorico",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").show();
            $("#lblTabla").html(data.d);
            $("#tblReporteSinAsignar").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "order": [[4, 'desc']],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "8%", targets: [0, 3] },
                    { width: "10%", targets: [1, 2, 4, 7] },
                    { width: "33%", targets: [5] },
                    { width: "7%", targets: [6] },
                    { width: "4%", targets: [8] },
                ],
            });
            $("#tblReporteSinAsignar").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });

            location.href = "#spanActive";
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Construccion de tabla de reportes asignados
//Obetener reportes que tienen Responsable
function getReportesAsignados() {
    block();
    $.ajax({
        url: "Inicio.aspx/getReportesAsignados",
        type: "POST",
        data: "{idUsuario:'" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").show();
            $("#lblTabla").html(data.d);
            $("#tblReporteAsignados").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "order": [[4, 'desc']],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "8%", targets: [0, 3] },
                    { width: "10%", targets: [1, 2, 4, 7] },
                    { width: "33%", targets: [5] },
                    { width: "7%", targets: [6] },
                    { width: "4%", targets: [8] },
                ],
            });
            $("#tblReporteAsignados").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });
            location.href = "#spanActive";
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Construir tabla de reportes por validar
//obtener los reportes que ya tienen respuesta, de que fueron resueltos
function getReportesPorValidar() {
    block();
    $.ajax({
        url: "Inicio.aspx/getReportesPorValidar",
        type: "POST",
        data: "{idUsuario:'" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").show();
            $("#lblTabla").html(data.d);
            $("#tblReportePorValidar").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "order": [[4, 'desc']],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "8%", targets: [0, 3] },
                    { width: "10%", targets: [1, 2, 4, 7] },
                    { width: "33%", targets: [5] },
                    { width: "7%", targets: [6] },
                    { width: "4%", targets: [8] },
                ],
            });
            $("#tblReportePorValidar").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });
            unBlock();
            location.href = "#spanActive";

        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//funcion para obtner los reportes creados por todos los usuarios.
function getReportesCreados() {
    block();
    $.ajax({
        url: "Inicio.aspx/getReportesCreados",
        type: "POST",
        data: "{idUsuario:'" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").show();
            $("#lblTabla").html(data.d);
            $("#tblReportesCreados").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "order": [[4, 'desc']],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "8%", targets: [0, 3] },
                    { width: "10%", targets: [1, 2, 4, 7] },
                    { width: "33%", targets: [5] },
                    { width: "7%", targets: [6] },
                    { width: "4%", targets: [8] },
                ],
            });
            $("#tblReportesCreados").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });

            location.href = "#spanActive";
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//funcion que crea el objeto de uploadify para que tome los atributos del plugin(uploadify)
function subir(id) {
    $("#flSubirEvidencia" + id).uploadify({
        'onSelect': function (file) {
            $("#txtnombreArchivo").val(file.name);
        },
        'swf': 'js/Sistema/uploadIfy/uploadify.swf',
        'uploader': 'uploadEvidencia.ashx',
        'cancelImg': '../../uploadIfy/uploadify-cancel.png',
        'folder': '',
        'auto': false,
        'multi': false,
        'formData': { 'idReporte': id },
        'queueSizeLimit': 30,
        'uploadLimit': 2,
        'fileDesc': 'Image Files', //<-- This can be whatever you want
        'fileExt': '*.*',
        'buttonText': "Adjuntar Archivo",
        'onUploadSuccess': function (event, ID, fileObj, response, data) {
            if ($("#accion").val() == 3) {
                var idReporte = $("#txtid").val();
                var comentario = $("#txtComentarioEnvio").val();
                var nombreArchivo = $("#txtnombreArchivo").val();
                var urlERP = $("#txtUrl").val();
                if ($("#tipoReporte").val() == 3) {
                    if (typeof $("#txtUrl").val() == "undefined") {
                        insertaComentarioRespuesta(idReporte, comentario, nombreArchivo, "");
                    } else {
                        insertaComentarioRespuesta(idReporte, comentario, nombreArchivo, urlERP);
                    }
                } else {
                    insertaComentarioRespuesta(idReporte, comentario, nombreArchivo, "");
                }
            } else if ($("#accion").val() == 4) {
                var id = $("#txtid").val();
                var comentario = $("#txtComentarioRechazo").val();
                var nombreArchivo = $("#txtnombreArchivo").val();
                insertaComentarioRechazo(id, comentario, nombreArchivo);
            } else if ($("#accion").val() == 5) {
                var id = $("#txtid").val();
                var nombreArchivo = $("#txtnombreArchivo").val();
                var comentario = $("#txtComentario").val();
                var cal = $("#cal").val();
                insertaComentario(id, comentario, nombreArchivo, cal);
            } else if ($("#accion").val() == 9) {
                var id = $("#txtid").val();
                var comentario = $("#txtComentarioCancelarIncidencia").val();
                var nombreArchivo = $("#txtnombreArchivo").val();
                cancelarIncidencia(id, comentario, nombreArchivo);
            } else if ($("#accion").val() == 7) {
                var idReporte = $("#txtid").val();
                var comentario = $("#txtComentarioCancelarIncidencia").val();
                var nombreArchivo = $("#txtnombreArchivo").val();
                enviarIncidenciaModificar(comentario, idReporte, nombreArchivo);
            }
        }
    });
}

//Inserta la respuesta del reporte validado (Reporte Correcto)
function insertaComentario(idReporte, comentario, nombreArchivo, cal) {
    block();
    $.ajax({
        url: "inicio.aspx/insertaComentario",
        dataType: "json",
        data: "{idReporte: " + idReporte + ", comentario: '" + comentario + "',nombreArchivo:'" + nombreArchivo + "', cal:" + cal + ", idUsuario: '" + txtidUser + "'}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            //true= insert correcto
            //false= No se inerto
            //se valida si data.d viene en true, para mandar las alertas y acciones necesarias al usuario
            if (data.d) {
                alertify.success("<span id='icon-25' class='success blanco'></span>La incidencia se terminó correctamente");
                cerrarDialog("#dlogReporte");
                $("#lblTabla").hide();
                start();
            } else {
                alertify.error("<span id='icon-25' class='warning blanco'></span>No se terminó la incidencia");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}

//Funcion para cerrar los dialogs
function cerrarDialog(id) {
    $(id).dialog("close");
}

//se construye el atributo fiel para subir evidencia y se abre el dialog con el formulario para rechazar el reporte
function confirmarRechazo(id) {
    $("#subirRechazo").append('<input type="file" name="flSubirEvidencia' + id + '" id="flSubirEvidencia' + id + '" />');
    $("#dlogConfirmaRechazo").dialog({ modal: true, height: 450, width: 500, resizable: false, draggable: false, show: "clip", hide: "clip",
        open: function (event, ui) {
            subir(id);
        },
        //se limpian cajas de texto
        close: function (event, ui) {
            $("#subirRechazo").empty();
            $("#fecha").empty();
            $("#txtnombreArchivo").val('');
            $("#txtComentarioRechazo").val('');
        }

    });
}

//insert para enviar el reporte de falla a revision, se envian como parametros idReporte, comentario (Falla)
//nombrearchvo (Evidencia), fechaPropuesta (fecha en la que el usuario pretende que este resuelto el fallo)
function insertaComentarioRechazo(idReporte, comentario, nombreArchivo) {
    block();
    $.ajax({
        url: "inicio.aspx/insertaComentarioRechazo",
        dataType: "json",
        data: "{idReporte: " + idReporte + ", comentario: '" + comentario + "',nombreArchivo:'" + nombreArchivo + "', idUsuario: '" + txtidUser + "'}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d) {
                alertify.success("<span id='icon-25' class='success blanco'></span>La incidencia se ha enviado a revisión");
                cerrarDialog("#dlogReporte");
                $("#lblTabla").hide();
                start();
                //getReportesPorValidar();
            } else {
                alertify.error("<span id='icon-47' class='rechazar blanco'></span>Error en guardar información");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}

//Limpiar caja oculta donde se guarda el nombre del archivo (evidencia)
function cancelar() {
    $("#txtnombreArchivo").val('');
}

//Funcion para abrir dialog con formulario para la validar el reporte
function enviarRespuesta(id) {
    //id= idReporte: se recibe como parametro para crear el uploadify y para realizar la insercion
    //En el div #subir se agrega un atributo file para subir el archivo de evidencia
    $("#subirRespuesta").append('<input type="file" name="flSubirEvidencia' + id + '" id="flSubirEvidencia' + id + '" />');
    $("#dlogEnviarReporte").dialog({ modal: true, height: 450, width: 500, resizable: false, draggable: false, show: "clip", hide: "clip",
        open: function (event, ui) {
            subir(id);
        },
        close: function (event, ui) {
            $("#subirRespuesta").empty();
            $("#txtComentarioEnvio").val('');
            $("#txtnombreArchivo").val('');
        }
    });
}

//Inserta la respuesta del reporte por validar
function insertaComentarioRespuesta(idReporte, comentario, nombreArchivo, urlERP) {
    block();
    $.ajax({
        url: "inicio.aspx/insertaComentarioRespuesta",
        dataType: "json",
        data: "{idReporte: " + idReporte + ", comentario: '" + comentario + "',nombreArchivo:'" + nombreArchivo + "', urlERP: '" + urlERP + "', idUsuario: '" + txtidUser + "'}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d) {
                cerrarDialog("#dlogReporte");
                alertify.success("<span id='icon-25' class='success blanco'></span>Se envió la incidencia a revisión");
                $("#lblTabla").hide();
                start();

            } else {
                alertify.error("<span id='icon-25' class='rechazar blanco'></span>Error en guardar información");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}

//Guardar a las personas que van a estar como Apoyo en un Reporte
function guardarPersonaApoyo(idReporte) {
    var idResponsable = $('#txtIdApoyo').val();
    var Usuario = $("#txtApoyo").val();
    if (idResponsable != "") {
        block();
        $.ajax({
            url: "inicio.aspx/guardarPersonaApoyo",
            type: "POST",
            data: "{idReporte:" + idReporte + ", idResponsable:" + idResponsable + "}",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                unBlock();
                if (data.d == true) {
                    alertify.success("<span id='icon-25' class='success blanco'>Se asignó a personal de apoyo");
                    var uDiv = $('div[id^="divLst"]:last');
                    if (uDiv["length"] > 0) {
                        var uNumDiv = parseInt(uDiv.prop("id").match(/\d+/g), 10);
                    } else {
                        var uNumDiv = parseInt("0");
                    }

                    uNumDiv = (uNumDiv + 3)

                    $("#contLista").add("<div id='divLst" + uNumDiv + "' class='listadoConsulta' style='padding-left: 5%'>" + Usuario + "<div style='float: right'><a onclick='javascript:eliminarPersonaResponsable(" + uNumDiv + "," + idResponsable + "," + idReporte + ")'><span id='icon-25' class='rojo eliminar elim' title='Eliminar'></span></a></div></div><div id='divEsp" + uNumDiv + "' style='height:2px'></div>").appendTo("#contLista");
                    $("#txtLista").val("");

                    estiloEliminar();
                    //Al guardar la persona de apoyo limpiar el Autocomplete
                    $('#txtIdApoyo').val('');
                    $('#txtApoyo').val('');
                } else {
                    alertify.error('No se asignó el usuario a la incidencia');
                }
            },
            error: function (xhr, status, error) {
                unBlock();
                alertify.error('Error en agregarPersonaApoyo, idReporte: ' + idReporte + ' idResponsable: ' + idResponsable);
            }
        });
    } else {
        $('#alertas').remove();
        $('#txtApoyo').focus().after("<span id='alertas' class='errorAbajo'>Escribe un responsable</span>");
        setTimeout(function () {
            $('#alertas').fadeOut('slow');
        }, 3000);
    }
}

//dar estilo a icono de agregar personal de apoyo y eliminar personal de apoyo
function estiloEliminar() {
    $(".elim").mouseover(function () {
        $(this).css("transition", "all .3s");
        $(this).css("cursor", "pointer");
        $(this).removeClass();
        $(this).addClass("blanco eliminar");
        $(this).css("transition", "all .3s");
    })
    .mouseout(function () {
        $(this).css("transition", "all .3s");
        $(this).removeClass();
        $(this).addClass("rojo eliminar");
    });
}

//dar estilo a icono de agregar personal de apoyo
function estiloAgregar(id) {
    $(id).mouseover(function () {
        $(this).css("transition", "all .3s");
        $(this).css("cursor", "pointer");
        $(this).removeClass();
        $(this).addClass("blanco agregar");
        $(this).css("transition", "all .3s");
    })
    .mouseout(function () {
        $(this).css("transition", "all .3s");
        $(this).removeClass();
        $(this).addClass("verde agregar");
    });
}

//funcion para asignar el reporte a un responsable que tiene el rol de soporte de reportes,
//se cambia de estatus de reporte en proceso
function asignarReporte(idReporte, fechaTermino, comentario, personalApoyo) {
    block();
    $.ajax({
        url: "inicio.aspx/asignarReporte",
        type: "POST",
        data: "{idReporte:" + idReporte + ", fechaPropuestaTermino:'" + fechaTermino + "',comentario: '" + comentario + "', personalApoyo: '" + personalApoyo + "', idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d == true) {
                cerrarDialog("#dlogReporte");
                cerrarDialog("#dlogConfirmarAsig");
                alertify.success("<span id='icon-25' class='success blanco'></span>Se asignó correctamente la incidencia");
                start();
                $("#lblTabla").hide();

            } else {
                alertify.error('<span id="icon-25" class="warning blanco"></span>No se asignó la incidencia');
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error('Error en asignar reporte');
        }
    });
}

//Construir dialog para ver el detalle del reporte los usuarios de generar reportes y dar soporte a reportes
function getDetalleReporte(idReporte, valor, folioVinculado = "", idReporteAnt = 0) {
    // Consulta para Obtener el detalle de un reporte por id
    var sUrl = "inicio.aspx/consultaReporteUsuario";
    var sData = "{idReporte: " + idReporte + "}";

    // Consulta para Obtener el detalle de un reporte por folio (reporte vinculado)
    if (idReporte == 0 && folioVinculado != "") {
        sUrl = "inicio.aspx/consultaReporteUsuarioVinculado";
        sData = "{ sFolioV: '" + folioVinculado + "' }";
    }

    block();
    $.ajax({
        url: sUrl,
        dataType: "json",
        data: sData,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#tipoReporte").val(data.d.idTipoReporte);
            $("#txtIdReporte").val(idReporte);
            ocultarDetalleAcciones();
            $("#fechaConsulta").html('');
            // Verificar si hay un reporte anterior y el reporte actual es nulo
            if (idReporteAnt != 0 && idReporte === 0) {
                // Si es el caso, cerrar el detalle del reporte anterior y mostrar un enlace para ocultar el detalle de la incidencia anterior
                cerrarReporteDetalle();
                $("#divAccionesCerrar").html("<br> <a  onclick='javascript:  getDetalleReporte(" + idReporteAnt + ",13);' class='btn azul' > <span id='icon - 25' class='cerrar blanco'>Ocultar detalle Incidencia</span> </a>");
            } else {
                // Oculta el boton para regresar a el reporte anterior 
                $("#divAccionesCerrar").html("");
            }
            $("#nombreResp").html("");
            /*$("#folioIncidencia").text(data.d.folio);*/
            //$("#legendFolio").html("<span id='icon-25' class='evidencia " + (data.d.idEstatusReporte === 9 ? 'rojo' : 'azul') + "' style='margin-right: 6px;'></span><label class='" + (data.d.idEstatusReporte === 9 ? 'txt-rojo' : 'txt-azul') +"'>" + data.d.folio +"</label>");
            $("#legendFolio").html("<span id='icon-25' class='evidencia " + (data.d.idEstatusReporte === 9 ? 'pausado' : 'azul') + "' style='margin-right: 6px;'></span><label class='" + (data.d.idEstatusReporte === 9 ? 'txt-pausado' : 'txt-azul') + "'>" + data.d.folio + "</label>");
            $("#foliot").text(data.d.folio);
            $("#folior").text(data.d.folio);
            $("#folioRes").text(data.d.folio);
            $("#foliori").text(data.d.folio);
            $("#dlogReporte").dialog({ title: "Detalle Ticket", modal: true });
            $("#divAcciones").html("");
            // Verificar si hay un folio vinculado en los datos recibidos
            if (data.d.folioVinculado != "") {
                // Si hay un folio vinculado, mostrar un enlace para consultar la incidencia vinculada
                $("#divFolioVinculado").html("<label for='txtFechaTermino' class='txt-verde lbl'>Folio Vinculado:</label><br><br><a title='Consultar Incidencia Vinculada' onclick='javascript:getDetalleReporte(0, 43,\"" + data.d.folioVinculado + "\"," + idReporte + ");'><span id='icon-25' class='ticket'>" + data.d.folioVinculado + "</span></a>");
            } else {
                // Si no hay un folio vinculado, mostrar un texto descriptivo
                $("#divFolioVinculado").html("<label for='txtFechaTermino' class='txt-verde lbl'>Folio Vinculado:</label><br><br><label title='Consultar Incidencia Vinculada'><span id='icon-25' class='ticket'>" + data.d.folioVinculado + "</span></label>");
            }

            $("#divPoints").html("<label><span id='icon-25' class='ticket'>Puntos de Historia: " + data.d.puntos + "</span></label><br /><br />");

            valor = (data.d.idEstatusReporte === 1 && idReporte === 0) ? 13 : valor;
            idReporte = (idReporte === 0) ? data.d.idReporte : idReporte;

            $("#txtid").val(idReporte);
            $("#tipoReporte").val(data.d.idTipoReporte);
            $("#txtIdReporte").val(idReporte);

            if (data.d.idTipoReporte == 3) {
                $("#dlogReporteV").dialog({ title: "Petición ERP", modal: true });
                if (data.d.asunto == "" || data.d.asunto == null) {
                    $("#asunto-comentario").html('<label for="" class="txt-verde lbl">Descripción:</label><br /><br /><label id="lblDescripcion"></label><br />');
                    $("#url").html('<label for="txtUrl">Introduce url ERP.</label><br /><input type="text" id="txtUrl" class="input400" />');
                } else {
                    //$("#txtUrlValidar").val(data.d.asunto);
                    $("#asunto-comentario").html('<label for="" class="txt-verde lbl">Descripción:</label><br /><br /><label id="lblDescripcion"></label><br /><br /><label class="txt-verde lbl">Direcci&oacute;n URL:</label><br /><br /><a title="' + data.d.asunto + '" href="' + data.d.asunto + ' " target="_new">' + data.d.asunto + ' </a><br />');
                }
                if (data.d.evidencia == '') {
                    $("#evidencia").html('<br />');
                }
                else if (data.d.evidencia != '') {
                    $("#evidencia").html("<br /><a class='evidencia verde' id='icon-25' href='Configuracion/Reportes/Evidencia/" + data.d.evidencia + "' target='_new' title='Clic Para Ver Evidencia'>Ver Evidencia<a><br><br><a>Sistemas Solictados:</a><br/>");
                }
                obtenerSistema(idReporte, valor);
                unBlock();
            } else if (data.d.idTipoReporte != 3) {
                $("#dlogReporte").dialog({ title: "Detalle Ticket" });
                
                $("#asunto-comentario").html('<label id="asunto" for="lblAsunto" class="txt-verde lbl">Asunto:</label><br /><br />' +
                    '<label id="lblAsunto"></label><br />' +
                    '<br /><label for="" class="txt-verde lbl">Descripción:</label><br />' +
                    '<div style="text-justify">' +
                    '<br /><label id="lblDescripcion"></label>' +
                    '</div>');
                if (data.d.evidencia == '') {
                    $("#evidencia").html('<br />');
                }
                else if (data.d.evidencia != '') {
                    $("#evidencia").html("<br /><a class='evidencia verde' id='icon-25' href='Configuracion/Reportes/Evidencia/" + data.d.evidencia + "' target='_new' title='Clic Para Ver Evidencia'>Ver Evidencia<a>");
                }
            }

            if (valor == 11) {
                $("#btnTerminarReporte").hide();
                $("#btnRechazarReporte").hide();
                $("#btnAsignarReporte").hide();
                $("#btnEnviarModificar").hide();
                $("#btnResponderReporte").hide();
                $("#fechaconsulta").hide();
                $("#fechatermino").hide();
                $("#nombreResp").html('<label><span id="icon-25" class="warning verde"><span>La Incidencia no ha sido asignada</label>');
                unBlock();
            } else if (valor == 13) {
                obtnerUsuarioSession();
                $("#contenedorAuto").html('<div id="autocomplete"  style="padding-top:10px;"><input id="autoC" class="autocomplete transparente" name="aut" /></div>');
                $("#lblboton").html('<a class="btn blanco" onclick="javascript:cerrarCancelarIncidencia(1);">Cancelar</a>');
                $("#boton").html('<a  onclick = "javascript:rechazarIncidencia(' + data.d.idReporte + ',1);" class="btn azul btn-acciones-reporte" title="Rechazar Incidencia"><span class="blanco">RECHAZAR</span></a>');
                $("#btnRegresar").html('<a  onclick = "javascript:enviarModificar(' + data.d.idReporte + ')" class="btn azul btn-acciones-reporte" id="btnEnviarModificar" title="Enviar a Modificar"><span class="blanco">MODIFICAR</span></a>');
                $("#btnRegresar").show();
                $('#autoc').append('<label class="txt-verde lbl" for="txtApoyo">Personal de Apoyo:</label><input type="text" id="txtApoyo" class="inputP70" placeholder="Escribe el Nombre"/><input type="hidden" id="txtIdApoyo"/><a onclick="javascript:"><span title="Agregar" id="icon-25" class="verde agregar agregar-apoyo" onmouseover="javascript:estiloAgregar(this);" onclick="javascript:guardarPersonaApoyo(' + idReporte + ');"></span></a>');
                ///--cargarPersonalApoyo("#txtIdApoyo", "#txtApoyo", "obtenerPersonalApoyo");
                $("#lblPersonalApoyo").html('<label class="txt-verde lbl">Personal de Apoyo:</label>');
                autocomplete(idReporte);
                $("#btnTerminarReporte").hide();
                $("#btnRechazarReporte").hide();
                $("#btnResponderReporte").hide();
                $("#btnAsignarReporte").show();
                $("#fechatermino").show();
                //$("#fechaPtermino").html(' <label for="txtFechaTermino" class="txt-verde">Fecha Propuesta Termino:</label><br />');
                $("#txtFechaTermino").datepicker({
                    minDate: 0,
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: "yy-mm-dd"
                });
                unBlock();
            }

            if (valor == 21) {
                $("#btnTerminarReporte").hide();
                $("#btnRechazarReporte").hide();
                $("#btnAsignarReporte").hide();
                $("#btnResponderReporte").hide();
                $("#fechatermino").hide();
                var fechaTermino = data.d.fechaPropuestaTermino.substring(6, 19);
                var fechapt = parseInt(fechaTermino);
                var ft = new Date(fechapt);
                var anno = ft.getFullYear();
                var mes = ft.getMonth() + 1;
                mes = "" + mes;
                if (mes.length == 1) {
                    mes = "0" + mes;
                }
                var dia = ft.getDate();
                dia = "" + dia;
                if (dia.length == 1) {
                    dia = "0" + dia;
                }
                var fechaFormada = anno + "-" + mes + "-" + dia;
                $("#fechaConsulta").html('<label for="txtFechaTermino" class="txt-verde lbl">Fecha Termino: </label><br><br><label><span id="icon-25" class="calendario"> ' + fechaFormada + '</span></label>');
                obtenerReponsableReporte(idReporte, 1);

                $("#divAcciones").html(`<a href="javascript:;" onclick="javascript:detalleAccionesReporte();" class="btn azul" id="btnMostrarDetalle"> <span id="icon-25" class="historial blanco">Detalle Incidencia</span> </a>
                <a href="javascript:;" onclick="javascript:ocultarDetalleAcciones();" class="btn azul" id="btnOcultarDetalle"> <span id="icon-25" class="cerrar blanco">Ocultar detalle Incidencia</span> </a>`)
            } else if (valor == 23) {
                $("#lblboton").html('<a class="btn blanco" onclick="javascript:cerrarCancelarIncidencia(2);">Cancelar</a>');
                $("#boton").html('<a  onclick = "javascript:rechazarIncidencia(' + data.d.idReporte + ',2)" class="btn azul btn-acciones-reporte" id="btnRechazarReporte" title="Rechazar Incidencia"><span class="blanco">RECHAZAR</span></a>');
                $("#btnRegresar").html('<a  onclick = "javascript:enviarModificar(' + data.d.idReporte + ')" class="btn azul btn-acciones-reporte" id="btnEnviarModificar" title="Enviar a Modificar"><span class="blanco">MODIFICAR</span></a>');
                $("#btnRegresar").show();
                $("#lblPausarReanudar").html('<a  onclick = "javascript:pausarReanudar(' + data.d.idReporte + ',' + (data.d.idEstatusReporte === 9 ? 2 : 1) +')" class="btn blanco btn-acciones-reporte" id="btnPausarReanudar" title="Pausar / Reanudar"><span class="blanco">' + (data.d.idEstatusReporte === 9 ? 'REANUDAR' : 'PAUSAR') +'</span></a>');
                $("#lblPausarReanudar").show();
                $("#btnTerminarReporte").hide();
                $("#btnRechazarReporte").hide();
                $("#btnAsignarReporte").hide();
                $("#fechatermino").hide();
                var fechaTermino = data.d.fechaPropuestaTermino.substring(6, 19);
                var fechapt = parseInt(fechaTermino);
                var ft = new Date(fechapt);
                var anno = ft.getFullYear();
                var mes = ft.getMonth() + 1;
                mes = "" + mes;
                if (mes.length == 1) {
                    mes = "0" + mes;
                }
                var dia = ft.getDate();
                dia = "" + dia;
                if (dia.length == 1) {
                    dia = "0" + dia;
                }
                var fechaFormada = anno + "-" + mes + "-" + dia;
                $("#fechaConsulta").html('<label for="txtFechaTermino" class="txt-verde lbl">Fecha Termino: </label><br><br><label><span id="icon-25" class="calendario"> ' + fechaFormada + '</span></label>');
                obtenerReponsableReporte(idReporte, 3);
                //if (data.d.idTipoReporte > 4 || data.d.idTipoReporte == 14 || data.d.idTipoReporte == 15 || data.d.idTipoReporte == 16 || data.d.idTipoReporte == 17 || data.d.idTipoReporte == 18 || data.d.tipoIncidencia == 19 || data.d.tipoIncidencia == 20 || data.d.tipoIncidencia == 21) {
                    $('#btnAvance').show();
                    bAvance = 1;
                //}
            }
            if (valor == 31) {
                $("#btnResponderReporte").hide();
                $("#btnAsignarReporte").hide();
                $("#fechatermino").hide();
                var fechaTermino = data.d.fechaPropuestaTermino.substring(6, 19);
                var fechapt = parseInt(fechaTermino);
                var ft = new Date(fechapt);
                var anno = ft.getFullYear();
                var mes = ft.getMonth() + 1;
                mes = "" + mes;
                if (mes.length == 1) {
                    mes = "0" + mes;
                }
                var dia = ft.getDate();
                dia = "" + dia;
                if (dia.length == 1) {
                    dia = "0" + dia;
                }
                var fechaFormada = anno + "-" + mes + "-" + dia;
                $("#fechaConsulta").html('<label for="txtFechaTermino" class="txt-verde lbl">Fecha Termino: </label><br><br><label><span id="icon-25" class="calendario"> ' + fechaFormada + '</span></label>');
                obtenerReponsableReporte(idReporte, 1);
            } else if (valor == 33) {
                $("#btnAsignarReporte").hide();
                $("#btnResponderReporte").hide();
                $("#btnTerminarReporte").hide();
                $("#btnRechazarReporte").hide();
                $("#fechatermino").hide();
                $("#btnEnviarModificar").hide();
                var fechaTermino = data.d.fechaPropuestaTermino.substring(6, 19);
                var fechapt = parseInt(fechaTermino);
                var ft = new Date(fechapt);
                var anno = ft.getFullYear();
                var mes = ft.getMonth() + 1;
                mes = "" + mes;
                if (mes.length == 1) {
                    mes = "0" + mes;
                }
                var dia = ft.getDate();
                dia = "" + dia;
                if (dia.length == 1) {
                    dia = "0" + dia;
                }
                var fechaFormada = anno + "-" + mes + "-" + dia;
                $("#fechaConsulta").html('<label for="txtFechaTermino" class="txt-verde lbl">Fecha Termino: </label><br><br><label><span id="icon-25" class="calendario"> ' + fechaFormada + '</span></label>');
                obtenerReponsableReporte(idReporte, 3);
            }
            //Detalle de incidencia vinculada
            else if (valor == 43) {
                $("#btnAsignarReporte").hide();
                $("#btnResponderReporte").hide();
                $("#btnTerminarReporte").hide();
                $("#btnRechazarReporte").hide();
                $("#fechatermino").hide();
                $("#lblPausarReanudar").hide();
                $("#btnEnviarModificar").hide();
                var fechaTermino = data.d.fechaPropuestaTermino.substring(6, 19);
                var fechapt = parseInt(fechaTermino);
                var ft = new Date(fechapt);
                var anno = ft.getFullYear();
                var mes = ft.getMonth() + 1;
                mes = "" + mes;
                if (mes.length == 1) {
                    mes = "0" + mes;
                }
                var dia = ft.getDate();
                dia = "" + dia;
                if (dia.length == 1) {
                    dia = "0" + dia;
                }
                var fechaFormada = anno + "-" + mes + "-" + dia;
                $("#fechaConsulta").html('<label for="txtFechaTermino" class="txt-verde lbl">Fecha Termino: </label><br><br><label><span id="icon-25" class="calendario"> ' + fechaFormada + '</span></label>');
                obtenerReponsableReporte(idReporte, 2);
            }

            if (data.d.descripcion == '') {
                $("#lblDescripcion").text('Sin comentario');
            } else {
                $("#lblDescripcion").text(data.d.descripcion);
            }
            $("#lblAsunto").text(data.d.asunto);
            var fechaReporte = data.d.fechaReporte.substring(6, 19);
            var fechar = parseInt(fechaReporte);
            var fReporte = new Date(fechar);
            var anno = fReporte.getFullYear();
            var mes = fReporte.getMonth() + 1;
            mes = "" + mes;
            if (mes.length == 1) {
                mes = "0" + mes;
            }
            var dia = fReporte.getDate();
            dia = "" + dia;
            if (dia.length == 1) {
                dia = "0" + dia;
            }
            var fechaReporteL = anno + "-" + mes + "-" + dia
            $("#lblFechaReporte").text(fechaReporteL);
            var fechaRPopuesta = data.d.fechaPropuesta.substring(6, 19);
            var fp = new Date(parseInt(fechaRPopuesta));
            var a = Date.parse(fechaRPopuesta);
            var anno = fp.getFullYear();
            var mes = fp.getMonth() + 1;
            mes = "" + mes;
            if (mes.length == 1) {
                mes = "0" + mes;
            }
            var dia = fp.getDate();
            dia = "" + dia;
            if (dia.length == 1) {
                dia = "0" + dia;
            }
            var fechaReportePropuesta = anno + "-" + mes + "-" + dia;
            $("#fechaT").val(fechaReportePropuesta);
            $("#lblFechaPropuesta").text(fechaReportePropuesta);
            $("#dlogReporte").dialog({ modal: true, resizable: false, draggable: false, height: 580, width: 800, show: "clip", hide: "clip",
                close: function (event, ui) {
                    cerrarReporteDetalle();
                },
                open: function (event, ui) {
                    $('#focus').focus();
                }
            });

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}

//Funcion para limpiar los campos del modal para los Detalles de los Reportes
function cerrarReporteDetalle() {
    $("#txtid").val('');
    $("#btnAsignarReporte").show();
    $("#btnResponderReporte").show();
    $("#btnTerminarReporte").show();
    $("#btnRechazarReporte").show();
    $("#fechatermino").hide();
    $('#nombreResp').empty();
    $('#txtFechaTermino').val('');
    $("#consulta").show("slow");
    $("#termino").hide();
    $("#responder").hide();
    $("#rechazar").hide();
    $("#cerrar").show();
    $("#cal").val('');
    $("#lblsistemas").empty();
    $("#fechaConsulta").empty();
    $("#subirRechazo").empty();
    $("#subirRespuesta").empty();
    $("#txtComentarioEnvio").val('');
    $("#tipoReporte").val('');
    $("#url").empty();
    $("#accion").val('');
    $("#texta").empty();
    $("#urlConsulta").empty();
    $("#boton").empty();
    $("#cancelarIncidencia").hide();
    $("#boton").show();
    $("#subirCancelaIncidencia").empty();
    $("#lblPersonalApoyo").empty();
    $("#nombreResp").empty();
    $("#contenedorAuto").empty();
    $("#txtComentarioRechazo").val('');
    $("#autocomplete").show();
    $("#lblPersonalApoyo").show();
    $("#txtComentarioCancelarIncidencia").val('');
    $('#btnEnviar').hide();
    $("#avance").hide();
    $("#btnAvance").hide();
    $("#txtComentarioAvance").val('');
    bAvance = 0;
}


//Funcion para obtener tabala para incidencias en proceso para usuario(cliente)
function getReportesEnProceso() {
    block();
    $.ajax({
        url: "Inicio.aspx/getReportesEnProceso",
        type: "POST",
        data: "{idUsuario:'" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").show();
            $("#lblTabla").html(data.d);
            $("#tblReportesEnProceso").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "order": [[4, 'desc']],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "8%", targets: [0, 3] },
                    { width: "10%", targets: [1, 2, 4, 7] },
                    { width: "33%", targets: [5] },
                    { width: "7%", targets: [6] },
                    { width: "4%", targets: [8] },
                ],
            });
            $("#tblReportesEnProceso").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });
            unBlock();
            location.href = "#spanActive";

        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Funcion para construir tabla para las incidencias por validar por el usuario(Cliente)
function getReportesCValidar() {
    block();
    $.ajax({
        url: "Inicio.aspx/getReportesCValidar",
        type: "POST",
        data: "{idUsuario:'" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").show();
            $("#lblTabla").html(data.d);
            $("#tblReportesCReportesValidar").dataTable({

                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "order": [[4, 'desc']],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "8%", targets: [0, 3] },
                    { width: "10%", targets: [1, 2, 4, 7] },
                    { width: "33%", targets: [5] },
                    { width: "7%", targets: [6] },
                    { width: "4%", targets: [8] },
                ],
            });
            $("#tblReportesCReportesValidar").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });
            unBlock();
            location.href = "#spanActive";

        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Funcion para obtener el responsable de la incidencia.
function obtenerReponsableReporte(idReporte, tipoU) {
    block();
    $.ajax({
        url: "inicio.aspx/obtenerReponsableReporte",
        dataType: "json",
        data: "{idReporte: " + idReporte + ", tipo: " + tipoU + "}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var x = 0;
            $("#nombreResp").append('<label for="" class="txt-verde lbl">Responsable:</label>')
            $("#nombreResp").append("<div id='divLstresponsable' class='listadoConsultaResponsable' style='padding-left: 2%; width: 200px; margin-top: 15px;'>" + data.d[0] + "<div style='float: right'></div></div><div id='divEsp" + x + "' style='height:2px'></div>").appendTo("#contLista");
            $("#txtLista").val("");
            if (data.d.length > 1) {
                $("#nombreResp").append('<br><label for="" class="txt-verde lbl">Personal de Apoyo:</label>');
                for (var i = 1; i < data.d.length; i++) {
                    x++;
                    $("#nombreResp").append("<div id='divLst" + x + "' class='listadoConsulta' style='padding-left: 2%; width: 200px; margin-top: 15px;'>" + data.d[i] + "<div style='float: right'></div></div><div id='divEsp" + x + "' style='height:2px'></div>").appendTo("#contLista");
                    $("#txtLista").val("");
                }
            }
            unBlock();

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}

//Funcion para obtener el usuario en sesion para colocarlo cuando se asignara una
//incidencia
function obtnerUsuarioSession() {
    block();
    $.ajax({
        url: "inicio.aspx/obtnerUsuarioSession",
        dataType: "json",
        data: "{idUsuario: '" + txtidUser + "'}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#nombreResp").append('<label class="txt-verde lbl">Responsable:</label>')
            $("#nombreResp").append("<div id='responsable' class='listadoConsultaResponsable clear'  style='padding-left: 2%; font-color='red''>" + data.d + "<div style='float: right'></div></div><br />");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}

function fechaPropuestaTermino() {
    $("#txtFechaTermino").focus();
}

//Funcion para obtener los sistemas solicitados en peticion de ERP
function obtenerSistema(idReporte, valor) {
    block();
    $.ajax({
        url: "inicio.aspx/obtenerSistema",
        dataType: "json",
        data: "{idReporte: " + idReporte + "}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //Se coloca el resultado de la llamada ajax en un label
            $("#lblsistemas").html(data.d);
            if ($("#txtvalidar").val() == 0) {
                $("#btnResponderReporte").hide();
            }
            if (valor == 23) {
                $("#urlConsulta").html('<a href="Configuracion/Reportes/Sistemas.aspx?id=' + $("#txtEncrIdReporte").val() + '"><span id="icon-47" class="enviar azul" style="transition: all 0.3s; cursor: pointer;">Crear Base de Datos</span></a>');

            }
            unBlock();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}

//Función para rechazar incidencia 
//recibe idReporte y tipo de Recahzo de incidencia
function rechazarIncidencia(idReporte, tipo) {
    if (tipo == 1) {
        $("#subirCancelaIncidencia").empty();
        $("#subirCancelaIncidencia").append('<input type="file" name="flSubirEvidencia' + idReporte + '" id="flSubirEvidencia' + idReporte + '" />');
        subir(idReporte);
        $('#btnAsignarReporte').hide();
        $('#cerrar').hide();
        $('#cancelarIncidencia').toggle("slow");
        $('#consulta').toggle("slow");
        $('#boton').hide();
        $("#btnAceptarEnviarM").hide();
        $("#btnAceptarCancelacion").show();
        $("#btnEnviarModificar").hide();
        $("#btnAceptarCancelacion").show();
    } else if (tipo == 2) {
        $("#subirCancelaIncidencia").empty();
        $("#subirCancelaIncidencia").append('<input type="file" name="flSubirEvidencia' + idReporte + '" id="flSubirEvidencia' + idReporte + '" />');
        subir(idReporte);
        $('#btnResponderReporte').hide();
        $('#cerrar').hide();
        $('#cancelarIncidencia').toggle("slow");
        $('#consulta').toggle("slow");
        $('#boton').hide();
        $("#btnAceptarEnviarM").hide();
        $("#btnEnviarModificar").hide();
        $("#btnAceptarCancelacion").show();
    }
    $("#btnAvance").hide();
}

//Cerrar el dialog para incidencia.
function cerrarCancelarIncidencia(tipo) {
    $('#txtnombreArchivo').val('');
    $('#cancelarIncidencia').toggle("slow");
    $('#consulta').toggle("slow");
    $("#btnRegresar").show();
    $('#boton').show();
    if (tipo == 1) {
        $('#btnAsignarReporte').show();
        $("#btnEnviarModificar").show();
    } else {
        $('#btnResponderReporte').show();
        $("#btnRegresar").show();
        $("#btnEnviarModificar").show();
        $("#subirCancelaIncidencia").empty();
    }
    if ($("#txtvalidar").val() == 0) {
        $("#btnResponderReporte").hide();
    }
    if (bAvance == 1) {
        $("#btnAvance").show();
    }

    $('#cerrar').show();
}

//Función para cancelar incidencia desde el apartado de incidencias en proceso
function cancelarIncidencia(idReporte, comentario, nombreArchivo) {
    block();
    $.ajax({
        url: "inicio.aspx/cancelarPeticion",
        dataType: "json",
        data: "{idReporte: " + idReporte + ", comentario: '" + comentario + "', nombreArchivo: '" + nombreArchivo + "', idUsuario: '" + txtidUser + "'}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d) {
                alertify.success("<span id='icon-25' class='success blanco'></span>La incidencia se ha cancelado correctamente");
                cerrarDialog("#dlogReporte");
                $("#lblTabla").hide();
                start();
            } else {
                alertify.error("<span id='icon-47' class='rechazar blanco'></span>No se insertó");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}

//Función para invocar el autocomplete, para obtener el personal de soporte
function autocomplete(idReporte) {
    block();
    $.ajax({
        url: "inicio.aspx/obtenerPersonalApoyo",
        data: "{idReporte: " + idReporte + ", idUsuario: '" + txtidUser + "'}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == null || data.d == "") {
                $("#autocomplete").hide();
                $("#lblPersonalApoyo").hide();
            } else {
                var arreglo = [];
                var datos = "";
                for (i = 0; i < data.d.length; i += 2) {
                    arreglo[i] = '{"id" : "' + data.d[i] + '", "name" : "' + data.d[i + 1] + '"}';
                    datos += arreglo[i] + '| ';
                }
                datos = datos.substring(0, (datos.length - 2));
                var nomSeparados = datos.split("| ");
                var arregloUsuarios = [];
                var posicion = 0;
                for (var i = 0; i < nomSeparados.length; i++) {
                    arregloUsuarios[posicion] = JSON.parse(nomSeparados[i]);
                    posicion = posicion + 1;
                }
                $(".autocomplete").tokenInput(arregloUsuarios, { theme: "facebook", preventDuplicates: true, hintText: "Escribe para buscar", noResultsText: "No hay resultados", searchingText: "Buscando..." });
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error");
        }
    });
}


function enviarModificar(idReporte) {
    $('#btnRegresar').hide();
    $("#btnAceptarEnviarM").show();
    $("#btnAceptarCancelacion").hide();
    $("#subirCancelaIncidencia").empty();
    $("#subirCancelaIncidencia").append('<input type="file" name="flSubirEvidencia' + idReporte + '" id="flSubirEvidencia' + idReporte + '" />');
    subir(idReporte);
    $('#btnAsignarReporte').hide();
    $('#cerrar').hide();
    $('#cancelarIncidencia').toggle("slow");
    $('#consulta').toggle("slow");
    $('#boton').hide();
    $("#btnResponderReporte").hide();
    $("#btnAvance").hide();
}

function enviarIncidenciaModificar(comentario, idReporte, nombreArchivo) {
    block();
    $.ajax({
        url: "Inicio.aspx/enviarIncidenciaModificar",
        dataType: "json",
        data: "{idReporte: " + idReporte + ", comentario: '" + comentario + "', nombreArchivo: '" + nombreArchivo + "', idUsuario: '" + txtidUser + "'}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d) {
                alertify.success("<span id='icon-25' class='success blanco'></span>La incidencia se ha cancelado correctamente");
                cerrarDialog("#dlogReporte");
                $("#lblTabla").hide();
                start();
            } else {
                alertify.error("<span id='icon-47' class='rechazar blanco'></span>No se insertó");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}

function getReportesSoportes() {
    block();
    $.ajax({
        url: "Inicio.aspx/getReportesSoportes",
        type: "POST",
        data: "{idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").show();
            $("#lblTabla").html(data.d);
            $("#tblReporteSoporte").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "aoColumnDefs": [
                        { "sClass": "txt-left", "aTargets": [1, 3, 4] }
                    ]
            });
            $("#tblReporteSoporte").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });

            location.href = "#spanActive";
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

function enviarAvance(comentario, idReporte, nombreArchivo) {
    block();
    $.ajax({
        url: "Inicio.aspx/enviarAvance",
        dataType: "json",
        data: "{idReporte: " + idReporte + ", comentario: '" + comentario + "', nombreArchivo: '" + nombreArchivo + "', idUsuario: '" + txtidUser + "'}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d) {
                alertify.success("<span id='icon-25' class='success blanco'></span>Se ha enviado el avance correctamente");
                cerrarDialog("#dlogReporte");
                $("#lblTabla").hide();
                start();
            } else {
                alertify.error("<span id='icon-47' class='rechazar blanco'></span>No se insertó");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}

function cerrar() {
    $("#avance").hide();
    $("#btnResponderReporte").show();
    $("#consulta").toggle("slow");
    $("#cerrar").show();
    $("#boton").show();
    $("#btnEnviarModificar").show();
    $("#btnAvance").show();
}

function subirAvance(id) {
    $("#UpSubirAvance" + id).uploadify({
        'onSelect': function (file) {
            $("#txtnombreArchivo").val(file.name);
        },
        'swf': 'js/Sistema/uploadIfy/uploadify.swf',
        'uploader': 'uploadEvidencia.ashx',
        'cancelImg': '../../uploadIfy/uploadify-cancel.png',
        'folder': '',
        'auto': false,
        'multi': false,
        'formData': { 'idReporte': id },
        'queueSizeLimit': 30,
        'uploadLimit': 2,
        'fileDesc': 'Image Files', //<-- This can be whatever you want
        'fileExt': '*.*',
        'buttonText': "Adjuntar Archivo",
        'onUploadSuccess': function (event, ID, fileObj, response, data) {
            var idReporte = $("#txtid").val();
            var comentario = $("#txtComentarioAvance").val();
            var nombreArchivo = $("#txtnombreArchivo").val();
            enviarAvance(comentario, idReporte, nombreArchivo);
        }
    });
}


function pausarReanudar(idReporte, tipo) {
    if (tipo == 1) { //PUSAR
        $("#dialogPausarIncidencia").dialog({
            modal: true, height: 370, width: 400, resizable: true, draggable: false, show: "clip", hide: "clip",
            close: function (event, ui) {
            }
        });
    } else if (tipo == 2) { //REANUDAR
        $("#dialogPausarIncidencia").dialog({
            modal: true, height: 370, width: 400, resizable: true, draggable: false, show: "clip", hide: "clip",
            close: function (event, ui) {
            }
        });
    }
}

function aceptarPausarReanudar(idReporte, comentario) {
    block();
    $.ajax({
        url: "inicio.aspx/PausarReanudar",
        type: "POST",
        data: "{idReporte:" + idReporte + ", idUsuario: '" + txtidUser + "', comentario: '" + comentario + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d == true) {
                cerrarDialog("#dialogPausarIncidencia");
                cerrarDialog("#dlogReporte");                
                alertify.success("<span id='icon-25' class='success blanco'></span>Estado de reporte actualizado.");
                getReportesAsignados();
                $("#lblTabla").hide();

            } else {
                alertify.error('<span id="icon-25" class="warning blanco"></span>Error al intentar cambiar el estado del reporte.');
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error('Error al intentar cambiar el estado del reporte.');
        }
    });
}