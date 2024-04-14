function cargarResponsables(idObjeto, nombre, metodo) {
    var idResponsable = $('#txtIdApoyo').val();    
    $('#txtApoyo').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "Dialogs.aspx/obtenerPersonalApoyo",
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
                    alert('Error en cargarResponsables');
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
