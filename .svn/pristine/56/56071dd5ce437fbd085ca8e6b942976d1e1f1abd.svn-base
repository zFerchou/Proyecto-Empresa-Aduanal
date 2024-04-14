<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Sprint.aspx.cs" Inherits="Configuracion_Sprint_Sprint" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../js/Sprint/Sprint.js" type="text/javascript"></script>
    <script src="../../js/site.js" type="text/javascript"></script>
    <script src="../../js/Sistema/SimpleAjaxUploader.js" type="text/javascript"></script>
    <link href="../../css/Sistema/forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <div class="clear center"  style="padding-top:50px;">
        <div id="divSinIncidencias" class="hide width98 txt-right">
            <br /><br /><div class='center bg-alert width98 clear txt-left'><span id='icon-47' class='warning blanco'>No se ha generado ninguna incidencia</span></div> <br /> <br />
        </div>

        <asp:Label ID="lblTabla" runat="server"></asp:Label>
        <asp:Label ID="lblTablaLog" runat="server"></asp:Label>
    </div>
    <asp:TextBox runat="server" class="hide" ID="txtidUser"></asp:TextBox>
    <asp:TextBox runat="server" class="hide" ID="txtTipoUsuario"></asp:TextBox>

     <%-- DIALOG PARA DETALLE DEL SPRINT --%>
    <div class="hide" id="dlogReporte" title="Detalle de Sprint">
        <div class="alert clear width90 center frm">
            <div id="divDetalle">
            </div>
            <div id="divTblDetalle"></div>
        </div>
  
    </div>
    <%-- FIN DIALOG PARA EL DETALLE DEL SPRINT --%>
</asp:Content>

