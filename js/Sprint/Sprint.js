var txtidUser;
var typeUser;



$(document).ready(function () {
    typeUser = $("#txtTipoUsuario").val();
    txtidUser = $("#txtidUser").val();
    $("#dlogReporte").dialog({
        modal: true,
        autoOpen: false,
        height: 600,
        width: 1800,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            
            $('#divTblDetalle').empty();
        },
        open: function (event, ui) {

        }
    });
    getTablaSprint();

   /* $('select#sp').on('change', function () {
        cambiarSprint();
    });*/
    
});

function getTablaSprint() {
    block();
    $.ajax({
        url: "Sprint.aspx/GetTablaSprint",
        type: "POST",
        data: "{}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#lblTabla").html(data.d);
            $("#tblSprint").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "aoColumnDefs": [
                    { "sClass": "txt-left", "aTargets": [1, 2, 3] }
                ]
            });
            unBlock();


        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

function getReportesCreados(sprint) {
    block();
    $('#divDetalle').show();
    $('#dlogReporte').dialog("open");
    $.ajax({
        url: "Sprint.aspx/GetSprint",
        type: "POST",
        data: "{sprint: '" + sprint + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $('#divTblDetalle').append(data.d);
            $("#tblDetalle").dataTable({
                "pageLength": 50,
                "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                "stripeClasses": ['gray', 'white'],
                "aoColumnDefs": [
                    { "sClass": "txt-left", "aTargets": [1, 2, 3] }
                ],
            });
            $("#tblDetalle").on('draw.dt', function () {
                $("#grid-head1").addClass("round-border2");
            });

           unBlock();
            

        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

function getSprint() {
    block();
    $.ajax({
        url: "Sprint.aspx/GetSprintByID",
        type: "POST",
        data: "{idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $('select#sp option[value=' + data.d[0] + ']').attr('selected', 'selected');
           $("#txtPeriodo").val(data.d[1]);
            $("#txtSP").val(data.d[2]);
            $("#txtTH").val(data.d[3])
            $("#txtHA").val(data.d[4]);
            $("#txtHP").val(data.d[5]);
            $("#txtHC").val(data.d[6]);
            $("#txtHT").val(data.d[7]);
            $("#txtSA").val(data.d[8]);
            $("#txtSC").val(data.d[9]);
            $("#txtAvance").val(data.d[10]);
            $('select#es option[value=' + data.d[11] + ']').attr('selected', 'selected');
            var sprint = $("#sp").val();
            getReportesCreados(sprint);
            unBlock();
            

        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}

function cambiarSprint() {
    var sprint = $("#sp").val();
    block();
    $.ajax({
        url: "Sprint.aspx/GetSprintBySprint",
        type: "POST",
        data: "{sprint: '" + sprint + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $('select#sp option[value=' + data.d[0] + ']').attr('selected', 'selected');
            $("#txtPeriodo").val(data.d[1]);
            $("#txtSP").val(data.d[2]);
            $("#txtTH").val(data.d[3])
            $("#txtHA").val(data.d[4]);
            $("#txtHP").val(data.d[5]);
            $("#txtHC").val(data.d[6]);
            $("#txtHT").val(data.d[7]);
            $("#txtSA").val(data.d[8]);
            $("#txtSC").val(data.d[9]);
            $("#txtAvance").val(data.d[10]);
            $('select#es option[value=' + data.d[11] + ']').attr('selected', 'selected');
            getReportesCreados(sprint);
            unBlock();


        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}