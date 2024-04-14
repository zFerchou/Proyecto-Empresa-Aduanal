<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="CuotasClientes.aspx.cs" Inherits="Configuracion_Clientes_CuotasClientes" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="Server">
    
    <script src="../../js/Sistema/jquery.tokeninput.js" type="text/javascript"></script>
    <link href="../../css/reportescss/token-input-facebook.css" rel="stylesheet" type="text/css" />
    <script src="../../js/Clientes/CuotasClientes.js"></script>
    <link href="../../css/clientescss/ClientesCss.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server">
    <div id="form-layer-98">
        <div class="alert clear form-layer-98 center frm">
            <h3 class="txt-azul">CLIENTES |<span id="icon-25" class="pagar txt-verde">CUOTAS POR SISTEMAS</span></h3>
            <div class="form-layer-width98" id="divConten">
                <!--Filtro por Grupo o por sistema.  -->
                <h3 class="txt-azul"><a href="javascript:;" onclick="javascript:consulGrupo();" title="Ver la lista de los grupos">GRUPOS</a> | <a href="javascript:;" onclick="javascript:consulSistem();" title="Ver la lista de los sistemas">SISTEMAS</a></h3>

                <div class="left width98 hide" id="divSistemas">

                <%--NUEVO DIV DE FILTROS --%>
                <div class="left width98" id="divFiltrosBusqueda">
                    <div class="left width20" id="divGrupo">
                        <div class="left width98 margin-top">
                            <label for="txtGrupo">Grupo:</label>
                        </div>
                        <div class="left width98 margin">
                            <input type="text" id="txtNomGrupo" value="" placeholder="Ej: DANA"/>
                            <input type="hidden" id="txtIdGrupo" value="" />
                        </div>
                    </div>
                    <div class="left width20" id="divSistema">
                        <div class="left width98 margin-top">
                            <label for="">Sistema:</label>
                        </div>
                        <div class="left width98 margin">
                            <input type="text" id="txtNomSistema" value="" placeholder="Ej: Sistema de Gestión ..."/>
                            <input type="hidden" id="txtIdSistema" value="" />
                        </div>
                    </div>
                    <div class="left width20" id="divFechaInicio">
                        <div class="left width98 margin-top">
                            <label for="">Fecha Inicio:</label>
                        </div>
                        <div class="left width98 margin">
                            <input type="text" id="txtFechaInicio" value="" placeholder="Ej: 2016-02-01"/>
                            <a>
                                <span id="icon-25" class="azul calendario calendarioInicio" title="Seleccionar día" style="transition: all 0.3s; cursor: pointer;"></span>
                            </a>
                        </div>
                    </div>
                    <div class="left width30" id="divFechaFin">
                        <div class="left width98 margin-top">
                            <label for="txtFechaVencimiento">Fecha Vencimiento:</label>
                        </div>
                        <div class="left width98 margin">
                            <input type="text" id="txtFechaVencimiento" value="" placeholder="Ej: 2016-02-02"/>
                            <a>
                                <span id="icon-25" class="azul calendario calendarioFin" title="Seleccionar día" style="transition: all 0.3s; cursor: pointer;"></span>
                            </a>
                            <a href="#" id="btnBuscar" class="btn verde shadow2 borde-blanco"><span id="icon-25" class="blanco buscar">BUSCAR</span></a>
                            <a id="btnVerTodos" title="Ver la lista de todas las Cuotas">Ver Todos</a>
                        </div>
                    </div>
                    <div class="left width10 hide" id="divReturn">
                        <a href="javascript:;" onclick="javascript:consulGrupo();"><span id="icon-47" class="enviar">Regresar</span> </a>
                    </div>

                </div>
                <%-- FIN NUEVO DIV DE FILTROS --%>
                <br />
                <br />
                <br />
                <br />
                <br />
                <div id="divTblCuotas"></div>
                
               </div>
               <div class="left width98 hide" id="divGrupos">
                    <div id="divTblCuotas2"></div>
               </div>

             </div>
        </div>
    </div>

    <%-- dialog modificar cuota --%>
        <div id="dlogModificarCuotas" class="hide" title="Modificar Cuota" style="display: none">
        <div class="clear width98 left frm">
            <div class="width98 clear">
                <div id="resp"><span id="icon-47" class="usuarios"><label class="txt-verde">Responsable(s):</label></span></div>
                <div id="auto"></div>
                <div id="responsable"></div>
            </div>
            <div class="width10 left">
                <span id="icon-47" class="gastos_fijos"></span>
            </div>
            <div class="width40 left">
                <label for="txtCuotaM" class="txt-verde">Cuota:</label><br />
                <input id="txtCuotaM" type="text" class="input100" minlength="2" maxlength="8" /><label id="cboMoneda"></label> 
                <span id="alertaCuota" class="requerido-evidencia hide">Campo requerido</span>               
            </div>
        </div>
        <div class="clear">
            <br />
        </div>
        <div class="clear width98 left frm">
            <div class="width10 left">
                <span id="icon-47" class="calendario"></span>
            </div>
            <div class="width40 left">
                <label for="txtFechaInicioM" class="txt-verde">Fecha de Inicio:</label><br />
                <input id="txtFechaInicioM" type="text" class="input150" />                 
                <span id="icon-25" class="azul calendario calendarioInicioM" title="Seleccionar día" style="transition: all 0.3s; cursor: pointer;"></span>
                <span id="alertaFI" class="requerido-evidencia hide">Campo requerido</span>
                
            </div>
            <div class="width40 left">
                <label for="txtFechaVencimientoM" class="txt-verde">Fecha de Vencimiento:</label><br />
                <input id="txtFechaVencimientoM" type="text" class="input150" />                
                <span id="icon-25" class="azul calendario calendarioFinM" title="Seleccionar día" style="transition: all 0.3s; cursor: pointer;"></span>
                <span id="alertaFF" class="requerido-evidencia hide">Campo requerido</span>
                
            </div>
        </div>

        <div class="clear width98 txt-right">
            <input type="hidden" value="" id="txtIdCuota"/>
            <a class="btn verde" id="btnModificarCuota">GUARDAR</a>
            <a class="btn blanco" onclick="javascript:$('#dlogModificarCuotas').dialog('close');">CANCELAR</a>            
        </div>            
        <div class="clear">
            <br />
        </div>
    </div>
    <%-- fin dialog modificar cuota --%>
        <%-- DIALOG ELIMINAR REPORTE --%>
    <div class="hide" id="dlogEliminarCuota" title="Eliminar">
       <p id="frm">¿Seguro de eliminar la Cuota?</p>   <br />           
        <div class="clear width98 txt-right center">
            <input type="button" class="btn verde" id="btnSi" value="ACEPTAR"/>
            <input type="button" class="btn blanco" id="btnNo" onclick="javascript: $('#dlogEliminarCuota').dialog('close');" value="CANCELAR"/>
            
        </div>
    </div>
    <%-- FIN DIALOG ELIMINAR REPORTE --%>

    <input type="hidden" id="txtidERPGS" />
</asp:Content>
