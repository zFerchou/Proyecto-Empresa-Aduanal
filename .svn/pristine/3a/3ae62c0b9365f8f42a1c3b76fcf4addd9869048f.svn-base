var grupo = "";
var txtiduser;
var idUs;
var arregloGrupos = [];
$(document).ready(function () {
    getTableUsuariosRoles();
    generarDataTabla("tblPuestoUsuarios");
    txtiduser = $("#txtid").val();
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
            $('#btnAgregarGpo').click(function (e) {
                e.preventDefault();
                agregarUsuarioConGrupoNuevo();
            });
        }
    });
    $("#btnCancelarGpo").click(function () {
        $("#dlogAgregarGrupo").dialog("close").fadeIn();
    });

    $("#dlogAgregarUsuario").dialog({
        modal: true,
        autoOpen: false,
        height: 300,
        width: 700,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            $('#txtNombre').val('');
            $('#txtApp').val('');
            $('#txtApm').val('');
            $('#txtCorreo').val('');
            $('#txtIdGrupo').val('');
            $('#txtGrupo').val('');
            $('#txtIdArea').val('');
            $('#txtArea').val('');
        },
        open: function (event, ui) {

        }
    });


    $('#btnCerrar').click(function (e) {
        e.preventDefault();
        $('#dlogAgregarUsuario').dialog('close');
    });

    //Obtener los Grupos de RH
    obtenerGruposRH("#txtIdGrupo", "#txtGrupo", "obtenerGruposRH");
    //Obtener las Áreas de RH
    obtenerAreasRH("#txtIdArea", "#txtArea", "obtenerAreasRH");
    
    $('#btnAgregarUsuario').click(function (e) {
        e.preventDefault();
        $("#dlogAgregarUsuario").dialog("open");
    });
    $('#btnGuardarUsuario').click(function (e) {
        e.preventDefault();
        guardarUsuario();
    });
});

function getRolesByUsuario(idUsuario, nUsuario) {
    idUs = idUsuario;
    block();
    $.ajax({
        url: "Configuracion.aspx/getRolesByUsuario",
        type: "POST",
        data: "{idUsuario:" + idUsuario + " }",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblChecksPermisos").html("<div id='rolesERP' class='clear'><span id='icon-47' class='usuario_logueado'><a id='frm'>" + nUsuario + "</a><span></a></div><br />" + data.d);
            $("#lblBtnAceptar").html("<a class='btn verde' onclick='javascript:agregarPermisosByUsuario(" + idUsuario + ");'>Aceptar</a>");
            $("#dlogPermisos").dialog({
                modal: true,
                title: "PERMISOS USUARIO",
                width: 650,
                height: 600,
                open: function (event, ui) {
                    autocomplete();

                }
            });
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("No se pudieron traer los roles");
        }
    });
}

function getPermisosByUsuario(idUsuario, nUsuario) {
    idUs = idUsuario;
    block();
    $.ajax({
        url: "Configuracion.aspx/getPermisosByUsuario",
        type: "POST",
        data: "{idUsuario:" + idUsuario + " }",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblChecksPermisosI").html("<div id='rolesERP' class='clear'><span id='icon-47' class='usuario_logueado'><a id='frm'>" + nUsuario + "</a><span></a></div><br />" + data.d);
            $("#lblBtnAceptarI").html("<a class='btn verde' onclick='javascript:agregarPermisosIncByUsuario(" + idUsuario + ");'>Aceptar</a>");
            $("#dlogPermisosI").dialog({
                modal: true,
                title: "PERMISOS USUARIO",
                width: 650,
                height: 600,
                open: function (event, ui) {
                    autocomplete();

                }
            });
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("No se pudieron traer los permisos");
        }
    });
}

function getRolesByPuesto(idPuesto, nPuesto) {
    block();
    $.ajax({
        url: "Configuracion.aspx/getRolesByPuesto",
        type: "POST",
        data: "{idPuesto:" + idPuesto + " }",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#lblChecksPermisos").html("<div class='clear center'><span id='icon-47' class='roles'><a id='frm'>" + nPuesto + "</a><span></div>" + data.d);
            $("#lblBtnAceptar").html("<a class='btn verde' onclick='javascript:agregarPermisosByPuesto(" + idPuesto + ");'>Aceptar</a>");
            $("#dlogPermisos").dialog({
                modal: true,
                title: "PERMISOS PUESTO",
                width: 650,
                height: 600,
                open: function (event, ui) {
                    autocompletePuesto(idPuesto);
                    $("#agregaGrupo").hide()
                }
            });
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("No se pudieron traer los roles");
        }
    });
}

function agregarPermisosByUsuario(idUsuario) {
    block();
    var permisos = "";
    var bandera = false;
    $(".check").each(function () {
        if ($(this).is(":checked")) {
            var valor = $("#autoC" + $(this).val()).val();
            if (valor == undefined) {
                permisos += $(this).val() + "|";
                bandera = true;
            } else {
                if (valor != "") {
//                    var nGrupos=valor.unique();
                    permisos += $(this).val() + "." + valor + "|";
                    bandera = true;
                } else {
                    permisos += $(this).val();
                    var nombre = document.getElementById('lbl' + $(this).val()).innerHTML;
                    $("#agregaGrupo").html('<div class="width98 bg-alert">Complete el campo grupo de <b>' + nombre + '</b></div>');
                    $('#agregaGrupo').show('blind');
                    setTimeout(function () {
                        $('#agregaGrupo').toggle('blind');
                    }, 4000);
                    bandera = false;
                    return bandera;

                }
            }
        }
    });
    if (bandera == true || (permisos == "" && !bandera)) {
        $("#agregaGrupo").hide();
        $.ajax({
            url: "Configuracion.aspx/agregarPermisosByUsuario",
            type: "POST",
            data: "{idUsuario:" + idUsuario + ",lstPermisos: '" + permisos + "' }",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.d) {
                    alertify.success("<span id='icon-47' class='success blanco'></span>Cambios correctos");
                    $("#dlogPermisos").dialog("close");
                } else {
                    alertify.error("<span id='icon-47' class='success blanco'></span>No se realizó el cambio");
                }
                unBlock();
            },
            error: function (xhr, status, error) {
                unBlock();
                alert("No se realizó el cambio");
            }
        });
    } else {
        unBlock();
    }
}


function agregarPermisosIncByUsuario(idUsuario) {
    block();
    var permisos = "";
    var bandera = false;
    $(".check").each(function () {
        if ($(this).is(":checked")) {
            var valor = $(this).val();// $("#autoCI" + $(this).val()).val();
            if (valor != "") {
                permisos += $(this).val() + "|";
                bandera = true;
            }
        }
    });
    if (bandera == true || (permisos == "" && !bandera)) {
        $("#agregaGrupoI").hide();
        $.ajax({
            url: "Configuracion.aspx/agregarPermisosIncByUsuario",
            type: "POST",
            data: "{idUsuario:" + idUsuario + ",lstPermisos: '" + permisos + "' }",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.d) {
                    alertify.success("<span id='icon-47' class='success blanco'></span>Cambios correctos");
                    $("#dlogPermisosI").dialog("close");
                } else {
                    alertify.error("<span id='icon-47' class='success blanco'></span>No se realizó el cambio");
                }
                unBlock();
            },
            error: function (xhr, status, error) {
                unBlock();
                alert("No se realizó el cambio");
            }
        });
    } else {
        unBlock();
    }
}


function agregarPermisosByPuesto(idPuesto) {
    block();
    var permisos = "";
    var bandera = false;
    $(".check").each(function () {
        if ($(this).is(":checked")) {
            var valor = $("#autoC" + $(this).val()).val();
            if (valor == undefined) {
                permisos += $(this).val() + "|";
                bandera = true;
            } else {
                if (valor != "") {
//                    var nGrupos=arrGrupo.unique();
                    permisos += $(this).val() + "." + valor + "|";
                    bandera = true;
                } else {
                    permisos += $(this).val();
                    var nombre = document.getElementById('lbl' + $(this).val()).innerHTML;
                    $("#agregaGrupo").html('<div class="width98 bg-alert">Complete el campo grupo de <b>' + nombre + '</b></div>');
                    $('#agregaGrupo').show('blind');
                    setTimeout(function () {
                        $('#agregaGrupo').toggle('blind');
                    }, 4000);
                    return bandera = false;

                }
            }
        }
    });
    if (bandera == true || (permisos == "" && bandera == false)) {
        $.ajax({
            url: "Configuracion.aspx/agregarPermisosByPuesto",
            type: "POST",
            data: "{idPuesto:" + idPuesto + ",lstPermisos: '" + permisos + "' }",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.d) {
                    alertify.success("<span id='icon-47' class='success blanco'></span>Cambios correctos");
                    $("#dlogPermisos").dialog("close");
                }
                unBlock();
            },
            error: function (xhr, status, error) {
                unBlock();
                alert("No se realizó el cambio");
            }
        });
    } else {
        unBlock();
    }
}

function generarDataTabla(idTbl) {
    $("#" + idTbl).dataTable({
        "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
        "stripeClasses": ['gray', 'white']
    });
    $("#" + idTbl).on('draw.dt', function () {
        $("#grid-head1").addClass("round-border2");
    });
}

function autocomplete() {
    block();
    $.ajax({
        url: "Configuracion.aspx/obtenerGrupo",
        data: "{}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d != 0) {
                var arreglo = [];
                var datos = "";
                $.each(data.d, function (i, dato) {
                    arreglo[i] = '{"id" : "' + dato.idERPGrupo + '", "name" : "' + dato.nomGrupo + '"}';
                    datos += arreglo[i] + '| ';
                });
                datos = datos.substring(0, (datos.length - 2));
                var nomSeparados = datos.split("| ");

                var posicion = 0;
                for (var i = 0; i < nomSeparados.length; i++) {
                    arregloGrupos[posicion] = JSON.parse(nomSeparados[i]);
                    posicion = posicion + 1;
                }

                var resultado;
                var soportes = [8, 9, 10, 11, 12,13,14,15,16];
                var tam = soportes.length;
                for (var x = 0; x < tam; x++) {

                    obtenerGrupoActivo(soportes[x]);

                }
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error");
        }
    });
}


var arrGrupo;
function obtenerGrupoActivo(idSoporte) {
    block();
    $.ajax({
        url: "Configuracion.aspx/obtenerGrupoActivo",
        data: "{idUsuario: " + idUs + ", idSoporte: " + idSoporte + "}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var tam = data.d.length;
            if (tam > 0) {
                grupo = "prePopulate:["
                for (var i = 0; i < tam; i += 2) {
                    if (i == tam - 2) {
                        grupo += "{id: " + data.d[i] + ", name:'" + data.d[i + 1] + "'}";
                        arrGrupo += "{id: " + data.d[i] + ", name:'" + data.d[i + 1] + "'}";
                    } else {
                        grupo += "{id: " + data.d[i] + ", name:'" + data.d[i + 1] + "'},";
                        arrGrupo += "{id: " + data.d[i] + ", name:'" + data.d[i + 1] + "'},";
                    }
                }
                grupo += "],";
            } else {
                grupo = "";
            }
            if (grupo != '') {
                resultado = "$('#autoC" + idSoporte + "').tokenInput(arregloGrupos, { theme: 'facebook'," + grupo + "" +
                              "preventDuplicates: true, hintText: 'Escribe para buscar', noResultsText: 'No hay resultados', searchingText: 'Buscando...' });";
                eval(resultado);
                $('#autocomplete' + idSoporte + "").toggle();
            } else {
                $("#autoC" + idSoporte).tokenInput(arregloGrupos, { theme: "facebook", preventDuplicates: true, hintText: 'Escribe para buscar', noResultsText: 'No hay resultados', searchingText: 'Buscando...' });
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error");
        }
    });
}

Array.prototype.unique = function (a) {
    return function () { return this.filter(a) }
} (function (a, b, c) {
    return c.indexOf(a, b + 1) < 0
});

function mostrar(id) {
    $("#autocomplete" + id + "").toggle("blind");
}

function autocompletePuesto(idPuesto) {
    block();
    $.ajax({
        url: "Configuracion.aspx/obtenerGrupo",
        data: "{}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d != 0) {
                var arreglo = [];
                var datos = "";
                $.each(data.d, function (i, dato) {
                    arreglo[i] = '{"id" : "' + dato.idERPGrupo + '", "name" : "' + dato.nomGrupo + '"}';
                    datos += arreglo[i] + '| ';
                });
                datos = datos.substring(0, (datos.length - 2));
                var nomSeparados = datos.split("| ");

                var posicion = 0;
                for (var i = 0; i < nomSeparados.length; i++) {
                    arregloGrupos[posicion] = JSON.parse(nomSeparados[i]);
                    posicion = posicion + 1;
                }

                var resultado;
                var soportes = [8, 9, 10, 11, 12];
                var tam = soportes.length;
                for (var x = 0; x < tam; x++) {

                    obtenerGrupoPuestoActivo(soportes[x], idPuesto);

                }
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error");
        }
    });
}

var arrGrupo;
function obtenerGrupoPuestoActivo(idSoporte, idPuesto) {
    block();
    $.ajax({
        url: "Configuracion.aspx/obtenerGrupoPuestoActivo",
        data: "{idSoporte: " + idSoporte + ", idPuesto: " + idPuesto + "}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            grupo = "";
            var tam = data.d.length;
            if (tam > 0) {
                grupo = "prePopulate:["
                for (var i = 0; i < tam; i += 2) {
                    if (i == tam - 2) {
                        grupo += "{id: " + data.d[i] + ", name:'" + data.d[i + 1] + "'}";
                        arrGrupo += "{id: " + data.d[i] + ", name:'" + data.d[i + 1] + "'}";
                    } else {
                        grupo += "{id: " + data.d[i] + ", name:'" + data.d[i + 1] + "'},";
                        arrGrupo += "{id: " + data.d[i] + ", name:'" + data.d[i + 1] + "'},";
                    }
                }
                grupo += "],";
            } else {
                grupo = "";
            }
            if (grupo != '') {
                resultado = "$('#autoC" + idSoporte + "').tokenInput(arregloGrupos, { theme: 'facebook'," + grupo + "" +
                              "preventDuplicates: true, hintText: 'Escribe para buscar', noResultsText: 'No hay resultados', searchingText: 'Buscando...' });";
                eval(resultado);
                $('#autocomplete' + idSoporte + "").toggle();
            } else {
                $("#autoC" + idSoporte).tokenInput(arregloGrupos, { theme: "facebook", preventDuplicates: true, hintText: 'Escribe para buscar', noResultsText: 'No hay resultados', searchingText: 'Buscando...' });
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error");
        }
    });
}

//Cargar los Grupos en un autocomplete
function obtenerGruposRH(idObjeto, nombre, metodo) {
    $('#txtGrupo').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "Configuracion.aspx/obtenerGruposRH",
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
//Cargar las Áreas en un autocomplete
function obtenerAreasRH(idObjeto, nombre, metodo) {
    $('#txtArea').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "Configuracion.aspx/obtenerAreasRH",
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
                    alert('Error en cargarAreasRH');
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

//Guardar Usuario
function guardarUsuario() {
    var bandera;
    $('.validar').each(function () {
        if ($.trim($(this).val()) == '') {
            $(this).attr('placeholder', 'Información requerida');
            $(this).addClass('parpadea');            
            bandera = false;
        } else {
            bandera=true;
        }
    });
    setTimeout(function () {
        $('.validar').removeClass('parpadea');
    }, 1000);
    if (bandera==true) {
        // Expresion regular para validar el correo
        var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;

        // Se utiliza la funcion test() nativa de JavaScript
        if (regex.test($('#txtCorreo').val().trim())) {
            block();
            var nombre = $('#txtNombre').val();
            var app = $('#txtApp').val();
            var apm = $('#txtApm').val();
            var correo = $('#txtCorreo').val();
            var idGrupo = $('#txtIdGrupo').val();
            var idArea = $('#txtIdArea').val();
            if (idGrupo =="") {//Si el Grupo no existe en RH
                existeGrupo();
            }else{//El Grupo SI existe en RH
                $.ajax({
                    url: "Configuracion.aspx/guardarUsuario",
                    type: "POST",
                    data: "{nombre: '" + nombre + "',app: '" + app + "',apm: '" + apm + "',correo: '" + correo + "',idGrupo: " + idGrupo + ",idArea: " + idArea + " }",
                    dataType: "JSON",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d == true) {
                            alertify.success("<span id='icon-47' class='success blanco'></span>Usuario guardado correctamente.");
                            $("#dlogAgregarUsuario").dialog("close");
                            getTableUsuariosRoles();
                            unBlock();
                        } else {
                            alertify.error("<span id='icon-47' class='success blanco'></span>Error al guardar el usuario (1).");
                            unBlock();
                        }
                        unBlock();
                    },
                    error: function (xhr, status, error) {
                        unBlock();
                        alert("Error en guardarUsuario()");
                    }
                });
            }
        } else {
            $('#txtCorreo').val('').attr('placeholder', 'Ingresa un correo válido');
            $('#txtCorreo').addClass('parpadea');
        }
    }

}
//Validar si existe el grupo en RH
function existeGrupo() {
    var nomGrupo = $('#txtGrupo').val();
    block();
    $.ajax({
        url: "Configuracion.aspx/existeGrupo",
        type: "POST",
        data: "{nomGrupo: '" + nomGrupo + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == false) {
                $("#dlogAgregarGrupo").dialog("open");                
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error en existeGrupo()");
        }
    });
}
function getTableUsuarios() {
    block();
    $.ajax({
        url: "Configuracion.aspx/getTableUsuarios",
        data: "{}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d != "") {
                $("#lblTblUsuarios").html(data.d);
                $("#tblUsuarios").dataTable({
                    "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                    "stripeClasses": ['gray', 'white']
                });
                $("#tblUsuarios").on('draw.dt', function () {
                    $("#grid-head1").addClass("round-border2");
                });
            }
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error");
        }
    });
}
function getTableUsuariosRoles() {
    $("#lblTblUsuarios").html("<div><fieldset>" +
                            "<legend><span id='icon-25' class='usuarios blanco'>Permisos por usuario</span></legend>" +
                                "<table class='data_grid display' id='usuariosRoles'>"
                                    + "<thead>" +
                                        "<tr id='grid-head2'>" +
                                            "<th>Usuarios</th>" +
                                            "<th>Puesto</th>" +
                                            "<th>Permisos</th>" +
                                            "<th>Tipo Soporte</th>" +
                                        "</tr>" +
                                    "</thead>" + 
                                    "<tbody id='grid-body'>"+
                                    "</tbody>" +
                                    "<tfoot>" +
                                        "<tr>" +
                                            "<td class='foot shadow3 round-down2' colspan='3'>&nbsp;</td>" +
                                        "</tr>" +
                                    "</tfoot></table></legend></div>");
    $('#usuariosRoles').dataTable({
        "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
        "stripeClasses": ['gray', 'white'],
        "pagingType": "simple_numbers",
        "processing": false,
        "serverSide": true,
        "ajax": "usuarios.ashx"
    });
}
function agregarUsuarioConGrupoNuevo() {
    block();
    var nombre = $('#txtNombre').val();
    var app = $('#txtApp').val();
    var apm = $('#txtApm').val();
    var correo = $('#txtCorreo').val();
    var nomGrupo = $('#txtGrupo').val();
    var idArea = $('#txtIdArea').val();
    if ($.trim($('#txtGrupo').val()) == '') {
        $('#txtGrupo').addClass('parpadea');
        $('#txtGrupo').attr('placeholder','Campo Requerido');
    } else {
        $('#txtGrupo').removeClass('parpadea');
        $('#txtGrupo').removeAttr('placeholder', 'Campo Requerido');
        $.ajax({
            url: "Configuracion.aspx/agregarUsuarioConGrupoNuevo",
            type: "POST",
            data: "{nombre: '" + nombre + "',app: '" + app + "',apm: '" + apm + "',correo: '" + correo + "',nomGrupo: '" + nomGrupo + "' ,idArea: " + idArea + " }",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.d == true) {
                    alertify.success("<span id='icon-47' class='success blanco'></span>Usuario guardado correctamente.");
                    $("#dlogAgregarGrupo").dialog("close");
                    $("#dlogAgregarUsuario").dialog("close");
                    getTableUsuariosRoles();
                    unBlock();
                } else {
                    alertify.error("<span id='icon-47' class='success blanco'></span>Error al guardar el usuario (2).");
                    unBlock();
                }
                unBlock();
            },
            error: function (xhr, status, error) {
                unBlock();
                alert("Error en guardarUsuario()");
            }
        });
    }    
}