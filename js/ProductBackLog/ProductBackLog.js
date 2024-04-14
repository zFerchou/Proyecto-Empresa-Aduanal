var txtidUser;
var typeUser;
var vmodificarERP = 0;
var evidenciaUploader; //variable para asignarle la funcion de subir evidencia
var excelUploader; //variable para asignarle la funcion de subir excel
var Points = false; //variable para actualizar/dejar estaticos los storyPoints
var marcados = false; //variable para mercar/desmarcar checkboxs
$(document).ready(function () {

    var typeUser = $("#txtTipoUsuario").val();
    if (typeUser == 3) {
        $('#btnAdd').html('<a id="btn1" href="#" class="btn verde shadow2 borde-blanco" ' +
            'onclick="javascript:verFormulario(1);">Agregar Historia</a>' +
            '<a id="btn5" href="#" class="btn verde shadow2 borde-blanco" ' +
            'onclick="javascript:verFormulario(5);">Agregar Consulta</a>');
    } else if (typeUser == 2) {
        $('#btnAdd').html('<a id="btn5" href="#" class="btn verde shadow2 borde-blanco" ' +
            'onclick="javascript:verFormulario(5);">Agregar Consulta</a>');
    } else if (typeUser == 1) {
        $('#btnAdd').html('<a id="btn1" href="#" class="btn verde shadow2 borde-blanco" ' +
            'onclick="javascript:verFormulario(1);">Agregar Historia</a>' +
            '<a href="#" class="btn verde shadow2 borde-blanco" ');
    }

    typeUser = $("#txtTipoUsuario").val();
    txtidUser = $("#txtidUser").val();
    start();

   
    $("#btnGenerar").click(function (e) {
        
        var bandera = true;
        var archivo = $("#nombreArchivo").val();
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
                $(this).focus().after("<span class='requerido-campos'>Campo Requerido</span>");
                setTimeout(function () {
                    $(".requerido-campos").fadeOut('slow');
                }, 3000);
                bandera = false;
            }
        });


        

        // Verificar si el elemento con ID ddlTipoImpacto es nulo
        var ddlTipoImpacto = $("#ddlTipoImpacto");
        if (ddlTipoImpacto.length === 0 || ddlTipoImpacto.val() == null) {
            // Buscar el div padre
            var divPadre = ddlTipoImpacto.closest('div');

            // Buscar un hijo con la clase .select2-container dentro del div padre
            var select2Container = divPadre.find('.select2-container');

            // Realizar la verificación en el elemento encontrado
            if (select2Container.length > 0) {
                select2Container.focus().after("<span class='requerido-campos'>Seleccione una opción</span>");
                setTimeout(function () {
                    $(".requerido-campos").fadeOut('slow');
                }, 3000);
                
            }
            bandera = false;
        }

        if (bandera) {
         
            if (archivo != "") {
                evidenciaUploader.submit();
            } else {
                agregarHistorias();
            }
            
        }
    });


    $("#btnModificar").click(function (e) {
        var bandera = true;
        var archivo = $("#nombreArchivo").val();
        var numeroTexto = $("#txtStoryPoints").val();

        $(".validaCajas").each(function () {
            if ($(this).val() == "" || $(this).val().trim() === "") {
                $(this).focus().after("<span class='requerido-campos'>Campo Requerido</span>");
                setTimeout(function () {
                    $(".requerido-campos").fadeOut('slow');
                }, 3000);
                bandera = false;
            }
        });
        $(".validaCombo").each(function () {
            if ($(this).val() == null) {
                $(this).focus().after("<span class='requerido-campos'>Seleccione una opción</span>");
                setTimeout(function () {
                    $(".requerido-campos").fadeOut('slow');
                }, 3000);
                bandera = false;
            }
        });

        // Verificar si el elemento con ID ddlTipoImpacto es nulo
        var ddlTipoImpacto = $("#ddlTipoImpacto");
        if (ddlTipoImpacto.length === 0 || ddlTipoImpacto.val() == null) {
            // Buscar el div padre
            var divPadre = ddlTipoImpacto.closest('div');

            // Buscar un hijo con la clase .select2-container dentro del div padre
            var select2Container = divPadre.find('.select2-container');

            // Realizar la verificación en el elemento encontrado
            if (select2Container.length > 0) {
                select2Container.focus().after("<span class='requerido-campos'>Seleccione una opción</span>");
                setTimeout(function () {
                    $(".requerido-campos").fadeOut('slow');
                }, 3000);

            }
            bandera = false;
        }

        if (numeroTexto.length == 0 && Points || numeroTexto === null && Points) {
            numeroTexto.focus().after("<span class='requerido-campos'>Campo Requerido</span>");
            bandera = false;
        } else {
            var numero = parseInt(numeroTexto);
            if (isNaN(numero) && numero < 1 && numero > 13 && Points) {
                numeroTexto.focus().after("Por favor, ingresa un número válido del 1 al 13.");
                bandera = false;
            }
        }

        if (bandera) {
            if (archivo != "") {
                evidenciaUploader.submit();
            } else {
                modificaHistoria();
            }
        }
    });

    $("#txtidGrupo").val(1);
    ddlSistemas();

    aplicarMultiSelect('ddlTipoImpacto');

    $("#dlogSeleccionarSprint").hide();
    $("#btnCerrarDlogSeleccionarSprint").click(function (e) {
        cerrarDialogSprint();
    });
    $("#btnAsingarSprint").click(function (e) {
        var idSprint = $("#ddlSeleccionaSprint").val();
        if (idSprint === "" || idSprint === null) {
            $("#ddlSeleccionaSprint").focus().after("<span class='requerido-campos'>Seleccione una opción</span>");
            setTimeout(function () {
                $(".requerido-campos").fadeOut('slow');
            }, 3000);
        }
        else {
           
            // Seleccionar todos los checkboxes con la clase "validar" que están seleccionados
            var historiasSeleccionadas = $('.validar:checked').map(function () {
                return this.value;
            }).get();
            // Almacenar los valores en una variable separados por comas
            var data = historiasSeleccionadas.join(',');
            if (data === "" || data === null) {
                alertify.error("<span id='icon-25' class='error blanco'></span>Selecciona almenos una Historia de Usuario");
            } else {
                asignarSprint(idSprint, data);
            }

            
        }
        
    });
   
});


function start(estatus=0) {
    block();
    $.ajax({
        url: "ProductBackLog.aspx/start",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblEtiquetas").html(data.d[0]);
            $('.pdtes').click(function () {
                $('.pdtes').removeClass('divSelected');
                $(this).addClass('divSelected');
            });
            if (estatus == 3) {
                // Luego de completar start(), realiza las siguientes acciones
                $("#rPuntuado").click();
                $("#rPuntuado").addClass("divSelected");
            }
            if (estatus == 4) {
                // Luego de completar start(), realiza las siguientes acciones
                $("#rSprint").click();
                $("#rSprint").addClass("divSelected");
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

 function getTablaHistorias(idEstatus) {
    block();
    $("#lblTabla").show();
    $("#lblTablaLog").hide();
    $("#btnMostrar").show();
     $("#btnOcultar").hide();
     $("#nombreExcel").val(null);
     if ($("#divExcel").length) {
         $("#divExcel").remove();
     }
    $.ajax({
        url: "ProductBackLog.aspx/getReportesCreados",
        type: "POST",
        data: "{idUsuario: '" + txtidUser + "', idEstatus: "+idEstatus+"}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            
            $("#lblTabla").html(data.d);
            $("#tblProductBackLog").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "order": [],
                "stripeClasses": ['gray', 'white'],
                "aoColumnDefs": [
                    { "sClass": "txt-left", "aTargets": [1, 2, 3] }
                ]
            });
            if ($("#btnLeerExcel").length) {
                excelUploader = new ss.SimpleUpload({
                    button: 'btnLeerExcel', // file upload button
                    url: 'uploadReporte.ashx', // server side handler
                    name: 'uploadfile', // upload parameter name       
                    responseType: 'json',
                    allowedExtensions: ['xlsx', 'xls'],
                    accept: ".xlsx, .xls",
                    hoverClass: 'ui-state-hover',
                    focusClass: 'ui-state-focus',
                    disabledClass: 'ui-state-disabled',
                    autoSubmit: true,
                    onComplete: function (filename, response) {
                        if (!response) {
                            alert(filename + 'upload failed');
                            return false;
                        }
                        console.log("Archivo cargado:", filename);
                        $("#nombreExcel").val(filename);
                        $("#btnLeerExcel").html(filename);
                        leerExcel(filename);
                    }

                });

            }
            if (idEstatus == 3) {
                marcados = false;
                Points = true;
                // Evento click para el botón
                $('#btnSeleccionarSprint').click(function () {
                    // Verificar si al menos un checkbox en la clase 'validar' está marcado
                    if ($('.validar:checkbox').is(':checked')) {
                        mostrarDialogSprint();
                    } else {
                        alertify.error("<span id='icon-25' class='error blanco'></span>Selecciona almenos una Historia de Usuario");
                    }
                    
                });

                // Función que se ejecuta al hacer clic en el botón
                $('#btnSeleccionarTodos').on('click', function () {
                    // Cambiar el estado de marcados
                    marcados = !marcados;
                    // Obtener todos los checkboxes con la clase 'validar' y establecer su propiedad 'checked'
                    $('.validar').prop('checked', marcados);
                    // Cambiar el texto del botón según el estado actual
                    if (marcados) {
                        $(this).text('Desmarcar Todos');
                    } else {
                        $(this).text('Seleccionar Todos');
                    }
                });
                
            } else { Points = false; }

            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });

}

function verFormulario(tipoIncidenciaP) {
    nomArchivoM = "";
    tipoIncidencia = tipoIncidenciaP;
    $("#area").hide();
    $("#nArea").show();
    $("#estatusH").hide();
    var n = document.getElementById("btn" + tipoIncidenciaP + "").innerHTML;
    $("#lblTipo").html(n);
    var typeUser = $("#txtTipoUsuario").val();
    getTipoReporteGrupo();

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
    limpiarCampos();
    $("#btnGenerar").show();
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
            if (tipoIncidenciaP == 1) {
                agregarHistorias();
            }
        },
        onChange(filename, extension, uploadBTn, fileSize, file) {
            $("#nombreArchivo").val(filename);
            $("#btnEvidencia").html(filename);
        }
    });
}

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

function limpiarCampos() {
    $("select#ddlPrioridad").val(0);
    $("select#ddlERPGrupo").val(0);
    $("select#ddlSistemaG").val(0);
    $('#ddlTipoImpacto').val([]).trigger('change');
    $("#txtEpica").val("");
    $("#txtDescripcion").val("");
    $("#txtHistoria").val("");
    $("#txtCriterios").val("");
    $("#txtRiesgos").val("");

    $("#ddlSistemaG").show();
    $("#ddlERPGrupo").show();
    $("#sistemasERP").hide();
    $("#txtGrupo").hide();
    $("#btnGenerarERP").hide();
    $("#btnGenerar").hide();
}

function getTipoReporteGrupo() {
    var idGrupo = $("#txtidGrupo").val();

    block();
    $.ajax({
        url: "ProductBacklog.aspx/cboTipoReporteGrupo",
        type: "POST",
        data: "{}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
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

function getTipoImpacto(idHistoria) {
    block();
    $.ajax({
        url: "ProductBacklog.aspx/TipoImpacto",
        type: "POST",
        data: "{id:" + idHistoria +"}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('#ddlTipoImpacto').val(data.d).trigger('change');
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

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
    $.ajax({
        url: "ProductBacklog.aspx/getDdlSistemasGrupo",
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


function agregarHistorias() {
    block();

    var idPrioridad = $("#ddlPrioridad").val();
    var idSistema = 0;

    var epica = $("#txtEpica").val();
    var descripcion = $("#txtDescripcion").val();
    var Historia = $("#txtHistoria").val();
    var criterios = $("#txtCriterios").val();
    var riesgos = $("#txtRiesgos").val();
    var evidencia = $("#nombreArchivo").val() === "" ? "NULL" : $("#nombreArchivo").val();
    idERPGrupo = $('select[id="ddlERPGrupo"] option:selected').text();
    idSistema =  $("#ddlSistemaG").val();

    var tipoImpacto = $('#ddlTipoImpacto').val();

    var dataToSend = {
        epica: epica,
        descripcion: descripcion,
        historia: Historia,
        criterios: criterios,
        idPrioridad: idPrioridad,
        idSistema: idSistema,
        idERPGrupo: idERPGrupo,
        riesgos: riesgos,
        evidencia: evidencia,
        tipoImpacto: tipoImpacto,  // Asumiendo que tipoImpacto es un array
        idUsuario: txtidUser
    };
    $.ajax({
        url: "ProductBacklog.aspx/agregarHistoria",
        type: "POST",
        data: JSON.stringify(dataToSend),
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            if (data.d == true) {
                alertify.success("<span id='icon-25' class='success blanco' ></span>Se agregó correctamente la Historia.");
                start();
                getTablaHistorias(1);
            } else {
                alertify.error("<span id='icon-25' class='error blanco'></span>No se agregó la Historia");
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);

        }
    });
    cerrarFormulario();
}

function consultarHistoria(idHistoria) {
    block();
    getTipoReporteGrupo();
    getTipoImpacto(idHistoria);
    $("#lblTipo").html("Modificar Historia");
    cerrarFormulario();
    $("#txtGrupo").hide();
    $("#txtAsunto").show();
    $("#lblAsun").show();
    $("#estatusH").show();
    $.ajax({
        url: "ProductBacklog.aspx/consultarHistoriaCreada",
        type: "POST",
        data: "{idHistoria:" + idHistoria + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#txtEpica").val(data.d[2]);
            $("#txtHistoria").val(data.d[3]);
            $("#txtDescripcion").val(data.d[5]);
            $("#ddlPrioridad").val(data.d[4]);
            $("#ddlERPGrupo").val(data.d[0]);
            $("#txtCriterios").val(data.d[6]);
            $("#txtRiesgos").val(data.d[7]);
            $("#ddlEstatus").val(data.d[8]);
            $("#ddlSistemaG").val(data.d[1]);
            $("#txtIdReporte").val(data.d[9]);
            $("#txtStoryPoints").val(data.d[11]);
            nomArchivoM = data.d[10];
            if (nomArchivoM == "" || nomArchivoM == "NA") {
                $("#lblEvidenciaM").html('<label><span id="icon-47" class="warning rojo">No hay Evidencia</span></label>');
            } else {
                $("#lblEvidenciaM").empty();
                $("#lblEvidenciaM").show();
                $("#lblEvidenciaM").append('<a href="../../Configuracion/Reportes/Evidencia/' + nomArchivoM + '" target="_new"><span id="icon-47" class="evidencia verde">Evidencia</span></a>');
            }
            unBlock();
        
        },
        error: function (xhr, status, error) {
            unBlock();
            alert('Error al consultar Reporte Creado, idReporte: ' + idReporte);
        }
    });
abrirFormularioModificar();
}


function abrirFormularioModificar() {
    $("#divFormulario").slideDown("slow");
    $("#btnModificar").show();
    $("#btnModificarERP").hide();
    $("#btnGenerar").hide();
    $(".gpo").hide();
    $(".advgpo").hide();
    $("#nombreArchivo").val(null);
    $("#lblEvidenciaM").empty();
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
            modificaHistoria();
        },
        onChange(filename, extension, uploadBTn, fileSize, file) {
            $("#nombreArchivo").val(filename);
            $("#btnEvidencia").html(filename);
        }
    });
}


function modificaHistoria() {

    var idPrioridad = $("#ddlPrioridad").val();
    var idSistema = 0;

    var epica = $("#txtEpica").val();
    var descripcion = $("#txtDescripcion").val();
    var Historia = $("#txtHistoria").val();
    var criterios = $("#txtCriterios").val();
    var riesgos = $("#txtRiesgos").val();

    idERPGrupo = $('select[id="ddlERPGrupo"] option:selected').text();
    idSistema = $("#ddlSistemaG").val();

    var estatus = $("#ddlEstatus").val();
    var idHistoria = $("#txtIdReporte").val();

    var txtidUser = $("#txtidUser").val();
    var evidencia = $("#nombreArchivo").val() === "" ? "NULL" : $("#nombreArchivo").val();
    var tipoImpacto = $('#ddlTipoImpacto').val();
    var storyPoint = $("#txtStoryPoints").val();

    var dataToSend = {
        epica: epica,
        descripcion: descripcion,
        historia: Historia,
        criterios: criterios,
        idPrioridad: idPrioridad,
        idSistema: idSistema,
        idERPGrupo: idERPGrupo,
        riesgos: riesgos,
        idUsuario: txtidUser,
        idHistoria: idHistoria,
        estatus: estatus,
        evidencia: evidencia,
        tipoImpacto: tipoImpacto,
        storyPoint: storyPoint
    };

    block();
    $.ajax({
        url: "ProductBacklog.aspx/modificarHistoria",
        type: "POST",
        data: JSON.stringify(dataToSend),
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == true) {
                unBlock();
                alertify.success("<span id='icon-25' class='success blanco'></span>Se actualizó correctamente la historia.");
                cerrarFormulario();
                start();
                getTablaHistorias(1);
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
            alert('Error al modificar reporte, idReporte: ' + idHistoria);
        }
    });
}
function generarExcel(idEstatus) {
    block();
    $.ajax({
        url: "ProductBacklog.aspx/generarExcelHistorias",
        type: "POST",
        data: "{ idEstatus: "+ idEstatus+ "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            //verifica que regrese datos
            if (data.d != "") {
                unBlock();
                // Convertir la cadena Base64 a un Blob
                var byteCharacters = atob(data.d);
                var byteNumbers = new Array(byteCharacters.length);
                for (var i = 0; i < byteCharacters.length; i++) {
                    byteNumbers[i] = byteCharacters.charCodeAt(i);
                }
                var byteArray = new Uint8Array(byteNumbers);
                var blob = new Blob([byteArray], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

                // Utilizar FileSaver.js para descargar el archivo
                saveAs(blob, 'ReporteDeHistorias.xlsx');
                alertify.success("<span id='icon-25' class='success blanco'></span>Se genero correctamente el reporte.");
            }else {
                unBlock();
                alertify.error("<span id='icon-25' class='error blanco'></span>Error al generar reporte.");
            }
        },
        error: function (xhr, status, error) {
            alert(status);
            unBlock();
            alert('Error al generar reporte ');
        }
    });
}

function leerExcel(nombre) {
    block();
    if (nombre === "") {
        unBlock();
        alertify.error("<span id='icon-25' class='error blanco'></span>No se encontró el archivo.");
        return false;
    }
    $.ajax({
        url: "ProductBacklog.aspx/leerExcelHistorias",
        type: "POST",
        data: "{ nombreExcel: '" + nombre + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            //verifica que regrese datos
            if (data.d === "2") {
                unBlock();
                alertify.success("<span id='icon-25' class='success blanco'></span>Se actualizaron correctamente los reportes.");
                start(3);
            }
            else if (data.d === "0") {
                unBlock();
                alertify.error("<span id='icon-25' class='error blanco'></span>Error al leer reporte.");
            }
            else {
                unBlock();  
                alertify.error("<span id='icon-25' class='error blanco'></span>Los datos del reporte son incorrectos.");
            }
        },
        error: function (xhr, status, error) {
            alert(status);
            unBlock();
            alert('Los datos del reporte son incorrectos.');
        }
    });
    $("#nombreArchivo").val("");
}

//Función que aplica a un select ser un multiselect y con la opción de busqueda
function aplicarMultiSelect(idSelect) {
    $('#' + idSelect).select2({
        language: "es",
        placeholder: 'Seleccione una opción',
        allowClear: true, //Permite borrar la selección
        multiple: true, //Habilita la selección multiple
        minimumInputLength: 0, // only start searching when the user has input 3 or more characters
        dropdownParent: $('body'), // Adjuntar el dropdown al body del documento
        minimumResultsForSearch: -1 // Evita que se seleccione automáticamente un valor
    });
    $('#' + idSelect).val([]).trigger('change');
}

function ddlSprint() {
    block();
    $('#divDDLSprint').empty();
    $.ajax({
        url: "ProductBacklog.aspx/getDdlSprint",
        type: "POST",
        dataType: "JSON",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#divDDLSprint").html(data.d);
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

function mostrarDialogSprint() {
    $("#dlogSeleccionarSprint").show();
    ddlSprint();
}

function cerrarDialogSprint() {
    $('#divDDLSprint').empty();
    $('.validar').prop('checked', false);
    $('#btnSeleccionarTodos').text('Seleccionar Todos');
    $("#dlogSeleccionarSprint").hide();
}

function asignarSprint(sprint,historias) {
    block();
    $.ajax({
        url: "ProductBacklog.aspx/asignarSprint",
        type: "POST",
        data: "{ idSprint : "+sprint+" , historias: '" + historias + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            //verifica que regrese datos
            if (data.d === true) {
                unBlock();
                alertify.success("<span id='icon-25' class='success blanco'></span>Se asingaron correctamente las historias a el Sprint.");
                start(4);
            }
            else {
                unBlock();
                alertify.error("<span id='icon-25' class='error blanco'></span>No se asingaron las historias a el Sprint.");
                start(3);
            }
        },
        error: function (xhr, status, error) {
            alert(status);
            unBlock();
            alert('Los datos del reporte son incorrectos.');
        }
    });
    cerrarDialogSprint();

}