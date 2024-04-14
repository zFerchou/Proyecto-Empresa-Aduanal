// Abre el dialogo con las especificaciones dadas modal es (Agregar Servidor)
function AgregarServidor() {
        // Initialize the dialog
        $("#dlogServidor").dialog({
            autoOpen: false, // Dialog is initially hidden
            modal: true,     // Modal dialog
            width: 'auto',   // Auto width
            height: 'auto',  // Auto height
            resizable: false, // Disable resizing
            closeText: "Cerrar", // Close button text
        });

       
        // Open the dialog
        $("#dlogServidor").dialog("open");
        
    };
    

// Funcion para cuando se requiera agrega un servidor
function agregarDatosServidor() {
    $('#btnAgregar').click(function () {
        $.ajax({
            type: 'POST',
            url: 'ClienteServidor.aspx/btnAgregar_Click', // Aquí debes proporcionar la ruta correcta a tu método de servidor
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({
                Categoria: $('#txtCategoria').val(),
                NomInstancia: $('#txtNomInstancia').val(),
                IpPublica: $('#txtIpPublica').val(),
                IpPrivada: $('#txtIpPrivada').val(),
                UrlServidor: $('#txtUrlServidor').val(),
                ZonaDisponible: $('#txtZonaDisponible').val(),
                Estatus: $('#txtEstatus').val(),
                idUsuario: $('#txtIdUsuario').val(),
                idTipoInstancia: $('#txtIdTipoInstancia').val(),
                idApp: $('#txtIdApp').val()
            }),
            success: function (response) {
                // Manejar la respuesta del servidor
                alert(response.d);
            },
            error: function (xhr, status, error) {
                // Manejar errores
                alert('Error: ' + error);
            }
        });
    });
}

// Accion para cuando dan click en "Ver detalles"
function InformacionServidor(idServidor) {


    // Inicializar el diálogo
    $("#dlogInfoServidor").dialog({
        autoOpen: false, // El diálogo está oculto inicialmente
        modal: true,     // Diálogo modal
        width: '70%',    // Establecer el ancho (ajustar según sea necesario)
        height: 'auto',  // Altura automática
        resizable: false, // Deshabilitar redimensionamiento
        closeText: "Cerrar", // Texto del botón de cierre
        open: function () { // Acción al abrir el diálogo
            // Realizar una petición AJAX para obtener la información del servidor
            $.ajax({
                type: "POST",
                url: "Servidor.aspx/ObtenerInformacion",
                data: JSON.stringify({ idServidor: idServidor }), // Corrección aquí
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // Llenar los campos de entrada con la información obtenida del servidor

                    $("#catego").val(response.d.Categoria);
                    $("#nombreInstancia").val(response.d.NomInstancia);
                    $("#ipPublica").val(response.d.IpPublica);
                    $("#ipPrivada").val(response.d.IpPrivada);
                    $("#urlServidor").val(response.d.UrlServidor);
                    $("#zonaDisponible").val(response.d.ZonaDisponible);
                    console.log(response);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.log(xhr.status + ": " + thrownError);

                }
            });
        }
        

    });

    // Centrar el diálogo al redimensionar la ventana
    $(window).resize(function () {
        $("#dlogInfoServidor").dialog("option", "position", { my: "center", at: "center", of: window });
    });

    // Abrir el diálogo
    $("#dlogInfoServidor").dialog("open");

};


 // Crear Tabla para llenar los datos del modulo
function getTabla() {
    $('#GridView1 thead tr').attr("id", "grid-head2");
    $('#GridView1 tbody').attr("id", "grid-body");

    $("#GridView1").dataTable({
        "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
        "stripeClasses": ['gray', 'white'],
        "columnDefs": [
            { className: "txt-left", "targets": [0, 1] }
        ]
    });
    $("#GridView1").on('draw.dt', function () {
        $("#grid-head1").addClass("round-border2");
    });
}


$(document).ready(function () {
    $("#dDialogo").dialog({
        modal: true
    });

    getTabla();
   
   
});








