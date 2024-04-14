$(document).ready(function () {
    //Dialog para el datelle del Reporte
    $("#dlogReporte").dialog({
        modal: true,
        autoOpen: false,
        height: 600,
        width: 800,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            //Funciones a ejecutar al cerrar el dialog del detalle de reporte
        },
        open: function (event, ui) {
            $(function () {
                $("#txtFechaPlazo").datepicker();
            });
        }
    });

    //Dialog para la lista de Responsables y Apoyo
    $("#dlogLstApoyo").dialog({
        modal: true,
        autoOpen: false,
        height: 500,
        width: 600,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            //Funciones a ejecutar al cerrar el dialog del detalle de reporte
            $('#txtIdApoyo').val('');
            $('#txtApoyo').val('');
        },
        open: function (event, ui) {                            
            cargarResponsables("#txtIdApoyo", "#txtApoyo", "obtenerPersonalApoyo");            
            tblPersonalApoyo(4);
            tblPersonalResponsable(4);
        }
    });

    
    $('#btnDialog').click(function () {
        $('#dlogReporte').dialog("open");
        obtenerDetalleReporte();
    });

    $('#btnCerrarDlogDetalleReporte').click(function (e) {
        e.preventDefault();
        $('#dlogReporte').dialog("close");
    });

    $('#btnCerrarDlogResponsables').click(function (e) {
        e.preventDefault();
        $('#dlogLstApoyo').dialog("close");
    });
    
    //CLIC EN APOYO PARA VER EL DIALOG PARA AGREGAR PERSONAL DE APOYO
    $('.lblResponsables').click(function () {
        $('#dlogLstApoyo').dialog("open");
    });

    //Clic en el boton de guardar persona de Apoyo
    $('.agregar-apoyo').click(function () {
        guardarPersonaApoyo();
    });
        
    
});//FIN DOCUMENT READY

//Al dar clic en el icono de calendario mostrar el datePicker
function mostrarDatepicker() {
    $("#txtFechaPlazo").datepicker("show");
}

//Función para obtener las personas de Apoyo para el reporte
function tblPersonalApoyo(idReporte) {
    $.ajax({
        url: "Dialogs.aspx/tblPersonalApoyo",
        type: "POST",
        data: "{idReporte:'" + idReporte + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {            
            $('#divTblApoyo').html(data.d);
        },
        error: function (xhr, status, error) {
            alert('Error en tblPersonalApoyo, idReporte: '+idReporte);
        }
    });
}

//Función para obtener las personas de Apoyo para el reporte
function tblPersonalResponsable(idReporte) {
    $.ajax({
        url: "Dialogs.aspx/tblPersonalResponsable",
        type: "POST",
        data: "{idReporte:'" + idReporte + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            $('#lblResponsable').text(data.d);
        },
        error: function (xhr, status, error) {
            alert('Error en tblPersonalApoyo, idReporte: ' + idReporte);
        }
    });
}

//Guardar a las personas que van a estar como Apoyo en un Reporte
function guardarPersonaApoyo() {
    var idResponsable = $('#txtIdApoyo').val();
    var idReporte = 4;
    $.ajax({
        url: "Dialogs.aspx/guardarPersonaApoyo",
        type: "POST",
        data: "{idReporte:" + idReporte + ", idResponsable:"+idResponsable+"}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {            
            if (data.d == true) {
                alert('Se Guardo Correctamente la Información');
                $('#tblApoyo').empty();
                tblPersonalApoyo(4);
            } else {
                alert('No se Guardo la Información');
            }
        },
        error: function (xhr, status, error) {
            alert('Error en agregarPersonaApoyo, idReporte: ' + idReporte+' idResponsable: '+idResponsable);
        }
    });
}

//Eliminar a las personas que están como Apoyo en un Reporte
function eliminarPersonaApoyo(idResponsable) {    
    var idReporte = 4;
    $.ajax({
        url: "Dialogs.aspx/eliminarPersonaApoyo",
        type: "POST",
        data: "{idReporte:" + idReporte + ", idResponsable:" + idResponsable + "}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            alert(data.d);
            if (data.d == true) {
                alert('Se Eliminó Correctamente la Información');
                $('#tblApoyo').empty();
                tblPersonalApoyo(4);
            } else {
                alert('No se Eliminó la Información');
            }
        },
        error: function (xhr, status, error) {
            alert('Error en agregarPersonaApoyo, idReporte: ' + idReporte + ' idResponsable: ' + idResponsable);
        }
    });
}