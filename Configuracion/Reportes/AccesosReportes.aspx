<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AccesosReportes.aspx.cs" Inherits="Configuracion_Reportes_AccesosReportes" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/reportescss/GestionReportesCss.css" rel="stylesheet" type="text/css" />
    <script src="../../js/Sistema/Highcharts-4.2.5/highcharts.js" type="text/javascript"></script>
    <script src="../../js/Sistema/Highcharts-4.2.5/modules/data.js" type="text/javascript"></script>
    <script src="../../js/Sistema/Highcharts-4.2.5/modules/exporting.js" type="text/javascript"></script>
    <script src="../../js/Sistema/Highcharts-4.2.5/modules/drilldown.js" type="text/javascript"></script>

    <script src="../../js/Sistema/jspdf.js" type="text/javascript"></script>
    <script src="../../js/Sistema/rgbcolor.js" type="text/javascript"></script>
    <script src="../../js/Sistema/canvg.js" type="text/javascript"></script>
    <script src="../../js/Sistema/jspdf.plugin.autotable.src.js" type="text/javascript"></script>
    <script src="../../js/Reportes/accesosReportes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server">
    <div class="clear width98 center">
        <div class="left width50">
            <h1 class="tit">Clientes <strong>│</strong> <span id="icon-47" class="reporte"></span>Reportes de Accesos</h1>
        </div>
        <div class="left width50">
            <h3 id="lblReportePDF" class="txt-right hide"><a href="javascript:;" onclick="javascript:getReportePDF();"><span id="icon-47" class="pdf">Generar Reporte</span></a></h3>
        </div>
    </div>
    <div class="bg-primary clear">
            <canvas id="canvas" width="1000px" height="600px" class="hide"></canvas> 
            <div class="width100 clear">
                <div class="width20 left">
                    <span>Fecha Inicio:</span>
                </div>
                <div class="width20 left">
                     <span>Fecha Fin:</span>
                </div>
                <div class="width20 left">
                    <span> Grupo: </span>
                </div>
                <div class="width20 left">
                    <span>Sistema:</span>
                </div>
            </div>
            <div class="width100 clear">
                <div class="width20 left">
                    <input type="text" id="fechaInicio" class="input200" /><span id="icon-25" class="blanco calendario" onclick="javascript:mostrarCalendarioInicio();"></span>
                </div>
                <div class="width20 left">
                    <input type="text" id="fechaFin" class="input200" /><span id="icon-25" class="blanco calendario" onclick="javascript:mostrarCalendarioFin();"></span>
                </div>
                <div class="width20 left">
                    <asp:DropDownList ID="ddlGrupo" class="select input200" runat="server"></asp:DropDownList>
                </div>
                <div class="width20 left">
                    <asp:DropDownList ID="ddlSistema" class="select input300" runat="server"></asp:DropDownList>
                </div>

             </div>
              <div class="width100 clear"></div>
            </div>
            
    <div class="clear"><br /></div>


    <div class="form-layer-width98">
        <div class="clear width98 center">
            <label id="lblMsg"></label>
            <div id="container"></div>
        </div>
        <div class="clear"><br /></div>
        <div class="clear width98 center">
            <label id="lblTblIngresos"></label>
        </div>
        <div class="clear"><br /></div>
    </div>

</asp:Content>

