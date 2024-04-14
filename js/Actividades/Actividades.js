var txtidUser;
var typeUser;



$(document).ready(function () {
    typeUser = $("#txtTipoUsuario").val();
    txtidUser = $("#txtidUser").val();

    if (typeUser == 1) {
        $('#btnAdd').html('<a id="btnEvidencia" href="#" class="btn verde shadow2 borde-blanco">Cargar Actividades</a>');
    }

    evidenciaUploader = new ss.SimpleUpload({
        button: 'btnEvidencia',
        url: 'UploadActividades.ashx',
        name: 'uploadfile',
        responseType: 'json',
        allowedExtensions: ['xlsx'],
        accept: ".xlsx",
        hoverClass: 'ui-state-hover',
        focusClass: 'ui-state-focus',
        disabledClass: 'ui-state-disabled',
        customHeaders: { 'idUsuario': txtidUser },
        onComplete(filename, response, uploadBtn, fileSize) {
            if (response == "1") {
                alert("Actividades cargadas con éxito.");
            }
            else {
                alert("Algo salio mal, reintente.");
            }            
            setTimeout(location.reload.bind(location), 500);
        },
        onExtError(filename, extension) {
            alert(`Archivo ${filename} no permitido.`);
            setTimeout(location.reload.bind(location), 500);
        },
        onError(filename, errorType, status, statusText, response, uploadBtn, fileSize) {
            alert(`Error. ${errorType} ${statusText}`);
            setTimeout(location.reload.bind(location), 500);
        }
    });

    getReportesCreados();
});

function getReportesCreados() {
    block();
    $.ajax({
        url: "Actividades.aspx/GetActividades",
        type: "POST",
        data: "{idUsuario: '" + txtidUser + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d == "") {
                $("#lblTabla").hide();
                unBlock();
            } else {
                $("#lblTabla").html(data.d);
                $("#tblActividades").dataTable({
                    "pageLength": 50,
                    "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                    "stripeClasses": ['gray', 'white'],
                    "aoColumnDefs": [
                        { "sClass": "txt-left", "aTargets": [1, 2, 3] }
                    ]
                });
                unBlock();
            }

        },
        error: function (xhr, status, error) {
            unBlock();
            alert(error);
        }
    });
}