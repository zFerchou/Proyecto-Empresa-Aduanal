var txtidUser;
var nomArchivoM;
var tipoIncidencia;
var vmodificarERP=0;
var evidenciaUploader;
$(document).ready(function () {
    //Si el usuario es externo y tiene area coultar el CBO de Sistemas
    //    if ($('#lblAreaExterno').text().length > 0) {
    //        $('#ddlSistemaG').remove();
    //    }
    //    if ($('#lblGrupoExterno').text().length > 0) {
    //        $('#ddlERPGrupo').remove();
    //    }

    var typeUser = $("#txtTipoUsuario").val();
    if (typeUser == 3) {
        $('#btnAdd').html('<a id="btn1" href="#" class="btn verde shadow2 borde-blanco" ' +
        'onclick="javascript:verFormulario(1);">Agregar Incidencia</a>' +
        '<a id="btn5" href="#" class="btn verde shadow2 borde-blanco" ' +
        'onclick="javascript:verFormulario(5);">Agregar Consulta</a>');
    } else if (typeUser == 2) {
        $('#btnAdd').html('<a id="btn5" href="#" class="btn verde shadow2 borde-blanco" ' +
        'onclick="javascript:verFormulario(5);">Agregar Consulta</a>');
    } else if (typeUser == 1) {
        $('#btnAdd').html('<a id="btn1" href="#" class="btn verde shadow2 borde-blanco" ' +
        'onclick="javascript:verFormulario(1);">Agregar Incidencia</a>' +
        '<a href="#" class="btn verde shadow2 borde-blanco" ');
    }
    //    getEstatusUsuario();
    txtidUser = $("#txtidUser").val();
    //Dialog para verificar la eliminacion de reporte
    $("#dlogEliminarReporte").dialog({
        modal: true,
        autoOpen: false,
        height: 270,
        width: 450,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            $("#txtJustifi").val("");
        },
        open: function (event, ui) {

        }
    });


    //Dialog para verificar la eliminacion de reporte
    $("#dlogEliminarReporteERP").dialog({
        modal: true,
        autoOpen: false,
        height: 270,
        width: 450,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            $("#txtJustifiERP").val("");
        },
        open: function (event, ui) {

        }
    });

    //Dialog para informar que se agregará un nuevo grupo
    $("#dlogAgregarGrupo").dialog({
        modal: true,
        autoOpen: false,
        height: 180,
        width: 270,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {

        },
        open: function (event, ui) {

        }
    });

    //Dialog para ver tabla de lecciones Aprendidas
    $("#dlogLeccionesAprendidas").dialog({
        modal: true,
        autoOpen: false,
        height: 600,
        width: 800,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            $("#divTablaLecciones").show();
            $("#divDetalleL").hide();
        },
        open: function (event, ui) {

        }
    });

    getReportesCreados();

    getLeccionesAprendidas();

    iniciar();

    mostrarLogReportes();

    validaLongitud("txtAsunto", 500);

    //Agregar estilo a los title de los elementos.
    $(document).tooltip();

    //Dar formato de fecha "yy-mm-dd" al datepicker de la fecha propuesta
    $("#txtFechaPropuesta").datepicker({
        minDate: 0,
        dateFormat: 'yy-mm-dd'
    });

    //Botón para cerrar Dialog para el Detalle del Reporte
    $("#btnCerrarDlogDetalleReporte").click(function (e) {
        e.preventDefault();
        $("#frmReporte").dialog("close");
        ocultarDetalleAcciones();
    });

    //Botones para cerrar el dialog con el detalle de leccciones aprendidas
    $("#btnCerrarDlogDetalleLecciones").click(function (e) {
        e.preventDefault();
        $("#dlogLeccionesAprendidas").dialog("close");
        $("#divTablaLecciones").toggle();
        $("#divDetalleL").toggle();
    });

    //Botones para cerrar el dialog con el detalle de leccciones aprendidas
    $("#btnCerrarDlogLecciones").click(function (e) {
        e.preventDefault();
        $("#dlogLeccionesAprendidas").dialog("close");
    });

    //Botón para ver el listado de las lecciones aprendidas
    $("#btnLeccionesAprendidas").click(function (e) {
        e.preventDefault();
        getLeccionesAprendidas();
        $("#dlogLeccionesAprendidas").dialog("open");
    });

    //Botón para cerrar Dialog para cancelar el eliminado de un reporte
    $("#btnCancelarEliminacion").click(function (e) {
        e.preventDefault();
        $("#dConfirmar").dialog("close");
    });

    //Botón para realizar validaciones y eliminar un reporte 
    $("#btnSi").click(function (e) {
        var justifi = $('#txtJustifi').val();
        $("#alertJustificacion").remove();
        if (justifi == "" || justifi.trim() === "") {
            $("#txtJustifi").focus().after("<span id='alertJustificacion' class='requerido-justificacion'>Escriba una justificación</span>");
            setTimeout(function () {
                $("#alertJustificacion").fadeOut('slow');
            }, 3000);
        } else {
            eliminar();
        }
    });

    //Botón para realizar validaciones cuando se desea eleminar una petición de ERP y posteriormente eliminarla
    $("#btnSiERP").click(function (e) {
        var justifi = $('#txtJustifiERP').val();
        $("#alertJustificacion").remove();
        if (justifi == "" || justifi.trim() === "") {
            $("#txtJustifiERP").focus().after("<span id='alertJustificacion' class='requerido-justificacion'>Escriba una justificación</span>");
            setTimeout(function () {
                $("#alertJustificacion").fadeOut('slow');
            }, 3000);
        } else {
            eliminarReporteERP();
        }
    });

    //Botón para cerrar el Dialog una vez que se elimino un reporte
    $("#btnEliminarReporte").click(function (e) {
        e.preventDefault();
        $("#dConfirmar").dialog("close");
    });

    //Botón para cerrar el Dialog una vez que se elimino una petición de ERP
    $("#btnEliminarERP").click(function (e) {
        e.preventDefault();
        $("#dConfirmar").dialog("close");
    });

    //Botón por medio del cual se realizan las validaciones de los campos para agregar un reporte, además de invocar la función para
    //agregar reportes
    $("#btnGenerar").click(function (e) {
        var asunto = $("#txtAsunto").val();
        var descripcion = $("#txtDescripcion").val();
        var fecha = $("#txtFechaPropuesta").val();
        var archivo = $("#nombreArchivo").val();
        var tipoReporte = $("#ddlTipoReporte").val();
        var prioridad = $("#ddlPrioridad").val();
        var tiposExcluidos = [5, 6, 7, 8, 9, 10, 11, 12, 13]
        if (tiposExcluidos.indexOf(tipoIncidencia) === -1) {
            idSistema = $("#ddlSistemaG").val();
            if (idTipoUsuario == 1) {
                idERPGrupo = $("#ddlERPGrupo").val();
            } else {
                idERPGrupo = $("#txtidGrupo").val();
            }
        } else {
            idSistema = 0;
            idERPGrupo = $("#txtidGrupo").val();
        }
        var grupo = $("#ddlERPGrupo").val();
        var tamanioAsunto = asunto.length;
        var txtGrupo = $("#txtidGrupo").val();

        var bandera = true;

        $(".validaCombo").each(function () {
            if ($(this).val() == null) {
                $(this).focus().after("<span class='requerido-campos'>Seleccione una opción</span>");
                setTimeout(function () {
                    $(".requerido-campos").fadeOut('slow');
                }, 3000);
                bandera = false;
            }
        });

        $(".validaCajas").each(function () {
            if ($(this).val() == "" || $(this).val().trim() === "") {
                $(this).focus().after("<span class='requerido-campos'>Seleccione una opción</span>");
                setTimeout(function () {
                    $(".requerido-campos").fadeOut('slow');
                }, 3000);
                bandera = false;
            }
        });

        if (fecha == "" || fecha.trim() === "") {
            $("#txtFechaPropuesta").after("<span class='requerido-campos'>Seleccione una opción</span>");
            setTimeout(function () {
                $(".requerido-campos").fadeOut('slow');
            }, 3000);
            bandera = false;
        }

        //if (archivo == "") {
        //    $("#btnEvidencia").after("<span id='alertArchivo' class='requerido-evidencia'>Campo requerido(Evidencia)</span>");
        //    setTimeout(function () {
        //        $("#alertArchivo").fadeOut('slow')
        //    }, 3000);
        //    bandera = false;
        //}
        console.log("Reporte generado.");
        if (bandera) {
            //            if (tipoReporte == 1) {
            evidenciaUploader.submit();
            //            }
            //            else {
            //                agregarReporte();
            //            }
        }
    });

    //Botón por medio del cual se realizan las validaciones de los campos para modificar un reporte, 
    //además de invocar la función para modificar reportes
    $("#btnModificar").click(function (e) {
        var asunto = $("#txtAsunto").val();
        var descripcion = $("#txtDescripcion").val();
        var fecha = $("#txtFechaPropuesta").val();
        var archivo = $("#nombreArchivo").val();
        var tipoReporte = $("#ddlTipoReporte").val();
        var prioridad = $("#ddlPrioridad").val();
        var grupo = $("#ddlERPGrupo").val();
        var sistema = $("#ddlSistemaG").val();
        var tamanioAsunto = asunto.length;
        var txtGrupo = $("#txtidGrupo").val();
        var idTipoUsuario = $('#txtTipoUsuario').val();
        var bandera = true;
        if (tipoIncidencia < 5 || tipoIncidencia == 14 || tipoIncidencia == 15 || tipoIncidencia == 16 || tipoIncidencia == 17 || tipoIncidencia == 18 || tipoIncidencia == 19 || tipoIncidencia == 20 || tipoIncidencia == 21 || tipoIncidencia == 22) {
            $(".validaCombo").each(function () {
                if ($(this).val() == null) {
                    $(this).focus().after("<span class='requerido-campos'>Seleccione una opción</span>");
                    setTimeout(function () {
                        $(".requerido-campos").fadeOut('slow');
                    }, 3000);
                    bandera = false;
                }
            });

        }
        //        if (idTipoUsuario == 1) {
        //        }
        $(".validaCajas").each(function () {
            if ($(this).val() == "" || $(this).val().trim() === "") {
                $(this).focus().after("<span class='requerido-campos'>Campo Requerido</span>");
                setTimeout(function () {
                    $(".requerido-campos").fadeOut('slow');
                }, 3000);
                bandera = false;
            }
        });

        if (fecha == "" || fecha.trim() === "") {
            $("#txtFechaPropuesta").after("<span class='requerido-campos'>Seleccione una fecha</span>");
            setTimeout(function () {
                $(".requerido-campos").fadeOut('slow');
            }, 3000);
        }

        //        if (archivo == "") {
        //            $("#btnEvidencia").after("<span id='alertArchivo' class='requerido-evidencia'>Campo requerido(Evidencia)</span>");
        //            setTimeout(function () {
        //                $("#alertArchivo").fadeOut('slow')
        //            }, 3000);
        //            bandera = false;
        //        } 

        if (bandera) {
            if (archivo != "") {
                evidenciaUploader.submit();
            } else {
                modificaReporte();
            }

            //            modificaReporte();
        }
        //        var archivo = $("#nombreArchivo").val();
        //        if (archivo != "") {
        //            $("#btnEvidencia").uploadify('upload');
        //        }
    });

    //Botón por medio del cual se realizan las validaciones de los campos para agregar una petición de ERP, a
    //además de invocar la función para agregar una peticion de ERP
    $("#btnGenerarERP").click(function (e) {
        var descripcion = $("#txtDescripcion").val();
        var fecha = $("#txtFechaPropuesta").val();
        var tipoReporte = $("#ddlTipoReporte").val();
        var prioridad = $("#ddlPrioridad").val();
        var grupo;
        var ng = $("#txtNombreGpo").val();
        if (ng != "") {
            grupo = ng;
        } else {
            grupo = $("#txtGrupo").val();
        }
        var idGrupo = $("#txtidGrupo").val();
        var archivo = $("#nombreArchivo").val();
        $("#txtFechaPropuesta").val();
        $("#alertDescripcion").remove();
        $("#alertFecha").remove();
        $("#alertArchivo").remove();

        $("#alertTipoReporte").remove();
        $("#alertPrioridad").remove();

        var sistemas = "";
        $(".sistemas:checkbox:checked").each(function () {
            //cada elemento seleccionado
            sistemas += $(this).val() + ",";
        });

        if ((descripcion == "" && fecha == "") || tipoReporte == null || prioridad == null || grupo == "" || sistemas == "" || archivo == "" || descripcion == "") {

            if (tipoReporte == null) {
                $("#ddlTipoReporte").focus().after("<span id='alertTipoReporte' class='requerido-reporte'>Seleccione una opción</span>");
                setTimeout(function () {
                    $("#alertTipoReporte").fadeOut('slow');
                }, 3000);
            }
            if (prioridad == null) {
                $("#ddlPrioridad").focus().after("<span id='alertPrioridad' class='requerido-reporte'>Campo requerido(Prioridad)</span>");
                setTimeout(function () {
                    $("#alertPrioridad").fadeOut('slow');
                }, 3000);
            }
            if (descripcion == "") {
                $("#txtDescripcion").focus().after("<span class='requerido-descripcion'>Campo requerido</span>");
                setTimeout(function () {
                    $(".requerido-descripcion").fadeOut('slow');
                }, 3000);
            }
            if (grupo == "") {
                $("#txtGrupo").focus().after("<span class='requerido-reporte'>Campo requerido</span>");
                setTimeout(function () {
                    $(".requerido-reporte").fadeOut('slow');
                }, 3000);
            }
            if (grupo != "" && sistemas == "") {
                $("#lblSiste").focus().after("<span id='alertTipoReporte' class='requerido-reporte'>Seleccione una opción</span>");
                setTimeout(function () {
                    $("#alertTipoReporte").fadeOut('slow');
                }, 3000);
            }
            //            if (descripcion == "") {
            //                $("#txtDescripcion").focus().after("<span id='alertDescripcion' class='requerido-descripcion'>Campo requerido(Descripción)</span>");
            //                setTimeout(function () {
            //                    $("#alertDescripcion").fadeOut('slow')
            //                }, 3000);
            //            }
            if (fecha == "") {
                $("#txtFechaPropuesta").after("<span id='alertFecha' class='requerido-campos'>Campo requerido(Fecha Propuesta)</span>");
                setTimeout(function () {
                    $("#alertFecha").fadeOut('slow')
                }, 3000);
            }

            if (archivo == "") {
                $("#btnEvidencia").after("<span id='alertArchivo' class='requerido-evidencia'>Campo requerido(Evidencia)</span>");
                setTimeout(function () {
                    $("#alertArchivo").fadeOut('slow')
                }, 3000);
            }

        } else {
            if (idGrupo == "" || idGrupo == 0) {
                $("#dlogAgregarGrupo").dialog("open");
            } else {
                evidenciaUploader.submit();
            }
        }
    });

    $("#btnCancelarGpo").click(function () {
        $("#dlogAgregarGrupo").dialog("close").fadeIn();
    });

    $("#btnAgregarGpo").click(function (e) {
        evidenciaUploader.submit();
        var grupo = $("#txtGrupo").val();
        var nombreGrupo = grupo.replace(/\s/g, "");
        agregarGrupo(nombreGrupo);
        idERPGrupo = $("#txtidGrupo").val();
        $("#dlogAgregarGrupo").dialog("close");
    });

    //Botón por medio del cual se realizan las validaciones de los campos para modificar una petición de ERP,
    //además de invocar la función para modificar una petición de ERP
    $("#btnModificarERP").click(function (e) {
        var descripcion = $("#txtDescripcion").val();
        var fecha = $("#txtFechaPropuesta").val();
        var tipoReporte = $("#ddlTipoReporte").val();
        var prioridad = $("#ddlPrioridad").val();
        var grupo;
        var ng = $("#txtNombreGpo").val();
        if (ng != "") {
            grupo = ng;
        } else {
            grupo = $("#txtGrupo").val();
        }
        var idGrupo = $("#txtidGrupo").val();
        var archivo = $("#nombreArchivo").val();
        $("#txtFechaPropuesta").val();
        $("#alertDescripcion").remove();
        $("#alertFecha").remove();
        $("#alertArchivo").remove();

        $("#alertTipoReporte").remove();
        $("#alertPrioridad").remove();
        var sistemas = "";
        $(".sistemas:checkbox:checked").each(function () {
            sistemas += $(this).val() + ",";
        });

        if (descripcion == "" || fecha == "" || tipoReporte == null || prioridad == null || grupo == "" || sistemas == "") {

            if (tipoReporte == null) {
                $("#ddlTipoReporte").focus().after("<span id='alertTipoReporte' class='requerido-reporte'>Seleccione una opción</span>");
                setTimeout(function () {
                    $("#alertTipoReporte").fadeOut('slow');
                }, 3000);
            }
            if (prioridad == null) {
                $("#ddlPrioridad").focus().after("<span id='alertPrioridad' class='requerido-reporte'>Campo requerido(Prioridad)</span>");
                setTimeout(function () {
                    $("#alertPrioridad").fadeOut('slow');
                }, 3000);
            }
            if (grupo == "") {
                $("#txtGrupo").focus().after("<span class='requerido-reporte'>Campo requerido(Prioridad)</span>");
                setTimeout(function () {
                    $(".requerido-reporte").fadeOut('slow');
                }, 3000);
            }
            if (grupo != "" && sistemas == "") {
                $("#lblSiste").focus().after("<span id='alertTipoReporte' class='requerido-reporte'>Seleccione una opción</span>");
                setTimeout(function () {
                    $("#alertTipoReporte").fadeOut('slow');
                }, 3000);
            }
            if (fecha == "") {
                $("#txtFechaPropuesta").after("<span id='alertFecha' class='requerido-campos'>Campo requerido(Fecha Propuesta)</span>");
                setTimeout(function () {
                    $("#alertFecha").fadeOut('slow')
                }, 3000);
            }
            if (descripcion == "") {
                $("#txtDescripcion").after("<span id='alertDescripcion' class='requerido-descripcion'>Campo requerido(Descripción)</span>");
                setTimeout(function () {
                    $("requerido-descripcion").fadeOut('slow')
                }, 3000);
            }

            //            if (archivo == "") {
            //                $("#btnEvidencia").after("<span id='alertArchivo' class='requerido-evidencia'>Campo requerido(Evidencia)</span>");
            //                setTimeout(function () {
            //                    $("#alertArchivo").fadeOut('slow')
            //                }, 3000);
            //            }

        } else {
            if (archivo != "") {
                evidenciaUploader.submit();
            } else {
                modificaReporteERP();
            }

            //            modificaReporteERP();
        }
    });

});


//Función que abre el formulario para agregar un nuevo reporte, además de crear un objeto uploadify para cargar la evidencia
//Además de invocar la función para Agregar una incidencia
function abrirFormulario() {
    var typeUser = $("#txtTipoUsuario").val();
    if (typeUser == 3) {
        $("#tipoIncidencia").toggle();
    } else if (typeUser == 2) {
        $("#divFormulario").toggle("swing");
    } else if (typeUser == 1) {
        $("#divFormulario").toggle("swing");
        getTipoReporteGrupo();
    }
    cargarGruposInicio("#txtidGrupo", "#txtGrupo", "obtenerGrupos");
    //    $("#lblTabla").show();
    $("#lblTablaLog").hide();
    $("#btnMostrar").show();
    $("#btnOcultar").hide();

    $("#btnGenerar").show();
    $("#btnModificar").hide();
    $("#btnModificarERP").hide();
    $("#lblEvidenciaM").empty();
    $(".gpo").hide();
    $(".advgpo").hide();
    $("#nombreArchivo").val(null);
    evidenciaUploader = new ss.SimpleUpload({
        button: 'btnEvidencia', // file upload button
        url: 'uploadEvidencia.ashx', // server side handler
        name: 'uploadfile', // upload parameter name       
        responseType: 'json',
        allowedExtensions: ['xlsx', 'xls', 'pdf', 'docx', 'zip', 'rar', 'msg', 'pptx', 'png', 'jpg', 'gif', 'xml', 'txt'],
        accept: ".xlsx, .xls, .pdf, .docx, .zip, .rar, .msg, .pptx, .png, .jpg, .gif, .xml, .txt",
        hoverClass: 'ui-state-hover',
        focusClass: 'ui-state-focus',
        disabledClass: 'ui-state-disabled',
        autoSubmit: false,
        onComplete: function (filename, response) {
            if (!response) {
                alert(filename + 'upload failed');
                return false;
            }
            var tipoReporte = $("#ddlTipoReporte").val();
            var idGrupo;
            if (tipoReporte == 3) {
                idGrupo = $("#txtidGrupo").val();
            } else {
                idGrupo = $("#ddlERPGrupo").val();
            }

            if (idGrupo != "" && idGrupo != 0) {
                if (tipoReporte == 3) {
                    agregarPeticion();
                } else {
                    agregarReporte();
                }
            }
        },
        onChange(filename, extension, uploadBTn, fileSize, file){
            $("#nombreArchivo").val(filename);
            $("#btnEvidencia").html(filename);
        }
    });
    limpiarCampos();
}

//Función que abre el formulario para modificar un reporte, y solo se muestre el botón que dará la opción para modificarlo
//además un objeto uploadify para cargar otra evidencia, encaso de que se desee modificar la que anteriormente se habia cargado
//Mandando llamar la funcion para mofificar Incidencia
function abrirFormularioModificar() {
    $("#divFormulario").slideDown("slow");
    $("#btnModificar").show();
    $("#btnModificarERP").hide();
    $("#btnGenerar").hide();
    $(".gpo").hide();
    $(".advgpo").hide();
    $("#nombreArchivo").val(null);
    $("#txtFechaPropuesta").datepicker({
        minDate: 0,
        dateFormat: 'yy-mm-dd'
    });
    evidenciaUploader = new ss.SimpleUpload({
        button: 'btnEvidencia', // file upload button
        url: 'uploadEvidencia.ashx', // server side handler
        name: 'uploadfile', // upload parameter name       
        responseType: 'json',
        allowedExtensions: ['xlsx', 'xls', 'pdf', 'docx', 'zip', 'rar', 'msg', 'pptx', 'png', 'jpg', 'gif', 'xml', 'txt'],
        accept: ".xlsx, .xls, .pdf, .docx, .zip, .rar, .msg, .pptx, .png, .jpg, .gif, .xml, .txt",
        hoverClass: 'ui-state-hover',
        focusClass: 'ui-state-focus',
        disabledClass: 'ui-state-disabled',
        autoSubmit: false,
        onComplete: function (filename, response) {
            if (!response) {
                alert(filename + 'upload failed');
                return false;
            }
            modificaReporte();
        },
        onChange(filename, extension, uploadBTn, fileSize, file){
            $("#nombreArchivo").val(filename);
            $("#btnEvidencia").html(filename);
        }
    });
}

//Función que abre el formulario para agregar una nueva peticion de ERP, además de crear un objeto uploadify para adjuntar
//el archivo que es requerido y llamando la funcion para agregar una peticion de ERP
function abrirFormularioModificarERP() {
    cargarGrupos("#txtidGrupo", "#txtGrupo", "obtenerGrupos");
    $("#divFormulario").slideDown("slow");
    $("#btnModificarERP").show();
    $("#btnModificar").hide();
    $("#btnGenerar").hide();
    var typeUser = $("#txtTipoUsuario").val();
    if (typeUser == 3) {
        $(".gpo").hide();
    } else {
        $(".gpo").hide();
    }

    $(".advgpo").hide();
    $("#nombreArchivo").val(null);
    $("#txtFechaPropuesta").datepicker({
        minDate: 0,
        dateFormat: 'yy-mm-dd'
    });
    evidenciaUploader = new ss.SimpleUpload({
        button: 'btnEvidencia', // file upload button
        url: 'uploadEvidencia.ashx', // server side handler
        name: 'uploadfile', // upload parameter name       
        responseType: 'json',
        allowedExtensions: ['xlsx', 'xls', 'pdf', 'docx', 'zip', 'rar', 'msg', 'pptx', 'png', 'jpg', 'gif', 'xml', 'txt'],
        accept: ".xlsx, .xls, .pdf, .docx, .zip, .rar, .msg, .pptx, .png, .jpg, .gif, .xml, .txt",
        hoverClass: 'ui-state-hover',
        focusClass: 'ui-state-focus',
        disabledClass: 'ui-state-disabled',
        autoSubmit: false,
        onComplete: function (filename, response) {
            if (!response) {
                alert(filename + 'upload failed');
                return false;
            }
            modificaReporteERP();
        },
        onChange(filename, extension, uploadBTn, fileSize, file){
            $("#nombreArchivo").val(filename);
            $("#btnEvidencia").html(filename);
        }
    });
}

//Función para cerrar el formulario de agregar reporte y llamar a la funcion de limpiar campos
function cerrarFormulario() {

    limpiarCampos();
    $(".check").prop("checked", "");
    $("#divFormulario").slideUp("slow");    
    $("#btnAgregar").show();
    $("#btnModificar").hide();
    $("#btnGenerar").hide();
    $("#btnEvidencia").html("Seleccionar evidencia");
    $(".gpo").hide();
    $(".advgpo").hide();
    $("#btn1").removeClass("azul");
    $("#btn5").removeClass("azul");
}

//Función para que se muestre el datepicker de la fecha propuesta
function mostrarCalendario() {
    $("#txtFechaPropuesta").datepicker("show");
}

//Función para limpiar los campos del formulario para agregar reporte
function limpiarCampos() {
    $("select#ddlTipoReporte").val(0);
    $("select#ddlPrioridad").val(0);
    $("select#ddlERPGrupo").val(0);
    $("select#ddlSistemaG").val(0);

    $("#txtAsunto").val("");
    $("#txtDescripcion").val("");
    $("#txtFechaPropuesta").val("");
    $("#txtGrupo").val("");
    $("#txtIdGrupo").val("");
    $("#nombreArchivo").val("");
    
    $("#btnCargaEvidencia").show();
    $("#ddlSistemaG").show();
    $("#ddlERPGrupo").show();
    $("#sistemasERP").hide();
    $("#txtGrupo").hide();
    $("#btnGenerarERP").hide();
    $("#btnGenerar").hide();
}

//Función para agregar un nuevo reporte
function agregarReporte() {
    block();
    
    var idPrioridad = $("#ddlPrioridad").val();
    var asunto = $("#txtAsunto").val();
    var descripcion = $("#txtDescripcion").val();
    var fechaPropuesta = $("#txtFechaPropuesta").val();
    var nombreArchivo = $("#nombreArchivo").val();
    var idTipoReporte = $("#ddlTipoReporte").val();
    var idTipoUsuario = $('#txtTipoUsuario').val();
    var ticketVinculado = $('#inputTicketVinculado').val();
    var idERPGrupo = 0;
    var idSistema = 0;
    var idArea = 0;

    var sPrint = 0;
    var puntos = 0;

    var tipoIncidencia = Number(idTipoReporte);

    if (tipoIncidencia < 5 || tipoIncidencia == 14 || tipoIncidencia == 15 || tipoIncidencia == 16 || tipoIncidencia == 17 || tipoIncidencia == 18 || tipoIncidencia == 19 || tipoIncidencia == 20 || tipoIncidencia == 21 || tipoIncidencia == 22) {
        idSistema = $("#ddlSistemaG").val();
        if (idTipoUsuario == 1) {
            idERPGrupo = $("#ddlERPGrupo").val();
            idArea = 0;
        } else {
            idERPGrupo = $("#txtidGrupo").val();
            idArea = $("#txtIdArea").val();
        }
    } else {
        idERPGrupo = $("#txtidGrupo").val();
        idArea = $("#txtIdArea").val();
        idSistema = 0;
    }

    if (idTipoReporte == 1 || idTipoReporte == 2 || idTipoReporte == 3 || idTipoReporte == 4 || idTipoReporte == 14 || idTipoReporte == 15 || idTipoReporte == 16 || idTipoReporte == 17 || idTipoReporte == 18 || idTipoReporte == 19 || idTipoReporte == 20 || idTipoReporte == 21 || idTipoReporte == 22) {
        var tipoUsuario = $("#txtTipoUsuario").val();
        if (tipoUsuario == 1) {
            idERPGrupo = $("#ddlERPGrupo").val();
            idSistema = $("#ddlSistemaG").val();
            idArea = 0;
        } else {
            idERPGrupo = $("#txtidGrupo").val();
            idSistema = $("#ddlSistemaG").val();
            idArea = 0;

        }
    }
    else if (idTipoReporte == 5 || idTipoReporte == 6 || idTipoReporte == 7 || idTipoReporte == 8 || idTipoReporte == 9) {
        idERPGrupo = $("#txtidGrupo").val();
        idArea = $("#txtIdArea").val();
        idSistema = 0;
    } else if (idTipoReporte == 25) {
        sPrint = $("#inputSprint").val();
        puntos = $("#inputPoints").val();
        idERPGrupo = $("#ddlERPGrupo").val();
        idSistema = $("#ddlSistemaG").val();
        idArea = 0;
    }

    $.ajax({
        url: "Reportes.aspx/agregarReporte",
        type: "POST",
        data: "{asunto:'" + asunto + "', descripcion:'" + descripcion + "', fechaPropuesta:'" + fechaPropuesta + "', idTipoReporte:" + idTipoReporte + ", idPrioridad: " + idPrioridad + ", idSistema:" + idSistema + ", idERPGrupo:" + idERPGrupo + ", nombreArchivo:'" + nombreArchivo + "', idUsuario: '" + txtidUser + "', idArea: " + idArea + ", ticketVinculado: '" + ticketVinculado + "', sprint: " + sPrint + ", puntos: " + puntos + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            if (data.d == true) {
                alertify.success("<span id='icon-25' class='success blanco' ></span>Se agregó correctamente la incidencia.");                
                getReportesCreados();
            } else {
                alertify.error("<span id='icon-25' class='error blanco'></span>No se agregó la incidencia");
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);

        }
    });
    cerrarFormulario();
}

function getEstatusUsuario() {
    $.ajax({
        url: "Reportes.aspx/getEstatusUsuario",
        type: "POST",
        data: "{}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            $.each(data.d, function (i, dato) {
                if (dato.idTipoUsuario == '1') {
                    $('#idTipoUsuario').val(1);
                } else if (dato.idTipoUsuario == '2') {
                    $('#idTipoUsuario').val(2);
                }
            });
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
}

//Función para visualizar la tabla de los reportes que han sido creados y que se encuentran en estatus 1 o 2(Creado/Asignado)
function getReportesCreados() {
    block();
    $("#lblTabla").show();
    $("#lblTablaLog").hide();
    $("#btnMostrar").show();
    $("#btnOcultar").hide();
    $.ajax({
        url: "Reportes.aspx/getReportesCreados",
        type: "POST",
        data: "{idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == "") {
                $("#divSinIncidencias").show();
                $("#btnGenerado").hide();
                $("#btnSoporte").hide();
                $("#btnPorValidar").hide();
                $("#btnTerminado").hide();
                $("#btnVerTodos").hide();
                $("#btnLeccionesAprendidas").hide();
                $("#lblTabla").hide();

            } else {
                $("#divSinIncidencias").hide();
                $("#btnGenerado").show();
                $("#btnSoporte").show();
                $("#btnPorValidar").show();
                $("#btnTerminado").show();
                $("#btnVerTodos").show();
                // $("#btnLeccionesAprendidas").show();
                $("#lblTabla").html(data.d);
                $("#tblReporteCreados").dataTable({
                    "pageLength": 50,
                    "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                    "stripeClasses": ['gray', 'white'],
                    columnDefs: [
                        { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                        { width: "10%", targets: [0, 2, 3, 4, 6, 7] },
                        { width: "11%", targets: [1] },
                        { width: "29%", targets: [5] },
                    ],
                });
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Función que trae solo los reportes que han sido generados y estan en el estatus 1
function getReportesGenerados() {
    block();
    $("#lblTabla").show();
    $("#lblTablaLog").hide();
    $("#btnMostrar").show();
    $("#btnOcultar").hide();
    $.ajax({
        url: "Reportes.aspx/getReportesGenerados",
        type: "POST",
        data: "{idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").html(data.d);
            $("#tblReporteCreados").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "10%", targets: [0, 2, 3, 4, 6, 7] },
                    { width: "11%", targets: [1] },
                    { width: "29%", targets: [5] },
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

//Función que trae solo los reportes que han sido asignados y estan en el estatus 2
function getReportesEnSoporte() {
    block();
    $("#lblTabla").show();
    $("#lblTablaLog").hide();
    $("#btnMostrar").show();
    $("#btnOcultar").hide();
    $.ajax({
        url: "Reportes.aspx/getReportesEnSoporte",
        type: "POST",
        data: "{idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").html(data.d);
            $("#tblReporteCreados").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "10%", targets: [0, 2, 3, 4, 6, 7] },
                    { width: "11%", targets: [1] },
                    { width: "29%", targets: [5] },
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

//Función para traer solo los reportes que se encuentran en el estatus 3(Por Validar)
function getReportesPorValidar() {
    block();
    $("#lblTabla").show();
    $("#lblTablaLog").hide();
    $("#btnMostrar").show();
    $("#btnOcultar").hide();
    $.ajax({
        url: "Reportes.aspx/getReportesValidar",
        type: "POST",
        data: "{idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").html(data.d);
            $("#tblReporteCreados").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "10%", targets: [0, 2, 3, 4, 6, 7] },
                    { width: "11%", targets: [1] },
                    { width: "29%", targets: [5] },
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

//Función que trae solamente los reportes que han sido terminados y estan en estatus 5
function getReportesTerminados() {
    block();
    $("#lblTabla").show();
    $("#lblTablaLog").hide();
    $("#btnMostrar").show();
    $("#btnOcultar").hide();
    var txtidUser = $("#txtidUser").val();
    $.ajax({
        url: "Reportes.aspx/getReportesTerminados",
        type: "POST",
        data: "{idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblTabla").html(data.d);
            $("#tblReporteCreados").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                columnDefs: [
                    { className: "txt-left col-reporte", targets: [0, 1, 2, 3, 4, 5, 6, 7] },
                    { width: "10%", targets: [0, 2, 3, 4, 6, 7] },
                    { width: "11%", targets: [1] },
                    { width: "29%", targets: [5] },
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


//Función que limpia la caja de texto en la cual se encuentra el nombre de la evidencia que se cargo, en caso de que el usuario 
//cancele la carga de la evidencia
function cancelar() {
    $("#nombreArchivo").val("");
}


//Función para consultar los Reportes que han sido creados 
function consultarReporte(idReporte) {
    block();
    cerrarFormulario();
    $("#btnOcultarDetalle").hide();
    $("#divEvidencia").show();
    $("#sistemasERPSeleccionados").hide();
    $("#lblSistema").show();
    $("#divAsunto").show();
    $("#fechaProp").show();
    $("#fechaResp").hide();
    $("#divRespuesta").hide();
    $("#divAcciones").show();
    $.ajax({
        url: "Reportes.aspx/consultarReporteCreado",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            obtenerResponsable(idReporte);
            $("#txtIdReporte").val(data.d.idReporte);
            $("#lblAsunto").text(data.d.asunto);
            $("#lblDescripcion").text(data.d.descripcion);

            var fechaReporte = data.d.fechaReporte.substring(6, 19);
            var fechaR = parseInt(fechaReporte);
            var fr = new Date(fechaR);
            var fecha = fr.toLocaleString();
            var anno = fr.getFullYear();
            var mes = fr.getMonth() + 1;
            mes = "" + mes;
            if (mes.length == 1) {
                mes = "0" + mes;
            }
            var dia = fr.getDate();
            dia = "" + dia;
            if (dia.length == 1) {
                dia = "0" + dia;
            }
            var fechaRep = anno + "-" + mes + "-" + dia;
            $("#lblFechaReporte").text(fechaRep);

            var fechaPropuesta = data.d.fechaPropuesta.substring(6, 19);
            var fechaP = parseInt(fechaPropuesta);
            var fp = new Date(fechaP);
            var fechaPro = fp.toLocaleString();
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
            var fechaPropu = anno + "-" + mes + "-" + dia;
            $("#lblFechaPropuesta").text(fechaPropu);

            if (data.d.evidencia == "" || data.d.evidencia == "NA") {
                $("#lblEvidencia").html('<label><span id="icon-47" class="warning rojo">No hay Evidencia</span></label>');
            } else {
                $("#lblEvidencia").empty();
                $("#lblEvidencia").append('<a href="../../Configuracion/Reportes/Evidencia/' + data.d.evidencia + '" target="_new"><span id="icon-47" class="evidencia verde">Evidencia</span></a>');
            }
            $("#lblFolio").text(data.d.folio);
            $("#lblTipoReporte").text(data.d.nombreTipoReporte);
            $("#lblSistema").html(data.d.nomSistema);
            $("#lblGrupo").text(data.d.nomGrupo);
        },
        error: function (xhr, status, error) {
            unBlock();
            alert('Error al consultar Reporte, idReporte: ' + idReporte);
        }
    });
    $("#frmReporte").dialog({
        autoOpen: true,
        modal: true,
        width: 800,
        height: 570,
        close: function (event, ui) {
            ocultarDetalleAcciones();
        }
    });
}


//Función para eliminar los reportes que han sido Creados
function eliminarReporteCreado() {
    block();
    var idReporte = $("#txtIdReporteE").val();
    $.ajax({
        url: "Reportes.aspx/eliminarReporteCreado",
        type: "POST",
        data: "{idReporte:" + idReporte + ", idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == true) {
                unBlock();
                alertify.success("<span id='icon-25' class='success blanco' ></span>Se eliminó correctamente la Incidencia.");

            } else {
                unBlock();
                alertify.error("<span id='icon-25' class='error blanco'></span>No se eliminó la Incidencia. ");
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert('Error al eliminar reporte, idReporte: ' + idReporte);
        }
    });
    //    getReportesCreados();
}

//Función por medio de la cual se llenan los datos para modificar un reporte que ha sido creado
function llenarDatosReporte(idReporte, idTipoReporte, idGrupo) {
    block();
    $("#txttipoIncidenciaCbo").val(idTipoReporte);
    var typeUser = $("#txtTipoUsuario").val();
    if (typeUser == 3) {
        getTipoReporte(idTipoReporte);
    } else if (typeUser == 2) {
        getTipoReporte(idTipoReporte);
    } else if (typeUser == 1) {
        $("#txtidGrupo").val(idGrupo);
        getTipoReporteGrupo();
    }

    if (idTipoReporte < 5 || idTipoReporte == 14 || idTipoReporte == 15 || idTipoReporte == 16 || idTipoReporte == 17 || idTipoReporte == 18 || idTipoReporte == 19 || idTipoReporte == 20 || idTipoReporte == 21 || idTipoReporte == 22) {
        ddlSistemas();
        $("#lblTipo").html("Modificar Incidencia");
    } else {
        $("#lblTipo").html("Modificar Consulta");
    }

    $("#area").show();
    cerrarFormulario();
    $("#txtGrupo").hide();
    $("#txtAsunto").show();
    $("#lblAsun").show();
    $.ajax({
        url: "Reportes.aspx/consultarReporteCreado",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            tipoIncidencia = idTipoReporte;
            //            if (typeUser != 2) {
            //                $("#txtidGrupo").val('');
            //            }

            $("#txtIdReporte").val(data.d.idReporte);
            $("#txtAsunto").val(data.d.asunto);
            $("#txtDescripcion").val(data.d.descripcion);

            var fechaPropuesta = data.d.fechaPropuesta.substring(6, 19);
            var fechaP = parseInt(fechaPropuesta);
            var fp = new Date(fechaP);
            var fechaPro = fp.toLocaleString();
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
            var fechaPropu = anno + "-" + mes + "-" + dia;
            $("#txtFechaPropuesta").val(fechaPropu);
            nomArchivoM = data.d.evidencia;
            $("#ddlERPGrupo").val(data.d.idERPGrupo);
            if (tipoIncidencia == 1 || tipoIncidencia == 2 || tipoIncidencia == 4 || tipoIncidencia == 14 || tipoIncidencia == 15 || tipoIncidencia == 16 || tipoIncidencia == 17 || tipoIncidencia == 18 || tipoIncidencia == 19 || tipoIncidencia == 20 || tipoIncidencia == 21 || tipoIncidencia == 22) {
                $("#nArea").hide();
                ddlSistemasModificar();
                setTimeout(function () {

                    $("#ddlSistemaG").val(data.d.idSistema);
                }, 1500
                        );
            } else {
                $("#nArea").show();
                $("#area").hide();
            }

            $("#ddlPrioridad").val(data.d.idPrioridad);
            if (nomArchivoM == "" || nomArchivoM == "NA") {
                $("#lblEvidenciaM").html('<label><span id="icon-47" class="warning rojo">No hay Evidencia</span></label>');
            } else {
                $("#lblEvidenciaM").empty();
                $("#lblEvidenciaM").show();
                $("#lblEvidenciaM").append('<a href="../../Configuracion/Reportes/Evidencia/' + nomArchivoM + '" target="_new"><span id="icon-47" class="evidencia verde">Evidencia</span></a>');
            }
            //                $("#ddlTipoReporte").val(data.d.idTipoReporte);

            unBlock();
            //            if (data.d.idTipoReporte == 2) {
            //                $("#btnCargaEvidencia").hide();
            //                $("#lblEvidenciaM").hide();
            //            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert('Error al consultar Reporte Creado, idReporte: ' + idReporte);
        }
    });
    abrirFormularioModificar();
}

//Función por medio de la cual se puede modificar un reporte que ha sido generado
function modificaReporte() {
    //    if (nombreArchivo != "") {
    //        $("#btnEvidencia").uploadify('upload');
    //    }
    var idTipoUsuario = $('#txtTipoUsuario').val();
    var idReporte = $("#txtIdReporte").val();
    var idTipoReporte = $("#ddlTipoReporte").val();
    var idPrioridad = $("#ddlPrioridad").val();
    var idSistema;
    var idERPGrupo;
    if (tipoIncidencia < 5 || tipoIncidencia == 14 || tipoIncidencia == 15 || tipoIncidencia == 16 || tipoIncidencia == 17 || tipoIncidencia == 18 || tipoIncidencia == 19 || tipoIncidencia == 20 || tipoIncidencia == 21 || tipoIncidencia == 22) {
        idSistema = $("#ddlSistemaG").val();
        if (idTipoUsuario == 1) {
            idERPGrupo = $("#ddlERPGrupo").val();
        } else {
            idERPGrupo = $("#txtidGrupo").val();
        }
    } else {
        idSistema = 0;
        idERPGrupo = $("#txtidGrupo").val();
    }

    var asunto = $("#txtAsunto").val();
    var descripcion = $("#txtDescripcion").val();
    var fechaPropuesta = $("#txtFechaPropuesta").val();
    var nombreArchivo = $("#nombreArchivo").val();
    if (nombreArchivo == "") {
        nombreArchivo = nomArchivoM;
    }
    var txtidUser = $("#txtidUser").val();
    block();
    $.ajax({
        url: "Reportes.aspx/modificarReporteCreado",
        type: "POST",
        data: "{idReporte: " + idReporte + ", asunto:'" + asunto + "', descripcion:'" + descripcion + "' , idTipoReporte: " + idTipoReporte + ", idPrioridad: " + idPrioridad + ", idSistema:" + idSistema + ", idERPGrupo:" + idERPGrupo + ", fechaPropuesta:'" + fechaPropuesta + "', nombreArchivo:'" + nombreArchivo + "', idUsuario: '" + txtidUser + "' }",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == true) {
                unBlock();
                nomArchivoM = "";
                alertify.success("<span id='icon-25' class='success blanco'></span>Se actualizó correctamente la incidencia.");
                cerrarFormulario();
                getReportesCreados();
                $("#btnModificar").hide();
                $("#btnGenerar").hide();
            } else {
                unBlock();
                alertify.error("<span id='icon-25' class='error blanco'></span>No se actualizó.");
            }
        },
        error: function (xhr, status, error) {
            alert(status);
            unBlock();
            alert('Error al modificar reporte, idReporte: ' + idReporte);
        }
    });
}

//Función con la cual se genera la tabla en la que se encuentra el log de las acciones que se han realizado con los reportes
//Altas, Bajas y Modificaciones
function getLogReportes() {
    block();

    $.ajax({
        url: "Reportes.aspx/getLogReportes",
        type: "POST",
        data: "{idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#btnMostrar").hide();
            $("#btnOcultar").show();
            $("#lblTabla").hide();
            $("#lblTablaLog").show();

            $("#lblTablaLog").html(data.d);
            $("#tblLogReporte").dataTable({
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white']
            });
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Función que permite ocultar el log, para ver la pantalla principal
function ocultarLog() {
    getReportesCreados();
    $("#btnMostrar").show();
    $("#btnOcultar").hide();
    $("#lblTablaLog").hide();
    $("#lblTabla").show();
}

//Función para eliminar una incidencia
function eliminar() {
    var idReporte = $("#txtIdReporteE").val();
    var justifi = $("#txtJustifi").val();
    block();
    $.ajax({
        url: "Reportes.aspx/eliminarReporte",
        type: "POST",
        data: "{idReporte:" + idReporte + ", idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == true) {
                unBlock();
                alertify.success("<span id='icon-25' class='success blanco'></span>Se eliminó correctamente la Incidencia.");
                $('#' + idReporte + '').closest('tr').remove().fadeOut(1000);
                $("#dlogEliminarReporte").dialog("close").fadeIn();

                guardarJustificacion(idReporte, justifi);
                $("#txtJustifi").val("");
                //                getReportesCreados();
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
    getReportesCreados();
}

//Función para confirmar si se desea eliminar el reporte que fue seleccionado.
function confirmarEliminado(idReporte) {
    block();
    cerrarFormulario();
    $.ajax({
        url: "Reportes.aspx/consultarReporteCreado",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            $("#txtIdReporteE").val(data.d.idReporte);
            /////////////////
            $("#dlogEliminarReporte").dialog("open");
            $('#btnNo').click(function () {
                $("#dlogEliminarReporte").dialog("close").fadeIn();
            });
            //            ////////////////
        },
        error: function (xhr, status, error) {
            unBlock
            alertify.error('Error al confirmar el Reporte , idReporte: ' + idReporte);
        }
    });
}

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
        url: "Reportes.aspx/detalleAccionesReporte",
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

//Función en la cual se cargan los elementos de la pantalla correspondientes al tipo de reporte que se seleccione
function tipoReporte() {
    block();
    $("#sistemasERP").empty();
    var idTipoReporte = $("#ddlTipoReporte").val();
    var idTipoUsuario = $("#txtTipoUsuario").val();
    if (idTipoReporte == 3) {
        if (idTipoUsuario == 1) {
            cargarGrupos("#txtidGrupo", "#txtGrupo", "obtenerGrupos");
            $("#txtGrupo").show();
            $("#txtGrupo").val("");
            $(".gpo").show();
            $("#divSistGpo").hide();
            $("#area").hide();
            $("#sistemasERP").show();
        } else if (idTipoUsuario == 3) {
            $(".advgpo").hide();
            getSistemasERP($("#txtidGrupo").val());
            $('#txtGrupo').text($("#txtidGrupo").val());
            $("#divSistGpo").hide();
            $("#nArea").hide();
            $("#area").hide();
            $("#sistemasERP").show();
            $("#txtGrupo").hide();
        }

        $("#btnCargaEvidencia").show();
        $("#lblAsun").hide();
        $("#txtAsunto").hide();
        $("#ddlSistemaG").hide();
        $("#lblSiste").hide();
        $("#ddlERPGrupo").hide();


        if (nomArchivoM == "") {
            $("#btnGenerar").hide();
            $("#btnGenerarERP").show();
        } else {
            $("#btnGenerarERP").hide();
            $("#btnGenerar").hide();
        }

    } else {
        $("#divSistGpo").show();
        $("#btnCargaEvidencia").show();
        $("#lblAsun").show();
        $("#txtAsunto").show();
        $("#ddlSistemaG").show();
        $("#ddlERPGrupo").show();
        $("#lblSiste").show();
        $("#sistemasERP").hide();
        $("#txtGrupo").hide();
        if (nomArchivoM == "") {
            $("#btnGenerar").show();
            $("#btnGenerarERP").hide();
        } else {
            $("#btnGenerar").hide();
            $("#btnGenerarERP").hide();
        }
        $(".gpo").hide();
        $(".advgpo").hide();
    }

    if (idTipoReporte == 25) {
        $("#divSprint").show();
        $("#divPoints").show();
    } else {
        $("#divSprint").hide();
        $("#divPoints").hide();
    }

    if (idTipoReporte == 1 || idTipoReporte == 2 || idTipoReporte == 4 || idTipoReporte == 14 || idTipoReporte == 15 || idTipoReporte == 16 || idTipoReporte == 17 || idTipoReporte == 18 || idTipoReporte == 19 || idTipoReporte == 20 || idTipoReporte == 21 || idTipoReporte == 22 || idTipoReporte == 25) {
        $("#area").show();
    }
    unBlock();
}

//Función para agregar un nuevo grupo, cuando este no se encuentra en la Base de Datos
function agregarGrupo(nombreGrupo) {

    nombreGrupo = nombreGrupo.toUpperCase();
    block();
    $.ajax({
        url: "Reportes.aspx/agregarGrupo",
        type: "POST",
        dataType: "JSON",
        data: "{nombreGrupo:'" + nombreGrupo + "', idUsuario: '" + txtidUser + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d) {
                alertify.success("<span id='icon-25' class='success blanco' ></span>Se agregó correctamente el grupo.");
                obtenerIdGrupo(nombreGrupo);
            } else {
                alertify.error("<span id='icon-25' class='error blanco' ></span>El grupo ya existe");
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error(error);
        }
    });
}


////Función para verificar si se generará un nuevo grupo o no
//function generarERP() {
//    block();
////    var idERPGrupo = $("#txtIdGrupo").val();
////    unBlock();
////    if (idERPGrupo == "" || idERPGrupo==0) {
////        $("#dlogAgregarGrupo").dialog("open");
////        $("#btnCancelarGpo").click(function () {
////            $("#dlogAgregarGrupo").dialog("close").fadeIn();
////        });
////        $("#btnAgregarGpo").click(function (e) {
////            var grupo = $("#txtGrupo").val();
////            var nombreGrupo = grupo.replace(/\s/g, "");
////            agregarGrupo(nombreGrupo);
////            idERPGrupo = $("#txtIdGrupo").val();
////            $("#dlogAgregarGrupo").dialog("close");
////        });
////    } else {
//    agregarPeticion();
//    unBlock();
////    }

//}

//Función para agregar una nueva petición de ERP
function agregarPeticion() {
    block();
    var idERPGrupo = $("#txtidGrupo").val();
    var idTipoReporte = $agregarPeticion("#ddlTipoReporte").val();
    var idPrioridad = $("#ddlPrioridad").val();
    var asunto = "Petición de ERP";
    var descripcion = $("#txtDescripcion").val();
    var fechaPropuesta = $("#txtFechaPropuesta").val();
    var nombreArchivo = $("#nombreArchivo").val();
    var sistemas = "";
    $(".sistemas:checkbox:checked").each(function () {
        //cada elemento seleccionado
        sistemas += $(this).val() + ",";
    });

    if (descripcion == "") {
        descripcion = "Petición de ERP";
    }
    $.ajax({
        url: "Reportes.aspx/agregarPeticion",
        type: "POST",
        data: "{asunto:'" + asunto + "', descripcion:'" + descripcion + "' , idTipoReporte: " + idTipoReporte + ", idPrioridad: " + idPrioridad + ", idERPGrupo:" + idERPGrupo + ", fechaPropuesta:'" + fechaPropuesta + "', sistemas:'" + sistemas + "', nombreArchivo:'" + nombreArchivo + "', idUsuario:'" + txtidUser + "' }",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            if (data.d == true) {
                alertify.success("<span id='icon-25' class='success blanco' ></span>Se agregó correctamente la petición de ERP");
                cerrarFormulario();
                getReportesCreados();
            } else {
                alertify.error("<span id='icon-25' class='error blanco'></span>No se agregó correctamente");
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error(error);
        }
    });
}

//Función para agregar estilos a los CheckBox de los sistemas, además completar la fecha de acuerdo a la cantidad de 
//sistemas que son solicitados
function seleccionado(idl, idchk) {
    var sistemas = "";
    $(".sistemas:checkbox:checked").each(function () {
        //cada elemento seleccionado
        sistemas += $(this).val() + ",";
    });
    if (sistemas == "") {
        var numSis = 1;
    } else {
        var arreglo = sistemas.split(",");
        numSis = (arreglo.length);
    }
    if ($(idchk).prop('checked')) {
        $("#txtCantidad").val(numSis);
        $('#' + idl).css("background-color", "#7FC44B !important;");
        //        $('#' + idl).css("color", "#FFFFFF !important;");
    } else {
        $("#txtCantidad").val(numSis);
        $('#' + idl).css("background-color", "#e6e8eb !important;");
        $('#' + idl).css("color", "#225069 !important;");
    }
    var fecha = new Date();
    var dd = fecha.getDate();
    var mm = fecha.getMonth() + 1; //January is 0!
    var yyyy = fecha.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    var fecha = yyyy + '-' + mm + '-' + dd;
    var Fecha = new Date();
    var sFecha = fecha || (Fecha.getFullYear() + "-" + (Fecha.getMonth() + 1) + "-" + Fecha.getDate());
    var sep = sFecha.indexOf('-') != -1 ? '-' : '-';
    var aFecha = sFecha.split(sep);
    var fecha = aFecha[0] + '-' + aFecha[1] + '-' + aFecha[2];
    fecha = new Date(fecha);
    fecha.setDate(fecha.getDate() + parseInt(numSis));
    var anno = fecha.getFullYear();
    var mes = fecha.getMonth() + 1;
    var dia = fecha.getDate();
    mes = (mes < 10) ? ("0" + mes) : mes;
    dia = (dia < 10) ? ("0" + dia) : dia;
    var fechaFinal = anno + sep + mes + sep + dia;
    $("#txtFechaPropuesta").val(fechaFinal);
    $("#txtFechaPropuesta").datepicker("option", "minDate", fechaFinal);
}

//Función Para obtener el ID de un grupo, cuando se conoce solamente su nombre
function obtenerIdGrupo(nombreGrupo) {
    $.ajax({
        url: "Reportes.aspx/obtenerIdGrupo",
        type: "POST",
        dataType: "JSON",
        data: "{nombreGrupo:'" + nombreGrupo + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#txtidGrupo").val(data.d);
            var idG = $("#txtidGrupo").val();
            agregarPeticion();
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error(error);
        }
    });
}

//Función para obtener el ID de un grupo que solicita un ERP
function obtenerIdGrupoERP(nombreGrupo) {
    $.ajax({
        url: "Reportes.aspx/obtenerIdGrupo",
        type: "POST",
        dataType: "JSON",
        data: "{nombreGrupo:'" + nombreGrupo + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#txtidGrupo").val(data.d);
            var idG = $("#txtidGrupo").val();
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error(error);
        }
    });
}

//Función para obtener los sistemas de un ERP(Con los que ya cuanta un grupo y la posibilidad de seleccionar más)
function getSistemasERP(idGrupo) {
    var idGrupo = $("#txtidGrupo").val();
    block();
    $.ajax({
        url: "Reportes.aspx/getSistemasERP",
        type: "POST",
        dataType: "JSON",
        data: "{idGrupo:" + idGrupo + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            $("#lblSiste").show();
            $("#sistemasERP").html("<label class='frmReporte'>Sistemas: </label><span id='alertTipoReporte' class='requerido-reporte' style='display: none;'>Seleccione una opción</span><br>" + data.d);
            $("#sistemasERP").show();
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Función para obtener el nombre de un Grupo
function obtenerNombreGrupo(idGrupo) {
    $.ajax({
        url: "Reportes.aspx/obtenerNombreGrupo",
        type: "POST",
        data: "{idGrupo:" + idGrupo + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            $("#txtGrupo").val(data.d);
            $("#txtGrupo").show();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error al obtener nombre de grupo");
        }
    });
}

//Función para llenar los datos de una peticion de ERP, cuando esta se desea modificar
function llenarDatosReporteERP(idReporte, idTipoReporte) {
    block();
    vmodificarERP = 1;
    $("#area").hide();
    $("#txttipoIncidenciaCbo").val(idTipoReporte);
    $("#btnGenerarERP").hide();
    $("#btnGenerar").hide();
    $("#lblTipo").html("Modificar Petición ERP");
    var typeUser = $("#txtTipoUsuario").val();
    if (typeUser == 3) {
        getTipoReporte(idTipoReporte);
        $("#nArea").hide();
        $("#txtGrupo").hide();
        $("#area").hide();
    } else if (typeUser == 2) {

    } else if (typeUser == 1) {
        getTipoReporteGrupo();
        $("#area").hide();

    }
    $("#ddlSistemaG").hide();
    $("#txtAsunto").hide();
    $("#lblAsun").hide();
    $("#ddlSistemaG").show();

    $.ajax({
        url: "Reportes.aspx/consultarReporteERP",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            
            $("#txtIdReporte").val(data.d.idReporte);
            $("#txtidGrupo").val(data.d.idERPGrupo);
            if (typeUser != 3) {
                obtenerNombreGrupo(data.d.idERPGrupo);
                $(".gpo").show();
            }
            //$("#txtAsunto").val(data.d.asunto);
            $("#txtDescripcion").val(data.d.descripcion);

            var fechaPropuesta = data.d.fechaPropuesta.substring(6, 19);
            var fechaP = parseInt(fechaPropuesta);
            var fp = new Date(fechaP);
            var fechaPro = fp.toLocaleString();
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
            var fechaPropu = anno + "-" + mes + "-" + dia;

            $("#txtFechaPropuesta").val(fechaPropu);
            nomArchivoM = data.d.evidencia;
            $("#ddlTipoReporte").val(data.d.idTipoReporte);
            $("#ddlSistemaG").val(data.d.idSistema);
            //            $("#ddlERPGrupo").val(data.d.idERPGrupo);
            if (nomArchivoM == "" || nomArchivoM == "NA") {
                $("#lblEvidenciaM").html('<label><span id="icon-47" class="warning rojo">No hay Evidencia</span></label>');
            } else {
                $("#lblEvidenciaM").empty();
                $("#lblEvidenciaM").show();
                $("#lblEvidenciaM").append('<a href="../../Configuracion/Reportes/Evidencia/' + nomArchivoM + '" target="_new"><span id="icon-47" class="evidencia verde">Evidencia</span></a>');
            }
            $("#ddlPrioridad").val(data.d.idPrioridad);
            var idReporte = data.d.idReporte;
            getSistemasERPModificar(idReporte);
            $("#txtIdERPGrupo").val(data.d.idERPGrupo);
            $("#ddlERPGrupo").hide();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert('Error al consultar Reporte Creado, idReporte: ' + idReporte);
        }
    });
    abrirFormularioModificarERP();
}

//Función para consultar los Reportes que han sido creados 
function consultarReporteERP(idReporte) {
    block();
    $("#btnOcultarDetalle").hide();
    $("#divEvidencia").show();
    $("#sistemasERPSeleccionados").show();
    $("#lblSistema").hide();
    $("#divAsunto").hide();
    $("#fechaProp").show();
    $("#fechaResp").hide();
    $("#divRespuesta").hide();
    $("#divAcciones").show();
    $.ajax({
        url: "Reportes.aspx/consultarReporteERP",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            obtenerResponsable(data.d.idReporte);
            $("#txtIdReporte").val(data.d.idReporte);
            $("#lblAsunto").text(data.d.asunto);
            $("#lblDescripcion").text(data.d.descripcion);
            var fechaReporte = data.d.fechaReporte.substring(6, 19);
            var fechaR = parseInt(fechaReporte);
            var fr = new Date(fechaR);
            var fecha = fr.toLocaleString();
            var anno = fr.getFullYear();
            var mes = fr.getMonth() + 1;
            mes = "" + mes;
            if (mes.length == 1) {
                mes = "0" + mes;
            }
            var dia = fr.getDate();
            dia = "" + dia;
            if (dia.length == 1) {
                dia = "0" + dia;
            }
            var fechaRep = anno + "-" + mes + "-" + dia;
            $("#lblFechaReporte").text(fechaRep);

            var fechaPropuesta = data.d.fechaPropuesta.substring(6, 19);
            var fechaP = parseInt(fechaPropuesta);
            var fp = new Date(fechaP);
            var fechaPro = fp.toLocaleString();
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
            var fechaPropu = anno + "-" + mes + "-" + dia;
            $("#lblFechaPropuesta").text(fechaPropu);
            $("#lblFolio").text(data.d.folio);
            $("#lblTipoReporte").text(data.d.nombreTipoReporte);
            // $("#lblSistema").text(data.d.nomSistema);
            if (data.d.evidencia == "" || data.d.evidencia == "NA") {
                $("#lblEvidencia").html('<label><span id="icon-47" class="warning rojo">No hay Evidencia</span></label>');
            } else {
                $("#lblEvidencia").empty();
                $("#lblEvidencia").append('<a href="../../Configuracion/Reportes/Evidencia/' + data.d.evidencia + '" target="_new"><span id="icon-47" class="evidencia verde">Evidencia</span></a>');
            }

            var idReporte = data.d.idReporte;
            getSistemasERPSeleccionados(idReporte);
            $("#lblGrupo").text(data.d.nomGrupo);
            //alert(data.d.area);
        },
        error: function (xhr, status, error) {
            unBlock
            alert('Error al consultar Reporte, idReporte: ' + idReporte);
        }
    });
    $("#frmReporte").dialog({
        autoOpen: true,
        modal: true,
        width: 800,
        height: 570,
        close: function (event, ui) {
            ocultarDetalleAcciones();
        }
    });
}

//Guardar la justifiación al dar clic en eliminar reporte
function guardarJustificacion(idReporte, justifi) {
    $.ajax({
        url: "Reportes.aspx/guardarJustificacion",
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
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en guardarJustificacion.");
        }
    });
}

//Función para obtener los sistemas que fueron seleccionados cuando se realizo la peticion de un ERP.
function getSistemasERPSeleccionados(idReporte) {
    block();
    $.ajax({
        url: "Reportes.aspx/getSistemasERPSeleccionados",
        type: "POST",
        dataType: "JSON",
        data: "{idReporte:" + idReporte + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            $("#sistemasERPSeleccionados").html(data.d);
            $("#sistemasERPSeleccionados").show();
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Función para obtener los sistemas que se seleccionaron en una peticion de ERP, cuando esta se convirtió en lecciones aprendidas
function getSistemasERPSeleccionadosLecciones(idReporte) {
    block();
    $.ajax({
        url: "Reportes.aspx/getSistemasERPSeleccionados",
        type: "POST",
        dataType: "JSON",
        data: "{idReporte:" + idReporte + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            $("#sistemasERPSeleccionadosL").html(data.d);
            $("#sistemasERPSeleccionadosL").show();
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Función para obtener los sistemas que fueron seleccionados en una petición de ERP, y teniendo la posibilidad para 
//poderlos modificar.
function getSistemasERPModificar(idReporte) {
    block();
    $.ajax({
        url: "Reportes.aspx/getSistemasERPModificar",
        type: "POST",
        dataType: "JSON",
        data: "{idReporte:" + idReporte + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            $("#sistemasERP").html(data.d);
            $("#sistemasERP").show();
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Función para mostrar la Tabla en la cual se encuentran las acciones realizadas con las incidencias
function mostrarLogReportes() {
    block();
    $("#btnMostrar").hide();
    $("#btnOcultar").hide();
    $.ajax({
        url: "Reportes.aspx/getLogReportes",
        type: "POST",
        data: "{idUsuario:'" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            if (data.d == "") {
                $("#btnMostrar").hide();
                $("#btnOcultar").hide();
            } else {
                $("#btnMostrar").show();
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Funcion para modificar uan peticion de ERP.
function modificaReporteERP() {
    block();
    var idTipoUsuario = $('#txtTipoUsuario').val();
    var idReporte = $("#txtIdReporte").val();
    var idTipoReporte = $("#ddlTipoReporte").val();
    var idPrioridad = $("#ddlPrioridad").val();
    var idERPGrupo = $("#txtidGrupo").val();
    var idERPGpoM = $("#txtIdERPGrupo").val();
    var asunto = "Petición de ERP";
    var descripcion = $("#txtDescripcion").val();
    var fechaPropuesta = $("#txtFechaPropuesta").val();
    if (tipoIncidencia < 5 || tipoIncidencia == 14 || tipoIncidencia == 15 || tipoIncidencia == 16 || tipoIncidencia == 17 || tipoIncidencia == 18 || tipoIncidencia == 19 || tipoIncidencia == 20 || tipoIncidencia == 21 || tipoIncidencia == 22) {
        idSistema = $("#ddlSistemaG").val();
        if (idTipoUsuario == 1) {
            idERPGrupo = $("#ddlERPGrupo").val();
        } else {
            idERPGrupo = $("#txtidGrupo").val();
        }
    } else {
        idSistema = 0;
        idERPGrupo = $("#txtidGrupo").val();
    }
    var nombreArchivo = $("#nombreArchivo").val();
    if (nombreArchivo == "") {
        nombreArchivo = nomArchivoM;
    }
    var sistemas = "";
    var nombreGrupo = $("#txtGrupo").val();
    var nomGrupo = nombreGrupo.toUpperCase();
    $(".sistemas:checkbox:checked").each(function () {
        //cada elemento seleccionado
        sistemas += $(this).val() + ",";
    });
    if (idERPGrupo == 0 || idERPGrupo == null) {
        idERPGrupo = idERPGpoM;
    }
    $.ajax({
        url: "Reportes.aspx/modificaReporteERP",
        type: "POST",
        data: "{idReporte: " + idReporte + ", asunto:'" + asunto + "', descripcion:'" + descripcion + "' , idTipoReporte: " + idTipoReporte + ", idPrioridad: " + idPrioridad + ", idERPGrupo:" + idERPGrupo + ", fechaPropuesta:'" + fechaPropuesta + "', sistemas:'" + sistemas + "', nombreArchivo:'" + nombreArchivo + "', nomGrupo:'" + nomGrupo + "', idUsuario: '" + txtidUser + "' }",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            nomArchivoM = "";
            if (data.d == true) {
                alertify.success("<span id='icon-25' class='success blanco' ></span>Se actualizó la petición de ERP.");
                cerrarFormulario();
                getReportesCreados();
            } else {
                alertify.error("<span id='icon-25' class='error blanco'></span>No se actualizó la incidencia.");
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });

}

//Función para confirmar el eliminado de una petición de ERP
function confirmarEliminadoERP(idReporte) {
    block();
    $.ajax({
        url: "Reportes.aspx/consultarReporteCreado",
        type: "POST",
        data: "{idReporte:" + idReporte + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            $("#txtIdReporteE").val(data.d.idReporte);
            /////////////////
            $("#dlogEliminarReporteERP").dialog("open");

            $('#btnNoERP').click(function () {
                $("#dlogEliminarReporteERP").dialog("close").fadeIn();
            });
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error('Error al confirmar el Reporte , idReporte: ' + idReporte);
        }
    });
}

//Función para eliminar una petición de ERP
function eliminarReporteERP() {
    block();
    var idReporte = $("#txtIdReporteE").val();
    var justifi = $("#txtJustifiERP").val();
    $.ajax({
        url: "Reportes.aspx/eliminarReporteERP",
        type: "POST",
        data: "{idReporte:" + idReporte + ", idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == true) {
                unBlock();

                alertify.success("<span id='icon-25' class='success blanco'></span>Se eliminó la Petición de ERP.");
                getReportesCreados();
                $('#' + idReporte + '').closest('tr').remove().fadeOut(1000);
                $("#dlogEliminarReporteERP").dialog("close").fadeIn();
                guardarJustificacion(idReporte, justifi);
                $("#txtJustifiERP").val("");

                //                alertify.success("<span id='icon-25' class='success blanco' ></span>Se eliminó correctamente la Incidencia.");
                //                getReportesCreados();
            } else {
                unBlock();
                alertify.error("<span id='icon-25' class='error blanco'></span>No se eliminó la Incidencia.");
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert('Error al eliminar reporte, idReporte: ' + idReporte);
        }
    });
}

//Función para cargar al incio para verificar que elementos mostrar al momento de abrir el dialog para generar una nueva incidencia
function iniciar() {
    block();
    $.ajax({
        url: "Reportes.aspx/iniciar",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d) {
                $("#lblGrupoUsuario").hide();
            } else {
                //$("#ddlGrupo").hide();
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error al validar el usuario");
        }
    });
}

//Función por medio de la cual se obtiene el responsable de un ERP
function obtenerResponsable(idReporte) {
    $.ajax({
        url: "Reportes.aspx/obtenerResponsable",
        type: "POST",
        dataType: "JSON",
        data: "{idReporte:" + idReporte + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#responsable").html(data.d);
            unBlock();
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
    var tipoPeticion = $('#ddlTipoReporte').val();
    var idGrupo;
    var grupo = $("#txtidGrupo").val();
    if (grupo != "") {
        idGrupo = grupo;
    } else {
        idGrupo = $("#ddlERPGrupo").val();
    }
    //    if ($("#txtidGrupo").val() == "" && tipoPeticion != 1 && tipoPeticion != 2 && tipoPeticion != 3 && tipoPeticion != 4) {
    //        idGrupo = $("#ddlERPGrupo").val();
    //    }
    //    else if (tipoPeticion == 1 || tipoPeticion == 2 || tipoPeticion == 3 || tipoPeticion == 4 || tipoPeticion == null) {
    //        idGrupo = $("#ddlERPGrupo").val();
    //    } else {

    //    }
    $.ajax({
        url: "Reportes.aspx/getDdlSistemasGrupo",
        type: "POST",
        dataType: "JSON",
        data: "{idGrupo:" + idGrupo + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#area").html(data.d);
            $("#area").show();
            $("#nArea").hide();
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Función para modificar los sistemas de una peticion de una incidencia
function ddlSistemasModificar() {
    block();
    $('#ddlSistemaG').empty();
    $("#ddlSistemaG").removeAttr("disabled");
    var idGrupo;
    var grupo = $("#txtidGrupo").val();
    if (grupo != "") {
        idGrupo = grupo;

    } else {
        idGrupo = $("#ddlERPGrupo").val()
    }

    //    if ($("#ddlERPGrupo").val() !== "") {
    //        idGrupo = $("#ddlERPGrupo").val();
    //    }
    //    else {
    //        idGrupo = $("#txtidGrupo").val();
    //    }
    $.ajax({
        url: "Reportes.aspx/getDdlSistemasGrupo",
        type: "POST",
        dataType: "JSON",
        data: "{idGrupo:" + idGrupo + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            $("#area").html(data.d);
            $("#area").show();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Función para obtener todos los sistemas que puede contener un ERP 
function getSistemasTodosERP() {
    block();
    $.ajax({
        url: "Reportes.aspx/getSistemasTodosERP",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            unBlock();
            $("#lblSiste").show();
            $("#sistemasERP").html(data.d);
            $("#sistemasERP").show();
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Función para obtener la tabla en la cual se encuentra el listado de las lecciones aprendidas
function getLeccionesAprendidas() {
    block();

    $.ajax({
        url: "Reportes.aspx/getLeccionesAprendidas",
        type: "POST",
        data: "{idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == "") {
                $("#btnLeccionesAprendidas").hide();
            } else {
                $("#btnLeccionesAprendidas").show();
                $("#lblLeccionesAprendidas").html(data.d);
                $("#tblLeccionesAprendidas").dataTable({
                    "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                    "stripeClasses": ['gray', 'white'],
                    "aoColumnDefs": [
                                      { "sClass": "txt-left", "aTargets": [1] }
                                    ]
                });
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

//Función para obtener el detalle de las lecciones aprendidas
function detalleLecciones(idLecciones) {
    block();
    $("#fechaRespL").show();
    $("#lblSistema").show();
    $("#divDetalleL").toggle("slow");
    $("#divTablaLecciones").toggle("slow");
    $.ajax({
        url: "Reportes.aspx/consultarLeccionesAprendidas",
        type: "POST",
        data: "{idLecciones:" + idLecciones + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            $("#txtIdReporte").val(data.d.idReporte);
            $("#lblAsuntoL").text(data.d.asunto);
            $("#lblDescripcionL").text(data.d.descripcion);

            var fechaReporte = data.d.fechaReporte.substring(6, 19);
            var fechaR = parseInt(fechaReporte);
            var fr = new Date(fechaR);
            var fecha = fr.toLocaleString();
            var anno = fr.getFullYear();
            var mes = fr.getMonth() + 1;
            mes = "" + mes;
            if (mes.length == 1) {
                mes = "0" + mes;
            }
            var dia = fr.getDate();
            dia = "" + dia;
            if (dia.length == 1) {
                dia = "0" + dia;
            }
            var fechaRep = anno + "-" + mes + "-" + dia;
            $("#lblFechaReporteL").text(fechaRep);

            var fechaPropuesta = data.d.fechaRespuesta.substring(6, 19);
            var fechaP = parseInt(fechaPropuesta);
            var fp = new Date(fechaP);
            var fechaPro = fp.toLocaleString();
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
            var fechaResp = anno + "-" + mes + "-" + dia;
            $("#lblFechaRespuestaL").text(fechaResp);

            if (data.d.evidencia == "" || data.d.evidencia == null) {
                $("#lblEvidenciaL").html('<label><span id="icon-47" class="warning rojo">No hay Evidencia</span></label>');
            } else {
                $("#lblEvidenciaL").empty();
                $("#lblEvidenciaL").append('<a href="../../Configuracion/Reportes/Evidencia/' + data.d.evidencia + '" target="_new"><span id="icon-47" class="evidencia verde">Evidencia</span></a>');
            }
            if (data.d.comentario == "" || data.d.comentario == null) {
                $("#lblRespuestaLeccionesL").html('<label><span id="icon-47" class="warning rojo">No hay Respuesta</span></label>');
            } else {
                $("#lblRespuestaLeccionesL").text(data.d.comentario);
            }
            $("#lblFolioL").text(data.d.folio);
            $("#lblTipoReporteL").text(data.d.nombreTipoReporte);
            $("#lblSistemaL").text(data.d.nomSistema);
            $("#lblGrupoL").text(data.d.nomGrupo);
            $("#responsableL").html("<span id='icon-25' class='usuario_loggeado verde'></span> <label id='frm'>Responsable: </label> <br /> <div id='responsable' class='listadoConsultaResponsable clear'>" + data.d.nombreCompleto + "<div style='float: right'></div></div>");
            if (data.d.idTipoReporte == 3) {
                $("#lblSistemaL").hide();
                var idReporte = data.d.idReporte;
                getSistemasERPSeleccionadosLecciones(idReporte);
            } else {
                if ($("#lblSistemaL").text() != 'null') {
                    $("#lblSistemaL").show();
                    $("#sistemasERPSeleccionadosL").hide();
                } else {
                    $("#lblSistemaL").empty();
                    $("#lblSistemaL").text('N/A')
                    $("#lblSistemaL").show();
                }
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert('Error al consultar Reporte, idReporte: ' + idLecciones);
        }
    });
}

//Función para regresar a la tabla de lecciones aprendidas, cuando se encuentra en el detalle de una en especifico.
function regresarLecciones() {
    $("#divTablaLecciones").toggle("slow");
    $("#divDetalleL").hide();
}

function verFormulario(tipoIncidenciaP) {
    nomArchivoM = "";
    tipoIncidencia = tipoIncidenciaP;
    $("#area").hide();
    $("#nArea").show();
    var n = document.getElementById("btn" + tipoIncidenciaP + "").innerHTML;
    $("#lblTipo").html(n);
    var typeUser = $("#txtTipoUsuario").val();
    if (typeUser == 3) {
        if (tipoIncidenciaP == 1) {
            ddlSistemas();
        }
        getTipoReporte(tipoIncidenciaP);
    } else if (typeUser == 2) {
        getTipoReporte(tipoIncidenciaP);
    } else if (typeUser == 1) {
        getTipoReporteGrupo();
    }

    if (tipoIncidencia == 1 && typeUser != 1) {
        $("#btn" + tipoIncidenciaP + "").addClass("azul");
        $("#btn5").removeClass("azul");
        $("#ddlSistemaG").addClass('validaCombo');
    } else {
        $("#btn" + tipoIncidenciaP + "").addClass("azul");
        $("#btn1").removeClass("azul");
        $("#ddlSistemaG").removeClass('validaCombo');
        //        getTipoReporteGrupo();
    }
    //    $("#btnAgregar").hide();
    $("#tipoIncidencia").hide();
    $("#divFormulario").show();
    //$("#form").toggle("swing");
    cargarGruposInicio("#txtidGrupo", "#txtGrupo", "obtenerGrupos");
    //    $("#lblTabla").show();
    $("#lblTablaLog").hide();
    $("#btnMostrar").show();
    $("#btnOcultar").hide();
    //    $("#divFormulario").toggle(1500, "swing");

    $("#btnGenerar").show();
    $("#btnModificar").hide();
    $("#btnModificarERP").hide();
    $("#lblEvidenciaM").empty();
    $(".gpo").hide();
    $(".advgpo").hide();
    $("#nombreArchivo").val(null);
    evidenciaUploader = new ss.SimpleUpload({
        button: 'btnEvidencia', // file upload button
        url: 'uploadEvidencia.ashx', // server side handler
        name: 'uploadfile', // upload parameter name       
        responseType: 'json',
        allowedExtensions: ['xlsx', 'xls', 'pdf', 'docx', 'zip', 'rar', 'msg', 'pptx', 'png', 'jpg', 'gif', 'xml', 'txt'],
        accept: ".xlsx, .xls, .pdf, .docx, .zip, .rar, .msg, .pptx, .png, .jpg, .gif, .xml, .txt",
        hoverClass: 'ui-state-hover',
        focusClass: 'ui-state-focus',
        disabledClass: 'ui-state-disabled',
        autoSubmit: false,
        onComplete: function (filename, response) {
            if (!response) {
                alert(filename + 'upload failed');
                return false;
            }
            var tipoReporte = $("#ddlTipoReporte").val();
            var idGrupo;
            if (tipoReporte == 3) {
                idGrupo = $("#txtidGrupo").val();
            } else {
                idGrupo = $("#ddlERPGrupo").val();
            }
            if (idGrupo != "" && idGrupo != 0) {
                if (tipoReporte == 3) {
                    agregarPeticion();
                } else {
                    agregarReporte();
                }
            }
        },
        onChange(filename, extension, uploadBTn, fileSize, file){
            $("#nombreArchivo").val(filename);
            $("#btnEvidencia").html(filename);
        }
    });
    limpiarCampos();
    $("#btnGenerar").show();
}

function getTipoReporte(tipoReporte) {
    var idGrupo = $("#txtidGrupo").val();
    block();
    $.ajax({
        url: "Reportes.aspx/cboTipoReporte",
        type: "POST",
        data: "{tipoReporte: '" + tipoReporte + "', idGrupo: '" + idGrupo + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#ddlTReporte").html(data.d[0]);
            //            if (data.d[1] != "") {
            //                $("#divSistGpo").html(data.d[1]);
            //                $("#divSistGpo").hide();
            //                $("#area").hide();
            //            }
            var tipoI = $("#txttipoIncidenciaCbo").val();
            if (tipoI != "") {
                $("#ddlTipoReporte").val(tipoI);
                $("#txttipoIncidenciaCbo").val('');
            }
            var typeUser = $("#txtTipoUsuario").val();
            if (typeUser == 3) {
                ddlTipoReporte.options[3] = null;
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

function getTipoReporteGrupo() {
    var idGrupo = $("#txtidGrupo").val();
    block();
    $.ajax({
        url: "Reportes.aspx/cboTipoReporteGrupo",
        type: "POST",
        data: "{}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#ddlTReporte").html(data.d[0]);
            var tipoI = $("#txttipoIncidenciaCbo").val();
            if (tipoI != "") {
                $("#ddlTipoReporte").val(tipoI);
                $("#txttipoIncidenciaCbo").val('');
                if (tipoI == 3) {
                    ddlTipoReporte.options[1] = null;
                    ddlTipoReporte.options[1] = null;
                    ddlTipoReporte.options[2] = null;
                }
            }
            if (tipoI == 1 || tipoI == 2 || tipoI == 4) {
                ddlTipoReporte.options[3] = null;
            }
            if (vmodificarERP == 0) {
                $("#grupo").html(data.d[1]);
            } else {
                $("#grupo").html("<label class='frmReporte'>Grupo:</label><br />");
                vmodificarERP = 0;
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

function cerrarTI() {
    $(".check").prop("checked", "");
    $("#tipoIncidencia").slideUp("slow");
}