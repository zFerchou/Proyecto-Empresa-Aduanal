<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="sistema.aspx.cs" Inherits="Configuracion_Implementador_sistema" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Historias de Usuario</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js" type="text/javascript"></script>
    <script src="/JS/Implementador/sistema.js" type="text/javascript"></script>
    <script src="../../js/site.js" type="text/javascript"></script>
    <link href="../../css/implementadorcss/styles.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" type="text/css" />

    <style type="text/css">
        .pdtes {
            width: 240px;
            height: 55px;
            border-radius: 15px;
        }

        label#icon-25.tipo-ticket.verde {
            padding: 15px 15px 15px 35px !important;
            font-size: 15px !important;
        }

        .qty-tickets {
            font-size: 16px;
            border: 2px solid #fff;
            margin-left: 190px;
            padding: 5px 10px 5px 10px;
            border-radius: 8px;
        }

        label.txt-verde {
            font-size: 14px !important;
            font-weight: 600 !important;
            font-style: normal !important;
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <!-- Separadores -->
    <div>
        <div class="clear"><br /></div>
        <div class="clear"><br /></div>
        <div class="clear"><br /></div>
        <div class="clear"><br /></div>
        <div class="clear"><br /></div>
        <div class="clear"><br /></div>
    </div>

    <!-- Botones para mostrar los sistemas -->
    <label id="lblEtiquetas" class="labelsPrincipales">
        <a href="#" onclick="mostrarSistemasNoAgendadas('sistemaSinAgendar'); return false;" class="divBlanco pdtes">
            <label id="icon-25" class="asig_equipo verde tipo-ticket">
                SIN AGENDAR
                <label id="lblNumNoAgendadas" class="txt-azul qty-tickets">0</label>
            </label>
        </a>
        <a href="#" onclick="mostrarSistemasAgendadas('sistemaAgendadas'); return false;" class="divBlanco pdtes">
            <label id="icon-25" class="configuracion verde tipo-ticket"> AGENDADAS
                <label id="lblNumAgendadas" class="txt-azul qty-tickets">0</label>
            </label>
        </a>
        <a href="javascript:;" onclick="mostrarSistemasLiberadas('sistemaLiberadas'); return false;" class="divBlanco pdtes">
            <label id="icon-25" class="validar verde tipo-ticket"> LIBERADAS
                <label class="txt-azul qty-tickets" id="lblNumLiberadas">0</label>
            </label>
        </a>
    </label>


    <!-- Separador -->
    <div class="clear"><br /></div>

    <!-- Contenido de los sistemas y se al seleccionar sistema se tendria que mostrar la tabla -->
    <div>
        <div id="contenedorBotonRegresar" style="display: none;">
            <label id="boton">
                <a href="#" class="btn azul btn-acciones-reporte" id="btnRegresar" title="Regresar">
                    <span class="blanco">REGRESAR</span>
                </a>
            </label>
        </div>
    </div>

    <div class="clear"><br /></div>
    <div class="clear"><br /></div>

    <div>
        <div id="contenidoSistemas">
            <div id="sistemaAgendadas" class="sistemaSection" style="display: none;">
            </div>
            <div id="sistemaSinAgendar" class="sistemaSection" style="display: none;">
            </div>
            <div id="sistemaLiberadas" class="sistemaSection" style="display: none;">
            </div>
        </div>
    </div>

        <!-- Separador -->
        <div class="clear"><br /></div>

        <!-- Tablas -->
        <div id="agendadas" style="display: none;">
           <fieldset>
            <legend>HISTORIAS AGENDADAS</legend>
               <div id="tblReporteAgendadas_wrapper" class="dataTables_wrapper">
                    <div id="datagrid-layer">
                        <div id="grid-head1" class="dataTables_length" id="tblReporteAgendadas_length">
                            <label>Show 
                                <select name="tblReporteAgendadas_length" aria-controls="tblReporteAgendadasr" class="">
                                    <option value="10">10</option>
                                    <option value="25">25</option>
                                    <option value="50">50</option>
                                    <option value="100">100</option>
                                </select> entries
                            </label>
                        </div>
                        <div id="tblReporteAgendadas_filter" class="dataTables_filter">
                            <label>Search:
                                <input type="search" class="" placeholder="" aria-controls="tblReporteAgendadas">
                            </label>
                        </div>
                    </div>
            <!-- Aquí se llenará la tabla de historias agendadas correspondientes al sistema -->
            <table id="tblReporteAgendadas" class="data_grid display dataTable" role="grid" aria-describedby="tblReporteAgendadas_info" style="width: 1301px;">
                <thead id="grid-head2">
                    <tr role="row">
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteAgendadas" rowspan="1" colspan="1" style="width: 40.083px;" aria-label="ID: activate to sort column ascending">
                            ID
                        </th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteAgendadas" rowspan="1" colspan="1" style="width: 104.083px;" aria-label="Tipo Reporte: activate to sort column ascending">
                            Folio
                        </th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteAgendadas" rowspan="1" colspan="1" style="width: 119.083px;" aria-label="Grupo: activate to sort column ascending">
                            Descripción
                        </th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteAgendadas" rowspan="1" colspan="1" style="width: 389.083px;" aria-label="Asunto: activate to sort column ascending">
                            Grupo
                        </th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteAgendadas" rowspan="1" colspan="1" style="width: 74.0833px;" aria-label="Prioridad: activate to sort column ascending">
                            Puntos De Historia
                        </th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteAgendadas" rowspan="1" colspan="1" style="width: 79.0833px;" aria-label="Sistema/Área: activate to sort column ascending">
                            Fecha Propuesta Owner
                        </th>
                         <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteAgendadas" rowspan="1" colspan="1" style="width: 50px;" aria-label="Seleccionar:">
                            Seleccionar
                        </th>
                    </tr>
                </thead>
                <tbody id="grid-body"></tbody>
            </table>
                <button id="btnliberar" class="btn verde shadow2 borde-blanco"
                    style="float: right; margin-top: 10px;">
                    Liberar</button>
        </div>
       </fieldset>
        </div>
            <!-- Aquí se llenará la tabla de historias no agendadas correspondientes al sistema -->
           <div id="sinAgendar" style="display:none;">
            <fieldset>
            <legend>HISTORIAS SIN AGENDAR</legend>
            <div id="tblReporteSinAsignar_wrapper" class="dataTables_wrapper">
                <div id="datagrid-layer">
                    <div id="grid-head1" class="dataTables_length" id="tblReporteSinAsignar_length">
                        <label>Show 
                            <select name="tblReporteSinAsignar_length" aria-controls="tblReporteSinAsignar" class="">
                                <option value="10">10</option>
                                <option value="25">25</option>
                                <option value="50">50</option>
                                <option value="100">100</option>
                            </select> entries
                        </label>
                        <div id="tblReporteSinAsignar_filter" class="dataTables_filter">
                            <label>
                                Search:
                                <input type="search" class="" placeholder="" aria-controls="tblReporteSinAsignar">
                            </label>
                        </div>
                    </div>
                    
                </div>
            <table id="tblReporteSinAgendar" class="data_grid display dataTable" role="grid" aria-describedby="tblReporteSinAsignar_info" style="width: 1230px;">
                <thead id="grid-head2">
                    <tr role="row">
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteSinAsignar" rowspan="1" colspan="1" style="width: 40.083px;" aria-label="ID: activate to sort column ascending">
                            ID
                        </th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteSinAsignar" rowspan="1" colspan="1" style="width: 104.083px;" aria-label="Tipo Reporte: activate to sort column ascending">
                            Folio
                        </th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteSinAsignar" rowspan="1" colspan="1" style="width: 208.083px;" aria-label="Grupo: activate to sort column ascending">
                            Descripción
                        </th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteSinAsignar" rowspan="1" colspan="1" style="width: 109.083px;" aria-label="Asunto: activate to sort column ascending">
                            Grupo
                        </th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteSinAsignar" rowspan="1" colspan="1" style="width: 74.0833px;" aria-label="Prioridad: activate to sort column ascending">
                            Puntos De Historia
                        </th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteSinAsignar" rowspan="1" colspan="1" style="width: 149.0833px;" aria-label="Sistema/Área: activate to sort column ascending">
                            Fecha Propuesta Owner
                        </th>
                         <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteAgendadas" rowspan="1" colspan="1" style="width: 50px;" aria-label="Seleccionar:">
                            Seleccionar
                        </th>
                    </tr>
                </thead>
                <tbody id="grid-body"></tbody>
            </table>
                <div class="width20 left">
                    <input type="text" id="fechaFin" class="input200 calendario" />
                    <span id="icon-25" class="blanco calendario"></span>
                </div>

                <button id="btnAgendarHistoria" class="btn verde shadow2 borde-blanco"
                    style="float: right; margin-top: 10px;">
                    Agendar</button>
        </div>
       </fieldset>
     </div>
    
        <div id="liberadas" style="display:none;">
                <fieldset>
            <legend>HISTORIAS LIBERADAS</legend>
            <div id="tblReporteLiberadas_wrapper" class="dataTables_wrapper">
                <div id="datagrid-layer">
                    <div id="grid-head1" class="dataTables_length" id="tblReporteLiberadas_length">
                        <label>Show 
                            <select name="tblReporteLiberadas_length" aria-controls="tblReporteLiberadas" class="">
                                <option value="10">10</option>
                                <option value="25">25</option>
                                <option value="50">50</option>
                                <option value="100">100</option>
                            </select> entries
                        </label>
                    </div>
                    <div id="tblReporteLiberadas_filter" class="dataTables_filter">
                        <label>Search:
                            <input type="search" class="" placeholder="" aria-controls="tblReporteLiberadas">
                        </label>
                    </div>
                </div>
            <!-- Aquí se llenará la tabla de historias liberadas correspondientes al sistema -->
            <table id="tblReporteLiberadas" class="data_grid display dataTable" role="grid" aria-describedby="tblReporteLiberadas_info" style="width: 1301px;">
                <thead id="grid-head2">
                    <tr role="row">
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteLiberadas" rowspan="1" colspan="1" style="width: 104.083px;" aria-label="Tipo Reporte: activate to sort column ascending">Folio</th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteLiberadas" rowspan="1" colspan="1" style="width: 119.083px;" aria-label="Grupo: activate to sort column ascending">Descripción</th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteLiberadas" rowspan="1" colspan="1" style="width: 79.0833px;" aria-label="Sistema/Área: activate to sort column ascending">Grupo</th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteLiberadas" rowspan="1" colspan="1" style="width: 389.083px;" aria-label="Asunto: activate to sort column ascending">Puntos de Historia</th>
                        <th class="txt-left col-reporte sorting_desc" tabindex="0" aria-controls="tblReporteLiberadas" rowspan="1" colspan="1" style="width: 104.083px;" aria-sort="descending" aria-label="Fecha Reporte: activate to sort column ascending">Fecha Propuesta Owner</th>
                        <th class="txt-left col-reporte sorting" tabindex="0" aria-controls="tblReporteLiberadas" rowspan="1" colspan="1" style="width: 74.0833px;" aria-label="Prioridad: activate to sort column ascending">Fecha de liberacion</th>
                     
                    </tr>
                </thead>
                <tbody id="grid-body"></tbody>
            </table>
        </div>

    <!-- Separador -->
    <div class="clear"><br /></div>
</asp:Content>
