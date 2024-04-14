<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="HistoriasTerminadas.aspx.cs" Inherits="Configuracion_HistoriasTerminadas_HistoriasTerminadas" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="Server">
    <title>Historias de Usuario</title>
    <link href="../../css/reportescss/token-input-facebook.css" rel="stylesheet" type="text/css" />
    <link href="../../css/Sistema/forms.css" rel="stylesheet" type="text/css" />
    
    <!--<script src="../../js/HistoriasTerminadas/HistoiasTerminadas.js" type="text/javascript"></script>-->
    <script src="../../js/HistoriasTerminadas/HistoiasTerminadas.js" type="text/javascript"></script>
    <style>
        .sorting_11{
            display:none
        }
    </style>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">
<asp:TextBox runat="server" ID="txtid" class="hide"></asp:TextBox>
<div id="form-layer-98">
        <div class="alert clear form-layer-98 center frm">
            <h3 class="txt-azul">Historias Terminadas</h3>
    
            <button id="asignarFecha" class="btn verde shadow2 borde-blanco" onclick="javascript:;">Asignar Fecha Liberacion</button>
            <button id="marcarTodos" class="btn verde shadow2 borde-blanco">Marcar Todas</button>
            <fieldset class="form-layer-width98" id="divConten">
                <legend>
                    Historias
                </legend>
                <div class="dataTables_wraper" id="divTblHistoriaT">
                    <div id="datagrid-layer">

                       <table class="data_grid display dataTable" style="min-width:100%;" id="tblHistoriaT">
                        <thead id="grid-head2">
                                <tr role="row">
                                    <th></th>
                                    <th class="txt-left col-reporte sorting_asc" style="display:none"></th>
                                    <th aria-controls="tblReporteCreados" aria-label="Folio: activate to sort column descending" aria-sort="ascending" class="txt-left col-reporte sorting_asc" colspan="1" rowspan="1" style="width: 55.2px;" tabindex="0">Ticket</th>
                                    <th class="txt-left col-reporte sorting_asc" style="display:none;">idSistema</th>
                                    <th class="txt-left col-reporte sorting_asc">Sistema</th>
                                    <th class="txt-left col-reporte sorting_asc">Asunto</th>
                                    <th class="txt-left col-reporte sorting_asc">Grupo</th>
                                </tr>
                        </thead>
                        <tbody id="grid-body">
                            
                        </tbody>
                        </table>


                    </div>

                </div>

            </fieldset>
        </div>
    </div>


    <!-- Modal -->
	<!--<div id="myModal" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-front shadow3">
	  <!-- Contenido -->
    <div id="myModal" class="hide" title="Asignar Fecha de Liberacion">
 
        <br/>
        <div class="clear width98 center" style="height:70%">
            <div class="left width70 margin10" id="sistemasHist">
			    
            </div>
            <div class="left width30 margin5" id="Aggfecha">
   
            </div>
       
        </div>
        <br/>
        <div class="clear width90 txt-right center margin" style="height:20%">
            <button class="btn verde" id="btnGuardar">Guardar</button>
        </div>

			<%--<label for="estatus">Estatus:</label>
			<%--<select id="country" name="sestatus">
			<%--<option value="En Espera">En espera</option>
			</select>--%>

		<%--	<label for="subject">Descripcion</label>
		<textarea rows="0" cols="0" id="subject" name="subject" placeholder="Descripcion.." style="height:200px"></textarea>--%>

	  	</div>

</asp:Content>