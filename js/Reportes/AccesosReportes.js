var iAnterior = 0;
var c;

$(document).ready(function () {
    c = document.getElementById('canvas');
    setDPI(c, 300);
    $("#fechaInicio").datepicker({
        dateFormat: "yy-mm-dd",
        firstDay: 1,
        autoOpen: false,
        onClose: function (selectedDate) {
            $("#fechaFin").datepicker("option", "minDate", selectedDate);
        }
    });
    $("#fechaFin").datepicker({
        dateFormat: "yy-mm-dd",
        firstDay: 1,
        autoOpen: false,
        onClose: function (selectedDate) {
            $("#fechaInicio").datepicker("option", "maxDate", selectedDate);
        }
    });
    getReporte(false);
    $("select").on("change", function(){
        getReporte(false);
    });
});

/**
* Función para generar el reporte en PDF.
**/
function getReportePDF(){
    block();
    var grupo = $("#ddlGrupo").val();
    var sistema = $("#ddlSistema").val();
    var fechaInicio = $("#fechaInicio").val();
    var fechaFin = $("#fechaFin").val();

    
    if(grupo != "0" && sistema != "0"){//Se requiere la tabla de usuario y la del sistema
        var imageDataUsuarios = toDataURL($("svg")[0]);
        $('#ddlSistema').prop("selectedIndex", 0);
        getReporte(true);
        var imageDataSistema;
        setTimeout(function(){
            imageDataSistema = toDataURL($("svg")[0]);
            $.ajax({
                url: "AccesosReportes.aspx/GenerarImagenes",
                type: "POST",
                dataType: "JSON",
                data: "{sNombreGrafica:'imgSistema',sBase:'"+imageDataSistema+"',sNombreSegGrafica:'imgUsuario',sBaseSeg:'"+imageDataUsuarios+"'}",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $.ajax({
                        url: "AccesosReportes.aspx/generarPDF",
                        type: "POST",
                        dataType: "JSON",
                        data: "{grupo:'"+$("#ddlGrupo :selected").text()+"', fechaInicio:'"+fechaInicio+"', fechaFin:'"+fechaFin+"', sistema:'"+$("#ddlSistema :selected").text()+"'}",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            unBlock();
                            window.open(data.d, '_blank');
                        }, 
                        error:function(xhr, status, error){
                            alertify.error('Error al generar reporte');
                            unBlock();
                        }
                    });
                }, 
                error:function(xhr, status, error){
                    alertify.error('Error al generar reporte');
                    console.log(error);
                            console.log(xhr);
                            console.log(status);
                    unBlock();
                }
            });
        }, 2000);
    }
    else{ //Solo se requiere la gráfica actual
        var imageDataSistema = toDataURL($("svg")[0]);
            $.ajax({
                url: "AccesosReportes.aspx/GenerarImagen",
                type: "POST",
                dataType: "JSON",
                data: "{sNombreGrafica:'imgSistema',sBase:'"+imageDataSistema+"'}",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $.ajax({
                        url: "AccesosReportes.aspx/generarPDF",
                        type: "POST",
                        dataType: "JSON",
                        data: "{grupo:'"+$("#ddlGrupo :selected").text()+"', fechaInicio:'"+fechaInicio+"', fechaFin:'"+fechaFin+"', sistema:'"+$("#ddlSistema :selected").text()+"'}",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            unBlock();
                            window.open(data.d, '_blank');
                        }, 
                        error:function(xhr, status, error){
                            alertify.error('Error al generar reporte');
                            unBlock();
                        }
                    });
                }, 
                error:function(xhr, status, error){
                    alertify.error('Error al generar reporte');
                    console.log(error);
                            console.log(xhr);
                            console.log(status);
                    unBlock();
                }
            });
    }
}



/**
*Función para poder generar canvas de gráficas Highcharts
**/
(function (H) {
    H.Chart.prototype.createCanvas = function (divId) {
        var svg = this.getSVG(),
            width = parseInt(svg.match(/width="([0-9]+)"/)[1]),
            height = parseInt(svg.match(/height="([0-9]+)"/)[1]),
            canvas = document.createElement('canvas');

        canvas.setAttribute('width', width);
        canvas.setAttribute('height', height);

        if (canvas.getContext && canvas.getContext('2d')) {

            canvg(canvas, svg);

            return canvas.toDataURL("image/jpeg");

        } 
        else {
            alert("Your browser doesn't support this feature, please use a modern browser");
            return false;
        }

    }
}(Highcharts));


/**
*Función para generar la tabla y 
*la gráfica de las entradas a los
*módulos.
**/
function getReporte(noUnBlock) {
    block();
    var fechaInicio = $("#fechaInicio").val();
    var fechaFin = $("#fechaFin").val();
    var grupo = $("#ddlGrupo").val();
    var sistema = $("#ddlSistema").val();

    $.ajax({
        url: "AccesosReportes.aspx/obtenerReporte",
        type: "POST",
        dataType: "JSON",
        data: "{grupo:" + grupo + ",fechaInicio:'" + fechaInicio + "',fechaFin:'" + fechaFin + "' ,sistema: " + sistema + "}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            
            if (data.d != "") {
                $("#lblTblIngresos").html(data.d);
                $("#lblMsg").html("");

                var subtitulo = "";

                if (fechaInicio == "" && fechaFin == "") {
                    subtitulo += "Por cualquier fecha"
                }
                
                if (fechaInicio != "") {
                    subtitulo += "Fecha de Inicio: " + fechaInicio;
                }
                if (fechaFin != "") {
                    subtitulo += " | Fecha Fin: " + fechaFin;
                }

                subtitulo += " | Grupo: " + $("#ddlGrupo :selected").text(); ;
                subtitulo += " | Módulo: " + $("#ddlSistema :selected").text(); ;

                $("#tblDetalleIngresos").dataTable({
                    "dom": '<"#datagrid-layer"<"#grid-head1"lf"round-border2">t<"footer_table"><"#grid-foot"ip>>',
                    "stripeClasses": ['gray', 'white'],
                    "bLengthChange": false,
                });

                $('#container').highcharts({
                    data: {
                        table: 'tblDetalleIngresos',seriesMapping: [{
                            drilldownSeries: 2,
                            drilldown: 3
                        }]
                    },
                    chart: {
                        type: 'column',
                        events: {
                            drilldown: function (e) {
                                if (!e.seriesOptions) {
                                    fechaInicio = $("#fechaInicio").val();
                                    fechaFin = $("#fechaFin").val();
                                    grupo = $("#ddlGrupo").val();
                                    sistema = $("#ddlSistema").val();
                                    chart = this;
                                    chart.showLoading('Cargando...');
                                     $.ajax({
                                        url: "AccesosReportes.aspx/obtenerSerie",
                                        type: "POST",
                                        dataType: "JSON",
                                        data: "{grupo:" + grupo + ",fechaInicio:'" + fechaInicio + "',fechaFin:'" + fechaFin + "' ,sistema: " + sistema + ", name: '" + e.point.name + "'}",
                                        contentType: "application/json; charset=utf-8",
                                        success: function (data) {
                                            if(data.d == "" || data.d == '{"data":[]}'){
                                                chart.hideLoading();
                                                alertify.success("No hay más datos que mostrar");
                                                return;
                                            }
                                            if (grupo == 0 && sistema == 0){
                                                iAnterior = 1;
                                                var index = 0;
                                                $("#ddlGrupo option").each(function(){
                                                    if ($(this).text() == e.point.name) {
                                                        return false;
                                                    }
                                                    index++;
                                                });
                                                $('#ddlGrupo').prop("selectedIndex", index);
                                            }else if (grupo != 0 && sistema == 0){
                                                iAnterior = 2;
                                                var index = 0;
                                                $("#ddlSistema option").each(function(){
                                                    if ($(this).text() == e.point.name) {
                                                        return false;
                                                    }
                                                    index++;
                                                });
                                                $('#ddlSistema').prop("selectedIndex", index);
                                            }else if (grupo == 0 && sistema != 0){
                                                iAnterior = 3;
                                                var index = 0;
                                                $("#ddlGrupo option").each(function(){
                                                    if ($(this).text() == e.point.name) {
                                                        return false;
                                                    }
                                                    index++;
                                                });
                                                $('#ddlGrupo').prop("selectedIndex", index);
                                            }
                                            var dataJSON = JSON.parse(data.d);
                                            chart.hideLoading();
                                            chart.addSeriesAsDrilldown(e.point, dataJSON);
                                            $(chart.legend.allItems[0].legendItem.element.childNodes).text('No. Ingresos');
                                            var subtitulo = "";

                                            if (fechaInicio == "" && fechaFin == "") {
                                                subtitulo += "Por cualquier fecha"
                                            }
                
                                            if (fechaInicio != "") {
                                                subtitulo += "Fecha de Inicio: " + fechaInicio;
                                            }
                                            if (fechaFin != "") {
                                                subtitulo += " | Fecha Fin: " + fechaFin;
                                            }

                                            subtitulo += " | Grupo: " + $("#ddlGrupo :selected").text(); 
                                            subtitulo += " | Módulo: " + $("#ddlSistema :selected").text(); 

                                            chart.setTitle(null, {
                                                text: subtitulo
                                              });
                                        }, 
                                        error:function(xhr, status, error){
                                            chart.hideLoading();
                                            $(chart.legend.allItems[0].legendItem.element.childNodes).text('No. Ingresos');
                                            alertify.error('Error en Obtener Sistemas');

                                        }
                                    });
                                }

                            },
                            drillup: function(){
                                if(iAnterior == 1 || iAnterior == 3){
                                    $('#ddlGrupo').prop("selectedIndex", 0);
                                    iAnterior = 2;
                                }else if(iAnterior == 2){
                                    $('#ddlSistema').prop("selectedIndex", 0);
                                    iAnterior = 1;
                                }
                                var subtitulo = "";

                                            if (fechaInicio == "" && fechaFin == "") {
                                                subtitulo += "Por cualquier fecha"
                                            }
                
                                            if (fechaInicio != "") {
                                                subtitulo += "Fecha de Inicio: " + fechaInicio;
                                            }
                                            if (fechaFin != "") {
                                                subtitulo += " | Fecha Fin: " + fechaFin;
                                            }

                                            subtitulo += " | Grupo: " + $("#ddlGrupo :selected").text(); 
                                            subtitulo += " | Módulo: " + $("#ddlSistema :selected").text(); 

                                            chart.setTitle(null, {
                                                text: subtitulo
                                              });
                                              setTimeout(function(){$(chart.legend.allItems[0].legendItem.element.childNodes).text('No. Ingresos')},100);
                            }
                        }
                    },
                    title: {
                        text: 'Cifra de ingresos a modulos ERP'
                    },
                    subtitle: {
                        text: subtitulo
                    },
                    yAxis: {
                        allowDecimals: false,
                        title: {
                            text: 'Cantidad de Ingresos'
                        }
                    },
                    tooltip: {
                        formatter: function () {
                            return '<b>' + this.series.name + '</b><br/>' +
                    this.point.y + ' ' + this.point.name.toLowerCase();
                        }
                    },
                     plotOptions: {
                        series: {
                            borderWidth: 0,
                            dataLabels: {
                                enabled: true,
                                color: '#2181a3',
                                format: '{point.y}'
                            }
                        }
                    },
                    series: [{
                    }],
                    drilldown: {
                        series: []
                    }
                });

                $("#lblReportePDF").removeClass("hide");
            }else{
                $("#lblReportePDF").addClass("hide");
                $('#container').html("");
                $("#lblTblIngresos").html("");
                $("#lblMsg").html("<div class='bg-alert width98 center clear'><span id='icon-47' class='blanco warning'></span>No hay registros de ingreso para el filtro especificado</div>");
            }

            if(!noUnBlock)
             unBlock();
        },
        error: function (xhr, status, error) {
            if(!noUnBlock)
             unBlock();
            alertify.error('Error en Obtener Sistemas');
        }
    });
}

function toDataURL(SVG) {
  var svg_xml = (new XMLSerializer()).serializeToString(SVG);  
  var ctx = c.getContext('2d');
  ctx.drawSvg(svg_xml, 0, 0);
  return c.toDataURL("image/png"); 
}

function setDPI(canvas, dpi) {
    // Poniendo el tamaño fixed
    canvas.style.width = canvas.style.width || canvas.width + 'px';
    canvas.style.height = canvas.style.height || canvas.height + 'px';

    // Cambiando el tamaño del canvas para luego agregar los dibujos
    var scaleFactor = dpi / 96;
    canvas.width = Math.ceil(canvas.width * scaleFactor);
    canvas.height = Math.ceil(canvas.height * scaleFactor);
    var ctx = canvas.getContext('2d');
    ctx.scale(scaleFactor, scaleFactor);
}