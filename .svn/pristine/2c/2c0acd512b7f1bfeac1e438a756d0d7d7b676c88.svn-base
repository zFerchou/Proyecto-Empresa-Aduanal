var cantidad = 0;
//arreglo para colocar los id de los sistemas.
var id = [];
var datos = "";
var txtidUsuario;
$(document).ready(function () {
    //invocar función para obtener los sistemas.
    obtneridSistemas($("#txtidReporte").val());
    txtidUsuario = $("#txtidUsuario").val();
    $(document).tooltip();


});

//Función para invocar el autocomplete, se obtienen los responsables de los sistemas
function autocomplete(idReporte, idSistema) {
    block();
    $.ajax({
        url: "cuotasSistemas.aspx/obtenerResponsablesSistema",
        data: "{idReporte: " + idReporte + "  }",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d != 0) {
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

            } else {
                $(".autocomplete").hide();
                $(".lblResponsable").html("<span id='icon-47' class='informacion'></span><label>No hay personal que pueda ser responsable del sistema, comuniquese con personal de la empresa.</label><input type='hidden' id='txtSinResp' value=0>");

            }
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error");
        }
    });
}

//Función pra obtener los sistemas que se solictaron y poder configurarlos
function obtneridSistemas() {
    block();
    $.ajax({
        url: "cuotasSistemas.aspx/obtneridSistemas",
        data: "{}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var resultado = "";
            autocomplete($("#txtidReporte").val(), data.d[i]);
            for (var i = 0; i < data.d.length; i++) {
                id.push(data.d[i]);
                resultado += "$('#txtFechaInicio" + data.d[i] + "').datepicker({" +
                            "changeMonth: true," +
                            "changeYear: true," +
                            "dateFormat: 'yy-mm-dd', " +
                            "onClose: function (selectedDate) {" +
                            "$('#txtFechaFin" + data.d[i] + "').datepicker('option', 'minDate', selectedDate)" +
                            "}" +
                    "});" +
                    "$('#txtFechaFin" + data.d[i] + "').datepicker({" +
                            "changeMonth: true," +
                            "changeYear: true," +
                            "dateFormat: 'yy-mm-dd'," +
                            "onClose: function (selectedDate) {" +
                            "$('#txtFechaInicio" + data.d[i] + "').datepicker('option', 'maxDate', selectedDate)" +
                            "}" +
                    "});";
            }
            eval(resultado);
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error");
        }
    });
}

//Función para validar el formulario de la configuración de los sistemas.
function validar() {
    var canti = 0;
    var banderaEach = false;
    var banderaAutoComplete = false;
    var banderaCuota = false;
    var banderaValida = false;
    var banderaCbo = false;
    var patron = /^-?[1-9][0-9]+([.][0-9][0-9])?$/;
    var tam = id.length;
    for (var i = 0; i < tam; i++) {
        datos += $("#autoC" + id[i]).val() + ",";
        var numero = $(".validarCuota" + id[i]).val();
        if (numero != '') {
            if (!patron.test(numero)) {
                banderaCuota = false;
                $(".validarCuota" + id[i]).addClass('bordeRequerido');
            } else {
                $(".validarCuota" + id[i]).removeClass('bordeRequerido');
                datos += $(".validarCuota" + id[i]).val() + ":";
                if (i > 0) {
                    if (!banderaCuota) {
                        banderaCuota = false;
                    } else {
                        banderaCuota = true;
                    }
                } else {
                    banderaCuota = true;
                }
            }
        } else {
            $(".validarCuota" + id[i]).addClass('bordeRequerido');
            banderaCuota = false;
        }
        var tipoMoneda = $("#cbo" + id[i]).val();
        if (tipoMoneda == "" || tipoMoneda == null) {
            $("#cbo" + id[i]).addClass('bordeRequerido');
            banderaCbo = false;
        } else {
            $("#cbo" + id[i]).removeClass('bordeRequerido');
            datos += $("#cbo" + id[i]).val() + "_";
            if (i > 0) {
                if (!banderaCbo) {
                    banderaCbo = false;
                } else {
                    banderaCbo = true;
                }
            } else {
                banderaCbo = true;
            }
        }
        var veces = 0;
        $(".validar" + id[i]).each(function () {
            if ($(this).val() == '') {
                canti++;
                if (canti == 1) {
                    $("#validar").html('<div class="mensaje center bg-alert width50 clear txt-left"><span id="icon-47" class="warning blanco">Complete todos los campos (Responsable, Cuota, Fecha Inicio, Fecha Vencimiento)</span></div>');
                    $("#validar").show("blind");
                    setTimeout(function () {
                        $('.mensaje').hide('blind');
                    }, 4000);
                }
                $(this).addClass('bordeRequerido');
                banderaEach = false;
            } else if ($(this).val() != '') {
                $(this).removeClass('bordeRequerido');
                if (canti == 1) {
                    if (banderaEach) {
                        banderaEach = true;
                    } else {
                        banderaEach = false;
                    }
                } else {
                    banderaEach = true;
                }
            }
            if (banderaEach == true && banderaCuota == true && banderaCbo == true) {
                $(this).removeClass('bordeRequerido');
                datos += $(this).val() + ":";
                banderaValida = true;
            } else {
                banderaValida = false;
            }
        });
        if (banderaEach == false && banderaCuota == true) {
            $("#validar").html('<div class="mensaje center bg-alert width50 clear txt-left"><span id="icon-47" class="warning blanco">Complete los campos(Agregue las fechas).</span></div>');
            $("#validar").show("blind");
            setTimeout(function () {
                $('.mensaje').hide('blind');
            }, 4000);
        }
        datos += id[i];
        if (i != id.length - 1) {
            datos += '|';
        }
    }
    if (banderaValida) {
        terminoPeticion(datos);
    }
}

//Función para insertar la configuracion de los sistemas.
function insertarConfiguracion(datos) {
    block();
    $.ajax({
        url: "cuotasSistemas.aspx/insertarConfiguracion",
        data: "{datos:'" + datos + "', idUsuario: '" + txtidUsuario + "'}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d) {
                alertify.success('<span id="icon-25" class="success blanco">Se guardaron correctamente los datos</span>');
                setTimeout(function () {
                    location.href = "../../Inicio.aspx";
                }, 2000);

            } else {
                alertify.error('<span id="icon-25" class="rechazar blanco">No se guardaron los datos.</span>');
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error");
        }
    });

}

//funcion que crea el objeto de uploadify para que tome los atributos del plugin(uploadify)
function subir(id) {
    $("#flSubirEvidencia" + id).uploadify({
        'onSelect': function (file) {
            $("#txtnombreArchivo").val(file.name);
        },
        'swf': '../../js/Sistema/uploadIfy/uploadify.swf',
        'uploader': '../../uploadEvidencia.ashx',
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
            if ($("#accion").val() == 1) {
                validarERP($("#txtidReporte").val(), $("#txtComentario").val(), $("#txtnombreArchivo").val(), $("#cal").val(), datos);
            } else if ($("#accion").val() == 2) {
                rechazarERP($("#txtidReporte").val(), $("#txtComentarioRechazo").val(), $("#txtnombreArchivo").val());
            }
        }
    });
}

//Función para construir y mostrar el dialog de rechazo de petición
function rechazarPeticion() {
    var id = $("#txtidReporte").val();
    var folio = $("#txtFolio").val();
    $("#dlogRechazar").html('<fieldset>' +
                            '<legend  class="verde">                    ' +
                            '<span id="icon-25" class="evidencia blanco"></span>' +
                            '<a color="FFFFFF" id="folior">' + folio + '</a><a id="A3" href=""></a>' +
                            '</legend>' +
                            '<span id="icon-90" class="verde warning left"></span>' +
                            '<p class="width70 left txt-justify">Se enviar&aacute; la Petición de ERP a revisi&oacute;n.</p>' +

                            '<div class="clear">' +
                            '<label for="txtComentarioRechazo">Escribe un comentario:</label><br />' +
                            '</div>' +
                            '<input type="hidden" id="accion" value="2" /><textarea id="txtComentarioRechazo" placeholder="Escribe tu comentario aqui..." class="input400" rows="4" cols="10"></textarea>' +
                            '<div class="left width50" id="fecha"></div><br /><br />' +
                            '<div class="left width50" id="subirRechazo"></div><br /><br /><br />' +
                            '</fieldset>' +
                            '<br />' +
                            '<div class="clear txt-right">' +
                            '<a class="btn verde" id="btnAceptarRechazo" onclick="javascript:validarRechazo();">Aceptar</a>' +
                            '<a class="btn blanco" id="btnCancelarRechazo" onclick="javascript:cerrarDialog();">Cancelar</a>' +
                            '</div>' +
                            '</div>');
    $("#subirRechazo").html('<input type="file" name="flSubirEvidencia' + id + '" id="flSubirEvidencia' + id + '" />');
    subir(id);
    $("#dlogRechazar").dialog({
        modal: true, resizable: false, draggable: false, height: 550, width: 500, show: "clip", hide: "clip",
        close: function (event, ui) {
            $("#txtnombreArchivo").val('');
            $("#txtcomentarioRecazo").val('');
            $("#accion").val('');
        }
    });
}

//Función para cerrar dialog
function cerrarDialog() {
    $("#dlogRechazar").dialog("close");
}

function cerrarDialogT() {
    $("#dlogTermino").dialog("close");
}
//Función para validar el formulario de rechazo.
function validarRechazo() {
    if ($("#txtnombreArchivo").val() != "" || $.trim($("#txtComentarioRechazo").val()) != "") {
        if ($("#txtnombreArchivo").val() != "") {
            $("#flSubirEvidencia" + $("#txtidReporte").val()).uploadify('upload');
        } else {
            rechazarERP($("#txtidReporte").val(), $("#txtComentarioRechazo").val(), "");
        }
    } else {
        $("#alertas").remove();
        $('#txtComentarioRechazo').focus().after("<span id='alertas' class='requeridoComentario'>Introduce un comentario o adjunta un archivo.</span>");
        setTimeout(function () {
            $('#alertas').fadeOut('slow');
        }, 3000);
    }
}

//Función para rechazar petición de sistemas.
function rechazarERP(idReporte, comentario, nombreArchivo) {
    block();
    $.ajax({
        url: "CuotasSistemas.aspx/rechazarERP",
        dataType: "json",
        data: "{idReporte: " + idReporte + ", comentario: '" + comentario + "',nombreArchivo:'" + nombreArchivo + "',  idUsuario: '" + txtidUsuario + "'}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d) {
                alertify.success("<span id='icon-25' class='success blanco'></span>La Petición se ha enviado a revisión");
                cerrarDialog("#dlogRechazar");
                setTimeout(function () {
                    location.href = "../../Inicio.aspx";
                }, 2000);
            } else {
                alertify.error("<span id='icon-47' class='rechazar blanco'></span>No se inserto");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}

//Función para limpiar la caja de texto del nombre del archivo
function cancelar() {
    $("#txtnombreArchivo").val('');
}

function terminoPeticion(datos) {
    var id = $("#txtidReporte").val();
    var folio = $("#txtFolio").val();
    $("#dlogTermino").html('<div id="termino" class="alert clear width90 center">' +
        '<fieldset>' +
                '<legend  class="verde">' +
                    '<span id="icon-25" class="evidencia blanco"></span>' +
                    '<a color="FFFFFF" id="folior"></a>' + folio + '<a id="+6" href=""></a>' +
                '</legend>' +
		    '<span id="icon-90" class="verde warning left"></span>' +
		    '<p class="left txt-justify width70">Se concluira la petición, por favor califica e introduce un comentario o evidencia</p>' +
            '<div class="clear">' +
            '<label>Evalua la respuesta de la incidencia:</label><br />' +
            '<div id="calif" class="clear calificacion" data-average="10" data-id="1"></div>' +
            '</div>' +
            '<br /><label>Escribe un Comentario</label>' +
            '<div class="clear width20">' +
            '<input type="hidden" id="cal" /><input type="hidden" id="accion" value="1" /><textarea id="txtComentario" placeholder="Escribe tu comentario aqui..." class="input400" rows="4" cols="5"></textarea><br /><br />' +
           '</div>' +
            '<div id="subir"></div>' +
            '</fieldset>' +
            '<br />' +
            '<div class="clear right">' +
                '<a class="btn verde" id="btnAceptarTermino" >Aceptar</a>' +
                '<a class="btn blanco" id="btnCancelarTermino" onclick="javascript:cerrarDialogT();">Cancelar</a>' +
            '</div>');
    $("#subir").html('<input type="file" name="flSubirEvidencia' + id + '" id="flSubirEvidencia' + id + '" />');
    subir(id);
    $("#dlogTermino").dialog({
        modal: true, resizable: false, draggable: false, height: 550, width: 500, show: "clip", hide: "clip",
        close: function (event, ui) {
            $("#calif").empty();
            $("#cal").val('');
            $("#txtidReporte").val('');
            $("#txtnombreArchivo").val('');
            $("#accion").val('');
        },
        open: function (event, ui) {
            $('#btnAceptarTermino').click(function (e) {
                validarTermino(datos);
            });
            $(".calificacion").jRating({
                step: false,
                length: 5
            });
        }

    });
}

function validarTermino(datos) {
    var id = $("#txtidReporte").val();
    var nombreArchivo = $("#txtnombreArchivo").val();
    var comentario = $("#txtComentario").val();
    var cal = $("#cal").val();
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
            validarERP(id, comentario, nombreArchivo, cal, datos);
        }
    }
}

function validarERP(idReporte, comentario, nombreArchivo, cal, datos) {
    block();
    $.ajax({
        url: "CuotasSistemas.aspx/insertarConfiguracion",
        dataType: "json",
        data: "{datos: '" + datos + "', comentario: '" + comentario + "',nombreArchivo:'" + nombreArchivo + "', calificacion: " + cal + ", idUsuario: '" + txtidUsuario + "'}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d) {
                alertify.success('<span id="icon-25" class="success blanco">Se guardaron correctamente los datos</span>');
                setTimeout(function () {
                    location.href = "../../Inicio.aspx";
                }, 2000);

            } else {
                alertify.error('<span id="icon-25" class="rechazar blanco">No se guardaron los datos.</span>');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            unBlock();
            alert(textStatus);
        }
    });
}
