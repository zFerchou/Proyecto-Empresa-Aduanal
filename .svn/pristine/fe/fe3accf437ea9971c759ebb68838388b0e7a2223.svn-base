<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProductBackLog.aspx.cs" Inherits="Configuracion_ProductBackLog_ProductBackLog" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../js/Sistema/uploadIfy/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../../js/Reportes/autocompleteGrupo.js" type="text/javascript"></script>
    <script src="../../js/Sistema/uploadIfy/jquery.uploadify.js" type="text/javascript"></script>
    <script src="../../js/Sistema/jspdf.js" type="text/javascript"></script>
    <script src="../../js/ProductBackLog/ProductBackLog.js" type="text/javascript"></script>
    <script src="../../js/site.js" type="text/javascript"></script>
    <script src="../../js/Sistema/SimpleAjaxUploader.js" type="text/javascript"></script>
    <link href="../../css/reportescss/alertaReporte.css" rel="stylesheet" type="text/css" />
    <link href="../../css/Sistema/forms.css" rel="stylesheet" type="text/css" />
     
    <!-- INICIO Liberia para el multi select -->
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <link href="../../css/reportescss/select2.css" rel="stylesheet" type="text/css" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/i18n/es.js"></script>
   
    <!-- FIN Liberia para el multi select -->
    <style>
        .col-reporte {
            padding-left: 0.80rem !important;
        }
        .btn-acciones-reporte {
            font-size: 0.60rem !important;
            letter-spacing: 0.08rem !important;
            margin-left: 0.60rem !important;
            margin-right: 0px !important;
            padding-left: 10px !important;
        }
        .row-pendientes {
            padding-bottom:15px;
        }
        .pdtes {
            width:240px;
            height:55px;
            border-radius:15px;
        }
            label#icon-25.tipo-ticket.verde {
                padding:15px 15px 15px 35px !important;
                font-size:15px !important;
            }

        .qty-tickets {
            font-size:16px;
            border:2px solid #fff;
            margin-left:190px;
            padding:5px 10px 5px 10px;
            border-radius:8px;
        }

        .ui-dialog#dlogReporte {
            min-width:800px !important;
        }

        .txt-pausado {
            color:#7f8c8d;
        }
        .pausado {
            color:#7f8c8d;
        }
        label.txt-verde {
            font-size:14px !important;
            font-weight:600 !important;
            font-style:normal !important;
        }
        .token-input-list-facebook {
            width:312px !important;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">

    <label id="lblEtiquetas"></label>

    <div class="clear"><br /></div>

    <div class="clear left width98" id="btnAdd">
</div><br />
<div class="clear width98"><br /><br /></div>
    
<%-- DIALOG SELECCIONAR SPRINT --%>
    <div class="hide form-layer clear" id="dlogSeleccionarSprint" title="Selecciona Sprint" style="display: block; min-height: 200px !important;">
       <div class="right"><a href="#" onclick="javascript:cerrarDialogSprint() "><span id="icon-25" class="verde cerrar cursor-link"></span></a></div>
       <div class="clear center width98">
             <div class="left width98" id="divDDLSprint">
             
             </div>
        </div> 
        <br /> <div class="clear"><br /></div> <br /> <br />
        <div class="clear width98 txt-right">      
            <a href="javascript:;" id="btnAsingarSprint" class="btn verde">Asignar Sprint</a>
            <a href="#" class="btn blanco" id="btnCerrarDlogSeleccionarSprint">CERRAR</a>
        </div> 
    </div>
 <%-- FIN DIALOG SELECCIONAR SPRINT--%>
<%--Inicio del formulario para Agregar nuevo reporte--%>
<div id="divFormulario" class="hide form-layer clear" >
    <div class="form-tit round-border3">
		<div class="left" ><h3 class="left" id="lblTipo"></h3></div>
		<div class="right"><a href="#" onclick="javascript:cerrarFormulario(); javascript:limpiarCampos();"><span id="icon-25" class="verde cerrar cursor-link"></span></a></div>
        <div class="clear center width98">
            <div class="left width50" id="ddlTReporte">
            </div>
            <div class="left width50">
                <label for="ddlPrioridad" class="frmReporte">Prioridad: </label> <br />
                <asp:DropDownList ID="ddlPrioridad" runat="server" class="select inputP50 validaCombo"> </asp:DropDownList>
            </div>
            <div class="left width50">
                <label for="ddlTipoImpacto" class="frmReporte">Impacto: </label> <br />
                <asp:DropDownList  ID="ddlTipoImpacto" runat="server" class="select inputP50"> </asp:DropDownList>
                <br />
            </div>
        </div>
        <div class="clear center width98">
            <div class="left width50">
            <div id="nGpo"><asp:Label runat="server" class="frmReporte" ID="txtNombreGrupo"></asp:Label></div>
                <div id="grupo"></div>
                <asp:Label Text="Grupo:" runat="server" ID="ddlERPGrup" CssClass="frmReporte hide"/>
                <asp:Label ID="lblGrupoUsuario" runat="server"></asp:Label>
                <div class="clear center width98">
                            
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
                <div class="hide clear width98 center" id="divSistGpo">
                </div>

            </div>
        </div>
        <div class="clear left width50">   
        </div>
        <div class="clear center width98">
            <label id="lbEpica" for="txtEpica" class="frmReporte">Epica: </label> <br />
            <input id="txtEpica" type="text" class="inputP98 validaCajas" />
        </div>
        <div class="clear center width98">
            <label id="lblAsun" for="txtHistoria" class="frmReporte">Historia: </label> <br />
            <input id="txtHistoria" type="text" class="inputP98 validaCajas" />
        </div>
        <div class="clear center width98">
            <label for="txtDescripcion" class="frmReporte">Descripción: </label> <br />
            <textarea id="txtDescripcion" class="inputP98 validaCajas" rows="2"></textarea>
        </div>
        <div class="clear center width98">
            <label for="txtCriterios" class="frmReporte">Criterios de Aceptación: </label> <br />
            <textarea id="txtCriterios" class="inputP98 validaCajas" rows="2"></textarea>
        </div>
        <div class="clear center width98">
            <label for="txtRiesgos" class="frmReporte">Riesgos: </label> <br />
            <textarea id="txtRiesgos" class="inputP98" rows="2"></textarea>
        </div>
       <div class="clear center width98">
          <div id="estatusH" class="left width50">
             <label for="ddlEstatus" class="frmReporte">Estatus Historia: </label> <br />
             <asp:DropDownList ID="ddlEstatus" runat="server" class="select inputP98"> </asp:DropDownList>
         </div>
         <div id="storyPoints" class="left width50">
             <label for="ddlEstatus" class="frmReporte">Story Points: </label> <br />
             <input id="txtStoryPoints" type="text" class="inputP98" />
         </div>
       </div>
       
        <div class="clear"><br /></div>
       <div class="left width50">
            <br />
            <div class="left width50" id="btnCargaEvidencia">
                 <a id="btnEvidencia" href="#" class="btn azul shadow2">Seleccionar evidencia</a>
            </div>
                
            <div class="left width50">
                <div id="lblEvidenciaM"></div>
            </div>
        </div>
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
<input type="hidden" id="nombreExcel" />

<div class="clear center">
    <asp:Label ID="lblTabla" runat="server"></asp:Label>
     <asp:Label ID="lblTablaLog" runat="server"></asp:Label>
</div>
<div class="clear center">
       <%--Aqui puede ir la tabla del log--%>
</div>

<div class="clear"> <br /> </div>

<%--Inicio Formulario para Ver el detalle de cada reporte--%>
    <div class="hide" id="frmReporte" title="Detalle de la Historia">
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

    
		


<input type="hidden" id="txttipoIncidenciaCbo" />
<input type="hidden" id="txtCantidad" value="0"/>
<asp:TextBox runat="server" class="hide" ID="txtidUser"></asp:TextBox>
    <asp:TextBox class="hide" runat="server" ID="txtidGrupo"></asp:TextBox>
<input type="hidden" value="" id="txtIdReporte"/>
<asp:TextBox runat="server" ID="txtIdArea" CssClass="hide"/>  
<asp:TextBox runat="server" ID="txtIdUsuario" CssClass="hide"/>  
<input type="hidden" value="" id="idTipoUsuario"/>
<asp:TextBox runat="server" class="hide" ID="txtTipoUsuario"></asp:TextBox>
<asp:TextBox runat="server" class="hide" ID="txtNombreGpo"></asp:TextBox>
</asp:Content>

