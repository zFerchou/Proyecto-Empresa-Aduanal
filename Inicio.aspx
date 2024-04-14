﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Inicio.aspx.cs" Inherits="Inicio" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="head" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/reportescss/alertaReporte.css" rel="stylesheet" type="text/css" />
    <link href="js/Sistema/uploadIfy/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="css/reportescss/alertas.css" rel="stylesheet" type="text/css" />
    <link href="css/reportescss/token-input-facebook.css" rel="stylesheet" type="text/css" />

    <link href="js/JRating/jRating.jquery.css" rel="stylesheet" type="text/css" />
    <script src="js/JRating/jRating.jquery.js" type="text/javascript"></script>
    <script src="js/Sistema/jquery.tokeninput.js" type="text/javascript"></script>
    <script src="js/Sistema/uploadIfy/jquery.uploadify.js" type="text/javascript"></script>
    <script src="js/inicio.js" type="text/javascript"></script>

    <style>
        .btn-acciones-reporte {
            font-size: 0.60rem !important;
            letter-spacing: 0.08rem !important;
            margin-left: 0.60rem !important;
            margin-right: 0px !important;
            padding-left: 10px !important;
        }
        .col-reporte {
            padding-left: 0.80rem !important;
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
        /*#icon-25 {
            margin-right:5px !important;
        }*/
    </style>

</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" Runat="Server">
    <div class="row-pendientes">
        <label id="lblPendiente"></label>
    </div>

    <label id="lblEtiquetas"></label>

    <div class="clear"><br /></div>

    <div class="clear center">
        <span id="spanActive"></span>
        <asp:Label ID="lblTabla" runat="server"></asp:Label>
    </div>
    
    <div class="hide" id="dlogReporte" title="Detalle Incidencia">
        <div id="divAccionesCerrar" class="clear txt-right""></div>
        <div id="termino" class="alert clear width90 center frm hide">
            <fieldset>
                <legend class="verde">
                    <span id="icon-25" class="evidencia blanco"></span>
                    <a color="FFFFFF" id="foliot"></a><a id="A2" href=""></a>
                </legend>
                <span id="icon-90" class="verde warning left"></span>
                <p class="left txt-justify width70">Se concluira la incidencia, por favor califica e introduce un comentario o evidencia</p>
                <div class="clear">
                    <label>Evalua la respuesta de la incidencia:</label>
                    <br />
                    <div id="calif" class="clear calificacion" data-average="10" data-id="1"></div>
                </div>
                <br />
                <label>Escribe un Comentario</label>
                <div class="clear width20">
                    <textarea id="txtComentario" placeholder="Escribe tu comentario aqui..." class="input400" rows="4" cols="5"></textarea>
                    <br /><br />
                </div>
                <div id="subir"></div>
            </fieldset>
            <br />
            <div class="clear right">
                <a class="btn verde" id="btnAceptarTermino">Aceptar</a>
                <a class="btn blanco" id="btnCancelarTermino">Cancelar</a>
            </div>
        </div>
        <div id="rechazar" class="alert clear width90 center frm hide">
            <fieldset>
                <legend class="verde">
                    <span id="icon-25" class="evidencia blanco"></span>
                    <a color="FFFFFF" id="folior"></a><a id="A3" href=""></a>
                </legend>
                <span id="icon-90" class="verde warning left"></span>
                <p class="width70 left txt-justify">Se enviar&aacute; la incidencia a revisi&oacute;n.</p>

                <div class="clear">
                    <label for="txtComentarioRechazo">Escribe un comentario:</label>                
                </div>
                <br />
                <textarea id="txtComentarioRechazo" placeholder="Escribe tu comentario aqui..." class="input400" rows="4" cols="10"></textarea>
                <div class="left width50" id="fecha"></div>
                <br /><br />
                <div class="left width50" id="subirRechazo"></div>
                <br /><br /><br />
            </fieldset>
            <br />
            <div class="clear txt-right">
                <a class="btn verde" id="btnAceptarRechazo">Aceptar</a>
                <a class="btn blanco" id="btnCancelarRechazo">Cancelar</a>
            </div>
        </div>
        <div id="cancelarIncidencia" class="alert clear width90 center frm hide">
            <fieldset>
                <legend class="verde">
                    <span id="icon-25" class="evidencia blanco"></span>
                    <a color="FFFFFF" id="foliori"></a><a id="A5" href=""></a>
                </legend>
                <span id="icon-90" class="verde warning left"></span>
                <p class="width70 left txt-justify">Se cancelar&aacute; la petici&oacute;n.</p>

                <div class="clear">
                    <label for="txtComentarioCancelarIncidencia">Escribe un comentario:</label>
                </div>
                <br />
                <textarea id="txtComentarioCancelarIncidencia" placeholder="Escribe tu comentario aqui..." class="input400" rows="4" cols="10"></textarea>
                <div class="left width50" id="subirCancelaIncidencia"></div>
                <br /><br />
            </fieldset>
            <br />
            <div class="clear txt-right">
                <a class="btn verde" id="btnAceptarCancelacion">Aceptar</a>
                <a class="btn verde hide" id="btnAceptarEnviarM">Aceptar</a>
                <label id="lblboton"></label>
            </div>
        </div>
        <div id="responder" class="alert clear width90 center frm hide">
            <fieldset>
                <legend class="verde">
                    <span id="icon-25" class="evidencia blanco"></span>
                    <a color="FFFFFF" id="folioRes"></a><a id="A4" href=""></a>
                </legend>
                <span id="icon-90" class="verde warning left"></span>
                <p class="width70 left txt-justify">Se enviar&aacute; la incidencia a validaci&oacute;n.</p>
                <div class="clear">
                    <label for="txtComentarioEnvio">Escribe un comentario:</label>
                </div>
                <textarea id="txtComentarioEnvio" placeholder="Escribe tu comentario aqui..." class="input400" rows="4" cols="10"></textarea><br />
                <div id="url"></div>
                <div class="clear" id="subirRespuesta"></div>
                <br /><br />
            </fieldset>
            <br />
            <div class="clear width98 txt-right">
                <a class="btn verde" id="btnAceptarEnvio">Aceptar</a>
                <a class="btn blanco" id="btnCancelarEnvio">Cancelar</a>
            </div>
        </div>
        <div id="avance" class="alert clear width90 center frm hide">
            <fieldset>
                <legend class="verde">
                    <span id="icon-25" class="evidencia blanco"></span>
                    <a color="FFFFFF" id="afolio" style="color: #FFFFFF;"></a><a id="a5" href=""></a>
                </legend>
                <span id="icon-90" class="verde warning left"></span>
                <p class="width70 left txt-justify">Se enviar&aacute; el avance de la consulta.</p>
                <div class="clear">
                    <label for="txtComentarioEnvio">Escribe un comentario:</label>
                </div>
                <textarea id="txtComentarioAvance" placeholder="Escribe tu comentario aqui..." class="input400" rows="4" cols="10"></textarea><br />
                <div class="clear" id="subirAvance"></div>
                <br /><br />
            </fieldset>
            <br />
            <div class="clear width98 txt-right">
                <a class="btn verde" id="btnAceptarAvance">Aceptar</a>
                <a class="btn blanco" onclick="javascript:cerrar();">Cancelar</a>
            </div>
        </div>
        <div id="consulta" class="alert clear width98 center frm"><br />
            <fieldset>
                <legend id="legendFolio" style="background-color:ghostwhite !important;"></legend>
                <input type="hidden" value="" id="txtIdReporte" />
<%--                <table>
                    <tr>
                        <td>
                            <label class="txt-verde">Fecha incidencia:</label>
                            <span id="icon-25" class="calendario"><label id="lblFechaReporte"></label></span>
                        </td>
                        <td style="padding-left:20px !important;">
                            <div class="left with25" id="fechaConsulta">
                            <div class="left with25" id="fechatermino">
                                <label class="txt-verde" >Fecha Termino:</label>                    
                                <span id="icon-25" class="calendario" onclick="javascript:fechaPropuestaTermino();" >   
                                    <input type="text" class="input98" id="txtFechaTermino""/>
                                <span>                        
                            </div>
                        </td>
                        <td style="padding-left:20px !important;">
                            <div id="responsable" class="left width50">
                                <div id="nombreResp"></div>
                                <label id="lblPersonalApoyo"></label>
                                <div id="contenedorAuto"></div>
                                <div id="urlConsulta"></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label class="txt-verde">Fecha Propuesta:</label>
                            <span id="icon-25" class="calendario azul"><label id="lblFechaPropuesta"></label></span>
                        </td>
                        
                    </tr>
                </table>

                <div class="center width98">
                    <div class="left with25" style="margin-right:2.5rem;">
                        <label for="" class="txt-verde">Fecha incidencia:</label>
                        <span id="icon-25" class="calendario"><label id="lblFechaReporte"></label></span>
                    </div>
                    <div class="left with25" style="margin-right:2.5rem;" id="fechaConsulta">
                    </div>
                    <div class="left with25" style="margin-right:2.5rem;" id="fechatermino">
                        <label for="" class="txt-verde" >Fecha Termino:</label>                    
                        <span id="icon-25" class="calendario" onclick="javascript:fechaPropuestaTermino();" >   
                            <input type="text" class="input98" id="txtFechaTermino""/>
                        <span>                        
                    </div>
                    <div class="left with25" style="margin-right:2.5rem;">
                        <label for="" class="txt-verde">Fecha Propuesta:</label>
                        <span id="icon-25" class="calendario azul"><label id="lblFechaPropuesta"></label></span>
                    </div>
                    <div class="left with98">
                        <div id="responsable" class="left width98">
                            <div id="nombreResp"></div>
                            <label id="lblPersonalApoyo"></label>
                            <div id="contenedorAuto"></div>
                            <div id="urlConsulta"></div>
                        </div>
                    </div>
                    <div class="left width98" id="asunto-comentario" style="padding-top:10px;"></div>
                    <div class="clear left with25" id="evidencia"  style="display: inline-block; padding-top:2.5rem;"></div>
                </div>--%>
                <br />
                <div class="left width25">
                    <label for="" class="txt-verde lbl">Fecha incidencia:</label>
                    <br /><br />
                    <span id="icon-25" class="calendario azul"><label id="lblFechaReporte"></span></label>
                    <br /><br />
                    <div id="fechaConsulta"></div>
                    <div id="fechatermino">
                        <label for="" class="txt-verde lbl">Fecha Termino:</label><br /><br />
                        <span onclick="javascript:fechaPropuestaTermino();" id="icon-25" class="calendario"><span>
                        <input type="text" class="input100" id="txtFechaTermino" />                        
                    </div>
                    <br /><br />
                    
                </div>
                <div class="left width25">
                    <label for="" class="txt-verde lbl">Fecha Propuesta:</label>
                    <br /><br />
                    <span id="icon-25" class="calendario azul"><label id="lblFechaPropuesta"></span></label>
                    <br /><br />
                    <div id="divFolioVinculado">
                        <%--<label for="txtFechaTermino" class="txt-verde lbl">Folio Vinculado:</label><br><br><label><span id="icon-25" class="ticket"> WSHOP-2023-0001</span></label>   --%>                  
                    </div>
                    <br />
                </div>
                <div id="responsable" class="left width50">
                    <div id="nombreResp"></div>
                    <br />
                    <div id="lblPersonalApoyo"></div>
                    <div id="contenedorAuto"></div>
                    <div id="urlConsulta"></div>
                </div>
                <div class="left width98" id="divPoints"></div>
                
                <div class="left width98" id="asunto-comentario"></div>
                <div class="clear">
                    <div id="evidencia" class="clear" style="display: inline-block"></div>
                </div>
                <div id="lblsistemas" class="clear left width50"></div>
            </fieldset>
        </div>
        <br />
        <br />
        <div class="width100">
            <a class="btn verde btn-acciones-reporte" id="btnTerminarReporte" title="Concluir Incidencia"><span class="blanco">VALIDAR</span></a>
            <a class="btn blanco btn-acciones-reporte" id="btnRechazarReporte" title="Rechazar Incidencia"><span class="azul">RECHAZAR</span></a>
            <a class="btn verde btn-acciones-reporte" id="btnResponderReporte" title="Responder Incidencia"><span class="blanco">RESPONDER</span></a>
            <a class="btn verde btn-acciones-reporte" id="btnAsignarReporte" title="Asignar Incidencia"><span class="blanco">ASIGNAR</span></a>
            <a class="btn azul btn-acciones-reporte hide" id="btnAvance" title="Enviar Avance"><span class="blanco">AVANCE</span></a>
            <a class="btn azul btn-acciones-reporte hide" id="btnCerrarDetalle" title="Cerrar Detalle Ticket Vinculado"><span class="blanco">AVANCE</span></a>
            <label id="boton"></label>
            <label id="btnRegresar"></label>
            <label id="lblPausarReanudar"></label>
            <a href="#" class="btn blanco btn-acciones-reporte" id="cerrar" onclick="javascript:cerrarDialogAsignado();">CERRAR</a>
            <label id="divAcciones" class="clear txt-right"></label>
        </div>
        <br />
        <br />
        <br />
        <div class=" clear">
            <asp:Label ID="lblTablaDetalleR" runat="server"></asp:Label>
        </div>
    </div>
    
    <div id="dlogConfirmarAsig" class="hide frm" title="Confirmar">
        <div class="alert clear width90 center">
            <span id="icon-90" class="azul informacion left icon"></span>
            <p class="width70 left txt-justify">¿Seguro que desea asignarse la incidencia con las siguientes fechas?</p>
            <div class="width40 left" id="fpt"></div>
            <div class="width40 left" style="padding-left:25px;" id="ft"></div>
            <div class="clear width98" style="padding-left:40px;" id="texta"></div>
            <br />
        </div>
        <div class="clear txt-right">
            <a class="btn verde" id="btnAceptarAsignacion">Aceptar</a>
            <a class="btn blanco" id="btncancelarAsignacion" onclick="javascript:cerrarDialog('#dlogConfirmarAsig');">Cancelar</a>
        </div>
    </div>

    <div id="dialogPausarIncidencia" class="hide frm" title="Confirmar">
        <div class="alert clear width100 center">
            <div>
                <p class="width100 left txt-justify">
                    <span id="icon-47" class="azul informacion left icon" style="margin-right:1.25rem; margin-top:0px;"></span>
                    ¿Seguro que desea pausar la incidencia?
                </p>
            </div>
            <br />
            <div class="clear width100" id="divComentarioPausar">
                <label for="textaComentarioPausar">Escribe un comentario:</label>
                <textarea placeholder="Escribe un comentario.." id="textaComentarioPausar" class="width100" rows="5" cols="5"></textarea>
            </div>
            <br />
        </div>
        <div class="clear txt-right">
            <a class="btn verde" id="btnAceptarPausar">Aceptar</a>
            <a class="btn blanco" id="btnCancelarPausar" onclick="javascript:cerrarDialog('#dialogPausarIncidencia');">Cancelar</a>
        </div>
    </div>

    <asp:TextBox runat="server" ID="txtidUser" class="hide"></asp:TextBox>
    <input type="hidden" id="txtUrlValidar" />
    <input type="hidden" id="accion" />
    <input type="hidden" id="tipoReporte" />
    <input type="hidden" id="cal" />
    <input type="hidden" id="fechaT" />
    <input type="hidden" id="vc" />
    <input id="txtIdApoyo" type="text" class="hide" />
    <input type="hidden" id="txtid" class="" />
    <input type="hidden" id="txtnombreArchivo" />
</asp:Content>