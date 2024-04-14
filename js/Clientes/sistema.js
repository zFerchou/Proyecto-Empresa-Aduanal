$(document).ready(function () {
    var tablaVisible = false; // Variable de estado para controlar la visibilidad de la tabla

    // Manejar el clic en el botón
    $("#btnSistema1").click(function () {
        // Alternar la visibilidad de la tabla
        if (tablaVisible) {
            $("#tablaVentana").hide();
        } else {
            $("#tablaVentana").show();
        }
        tablaVisible = !tablaVisible; // Cambiar el estado
    });
});
