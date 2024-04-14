$(document).ready(function () {

    $(function () {

        $(document).tooltip();

        $("#dConf").dialog({
            open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).show(); },
            open: function (event, ui) { $(".ui-dialog-titlebar", ui.dialog).show(); },
            autoOpen: false,
            modal: true,
            resizable: false,
            draggable: false,
            closeOnEscape: false
        });

        $("#dMsj").dialog({
            open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).hide(); },
            open: function (event, ui) { $(".ui-dialog-titlebar", ui.dialog).hide(); },
            autoOpen: false,
            modal: true,
            resizable: false,
            draggable: false,
            closeOnEscape: false
        });

        $("#dNotificacion").dialog({
            open: function (event, ui) { $(".ui-dialog-titlebar-close", ui.dialog).hide(); },
            open: function (event, ui) { $(".ui-dialog-titlebar", ui.dialog).hide(); },
            autoOpen: false,
            modal: true,
            resizable: false,
            draggable: false,
            closeOnEscape: false
        });

        $("#txtGrupo").focus();

        $("#txtGrupo").keydown(function () {
            $('#modulo1').show();
            $('#modulo2').show();
            $('#modulo3').show();
            $('#modulo4').show();
            $("#btn-aceptar").prop("disabled", false);
        });

    });

    iniciarAutocomplete("#txtIdGrupo", "#txtGrupo", "sistemaGrupo");

});

function iniciarAutocomplete(idObjeto, nombre, metodo) {
    $(nombre).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../ERPManagement/sistemas.aspx/" + metodo + "",
                data: "{ term: '" + request.term + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            value: item.nombre,
                            ID: item.ID,
                            label: item.nombre
                        }
                    }))
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            var value = ui.item.value;
            var id = $(ui.item).attr("ID");
            $(idObjeto).val(id);
            deshabilitarCampos();
        },
        change: function (event, ui) {
            if (ui.item == null || ui.item == undefined) {
                
                deshabilitarCampos();
            }
        }
    });
}


function deshabilitarCampos() {
    var id = $("#txtGrupo").val();
    $.ajax({
        url: "../ERPManagement/sistemas.aspx/deshabilitarCampos",
        type: "POST",
        data: "{id:'" + id + "' }",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {

            for (i = 0; i < data.d.length; i++) {

                if (data.d[i].indexOf("1") != -1) {
                    $('#modulo1').hide();
                    $("#check1").prop("checked", false);
                }

                if (data.d[i].indexOf("2") != -1) {
                    $('#modulo2').hide();
                    $("#check2").prop("checked", false);
                }

                if (data.d[i].indexOf("3") != -1) {
                    $('#modulo3').hide();
                    $("#check3").prop("checked", false);
                }

                if (data.d[i].indexOf("4") != -1) {
                    $('#modulo4').hide();
                    $("#check4").prop("checked", false);
                }

            }
            if (data.d.length == 4) {
                $("#dNotificacion").dialog("open");


            } else {
                $("#dNotificacion").dialog("close");

            }
        }
        ,

        error: function (xhr, status, error) {
            alert(textStatus);
        }
    });
}



function mayusculas() {
    var x = document.getElementById("txtGrupo");
    x.value = x.value.toUpperCase();
}

function confirmar() {

    $('#mr').fadeOut();
    $('#mm').fadeOut();

    var check = $("input:checkbox:checked").length;
    var nomGrupo = $.trim($("#txtGrupo").val());

    if (nomGrupo == "") {

        setTimeout("$('#txtGrupo').focus().after(\"<span id='mr' class='requerido'>Complete este campo</span>\")", 0);
        setTimeout("$('#mr').fadeOut('slow')", 2500);

    } else {
        if (check == 0) {
            setTimeout("$('#lblCheck').after(\"<span id='mm' class='requerido'>Seleccione una opción</span>\")", 0);
            setTimeout("$('#mm').fadeOut('slow')", 2500);
        } else {
            $("#dConf").dialog("open");
        }
    }
}

function insertar() {

    $("#dConf").dialog("close");

    var nomGrupo = $.trim($("#txtGrupo").val().toUpperCase() );
    var ids = "";

    $("input:checkbox:checked").each(function () {
        ids += $(this).val() + ",";
    });

    var menosUno = (ids.length) - 1;
    ids = ids.substr(0, menosUno);
    block();
    $.ajax({
        url: "Sistemas.aspx/insertarGrupo",
        data: "{nomGrupo: '" + nomGrupo + "', ids: '" + ids + "'}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            
            $("#txtGrupo").val("");
            $("input:checkbox:checked").each(function () {
                $(this).prop("checked", false);
            });

            $("#dMsj").dialog("open");
            $("#textoMsj").text(data.d);
            unBlock();


        },
        error: function (xhr, status, error) {
            alert("Error");
            unBlock();
        }
    });

}

function cerrarDialog(valor) {

    var dg = valor;
    if (dg == 0) {
        $("#dConf").dialog("close");
    } else {
        $("#dMsj").dialog("close");
        $('#modulo1').show();
        $('#modulo2').show();
        $('#modulo3').show();
        $('#modulo4').show();
        $('#txtGrupo').val("");
        $('#txtIdGrupo').val("");
        $("#btn-aceptar").prop("disabled", false);
    }
}

function cerrarDialogSist() {
    $("#dNotificacion").dialog("close");
    $('#modulo1').show();
    $('#modulo2').show();
    $('#modulo3').show();
    $('#modulo4').show();
    $('#txtGrupo').val("");
    $('#txtIdGrupo').val("");
    $("#btn-aceptar").prop("disabled", false);
}