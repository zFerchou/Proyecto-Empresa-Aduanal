<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Reportes.aspx.cs" Inherits="Configuracion_Reportes_Reportes" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../js/Sistema/uploadIfy/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../../js/Reportes/autocompleteGrupo.js" type="text/javascript"></script>
    <script src="../../js/Sistema/uploadIfy/jquery.uploadify.js" type="text/javascript"></script>
    <script src="../../js/Reportes/reportes.js" type="text/javascript"></script>
    <script src="../../js/site.js" type="text/javascript"></script>
    <script src="../../js/Sistema/SimpleAjaxUploader.js" type="text/javascript"></script>
    <link href="../../css/reportescss/alertaReporte.css" rel="stylesheet" type="text/css" />
    <link href="../../css/Sistema/forms.css" rel="stylesheet" type="text/css" />

    <style>
        .col-reporte {
            padding-left: 0.80rem !important;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">
    
<div class="clear left width98" id="btnAdd">
</div><br />
<div class="clear width98"><br /><br /></div>
<div id="tipoIncidencia" class="hide form-layer clear">
<div class="form-tit round-border3">
        <div class="left"><h3 class="left">¿Tipo de Acción?</h3></div>
        <div class="right"><a href="#" onclick="javascript:cerrarTI();"><span id="icon-25" class="verde cerrar cursor-link"></span></a></div>
        <div class="width98">
        <div class="clear center width98 frm">
            <label> ¿Que desea Agregar?</label><br /><br />
            <input onchange="javascript:verFormulario(1);" id="c1" type="checkbox" class="check" value="1" />
            <label for="c1">Incidencia</label>
            <input onchange="javascript:verFormulario(2);" id="c2" type="checkbox" class="check" value="2" />
            <label for="c2">Consulta</label>
        </div>
        <div class="clear right width70 frm"><br />
        <a class="btn blanco" onclick="javascript:cerrarTI();">Cancelar</a>
        </div>
        </div>
    </div>
</div>
<%--Inicio del formulario para Agregar nuevo reporte--%>
<div id="divFormulario" class="hide form-layer clear" >
    <div class="form-tit round-border3">
		<div class="left" ><h3 class="left" id="lblTipo"></h3></div>
		<div class="right"><a href="#" onclick="javascript:cerrarFormulario(); javascript:limpiarCampos();"><span id="icon-25" class="verde cerrar cursor-link"></span></a></div>
        <div class="clear center width98">
            <div class="left width50" id="ddlTReporte">
            <%--<input type="hidden" value=" " id="txtIdReporte"/>--%>
<%--                <label for="ddlTipoReporte" class="frmReporte">Tipo Reporte: </label> <br />
                <asp:DropDownList ID="ddlTipoReporte" runat="server" class="select inputP50 validaCombo" onchange="javascript:tipoReporte();" > </asp:DropDownList>--%>
            </div>
            <div class="left width50">
                <label for="ddlPrioridad" class="frmReporte">Prioridad: </label> <br />
                <asp:DropDownList ID="ddlPrioridad" runat="server" class="select inputP50 validaCombo"> </asp:DropDownList>
            </div>
        </div>
        <div class="clear center width98">
            <div class="left width50">
            <div id="nGpo"><asp:Label runat="server" class="frmReporte" ID="txtNombreGrupo"></asp:Label></div>
                <div id="grupo"></div>
                <%--<label for="ddlERPGrupo" class="frmReporte">Grupo: </label> <br />--%>
                 
                
                <asp:Label Text="Grupo:" runat="server" ID="ddlERPGrup" CssClass="frmReporte hide"/>
                <%--<div id="ddlGrupo" class="hide">
                    <asp:DropDownList ID="ddlERPGrupo" runat="server" class="select inputP98 validaCombo" onchange="javascript:ddlSistemas();">

                    </asp:DropDownList>
                    <asp:Label Text="" ID="lblGrupoExterno" runat="server" />
                </div>--%>
                <asp:Label ID="lblGrupoUsuario" runat="server"></asp:Label>
                <div class="clear center width98">
                            
                        <%--<input type="hidden" id="txtIdGrupo"/>--%>
                            <input type="text" id="txtGrupo" class="hide inputP50" style="text-transform:uppercase;"  placeholder="Escriba el nombre del Grupo"/>
                            <span id="icon-25" class="gpo informacion hide" title="Debe escribir la abreviación del grupo"></span>
                            <span id="icon-25" class="advgpo warning rojo hide" title="El grupo no existe en la Base de Datos"></span>

                    <div  id="agregarGrup"></div>
                </div> 
            </div> 
            <div class="left width50">
                <div id="area"></div>
                <div id="sistemasERP"></div>
                <div id="nArea">
                <asp:Label runat="server" class="frmReporte" ID="txtNombreArea"></asp:Label><br />
                </div>
                
<%--                <asp:DropDownList ID="ddlSistema" runat="server" class="select inputP98"> </asp:DropDownList>--%>
                <div class="hide clear width98 center" id="divSistGpo">
                    <%--<label for="ddlSistema" class="width98 frmReporte hide" id="lblSiste">Sistemas: </label> <br />
                    <asp:Label Text="" ID="lblAreaExterno" runat="server" />--%>
                    <%--<select id='ddlSistemaG' class='select inputP98 validaCombo hide' disabled>
                        <option value='0' disabled selected='true'>Seleccione una opción</option>
                    </select>--%>
                </div>
                <div class="hide clear width98 center" id="sistemasERP">
                </div>

            </div>
        </div>
        <div class="clear left width50">   
        </div>
        <div class="clear center width98">
            <label id="lblAsun" for="txtAsunto" class="frmReporte">Asunto: </label> <br />
            <input id="txtAsunto" type="text" class="inputP98 validaCajas" />
        </div>
        <div class="clear center width98">
            <label for="txtDescripcion" class="frmReporte">Descripción: </label> <br />
            <textarea id="txtDescripcion" class="inputP98 validaCajas" rows="2"></textarea>
        </div>
        <div class="clear center width98">
            <div class="left width50">
                <label for="txtFechaPropuesta" class="frmReporte">Fecha Propuesta:</label><br />
                    <input id="txtFechaPropuesta" type="text" class="inputP50">
                    <a onclick="javascript:mostrarCalendario();">
                        <span id="icon-25" class="azul calendario"></span>
                    </a>
            </div>
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
        <div class="clear center width98">
            <div class="left width50">
                <label id="lblTicketVinculado" for="inputTicketVinculado" class="frmReporte">Folio Ticket Para Vincular: </label> <br />
                <input id="inputTicketVinculado" type="text" class="inputP50" />
            </div>

            <div class="hide left width50" id="divSprint">
                <label id="lblSprint" for="inputSprint" class="frmReporte">Numero de Sprint: </label> <br />
                <input id="inputSprint" type="text" class="inputP50" />
            </div>

            <div class=" hide left width50" id="divPoints">
                <label id="lblPoints" for="inputPoints" class="frmReporte">Story Points: </label> <br />
                <input id="inputPoints" type="text" class="inputP50" />
            </div>
        </div>
        <div class="clear"><br /></div>
        <div class="clear width98 txt-right">
            <a href="javascript:;" id="btnGenerarERP" class="btn verde hide">Generar ERP</a>
            <a href="javascript:;" id="btnGenerar" class="btn verde hide">Generar</a>
            <a href="javascript:;" id="btnModificar" class="btn verde hide">Modificar</a>
            <a href="javascript:;" id="btnModificarERP" class="btn verde hide">Modificar ERP</a>
            <a class="btn blanco" onclick="javascript:cerrarFormulario();">Cancelar</a>
        </div>
        <%--<input type="hidden" value=" " id="txtIdReporte"/>--%>
	</div>
</div>
<%--Fin del formulario para Agregar nuevo reporte--%>

<div class="clear"> <br /> <br /> </div>
<input type="hidden" id="nombreArchivo" />

<%--Inicio Botones para realizar filtros de los reportes generados(Generados/Asignados/Terminados)--%>
<div class="clear center">
    <div id="divSinIncidencias" class="hide width98 txt-right">
        <br /><br /><div class='center bg-alert width98 clear txt-left'><span id='icon-47' class='warning blanco'>No se ha generado ninguna incidencia</span></div> <br /> <br />
    </div>
    <div id="divFiltros" class="clear width98 txt-right">
        <a href="javascript:;" onclick="javascript:getLeccionesAprendidas();" id="btnLeccionesAprendidas" class="hide btn2 azul shadow"> <span id="icon-25" class="ayuda blanco" title="Lecciones Aprendidas">Lecciones Aprendidas</span> </a>
        <a href="javascript:;" onclick="javascript:getReportesCreados();" id="btnVerTodos" class="btn azul"> <span id="icon-25" class="consultar blanco" title="Ver todos"></span> </a>
        <a href="javascript:;" onclick="javascript:getReportesGenerados();" id="btnGenerado" class="btn azul"> <span id="icon-25" class="ticket blanco" title="Generados"></span> </a>
        <a href="javascript:;" onclick="javascript:getReportesEnSoporte();" id="btnSoporte" class="btn azul"> <span id="icon-25" class="proceso blanco" title="Soporte en proceso"></span> </a>
        <a href="javascript:;" onclick="javascript:getReportesPorValidar();" id="btnPorValidar" class="btn azul"> <span id="icon-25" class="validar blanco" title="Por Validar"></span> </a>
        <a href="javascript:;" onclick="javascript:getReportesTerminados();" id="btnTerminado" class="btn azul"> <span id="icon-25" class="solicitud_aprobada blanco" title="Terminados"></span> </a>   
    </div>
    <asp:Label ID="lblTabla" runat="server"></asp:Label>
     <asp:Label ID="lblTablaLog" runat="server"></asp:Label>
</div>
<%--Fin Botones para realizar filtros de los reportes generados(Generados/Asignados/Terminados)--%>

<div class="clear"> <br /> </div>

<%--Inicio Botones para realizar Ver el log de las acciones de los reportes--%>
<div class="clear center width90">
    <a href="javascript:;" id="btnMostrar" onclick="javascript:getLogReportes();" class="btn azul"><span id="icon-25" class="historial blanco">Ver Log </span></a>
    <a href="javascript:;" id="btnOcultar" onclick="javascript:ocultarLog();" class="hide btn azul"><span id="icon-25" class="cerrar blanco">Ocultar Log</span></a>
</div>
<%--Inicio Botones para realizar Ver el log de las acciones de los reportes--%>

<div class="clear"> <br /> </div>

<%--Tabla en la que se encuentra el log de las acciones de los reportes--%>
<div class="clear center">
       <%--Aqui puede ir la tabla del log--%>
</div>

<div class="clear"> <br /> </div>

<%--Inicio Formulario para Ver el detalle de cada reporte--%>
    <div class="hide" id="frmReporte" title="Detalle Incidencia">
      <div id="divDetalle">
        <div class="alert clear width90 center frm" id="detalleRep"><br />      
            <fieldset>
                <legend class="verde">                    
                    <span id="icon-25" class="evidencia blanco"></span>
                    <label id="lblFolio" style="color: #ffffff"></label>
                </legend>
                <%--<input type="hidden" value=" " id="txtIdReporte"/>--%>
                <%--<div class="clear center width98">
                    <div class="left width50">
                        <label id="frm" >Folio:</label> <br />
                        <label id="lblFolio"></label>
                    </div>
                    <div>
                        <label id="frm" class="">Tipo de Reporte:</label> <br />
                        <label id="lblTipoReporte" ></label>
                    </div>
                </div>--%>
                <div class="clear center width98">
                    <div class="left width50">
                        <label id="frm">Grupo:</label> <br />
                        <label id="lblGrupo" ></label><br />
                    </div>
                    <div class="left width50">
                        <%--<label id="frm">Sistema:</label> <br />--%>
                        <label id="lblSistema"></label><br />
                        <div class="clear center width98 hide" id="sistemasERPSeleccionados"></div>
                    </div>
                </div><br /> 
                <div class="clear center width98" id="divAsunto">
                    <label for="" id="frm">Asunto:</label> <br />                    
                    <label id="lblAsunto" ></label>
                </div><br />
                <div class="clear center width98">
                    <label for="" id="frm" >Descripción:</label> <br />
                    <label id="lblDescripcion"></label>
                </div><br />
                <div class="center clear width98">
                    <div class="left width50">                   
                        <label for="" id="frm">Fecha Reporte:</label><br />
                        <span id="icon-25" class="azul calendario"></span> <label id="lblFechaReporte" ></label> <br />
                    </div>
                    <div class="left width50" id="fechaProp">                    
                        <label for="" id="frm" class="txt-verde">Fecha Propuesta:</label><br />
                        <span id="icon-25" class="azul calendario"></span> <label id="lblFechaPropuesta" ></label> <br />                    
                    </div>
                    <div class="left width50 hide" id="fechaResp">                    
                        <label for="" id="frm" class="txt-verde">Fecha Respuesta:</label><br />
                        <span id="icon-25" class="azul calendario"></span> <label id="lblFechaRespuesta" ></label> <br />                    
                    </div>
                </div>
                <div class="clear center width98 hide" id="divRespuesta">                   
                     <label for="" id="frm">Respuesta:</label><br />
                     <label id="lblRespuestaLecciones" ></label> <br />
                </div>
                <div class="center clear"> <br /> </div>
                <div class="clear center width98">
                    <div class="left width50" id="divEvidencia">
                        <label for="" id="frm">Evidencia:</label><br />                    
                        <div id="lblEvidencia"></div>
                    </div>
                    <div class="left width50">
                        <div id="responsable"></div> 
                    </div>
                </div>                   
             </fieldset>
        </div><br />

        <div id="divAcciones" class="clear txt-right"">
            <a href="javascript:;" onclick="javascript:detalleAccionesReporte();" class="btn azul" id="btnMostrarDetalle"> <span id="icon-25" class="historial blanco">Detalle Incidencia</span> </a>
            <a href="javascript:;" onclick="javascript:ocultarDetalleAcciones();" class="btn azul" id="btnOcultarDetalle"> <span id="icon-25" class="cerrar blanco">Ocultar detalle Incidencia</span> </a>
        </div><br /> 

        <div class="clear">
            <asp:Label ID="lblTablaDetalleR" runat="server"></asp:Label>
        </div>

        <br /> 
        <div class="clear width98 txt-right">      
            <a href="#" class="btn blanco" id="btnCerrarDlogDetalleReporte">CERRAR</a>
        </div> 
      </div>
    </div>  
<%--Fin Formulario para Ver el detalle de cada reporte--%>

<%--Inicio Dialog para confirmar eliminado de cada reporte--%>
    <div class="hide width98 clear txt-center" id="dConfirmar" title="¿Está seguro de eliminar?">
        <div class="left" style="width: 20%">
            <span id="icon-90" class="rojo warning"></span>
        </div>
        <div id="divBtns" class="left width80" align="right" style="padding-top: 15px">
            <input type="hidden" value=" " id="txtIdReporteE"/>
            <input type="button" class="btn verde" id="btnEliminarReporte" value="Aceptar"  onclick="javascript:eliminarReporteCreado();"/>
            <input type="button" class="btn verde" id="btnEliminarERP" value="Aceptar"  onclick="javascript:eliminarReporteERP();"/>
            <a href="#" class="btn blanco" id="btnCancelarEliminacion">Cancelar</a>
        </div>
    </div>
<%--Fin Dialog para confirmar eliminado de cada reporte--%>

     <%-- DIALOG ELIMINAR REPORTE --%>
    <div class="hide" id="dlogEliminarReporte" title="Eliminar">
       <span id="icon-47" class="warning rojo"></span><label class="frm" style="color:#166889 !important;">¿Esta seguro de Eliminar la Incidencia?</label>
        <div id="divJustifi">
            <label class="frm" style="color:#166889 !important;">Escriba la justificación:</label>
            <textarea rows="4" cols="65" id="txtJustifi"></textarea>
        </div>  <br />      
        <div class="clear width98 txt-right">
            <a href="#divTblReportes" class="btn verde" id="btnSi">Aceptar</a>
            <a href="#divTblReportes" class="btn blanco" id="btnNo">Cancelar</a>
        </div>
    </div>
    <%-- FIN DIALOG ELIMINAR REPORTE --%>

    <%-- DIALOG ELIMINAR REPORTE DE ERP --%>
    <div class="hide" id="dlogEliminarReporteERP" title="Eliminar">
       <span id="icon-47" class="warning rojo"></span><label class="frm" style="color:#166889 !important;">¿Esta seguro de Eliminar la Incidencia?</label>
        <div id="div2">
            <label class="frm" style="color:#166889 !important;">Escriba la justificación:</label>
            <textarea rows="4" cols="65" id="txtJustifiERP"></textarea>
        </div>  <br />      
        <div class="clear width98 txt-right">
            <a href="#divTblReportes" class="btn verde" id="btnSiERP">Aceptar</a>
            <a href="#divTblReportes" class="btn blanco" id="btnNoERP">Cancelar</a>
        </div>
    </div>
    <%-- FIN DIALOG ELIMINAR REPORTE DE ERP--%>
    
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

    <%--Inicio dialog lecciones Aprendidas--%>
    <div class="hide" id="dlogLeccionesAprendidas" title="Lecciones Aprendidas">
        <div class="clear" id="divTablaLecciones">
            <asp:Label ID="lblLeccionesAprendidas" runat="server"></asp:Label>
            <div class="clear width98 txt-right"> 
            <br />
            <br />     
                <a href="#" class="btn blanco" id="btnCerrarDlogLecciones">Cerrar</a>
            </div> 
        </div>

        <div id="divDetalleL" class="hide">
        <div class="alert clear width90 center frm" id="detalleRepL"><br />      
            <fieldset>
                <legend class="verde">                    
                    <span id="icon-25" class="evidencia blanco"></span>
                    <label id="lblFolioL" style="color: #ffffff"></label>
                </legend>
                <div class="clear center width98">
                    <div class="left width50">
                        <label id="frm">Grupo:</label> <br />
                        <label id="lblGrupoL" ></label><br />
                    </div>
                    <div class="left width50">
                        <label id="frm">Sistema:</label> <br />
                        <label id="lblSistemaL"></label><br />
                        <div class="clear center width98 hide" id="sistemasERPSeleccionadosL"></div>
                    </div>
                </div><br /> <br /> 
                <div class="clear center width98" id="divAsunto">
                    <label for="" id="frm">Asunto:</label> <br />                    
                    <label id="lblAsuntoL" ></label>
                </div><br />
                <div class="clear center width98">
                    <label for="" id="frm" >Descripción:</label> <br />
                    <label id="lblDescripcionL"></label>
                </div><br />
                <div class="center clear width98">
                    <div class="left width50">                   
                        <label for="" id="frm">Fecha Reporte:</label><br />
                        <span id="icon-25" class="azul calendario"></span> <label id="lblFechaReporteL" ></label> <br />
                    </div>
                    <div class="left width50" id="fechaRespL">                    
                        <label for="" id="frm" class="txt-verde">Fecha Respuesta:</label><br />
                        <span id="icon-25" class="azul calendario"></span> <label id="lblFechaRespuestaL" ></label> <br />                    
                    </div>
                </div><br /> 
                <div class="clear center width98">                   
                     <label for="" id="frm">Respuesta:</label><br />
                     <label id="lblRespuestaLeccionesL" ></label> <br />
                </div>
                <div class="center clear"> <br /> </div>
                <div class="clear center width98">
                    <div class="left width50" id="divEvidencia">
                        <label for="" id="frm">Evidencia:</label><br />                    
                        <div id="lblEvidenciaL"></div>
                    </div>
                    <div class="left width50">
                        <div id="responsableL"></div> 
                    </div>
                </div>                   
             </fieldset>
        </div><br /><br /> 
        <div class="clear width98 txt-right">      
            <a href="#" class="btn verde" onclick="javascript:regresarLecciones();" id="btnRegresar">Regresar</a>
            <a href="#" class="btn blanco" id="btnCerrarDlogDetalleLecciones">Cerrar</a>
        </div> 
      </div>
    </div>
    <%--Fin dialog lecciones Aprendidas--%>   
<input type="hidden" id="txttipoIncidenciaCbo" />
<input type="hidden" id="txtCantidad" value="0"/>
<asp:TextBox runat="server" class="hide" ID="txtidUser"></asp:TextBox>
<asp:TextBox class="hide" runat="server" ID="txtidGrupo"></asp:TextBox>
<input type="hidden" value="" id="txtIdReporte"/>
<input type="hidden" value="" id="txtIdERPGrupo"/>
<asp:TextBox runat="server" ID="txtIdArea" CssClass="hide"/>  
<asp:TextBox runat="server" ID="txtIdUsuario" CssClass="hide"/>  
<input type="hidden" value="" id="idTipoUsuario"/>
<asp:TextBox runat="server" class="hide" ID="txtTipoUsuario"></asp:TextBox>
<asp:TextBox runat="server" class="hide" ID="txtNombreGpo"></asp:TextBox>
</asp:Content>
