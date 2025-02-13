﻿$(document).ready(function () {

    $("#myModal").dialog({
        modal: true,
        autoOpen: false,
        height: 300,
        width: 600,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            $('#sistemasHist').empty();
            $('#Aggfecha').empty();
        },
        open: function (event, ui) {
            $('#asignarFecha').prop('disabled', !hayCheckboxSeleccionado());
            $('#asignarFecha').click(function (e) {
                e.preventDefault();
            });
        }
    });

    $('#tblHistoriaT').on('change', 'input[type="checkbox"]', function () {
        if (!hayCheckboxSeleccionado()) {
            $('#asignarFecha').removeClass('verde');
            $('#marcarTodos').text('Marcar Todos');
        } else {
            $('#asignarFecha').addClass("verde");

        }
        $('#asignarFecha').prop('disabled', !hayCheckboxSeleccionado());// Habilitar o deshabilitar según la selección de checkbox
    });

    $('#asignarFecha').click(function (e) {
        e.preventDefault();
        datosModal();
        hayCheckboxSeleccionado()
        $("#myModal").dialog("open");
    });

    $('#btnGuardar').click(function (e) {
        // Actualizar los datos de cada sistema en el array de sistemas
        var sistemas = obtenerFilasMarcadas(); // esto agrupa los sistemas
        var dataToShare = []; // alamacena arrays c/u representa un reporte con su fecha
        var reporte = { "idReporte": 0, "idSistema": 0, "dateOwner": Date.now() };

        for (var i = 0; i < sistemas.length; i++) {
            var sistema = sistemas[i]; // esto recupera los datos por sistema
            var idSistema = sistema[0][2];
            var fechaOtorgada = $('#ldate' + sistema[0][2]).val();
            console.log("Sistemas " + [i]);
            console.log(sistemas);
            for (var j = 0; j < sistema.length; j++) {
                var idReporte = sistema[j][1];
                sistema[j] = [idSistema, idReporte, fechaOtorgada];
                console.log("Sistema " + [j]);
                console.log(sistema[j]);
                reporte.idReporte = sistema[j][1];
                reporte.idSistema = sistema[j][0];
                reporte.dateOwner = sistema[j][2];
                console.log(JSON.stringify(reporte));
                dataToShare.push(reporte);
            }
        }
        // Mostrar un mensaje de éxito o realizar cualquier otra acción que desees
        //alert("Los datos se han guardado correctamente.");
        console.log("Nuevos datos");
        console.log(dataToShare);
        console.log("Ejecutando solicituud");
        enviarDatosAlServidor(dataToShare);

    });


    //toggleCheckboxes();


    $('#marcarTodos').click(function (e) {
        var checkboxes = $('#tblHistoriaT input[type="checkbox"]');
        checkboxes.prop('checked', !checkboxes.first().prop('checked'));

        var todosMarcados = $('#tblHistoriaT input[type="checkbox"]:not(:checked)').length === 0;

        if (todosMarcados) {
            $('#marcarTodos').text('Desmarcar');
        } else {
            $('#marcarTodos').text('Marcar Todos');
        }
        if (!hayCheckboxSeleccionado()) {
            $('#asignarFecha').removeClass('verde');
        } else {
            $('#asignarFecha').addClass("verde");
        }
        $('#asignarFecha').prop('disabled', !hayCheckboxSeleccionado());
        e.preventDefault();
    });


    console.log('Ejecutar ajax ...')
    obtenerHistorias();

    if (!hayCheckboxSeleccionado()) {
        $('#asignarFecha').removeClass('verde');
    }

    $('#asignarFecha').prop('disabled', !hayCheckboxSeleccionado());
});

function obtenerHistorias() {
    $.ajax({
        url: "HistoriasTerminadas.aspx/obtenerHistoriasTerminadas",
        type: "POST",
        data: "{}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log('Dentro de ajax...');
            console.log(data);
            mostrarDatos(data);
        },
        error: function (xhr, status, error) {
            alertify.error("<span id='icon-25' class='warning blanco'></span>Error en La obtencion de datos.");
        }
    });

    function mostrarDatos(data) {
        if ($.fn.DataTable.isDataTable('#tblHistoriaT')) {
            $('#tblHistoriaT').DataTable().destroy();
           /* $('#tblHistoriaT').empty();*/
        }
        let table = $("#tblHistoriaT").DataTable({
            "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
            "stripeClasses": ['gray', 'white'],
            "aoColumnDefs": [
                { "sClass": "txt-left", "aTargets": [0, 1] }
            ],
            order: [[2, 'asc'], [3, 'asc']]
        });

        table.clear(); // Limpiar datos existentes en la tabla

        console.log('mostrar datos...');
        for (let i = 0; i < data.d.length; i += 6) {
            let rowData = [];
            let checkbox = $("<input>").attr("type", "checkbox");
            let tdCheckbox = $("<td>").append(checkbox);
            rowData.push(tdCheckbox.prop('outerHTML'));
            for (let j = 0; j < 6; j++) {
                rowData.push(data.d[i + j]);
            }
            console.log(rowData)
            table.row.add(rowData).draw(false); // Agregar fila y dibujar la tabla
        }
        table.rows().every(function () {
            let row = this;
            let cell = row.node().getElementsByTagName('td')[1]; // Obtener la segunda celda
            let cell2 = row.node().getElementsByTagName('td')[3]; // Obtener la segunda celda

            $(cell).css('display', 'none'); // Aplicar estilo para ocultar visualmente la celda
            $(cell2).css('display', 'none'); // Aplicar estilo para ocultar visualmente la celda

        });
    }
}


function obtenerFilasMarcadas() {
    var filasMarcadas = [];
    $('#tblHistoriaT input[type="checkbox"]:checked').each(function () {
        var fila = [];
        $(this).closest('tr').find('td').each(function () {
            fila.push($(this).text());
        });
        filasMarcadas.push(fila);
    });
    filasMarcadas.sort(function (a, b) {
        return a[2] - b[2];
    });
    var filasAgrupadas = [];

    filasMarcadas.forEach(function (fila) {
        var valorPosicion2 = fila[3];
        // Verificar si ya hay un array para este valor
        var grupoExistente = filasAgrupadas.find(function (grupo) {
            return grupo[0][3] === valorPosicion2;
        });
        if (grupoExistente) {
            grupoExistente.push(fila);
        } else {
            filasAgrupadas.push([fila]);
        }
    });

    console.log("Filas agrupadas por sistema:", filasAgrupadas);

    return filasAgrupadas;
}

function datosModal() {
    var sistemas = obtenerFilasMarcadas();
    console.log(sistemas.length)

    for (var i = 0; i < sistemas.length; i++) {
        var sistema = sistemas[i];
        var sistemaId = sistema[0][3];
        var sistemaNombre = sistema[0][4];
        var inputHtml = '<label for="txtNomSis">Sistema: ' + sistemaId + ' </label><br/>'
            + '<input type="text" class="nombreSistema input300" style="margin:2.5px" readonly value="' + sistemaNombre + "(" + sistema.length + ")" + '">'
            + '<input type="hidden" name="idSistema" value="' + sistemaId + '"><br/>';
        var fechaHtml = '<label for="ldate">Asignar Fecha:</label><br/>'
            + '<input class="input30 validar fecha" type="date" id="ldate' + sistemaId + '" name="ldate" style="margin:2.5px"/>'
            + '<span id="icon-25" class="azul calendario calendarioInicio" title="Seleccionar día" style="transition: all 0.3s; cursor: pointer;"></span><br/>'
        $('#Aggfecha').append(fechaHtml);
        $('#sistemasHist').append(inputHtml);

    }
}

function hayCheckboxSeleccionado() {
    return $('#tblHistoriaT input[type="checkbox"]:checked').length > 0;
}


function enviarDatosAlServidor(data) {
    var dataToShare = obtenerDatosParaCompartir(); // obtener los datos a enviar al servidor
    //console.log(dataToShare[0]);
    //dataToShare = data;
    console.log(data);
    console.log(JSON.stringify(dataToShare[0]));
    let datamsg = "Hola 5";
    console.log(datamsg);
    $.ajax({
        url: "HistoriasTerminadas.aspx/InsertarHistoriasOwner",
        type: "POST",
        data: JSON.stringify({ data: JSON.stringify(dataToShare) }), // convertir a JSON
        //data: JSON.stringify({ data: dataToShare }),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            console.log("Objeto JSON enviado exitosamente");
            console.log(response);
            $("#myModal").dialog("close");
            alertify.success("<span id='icon-25' class='success blanco' ></span>Se agregó correctamente la incidencia.");
            obtenerHistorias();
            
            $('#asignarFecha').removeClass('verde');

            $('#asignarFecha').prop('disabled', true);

        },
        error: function (xhr, status, error) {
            console.error("Error al enviar el objeto JSON al servidor");
            console.error(error);
            alertify.error("<span id='icon-25' class='error blanco'></span>No se agregó la incidencia");
        }
    });
}

function obtenerDatosParaCompartir() {
    var sistemas = obtenerFilasMarcadas();
    var dataToShare = [];

    for (var i = 0; i < sistemas.length; i++) {
        var sistema = sistemas[i];
        console.log(sistema);
        for (var j = 0; j < sistema.length; j++) {
            var idSistema = sistema[j][3];
            console.log(idSistema)
            var idReporte = sistema[j][1];
            var fechaOtorgada = $('#ldate' + sistema[0][3]).val();

            var historia = {
                idSistema: idSistema,
                idReporte: idReporte,
                dateOwner: fechaOtorgada
            };

            dataToShare.push(historia);
        }
    }

    console.log('Data to share: ' + JSON.stringify(dataToShare));

    return dataToShare;
}

