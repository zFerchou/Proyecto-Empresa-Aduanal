<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master"  CodeFile="ModificarReportes.aspx.cs" Inherits="Configuracion_Reportes_ModificarReportes" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../js/Sistema/uploadIfy/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../../js/Reportes/autocompleteGrupo.js" type="text/javascript"></script>
    <script src="../../js/Sistema/uploadIfy/jquery.uploadify.js" type="text/javascript"></script>
    <script src="../../js/Reportes/modificarReportes.js" type="text/javascript"></script>
    <script src="../../js/site.js" type="text/javascript"></script>
    <script src="../../js/Sistema/SimpleAjaxUploader.js" type="text/javascript"></script>
    <link href="../../css/reportescss/alertaReporte.css" rel="stylesheet" type="text/css" />
    <link href="../../css/Sistema/forms.css" rel="stylesheet" type="text/css" />
    
    <link href="../../css/reportescss/GestionReportesCss.css"                       rel="stylesheet" />

    <style>
        .col-reporte {
            padding-left: 0.80rem !important;
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server" ClientIDMode="Static">
    <input type="hidden" value="" id="valReporte"/>
    <div id="form-layer-98">
        <div class="alert clear form-layer-98 center frm">
            <h3 class="txt-azul">INCIDENCIAS |<span id="icon-25" class="incidencia txt-verde">MODIFICAR INCIDENCIAS</span></h3>
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
                </div>
                <div class="left width20">
                    <div class="left width98 margin-top">
                        <label for="">Fecha Inicio:</label>

                    </div>
                    <div class="left width98 margin">
                        <input type="text" id="txtFechaInicio" value=""/>
                        <a>
                            <span id="icon-25" class="azul calendario calendarioInicio" title="Seleccionar día" style="transition: all 0.3s; cursor: pointer;"></span>
                        </a>
                    </div>
                    <div class="left width98 margin-top">
                        <label for="">Fecha Fin:</label>
                    </div>
                    <div class="left width98 margin-bottom">
                        <input type="text" id="txtFechaFin" value=""/>
                        <a>
                            <span id="icon-25" class="azul calendario calendarioFin" title="Seleccionar día" style="transition: all 0.3s; cursor: pointer;"></span>
                        </a>
                    </div>                    
                </div>
                <div class="left width20">
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

                <div class="left width20"></div>
                <div class="left width20 txt-right center">
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
                    <div class="clear center width98">
                        <div class="left width50" id="ddlTReporte">
                        </div>

                    </div>
                    <div class="left width98">
                         <div class="left width50" id="divTipoIncidencia"></div>
                         <div class="left width50" id="divPrioridad"></div>
                    </div>
                    <div class="left width98">
                        <div class="left width50" id="divGrupo"></div>
                        <div class="left width50" id="divArea"></div>
                    </div>

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
            <div class="clear center width90">
                <a href="javascript:;" id="btnMostrarLog" onclick="javascript:detalleAccionesReporte();" class="btn azul"><span id="icon-25" class="historial blanco">Ver Log </span></a>
                <a href="javascript:;" id="btnOcultarLog" onclick="javascript:ocultarLog();" class="hide btn azul"><span id="icon-25" class="cerrar blanco">Ocultar Log</span></a>
            </div>
            <h3 id="lblLog">Historial de la Incidencia:</h3>
            <div id="divTblLog" class="hide"></div>
        </div>
        <br />
        <br />
        
        <br />
        <div class="clear width98 txt-right center">
          <a href="javascript:;" onclick="javascript:validarReporte()" id="btnGenerar" class="btn verde">GUARDAR</a>
          <a href="#" class="btn blanco" id="btnCerrarDlogDetalleReporte">CERRAR</a>
        </div>        
    </div>
    <%-- FIN DIALOG PARA EL DETALLE DEL PROCESO --%>

</asp:Content>
