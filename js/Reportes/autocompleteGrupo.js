function cargarGrupos(idObjeto, nombre, metodo) {
    var idResponsable = $('#txtIdGrupo').val();
    var idGrupo = $('#txtIdGrupo').val();
    $('#txtGrupo').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "Reportes.aspx/obtenerGrupos",
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
                    alert('Error al cargar grupos');
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            var value = ui.item.value;
            var id = $(ui.item).attr("ID");
            $(idObjeto).val(id); // Valor de id oculto en la vista      
        },
        //        change: function (event, ui) {
        //            if (ui.item == null || ui.item == undefined) {
        //                $(idObjeto).val("");
        //                $(nombre).val("");
        //            }
        //        },
        close: function (event, ui) {
            var grupo = $('#txtGrupo').val();
            obtenerIdGrupoERP(grupo);
            setTimeout(function () {
                var idGrupo = $('#txtIdGrupo').val();
                if (idGrupo != "" && idGrupo !=0) {
                    $(".gpo").show();
                    $(".advgpo").hide();
                    getSistemasERP(idGrupo);
                    $('#txtGrupo').text(grupo);
                } else {
                    if ((grupo != "" || grupo.trim() !== "") && idGrupo == 0) {
                        $(".gpo").hide();
                        $(".advgpo").show();
                        getSistemasTodosERP();
                        $('#txtGrupo').text(grupo);
                    }
                }
            }, 800);
        }

    });
}

function cargarGruposInicio(idObjeto, nombre, metodo) {
    var idResponsable = $('#txtIdGrupo').val();
    $('#txtGrupo').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "Reportes.aspx/obtenerGruposInicio",
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
                    alert('Error al cargar grupos');
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            var value = ui.item.value;
            var id = $(ui.item).attr("ID");
            $(idObjeto).val(id); // Valor de id oculto en la vista      
        },
//        change: function (event, ui) {
//            if (ui.item == null || ui.item == undefined) {
//                $(idObjeto).val("");
//                $(nombre).val("");
//            }
//        },
        close: function (event, ui) {
            var idGrupo = $('#txtIdGrupo').val();
            $("#txtidGrupo").val(idGrupo);
            ddlSistemas(idGrupo);
        }
    });
}





//function cargarGrupos(idObjeto, nombre, metodo) {
//    var idResponsable = $('#txtIdGrupo').val();
//    $('#txtGrupo').autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: "Reportes.aspx/obtenerGrupos",
//                data: "{ term: '" + request.term + "' }",
//                dataType: "json",
//                type: "POST",
//                contentType: "application/json; charset=utf-8",
//                dataFilter: function (data) { return data; },
//                success: function (data) {
//                    response($.map(data.d, function (item) {
//                        return {
//                            value: item.Nombre,
//                            ID: item.Id,
//                            label: item.Nombre
//                        }
//                        alert(item.Id);
//                        alert(item.Nombre);
//                    }))

//                },
//                error: function (XMLHttpRequest, textStatus, errorThrown) {
//                    alert('Error al cargar grupos');
//                }
//            });
//        },
//        minLength: 2,
//        select: function (event, ui) {
//            var value = ui.item.value;
//            var id = $(ui.item).attr("ID");
//            $(idObjeto).val(id); // Valor de id oculto en la vista      
//            //obtenerTipoTarea();
//        },
//        change: function (event, ui) {
//            if (ui.item == null || ui.item == undefined) {
//                $(idObjeto).val("");
//                $(nombre).val("");
//            }
//        },
//        close: function (event, ui) {
//            var idGrupo = $('#txtIdGrupo').val();
//            var grupo = $("#txtGrupo").val();
//            if (idGrupo == "" && grupo != "") {
//                alert("si");
//                $("#agregarGrup").html('<a href="javascript:;" onclick="javascript:agregarGrupo();"><span id="icon-25" class="agregar" title="Agregar grupo"></span></a>');
//            } else {
//                $("#agregarGrup").remove();
//            }
//            if (grupo != "") {
//                getSistemasERP(idGrupo);
//            }
//        }
//    });
//}