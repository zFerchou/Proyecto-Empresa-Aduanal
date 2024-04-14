using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;
using System.Drawing;

using System.Configuration;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlClient;
/// <summary>
/// Descripción breve de ControllerGestionReportes
/// </summary>
public class ControllerGestionReportes
{
    private tReporte reporte;
    private ERPManagementDataContext erp;
    private view_reportePorId viewReportePorId;
    private view_HistorialReporte viewHistorialReporte;
    private vObtenerGraficasReportes obtenerGraficas;
    private cEstatusReporte estatusReporte;
    private tLogReporte tLogReporte;
    private tRespuestaReporte respuestaReporte;
    private ERPManagementRHDataContext erpRH;
    private tEmpleado empleado;
    private tJustificacion justificacion;
    public ControllerGestionReportes()
    {
        erp = new ERPManagementDataContext();
        reporte = new tReporte();
        viewReportePorId = new view_reportePorId();
        viewHistorialReporte = new view_HistorialReporte();
        obtenerGraficas = new vObtenerGraficasReportes();
        estatusReporte = new cEstatusReporte();
        tLogReporte = new tLogReporte();
        respuestaReporte = new tRespuestaReporte();
        empleado = new tEmpleado();
        justificacion = new tJustificacion();
    }

    /// <summary>
    /// Obtener todos los reportes (GENERAL)
    /// </summary>
    /// <returns></returns>
    public string getTblReportes(int idUsuario, bool isTipoReporteFull= false )
    {
        string result = "";   
     
        erpRH = new ERPManagementRHDataContext();
        var idTipoUsuario = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                             where vValUser.idUsuario == idUsuario
                             select vValUser).FirstOrDefault();
        if (idTipoUsuario.idTipoUsuario==1)
        {
            var lstReportes = (from reportes in erp.tReporte
                                   //where reportes.idEstatusReporte != 6 && reporte.idTipoReporte < 5
                               select reportes);
            if (isTipoReporteFull)
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte != 6));
            }
            else
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte != 6 && reportes.idTipoReporte <= 4));
            }
            
            if (lstReportes.ToList().Count > 0)
            {
                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                result +=
                          "<table id='tblReportes' class='data_grid display'>" +
                            "<thead id='grid-head2'>" +
                                "<th>Folio</th>" +
                    //"<th>Estatus</th>" +
                                "<th>Usuario</th>" +
                                "<th>Tipo Incidencia</th>" +
                                "<th>Asunto</th>" +
                                "<th>Empresa</th>" +
                                "<th>Sistema/Área</th>" +
                                "<th>Prioridad</th>" +
                                "<th>Fecha</th>" +
                                "<th>Acciones</th>" +
                            "</thead>" +
                            "<tbody id='grid-body'>";

                foreach (tReporte t in lstReportes)
                {
                    result += "<tr id=" + t.idReporte + ">";
                    result += "<td>" + t.folio + "</td>";
                    //result += "<td>" + t.idEstatusReporte + "</td>";
                    result += "<td>" + cUsuarios.getUsuarioById(t.idUsuario.GetValueOrDefault()) + "</td>";
                    result += "<td>" + t.cTipoReporte.nombreTipoReporte + "</td>";
                    result += "<td>" + t.asunto + "</td>";
                    result += "<td>" + t.tERPGrupo.nomGrupo + "</td>";

                    if (t.idSistema != 0 && t.idArea == 0)
                    {
                        result += "<td>" + t.cSistemas.nomSistema + "</td>";
                    }
                    else if (t.idSistema == null || t.idArea == null)
                    {
                        result += "<td>No Aplica</td>";
                    }
                    else if (t.idSistema.Equals(0) && t.idArea != null)
                    {
                        var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                                             where vValUser.idUsuario == t.idUsuario
                                             select vValUser).FirstOrDefault();

                        result += "<td>" + validacionUsu.area + "</td>";
                    }
                    result += "<td>" + t.cPrioridadReporte.nombrePrioridad + "</td>";
                    result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaReporte) + "</span>" + String.Format("{0:dd/MM/yyyy}", t.fechaReporte) + "</td>";
                    if (isTipoReporteFull)
                    {
                        result += "<td><a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                    }
                    else
                    {
                        if (t.idEstatusReporte == 2 || t.idEstatusReporte == 5)
                        {
                            result += "<td><a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                        }
                        else
                        {
                            result += "<td class='ocultar'><a onclick='javascript:eliminarReporte(" + t.idReporte + ");'><span id='icon-25' class='eliminar azul' title='Eliminar'></span></a> | <a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                        }
                    }
                   
                    result += "</tr>";
                }

                result += "</tbody>" +
                            "<tfoot>" +
                                "<tr>" +
                                    "<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>" +
                                "</tr>" +
                            "</tfoot>" +
                            "</table>";
            }
            else
            {
                result += "<div id='divTblApoyo123'><div align='center' style='font-size: 10px !important;margin-left: 25%;' class='bg-alert width50'><div style='width:10%' class='left'><span id='icon-25' class='blanco warning'></span></div><div style='width:90%; text-align: justify; padding-top:10px; padding-bottom:10px'>No hay información para mostrar.</div></div></div>°";
            }
        }
        else if (idTipoUsuario.idTipoUsuario == 2)
        {
            var lstReportes = (from reportes in erp.tReporte
                               //where reportes.idEstatusReporte != 6 && reportes.idTipoReporte >=5                               
                               select reportes);
            lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte != 6 && reportes.idTipoReporte >= 5));
            if (lstReportes.ToList().Count > 0)
            {
                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                result +=
                          "<table id='tblReportes' class='data_grid display'>" +
                            "<thead id='grid-head2'>" +
                                "<th>Folio</th>" +
                    //"<th>Estatus</th>" +
                                "<th>Usuario</th>" +
                                "<th>Tipo Incidencia</th>" +
                                "<th>Asunto</th>" +
                                "<th>Empresa</th>" +
                                "<th>Sistema/Área</th>" +
                                "<th>Prioridad</th>" +
                                "<th>Fecha</th>" +
                                "<th>Acciones</th>" +
                            "</thead>" +
                            "<tbody id='grid-body'>";

                foreach (tReporte t in lstReportes)
                {
                    result += "<tr id=" + t.idReporte + ">";
                    result += "<td>" + t.folio + "</td>";
                    //result += "<td>" + t.idEstatusReporte + "</td>";
                    result += "<td>" + cUsuarios.getUsuarioById(t.idUsuario.GetValueOrDefault()) + "</td>";
                    result += "<td>" + t.cTipoReporte.nombreTipoReporte + "</td>";
                    result += "<td>" + t.asunto + "</td>";
                    result += "<td>" + t.tERPGrupo.nomGrupo + "</td>";

                    if (t.idSistema != 0)
                    {
                        result += "<td>" + t.cSistemas.nomSistema + "</td>";
                    }
                    else if (t.idSistema.Equals(0) && t.idArea != null)
                    {
                        var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                                             where vValUser.idUsuario == t.idUsuario
                                             select vValUser).FirstOrDefault();

                        result += "<td>" + validacionUsu.area + "</td>";
                    }
                    else
                    {
                        result += "<td>N/A</td>";
                    }
                    result += "<td>" + t.cPrioridadReporte.nombrePrioridad + "</td>";
                    result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaReporte) + "</span>" + String.Format("{0:dd/MM/yyyy}", t.fechaReporte) + "</td>";
                    if (t.idEstatusReporte == 2 || t.idEstatusReporte == 5)
                    {
                        result += "<td><a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                    }
                    else
                    {
                        result += "<td class='ocultar'><a onclick='javascript:eliminarReporte(" + t.idReporte + ");'><span id='icon-25' class='eliminar azul' title='Eliminar'></span></a> | <a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                    }
                    result += "</tr>";
                }

                result += "</tbody>" +
                            "<tfoot>" +
                                "<tr>" +
                                    "<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>" +
                                "</tr>" +
                            "</tfoot>" +
                            "</table>";
            }
            else
            {
                result += "<div id='divTblApoyo123'><div align='center' style='font-size: 10px !important;margin-left: 25%;' class='bg-alert width50'><div style='width:10%' class='left'><span id='icon-25' class='blanco warning'></span></div><div style='width:90%; text-align: justify; padding-top:10px; padding-bottom:10px'>No hay información para mostrar.</div></div></div>°";
            }
        }


        return result;

    }

    /// <summary>
    /// Obtener todos los reportes Sin Asignar
    /// </summary>
    /// <returns></returns>
    public string getTblReportesSinAsignar()
    {
        string result = "";
        var lstReportes = (from reportes in erp.tReporte
                           where reportes.idEstatusReporte == 1
                           select reportes).ToList();

        if (lstReportes.Count > 0)
        {
            ControllerUsuarios cUsuarios = new ControllerUsuarios();
            result +=
                      "<table id='tblReporteSinAsignar' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Folio</th>" +
                            "<th>Usuario</th>" +
                            "<th>Tipo Incidencia</th>" +
                            "<th>Asunto</th>" +
                            "<th>Empresa</th>" +
                            "<th>Sistema/Área</th>" +
                            "<th>Prioridad</th>" +
                            "<th>Fecha</th>" +
                            "<th>Acciones</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            foreach (tReporte t in lstReportes)
            {
                result += "<tr>";
                result += "<td>" + t.folio + "</td>";
                result += "<td>" + cUsuarios.getUsuarioById(t.idUsuario.GetValueOrDefault()) + "</td>";
                result += "<td>" + t.cTipoReporte.nombreTipoReporte + "</td>";
                result += "<td>" + t.asunto + "</td>";
                result += "<td>" + t.tERPGrupo.nomGrupo + "</td>";
                result += "<td>" + t.cSistemas.nomSistema + "</td>";
                result += "<td>" + t.cPrioridadReporte.nombrePrioridad + "</td>";
                result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaReporte) + "</span>" + String.Format("{0:MM/dd/yyyy}", t.fechaReporte) + "</td>";
                result += "<td><a onclick='javascript:eliminarReporteSinAsignar(" + t.idReporte + ");'><span id='icon-25' class='eliminar azul' title='Eliminar'></span></a> | <a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul'></span></a></td>";
                result += "</tr>";
            }


            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>";
        }

        return result;

    }

    /// <summary>
    /// Obtener todos los reportes  Asignados
    /// </summary>
    /// <returns></returns>
    public string getTblReportesAsignados()
    {
        string result = "";
        var lstReportes = (from reportes in erp.tReporte
                           where reportes.idEstatusReporte == 2
                           select reportes).ToList();

        if (lstReportes.Count > 0)
        {
            ControllerUsuarios cUsuarios = new ControllerUsuarios();
            result +=
                      "<table id='tblReporteAsignados' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Folio</th>" +
                            "<th>Usuario</th>" +
                            "<th>TipoReporte</th>" +
                            "<th>Asunto</th>" +
                            "<th>Empresa</th>" +
                            "<th>Sistema/Área</th>" +
                            "<th>Prioridad</th>" +
                            "<th>Fecha</th>" +
                            "<th>Acciones</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            foreach (tReporte t in lstReportes)
            {
                result += "<tr>";
                result += "<td>" + t.folio + "</td>";
                result += "<td>" + cUsuarios.getUsuarioById(t.idUsuario.GetValueOrDefault()) + "</td>";
                result += "<td>" + t.cTipoReporte.nombreTipoReporte + "</td>";
                result += "<td>" + t.asunto + "</td>";
                result += "<td>" + t.tERPGrupo.nomGrupo + "</td>";
                result += "<td>" + t.cSistemas.nomSistema + "</td>";
                result += "<td>" + t.cPrioridadReporte.nombrePrioridad + "</td>";
                result += "<td>" + String.Format("{0:MM/dd/yyyy}", t.fechaReporte) + "</td>";
                //result += "<td><a onclick='javascript:eliminarReporteAsignado(" + t.idReporte + ");'><span id='icon-25' class='eliminar rojo' title='Eliminar'></span></a> | <a onclick='javascript:consultarReporteAsignado(" + t.idReporte + ");'><span id='icon-25' class='consultar verde' title='Ver Detalle'></span></a></td>";
                result += "<td><a onclick='javascript:consultarReporteAsignado(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                result += "</tr>";
            }


            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>";
        }
        return result;

    }

    /// <summary>
    /// Eliminar Reportes (GENERAL)
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns></returns>
    public bool eliminarReporte(int idReporte, int idUsuario)
    {
        try
        {
            var reporte = (from tr in erp.tReporte
                           where tr.idReporte == idReporte
                           select tr).FirstOrDefault();

            reporte.idEstatusReporte = 6;

            erp.SubmitChanges();

            string fechaAccion = DateTime.Now.ToShortDateString();
            DateTime fechaAcc = Convert.ToDateTime(fechaAccion);
            tLogReporte.idReporte = idReporte;
            tLogReporte.idUsuario = idUsuario;
            tLogReporte.fecha = fechaAcc;
            tLogReporte.idAccion = 2;

            erp.tLogReporte.InsertOnSubmit(tLogReporte);
            erp.SubmitChanges();

            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    /// <summary>
    /// Eliminar Reportes Sin Asignar
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns></returns>
    public bool eliminarReporteSinAsignar(int idReporte)
    {
        try
        {
            var registro = (from tReporte in erp.tReporte
                            where tReporte.idReporte == idReporte
                            select tReporte).Single();

            erp.tReporte.DeleteOnSubmit(registro);
            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    /// <summary>
    /// Consultar reporte(GENERAL)
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns></returns>
    public List<string> consultarReporte(int idReporte)
    {
        List<string> result = new List<string>();
        string asunto = "";
        string descripcion = "";
        string fechaReporte = "";
        string fechaPropuesta = "";
        int idEstatusSistema = 0;
        int claveReporte = 0;
        int calificacion = 0;
        int idPrioridad = 0;
        int idTipoIncidencia = 0;
        int idGrupo = 0;
        int idSistema = 0;
        var registro = (from reporte in erp.tReporte
                        where reporte.idReporte == idReporte
                        select reporte).ToList();
        if (registro.Count > 0)
        {
            foreach (var t in registro)
            {
                claveReporte = t.idReporte;
                asunto = t.asunto;
                descripcion = t.descripcion;
                fechaReporte = String.Format("{0:MM/dd/yyyy}", t.fechaReporte);
                fechaPropuesta = String.Format("{0:MM/dd/yyyy}", t.fechaPropuesta);
                idEstatusSistema = t.idEstatusReporte;
                calificacion = t.calificacion.GetValueOrDefault();
                idPrioridad = t.idPrioridad.GetValueOrDefault();
                idTipoIncidencia = t.idTipoReporte.GetValueOrDefault();
                idGrupo = t.idERPGrupo.GetValueOrDefault();
                idSistema = t.idSistema.GetValueOrDefault();
                result.Add(Convert.ToString(claveReporte));
                result.Add(asunto);
                result.Add(descripcion);
                result.Add(fechaReporte);
                result.Add(fechaPropuesta);
                result.Add(Convert.ToString(idEstatusSistema));
                result.Add(Convert.ToString(calificacion));
                result.Add(Convert.ToString(idPrioridad));
                result.Add(Convert.ToString(idTipoIncidencia));
                result.Add(Convert.ToString(idGrupo));
                result.Add(Convert.ToString(idSistema));
            }

        }
        return result;
    }

    /// <summary>
    /// Consultar reporte asignado por idReporte
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns></returns>
    public List<string> consultarReporteSinAsignar(int idReporte)
    {
        List<string> result = new List<string>();
        string asunto = "";
        string descripcion = "";
        string fechaReporte = "";
        string fechaPropuesta = "";
        int claveReporte = 0;

        var registro = (from reporte in erp.tReporte
                        where reporte.idReporte == idReporte
                        select reporte).ToList();

        //var registro = (from viewReportePorId in erp.view_reportePorId
        //                where viewReportePorId.idReporte == idReporte
        //                select viewReportePorId).ToList();
        if (registro.Count > 0)
        {
            foreach (var t in registro)
            {
                claveReporte = t.idReporte;
                asunto = t.asunto;
                descripcion = t.descripcion;
                fechaReporte = String.Format("{0:MM/dd/yyyy}", t.fechaReporte);
                fechaPropuesta = String.Format("{0:MM/dd/yyyy}", t.fechaPropuesta);

                result.Add(Convert.ToString(claveReporte));
                result.Add(asunto);
                result.Add(descripcion);
                result.Add(fechaReporte);
                result.Add(fechaPropuesta);
            }

        }
        return result;
    }

    /// <summary>
    /// Eliminar Reporte Asignado
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns></returns>
    public bool eliminarReporteAsignado(int idReporte)
    {
        try
        {
            var registro = (from tReporte in erp.tReporte
                            where tReporte.idReporte == idReporte
                            select tReporte).Single();

            erp.tReporte.DeleteOnSubmit(registro);
            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    /// <summary>
    /// Consultar reporte asignado por idReporte    
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns></returns>
    public List<string> consultarReporteAsignado(int idReporte)
    {
        List<string> result = new List<string>();
        string asunto = "";
        string descripcion = "";
        string fechaReporte = "";
        string fechaPropuesta = "";
        int claveReporte = 0;

        var registro = (from reporte in erp.tReporte
                        where reporte.idReporte == idReporte
                        select reporte).ToList();
        if (registro.Count > 0)
        {
            foreach (var t in registro)
            {
                claveReporte = t.idReporte;
                asunto = t.asunto;
                descripcion = t.descripcion;
                fechaReporte = String.Format("{0:MM/dd/yyyy}", t.fechaReporte);
                fechaPropuesta = String.Format("{0:MM/dd/yyyy}", t.fechaPropuesta);

                result.Add(Convert.ToString(claveReporte));
                result.Add(asunto);
                result.Add(descripcion);
                result.Add(fechaReporte);
                result.Add(fechaPropuesta);
            }

        }
        return result;
    }

    /// <summary>
    /// Obtener los Reportes Asignados según los filtros especificado
    /// </summary>
    /// <param name="idAsignado"></param>
    /// <param name="idSinAsignar"></param>
    /// <param name="folio"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <param name="idFallo"></param>
    /// <param name="idFuncionalidad"></param>
    /// <param name="idERP"></param>
    /// <returns></returns>
    public string getTblReportesFiltrados(int idAsignado, int idSinAsignar, int idPorValidar, int idTerminado, string folio, DateTime fechaInicio, DateTime fechaFin, int idFallo, int idFuncionalidad, int idERP, int idGrupo, int idUsuario, bool isTipoReporteFull=false)
    {
        string fechaI = String.Format("{0:yyyy-MM-dd}", fechaInicio);
        string fechaF = String.Format("{0:yyyy-MM-dd}", fechaFin);
        string result = "";
        erpRH = new ERPManagementRHDataContext();
        var idTipoUsuario = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                             where vValUser.idUsuario == idUsuario
                             select vValUser).FirstOrDefault();
        
        if (idTipoUsuario.idTipoUsuario==1)
        {
            var lstReportes = from reportes in erp.tReporte
                              //where reportes.idEstatusReporte != 6 && reporte.idTipoReporte <=4
                              select reportes;
            if (isTipoReporteFull)
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte != 6));
            }
            else
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte != 6 && reportes.idTipoReporte <= 4));
            }
            
            if (idAsignado != 0)
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idTerminado));
            }
            if (idSinAsignar != 0)
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idTerminado));
            }
            if (idPorValidar != 0)
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idTerminado));
            }
            if (idTerminado != 0)
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte == idTerminado || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idPorValidar));
            }
            if (folio != "")
            {
                lstReportes = lstReportes.Where(reportes => reportes.folio == folio);
            }
            if (fechaI != "0001-01-01" && fechaF == "0001-01-01")
            {
                lstReportes = lstReportes.Where(reportes => reportes.fechaReporte == Convert.ToDateTime(fechaI));
            }
            if (fechaF != "0001-01-01" && fechaI == "0001-01-01")
            {
                lstReportes = lstReportes.Where(reportes => reportes.fechaReporte == Convert.ToDateTime(fechaF));
            }
            if (fechaI != "0001-01-01" && fechaF != "0001-01-01")
            {
                lstReportes = lstReportes.Where(reportes => reportes.fechaReporte >= Convert.ToDateTime(fechaI) && reportes.fechaReporte <= Convert.ToDateTime(fechaF));
            }
            if (idFallo != 0)
            {
                lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idERP);
            }
            if (idFuncionalidad != 0)
            {
                lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idERP);
            }
            if (idERP != 0)
            {
                lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idERP || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo);
            }
            if (idGrupo != 0)
            {
                lstReportes = lstReportes.Where(reportes => reportes.idERPGrupo == idGrupo);
            }
            if (lstReportes.ToList().Count > 0)
            {
                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                result +=
                          "<table id='tblReportes' class='data_grid display'>" +
                            "<thead id='grid-head2'>" +
                                "<th>Folio</th>" +
                                "<th>Usuario</th>" +
                                "<th>TipoReporte</th>" +
                                "<th>Asunto</th>" +
                                "<th>Empresa</th>" +
                                "<th>Sistema/Área</th>" +
                                "<th>Prioridad</th>" +
                                "<th>Fecha</th>" +
                                "<th class='ocultar'>Acciones</th>" +
                            "</thead>" +
                            "<tbody id='grid-body'>";

                foreach (tReporte t in lstReportes)
                {
                    result += "<tr id" + t.idReporte + ">";
                    result += "<td>" + t.folio + "</td>";
                    result += "<td>" + cUsuarios.getUsuarioById(t.idUsuario.GetValueOrDefault()) + "</td>";
                    result += "<td>" + t.cTipoReporte.nombreTipoReporte + "</td>";
                    result += "<td>" + t.asunto + "</td>";
                    result += "<td>" + t.tERPGrupo.nomGrupo + "</td>";
                    if (t.idSistema != 0 && t.idArea == 0)
                    {
                        result += "<td>" + t.cSistemas.nomSistema + "</td>";
                    }
                    else if (t.idSistema == null || t.idArea == null)
                    {
                        result += "<td>No Aplica</td>";
                    }
                    else if (t.idSistema.Equals(0) && t.idArea != null)
                    {
                        var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                                             where vValUser.idUsuario == t.idUsuario
                                             select vValUser).FirstOrDefault();

                        result += "<td>" + validacionUsu.area + "</td>";
                    }
                    result += "<td>" + t.cPrioridadReporte.nombrePrioridad + "</td>";
                    result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaReporte) + "</span>" + String.Format("{0:dd/MM/yyyy}", t.fechaReporte) + "</td>";
                    if (isTipoReporteFull)
                    {
                        result += "<td><a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                    }
                    else
                    {
                        if (t.idEstatusReporte == 2 || t.idEstatusReporte == 5)
                        {
                            result += "<td><a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                        }
                        else
                        {
                            result += "<td class='ocultar'><a onclick='javascript:eliminarReporte(" + t.idReporte + ");'><span id='icon-25' class='eliminar azul' title='Eliminar'></span></a> | <a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                        }
                    }
                    
                    result += "</tr>";
                }
                result += "</tbody>" +
                            "<tfoot>" +
                                "<tr>" +
                                    "<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>" +
                                "</tr>" +
                            "</tfoot>" +
                            "</table>";
            }
            else
            {
                result += "<div id='divTblApoyo123'><div align='center' style='font-size: 10px !important;margin-left: 25%;' class='bg-alert width50'><div style='width:10%' class='left'><span id='icon-25' class='blanco warning'></span></div><div style='width:90%; text-align: justify; padding-top:10px; padding-bottom:10px'>No se encontraron resultados.</div></div></div>°";
            }
        }
        else if (idTipoUsuario.idTipoUsuario==2)
        {
            var lstReportes = from reportes in erp.tReporte
                              //where reportes.idEstatusReporte != 6 && reporte.idTipoReporte >=5
                              select reportes;
            lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte != 6 && reportes.idTipoReporte >= 5));
            if (idAsignado != 0)
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idTerminado));
            }
            if (idSinAsignar != 0)
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idTerminado));
            }
            if (idPorValidar != 0)
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idTerminado));
            }
            if (idTerminado != 0)
            {
                lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte == idTerminado || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idPorValidar));
            }
            if (folio != "")
            {
                lstReportes = lstReportes.Where(reportes => reportes.folio == folio);
            }
            if (fechaI != "0001-01-01" && fechaF == "0001-01-01")
            {
                lstReportes = lstReportes.Where(reportes => reportes.fechaReporte == Convert.ToDateTime(fechaI));
            }
            if (fechaF != "0001-01-01" && fechaI == "0001-01-01")
            {
                lstReportes = lstReportes.Where(reportes => reportes.fechaReporte == Convert.ToDateTime(fechaF));
            }
            if (fechaI != "0001-01-01" && fechaF != "0001-01-01")
            {
                lstReportes = lstReportes.Where(reportes => reportes.fechaReporte >= Convert.ToDateTime(fechaI) && reportes.fechaReporte <= Convert.ToDateTime(fechaF));
            }
            if (idFallo != 0)
            {
                lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idERP);
            }
            if (idFuncionalidad != 0)
            {
                lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idERP);
            }
            if (idERP != 0)
            {
                lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idERP || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo);
            }
            if (idGrupo != 0)
            {
                lstReportes = lstReportes.Where(reportes => reportes.idERPGrupo == idGrupo);
            }
            if (lstReportes.ToList().Count > 0)
            {
                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                result +=
                          "<table id='tblReportes' class='data_grid display'>" +
                            "<thead id='grid-head2'>" +
                                "<th>Folio</th>" +
                                "<th>Usuario</th>" +
                                "<th>TipoReporte</th>" +
                                "<th>Asunto</th>" +
                                "<th>Empresa</th>" +
                                "<th>Sistema/Área</th>" +
                                "<th>Prioridad</th>" +
                                "<th>Fecha</th>" +
                                "<th class='ocultar'>Acciones</th>" +
                            "</thead>" +
                            "<tbody id='grid-body'>";

                foreach (tReporte t in lstReportes)
                {
                    result += "<tr id" + t.idReporte + ">";
                    result += "<td>" + t.folio + "</td>";
                    result += "<td>" + cUsuarios.getUsuarioById(t.idUsuario.GetValueOrDefault()) + "</td>";
                    result += "<td>" + t.cTipoReporte.nombreTipoReporte + "</td>";
                    result += "<td>" + t.asunto + "</td>";
                    result += "<td>" + t.tERPGrupo.nomGrupo + "</td>";
                    if (t.idSistema != 0)
                    {
                        result += "<td>" + t.cSistemas.nomSistema + "</td>";
                    }
                    else if (t.idSistema.Equals(0) && t.idArea != null)
                    {
                        var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                                             where vValUser.idUsuario == t.idUsuario
                                             select vValUser).FirstOrDefault();

                        result += "<td>" + validacionUsu.area + "</td>";
                    }
                    else
                    {
                        result += "<td>N/A</td>";
                    }
                    result += "<td>" + t.cPrioridadReporte.nombrePrioridad + "</td>";
                    result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaReporte) + "</span>" + String.Format("{0:dd/MM/yyyy}", t.fechaReporte) + "</td>";

                    if (isTipoReporteFull)
                    {
                        result += "<td><a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                    }
                    else
                    {
                        if (t.idEstatusReporte == 2 || t.idEstatusReporte == 5)
                        {
                            result += "<td><a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                        }
                        else
                        {
                            result += "<td class='ocultar'><a onclick='javascript:eliminarReporte(" + t.idReporte + ");'><span id='icon-25' class='eliminar azul' title='Eliminar'></span></a> | <a onclick='javascript:consultarReporte(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                        }
                    }
                    
                    result += "</tr>";
                }
                result += "</tbody>" +
                            "<tfoot>" +
                                "<tr>" +
                                    "<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>" +
                                "</tr>" +
                            "</tfoot>" +
                            "</table>";
            }
            else
            {
                result += "<div id='divTblApoyo123'><div align='center' style='font-size: 10px !important;margin-left: 25%;' class='bg-alert width50'><div style='width:10%' class='left'><span id='icon-25' class='blanco warning'></span></div><div style='width:90%; text-align: justify; padding-top:10px; padding-bottom:10px'>No se encontraron resultados.</div></div></div>°";
            }
        }
        


        return result;
    }


    /// <summary>
    /// Obtener los Reportes Asignados según los filtros especificado
    /// </summary>
    /// <param name="idAsignado"></param>
    /// <param name="idSinAsignar"></param>
    /// <param name="folio"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <param name="idFallo"></param>
    /// <param name="idFuncionalidad"></param>
    /// <param name="idERP"></param>
    /// <returns></returns>
    public string getTblReportesAsignadosFiltrado(int idAsignado, int idSinAsignar, string folio, DateTime fechaInicio, DateTime fechaFin, int idFallo, int idFuncionalidad, int idERP)
    {
        string fechaI = String.Format("{0:yyyy-MM-dd}", fechaInicio);
        string fechaF = String.Format("{0:yyyy-MM-dd}", fechaFin);
        string result = "";
        var lstReportes = from reportes in erp.tReporte
                          select reportes;
        if (idAsignado != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idAsignado);
        }
        if (idSinAsignar != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idSinAsignar);
        }
        if (folio != "")
        {
            lstReportes = lstReportes.Where(reportes => reportes.folio == folio);
        }
        if (fechaI != "0001-01-01" && fechaF == "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte == Convert.ToDateTime(fechaI));
        }
        if (fechaF != "0001/01/01" && fechaI == "0001/01/01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaPropuesta == Convert.ToDateTime(fechaF));
        }
        if (fechaI != "0001-01-01" && fechaF != "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte >= Convert.ToDateTime(fechaI) && reportes.fechaPropuesta <= Convert.ToDateTime(fechaF));
        }
        if (idFallo != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idERP);
        }
        if (idFuncionalidad != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idERP);
        }
        if (idERP != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idERP || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo);
        }
        if (lstReportes.ToList().Count > 0)
        {
            ControllerUsuarios cUsuarios = new ControllerUsuarios();
            result +=
                      "<table id='tblFiltroReporteAsignados' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Folio</th>" +
                            "<th>Usuario</th>" +
                            "<th>TipoReporte</th>" +
                            "<th>Asunto</th>" +
                            "<th>Empresa</th>" +
                            "<th>Sistema/Área</th>" +
                            "<th>Prioridad</th>" +
                            "<th>Fecha</th>" +
                            "<th>Acciones</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            foreach (tReporte t in lstReportes)
            {
                result += "<tr>";
                result += "<td>" + t.folio + "</td>";
                result += "<td>" + cUsuarios.getUsuarioById(t.idUsuario.GetValueOrDefault()) + "</td>";
                result += "<td>" + t.cTipoReporte.nombreTipoReporte + "</td>";
                result += "<td>" + t.asunto + "</td>";
                result += "<td>" + t.tERPGrupo.nomGrupo + "</td>";
                result += "<td>" + t.cSistemas.nomSistema + "</td>";
                result += "<td>" + t.cPrioridadReporte.nombrePrioridad + "</td>";
                result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaReporte) + "</span>" + String.Format("{0:MM/dd/yyyy}", t.fechaReporte) + "</td>";
                result += "<td><a onclick='javascript:eliminarReporteAsignado(" + t.idReporte + ");'><span id='icon-25' class='eliminar azul' title='Eliminar'></span></a> | <a onclick='javascript:consultarReporteAsignado(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                result += "</tr>";
            }
            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>";
            //reportesAsignadosPDFList(lstReportes.ToList());
        }
        else
        {
            result += "No se encontraron resultados";
        }

        return result;
    }

    /// <summary>
    /// Obtener los Reportes Sin Asignar según los filtros especificado
    /// </summary>
    /// <param name="idAsignado"></param>
    /// <param name="idSinAsignar"></param>
    /// <param name="folio"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <param name="idFallo"></param>
    /// <param name="idFuncionalidad"></param>
    /// <param name="idERP"></param>
    /// <returns></returns>
    public string getTblReportesSinAsignarFiltrado(int idAsignado, int idSinAsignar, string folio, DateTime fechaInicio, DateTime fechaFin, int idFallo, int idFuncionalidad, int idERP)
    {
        string fechaI = String.Format("{0:yyyy-MM-dd}", fechaInicio);
        string fechaF = String.Format("{0:yyyy-MM-dd}", fechaFin);
        string result = "";
        var lstReportes = from reportes in erp.tReporte
                          where reportes.idEstatusReporte != 6
                          select reportes;
        if (idAsignado != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idAsignado);
        }
        if (idSinAsignar != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idSinAsignar);
        }
        if (folio != "")
        {
            lstReportes = lstReportes.Where(reportes => reportes.folio == folio);
        }
        if (fechaI != "0001-01-01" && fechaF == "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte == Convert.ToDateTime(fechaI));
        }
        if (fechaF != "0001/01/01" && fechaI == "0001/01/01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaPropuesta == Convert.ToDateTime(fechaF));
        }
        if (fechaI != "0001-01-01" && fechaF != "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte >= Convert.ToDateTime(fechaI) && reportes.fechaPropuesta <= Convert.ToDateTime(fechaF));
        }
        if (idFallo != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idERP);
        }
        if (idFuncionalidad != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idERP);
        }
        if (idERP != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idERP || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo);
        }
        if (lstReportes.ToList().Count > 0)
        {
            ControllerUsuarios cUsuarios = new ControllerUsuarios();
            result +=
                      "<table id='tblFiltroReporteSinAsignar' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Folio</th>" +
                            "<th>Usuario</th>" +
                            "<th>TipoReporte</th>" +
                            "<th>Asunto</th>" +
                            "<th>Empresa</th>" +
                            "<th>Sistema/Área</th>" +
                            "<th>Prioridad</th>" +
                            "<th>Fecha</th>" +
                            "<th>Acciones</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            foreach (tReporte t in lstReportes)
            {
                result += "<tr>";
                result += "<td>" + t.folio + "</td>";
                result += "<td>" + cUsuarios.getUsuarioById(t.idUsuario.GetValueOrDefault()) + "</td>";
                result += "<td>" + t.cTipoReporte.nombreTipoReporte + "</td>";
                result += "<td>" + t.asunto + "</td>";
                result += "<td>" + t.tERPGrupo.nomGrupo + "</td>";
                result += "<td>" + t.cSistemas.nomSistema + "</td>";
                result += "<td>" + t.cPrioridadReporte.nombrePrioridad + "</td>";
                result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaReporte) + "</span>" + String.Format("{0:MM/dd/yyyy}", t.fechaReporte) + "</td>";
                result += "<td><a onclick='javascript:eliminarReporteAsignado(" + t.idReporte + ");'><span id='icon-25' class='eliminar azul' title='Eliminar'></span></a> | <a onclick='javascript:consultarReporteAsignado(" + t.idReporte + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                result += "</tr>";
            }
            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>";
            //reportesAsignadosPDFList(lstReportes.ToList());
        }
        else
        {
            result += "No se encontraron resultados";
        }

        return result;
    }

    /// <summary>
    /// Generar los Reportes Asignados en PDF para usarlo cuando den Clic en "Generar Reporte Asignados".
    /// </summary>
    /// <param name="idAsignado"></param>
    /// <param name="idSinAsignar"></param>
    /// <param name="folio"></param>
    /// <param name="idFallo"></param>
    /// <param name="idFuncionalidad"></param>
    /// <param name="idERP"></param>
    public string generarPdfGeneral(int idAsignadoA, string folioA, DateTime fechaInicioA, DateTime fechaFinA, int idFalloA, int idFuncionalidadA, int idERPA, int idSinAsignarS, string folioS, DateTime fechaInicioS, DateTime fechaFinS, int idFalloS, int idFuncionalidadS, int idERPS)
    {
        string fechaIA = String.Format("{0:yyyy-MM-dd}", fechaInicioA);
        string fechaFA = String.Format("{0:yyyy-MM-dd}", fechaFinA);

        string fechaIS = String.Format("{0:yyyy-MM-dd}", fechaInicioS);
        string fechaFS = String.Format("{0:yyyy-MM-dd}", fechaFinS);

        var lstReportes = from reportes in erp.tReporte
                          select reportes;
        //var selected = users.Where(u => new[] { "Admin", "User", "Limited" }.Contains(u.User_Rights));
        if (idAsignadoA != 0 && idSinAsignarS != 0)
        {
            //lstReportes = lstReportes.Where(reportes =>  reportes.idEstatusReporte == idAsignadoA || reportes.idEstatusReporte == idSinAsignarS);
            lstReportes = lstReportes.Where(reportes => new[] { idAsignadoA, idSinAsignarS }.Contains(reportes.idEstatusReporte));

        }

        if (idAsignadoA != 0 && idSinAsignarS == 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idAsignadoA);
        }

        if (folioA != "")
        {
            lstReportes = lstReportes.Where(reportes => reportes.folio == folioA);
        }
        if (fechaIA != "0001-01-01" && fechaFA == "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte == Convert.ToDateTime(fechaIA));
        }
        if (fechaFA != "0001/01/01" && fechaIA == "0001/01/01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaPropuesta == Convert.ToDateTime(fechaFA));
        }
        if (fechaIA != "0001-01-01" && fechaFA != "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte >= Convert.ToDateTime(fechaIA) && reportes.fechaPropuesta <= Convert.ToDateTime(fechaFA));
        }
        //if (idFalloA != 0)
        //{
        //    lstReportes = lstReportes.Where(reportes => new[] {idFalloA,idFuncionalidadA,idERPA}.Contains(reportes.idTipoReporte.GetValueOrDefault()));
        //    //lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFalloA || reportes.idTipoReporte == idFuncionalidadA || reportes.idTipoReporte == idERPA);
        //}
        //if (idFuncionalidadA != 0)
        //{
        //    lstReportes = lstReportes.Where(reportes => new[] { idFuncionalidadA,idFalloA,idERPA }.Contains(reportes.idTipoReporte.GetValueOrDefault()));
        //    //lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFuncionalidadA || reportes.idTipoReporte == idFalloA || reportes.idTipoReporte == idERPA);
        //}
        //if (idERPA != 0)
        //{
        //    lstReportes = lstReportes.Where(reportes => new[] { idERPA,idFuncionalidadA,idFalloA }.Contains(reportes.idTipoReporte.GetValueOrDefault()));
        //    //lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idERPA || reportes.idTipoReporte == idFuncionalidadA || reportes.idTipoReporte == idFalloA);
        //}
        /////////////////////////////

        if (idSinAsignarS != 0 && idAsignadoA == 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idSinAsignarS);
        }
        if (folioS != "")
        {
            lstReportes = lstReportes.Where(reportes => reportes.folio == folioS);
        }
        if (fechaIS != "0001-01-01" && fechaFS == "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte == Convert.ToDateTime(fechaIS));
        }
        if (fechaFS != "0001/01/01" && fechaIS == "0001/01/01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaPropuesta == Convert.ToDateTime(fechaFS));
        }
        if (fechaIS != "0001-01-01" && fechaFS != "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte >= Convert.ToDateTime(fechaIS) && reportes.fechaPropuesta <= Convert.ToDateTime(fechaFS));
        }
        //if (idFalloS != 0)
        //{
        //    lstReportes = lstReportes.Where(reportes => new[] { idFalloS,idERPS,idFuncionalidadS }.Contains(reportes.idTipoReporte.GetValueOrDefault()));
        //    //lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFalloS || reportes.idTipoReporte == idFuncionalidadS || reportes.idTipoReporte == idERPS);
        //}
        //if (idFuncionalidadS != 0)
        //{
        //    lstReportes = lstReportes.Where(reportes => new[] { idFuncionalidadS,idFalloS,idERPS }.Contains(reportes.idTipoReporte.GetValueOrDefault()));
        //    //lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFuncionalidadS || reportes.idTipoReporte == idFalloS || reportes.idTipoReporte == idERPS);
        //}
        //if (idERPS != 0)
        //{
        //    lstReportes = lstReportes.Where(reportes => new[] { idERPS,idFuncionalidadS,idFalloS }.Contains(reportes.idTipoReporte.GetValueOrDefault()));
        //    //lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idERPS || reportes.idTipoReporte == idFuncionalidadS || reportes.idTipoReporte == idFalloS);
        //}
        if (idERPS != 0 || idFalloS != 0 || idFuncionalidadS != 0 || idERPA != 0 || idFuncionalidadA != 0 || idFalloA != 0)
        {
            lstReportes = lstReportes.Where(reportes => new[] { idERPS, idFuncionalidadS, idFalloS, idERPA, idFuncionalidadA, idFalloA }.Contains(reportes.idTipoReporte.GetValueOrDefault()));
            //lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idERPS || reportes.idTipoReporte == idFuncionalidadS || reportes.idTipoReporte == idFalloS);
        }
        if (lstReportes.ToList().Count > 0)//SI existen registros
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Users\Consultor 050\Downloads\Reporte General.pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("PDF NAD Global");
            doc.AddCreator("Departamento T.I");

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph("NAD Global"));
            doc.Add(Chunk.NEWLINE);

            // Creamos una tabla que contendrá N Columnas
            PdfPTable pdfReporstesAsignados = new PdfPTable(8);
            pdfReporstesAsignados.WidthPercentage = 100;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clFolio = new PdfPCell(new Phrase("Folio", _standardFont));
            clFolio.BorderWidth = 0;
            clFolio.BorderWidthBottom = 0.75f;

            PdfPCell clUsuario = new PdfPCell(new Phrase("Usuario", _standardFont));
            clUsuario.BorderWidth = 0;
            clUsuario.BorderWidthBottom = 0.75f;

            PdfPCell clTipoReporte = new PdfPCell(new Phrase("Tipo Reporte", _standardFont));
            clTipoReporte.BorderWidth = 0;
            clTipoReporte.BorderWidthBottom = 0.75f;

            PdfPCell clAsunto = new PdfPCell(new Phrase("Asunto", _standardFont));
            clAsunto.BorderWidth = 0;
            clAsunto.BorderWidthBottom = 0.75f;

            PdfPCell clEmpresa = new PdfPCell(new Phrase("Empresa", _standardFont));
            clEmpresa.BorderWidth = 0;
            clEmpresa.BorderWidthBottom = 0.75f;

            PdfPCell clSistema = new PdfPCell(new Phrase("Sistema", _standardFont));
            clSistema.BorderWidth = 0;
            clSistema.BorderWidthBottom = 0.75f;

            PdfPCell clPrioridad = new PdfPCell(new Phrase("Prioridad", _standardFont));
            clPrioridad.BorderWidth = 0;
            clPrioridad.BorderWidthBottom = 0.75f;

            PdfPCell clFecha = new PdfPCell(new Phrase("Fecha", _standardFont));
            clFecha.BorderWidth = 0;
            clFecha.BorderWidthBottom = 0.75f;

            // Añadimos las celdas a la tabla
            pdfReporstesAsignados.AddCell(clFolio);
            pdfReporstesAsignados.AddCell(clUsuario);
            pdfReporstesAsignados.AddCell(clTipoReporte);
            pdfReporstesAsignados.AddCell(clAsunto);
            pdfReporstesAsignados.AddCell(clEmpresa);
            pdfReporstesAsignados.AddCell(clSistema);
            pdfReporstesAsignados.AddCell(clPrioridad);
            pdfReporstesAsignados.AddCell(clFecha);

            ControllerUsuarios cUsuarios = new ControllerUsuarios();
            foreach (tReporte t in lstReportes)
            {
                // Llenamos la tabla con información
                clFolio = new PdfPCell(new Phrase(t.folio, _standardFont));
                clFolio.BorderWidth = 0;

                clUsuario = new PdfPCell(new Phrase(cUsuarios.getUsuarioById(t.idUsuario.GetValueOrDefault()), _standardFont));
                clUsuario.BorderWidth = 0;

                clTipoReporte = new PdfPCell(new Phrase(t.cTipoReporte.nombreTipoReporte, _standardFont));
                clTipoReporte.BorderWidth = 0;

                clAsunto = new PdfPCell(new Phrase(t.asunto, _standardFont));
                clAsunto.BorderWidth = 0;

                clEmpresa = new PdfPCell(new Phrase(t.tERPGrupo.nomGrupo, _standardFont));
                clEmpresa.BorderWidth = 0;

                clSistema = new PdfPCell(new Phrase(t.cSistemas.nomSistema, _standardFont));
                clSistema.BorderWidth = 0;

                clPrioridad = new PdfPCell(new Phrase(t.cPrioridadReporte.nombrePrioridad, _standardFont));
                clPrioridad.BorderWidth = 0;

                clFecha = new PdfPCell(new Phrase(String.Format("{0:MM/dd/yyyy}", t.fechaReporte), _standardFont));
                clFecha.BorderWidth = 0;

                // Añadimos las celdas a la tabla
                pdfReporstesAsignados.AddCell(clFolio);
                pdfReporstesAsignados.AddCell(clUsuario);
                pdfReporstesAsignados.AddCell(clTipoReporte);
                pdfReporstesAsignados.AddCell(clAsunto);
                pdfReporstesAsignados.AddCell(clEmpresa);
                pdfReporstesAsignados.AddCell(clSistema);
                pdfReporstesAsignados.AddCell(clPrioridad);
                pdfReporstesAsignados.AddCell(clFecha);
            }

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(pdfReporstesAsignados);

            doc.Close();
            writer.Close();
            return "Reporte generado correctamente. °";
        }
        else
        {
            return "No existen coincidencias.";
        }
    }//Fin Si Exiten registros

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idAsignado"></param>
    /// <param name="idSinAsignar"></param>
    /// <param name="idPorValidar"></param>
    /// <param name="idTerminado"></param>
    /// <param name="folio"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <param name="idFallo"></param>
    /// <param name="idFuncionalidad"></param>
    /// <param name="idERP"></param>
    /// <returns></returns>
    public List<string> generarGraficas(int idAsignado, int idSinAsignar, int idPorValidar, int idTerminado, string folio, DateTime fechaInicio, DateTime fechaFin, int idFallo, int idFuncionalidad, int idERP, int idERPGrupo)
    {
        string fechaI = String.Format("{0:yyyy-MM-dd}", fechaInicio);
        string fechaF = String.Format("{0:yyyy-MM-dd}", fechaFin);

        List<tReporte> lstObj = new List<tReporte>();
        var lstReportes = from reporteC in erp.tReporte
                          join cEstatus in erp.cEstatusReporte on reporteC.idEstatusReporte equals cEstatus.idEstatusReporte
                          select reporteC;
        lstReportes = lstReportes.Where(reportes => (reportes.idEstatusReporte != 6 && reportes.idTipoReporte <= 4));
        if (idERPGrupo != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idERPGrupo == idERPGrupo);
        }
        if (idAsignado != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idTerminado);
        }
        if (idSinAsignar != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idTerminado);
        }
        if (idPorValidar != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idTerminado);
        }
        if (idTerminado != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idTerminado || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idPorValidar);
        }
        if (folio != "")
        {
            lstReportes = lstReportes.Where(reportes => reportes.folio == folio);
        }
        if (fechaI != "0001-01-01" && fechaF == "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte == Convert.ToDateTime(fechaI));
        }
        if (fechaF != "0001-01-01" && fechaI == "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte == Convert.ToDateTime(fechaF));
        }
        if (fechaI != "0001-01-01" && fechaF != "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte >= Convert.ToDateTime(fechaI) && reportes.fechaReporte <= Convert.ToDateTime(fechaF));
        }
        if (idFallo != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idERP);
        }
        if (idFuncionalidad != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idERP);
        }
        if (idERP != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idERP || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo);
        }
        var lstReportesGroup = from reporteC in lstReportes
                               join cEstatus in erp.cEstatusReporte on reporteC.idEstatusReporte equals cEstatus.idEstatusReporte
                               group reporteC by new { reporteC.idEstatusReporte } into grouping
                               select new
                               {
                                   idEstatusReporte = grouping.Key.idEstatusReporte,
                                   cantidad = grouping.Count(),
                                   idTipoReporte = reporte.idEstatusReporte
                               };
        //var lstSubGroup= from reporteC in lstReportesGroup
        //                 group reporteC by new { reporteC.idTipoReporte } into subgrouping
        //                 select new
        //                 {
        //                     idTipoReporte = subgrouping.Key.idTipoReporte,
        //                     cantidad = subgrouping.Count(),
        //                 };
        List<string> lst = new List<string>();
        foreach (var t in lstReportesGroup)
        {
            lst.Add(Convert.ToString(t.idEstatusReporte));
            lst.Add(Convert.ToString(t.cantidad));
        }
        //foreach (var t in lstSubGroup)
        //{
        //    lst.Add(Convert.ToString(t.idTipoReporte));
        //    lst.Add(Convert.ToString(t.cantidad));
        //}

        return lst.ToList();

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="idAsignado"></param>
    /// <param name="idSinAsignar"></param>
    /// <param name="idPorValidar"></param>
    /// <param name="idTerminado"></param>
    /// <param name="folio"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <param name="idFallo"></param>
    /// <param name="idFuncionalidad"></param>
    /// <param name="idERP"></param>
    /// <returns></returns>
    public List<tReporte> generarGraficasDinamicas(int idAsignado, int idSinAsignar, int idPorValidar, int idTerminado, string folio, DateTime fechaInicio, DateTime fechaFin, int idFallo, int idFuncionalidad, int idERP)
    {
        string fechaI = String.Format("{0:yyyy-MM-dd}", fechaInicio);
        string fechaF = String.Format("{0:yyyy-MM-dd}", fechaFin);
        //var lstReportes = from reportes in erp.tReporte
        //                  select reportes;

        List<tReporte> lstObj = new List<tReporte>();

        var lstReportes = from reporte in erp.tReporte
                          join empleado in erp.cEstatusReporte on estatusReporte.idEstatusReporte equals reporte.idEstatusReporte
                          select reporte;

        if (idAsignado != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idTerminado);
        }
        if (idSinAsignar != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idTerminado);
        }
        if (idPorValidar != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idPorValidar || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idTerminado);
        }
        if (idTerminado != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idEstatusReporte == idTerminado || reportes.idEstatusReporte == idAsignado || reportes.idEstatusReporte == idSinAsignar || reportes.idEstatusReporte == idPorValidar);
        }

        if (fechaI != "0001-01-01" && fechaF == "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte == Convert.ToDateTime(fechaI));
        }
        if (fechaF != "0001/01/01" && fechaI == "0001/01/01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaPropuesta == Convert.ToDateTime(fechaF));
        }
        if (fechaI != "0001-01-01" && fechaF != "0001-01-01")
        {
            lstReportes = lstReportes.Where(reportes => reportes.fechaReporte >= Convert.ToDateTime(fechaI) && reportes.fechaPropuesta <= Convert.ToDateTime(fechaF));
        }
        if (idFallo != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idERP);
        }
        if (idFuncionalidad != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo || reportes.idTipoReporte == idERP);
        }
        if (idERP != 0)
        {
            lstReportes = lstReportes.Where(reportes => reportes.idTipoReporte == idERP || reportes.idTipoReporte == idFuncionalidad || reportes.idTipoReporte == idFallo);
        }


        return lstReportes.ToList(); ;

    }

    /// <summary>
    /// Conocer que el flijo que ha llevado cada reporte
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns></returns>
    public string detalleAccionesReporte(int idReporte)
    {
        string result = "";
        //var query = (from reporte in erp.tReporte
        //             join respuestaR in erp.tRespuestaReportes on reporte.idReporte equals respuestaR.idReporte
        //             join estatusR in erp.cEstatusReporte on respuestaReporte.idEstadoReporte equals estatusR.idEstatusReporte
        //             join emp in erpRH.tEmpleado on respuestaReporte.idUsuario equals emp.idEmpleado
        //             where respuestaR.idReporte == idReporte
        //             select new
        //             {
        //                 nombre = emp.nombre + " " + emp.apellidoPaterno + " " + emp.apellidoMaterno,
        //                 comentario = respuestaR.comentario,
        ////                 estatus = estatusR.nombreEstadoReporte
        //             }).ToList();
        var query = (from reporte in erp.view_HistorialReporte
                     where reporte.idReporte == idReporte
                     select reporte).ToList();
        if (query.Count > 0)
        {
            ControllerUsuarios cUsuarios = new ControllerUsuarios();
            result +=
                      "<table id='tblDetalleAccionesR' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Persona</th>" +
                            "<th>Comentario</th>" +
                            "<th>Estatus Reporte</th>" +
                            "<th>Fecha</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            foreach (var t in query)
            {
                result += "<tr>";
                result += "<td>" + t.nombre + "</td>";
                if (t.comentario.Equals(""))
                {
                    result += "<td>N/A</td>";
                }
                else
                {
                    result += "<td>" + t.comentario + "</td>";
                }

                result += "<td>" + t.estatus + "</td>";
                result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaRespuesta) + "</span>" + String.Format("{0:MM/dd/yyyy}", t.fechaRespuesta) + "</td>";
                result += "</tr>";
            }
            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='4'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>";
        }
        else
        {
            result += "<div id='divTblApoyo123'><div align='center' style='font-size: 10px !important;margin-left: 25%;' class='bg-alert width50'><div style='width:10%' class='left'><span id='icon-25' class='blanco warning'></span></div><div style='width:90%; text-align: justify; padding-top:10px; padding-bottom:10px'>No se encontraron resultados.</div></div></div>";
        }
        return result;
    }

    /// <summary>
    /// Guardar justificación al borrar un reporte
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="justifi"></param>
    /// <returns></returns>
    public bool guardarJustificacion(int idReporte, string justifi)
    {
        try
        {
            justificacion = new tJustificacion();
            justificacion.idReporte = idReporte;
            justificacion.justificacion = justifi;
            erp.tJustificacion.InsertOnSubmit(justificacion);
            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public void EnviarCorreo(string destinatarios, string emailorigen, string emailCopiaOculta)
    {
        /*-------------------------MENSAJE DE CORREO----------------------*/
        //Creamos un nuevo Objeto de mensaje
        System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

        //Direccion de correo electronico a la que queremos enviar el mensaje
        destinatarios = "ruben.balderas.fernandez@gmail.com";
        mmsg.To.Add(destinatarios);

        //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

        //Asunto
        mmsg.Subject = "Hay nuevas incidencias por atender";
        mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

        //Direccion de correo electronico que queremos que reciba una copia del mensaje
        mmsg.Bcc.Add("ruben3chivas5@gmail.com"); //Opcional

        //Cuerpo del Mensaje
        mmsg.Body = "Hay una nueva tarea por realalizar";
        mmsg.BodyEncoding = System.Text.Encoding.UTF8;
        mmsg.IsBodyHtml = false; //Si no queremos que se envíe como HTML

        //Correo electronico desde la que enviamos el mensaje
        mmsg.From = new System.Net.Mail.MailAddress("ruben3chivas5@gmail.coms");


        /*-------------------------CLIENTE DE CORREO----------------------*/
        //Creamos un objeto de cliente de correo
        System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

        //Hay que crear las credenciales del correo emisor
        cliente.Credentials =
            new System.Net.NetworkCredential("ruben3chivas5@gmail.com", "skaskapunk");

        //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
        cliente.Port = 587;
        cliente.EnableSsl = true;
        cliente.Host = "smtp.gmail.com"; //Para Gmail "smtp.gmail.com";
        /*-------------------------ENVIO DE CORREO----------------------*/
        try
        {
            //Enviamos el mensaje      
            cliente.Send(mmsg);
        }
        catch (System.Net.Mail.SmtpException ex)
        {
            //Aquí gestionamos los errores al intentar enviar el correo
        }
    }
}
