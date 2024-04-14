<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Configuracion.aspx.cs" Inherits="Configuracion_Configuracion_Configuracion" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/reportescss/token-input-facebook.css" rel="stylesheet" type="text/css" />
    <script src="../../js/Configuracion/configuracion.js" type="text/javascript"></script>
    <script src="../../js/Sistema/jquery.tokeninput.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">
    
    <div class="clear width98 center">
        <h3 class="tit"><span>Configuración de permisos</span><a id="btnAgregarUsuario" href="#" class="btn verde shadow2 borde-blanco" onclick="javascript:;"><span id="icon-25" class="blanco agregar">Agregar Usuario</span></a></h3>
    </div>
    <div class="clear width98 center">
        <div class="left width50">
            <asp:Label ID="lblTblPuestoUsuarios" class="hide" runat="server"></asp:Label>
        </div>
        <div class="left width50">
            <asp:Label ID="lblTblUsuarios" runat="server"></asp:Label>
        </div>
    </div>
    <div class="clear"><br /></div>
    <div id="dlogPermisos" class="hide">
        <div class="clear width98 center">
            <div id="lblChecksPermisos"></div>
            <div id="agregaGrupo" class="width98 hide"></div>
        </div>
        <div class="clear"><br /></div>
        <div class="clear width98 txt-right">
            <label id='lblBtnAceptar'></label>
            <a class="btn blanco" onclick="$('#dlogPermisos').dialog('close');">Cancelar</a>
        </div>
        <div class="clear"><br /></div>
    </div>

    <div class="clear"><br /></div>
    <div id="dlogPermisosI" class="hide">
        <div class="clear width98 center">
            <div id="lblChecksPermisosI"></div>
            <div id="agregaGrupoI" class="width98 hide"></div>
        </div>
        <div class="clear"><br /></div>
        <div class="clear width98 txt-right">
            <label id='lblBtnAceptarI'></label>
            <a class="btn blanco" onclick="$('#dlogPermisosI').dialog('close');">Cancelar</a>
        </div>
        <div class="clear"><br /></div>
    </div>

    <asp:TextBox runat="server" ID="txtid" class="hide"></asp:TextBox>


    <%-- DIALOG PARA LISTA DE APOYO PARA REPORTES --%>
    <div class="hide" id="dlogAgregarUsuario" title="Ingresa la Información del Usuario">
        <form id="" method="post" action="">
            <div class="clear width98 center frm" id="">
                <div class="left width50">
                    <label for="txtNombre">Nombre<span class="red" title="Campo Requerido">*</span>:</label><br>
                    <input class="input300 validar" type="text" id="txtNombre">
                </div>
                <div class="left width50">
                    <label for="txtApp">Apellido Paterno:</label><br>
                    <input class="input300" type="text" id="txtApp">
                </div>
            </div>
            <div class="clear width98 center frm" id="">
                <div class="left width50">
                    <label for="txtApm">Apellido Materno:</label><br>
                    <input class="input300" type="text" id="txtApm">
                </div>
                <div class="left width50">
                    <label for="txtCorreo">Correo electrónico<span class="red" title="Campo Requerido">*</span>:</label><br>
                    <input class="input300 validar" type="text" id="txtCorreo">
                </div>
            </div>
            <div class="clear width98 center frm" id="">
                <div class="left width50">
                    <label for="txtGrupo">Grupo<span class="red" title="Campo Requerido">*</span>:</label><br>
                    <input class="input300 validar" type="text" id="txtGrupo"/>
                    <input type="hidden"  value="" id="txtIdGrupo"/>
                </div>
                <div class="left width50">
                    <label for="txtArea">Área<span class="red" title="Campo Requerido">*</span>:</label><br>
                    <input class="input300 validar" type="text" id="txtArea">
                    <input type="hidden" value="" id="txtIdArea"/>
                </div>
            </div>

            <br>
            <br>
            <br>
            <br>
        </form>
        <br />
        <div class="clear width98 txt-right center">
            <a href="#" class="btn verde" id="btnGuardarUsuario">ACEPTAR</a>
            <a href="#" class="btn blanco" id="btnCerrar">CERRAR</a>
        </div>
    </div>
    <%-- FIN DIALOG PARA LISTA DE APOYO PARA REPORTE --%>
    <%--INICIO DIALOG AGREGAR GRUPO--%>
    <div class="hide" id="dlogAgregarGrupo" title="Agregar Grupo">
        <span id="icon-47" class="informacion verde"></span>
        <label class="frm" style="color:#166889 !important;">Se agregará un nuevo grupo</label>
        <div class="clear width98 txt-right">
            <a href="#" class="btn verde" id="btnAgregarGpo">Aceptar</a>
            <a href="#" class="btn blanco" id="btnCancelarGpo">Cancelar</a>
        </div>
    </div>
    <%--FIN DIALOG AGREGAR GRUPO--%>
</asp:Content>