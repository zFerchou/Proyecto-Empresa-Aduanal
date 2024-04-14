var x = 1;
$(document).ready(function () {
    eliminar = '<span id="icon-25" class="verde agregar" title="Click para gregar nuevo sistema requerido"></span>   '
    iniciarAutocomplete("#txtIdTipoObjeto", "#txtDescripcion", "getTiposObjetos");
    iniciarAutocomplete("#txtIdSistema", "#txtNomSistema", "getSistemas");
    iniciarAutocomplete("#txtIdSistemaReq", "#txtNomSistemaReq", "getSistemas");

    $('.agregar').click(function () {
        $("#txtNomSistemaReq").clone().appendTo("#SistemasReq").attr("id", "txtNomSistemaReq" + x).attr("id", "txtNomSistemaReq" + x).val("");

        $("#txtIdSistemaReq").clone().appendTo("#SistemasReq").attr("id", "txtIdSistemaReq" + x);

        iniciarAutocomplete("#txtIdSistemaReq" + x, "#txtNomSistemaReq" + x, "getSistemas");
        x++;
    });

    $('.eliminarSistema').click(function () {
        $('#txtNomSistemaReq').remove(); ;
    });

    $('#btnCA').click(function () {
        if ($('#txtNomObjeto').val() == "" || $('#txtNomObjeto').val() == " ") {
            $("#errorNomObjeto").text("El nombre del objeto es requerido.");
            $('#txtNomObjeto').css("border", "1px solid red");
        } else {
            $("#errorNomObjeto").text("");
            $('#txtNomObjeto').css("border", "1px solid green");
        }

        if ($('#txtDescripcion').val() == "" || $('#txtDescripcion').val() == " " || $('#txtIdTipoObjeto').val() == "" || $('#txtIdTipoObjeto').val() == " ") {
            $("#errorDescripcion").text("El Tipo del objeto es requerido.");
            $('#txtDescripcion').css("border", "1px solid red");
        } else {
            $("#errorDescripcion").text("");
            $('#txtDescripcion').css("border", "1px solid green");
        }

        if ($('#txtScriptObjeto').val() == "" || $('#txtScriptObjeto').val() == " ") {
            $("#errorScript").text("El Script del objeto es requerido.");
            $('#txtScriptObjeto').css("border", "1px solid red");
        } else {
            $("#errorScript").text("");
            $('#txtScriptObjeto').css("border", "1px solid green");
        }

        if ($('#txtNomSistema').val() == "" || $('#txtNomSistema').val() == " " || $('#txtIdSistema').val() == " " || $('#txtIdSistema').val() == " ") {
            $("#errorNomSistema").text("El nombre de sistema al cual se creara es requerido.");
            $('#txtNomSistema').css("border", "1px solid red");
        } else {
            $("#errorNomSistema").text("");
            $('#txtNomSistema').css("border", "1px solid green");
        }

        if ($('#txtNomObjeto').val() !== "" && $('#txtNomObjeto').val() !== " " && $('#txtDescripcion').val() !== "" && $('#txtDescripcion').val() !== " " && $('#txtIdTipoObjeto').val() !== "" && $('#txtIdTipoObjeto').val() !== " " && $('#txtScriptObjeto').val() !== "" && $('#txtScriptObjeto').val() !== " " && $('#txtNomSistema').val() !== "" && $('#txtNomSistema').val() !== " " && $('#txtIdSistema').val() !== " " && $('#txtIdSistema').val() !== " ") {
            crearObjetos()
        }
    });

}
);

function crearObjetos() {
    //alert('Entró a crearObjetos.js');
    var nomObjeto = $('#txtNomObjeto').val();
    //alert('Nombre Objeto: '+nomObjeto);
    var scriptObjeto = $('#txtScriptObjeto').val().replace(/\'/g, '°°');
    //alert(scriptObjeto);
    var idTipoObjeto = $('#txtIdTipoObjeto').val();
    //alert('Tipo Objeto: '+idTipoObjeto);
    var idSistema = $('#txtIdSistema').val();
    //alert('ID Sistema: '+idSistema);
    var idSisReq = $('#txtIdSistemaReq').val();
    var i;
    var idsSisReq = "";
    idsSisReq += $('#txtIdSistemaReq').val() + ',';
    for (i = 1; i < x; i++) {
            idsSisReq += $('#txtIdSistemaReq' + i).val() + ",";
    }
    idsSisReq = idsSisReq.slice(0, -1);
    block();
    $.ajax({
        url: "crearobjetos.aspx/crearObjetos",
        data: "{nomObjeto: '" + nomObjeto + "', scriptObjeto: '" + scriptObjeto + "', idTipoObjeto: '" + idTipoObjeto + "', idSistema: '" + idSistema + "', idSisReq: '" + idSisReq + "'}",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $(':input','#form')
             .not(':button, :submit, :reset, :hidden')
             .val('')
             .removeAttr('checked')
             .removeAttr('selected');
             
            $("#textoMsj").text("Se creó el objeto " + nomObjeto+" exitosamente.");
           
            unBlock();
        },
        error: function (xhr, status, error) {
            
            $("#textoMsj").text("Sucedio algo inesperado, favor de comunicarse con el departamento de TI, gracias.");
           
            unBlock();
        }
    });

}


function iniciarAutocomplete(idObjeto, nombre, metodo) {

    $(nombre).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "crearobjetos.aspx/" + metodo + "",
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
                        alert(item.ID);
                        alert(item.nombre);

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
            $(idObjeto).val(id); // Valor de id oculto en la vista           

        },
        change: function (event, ui) {
            if (ui.item == null || ui.item == undefined) {
                $(idObjeto).val("");
                $(nombre).val("");
            }
        }
    });
}
