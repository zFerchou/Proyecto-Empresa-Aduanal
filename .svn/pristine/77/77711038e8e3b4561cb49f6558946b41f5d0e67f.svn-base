<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultarReportes.aspx.cs" Inherits="Configuracion_Reportes_ConsultarReportes" MasterPageFile="~/Site.master" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/Sistema/Highcharts-4.2.5/highcharts.js"                   type="text/javascript"></script>
    <script src="../../js/Sistema/Highcharts-4.2.5/modules/data.js"                 type="text/javascript"></script>
    <script src="../../js/Sistema/Highcharts-4.2.5/modules/exporting.js"            type="text/javascript"></script>
    <script src="../../js/Sistema/Highcharts-4.2.5/modules/offline-exporting.js"    type="text/javascript"></script> 
    <!--EXPORTAR IMAGEN-->
    <script src="../../js/Sistema/jspdf.js"                                         type="text/javascript"></script>
    <script src="../../js/Sistema/jspdf.plugin.autotable.src.js"                    type="text/javascript"></script>
    <script src="../../js/Sistema/rgbcolor.js"                                      type="text/javascript"></script>
    <script src="../../js/Sistema/canvg.js"                                         type="text/javascript"></script>    
    <!--FIN EXPORTAR IMAGEN--> 
    <script src="../../js/Reportes/ConsultaReportes/AutoCompleteResponsables.js"    type="text/javascript"></script> 
    <script src="../../js/Reportes/ConsultaReportes/ConsultarReportes.js"           type="text/javascript"></script>
    
    <link href="../../css/Sistema/dataTables.tableTools.min.css"                    rel="stylesheet" />
    <link href="../../css/reportescss/GestionReportesCss.css"                       rel="stylesheet" />
</asp:Content>


<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server" ClientIDMode="Static">
    <input type="hidden" value="" id="valReporte"/>
    <div id="form-layer-98">
        <div class="alert clear form-layer-98 center frm">
            <h3 class="txt-azul">INCIDENCIAS |<span id="icon-25" class="incidencia txt-verde">CONSULTAR INCIDENCIAS</span></h3>
            <%--NUEVO DIV DE FILTROS --%>
            <div class="left width98" id="filtrosAsignados">
                <div class="left">
                    <span id="icon-90" class="azul reporte reporte-text"></span><br />
                    <%--<img src="../../images/icons/borrar.png" alt="Limpiar Campos" title="Limpiar Todos los Campos" id="btnBorrar"/>--%>
                    <span id="icon-25" class="rechazar btnBorrar txt-azul" title="Limpiar Todos los Campos">Limpiar</span>
                </div>
                <div class="left width20">
                    <div class="left width98 margin-top">
                        <label for="txtFolio">Folio:</label>
                    </div>
                    <div class="left width98 margin">
                        <input type="text" id="txtFolio" value=""/>
                        <input type="hidden" id="txtIdFolio" value=""/>
                    </div>
                    <div class="left width98 margin-top">
                        <label for="txtNomGrupo">Grupo:</label>
                    </div>
                    <div class="left width98 margin">
                        <input type="text" id="txtNomGrupo" value=""/>
                        <input type="hidden" id="txtIdGrupo" value=""/>
                    </div>
                    <div class="left width98 margin">
                        <label for="">Fecha Inicio:</label>

                    </div>
                    <div class="left width98 margin">
                        <input type="text" id="txtFechaInicio" value=""/>
                        <a>
                            <span id="icon-25" class="azul calendario calendarioInicio" title="Seleccionar día" style="transition: all 0.3s; cursor: pointer;"></span>
                        </a>
                    </div>
                    <div class="left width98 margin">
                        <label for="">Fecha Fin:</label>
                    </div>
                    <div class="left width98 margin-bottom">
                        <input class="hasDatepicker" type="text" id="txtFechaFin" value=""/>
                        <a>
                            <span id="icon-25" class="azul calendario calendarioFin" title="Seleccionar día" style="transition: all 0.3s; cursor: pointer;"></span>
                        </a>
                    </div>                    
                </div>
                <div class="left width20" id="divEstatus">
                    <div class="left width98 margin-top">
                        <label for="">Estatus de la Incidencia:</label>
                    </div>
                    <div class="left width98 margin">
                        <input type="checkbox" name="chkReportes" class="check validar" id="chkAsignados" value="2" />
                        <label for="chkAsignados">Asignados</label>
                        <input type="checkbox" name="chkReportes" class="check validar" id="chkSinAsignar" value="1" />
                        <label for="chkSinAsignar">Sin Asignar</label>
                        <input type="checkbox" name="chkReportes" class="check validar" id="chkPorValidar" value="3" />
                        <label for="chkPorValidar">Por Validar</label>
                        <input type="checkbox" name="chkReportes" class="check validar" id="chkTerminados" value="5" />
                        <label for="chkTerminados">Terminados</label>
                    </div>
                    
                </div>
                <div class="left width20"  id="divTiposReportes">
                    <div class="left width98 margin-top">
                        <label for="">Tipo de Incidencia:</label>
                    </div>
                    <div class="left width98 margin">
                        <input type="checkbox" name="chkTipoReporte" class="check validar" id="chkTipoReporteFallo" value="1" />
                        <label for="chkTipoReporteFallo">Fallos</label>
                        <input type="checkbox" name="chkTipoReporte" class="check validar" id="chkTipoReporteFuncionalidad" value="2" />
                        <label for="chkTipoReporteFuncionalidad">Funcionalidades</label>
                        <input type="checkbox" name="chkTipoReporte" class="check validar" id="chkTipoReporteErp" value="3" />
                        <label for="chkTipoReporteErp">ERP</label>
                    </div>                                        
                </div>  
                <div class="left width20"></div>
                <div class="left width20 txt-right center">
                    <div id="divBtnPDF">
                        <span id="icon-47" class="pdf btnGenerarPDF">Generar PDF</span>
                    </div>
                    <div id="divBtnBuscar">
                        <%--<input type="button" class="search btn azul" value="BUSCAR" id="btnBuscar" />--%>
                        <a href="#" id="btnBuscar" class="btn verde shadow2 borde-blanco"><span id="icon-25" class="blanco buscar">BUSCAR</span></a>
                        <%--<input type="button" class="search btn verde" value="Ver Todos" id="btnVerTodos" title="Ver la lista de todos los Reportes" />--%>
                        <a id="btnVerTodos" title="Ver la lista de todas las incidencias" class="tooltip_elemento center">Ver Todos</a>
                    </div>                                        
                </div>
            </div>
            <%-- FIN NUEVO DIV DE FILTROS --%>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br /><br /><br /><br /><br />
            <div id="divTblReportes"></div>
            <br />
            <br />
            <br />
            <div id="divGrafica">
                <h3 class="txt-azul">INCIDENCIAS |<label class="txt-verde">GRAFICA DE RESULTADOS</label></h3>
                <div id="container" class="grafica"></div>
            </div>
            
        </div>
    </div>

    <%-- DIALOG PARA DETALLE DEL PROCESO --%>
    <div class="hide" id="dlogReporte" title="Detalle de Incidencia">
        <div class="alert clear width90 center frm">
            <div id="divDetalle">
                <fieldset>
                    <legend class="verde">
                        <span id="icon-25" class="evidencia blanco"></span>
                        Detalle
                    </legend>
                    <input type="hidden" value=" " id="txtIdReporte" />
                    <div class="left width30">
                        <label for="" class="txt-verde frm">Fecha Incidencia:</label><br />
                        <p id="lblFechaReporte" class="txt-azul"></p>
                        <br />
                    </div>
                    <div class="left width30">
                        <label for="" class="txt-verde frm">Fecha Propuesta:</label><br />
                        <p id="lblFechaPropuesta" class="txt-azul"></p>
                        <br />
                    </div>
                    <div class="left width30">
                        <%--<label for="" class="txt-verde frm" id="lblPerosnalApoy">Personal de Apoyo:</label><br />--%>
                           
                        <%--<div id="iconPersonal" class="">
                            
                        </div>--%>
                        <label for="" class="txt-verde frm">Responsable:</label><br />
                        <div id="responsable"></div>
                        <div id="divAutocompleteResponsable">
                            <input type="text" id="txtResponsabl" class="input200" placeholder="Escriba el Nombre"/>
                            <input type="hidden" id="txtIdResponsabl"/>
                            <a onclick="javascript:">
                                <span title="Agregar" id="icon-25" class="verde agregar modificar-apoyo" onclick="javascript:editarPersonaResponsable()"></span>
                            </a>
                        </div>
                        <br />
                        <label for="" id="lblApoyo" class="txt-verde frm">Apoyo: <span id='icon-25' class='verde agregar btnAddApoyo' title='Agregar Apoyo'></span></label><br />
                        <div id="divCalificacion" class="hide"></div>
                        
                        <div id="apoyo"></div>
                        <div id="divAutocompleteApoyo">
                            <input type="text" id="txtApoy" class="input200" placeholder="Escriba el Nombre"/>
                            <input type="hidden" id="txtIdApoy"/>
                            <a onclick="javascript:">
                                <span title="Agregar" id="icon-25" class="verde agregar modificar-apoyo" onclick="javascript:editarPersonaApoyo()"></span>
                            </a>
                        </div>
                    </div>
                    <div class="left width98">
                        <label for="" class="txt-verde frm">Asunto:</label><br />
                        <p id="lblAsunto" class="txt-azul"></p>

                        <label for="" class="txt-verde frm">Descripción:</label><br />
                        <p id="lblDescripcion" class="txt-azul"></p>
                    </div>
                    
                </fieldset>  
            </div>
            <br />
            <h3>Historial de la Incidencia:</h3>
            <div id="divTblLog"></div>
        </div>
        <br />
        <br />
        
        <br />
        <div class="clear width98 txt-right center">
          <%--<a href="javascript:;" id="btnTblLogReporte"class="btn azul">
              <span id="icon-25" class="historial blanco">Ver Historial</span>
          </a>--%>
<%--            <a href="javascript:;" id="btnTblLogReporteOcultar"class="btn azul">
              <span id="icon-25" class="historial blanco">Ocultar Historial</span>
          </a>--%>
          <a href="#" class="btn blanco" id="btnCerrarDlogDetalleReporte">CERRAR</a>
        </div>        
    </div>
    <%-- FIN DIALOG PARA EL DETALLE DEL PROCESO --%>

    <%-- DIALOG PARA LISTA DE APOYO PARA REPORTES --%>
    <div class="hide" id="dlogLstApoyo" title="Lista de Responsbales de Incidencia">
        <div class="alert clear width90 center frm">
            <br />
            <fieldset>
                <legend class="verde">
                    <span id="icon-25" class="evidencia blanco lstResponsables"></span>
                    Responsable
                </legend>

                <div class="left width98">
                    <label for="lblResponsable" id="frm">Responsable Directo:</label><br />
                    <div id="inpAutocompleteResponsable">
                        <%-- Aquí se agregara el autocomplete para agregar personas de Apoyo a reporte --%>
                    </div>
                    <div id="divTblResponsables"></div>
                    <br />
                </div>
            </fieldset>
            <br />

            <fieldset>
                <legend class="verde">
                    <span id="icon-25" class="evidencia blanco"></span>
                    Apoyo
                </legend>
                <div class="left width98">
                    <label for="txtApoyo" id="frm">Personal de Apoyo:</label><br />
                    <div id="inpAutocomplete">
                        <%-- Aquí se agregara el autocomplete para agregar personas de Apoyo a reporte --%>
                    </div>
                    <%--<input type="text" id="txtApoyo" class="inputP50" placeholder="Escriba el Nombre"/><input type="hidden" id="txtIdApoyo"/>--%>
                    <%--<a onclick="javascript:">
                        <span title="Agregar" id="icon-25" class="verde agregar agregar-apoyo"></span>
                    </a> --%>
                </div>
                <div id="tablaDetalleReporte"></div>
                <%--<div id="divTblApoyo"></div>--%>
            </fieldset>

        </div>
        <br />
        <div class="clear width98 txt-right center">
            <a href="#" class="btn blanco" id="btnCerrarDlogResponsables">CERRAR</a>
        </div>
    </div>
    <%-- FIN DIALOG PARA LISTA DE APOYO PARA REPORTE --%>

    <%-- DIALOG ELIMINAR REPORTE --%>
    <div class="hide" id="dlogEliminarReporte" title="Eliminar">
       <p id="frm" class="lblEmpty">¿Seguro de Eliminar la Incidencia?</p>
        <div id="divJustifi">
            <p class="frm">Escribe la justificación:</p>
            <textarea rows="4" cols="50" id="txtJustifi"></textarea><br />
            <span id="alerta" class="requerido-evidencia hide">Campo requerido</span>
        </div>        <br />
        <div class="clear width98 txt-right center">
            <input type="button"  value="ACEPTAR" class="btn verde" id="btnSi"/>
            <input type="button"  value="CANCELAR" class="btn blanco" id="btnNo"/>
        </div>
    </div>
    <%-- FIN DIALOG ELIMINAR REPORTE --%>       
</asp:Content>
