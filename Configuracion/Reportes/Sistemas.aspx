<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Sistemas.aspx.cs" Inherits="Configuracion_Reportes_Sistemas" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="Server">
    <link href="../../css/Sistema/bootstrap-3.3.6/dist/css/bootstrap.css" rel="stylesheet" />
    <link href="../../css/csserp/TrackingObjetosERP.css" rel="stylesheet" />

    <script src="../../css/Sistema/bootstrap-3.3.6/dist/js/bootstrap.min.js"></script>
    <script src="../../js/Reportes/TrackingObjetosERP/TrackingObjetosERP.js"></script>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server">
    <asp:TextBox runat="server" ID="txtIdReporte"></asp:TextBox>
    <asp:TextBox runat="server" ID="txtIdUsuario"></asp:TextBox>
    <%--<h1 class="tit">ERP <strong>│</strong> SISTEMAS</h1>--%>
    <%-- SECCIÓN PARA VER EL GRUPO Y SISTEMAS --%>
    <div class="left width98 form-layer" id="filtrosAsignados">
        <div class="width98 clear">
            <div class="width80 left">
                <h2 class="txt-azul"><span id="icon-47" class="empleado"></span>GRUPO: <span class="lblGrupo txt-verde"></span></h2>
            </div>
            <div class="width20 left">
                
            </div>
        </div>
        <br />
        <a href="#a" id="btnVerHistorial" class="btn2 azul shadow" title="Ver el registro de las acciónes realizadas"><span id="icon-25" class="blanco docu_rel">Ver Historial</span></a>
        <a href="#a" id="btnTerminar" class="btn2 azul shadow" title="Terminar"><span id="icon-25" class="blanco ticket">Terminar</span></a>
        <div id="divSistemas" class="width98 clear">
            <div id="mainbox">
                <%-- Aquí se agregaran los sistemas solicitados por el grupo --%>
            </div>

        </div>
        <br />
        <%-- PINTAR TRAKIN PARA CADA SISTEMA --%>
        <div id="divTrakingEmp"></div>
        <div id="divTraking">
        </div>
        
        <%-- FIN PINTAR TRAKIN PARA CADA SISTEMA --%>

        <%-- INPUT PARA OBTENER EL ID Y NOMBRE DEL GRUPO --%>
        <input type="hidden"  value="" id="txtIdGrupo" />
        <input type="hidden"  value="" id="txtNomGrupo" />
        <input type="hidden"  value="" id="txtIdsSistemas" />
        <%-- FIN INPUT PARA OBTENER EL ID Y NOMBRE DEL GRUPO --%>

    </div>
    <%-- FIN SECCIÓN PARA VER GRUPO Y SISTEMAS --%>

    <%-- DIALOG PARA VER EL HISTORIAL DE EJECUCIONES --%>
    <div class="" id="dlogHistorial" title="Historial de Ejecuciones">
        <div id="divTblHistorial"></div>
        <br />
        <div class="clear width98 txt-right center">  
              <input type="button" name="name" value="CERRAR" id="btnCerrarDlogHistorial" class="btn blanco"/>   
        </div>        
    </div>
    <%-- DIALOG PARA VER EL HISTORIAL DE EJECUCIONES --%> 
    
    <%-- DIALOG PARA VER EL HISTORIAL DE EJECUCIONES --%>
    <div class="" id="dlogSGI" title="Ruta SGI">
        <label for="txtRutaSGI">URL:</label>
        <input type="text" class="input200" id="txtRutaSGI" placeholder="Eje. https://www.lca-sgi.com"/>
        <div class="clear width98 txt-right center">  
            <input type="button" value="CERRAR" id="btnCerrarDlogSGI" class="btn blanco"/>
            <input type="button" value="GUARDAR" id="btnGuardarSGI" class="btn verde"/>
        </div>        
    </div>
    <%-- DIALOG PARA VER EL HISTORIAL DE EJECUCIONES --%>
</asp:Content>
