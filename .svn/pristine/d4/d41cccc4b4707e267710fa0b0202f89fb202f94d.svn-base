/*
    Funcion para contar las historias y mandarlas a los
    labels
*/
function ObtenerDatoNumeroHistoriasAgendadas() {
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerNumeroHistoriasAgendadas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response); // Verifica la respuesta del servidor
            var numHistorias = response.d;
            $('#lblNumAgendadas').text(response.d); // Actualizar el label con el número de historias
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function ObtenerDatoNumeroHistoriasLiberadas() {
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerNumeroHistoriasLiberadas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response); // Verifica la respuesta del servidor
            var numHistorias = response.d;
            $('#lblNumLiberadas').text(response.d); // Actualizar el label con el número de historias
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}
/*
    Funcion para que se muestren los sistemas en donde
    las historias esten activas. (Por Modificar)
*/
function mostrarSistema(id) {
    // Ocultar todos los sistemas
    var sistemas = document.querySelectorAll('#contenidoSistemas > div');
    for (var i = 0; i < sistemas.length; i++) {
        sistemas[i].style.display = 'none';
    }

    // Mostrar el sistema correspondiente al ID recibido
    var sistema = document.getElementById(id);
    if (sistema) {
        sistema.style.display = 'block';
    }
}

/*
    Funcion de los Botones para que se llenen las tablas.
    (Por Modificar)
*/
function mostrarTabla(id) {
    // Ocultar todas las tablas
    document.getElementById("agendadas").style.display = "none";
    document.getElementById("sinAgendar").style.display = "none";
    document.getElementById("liberadas").style.display = "none";
    // Mostrar la tabla correspondiente al id
    document.getElementById(id).style.display = "block";
}
function obtenerDatosTablaAgendadas() {
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerHistoriasAgendadas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            var data = JSON.parse(response.d);
            var tableBody = $('#tblReporteAgendadas tbody');
            tableBody.empty();

            // Llenar la tabla con los datos obtenidos
            $.each(data, function (i, item) {
                var row = $('<tr>').append(
                    $('<td>').text(item.idHistoriasALiberar),
                    $('<td>').text(item.idReporte),
                    $('<td>').text(item.descripcion),
                    $('<td>').text(item.nomGrupo),
                    $('<td>').text(item.puntosDeHistoria),
                    $('<td>').text(item.fechaPropuestaOwner),
                    $('<td>').append($('<input>').attr('type', 'checkbox'))
                );
                tableBody.append(row);
            });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}


function obtenerDatosTablaSinAgendar() {
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerHistoriasNoAgendadas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            var data = JSON.parse(response.d);
            var tableBody = $('#tblReporteSinAsignar tbody');
            tableBody.empty();

            // Obtener el número de historias sin agendar
            var numHistorias = data.length;
            $('#lblNumSinAgendar').text(numHistorias); // Actualizar el label

            $.each(data, function (i, item) {
                var row = $('<tr>').append(
                    $('<td>').text(item.idHistoriasALiberar),
                    $('<td>').text(item.idReporte),
                    $('<td>').text(item.descripcion),
                    $('<td>').text(item.nomGrupo),
                    $('<td>').text(item.puntosDeHistoria),
                    $('<td>').text(item.fechaPropuestaOwner),
                    $('<td>').append($('<input>').attr('type', 'checkbox'))
                );
                tableBody.append(row);
            });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}



function obtenerDatosTablaLiberadas() {
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerHistoriasLiberadas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            var data = JSON.parse(response.d);
            var tableBody = $('#tblReporteLiberadas tbody');
            tableBody.empty();
            $.each(data, function (i, item) {
                var row = $('<tr>').append(
                    $('<td>').text(item.idHistoriasALiberar),
                    $('<td>').text(item.idReporte),
                    $('<td>').text(item.descripcion),
                    $('<td>').text(item.nomGrupo),
                    $('<td>').text(item.puntosDeHistoria),
                    $('<td>').text(item.fechaPropuestaOwner),
                    $('<td>').text(item.fechaPropuestaImplement),
                    $('<td>').append($('<input>').attr('type', 'checkbox'))
                );
                tableBody.append(row);
            });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

/*
    Funciones para precargar algunos metodos
*/
$(document).ready(function () {
    $(".divBlanco").click(function () {
        $("#btnasignarFecha").show();
    });
});

$(document).ready(function () {
    ObtenerDatoNumeroHistoriasAgendadas();
    ObtenerDatoNumeroHistoriasLiberadas();
});