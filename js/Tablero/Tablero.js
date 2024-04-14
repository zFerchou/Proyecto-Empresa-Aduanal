$(document).ready(function () {
    fn_abrirTablero();
});

function fn_abrirTablero() {
    block();
    $.ajax({
        url: "Tablero.aspx/abrirTablero",
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data.d != "-1") {
                window.open(data.d, 'TABLERO NAD',
                    'toolbar=yes,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=1350,height=600,top=100,left=0');
                fn_redirectInicio();
            }
            else {
                alert("ERROR AL ABRIR TABLERO NAD");
            }
            unBlock();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("ERROR AL ABRIR TABLERO NAD");
            unBlock();
        }
    });
}

function fn_redirectInicio() {
    location.href = "../../inicio.aspx";
}