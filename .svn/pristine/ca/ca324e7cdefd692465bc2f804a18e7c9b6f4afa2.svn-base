<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Actividades.aspx.cs" Inherits="Configuracion_Actividades_Actividades" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../js/Actividades/Actividades.js" type="text/javascript"></script>
    <script src="../../js/site.js" type="text/javascript"></script>
    <script src="../../js/Sistema/SimpleAjaxUploader.js" type="text/javascript"></script>
    <link href="../../css/Sistema/forms.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="clear left width98" id="btnAdd">
    </div>

    <div id="divFormulario" class="hide form-layer clear" >
        <div class="form-tit round-border3">
		    <div class="left" ><h3 class="left" id="lblTipo"></h3></div>
		    <div class="right"><a href="#" onclick="javascript:cerrarFormulario(); javascript:limpiarCampos();"><span id="icon-25" class="verde cerrar cursor-link"></span></a></div>
            <div class="clear left width50">   
            </div>
            <div class="clear center width98">
                <div class="left width50">
                    <br />
                    <div class="left width50" id="btnCargaEvidencia">
                        <a id="btnEvidencia" href="#" class="btn azul shadow2">Seleccionar evidencia</a>
                    </div>
                
                    <div class="left width50">
                        <div id="lblEvidenciaM"></div>
                    </div>
                </div>
            </div>
	    </div>
    </div>

    <div class="clear center"  style="padding-top:50px;">
        <div id="divSinIncidencias" class="hide width98 txt-right">
            <br /><br /><div class='center bg-alert width98 clear txt-left'><span id='icon-47' class='warning blanco'>No se ha generado ninguna incidencia</span></div> <br /> <br />
        </div>
        <asp:Label ID="lblTabla" runat="server"></asp:Label>
        <asp:Label ID="lblTablaLog" runat="server"></asp:Label>
    </div>
    <asp:TextBox runat="server" class="hide" ID="txtidUser"></asp:TextBox>
    <asp:TextBox runat="server" class="hide" ID="txtTipoUsuario"></asp:TextBox>
</asp:Content>

