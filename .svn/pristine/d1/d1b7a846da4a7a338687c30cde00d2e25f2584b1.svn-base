<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dialogs.aspx.cs" Inherits="Dialogs" MasterPageFile="~/Site.master" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../js/Dialogs/AutoCompleteResponsables.js" type="text/javascript"></script>
    <script src="../../js/Dialogs/Dialogs.js" type="text/javascript"></script>         
        
    <link href="../../css/reportescss/DetalleReporteDialog.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">   
    <input type="button" name="name" value="DIALOG" class="btn azul" id="btnDialog"/>

    <%-- DIALOG PARA DETALLE DEL PROCESO --%>
    <div class="hide" id="dlogReporte" title="Detalle Reporte">
        <div class="alert clear width90 center frm"><br />      
            <fieldset>
                <legend class="verde">                    
                    <span id="icon-25" class="evidencia blanco"></span>
                    Detalle
                </legend>

                <div class="left width30">                   
                    <label for="" id="frm">Fecha Reporte:</label><br />
                    <p id="lblFechaReporte">23/09/2016</p> <br />                    
                </div>
                <div class="left width30">                    
                    <label for="" id="frm">Fecha Propuesta:</label><br />
                    <p id="lblFechaPropuesta">23/09/2016</p> <br />                    
                </div>
                <div class="left width30">
                    <label for="" id="frm">Personal de Apoyo:</label><br />
                    <span id="icon-25" class="usuarios azul lblResponsables" title="Asignar Apoyo para realizar esta Tarea"></span>
                </div>

                <div class="left width98">
                    <label for="" id="frm">Asunto:</label><br />                    
                    <p id="lblAsunto">Lorem Ipsum es simplemente el texto de relleno de las imprentas y archivos de texto.</p>

                    <label for="" id="frm">Descripción:</label><br />
                    <p id="lblDescripcion">Es un hecho establecido hace demasiado tiempo que un lector se distraerá con el contenido del texto de un sitio mientras que mira su diseño. El punto de usar Lorem Ipsum es que tiene una distribución más o menos normal de las letras, al contrario de usar textos como por ejemplo "Contenido aquí, contenido aquí". Estos textos hacen parecerlo un español que se puede leer. Muchos paquetes de autoedición y editores de páginas web usan el Lorem Ipsum como su texto por defecto, y al hacer una búsqueda de "Lorem Ipsum" va a dar por resultado muchos sitios web que usan este texto si se encuentran en estado de desarrollo. Muchas versiones han evolucionado a través de los años, algunas veces por accidente, otras veces a propósito (por ejemplo insertándole humor y cosas por el estilo).</p>
                </div>      
                
                <div class="left width30">
                    <label for="" id="frm">Cambiar Fecha:</label><br />
                    <input type="text" id="txtFechaPlazo" value=" " />
                    <a onclick="javascript:mostrarDatepicker()">
                        <span id="icon-25" class="azul calendario" title="Seleccionar día" style="transition: all 0.3s; cursor: pointer;"></span>
                    </a>
                </div>                          
             </fieldset>
         
        </div>
        <br />
        <div class="clear width98 txt-right center">         
         <a href="#" class="btn verde shadow2 borde-blanco" id="btnAsignarReporte" title="Clic para asignarse esta Tarea"><span id="icon-25" class="blanco aceptar">ASIGNAR</span></a>
            <a href="#" class="btn verde shadow2 borde-blanco" id="btnTerminarReporte" title="Clic para terminar esta Tarea"><span id="icon-25" class="blanco enviar">TERMINAR</span></a>
            <a href="#" class="btn verde shadow2 borde-blanco" id="btnValidarReporte" title="Clic para concluir esta Tarea"><span id="icon-25" class="blanco validar">VALIDAR</span></a>
            <a href="#" class="btn verde shadow2 borde-blanco" id="btnRechazarReporte" title="Clic para rechazar esta Tarea"><span id="icon-25" class="blanco rechazar">RECHAZAR</span></a>
         <a href="#" class="btn blanco" id="btnCerrarDlogDetalleReporte">CERRAR</a>
        </div>   
    </div>

    <%-- FIN DIALOG PARA EL DETALLE DEL PROCESO --%>

    <%-- DIALOG PARA LISTA DE APOYO PARA REPORTES --%>
        <div class="hide" id="dlogLstApoyo" title="Lista de Responsbales del Reporte">
        <div class="alert clear width90 center frm"><br />      
            <fieldset>
                <legend class="verde">                    
                    <span id="icon-25" class="evidencia blanco lstResponsables"></span>
                    Responsable
                </legend>

                <div class="left width98">                   
                    <label for="lblResponsable" id="frm">Responsable Directo:</label><br />
                    <p id="lblResponsable"></p> <br />                    
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
                    <input type="text" id="txtApoyo" class="inputP50" placeholder="Escriba el Nombre"/>
                    <input type="hidden" id="txtIdApoyo"/>
                    <a onclick="javascript:">
                        <span title="Agregar" id="icon-25" class="verde agregar agregar-apoyo"></span>
                    </a>                                                                              
                </div>                                
                <div id="divTblApoyo"></div>                                   
             </fieldset>
         
        </div>
        <br />
        <div class="clear width98 txt-right center">                    
         <a href="#" class="btn blanco" id="btnCerrarDlogResponsables">CERRAR</a>
        </div>   
    </div>

    <%-- FIN DIALOG PARA LISTA DE APOYO PARA REPORTE --%>

</asp:Content>
