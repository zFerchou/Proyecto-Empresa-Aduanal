$(document).ready(function () {
    $("#dDialogo").dialog({
        modal: true
    });

    getTabla();

    $("#txtFInicio").datepicker({
        dateFormat: "yy-mm-dd",
        firstDay: 1,
        autoOpen: false,
        onClose: function (selectedDate) {
            $("#txtFVencimiento").datepicker("option", "minDate", selectedDate);
        }
    });
    $("#txtFVencimiento").datepicker({
        dateFormat: "yy-mm-dd",
        firstDay: 1,
        autoOpen: false,
        onClose: function (selectedDate) {
            $("#txtFInicio").datepicker("option", "maxDate", selectedDate);
        }
    });
});


function advertenciaCuota(idCuota) {
    alert(idCuota);

    getTabla();
}

function getTabla() {
    $('#GridView1 thead tr').attr("id", "grid-head2");
    $('#GridView1 tbody').attr("id", "grid-body");

    $("#GridView1").dataTable({
        "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
        "stripeClasses": ['gray', 'white'],
        "columnDefs": [
            { className: "txt-left", "targets": [ 0,1 ] }
            ]
    });
    $("#GridView1").on('draw.dt', function () {
        $("#grid-head1").addClass("round-border2");
    });
}

function modificarCuota(idCuota) {
    block();

    $.ajax({
        url: "clientes.aspx/getCuota",
        type: "POST",
        data: "{idCuota:" + idCuota + " }",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            $("#lblModCuota").html("<a class='btn verde' onclick='modificarRegistro(" + idCuota + ");'>Aceptar</a>");

            $("#txtFInicio").val(data.d.FechaInicio);
            $("#txtFVencimiento").val(data.d.FechaVencimiento);
            $("#txtCuota").val(data.d.CuotaSaldo);
            $("#dModificar").dialog({
                autoOpen: false,
                modal: true,
                width: 400,
                close: function (event, ui) {
                    $("#txtFInicio").val("");
                    $("#txtFVencimiento").val("");
                    $("#txtCuota").val("");
                }
            });

            $("#dModificar").dialog("open");
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(textStatus);
        }
    });

}

function eliminarCuota(idCuota) {

    $("#lblAceptarEli").html("<a class='btn verde' onclick='eliminarRegistro("+idCuota+");'>Aceptar</a>");

    $("#dConfirmar").dialog({
        autoOpen: true,
        modal: true
    });
}

function eliminarRegistro(idCuota) {
    block();
    $.ajax({
        url: "clientes.aspx/eliminarCuota",
        type: "POST",
        data: "{idCuota:" + idCuota + " }",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            if (data.d == "1") {
                alertify.success("Se elimino el registro exitosamente");
                setTimeout(function () { location.reload(); }, 2000)
            } else {
                alertify.log("El registro no fue eliminado");
            }
        },
        error: function (xhr, status, error) {
            unBlock();
            alert(textStatus);
        }
    });
}