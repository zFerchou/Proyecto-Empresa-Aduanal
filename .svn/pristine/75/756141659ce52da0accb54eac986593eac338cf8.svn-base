var cantidad=0;
var resp;
$(document).ready(function () {    
    //Obtener todas las cuotas
    //getCuotas();
    $("#divFiltrosBusqueda").addClass("hide");
    getGrupos();

    //Iniciar dialog para ver la información de la cuota
    $("#dlogModificarCuotas").dialog({
        modal: true,
        autoOpen: false,
        height: 430,
        width: 455,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            $('#alertaCuota').addClass('hide');
            $('#alertaFF').addClass('hide');
            $('#alertaFI').addClass('hide');
            $("#resp").show();
        },
        open: function (event, ui) {
            $("#txtCuotaM").focus();
        }
    });

    //Iniciar dialog para alerta al eliminar cuota
    $("#dlogEliminarCuota").dialog({
        modal: true,
        autoOpen: false,
        height: 180,
        width: 300,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            //$('#txtIdCuota').val("");
            //$('#txtCuotaM').val();
            //$('#txtFechaInicioM').val();
            //$('#txtFechaVencimientoM').val();
        },
        open: function (event, ui) {

        }
    });

    //Idioma en Español de DatePicker
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
    $("#txtFechaInicio").datepicker({
        onClose: function (selectedDate) {
            $("#txtFechaVencimiento").datepicker("option", "minDate", selectedDate);
        },
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });
    $("#txtFechaVencimiento").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });

    $("#txtFechaInicioM").datepicker({
        onClose: function (selectedDate) {
            $("#txtFechaVencimientoM").datepicker("option", "minDate", selectedDate);
        },
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });
    $("#txtFechaVencimientoM").datepicker({
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
    $('.calendarioInicioM').click(function () {
        mostrarDatepickerFechaInicioM();
    });
    $('.calendarioFinM').click(function (e) {
        e.preventDefault();
        mostrarDatepickerFechaFinM();
    });

    //Click para volver a ver todas las cuotas
    $('#btnVerTodos').click(function () {
        $("#divFiltrosBusqueda").removeClass("hide");
        $("#divConten").attr("style", "min-height: 580px;");
        $("#divSistemas").removeClass("hide");
        $("#divGrupos").addClass("hide");
        $('#divTblCuotas').empty();
        $('#divTblCuotas2').empty();
        getCuotas();
    });
    
    //Click para realizar búsqueda de cuotas según los filtros ingresados
    $('#btnBuscar').click(function () {
        obtenerCuotasFiltros();
    });
    //Autocompletes para Grupos y Sistemas
    cargarGrupos("#txtIdGrupo", "#txtNomGrupo", "cargarGrupos");
    cargarSistemas("#txtIdSistema", "#txtNomSistema", "cargarSistemas");
    //Click en Si para eliminar cuota
    $('#btnSi').click(function () {
        var idCuota = $('#txtIdCuota').val();
        block();
        $.ajax({
            url: "CuotasClientes.aspx/eliminarCuota",
            type: "POST",
            data: "{idCuota:" + idCuota + "}",
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            dataFilter: function (data) { return data; },
            success: function (data) {
                if (data.d == true) {
                    unBlock();
                    $("#dlogEliminarCuota").dialog("close");
                    $('#' + idCuota + '').closest('tr').remove().fadeOut(1000);
                    alertify.success("<span id='icon-25' class='success blanco'></span>Se eliminó correctamente la Cuota.");
                } else {
                    unBlock();
                    alertify.error("<span id='icon-25' class='warning blanco'></span>No se eliminó la Cuota.");
                }
            },
            error: function (xhr, status, error) {
                unBlock();
                alertify.error("<span id='icon-25' class='warning blanco'></span>Error en eliminarCuota");
            }
        });
    });
    $('#btnNo').click(function () {
        $("#dlogEliminarReporte").dialog("close").fadeIn();
    });
    //Cambiar colores de iconos de datepicker
    $(".calendarioFin").mouseover(function () {
        $(this).removeClass('azul').addClass('verde');
    });
    $(".calendarioFin").mouseout(function () {
        $(this).removeClass('verde').addClass('azul');
    });
    $(".calendarioInicio").mouseover(function () {
        $(this).removeClass('azul').addClass('verde');
    });
    $(".calendarioInicio").mouseout(function () {
        $(this).removeClass('verde').addClass('azul');
    });
    //Modificar la Cuota
    $('#btnModificarCuota').click(function () {
        var idCuota = $('#txtIdCuota').val();        
        var cuota = $.trim($('#txtCuotaM').val());
        var fechaI = $('#txtFechaInicioM').val();
        var fechaF = $('#txtFechaVencimientoM').val();
        var autocomplete= $("#autocomplete"+idCuota).val();
        var IdResp=autocomplete.split(",");
        var idRespoAutocomplete=IdResp.unique();
        var cbo=$("#cbo"+idCuota).val();
        var bandera=false;
        var idERPGS= $("#txtidERPGS").val();
        var patron = /^-?[1-9][0-9]+([.][0-9][0-9])?$/;
        if (cuota != '') {
            if (!patron.test(cuota)) {
              $('#alertaCuota').removeClass('hide');
            }else{
                $('#alertaCuota').addClass('hide');
                 bandera=true;  
            }
        }else{
            $('#alertaCuota').removeClass('hide');
        }
        if ($('#txtFechaInicioM').val().trim() === '') {
            $('#alertaFI').removeClass('hide');            
        }

        if ($('#txtFechaVencimientoM').val().trim() === '') {
            $('#alertaFF').removeClass('hide');
        }
        if(cbo==null){
            $('#cbo').removeClass('hide');
        }else{
            $('#cbo').addClass('hide');
        }
        if ($('#txtFechaVencimientoM').val().trim() != '' && $('#txtFechaInicioM').val().trim() != '' && bandera==true && cbo!=null) {
            $('#alertaCuota').addClass('hide');
            $('#alertaFF').addClass('hide');
            $('#alertaFI').addClass('hide');
            modificarCuota(idCuota, cuota, fechaI, fechaF, idRespoAutocomplete, idERPGS, cbo);
        }
    });
});
//Obtener todas las cuotas
function getCuotas() {
    block();
    $.ajax({
        url: "CuotasClientes.aspx/getCuotas",
        type: "POST",
        data: "{}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();

            $('#divTblCuotas').append(data.d);
            $("#tblCuotas").dataTable({
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                order: [[2, 'asc'], [3, 'asc']],
                "aoColumnDefs": [
                   { "sClass": "txt-left", "aTargets": [0,1] }
                ],
            });
            $("#tblCuotas").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en getCuotas.");
        }
    });
}

/*Obtener Grupos  */
function getGrupos() {
    block();
    $("#divGrupos").removeClass("hide");
    $("#divConten").attr("style", "min-height: 480px;");
    $.ajax({
        url: "CuotasClientes.aspx/getGrupos",
        type: "POST",
        data: "{}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();

            $('#divTblCuotas2').append(data.d);
            $("#tblCuotas").dataTable({
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                order: [[2, 'asc'], [3, 'asc']],
                "aoColumnDefs": [
                   { "sClass": "txt-left", "aTargets": [0,1] }
                ],
            });
            $("#tblCuotas").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en getGrupos.");
        }
    });
}

/*Funcion para ver los sistemas */
function consulSistem() {
    $("#divReturn").addClass("hide");
    $("#divFiltrosBusqueda").removeClass("hide");
    $("#divConten").attr("style", "min-height: 580px;");
    $("#divSistemas").removeClass("hide");
    $("#divGrupos").addClass("hide");
    $('#divTblCuotas').empty();
    $('#divTblCuotas2').empty();
    getCuotas();

}

/*Funcion para ver los grupos. clic en el boton */
function consulGrupo() {
    $("#divGrupos").removeClass("hide");
    $("#divSistemas").addClass("hide");
    $("#divReturn").addClass("hide");
    $("#divConten").attr("style", "min-height: 480px;");
    $("#divFiltrosBusqueda").addClass("hide");
    $("#divEspa").addClass("hide");
    $('#divTblCuotas2').empty();
    $('#divTblCuotas').empty();
    console.log("ocultar");
    $("#txtIdGrupo").val("");
    getGrupos();
}        

/* ver los sistemas de los grupos. */
function verGrupo(idERPGrupo) {
    
    $("#divSistemas").removeClass("hide");
    $("#divGrupos").addClass("hide");
    $("#divReturn").removeClass("hide");

    $("#txtIdGrupo").val(idERPGrupo);
    $("#divFiltrosBusqueda").removeClass("hide");
    $('#divTblCuotas').empty();
    $('#divTblCuotas2').empty();
    
    obtenerCuotasFiltros();

    $("#txtIdGrupo").val("");

}
/*--------------------  */

//Obtener información de una cuota
function obtenerCuota(idCuota, idERPGrupo) {
    $("#txtidERPGS").val(idERPGrupo);
    obtenerUsuarioSistema(idCuota, idERPGrupo);
    $("#dlogModificarCuotas").dialog('open');
    block();
    $.ajax({
        url: "CuotasClientes.aspx/obtenerCuota",
        type: "POST",
        data: "{idCuota:" + idCuota + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            $("#auto").html('<div id="autocomplete'+idCuota+'" style="padding-top:10px;"><input id="autoC'+idCuota+'" class="autocomplete transparente" name="aut" /></div><span id="alertaAuto" class="requerido-auto hide">Agregue al menos un Responsable</span> <br />');
            if(cantidad==0){
                autocomplete(idCuota);
            }
            $('#txtIdCuota').val("");
            $('#txtIdCuota').val(idCuota);
            $('#txtCuotaM').val(data.d[0]);
            $('#txtFechaInicioM').val(data.d[1]);
            $('#txtFechaVencimientoM').val(data.d[2]);
            $("#cboMoneda").html(data.d[3]);
            if(data.d[4]!=""){
                $("#cbo"+idCuota).val(data.d[4]);
            }
            unBlock();

        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en modificarCuota.");
        }
    });

}
//Modificar la información de una cuota
function modificarCuota(idCuota, cuota, fechaInicio, fechaVencimiento, personalResp, idERPGruposistema, tMoneda) {
    //alert('idCuota: ' + idCuota + ', cuota: ' + cuota + ', fechaI: ' + fechaInicio + ', fechaF: ' + fechaVencimiento);
    block();
    $.ajax({
        url: "CuotasClientes.aspx/modificarCuota",
        type: "POST",
        data: "{idCuota:" + idCuota + ", cuota:'"+cuota+"', fechaInicio:'"+fechaInicio+"', fechaVencimiento:'"+fechaVencimiento+"', personalResp: '"+personalResp+"', idERPGruposistema: "+idERPGruposistema+", tMoneda: "+tMoneda+"}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            if (data.d == true) {
                alertify.success("<span id='icon-25' class='warning blanco'></span>Se modificó correctamente la Cuota.");
                $("#dlogModificarCuotas").dialog('close');
                $('#divTblCuotas').empty();
                getCuotas();
            } else {
                alertify.error("<span id='icon-25' class='warning blanco'></span>Error al modificar la Cuota.");
            }
            unBlock();
               
        },
        error: function (xhr, status, error) {
            unBlock();
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en modificarCuota.");
        }
    });
}
//Eliminar una cuota (Solo se cambia su estatus a 2 de Eliminado)
function eliminarCuota(idCuota) {
    $('#txtIdCuota').val(idCuota);
    $("#dlogEliminarCuota").dialog("open");
    
}
//Clicks en los calendarios para mostrar datepicker
function mostrarDatepickerFechaFin() {
    $("#txtFechaVencimiento").datepicker("show");
}
function mostrarDatepickerFechaInicio() {
    $("#txtFechaInicio").datepicker("show");
}
function mostrarDatepickerFechaFinM() {
    $("#txtFechaVencimientoM").datepicker("show");
}
function mostrarDatepickerFechaInicioM() {
    $("#txtFechaInicioM").datepicker("show");
}
//Obtener las cuotas según los filtros de búsqueda
function obtenerCuotasFiltros() {
    var idGrupo = 0;
    var idSistema = 0;
    var fechaInicio = "";
    var fechaFin = "";
    if ($('#txtIdGrupo').val() !== '') {
        idGrupo = $('#txtIdGrupo').val();
    } else {
        idGrupo;
    }
    if ($('#txtIdSistema').val() !== '') {
        idSistema = $('#txtIdSistema').val();
    } else {
        idSistema;
    }
    if ($('#txtFechaInicio').val() !== '') {
        fechaInicio = $('#txtFechaInicio').val();
    } else {
        fechaInicio;
    }
    if ($('#txtFechaVencimiento').val() !== '') {
        fechaFin = $('#txtFechaVencimiento').val();
    } else {
        fechaFin;
    }
    //alert(idGrupo+' '+idSistema+' '+fechaInicio+' '+fechaFin);
    block();
    $.ajax({
        url: "CuotasClientes.aspx/obtenerCuotasFiltros",
        type: "POST",
        data: "{idGrupo:" + idGrupo + ", idSistema:" + idSistema + ", fechaInicio:'" + fechaInicio + "',fechaFin:'" + fechaFin + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            //alert(data.d.indexOf("°"));
            if (data.d.indexOf("°") == -1) {
                $('#divTblCuotas').empty();
                $('#divTblCuotas').append(data.d);
                $("#tblCuotas").dataTable({
                    "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                    "stripeClasses": ['gray', 'white'],
                    order: [[2, 'asc'], [3, 'asc']],
                    "aoColumnDefs": [
                       { "sClass": "txt-left", "aTargets": [0,1] }
                    ],
                });
                $("#tblCuotas").on('draw.dt', function () {
                    $("#grid-head1").addClass("round-border2");
                });
            } else {
                $('#divTblCuotas').empty();
                $('#divTblCuotas').append(data.d.substring(0, data.d.indexOf("°")));

            }


            unBlock();
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en obtenerCuotasFiltros.");
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
                url: "CuotasClientes.aspx/cargarGrupos",
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
//Cargar los Sistemas en un autocomplete
function cargarSistemas(idObjeto, nombre, metodo) {
    var idGrupo = $('#txtIdSistema').val();
    $('#txtNomSistema').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "CuotasClientes.aspx/cargarSistemas",
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

function autocomplete(id) {
    block();
    $.ajax({
        url: "CuotasClientes.aspx/obtenerResponsablesSistema",
        data: "{}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if(data.d!=0){
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
                var resultado;
            
                if(resp!=''){
                    resultado="$('#autocomplete"+id+"').tokenInput(arregloUsuarios, { theme: 'facebook',"+resp+""+
                              "preventDuplicates: true, hintText: 'Escribe para buscar', noResultsText: 'No hay resultados', searchingText: 'Buscando...' });";
                    eval(resultado);
                }else{
                    $("#autocomplete"+id).tokenInput(arregloUsuarios, { theme: "facebook",preventDuplicates: true, hintText: 'Escribe para buscar', noResultsText: 'No hay resultados', searchingText: 'Buscando...' });
                }
            
            }else{
                if(resp!=''){
                    resultado="$('#autocomplete"+id+"').tokenInput(arrAyu, { theme: 'facebook',"+resp+""+
                              "preventDuplicates: true, hintText: 'No se encontraron usuario que puedan cumplir con esta función', noResultsText: 'No se encontraron usuario que puedan cumplir con esta función', searchingText: 'No se encontraron usuario que puedan cumplir con esta función' });";
                    eval(resultado);
                }else{
                    $("#resp").hide();
                    $("#autocomplete"+id).html("<span id='icon-47' class='informacion'></span><label>No hay personal que pueda ser responsable del sistema, comuniquese con personal de la empresa.</label><input type='hidden' id='autoC"+id+"' value=''>");
                }
            }
            
            unBlock();
        },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error");
        }
    });
}
var arrAyu;
function obtenerUsuarioSistema(idCuota, idERPGrupoSistema){
    block();
    $.ajax({
        url: "CuotasClientes.aspx/obtenerResponsablesSistemaActivo",
        data: "{idCuota: '"+idCuota+"', idERPGrupoSistema: '"+idERPGrupoSistema+"'}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var tam =data.d.length;
            if(tam>0){
            resp="prePopulate:["
            for(var i=0; i<tam;i+=2){
                if(i==tam-1){
                    resp+="{id: "+ data.d[i]+ ", name:'"+data.d[i+1]+"'}";
                    arrAyu+="{id: "+ data.d[i]+ ", name:'"+data.d[i+1]+"'}";
                }else {
                    resp+="{id: "+ data.d[i]+ ", name:'"+data.d[i+1]+"'},";
                    arrAyu+="{id: "+ data.d[i]+ ", name:'"+data.d[i+1]+"'},";
                }
            }
            resp+="],";
            }else{
                resp="";        
            }
          },
        error: function (xhr, status, error) {
            unBlock();
            alert("Error");
        }
});
}

Array.prototype.unique=function(a){
  return function(){return this.filter(a)}}(function(a,b,c){return c.indexOf(a,b+1)<0
});
