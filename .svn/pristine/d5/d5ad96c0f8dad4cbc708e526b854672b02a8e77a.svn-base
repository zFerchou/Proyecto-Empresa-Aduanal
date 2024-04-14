/**
 * Funciones Precargadas
 */
$(document).ready(function () {
    $("#agendadas").hide();
    $("#sinAgendar").hide();
    $("#liberadas").hide();
    $("#contenidoSistemas").hide();
    $("#btnRegresar").click(function () {
        location.reload();
    });
});

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

function ObtenerDatoNumeroHistoriasSinAgendar() {
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerNumeroHistoriasNoAgendadas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response); // Verifica la respuesta del servidor
            var numHistorias = response.d;
            $('#lblNumNoAgendadas').text(response.d); // Actualizar el label con el número de historias
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

/**
 * Funcion para que se muestren los labels
 * se tiene un boton para regresar y ademas
 * se tiene una funcion para ocultar etiquetas
 */
function mostrarSistemasAgendadas(id) {
    ocultarSistemas();
    $("#" + id).show();
    ObtenerNombresHistoriasAgendadas(id);
    $("#agendadas").hide();
    $("#sinAgendar").hide();
    $("#liberadas").hide();
    $("#contenidoSistemas").show();
    $("#contenedorBotonRegresar").show();
}

function mostrarSistemasNoAgendadas(id) {
    ocultarSistemas();
    $("#" + id).show();
    ObtenerNombresHistoriasNoAgendadas(id);
    $("#agendadas").hide();
    $("#sinAgendar").hide();
    $("#liberadas").hide();
    $("#contenidoSistemas").show();
    $("#contenedorBotonRegresar").show();
}

function mostrarSistemasLiberadas(id) {
    ocultarSistemas();
    $("#" + id).show();
    ObtenerNombresHistoriasLiberadas(id);
    $("#agendadas").hide();
    $("#sinAgendar").hide();
    $("#liberadas").hide();
    $("#contenidoSistemas").show();
    $("#contenedorBotonRegresar").show();
}

// Función para ocultar todos los sistemas
function ocultarSistemas() {
    $("#sistemaAgendadas").hide();
    $("#sistemaSinAgendar").hide();
    $("#sistemaLiberadas").hide();
}

/**
* Funcion para que se muestren los
* nombres con historias liberadas, agendadas
* y no agendadas.
*/
function ObtenerNombresHistoriasAgendadas(id) {
    $(".labelsPrincipales").hide();
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerNombresHistoriasAgendadas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response); // Verifica la respuesta del servidor
            // Parsear la respuesta JSON
            var data = JSON.parse(response.d);

            // Verificar si la respuesta es un array
            if (Array.isArray(data)) {
                // Obtener el contenedor donde se mostrarán los nombres de los sistemas
                var sistemaDiv = $('#' + id);

                // Limpiar el contenido existente
                sistemaDiv.empty();

                // Iterar sobre los datos y crear labels para cada nombre de sistema
                data.forEach(function (nombreSistema) {
                    // Crear label con el nombre del sistema
                    var labelSistema = $('<label>').text(nombreSistema).addClass('divBlanco pdtes asig_equipo');

                    // Agregar label al contenedor
                    sistemaDiv.append(labelSistema);

                    // Llamar a la función para mostrar la tabla cuando se hace clic en el sistema
                    labelSistema.click(function () {
                        mostrarTablaAgendadas(nombreSistema);
                        $("#agendadas").show();
                    });
                });
            } else {
                console.error("Los datos no son un array:", data);
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function ObtenerNombresHistoriasNoAgendadas(id) {
    $(".labelsPrincipales").hide();
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerNombresHistoriasNoAgendadas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response); // Verifica la respuesta del servidor

            // Parsear la respuesta JSON
            var data = JSON.parse(response.d);

            // Verificar si la respuesta es un array
            if (Array.isArray(data)) {
                // Obtener el contenedor donde se mostrarán los nombres de los sistemas
                var sistemaDiv = $('#' + id);

                // Limpiar el contenido existente
                sistemaDiv.empty();

                // Iterar sobre los datos y crear labels para cada nombre de sistema
                data.forEach(function (nombreSistema) {
                    // Crear label con el nombre del sistema
                    var labelSistema = $('<label>').text(nombreSistema).addClass('divBlanco pdtes asig_equipo');

                    // Agregar label al contenedor
                    sistemaDiv.append(labelSistema);

                    // Llamar a la función para mostrar la tabla cuando se hace clic en el sistema
                    labelSistema.click(function () {
                        mostrarTablaNoAgendadas(nombreSistema);
                        $("#sinAgendar").show();
                    });
                });
            } else {
                console.error("Los datos no son un array:", data);
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function ObtenerNombresHistoriasLiberadas(id) {
    $(".labelsPrincipales").hide();
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerNombresHistoriasLiberadas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response); // Verifica la respuesta del servidor
            // Parsear la respuesta JSON
            var data = JSON.parse(response.d);

            // Verificar si la respuesta es un array
            if (Array.isArray(data)) {
                // Obtener el contenedor donde se mostrarán los nombres de los sistemas
                var sistemaDiv = $('#' + id);

                // Limpiar el contenido existente
                sistemaDiv.empty();

                // Iterar sobre los datos y crear labels para cada nombre de sistema
                data.forEach(function (nombreSistema) {
                    // Crear label con el nombre del sistema
                    var labelSistema = $('<label>').text(nombreSistema).addClass('divBlanco pdtes asig_equipo');

                    // Agregar label al contenedor
                    sistemaDiv.append(labelSistema);

                    // Agregar función onclick para mostrar la tabla cuando se hace clic en el sistema
                    labelSistema.click(function () {
                        mostrarTablaLiberadas(nombreSistema);
                        $('#liberadas').show();
                    });
                });
            } else {
                console.error("Los datos no son un array:", data);
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function asignarHistoria(newFecha, data, idEstatus) {
    $.ajax({
        url: "sistema.aspx/asignarHistoria",
        type: "POST",
        data: "{ newFecha: '" + newFecha + "', historias: '" + data + "', idEstatus: " + idEstatus + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //verifica que regrese datos
            if (data.d === true) {
                alertify.success("<span id='icon-25' class='success blanco'></span>Se asignaron correctamente las historias al Sprint.");
            } else {
                alertify.error("<span id='icon-25' class='error blanco'></span>No se asignaron las historias al Sprint.");
            }
        },
        error: function (xhr, status, error) {
            alert(status);
            unBlock();
            alert('Los datos del reporte son incorrectos.');
        }
    });
}

function liberarHistoria(newFecha, data, idEstatus) {
    $.ajax({
        url: "sistema.aspx/liberarHistoria",
        type: "POST",
        data: "{ newFecha: '" + newFecha + "', historias: '" + data + "', idEstatus: " + idEstatus + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //verifica que regrese datos
            if (data.d === true) {
                alertify.success("<span id='icon-25' class='success blanco'></span>Se asignaron correctamente las historias al Sprint.");
            } else {
                alertify.error("<span id='icon-25' class='error blanco'></span>No se asignaron las historias al Sprint.");
            }
        },
        error: function (xhr, status, error) {
            alert(status);
            unBlock();
            alert('Los datos del reporte son incorrectos.');
        }
    });
}

function mostrarDatepickerAsignar() {
    $("#fechaFin").datepicker();
}

/*
    Funciones para precargar algunos metodos
*/

$(document).ready(function () {
    ObtenerDatoNumeroHistoriasAgendadas();
    ObtenerDatoNumeroHistoriasSinAgendar();
    ObtenerDatoNumeroHistoriasLiberadas();
    mostrarDatepickerAsignar();

    $(".divBlanco").click(function () {
        $("#btnasignarFecha").show();
    });

    $('.calendario').click(function () {
        mostrarDatepickerAsignar();
    });

    $("#btnAgendarHistoria").click(function (e) {
        const idEstatus = 4
        var fechaAge = document.getElementById("fechaFin").value
        console.log(fechaAge)

        var nvaFecha = new Date(fechaAge)
        var anio = nvaFecha.getFullYear();
        var mes = nvaFecha.getMonth();
        var dia = nvaFecha.getDay();

        var newFecha = anio + "-" + (mes < 10 ? '0' : '') + mes + "-" + (dia < 10 ? '0' : '') + dia;
        console.log(newFecha)

        var historias = $('#tblReporteSinAgendar input[type="checkbox"]:checked').map(function () {
            return this.value;
        }).get();

        var filasMarcadas = [];
        $('#tblReporteSinAgendar input[type="checkbox"]:checked').each(function () {
            var fila = [];
            $(this).closest('tr').find('td').each(function () {
                fila.push($(this).text());
            });
            filasMarcadas.push(fila);
        });

        var historias = [];
        for (var i = 0; i < filasMarcadas.length; i++) {
            var historia = filasMarcadas[i];
            var idHistoria = historia[0];

            historias.push(idHistoria);
        }
        data = historias.join(',');
        console.log(data);

        asignarHistoria(newFecha, data, idEstatus);
    })

    $("#btnliberar").click(function (e) {
        const idEstatus = 6

        var nvaFecha = new Date()
        var anio = nvaFecha.getFullYear();
        var mes = nvaFecha.getMonth();
        var dia = nvaFecha.getDay();

        var newFecha = anio + "-" + (mes < 10 ? '0' : '') + mes + "-" + (dia < 10 ? '0' : '') + dia;
        console.log(newFecha)

        var historias = $('#tblReporteAgendadas input[type="checkbox"]:checked').map(function () {
            return this.value;
        }).get();

        var filasMarcadas = [];
        $('#tblReporteAgendadas input[type="checkbox"]:checked').each(function () {
            var fila = [];
            $(this).closest('tr').find('td').each(function () {
                fila.push($(this).text());
            });
            filasMarcadas.push(fila);
        });

        var historias = [];
        for (var i = 0; i < filasMarcadas.length; i++) {
            var historia = filasMarcadas[i];
            var idHistoria = historia[0];

            historias.push(idHistoria);
        }
        data = historias.join(',');
        console.log(data);

        liberarHistoria(newFecha, data, idEstatus);
    })
});

/**
 * TODO: Mandar la informacion al frontend
 * para que se llenen las tablas dependiendo
 * del sistema que se seleccione
 */

function mostrarTablaAgendadas(nombreSistema) {
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerHistoriasAgendadas",
        data: JSON.stringify({ sistema: nombreSistema }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response); // Verifica la respuesta del servidor

            // Convertir la cadena JSON en un objeto
            var data = JSON.parse(response.d);

            console.log(data);

            // Limpiar la tabla de historias agendadas
            $("#tblReporteAgendadas tbody").empty();

            // Llenar la tabla con los datos recibidos del servidor
            data.forEach(function (historia) {
                // Crear una fila de la tabla
                var fila = $("<tr>");

                // Llenar la fila con los datos de la historia
                fila.append($("<td>").text(historia.idHistoriasALiberar))
                fila.append($("<td>").text(historia.folio));
                fila.append($("<td>").text(historia.descripcion));
                fila.append($("<td>").text(historia.nomGrupo));
                fila.append($("<td>").text(historia.puntosDeHistoria));
                fila.append($("<td>").text(formatDate(historia.fechaPropuestaOwner)));
                fila.append($("<td>").html("<input type='checkbox'>"));

                // Agregar la fila a la tabla
                $("#tblReporteAgendadas tbody").append(fila);
            });
        },

        failure: function (response) {
            alert(response.d);
        }
    });
}

function mostrarTablaNoAgendadas(nombreSistema) {
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerHistoriasNoAgendadas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ sistema: nombreSistema }),
        success: function (response) {
            console.log(response); // Verifica la respuesta del servidor
            var data = JSON.parse(response.d);
            var tableBody = $('#tblReporteSinAgendar tbody');
            tableBody.empty();
            $.each(data, function (i, item) {
                var row = $('<tr>').append(
                    $('<td>').text(item.idHistoriasALiberar),
                    $('<td>').text(item.folio),
                    $('<td>').text(item.descripcion),
                    $('<td>').text(item.nomGrupo),
                    $('<td>').text(item.puntosDeHistoria),
                    $('<td>').text(formatDate(item.fechaPropuestaOwner)),
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



function mostrarTablaLiberadas(nombreSistema) {
    $.ajax({
        type: "POST",
        url: "sistema.aspx/ObtenerHistoriasLibaradas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ sistema: nombreSistema }),

        success: function (response) {
            console.log(response); // Verifica la respuesta del servidor
            var data = JSON.parse(response.d);
            var tableBody = $('#tblReporteLiberadas tbody');
            tableBody.empty();
            $.each(data, function (i, item) {
                var row = $('<tr>').append(
                    $('<td>').text(item.folio),
                    $('<td>').text(item.descripcion),
                    $('<td>').text(item.nomGrupo),
                    $('<td>').text(item.puntosDeHistoria),
                    $('<td>').text(formatDate(item.fechaPropuestaOwner)),
                    $('<td>').text(formatDate(item.fechaPropuestaImplement))
                );
                tableBody.append(row);
            });
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function formatDate(dateString) {
    var match = dateString.match(/\/Date\((\d+)\)\//);
    if (match) {
        var date = new Date(parseInt(match[1], 10));
        return `${date.getDate()} / ${date.getMonth() + 1}/ ${date.getFullYear()}`;
    }
    return dateString;
}