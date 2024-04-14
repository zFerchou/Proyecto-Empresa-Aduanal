using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Collections;
using System.Data;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Activities.Statements;
/// <summary>
/// Descripción breve de ControllerReportes
/// </summary>
public class ControllerReportes
{
    private tReporte reporte;
    private ERPManagementDataContext erp;
    private cTipoReporte cTipoR;
    private tLogReporte tLogReporte;
    private tERPGrupo tERPGrupo;
    private tERPGrupoSistema tERPGrupoSis;
    private Utileria utileria;
    private tRolUsuarioERPM tRolUsuario;
    private ERPManagementRHDataContext erpRH;
    private view_correoPersonalExternos vCorreosExternos;
    /// <summary>
    /// Constructor, inicializa el data context.
    /// </summary>
    public ControllerReportes()
    {
        erp = new ERPManagementDataContext();
        cTipoR = new cTipoReporte();
        reporte = new tReporte();
        tLogReporte = new tLogReporte();
        tERPGrupo = new tERPGrupo();
        tERPGrupoSis = new tERPGrupoSistema();
        utileria = new Utileria();
        tRolUsuario = new tRolUsuarioERPM();
        erpRH = new ERPManagementRHDataContext();
        vCorreosExternos = new view_correoPersonalExternos();
    }

    /// <summary>
    /// Método para agregar reportes (*)
    /// Así como realizar un insert en la tabla tLogReporte, en el que se indica que el reporte generó
    /// </summary>
    /// <returns>True si el insert se llevó a cabo de manera exitosa y el insert en la tabla tLogReporte, de lo contrario False</returns>
    public bool agregaReporte(string asunto, string descripcion, int idTipoReporte, int idPrioridad, int idSistema, string fechaPropuesta, int idUsuario, int idERPGrupo, string nombreArchivo, int idArea, string ticketVinculado, int sprint, int puntos)
    {
        try
        {
            //Insert en tReporte
            if (nombreArchivo != null || nombreArchivo != "")
            {
                reporte.evidencia = nombreArchivo;
            }
            //if (idArea != 0)
            //{
            //    var idERPGRupoVal = (from erpg in erp.tERPGrupo
            //                         where erpg.idGrupoRH == idERPGrupo
            //                         select erpg.idERPGrupo).FirstOrDefault();
            //    reporte.idERPGrupo = idERPGRupoVal;
            //}
            //else
            //{
            //    reporte.idERPGrupo = idERPGrupo;
            //}
            reporte.idERPGrupo = idERPGrupo;
            //string fechaReporte = DateTime.Now.ToShortDateString();
            DateTime fechaRep = DateTime.Now;
            DateTime fechaProp = Convert.ToDateTime(fechaPropuesta);
            reporte.asunto = asunto;
            reporte.descripcion = descripcion;
            reporte.idTipoReporte = idTipoReporte;
            reporte.idPrioridad = idPrioridad;
            reporte.idSistema = idSistema;
            reporte.idEstatusReporte = 1;
            reporte.fechaReporte = fechaRep;
            reporte.idUsuario = idUsuario;
            reporte.fechaPropuesta = fechaProp;
            reporte.idArea = idArea;

            var idTicketVinculado = (from r in erp.tReporte where r.folio.Equals(ticketVinculado) select r.idReporte).FirstOrDefault();

            reporte.idTicketVinculado = idTicketVinculado;

            var incremento = (from trep in erp.tReporte
                              where trep.idTipoReporte == idTipoReporte
                              select trep.idReporte).Count();

            var claveTipo = (from ctr in erp.cTipoReportes
                             where ctr.idTipoReporte == idTipoReporte
                             select ctr).SingleOrDefault();
            string nuevoFolio = claveTipo.clave + "-" + fechaRep.Year + "-" + (incremento + 1);

            reporte.folio = nuevoFolio.Trim().Replace(" ", "");

            if (idTipoReporte == 25)
            {
                reporte.iSprint = sprint;
                reporte.StoryPoints = puntos;
            }

            reporte.bHistorico = 1;

            erp.tReporte.InsertOnSubmit(reporte);
            erp.SubmitChanges();

            //Insert en tLogReporte
            var idReporte = (from trep in erp.tReporte
                             where trep.idTipoReporte == idTipoReporte
                             select trep.idReporte).Max();

            string fechaAccion = DateTime.Now.ToShortDateString();
            DateTime fechaAcc = Convert.ToDateTime(fechaAccion);
            tLogReporte.idReporte = idReporte;
            tLogReporte.idUsuario = idUsuario;
            tLogReporte.fecha = fechaAcc;
            tLogReporte.idAccion = 1;

            erp.tLogReporte.InsertOnSubmit(tLogReporte);
            erp.SubmitChanges();

            if (idTipoReporte == 25)
            {
                storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
                string sQuery = "";

                sQuery = "select idSprint from tSprint where iNumeroSprint = " + sprint + " and sAnio = YEAR(GETDATE())";

                string idSprint = sp.recuperaValor(sQuery);

                sQuery = "INSERT INTO tTicketSprint(idReporte,idSprint,iEstatus,dFecha) VALUES(" + idReporte + "," + idSprint + ",1,GETDATE())";
                sp.ejecutaSQL(sQuery);
            }

            enviarCorreo(idUsuario, idReporte, idTipoReporte);

            var ruta = HttpContext.Current.Server.MapPath("");

            //var revertirRuta = Reverse(ruta);
            //var extraerRuta = revertirRuta.Substring(revertirRuta.IndexOf('\\'), revertirRuta.Length - revertirRuta.IndexOf('\\'));
            //var convertirRuta = Reverse(extraerRuta);

            //var rutaCarpeta = convertirRuta  + "\\Contenidos\\"  ;



            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }





    public bool agregaHistoria(string epica, string descripcion, string historia, string criterios, int idPrioridad, int idSistema, string idERPGrupo, string riesgos, string evidencia, string[] tipoImpacto, int idUsuario)
    {
        try
        {
            storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
            string sQuery = "";

            if (evidencia == "NULL")
            {
                sQuery = "INSERT INTO tProductBackLog(sCliente,idSistema,sEpica,sHistoria,idPrioridad,sDescripcion,sCriteriosAceptacion,sRiesgos,idEstatus,idUsuario) " +
               " VALUES('" + idERPGrupo + "'," + idSistema + ",'" + epica + "','" + historia + "'," + idPrioridad + ",'" + descripcion + "','" + criterios + "','" + riesgos + "',1," + idUsuario + ")"
               + " SELECT SCOPE_IDENTITY();";
            }
            else
            {
                sQuery = "INSERT INTO tProductBackLog(sCliente,idSistema,sEpica,sHistoria,idPrioridad,sDescripcion,sCriteriosAceptacion,sRiesgos,idEstatus,idUsuario,sEvidencia) " +
                " VALUES('" + idERPGrupo + "'," + idSistema + ",'" + epica + "','" + historia + "'," + idPrioridad + ",'" + descripcion + "','" + criterios + "','" + riesgos + "',1," + idUsuario + ",'" + evidencia + "')"
                + " SELECT SCOPE_IDENTITY();";
            }

            string idInsertado = sp.recuperaValor(sQuery);
            if (idInsertado != "" || idInsertado != "-1")
            {
                sQuery = "INSERT INTO ProductBackLog_TipoImpacto(idProductBackLog,idTipoImpacto) VALUES";
                int totalElementos = tipoImpacto.Length;

                for (int i = 0; i < totalElementos; i++)
                {
                    sQuery += "(" + idInsertado + ',' + tipoImpacto[i] + ")";

                    if (i < totalElementos - 1)
                    {
                        // Si no es el último elemento, agrega una coma
                        sQuery += ",";
                    }
                }
            }

            return sp.ejecutaSQL(sQuery);


        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public bool actualizarHistoria(string epica, string descripcion, string historia, string criterios, int idPrioridad, int idSistema, string idERPGrupo, string riesgos, int idUsuario, int idHistoria, int estatus, string evidencia, string[] tipoImpacto, int storyPoint)
    {
        try
        {
            storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
            string sQuery = "";
            if (evidencia == "NULL")
            {
                sQuery = "UPDATE tProductBackLog SET sCliente = '" + idERPGrupo + "' , idSistema = " + idSistema + " , sEpica = '" + epica + "', sHistoria = '" + historia + "', idPrioridad = " + idPrioridad + " " +
                                " ,sDescripcion = '" + descripcion + "', sCriteriosAceptacion = '" + criterios + "', sRiesgos = '" + riesgos + "', idEstatus = " + estatus + ",storyProints="+storyPoint+" WHERE idProductBackLog = " + idHistoria;
            }
            else
            {
                sQuery = "UPDATE tProductBackLog SET sCliente = '" + idERPGrupo + "' , idSistema = " + idSistema + " , sEpica = '" + epica + "', sHistoria = '" + historia + "', idPrioridad = " + idPrioridad + " " +
                                " ,sDescripcion = '" + descripcion + "', sCriteriosAceptacion = '" + criterios + "', sRiesgos = '" + riesgos + "', idEstatus = " + estatus + ", sEvidencia = '" + evidencia + "',storyProints="+storyPoint+" WHERE idProductBackLog = " + idHistoria;
            }

            if (sp.ejecutaSQL(sQuery))
            {
                // Elimina registros que ya no están presentes en la lista tipoImpacto
                string deleteQuery = "DELETE FROM ProductBackLog_TipoImpacto WHERE idProductBackLog = " + idHistoria +
                                     " AND idTipoImpacto NOT IN (" + string.Join(",", tipoImpacto) + ")";
                sp.ejecutaSQL(deleteQuery);

                // Obtén los tipos de impacto actualmente asociados al ProductBackLog desde la base de datos
                string queryTiposActuales = "SELECT idTipoImpacto FROM ProductBackLog_TipoImpacto WHERE idProductBackLog = " + idHistoria;
                DataSet dsTiposImpactoActuales = sp.getValues(queryTiposActuales);

                // Extrae los valores de la columna idTipoImpacto en una lista
                List<string> tiposImpactoActuales = new List<string>();
                foreach (DataRow row in dsTiposImpactoActuales.Tables[0].Rows)
                {
                    tiposImpactoActuales.Add(row["idTipoImpacto"].ToString());
                }

                // Filtra solo los tipos de impacto que no están presentes actualmente
                var tiposImpactoNoExistentes = tipoImpacto.Except(tiposImpactoActuales).ToArray();

                // Inserta nuevos registros que no existen aún en la lista tipoImpacto
                sQuery = "INSERT INTO ProductBackLog_TipoImpacto(idProductBackLog, idTipoImpacto) VALUES";
                int totalElementos = tipoImpacto.Length;

                for (int i = 0; i < tiposImpactoNoExistentes.Length; i++)
                {
                    sQuery += "(" + idHistoria + ',' + tiposImpactoNoExistentes[i] + ")";

                    if (i < tiposImpactoNoExistentes.Length - 1)
                    {
                        // Si no es el último elemento, agrega una coma
                        sQuery += ",";
                    }
                }
            }
            else
            {
                sQuery = "";
            }

            return sp.ejecutaSQL(sQuery);


        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }




//Metodo para traer los reportes
public tReporte getReporteGeneradoById(int idReporte)
    {

        var objReporte = (from r in erp.tReporte
                          where r.idReporte == idReporte
                          select r).FirstOrDefault();

        return objReporte;
    }



    /// <summary>
    /// Método para generar la tabla de reportes
    /// Sin Asignar.
    /// </summary>
    /// <returns>Cadena con estructura de la tabla</returns>
    public string getTblReportesSinAsignar(int idUsuario)
        {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string query = "";

        query = "select idTipoReporte from tUsuarioTipoIncidencia where idUsuario =" + idUsuario;
        List<string> lstUsuarioTipo = sp.recuperaRegistros(query);

        string[] tiposAdmitidos = { "1", "2", "3", "4", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24" };

        if(lstUsuarioTipo.Count == 0)
        {
            lstUsuarioTipo = tiposAdmitidos.ToList();
        }

        var lstReportes = (from r in erp.tReporte
                          join u in erp.vUsuariosERPM on r.idUsuario.ToString() equals u.idEmpleado
                          where r.idEstatusReporte == 1 && r.bHistorico == 1 && (lstUsuarioTipo.Contains(r.idTipoReporte.ToString()))
                          orderby r.fechaReporte descending
                          select new {
                            IdReporte = r.idReporte,
                            Folio = r.folio,
                            FechaReporte = String.Format("{0:yyyy/MM/dd HH:mm}", r.fechaReporte),
                            Usuario = u.nombreCompleto,
                            TipoReporte = r.cTipoReporte.nombreTipoReporte,
                            Prioridad = r.cPrioridadReporte.nombrePrioridad,
                            Grupo = r.tERPGrupo.nomGrupo.ToUpper(),
                            asunto = r.asunto,
                            IdSistema = r.idSistema,
                            nomSistema = r.cSistemas.nomenglatura.ToUpper(),
                          }).ToList();

        if (lstReportes.Count == 0)
        { return string.Empty; }

        StringBuilder result = new StringBuilder();

        result.Append("<fieldset>");
        result.Append("<legend>TICKETS SIN ASIGNAR</legend>");
        result.Append("<table id='tblReporteSinAsignar' class='data_grid display'>");
        result.Append("<thead id='grid-head2'>");
        result.Append("<th>Folio</th>")
            .Append("<th>Tipo Reporte</th>")
            .Append("<th>Grupo</th>")
            .Append("<th>Sistema/Área</th>")
            .Append("<th>Fecha Reporte</th>")
            .Append("<th>Asunto</th>")
            .Append("<th>Prioridad</th>")
            .Append("<th>Usuario</th>")
            .Append("<th>Ver</th>");
        result.Append("</thead>");
        result.Append("<tbody id='grid-body'>");

        foreach (var t in lstReportes)
        {
            result.Append("<tr>");
            result.AppendFormat("<td><span class='hide'>{0}</span>{1}</td>", t.FechaReporte, t.Folio);
            result.AppendFormat("<td>{0}</td>", t.TipoReporte);
            result.AppendFormat("<td>{0}</td>", t.Grupo);

            if (t.IdSistema != null)
            {
                result.AppendFormat("<td>{0}</td>", t.nomSistema);
            }
            //else if (t.idSistema.Equals(0) && t.idArea != null)
            //{
            //    result += "<td>" + validacionUsu.area + "</td>";
            //}
            else
            {
                result.Append("<td>No Aplica</td>");
            }
                        
            result.AppendFormat("<td>{0}</td>", t.FechaReporte);
            result.AppendFormat("<td>{0}</td>", t.asunto);
            result.AppendFormat("<td>{0}</td>", t.Prioridad);
            var partesNombre = t.Usuario.Trim().Split(' ');
            var partesSinUltimo = partesNombre.Take(partesNombre.Length - 1).ToArray();
            result.AppendFormat("<td>{0}</td>", string.Join(" ",partesSinUltimo));
            result.AppendFormat("<td><a title='Consultar Incidencia' onclick='javascript:getDetalleReporte({0},13);'><span id='icon-25' class='consultar verde'></span></a></td>", t.IdReporte);
            result.Append("</tr>");
        }

        result.Append("</tbody>");
        result.Append("<tfoot>");
        result.Append("<tr>");
        result.Append("<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>");
        result.Append("</tr>");
        result.Append("</tfoot>");
        result.Append("</table>");
        result.Append("</fieldset>");

        return result.ToString();
    }

    public string getTblReportesSinAsignarHistorico(int idUsuario)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string query = "";

        query = "select idTipoReporte from tUsuarioTipoIncidencia where idUsuario =" + idUsuario;
        List<string> lstUsuarioTipo = sp.recuperaRegistros(query);

        string[] tiposAdmitidos = { "1", "2", "3", "4", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24" };

        if (lstUsuarioTipo.Count == 0)
        {
            lstUsuarioTipo = tiposAdmitidos.ToList();
        }

        var lstReportes = (from r in erp.tReporte
                           join u in erp.vUsuariosERPM on r.idUsuario.ToString() equals u.idEmpleado
                           where r.idEstatusReporte == 1 && r.bHistorico == 0 && (lstUsuarioTipo.Contains(r.idTipoReporte.ToString()))
                           orderby r.fechaReporte descending
                           select new
                           {
                               IdReporte = r.idReporte,
                               Folio = r.folio,
                               FechaReporte = String.Format("{0:yyyy/MM/dd HH:mm}", r.fechaReporte),
                               Usuario = u.nombreCompleto,
                               TipoReporte = r.cTipoReporte.nombreTipoReporte,
                               Prioridad = r.cPrioridadReporte.nombrePrioridad,
                               Grupo = r.tERPGrupo.nomGrupo.ToUpper(),
                               asunto = r.asunto,
                               IdSistema = r.idSistema,
                               nomSistema = r.cSistemas.nomenglatura.ToUpper(),
                           }).ToList();

        if (lstReportes.Count == 0)
        { return string.Empty; }

        StringBuilder result = new StringBuilder();

        result.Append("<fieldset>");
        result.Append("<legend>TICKETS SIN ASIGNAR</legend>");
        result.Append("<table id='tblReporteSinAsignar' class='data_grid display'>");
        result.Append("<thead id='grid-head2'>");
        result.Append("<th>Folio</th>")
            .Append("<th>Tipo Reporte</th>")
            .Append("<th>Grupo</th>")
            .Append("<th>Sistema/Área</th>")
            .Append("<th>Fecha Reporte</th>")
            .Append("<th>Asunto</th>")
            .Append("<th>Prioridad</th>")
            .Append("<th>Usuario</th>")
            .Append("<th>Ver</th>");
        result.Append("</thead>");
        result.Append("<tbody id='grid-body'>");

        foreach (var t in lstReportes)
        {
            result.Append("<tr>");
            result.AppendFormat("<td><span class='hide'>{0}</span>{1}</td>", t.FechaReporte, t.Folio);
            result.AppendFormat("<td>{0}</td>", t.TipoReporte);
            result.AppendFormat("<td>{0}</td>", t.Grupo);

            if (t.IdSistema != null)
            {
                result.AppendFormat("<td>{0}</td>", t.nomSistema);
            }
            //else if (t.idSistema.Equals(0) && t.idArea != null)
            //{
            //    result += "<td>" + validacionUsu.area + "</td>";
            //}
            else
            {
                result.Append("<td>No Aplica</td>");
            }

            result.AppendFormat("<td>{0}</td>", t.FechaReporte);
            result.AppendFormat("<td>{0}</td>", t.asunto);
            result.AppendFormat("<td>{0}</td>", t.Prioridad);
            var partesNombre = t.Usuario.Trim().Split(' ');
            var partesSinUltimo = partesNombre.Take(partesNombre.Length - 1).ToArray();
            result.AppendFormat("<td>{0}</td>", string.Join(" ", partesSinUltimo));
            result.AppendFormat("<td><a title='Consultar Incidencia' onclick='javascript:getDetalleReporte({0},13);'><span id='icon-25' class='consultar verde'></span></a></td>", t.IdReporte);
            result.Append("</tr>");
        }

        result.Append("</tbody>");
        result.Append("<tfoot>");
        result.Append("<tr>");
        result.Append("<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>");
        result.Append("</tr>");
        result.Append("</tfoot>");
        result.Append("</table>");
        result.Append("</fieldset>");

        return result.ToString();
    }

    public string getTblReportesSinAsignarSprint(int idUsuario)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string query = "";

        string sPrint = sp.recuperaValor("select iNumeroSprint from tSprint where dFechaFin >= convert(varchar(10),GETDATE(),121) AND dFechaInicio <= convert(varchar(10),GETDATE(),121) and sAnio = YEAR(GETDATE())");

        string sDias = sp.recuperaValor("DECLARE @FechaInicio DATE = GETDATE(),  @FechaFin DATE;" + "" +
                                        "SELECT @FechaFin = dFechaFin FROM tSprint WHERE iNumeroSprint = " + sPrint + "" +
                                        "SELECT DATEDIFF(DAY, @FechaInicio, @FechaFin) -  (DATEDIFF(WEEK, @FechaInicio, @FechaFin) * 2) -  CASE    WHEN DATEPART(WEEKDAY, @FechaInicio) = 1 THEN 1        WHEN DATEPART(WEEKDAY, @FechaInicio) = 7 THEN 1         ELSE 0     END -     CASE        WHEN DATEPART(WEEKDAY, @FechaFin) = 1 THEN 1       WHEN DATEPART(WEEKDAY, @FechaFin) = 7 THEN 1        ELSE 0    END AS DiferenciaDiasExcluyendoFinDeSemana;");

        query = "select idTipoReporte from tUsuarioTipoIncidencia where idUsuario =" + idUsuario;
        List<string> lstUsuarioTipo = sp.recuperaRegistros(query);

        string[] tiposAdmitidos = { "1", "2", "3", "4", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24" };

        if (lstUsuarioTipo.Count == 0)
        {
            lstUsuarioTipo = tiposAdmitidos.ToList();
        }

        var lstReportes = (from r in erp.tReporte
                           join u in erp.vUsuariosERPM on r.idUsuario.ToString() equals u.idEmpleado
                           where r.idEstatusReporte == 1 && r.iSprint == int.Parse(sPrint) && r.bHistorico == 1 && (lstUsuarioTipo.Contains(r.idTipoReporte.ToString()))
                           orderby r.fechaReporte descending
                           select new
                           {
                               IdReporte = r.idReporte,
                               Folio = r.folio,
                               FechaReporte = String.Format("{0:yyyy/MM/dd HH:mm}", r.fechaReporte),
                               Usuario = u.nombreCompleto,
                               TipoReporte = r.cTipoReporte.nombreTipoReporte,
                               StoryPoints = r.StoryPoints==null?0:r.StoryPoints,
                               Grupo = r.tERPGrupo.nomGrupo.ToUpper(),
                               asunto = r.asunto,
                               IdSistema = r.idSistema,
                               nomSistema = r.cSistemas.nomenglatura.ToUpper(),
                           }).ToList();

        if (lstReportes.Count == 0)
        { return string.Empty; }

        StringBuilder result = new StringBuilder();

        result.Append("<fieldset>");
        result.Append("<legend>TICKETS SIN ASIGNAR</legend>");
        result.Append("<div class='bg-success width30  clear mb-3 mx-auto' style='margin-left:35%' <span class='blanco pendientes'>Dias para terminar el Sprint: " + sDias+"</span><br></div> <br>");
        result.Append("<table id='tblReporteSinAsignar' class='data_grid display'>");
        result.Append("<thead id='grid-head2'>");
        result.Append("<th>Folio</th>")
            .Append("<th>Tipo Reporte</th>")
            .Append("<th>Grupo</th>")
            .Append("<th>Sistema/Área</th>")
            .Append("<th>Fecha Reporte</th>")
            .Append("<th>Asunto</th>")
            .Append("<th>StoryPoints</th>")
            .Append("<th>Usuario</th>")
            .Append("<th>Ver</th>");
        result.Append("</thead>");
        result.Append("<tbody id='grid-body'>");

        foreach (var t in lstReportes)
        {
            int puntuacion = (t.StoryPoints >= 1 && t.StoryPoints <= 3) ? 1 :
            (t.StoryPoints >= 4 && t.StoryPoints <= 5) ? 4 :
            (t.StoryPoints >= 6 && t.StoryPoints <= 8) ? 5 :
            (t.StoryPoints >= 9 && t.StoryPoints <= 13) ? 6 : 0;

            // Comparar puntuación con otro valor y asignar color
            string color = (puntuacion <= int.Parse(sDias)) ? "verde" : "rojo";

            result.Append("<tr >");
            result.AppendFormat("<td><span class='hide'>{0}</span>{1}</td>", t.FechaReporte, t.Folio);
            result.AppendFormat("<td>{0}</td>", t.TipoReporte);
            result.AppendFormat("<td>{0}</td>", t.Grupo);

            if (t.IdSistema != null)
            {
                result.AppendFormat("<td>{0}</td>", t.nomSistema);
            }
            //else if (t.idSistema.Equals(0) && t.idArea != null)
            //{
            //    result += "<td>" + validacionUsu.area + "</td>";
            //}
            else
            {
                result.Append("<td>No Aplica</td>");
            }


            result.AppendFormat("<td>{0}</td>", t.FechaReporte);
            result.AppendFormat("<td>{0}</td>", t.asunto);
            result.AppendFormat("<td>{0}</td>", t.StoryPoints);
            var partesNombre = t.Usuario.Trim().Split(' ');
            var partesSinUltimo = partesNombre.Take(partesNombre.Length - 1).ToArray();
            result.AppendFormat("<td>{0}</td>", string.Join(" ", partesSinUltimo));
            result.AppendFormat("<td><a title='Consultar Incidencia' onclick='javascript:getDetalleReporte({0},13);'><span id='icon-25' class='consultar "+color+"'></span></a></td>", t.IdReporte);
            result.Append("</tr>");
        }

        result.Append("</tbody>");
        result.Append("<tfoot>");
        result.Append("<tr>");
        result.Append("<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>");
        result.Append("</tr>");
        result.Append("</tfoot>");
        result.Append("</table>");
        result.Append("</fieldset>");

        return result.ToString();
    }

    /// <summary>
    /// Método para generar la tabla de reportes
    /// asignados.
    /// </summary>
    /// <returns>Cadena con estructura de la tabla</returns>
    public string getTblReportesAsignados(int idUsuario)
    {
        StringBuilder result = new StringBuilder();

        int[] estatusAdmitidos = { 2, 4, 8, 9 };
        int[] tiposResponsables = { 1, 2 };

        var lstReportes = (from tr in erp.tReporte
                           join rs in erp.tResponsableReporte on tr.idReporte equals rs.idReporte
                           join u in erp.vUsuariosERPM on tr.idUsuario.ToString() equals u.idEmpleado
                           where rs.idResponsable == idUsuario
                           && estatusAdmitidos.Contains(tr.idEstatusReporte)
                           && tiposResponsables.Contains(rs.idTipoResponsable)
                           orderby tr.fechaReporte descending
                           select new
                           {
                               IdReporte = tr.idReporte,
                               Folio = tr.folio,
                               FechaReporte = String.Format("{0:yyyy/MM/dd HH:mm}", tr.fechaReporte),
                               IdUsuario = tr.idUsuario,
                               Usuario = u.nombreCompleto.Trim(),
                               TipoReporte = tr.cTipoReporte.nombreTipoReporte,
                               Prioridad = tr.cPrioridadReporte.nombrePrioridad,
                               Grupo = tr.tERPGrupo.nomGrupo.ToUpper(),
                               IdSistema = tr.idSistema,
                               IdEstatusReporte = tr.idEstatusReporte,
                               IdArea = tr.idArea,
                               Sistema = tr.cSistemas.nomenglatura.ToUpper(),
                               TipoResponsable = rs.idTipoResponsable,
                               Asunto = tr.asunto,
                               FechaPropuestaTermino = tr.fechaPropuestaTermino,
                           }).ToList();

        if (lstReportes.Count == 0)
        { return string.Empty; }

        result.Append("<fieldset>");
        result.Append("<legend>TICKETS ASIGNADOS</legend>");
        result.Append("<table id='tblReporteAsignados' class='data_grid display'>");
        result.Append("<thead id='grid-head2'>");
        result.Append("<th>Folio</th>")
            .Append("<th>Tipo Reporte</th>")
            .Append("<th>Grupo</th>")
            .Append("<th>Sistema/Área</th>")
            .Append("<th>Fecha Reporte</th>")
            .Append("<th>Asunto</th>")
            .Append("<th>Prioridad</th>")
            .Append("<th>Usuario</th>")
            .Append("<th>Ver</th>");
        result.Append("</thead>");
        result.Append("<tbody id='grid-body'>");

        var fechaActual = DateTime.Now;

        foreach (var t in lstReportes)
        {
            result.AppendFormat("<tr {0}>", t.TipoResponsable == 1 ? "" : "style='color:#7f8c8d;'");

            var cssColorEstatus = "#85c555";
            if (fechaActual.Date > t.FechaPropuestaTermino)
            {
                cssColorEstatus = "#c91820";
            }
            else if (fechaActual.Date == t.FechaPropuestaTermino)
            {
                cssColorEstatus = "#f1c40f";
            }            

            result.AppendFormat("<td style='border-left:4px solid {2};'><span class='hide'>{0}</span>{1}</td>", t.FechaReporte, t.Folio, t.IdEstatusReporte == 9 ? "#7f8c8d" : cssColorEstatus);            
            result.AppendFormat("<td>{0}</td>", t.TipoReporte);            
            result.AppendFormat("<td>{0}</td>", t.Grupo);

            if (t.IdSistema != 0 && t.IdArea == 0)
            {
                result.AppendFormat("<td>{0}</td>", t.Sistema);
            }
            else if (t.IdSistema == null || t.IdArea == null)
            {
                result.Append("<td>No Aplica</td>");
            }
            else if (t.IdSistema.Equals(0) && t.IdArea != null)
            {
                var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                                        where vValUser.idUsuario == t.IdUsuario
                                        select vValUser).FirstOrDefault();

                result.AppendFormat("<td>{0}</td>", validacionUsu.area);
            }

            result.AppendFormat("<td>{0}</td>", t.FechaReporte);
            result.AppendFormat("<td>{0}</td>", t.Asunto);
            result.AppendFormat("<td>{0}</td>", t.Prioridad);
            var partesNombre = t.Usuario.Split(' ');
            var partesSinUltimo = partesNombre.Take(partesNombre.Length - 1).ToArray();
            result.AppendFormat("<td>{0}</td>", string.Join(" ", partesSinUltimo));
            result.AppendFormat("<td><a title='Consultar Incidencia' onclick='javascript:getDetalleReporte({0},23);'><span id='icon-25' class='consultar verde'></span></a></td>", t.IdReporte);
            result.Append("</tr>");
        }

        result.Append("</tbody>");
        result.Append("<tfoot>");
        result.Append("<tr>");
        result.Append("<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>");
        result.Append("</tr>");
        result.Append("</tfoot>");
        result.Append("</table>");
        result.Append("</fieldset>"); 

        return result.ToString();
    }

    /// <summary>
    /// Método para generar la tabla de reportes
    /// por validar.
    /// </summary>
    /// <returns>Cadena con estructura de la tabla</returns>
    public string getTblReportesPorValidar(int idUsuario)
    {
        StringBuilder result = new StringBuilder();

        ControllerUsuarios cUsuarios = new ControllerUsuarios();

        var lstReportes = (from rep in erp.tReporte
                           join res in erp.tResponsableReporte on rep.idReporte equals res.idReporte
                           where res.idResponsable == idUsuario && rep.bHistorico == 1 && res.idTipoResponsable == 1 && rep.idEstatusReporte == 3 // En pruebas
                           select new
                           {
                               IdReporte = rep.idReporte,
                               Folio = rep.folio,
                               FechaReporte = String.Format("{0:yyyy/MM/dd HH:mm}", rep.fechaReporte),
                               IdUsuario = rep.idUsuario,
                               TipoReporte = rep.cTipoReporte.nombreTipoReporte,
                               Prioridad = rep.cPrioridadReporte.nombrePrioridad,
                               Grupo = rep.tERPGrupo.nomGrupo.ToUpper(),
                               Asunto = rep.asunto,
                               IdSistema = rep.idSistema,
                               IdArea = rep.idArea,
                               IdEstatusReporte = rep.idEstatusReporte,
                               NombreEstadoReporte = rep.cEstatusReporte.nombreEstadoReporte,
                               IdTipoReporte = rep.idTipoReporte,
                               nomSistema = rep.cSistemas.nomenglatura.ToUpper(),
                           });        

        if (lstReportes.ToList().Count > 0)
        {
            result.Append("<fieldset>");
            result.Append("<legend>TICKETS POR VALIDAR</legend>");
            result.Append("<table id='tblReportePorValidar' class='data_grid display'>");
            result.Append("<thead id='grid-head2'>");
            result.Append("<th>Folio</th>")
                .Append("<th>Tipo Reporte</th>")
                .Append("<th>Grupo</th>")
                .Append("<th>Sistema/Área</th>")
                .Append("<th>Fecha Reporte</th>")
                .Append("<th>Asunto</th>")
                .Append("<th>Prioridad</th>")
                .Append("<th>Usuario</th>")
                .Append("<th>Ver</th>");
            result.Append("</thead>");
            result.Append("<tbody id='grid-body'>");

            erpRH = new ERPManagementRHDataContext();

            foreach (var t in lstReportes)
            {
                result.Append("<tr>");
                result.AppendFormat("<td><span class='hide'>{0}</span>{1}</td>", t.FechaReporte, t.Folio);
                result.AppendFormat("<td>{0}</td>", t.TipoReporte);
                result.AppendFormat("<td>{0}</td>", t.Grupo);

                if (t.IdSistema != 0 && t.IdArea == 0)
                {
                    result.AppendFormat("<td>{0}</td>", t.nomSistema);
                }
                else if (t.IdSistema == null || t.IdArea == null)
                {
                    result.Append("<td>No Aplica</td>");
                }
                else if (t.IdSistema.Equals(0) && t.IdArea != null)
                {                    
                    var usuarios = (from u in erpRH.view_ValidarTipoUsuarioAreaGrupo where u.idUsuario == idUsuario select u).FirstOrDefault();

                    result.Append("<td>").Append(usuarios.area).Append("</td>");
                }

                result.AppendFormat("<td>{0}</td>", t.FechaReporte);
                result.AppendFormat("<td>{0}</td>", t.Asunto);
                result.AppendFormat("<td>{0}</td>", t.Prioridad);
                var usuario = cUsuarios.getUsuarioById(t.IdUsuario.GetValueOrDefault());
                if (usuario != null)
                {
                    var partesNombre = usuario.Trim().Split(' ');
                    var partesSinUltimo = partesNombre.Take(partesNombre.Length - 1).ToArray();
                    result.AppendFormat("<td>{0}</td>", string.Join(" ", partesSinUltimo));
                }
                else
                {
                    result.Append("<td></td>");
                }

                result.AppendFormat("<td><a title='Consultar Incidencia' onclick='javascript:getDetalleReporte({0},33);'><span id='icon-25' class='consultar verde'></span></a></td>", t.IdReporte);
                result.Append("</tr>");
            }

            result.Append("</tbody>");
            result.Append("<tfoot>");
            result.Append("<tr>");
            result.Append("<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>");
            result.Append("</tr>");
            result.Append("</tfoot>");
            result.Append("</table>");
            result.Append("</fieldset>");
        }
        return result.ToString();
    }

    /// <summary>
    /// Método para generar tabla con todos los reportes que se encuentren en el estatus 1
    /// </summary>
    /// <returns>Estructura de la tabla con los reportes en el estatus 1</returns>
    public string getReportesCreados(int idUsuario)
    {
        StringBuilder result = new StringBuilder();

        var lstReportes = (from r in erp.tReporte
                           join u in erp.vUsuariosERPM on r.idUsuario.ToString() equals u.idEmpleado
                           where r.idEstatusReporte == 1 && r.idUsuario == idUsuario
                           orderby r.fechaReporte descending
                           select new
                           {
                               IdReporte = r.idReporte,
                               Folio = r.folio,
                               FechaReporte = String.Format("{0:yyyy/MM/dd HH:mm}", r.fechaReporte),
                               Usuario = u.nombreCompleto,
                               TipoReporte = r.cTipoReporte.nombreTipoReporte,
                               Prioridad = r.cPrioridadReporte.nombrePrioridad,
                               Grupo = r.tERPGrupo.nomGrupo.ToUpper(),
                               Asunto = r.asunto,
                               IdSistema = r.idSistema,
                               IdArea = r.idArea,
                               FechaPropuesta = String.Format("{0:yyyy/MM/dd}", r.fechaPropuesta),
                               nomSistema = r.cSistemas.nomenglatura.ToUpper(),
                           }).ToList();

        var usuarios = (from u in erpRH.view_ValidarTipoUsuarioAreaGrupo where u.idUsuario == idUsuario select u).FirstOrDefault();

        if (lstReportes.Count > 0)
        {
            result.Append("<fieldset>");
            result.Append("<legend>MIS TICKETS CREADOS</legend>");
            result.Append("<table id='tblReportesCreados' class='data_grid display'>");
            result.Append("<thead id='grid-head2'>");
            result.Append("<th>Folio</th>")
                .Append("<th>Tipo Reporte</th>")
                .Append("<th>Grupo</th>")
                .Append("<th>Sistema/Área</th>")
                .Append("<th>Fecha Reporte</th>")
                .Append("<th>Asunto</th>")
                .Append("<th>Prioridad</th>")
                .Append("<th>Fecha Propuesta</th>")
                .Append("<th>Ver</th>");
            result.Append("</thead>");
            result.Append("<tbody id='grid-body'>");

            foreach (var t in lstReportes)
            {
                result.Append("<tr>");
                result.AppendFormat("<td><span class='hide'>{0}</span>{1}</td>", t.FechaReporte, t.Folio);
                result.AppendFormat("<td>{0}</td>", t.TipoReporte);
                result.AppendFormat("<td>{0}</td>", t.Grupo);

                if (t.IdSistema != 0 && t.IdArea == 0)
                {
                    result.AppendFormat("<td>{0}</td>", t.nomSistema);
                }
                else if (t.IdSistema == null || t.IdArea == null)
                {
                    result.Append("<td>No Aplica</td>");
                }
                else if (t.IdSistema.Equals(0) && t.IdArea != null)
                {
                    result.Append("<td>").Append(usuarios.area).Append("</td>");
                }

                result.AppendFormat("<td>{0}</td>", t.FechaReporte);
                result.AppendFormat("<td>{0}</td>", t.Asunto);
                result.AppendFormat("<td>{0}</td>", t.Prioridad);
                result.AppendFormat("<td>{0}</td>", t.FechaPropuesta);
                result.AppendFormat("<td><a title='Consultar Incidencia' onclick='javascript:getDetalleReporte({0},11);'><span id='icon-25' class='consultar verde'></span></a></td>", t.IdReporte);
                result.Append("</tr>");
            }

            result.Append("</tbody>");
            result.Append("<tfoot>");
            result.Append("<tr>");
            result.Append("<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>");
            result.Append("</tr>");
            result.Append("</tfoot>");
            result.Append("</table>");
            result.Append("</fieldset>");
        }

        return result.ToString();
    }

    /// <summary>
    /// Método para generar la tabla de las incidencias que se encuentran en el estatus 2 o 4(Asignados/Rechazados)
    /// </summary>
    /// <returns>Estructura de la tabla de las incidencias en soporte</returns>
    public string getReportesEnProceso(int idUsuario)
    {
        StringBuilder result = new StringBuilder();

        erpRH = new ERPManagementRHDataContext();

        var statusPermitidos = new Int32[] { 2, 4, 8, 9 }; // En Proceso, En Correcciones, Avance, Detenido

        var lstReportes = (from r in erp.tReporte
                           join u in erp.vUsuariosERPM on r.idUsuario.ToString() equals u.idEmpleado
                           where r.idUsuario == idUsuario && statusPermitidos.Contains(r.idEstatusReporte)
                           orderby r.fechaReporte descending
                           select new
                           {
                               IdReporte = r.idReporte,
                               Folio = r.folio,
                               FechaReporte = String.Format("{0:yyyy/MM/dd HH:mm}", r.fechaReporte),
                               Usuario = u.nombreCompleto,
                               TipoReporte = r.cTipoReporte.nombreTipoReporte,
                               Prioridad = r.cPrioridadReporte.nombrePrioridad,
                               Grupo = r.tERPGrupo.nomGrupo.ToUpper(),
                               Asunto = r.asunto,
                               IdSistema = r.idSistema,
                               IdArea = r.idArea,
                               IdEstatusReporte = r.idEstatusReporte,
                               NombreEstadoReporte = r.cEstatusReporte.nombreEstadoReporte,
                               nomSistema = r.cSistemas.nomenglatura.ToUpper(),
                           }).ToList();

        var usuarios = (from u in erpRH.view_ValidarTipoUsuarioAreaGrupo where u.idUsuario == idUsuario select u).FirstOrDefault();

        if (lstReportes.Count > 0)
        {
            result.Append("<fieldset>");
            result.Append("<legend>MIS TICKETS EN PROCESO</legend>");
            result.Append("<table id='tblReportesEnProceso' class='data_grid display'>");
            result.Append("<thead id='grid-head2'>");
            result.Append("<th>Folio</th>")
                .Append("<th>Tipo Reporte</th>")
                .Append("<th>Grupo</th>")
                .Append("<th>Sistema/Área</th>")
                .Append("<th>Fecha Reporte</th>")
                .Append("<th>Asunto</th>")
                .Append("<th>Prioridad</th>")
                .Append("<th>Estatus</th>")
                .Append("<th>Ver</th>");
            result.Append("</thead>");
            result.Append("<tbody id='grid-body'>");

            foreach (var t in lstReportes)
            {
                result.Append("<tr>");
                result.AppendFormat("<td><span class='hide'>{0}</span>{1}</td>", t.FechaReporte, t.Folio);
                result.AppendFormat("<td>{0}</td>", t.TipoReporte);
                result.AppendFormat("<td>{0}</td>", t.Grupo);

                if (t.IdSistema != 0 && t.IdArea == 0)
                {
                    result.AppendFormat("<td>{0}</td>", t.nomSistema);
                }
                else if (t.IdSistema == null || t.IdArea == null)
                {
                    result.Append("<td>No Aplica</td>");
                }
                else if (t.IdSistema.Equals(0) && t.IdArea != null)
                {
                    result.Append("<td>").Append(usuarios.area).Append("</td>");
                }

                result.AppendFormat("<td>{0}</td>", t.FechaReporte);
                result.AppendFormat("<td>{0}</td>", t.Asunto);
                result.AppendFormat("<td>{0}</td>", t.Prioridad);

                switch (t.IdEstatusReporte)
                {
                    case 2: //Proceso
                        result.Append("<td> <span id='icon-25' class='proceso verde' title='En Proceso'>").Append(t.NombreEstadoReporte).Append("</span></td>");
                        break;
                    case 4: //Correcciones
                        result.Append("<td> <span id='icon-25' class='procesos' title='En Correcciones'>").Append(t.NombreEstadoReporte).Append("</span></td>");
                        break;
                    case 8: //Avance
                        result.Append("<td> <span id='icon-25' class='proceso verde' title='Con Avance'>").Append(t.NombreEstadoReporte).Append("</span></td>");
                        break;
                    case 9: //Detenido
                        result.Append("<td> <span id='icon-25' class='proceso rojo' title='Detenido'>").Append(t.NombreEstadoReporte).Append("</span></td>");
                        break;
                }

                result.AppendFormat("<td><a title='Consultar Incidencia' onclick='javascript:getDetalleReporte(").Append(t.IdReporte).Append(",21);'><span id='icon-25' class='consultar verde'></span></a></td>");
                result.AppendFormat("</tr>");
            }

            result.Append("</tbody>");
            result.Append("<tfoot>");
            result.Append("<tr>");
            result.Append("<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>");
            result.Append("</tr>");
            result.Append("</tfoot>");
            result.Append("</table>");
            result.Append("</fieldset>");
        }

        return result.ToString(); ;
    }

    /// <summary>
    /// Método para generar la tabla de las incidencias que se encuentren en estatus 3(Por Validar)
    /// </summary>
    /// <returns>Estructura de la tabla de incidencias por Validar</returns>
    public string getReportesCValidar(int idUsuario)
    {
        StringBuilder result = new StringBuilder();

        erpRH = new ERPManagementRHDataContext();

        var lstReportes = (from rep in erp.tReporte
                           join res in erp.tResponsableReporte on rep.idReporte equals res.idReporte
                           join usu in erp.vUsuariosERPM on res.idResponsable.ToString() equals usu.idEmpleado
                           where rep.idUsuario == idUsuario && res.idTipoResponsable == 1 && rep.idEstatusReporte == 3 // En pruebas
                           select new
                           {
                               IdReporte = rep.idReporte,
                               Folio = rep.folio,
                               FechaReporte = String.Format("{0:yyyy/MM/dd HH:mm}", rep.fechaReporte),
                               Responsable = usu.nombreCompleto.Trim(),
                               TipoReporte = rep.cTipoReporte.nombreTipoReporte,
                               Prioridad = rep.cPrioridadReporte.nombrePrioridad,
                               Grupo = rep.tERPGrupo.nomGrupo.ToUpper(),
                               Asunto = rep.asunto,
                               IdSistema = rep.idSistema,
                               IdArea = rep.idArea,
                               IdEstatusReporte = rep.idEstatusReporte,
                               NombreEstadoReporte = rep.cEstatusReporte.nombreEstadoReporte,
                               IdTipoReporte = rep.idTipoReporte,
                               nomSistema = rep.cSistemas.nomenglatura.ToUpper(),
                           });

        var query = erp.GetCommand(lstReportes);

        var usuarios = (from u in erpRH.view_ValidarTipoUsuarioAreaGrupo where u.idUsuario == idUsuario select u).FirstOrDefault();

        if (lstReportes.ToList().Count > 0)
        {
            result.Append("<fieldset>");
            result.Append("<legend>MIS TICKETS POR VALIDAR</legend>");
            result.Append("<table id='tblReportesCReportesValidar' class='data_grid display'>");
            result.Append("<thead id='grid-head2'>");
            result.Append("<th>Folio</th>")
                .Append("<th>Tipo Reporte</th>")
                .Append("<th>Grupo</th>")
                .Append("<th>Sistema/Área</th>")
                .Append("<th>Fecha Reporte</th>")
                .Append("<th>Asunto</th>")
                .Append("<th>Prioridad</th>")
                .Append("<th>Responsable</th>")
                .Append("<th>Ver</th>");
            result.Append("</thead>");
            result.Append("<tbody id='grid-body'>");

            foreach (var t in lstReportes)
            {
                result.Append("<tr>");
                result.AppendFormat("<td><span class='hide'>{0}</span>{1}</td>", t.FechaReporte, t.Folio);
                result.AppendFormat("<td>{0}</td>", t.TipoReporte);
                result.AppendFormat("<td>{0}</td>", t.Grupo);

                if (t.IdSistema != 0 && t.IdArea == 0)
                {
                    result.AppendFormat("<td>{0}</td>", t.nomSistema);
                }
                else if (t.IdSistema == null || t.IdArea == null)
                {
                    result.Append("<td>No Aplica</td>");
                }
                else if (t.IdSistema.Equals(0) && t.IdArea != null)
                {
                    result.Append("<td>").Append(usuarios.area).Append("</td>");
                }

                result.AppendFormat("<td>{0}</td>", t.FechaReporte);
                result.AppendFormat("<td>{0}</td>", t.Asunto);
                result.AppendFormat("<td>{0}</td>", t.Prioridad);

                var partesNombre = t.Responsable.Split(' ');
                var partesSinUltimo = partesNombre.Take(partesNombre.Length - 1).ToArray();
                result.AppendFormat("<td>{0}</td>", string.Join(" ", partesSinUltimo));

                if (t.IdTipoReporte == 3)
                {
                    result.AppendFormat("<td><a title='Consultar incidencia' href='Configuracion/Reportes/CuotasSistemas.aspx?id={0}'><span id='icon-25' class='consultar verde'></span></a></td>", utileria.EncodeTo64(t.IdReporte.ToString()));
                }
                else
                {
                    result.AppendFormat("<td><a title='Consultar Incidencia' onclick='javascript:getDetalleReporte({0},31);'><span id='icon-25' class='consultar verde'></span></a></td>", t.IdReporte);
                }

                result.Append("</tr>");
            }

            result.Append("</tbody>");
            result.Append("<tfoot>");
            result.Append("<tr>");
            result.Append("<td class='foot shadow3 round-down2' colspan='9'>&nbsp;</td>");
            result.Append("</tr>");
            result.Append("</tfoot>");
            result.Append("</table>");
            result.Append("</fieldset>");
        }

        return result.ToString();
    }

    /// <summary>
    /// Método para consultar un objeto de tipo
    /// tReporte por idReporte.
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>Objeto de tipo tReporte</returns>
    public tReporte getReporteById(int idReporte)
    {

        var objReporte = (from r in erp.tReporte
                          where r.idReporte == idReporte
                          select r).FirstOrDefault();

        return objReporte;
    }

    /// <summary>
    /// Método para traer el total de reportes
    /// sin asignar.
    /// </summary>
    /// <returns>Número de reportes sin asignar</returns>
    public int getCountReporteSinAsignar(int idUsuario, int rolSoporte, List<string> lstUsuarioTipo)
    {
        int countReportes = 0;
        string[] soportesIncidencia = { "1", "2", "3", "4" ,"14", "15", "16", "17", "18" , "19", "20", "21", "21", "22" };
        if (lstUsuarioTipo.Count == 0)
        {
            lstUsuarioTipo = soportesIncidencia.ToList();
        }
        if (rolSoporte > 0)
        {
            countReportes = (from reportes in erp.tReporte
                             join u in erp.vUsuariosERPM on reportes.idUsuario.ToString() equals u.idEmpleado
                             where reportes.idEstatusReporte == 1 && reportes.bHistorico == 1 && lstUsuarioTipo.Contains(reportes.idTipoReporte.ToString())
                             select reportes).Count();
        }
        return countReportes;
    }

    public int getCountReporteSinAsignarHistorico(int idUsuario, int rolSoporte, List<string> lstUsuarioTipo)
    {
        int countReportes = 0;
        string[] soportesIncidencia = { "1", "2", "3", "4", "14", "15", "16", "17", "18", "19", "20", "21", "21", "22" };
        if (lstUsuarioTipo.Count == 0)
        {
            lstUsuarioTipo = soportesIncidencia.ToList();
        }
        if (rolSoporte > 0)
        {
            countReportes = (from reportes in erp.tReporte
                             join u in erp.vUsuariosERPM on reportes.idUsuario.ToString() equals u.idEmpleado
                             where reportes.idEstatusReporte == 1 && reportes.bHistorico == 0 && lstUsuarioTipo.Contains(reportes.idTipoReporte.ToString())
                             select reportes).Count();
        }
        return countReportes;
    }

    public int getCountReporteSinAsignarSprint(int idUsuario, int rolSoporte, List<string> lstUsuarioTipo)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string sPrint = sp.recuperaValor("select iNumeroSprint from tSprint where dFechaFin >= convert(varchar(10),GETDATE(),121) AND dFechaInicio <= convert(varchar(10),GETDATE(),121) and sAnio = YEAR(GETDATE())");

        int countReportes = 0;
        string[] soportesIncidencia = { "1", "2", "3", "4", "14", "15", "16", "17", "18", "19", "20", "21", "21", "22" };
        if (lstUsuarioTipo.Count == 0)
        {
            lstUsuarioTipo = soportesIncidencia.ToList();
        }
        if (rolSoporte > 0)
        {
            //BORRAR COMENTARIOS SOLO ES PARA PRUEBAS
            countReportes = (from reportes in erp.tReporte
                             join u in erp.vUsuariosERPM on reportes.idUsuario.ToString() equals u.idEmpleado
                             where reportes.idEstatusReporte == 1 && reportes.iSprint == int.Parse(sPrint) && 
                             reportes.bHistorico == 1 && 
                             lstUsuarioTipo.Contains(reportes.idTipoReporte.ToString())
                             select reportes).Count();
        }
        return countReportes;
    }

    /// <summary>
    /// Método para traer el total de reportes
    /// asignados.
    /// </summary>
    /// <returns>Número de reportes asignados</returns>
    public int getCountReportesAsignados(int idUsuario, int soporteInc, List<vUsuariosSoporte> lstsoporteAdu)
    {

        int countReportes = 0;
        if (soporteInc > 0)
        {
            string[] soportesIncidencia = { "1", "2", "3", "4", "14", "15", "16", "17", "18", "19", "20", "21", "22" };
            countReportes = (from reportes in erp.tReporte
                             join responsables in erp.tResponsableReporte
                             on reportes.idReporte equals responsables.idReporte
                             where (reportes.idEstatusReporte == 2 || reportes.idEstatusReporte == 4 || reportes.idEstatusReporte == 8 || reportes.idEstatusReporte == 9) &&
                             responsables.idResponsable == idUsuario &&
                             (responsables.idTipoResponsable == 1 || responsables.idTipoResponsable == 2) &&
                             soportesIncidencia.Contains(reportes.idTipoReporte.ToString())
                             select reportes).Count();
        }

        if (lstsoporteAdu.Count > 0)
        {
            foreach (vUsuariosSoporte tu in lstsoporteAdu)
            {
                var lstReportes = (from r in erp.tReporte
                                   join responsables in erp.tResponsableReporte
                                   on r.idReporte equals responsables.idReporte
                                   where tu.idReporte == r.idReporte &&
                                   (tu.idEstatusReporte == 2 || tu.idEstatusReporte == 4 || tu.idEstatusReporte == 8 || tu.idEstatusReporte == 9) &&
                                   r.idERPGrupo == tu.idGrupo
                                   && r.idTipoReporte == tu.idSoporte && responsables.idResponsable == idUsuario &&
                                   (responsables.idTipoResponsable == 1 || responsables.idTipoResponsable == 2)
                                   select r).ToList();

                countReportes = countReportes + lstReportes.Count;
            }
        }
        return countReportes;
    }

    /// <summary>
    /// Método para traer el total de reportes
    /// en espera de validación.
    /// </summary>
    /// <returns>Número de reportes asignados</returns>
    public int getCountReportesPorValidar(int idUsuario, int soporteInc, List<vUsuariosSoporte> lstsoporteAdu)
    {
        int countReportes = 0;
        if (soporteInc > 0)
        {
            string[] soportesIncidencia = { "1", "2", "3", "4", "14", "15", "16", "17", "18", "19", "20", "21", "22" };
            countReportes = (from reportes in erp.tReporte
                             join responsables in erp.tResponsableReporte
                             on reportes.idReporte equals responsables.idReporte
                             where reportes.idEstatusReporte == 3 &&
                             responsables.idResponsable == idUsuario &&
                             responsables.idTipoResponsable == 1 &&
                             reportes.bHistorico == 1 &&
                             soportesIncidencia.Contains(reportes.idTipoReporte.ToString())
                             select reportes).Count();
        }

        if (lstsoporteAdu.Count > 0)
        {
            foreach (vUsuariosSoporte tu in lstsoporteAdu)
            {
                int lstReportes = (from r in erp.tReporte
                                   join responsables in erp.tResponsableReporte
                                   on r.idReporte equals responsables.idReporte
                                   where r.idERPGrupo == tu.idGrupo
                                   && r.idTipoReporte == tu.idSoporte && r.idEstatusReporte == 3 &&
                                   responsables.idResponsable == idUsuario &&
                                   r.bHistorico == 1 &&
                                   responsables.idTipoResponsable == 1
                                   select r).Count();

                countReportes = countReportes + lstReportes;
            }
        }
        return countReportes;
    }

    /// <summary>
    /// Método para traer el total de reportes se soporte
    /// </summary>
    /// <returns>Número de reportes de soportes sin asignar</returns>
    public int getCountReportesSoporte(int idUsuario, List<vUsuariosSoporte> lstTUsuarioSoporte, List<string> lstUsuarioTipo)
    {
        /*var lstReportes = (from ro in erp.vUsuariosSoporte
                           where ro.idUsuario == idUsuario && ro.idEstatusReporte == 1
                           select ro).Count();*/

        var lstReportes = (from ro in erp.vUsuariosSoporte
                           where ro.idUsuario == idUsuario && ro.idEstatusReporte == 1
                           && lstUsuarioTipo.Contains(ro.idTipoReporte.ToString())
                           select ro).Count();

        return lstReportes;

        //foreach (vUsuariosSoporte tu in lstTUsuarioSoporte)
        //{
        //    var lstReportes = (from r in erp.tReporte
        //                       where r.idERPGrupo == tu.idGrupo
        //                       && r.idTipoReporte == tu.idSoporte && tu.idEstatusReporte == 1 && r.idEstatusReporte == 1
        //                       select r).ToList();

        //    contador = contador + lstReportes.Count;
        //}
        //return contador;
    }

    public int getCountReporteCreado(int idUsuario)
    {
        var countReportes = (from reportes in erp.tReporte
                             where reportes.idEstatusReporte == 1 && reportes.idUsuario == idUsuario
                             select reportes).Count();

        return countReportes;
    }

    public int getCountReportesEnProceso(int idUsuario)
    {
        var countReportes = (from reportes in erp.tReporte

                             where (reportes.idEstatusReporte == 2 || reportes.idEstatusReporte == 4 || reportes.idEstatusReporte == 8 || reportes.idEstatusReporte == 9) && reportes.idUsuario == idUsuario
                             select reportes).Count();

        return countReportes;
    }

    public int getCountReportesCPorValidar(int idUsuario)
    {
        var countReportes = (from reportes in erp.tReporte

                             where reportes.idEstatusReporte == 3 && reportes.idUsuario == idUsuario
                             select reportes).Count();

        return countReportes;
    }

    /// <summary>
    /// Método para obtener el detalle de un reporte que es consultado.
    /// </summary>
    /// <returns>objeto de Tipo view_DetalleReporte, con la informacion del reporte</returns>
    public view_DetalleReporte consultarReporteCreado(int idReporte)
    {
        var detReporte = (from vdr in erp.view_DetalleReporte
                          where vdr.idReporte == idReporte
                          select vdr).SingleOrDefault();

        if (detReporte.idTipoReporte > 4)
        {

            var tr = (from tre in erp.tReporte
                      where tre.idReporte == detReporte.idReporte
                      select tre.idUsuario).FirstOrDefault();

            var nombreArea = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                              where vValUser.idUsuario == tr
                              select vValUser.area).FirstOrDefault();

            detReporte.nomSistema = "<label id='frm'>Area:</label> <br /><label>" + nombreArea + "</label>";
        }
        else
        {
            detReporte.nomSistema = "<label id='frm'>Sistema:</label> <br /><label>" + detReporte.nomSistema + "</label>";
        }

        return detReporte;
    }

    public List<string> consultarHistoriaCreada(int idHistoria)
    {
        List<string> lstResultados = new List<string>();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");

        string sQuery = "select teg.idERPGrupo,idSistema,sEpica,sHistoria,idPrioridad,sDescripcion,sCriteriosAceptacion " +
            " ,sRiesgos,idEstatus,idProductBackLog,sEvidencia,isnull(storyProints,0) from tProductBackLog tpb inner join tERPGrupo teg  on tpb.sCliente = nomGrupo" +
            " where idProductBackLog = "+idHistoria;

        lstResultados = sp.recuperaRegistros(sQuery);

        return lstResultados;
    }


    /// <summary>
    /// Método para eliminar(cambiar de estatus el reporte a 6 o Eliminado) Reporte
    /// Así como realizar un insert en la tabla tLogReporte, en el que se indica que el reporte se eliminó o cambió de estatus
    /// </summary>
    /// <returns>True si se cambio de manera exitosa el estatus del reporte 
    /// y el insert en la tabla tLogReporte de lo contrario False</returns>
    public bool eliminarReporteCreado(int idReporte, int idUsuario)
    {
        try
        {
            var reporte = (from tr in erp.tReporte
                           where tr.idReporte == idReporte
                           select tr).FirstOrDefault();

            reporte.idEstatusReporte = 6;

            erp.SubmitChanges();

            //Insert en tLogReporte
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
    /// Método por medio del cual se puede modificar un reporte que previamente ha sido creado
    /// Así como realizar un insert en la tabla tLogReporte, en el que se indica que el reporte se modifico
    /// </summary>
    /// <returns>True si se actualizó de manera exitosa el reporte y el insert en la tabla tLogReporte de lo contrario False</returns>
    public bool modificaReporte(int idReporte, string asunto, string descripcion, int idTipoReporte, int idPrioridad, int idSistema, int idERPGrupo, string fechaPropuesta, string nombreArchivo, int idUsuario)
    {

        try
        {
            var reporte = (from tr in erp.tReporte
                           where tr.idReporte == idReporte
                           select tr).FirstOrDefault();

            if (reporte.idTipoReporte != idTipoReporte)
            {
                var incremento = (from trep in erp.tReporte
                                  where trep.idTipoReporte == idTipoReporte
                                  select trep.idReporte).Count();

                var claveTipo = (from ctr in erp.cTipoReportes
                                 where ctr.idTipoReporte == idTipoReporte
                                 select ctr).SingleOrDefault();
                string fecha = reporte.fechaReporte.ToString();

                reporte.folio = claveTipo.clave + "-" + fecha.Substring(6, 4) + "-" + (incremento + 1);
            }

            DateTime fechaProp = Convert.ToDateTime(fechaPropuesta);
            reporte.asunto = asunto;
            reporte.descripcion = descripcion;
            reporte.idTipoReporte = idTipoReporte;
            reporte.idPrioridad = idPrioridad;
            if (idTipoReporte != 3 && idTipoReporte < 5)
            {
                reporte.idSistema = idSistema;
            }
            reporte.fechaPropuesta = fechaProp;
            reporte.idERPGrupo = idERPGrupo;
            reporte.evidencia = nombreArchivo;

            erp.SubmitChanges();

            //Insert en tLogReporte
            string fechaAccion = DateTime.Now.ToShortDateString();
            DateTime fechaAcc = Convert.ToDateTime(fechaAccion);
            tLogReporte.idReporte = idReporte;
            tLogReporte.idUsuario = idUsuario;
            tLogReporte.fecha = fechaAcc;
            tLogReporte.idAccion = 3;

            erp.tLogReporte.InsertOnSubmit(tLogReporte);
            erp.SubmitChanges();

            enviarCorreoModifica(idUsuario, idReporte, idTipoReporte);

            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }


    /// <summary>
    /// Método por medio del cual se puede modificar un reporte que previamente ha sido creado
    /// Así como realizar un insert en la tabla tLogReporte, en el que se indica que el reporte se modifico
    /// </summary>
    /// <returns>True si se actualizó de manera exitosa el reporte y el insert en la tabla tLogReporte de lo contrario False</returns>
    public bool modificarReportes(int idReporte,  int idTipoReporte, int idPrioridad, int idSistema, int idERPGrupo, int idUsuario)
    {

        try
        {
            var reporte = (from tr in erp.tReporte
                           where tr.idReporte == idReporte
                           select tr).FirstOrDefault();

            if (reporte.idTipoReporte != idTipoReporte)
            {
                var incremento = (from trep in erp.tReporte
                                  where trep.idTipoReporte == idTipoReporte
                                  select trep.idReporte).Count();

                var claveTipo = (from ctr in erp.cTipoReportes
                                 where ctr.idTipoReporte == idTipoReporte
                                 select ctr).SingleOrDefault();
                string fecha = reporte.fechaReporte.ToString();

                var folioNuevo = (claveTipo.clave + "-" + fecha.Substring(6, 4) + "-" + (incremento + 1)).Replace(" ", "");

                var folioExistente = (from tr in erp.tReporte
                                      where tr.folio == folioNuevo
                                      select tr.folio).FirstOrDefault();

                if (folioExistente != null && folioExistente == folioNuevo)
                {
                    reporte.folio = (claveTipo.clave + "-" + fecha.Substring(6, 4) + "-" + (incremento + 2)).Replace(" ", "");
                }
                else
                {
                    reporte.folio = folioNuevo;
                }
            }

            reporte.idTipoReporte = idTipoReporte;
            reporte.idPrioridad = idPrioridad;
            reporte.idSistema = idSistema;
            reporte.idERPGrupo = idERPGrupo;

            erp.SubmitChanges();

            //Insert en tLogReporte
            string fechaAccion = DateTime.Now.ToShortDateString();
            DateTime fechaAcc = Convert.ToDateTime(fechaAccion);
            tLogReporte.idReporte = idReporte;
            tLogReporte.idUsuario = idUsuario;
            tLogReporte.fecha = fechaAcc;
            tLogReporte.idAccion = 3;

            erp.tLogReporte.InsertOnSubmit(tLogReporte);
            erp.SubmitChanges();

            enviarCorreoModifica(idUsuario, idReporte, idTipoReporte);

            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public string getTblProductBackLog(int idUsuario, int idEstatus)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string result = "";
        string sQuery = "";

        sQuery = "select sCliente,cs.nomenglatura,sEpica,sHistoria,cp.nombrePrioridad,idProductBackLog " +
            " from tProductBackLog tp "+ 
            " inner join cSistemas  cs on tp.idSistema = cs.idSistema "+
            " inner join cPrioridadReporte cp on tp.idPrioridad = cp.idPrioridadReporte where idEstatus ="+idEstatus;

        DataSet ds = sp.getValues(sQuery);

        if (ds.Tables[0].Rows.Count > 0)
        {
            result += "<fieldset>" +
                        "<legend>Product BackLog</legend>";

            if(idEstatus == 2) //Si el estatus es Refinado mostrar los botones para generar y leer excel
            { 
                result += "<div class='width98 clear txt-right ' id ='divExcel'>" +
                            "<a id='divBtnGenerarExcel' onclick='javascript:generarExcel(2);' > <span id='icon-47' class='excel btnGenerarExcel azul' style='display: inline; cursor:pointer;'>Generar Excel</span> </a> " +
                            "<a id='btnLeerExcel' href='#' class='btn azul shadow2'> Leer Excel </a> " +
                          "</div> <br>";
            }
            if (idEstatus == 3)//Si el estatus es Puntuado mostrar los seleeccionar todos y seleccionar sprint para mover las historias seleccionadas a un sprint
            {
                result += "<div class='width98 clear txt-right ' id ='divExcel'>" +
                            "<a id='btnSeleccionarTodos' class='btn verde shadow2' >Seleccionar Todos</a> " +
                            "<a id='btnSeleccionarSprint'  class='btn azul shadow2'> Seleccionar Sprint </a> " +
                          "</div> <br> <br>";
                //Se agrega un espacio para checkboxs a la tabla
                result += "<table id='tblProductBackLog' class='data_grid display'>" +
                                "<thead id='grid-head2'>" +
                                "<th width=\"10%\"></th>" +
                                "<th width=\"15%\">Cliente</th>" +
                                "<th width=\"10%\">Sistema</th>" +
                                "<th width=\"23%\">Epica</th>" +
                                "<th width=\"23%\">Historia</th>" +
                                "<th width=\"10%\">Prioridad</th>" +
                                "<th width=\"9%\">Acciones</th>" +
                            "</thead>" +
                            "<tbody id='grid-body'>";
                var i = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result += "<tr>";
                    result += "<td> <input type=\"checkbox\" name=\"chkHistorias\" class=\"check validar\" id='chkHistoria"+ row[5].ToString()+ "' value= '" + row[5].ToString() + "' />  <label for=\"chkHistoria"+ row[5].ToString()+ "\" style=\"margin-top: 4%;\"></label></td>";
                    result += "<td style=\"text-align: center;\">" + row[0].ToString() + "</td>";
                    result += "<td style=\"text-align: center;\">" + row[1].ToString() + "</td>";
                    result += "<td style=\"text-align: center;\">" + row[2].ToString() + "</td>";
                    result += "<td style=\"text-align: center;\">" + row[3].ToString() + "</td>";
                    result += "<td style=\"text-align: center;\">" + row[4].ToString() + "</td>";
                    result += "<td><a onclick='javascript:consultarHistoria(" + row[5].ToString() + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                    result += "</tr>";
                }

                result += "</tbody>" +
                            "<tfoot>" +
                                "<tr>" +
                                    "<td class='foot shadow3 round-down2' colspan='7'>&nbsp;</td>" +
                                "</tr>" +
                            "</tfoot>" +
                            "</table>" +
                          "</fieldset>";
            }
            else
            {
                result += "<table id='tblProductBackLog' class='data_grid display'>" +
                                "<thead id='grid-head2'>"+
                                "<th width=\"15%\">Cliente</th>" +
                                "<th width=\"10%\">Sistema</th>" +
                                "<th width=\"25%\">Epica</th>" +
                                "<th width=\"25%\">Historia</th>" +
                                "<th width=\"15%\">Prioridad</th>" +
                                "<th width=\"10%\">Acciones</th>" +
                            "</thead>" +
                            "<tbody id='grid-body'>";

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result += "<tr>";
                    result += "<td style=\"text-align: center;\">" + row[0].ToString() + "</td>";
                    result += "<td style=\"text-align: center;\">" + row[1].ToString() + "</td>";
                    result += "<td style=\"text-align: center;\">" + row[2].ToString() + "</td>";
                    result += "<td style=\"text-align: center;\">" + row[3].ToString() + "</td>";
                    result += "<td style=\"text-align: center;\">" + row[4].ToString() + "</td>";
                    result += "<td><a onclick='javascript:consultarHistoria(" + row[5].ToString() + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                    result += "</tr>";
                }

                result += "</tbody>" +
                            "<tfoot>" +
                                "<tr>" +
                                    "<td class='foot shadow3 round-down2' colspan='6'>&nbsp;</td>" +
                                "</tr>" +
                            "</tfoot>" +
                            "</table>" +
                          "</fieldset>";
            }
            


        }
        else
        {
            result += "<fieldset>" +
                       "<legend>Product BackLog</legend>" +
                     "<table id='tblProductBackLog' class='data_grid display'>" +
                       "<thead id='grid-head2'>" +
                           "<th width=\"15%\">Cliente</th>" +
                           "<th width=\"10%\">Sistema</th>" +
                           "<th width=\"25%\">Epica</th>" +
                           "<th width=\"25%\">Historia</th>" +
                           "<th width=\"15%\">Prioridad</th>" +
                           "<th width=\"10%\">Acciones</th>" +
                       "</thead>" +
                       "<tbody id='grid-body'>";

                result += "<tr>";
                result += "<td style=\"text-align: center;\"></td>";
                result += "<td style=\"text-align: center;\"></td>";
                result += "<td style=\"text-align: center;\"></td>";
                result += "<td style=\"text-align: center;\"></td>";
                result += "<td style=\"text-align: center;\"></td>";
                 result += "<td></td>";
                result += "</tr>";
            
            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='6'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>" +
                      "</fieldset>";
        }
        return result;
    }


    /// <summary>
    /// Método para crear la tabla de los reportes generados (*)
    /// </summary>
    /// <returns>Cadena con la tabla de los reportes generados</returns>
    public string getTblReportesCreados(int idUsuario)
    {
        StringBuilder result = new StringBuilder();

        erpRH = new ERPManagementRHDataContext();

        var reportes = from r in erp.tReporte
                       where r.idUsuario == idUsuario && r.idEstatusReporte != 6 //Eliminado
                       select new
                       {
                           idReporte = r.idReporte,
                           folio = r.folio,
                           fechaReporte = r.fechaReporte,
                           tipoReporte = r.cTipoReporte.nombreTipoReporte,
                           nomGrupo = r.tERPGrupo.nomGrupo,
                           idSistema = r.idSistema,
                           idArea = r.idArea,
                           asunto = r.asunto,
                           nomSistema = r.cSistemas.nomenglatura.ToUpper(),
                           idEstatusReporte = r.idEstatusReporte,
                           nombreEstadoReporte = r.cEstatusReporte.nombreEstadoReporte,
                           idTipoReporte = r.idTipoReporte,
                           idERPGrupo = r.idERPGrupo
                       };

        var usuarios = (from u in erpRH.view_ValidarTipoUsuarioAreaGrupo where u.idUsuario == idUsuario select u).FirstOrDefault();

        if (reportes.ToList().Count() > 0)
        {
            ControllerUsuarios cUsuarios = new ControllerUsuarios();

            result.Append("<fieldset>");
            result.Append("<legend>Incidencias Generadas</legend>");
            result.Append("<table id='tblReporteCreados' class='data_grid display'>");
            result.Append("<thead id='grid-head2'>");
            result.Append("<th>Folio</th>");
            result.Append("<th>Tipo Reporte</th>");
            result.Append("<th>Grupo</th>");
            result.Append("<th>Sistema/Área</th>");
            result.Append("<th>Fecha Reporte</th>");
            result.Append("<th>Asunto</th>");
            result.Append("<th>Estatus</th>");
            result.Append("<th>Acciones</th>");
            result.Append("</thead>");
            result.Append("<tbody id='grid-body'>");

            foreach (var tr in reportes)
            {
                result.Append("<tr>");
                result.Append("<td> <span class='hide'>").Append(String.Format("{0:yyyy/MM/dd H:mm:ss}", tr.fechaReporte)).Append(" </span> ").Append(tr.folio).Append("</td>");
                result.Append("<td>").Append(tr.tipoReporte).Append("</td>");
                result.Append("<td>").Append(tr.nomGrupo).Append("</td>");

                if (tr.idSistema != 0 && tr.idArea == 0)
                {
                    result.Append("<td>").Append(tr.nomSistema).Append("</td>");
                }

                if (tr.idSistema == null || tr.idArea == null)
                {
                    result.Append("<td>No Aplica</td>");
                }

                if (tr.idSistema.Equals(0) && tr.idArea != null)
                {
                    //var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                    //                     where vValUser.idUsuario == tr.idUsuario
                    //                     select vValUser).FirstOrDefault();

                    result.Append("<td>").Append(usuarios.area).Append("</td>");
                }

                result.Append("<td>").Append(String.Format("{0:yyyy/MM/dd HH:mm}", tr.fechaReporte)).Append("</td>");
                var asunto = tr.asunto.Length <= 128 ? tr.asunto : string.Format("{0}...", tr.asunto.Substring(0, 127));
                result.Append("<td>").Append(asunto).Append("</td>");

                if (tr.idEstatusReporte == 1)
                {
                    result.Append("<td> <span id='icon-25' class='ticket' title='Generado'>").Append(tr.nombreEstadoReporte).Append("</span></td>");
                }
                else if (tr.idEstatusReporte == 2 || tr.idEstatusReporte == 4 || tr.idEstatusReporte == 8 || tr.idEstatusReporte == 9)
                {
                    result.Append("<td> <span id='icon-25' class='proceso' title='Soporte ").Append(tr.nombreEstadoReporte).Append("'>").Append(tr.nombreEstadoReporte + "</span></td>");
                }
                else if (tr.idEstatusReporte == 3)
                {
                    result.Append("<td> <span id='icon-25' class='validar' title='Por Validar'>").Append(tr.nombreEstadoReporte).Append("</span></td>");
                }
                else
                {
                    result.Append("<td> <span id='icon-25' class='solicitud_aprobada' title='Terminado'>").Append(tr.nombreEstadoReporte).Append("</span></td>");
                }

                if (tr.idEstatusReporte == 1)
                {
                    if (tr.idTipoReporte == 3)
                    {
                        result.Append("<td> <a onclick='javascript:llenarDatosReporteERP(").Append(tr.idReporte).Append(", ").Append(tr.idTipoReporte).Append(");'><span id='icon-25' class='modificar' title='Modificar Incidencia'></span></a>   <a onclick='javascript:confirmarEliminadoERP(").Append(tr.idReporte).Append(");'><span id='icon-25' class='eliminar' title='Eliminar Incidencia'></span></a>  <a onclick='javascript:consultarReporteERP(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                    }
                    else
                    {
                        result.Append("<td> <a onclick='javascript:llenarDatosReporte(").Append(tr.idReporte).Append(", ").Append(tr.idTipoReporte).Append(", ").Append(tr.idERPGrupo).Append(");'><span id='icon-25' class='modificar' title='Modificar Incidencia'></span></a>   <a onclick='javascript:confirmarEliminado(").Append(tr.idReporte).Append(");'><span id='icon-25' class='eliminar' title='Eliminar Incidencia'></span></a>  <a onclick='javascript:consultarReporte(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                    }
                }
                else
                {
                    if (tr.idTipoReporte == 3)
                    {
                        result.Append("<td> <a onclick='javascript:consultarReporteERP(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                    }
                    else
                    {
                        result.Append("<td> <a onclick='javascript:consultarReporte(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                    }
                }

                result.Append("</tr>");
            }

            result.Append("</tbody>");
            result.Append("<tfoot>");
            result.Append("<tr>");
            result.Append("<td class='foot shadow3 round-down2' colspan='8'>&nbsp;</td>");
            result.Append("</tr>");
            result.Append("</tfoot>");
            result.Append("</table>");
            result.Append("</fieldset>");
        }
        //else
        //{
        //    result += "<br /> <br /> <div class='center bg-alert width98 clear'><span id='icon-47' class='warning blanco'>No existe ningún reporte</span></div> <br /> <br />";
        //}
        return result.ToString();
    }

    /// <summary>
    /// Método para generar la Tabla de los reportes que han sido generados, pero aun no se han asignado, es decir, en el estatus 1(Generado)
    /// </summary>
    /// <returns>Cadena, en la que se encuentra la estructura de la tabla</returns>
    public string getTblReportesGenerados(int idUsuario)
    {
        StringBuilder result = new StringBuilder();

        erpRH = new ERPManagementRHDataContext();

        var reportes = from r in erp.tReporte
                       where r.idUsuario == idUsuario && r.idEstatusReporte == 1 //Creado
                       select new
                       {
                           idReporte = r.idReporte,
                           folio = r.folio,
                           fechaReporte = r.fechaReporte,
                           tipoReporte = r.cTipoReporte.nombreTipoReporte,
                           nomGrupo = r.tERPGrupo.nomGrupo,
                           idSistema = r.idSistema,
                           idArea = r.idArea,
                           asunto = r.asunto,
                           nomSistema = r.cSistemas.nomenglatura.ToUpper(),
                           idEstatusReporte = r.idEstatusReporte,
                           nombreEstadoReporte = r.cEstatusReporte.nombreEstadoReporte,
                           idTipoReporte = r.idTipoReporte,
                           idERPGrupo = r.idERPGrupo
                       };

        var usuarios = (from u in erpRH.view_ValidarTipoUsuarioAreaGrupo where u.idUsuario == idUsuario select u).FirstOrDefault();

        if (reportes.ToList().Count > 0)
        {
            result.Append("<fieldset>");
            result.Append("<legend>Incidencias Generadas</legend>");
            result.Append("<table id='tblReporteCreados' class='data_grid display'>");
            result.Append("<thead id='grid-head2'>");
            result.Append("<th>Folio</th>");
            result.Append("<th>Tipo Reporte</th>");
            result.Append("<th>Grupo</th>");
            result.Append("<th>Sistema/Área</th>");
            result.Append("<th>Fecha Reporte</th>");
            result.Append("<th>Asunto</th>");
            result.Append("<th>Estatus</th>");
            result.Append("<th>Acciones</th>");
            result.Append("</thead>");
            result.Append("<tbody id='grid-body'>");

            foreach (var tr in reportes)
            {
                result.Append("<tr>");
                result.Append("<td> <span class='hide'>").Append(String.Format("{0:yyyy/MM/dd H:mm:ss}", tr.fechaReporte)).Append(" </span> ").Append(tr.folio).Append("</td>");
                result.Append("<td>").Append(tr.tipoReporte).Append("</td>");
                result.Append("<td>").Append(tr.nomGrupo).Append("</td>");

                if (tr.idSistema != 0 && tr.idArea == 0)
                {
                    result.Append("<td>").Append(tr.nomSistema).Append("</td>");
                }
                
                if (tr.idSistema == null || tr.idArea == null)
                {
                    result.Append("<td>No Aplica</td>");
                }
                
                if (tr.idSistema.Equals(0) && tr.idArea != null)
                {
                    //var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                    //                     where vValUser.idUsuario == tr.idUsuario
                    //                     select vValUser).FirstOrDefault();

                    result.Append("<td>").Append(usuarios.area).Append("</td>");
                }


                result.Append("<td>").Append(String.Format("{0:yyyy/MM/dd HH:mm}", tr.fechaReporte)).Append("</td>");
                var asunto = tr.asunto.Length <= 128 ? tr.asunto : string.Format("{0}...", tr.asunto.Substring(0, 127));
                result.Append("<td>").Append(asunto).Append("</td>");

                result.Append("<td> <span id='icon-25' class='ticket' title='Creado'>").Append(tr.nombreEstadoReporte).Append("</span></td>");

                if (tr.idTipoReporte == 3)
                {
                    result.Append("<td> <a onclick='javascript:llenarDatosReporteERP(").Append(tr.idReporte).Append(", ").Append(tr.idTipoReporte).Append(");'><span id='icon-25' class='modificar' title='Modificar Incidencia'></span></a>   <a onclick='javascript:confirmarEliminadoERP(").Append(tr.idReporte).Append(");'><span id='icon-25' class='eliminar' title='Eliminar Incidencia'></span></a>  <a onclick='javascript:consultarReporteERP(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                }
                else
                {
                    result.Append("<td> <a onclick='javascript:llenarDatosReporte(").Append(tr.idReporte).Append(", ").Append(tr.idTipoReporte).Append(", ").Append(tr.idERPGrupo).Append(");'><span id='icon-25' class='modificar' title='Modificar Incidencia'></span></a>   <a onclick='javascript:confirmarEliminado(").Append(tr.idReporte).Append(");'><span id='icon-25' class='eliminar' title='Eliminar Incidencia'></span></a>  <a onclick='javascript:consultarReporte(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                }

                result.Append("</tr>");
            }

            result.Append("</tbody>");
            result.Append("<tfoot>");
            result.Append("<tr>");
            result.Append("<td class='foot shadow3 round-down2' colspan='8'>&nbsp;</td>");
            result.Append("</tr>");
            result.Append("</tfoot>");
            result.Append("</table>");
            result.Append("</fieldset>");

        }
        else
        {
            result.Append("<fieldset>");
            result.Append("<legend>Incidencias Generadas</legend>");
            result.Append("<br /> <div class='center bg-alert width60 clear'><span id='icon-47' class='warning blanco'>No se ha generado ninguna Incidencia</span></div> <br />");
            result.Append("</fieldset>");
        }

        return result.ToString();
    }

    /// <summary>
    /// Método para generar la Tabla de los reportes que se encuentran en soporte, o bien que se encuentran en el estatus 2(Asignado)
    /// </summary>
    /// <returns>Cadena, en la que se encuentra la estructura de la tabla</returns>
    public string getTblReportesEnSoporte(int idUsuario)
    {
        StringBuilder result = new StringBuilder();

        erpRH = new ERPManagementRHDataContext();

        var statusPermitidos = new Int32[] { 2,4,8,9 }; // En Proceso, En Correcciones, Avance, Detenido
  
        var reportes = from r in erp.tReporte
                       where r.idUsuario == idUsuario && statusPermitidos.Contains(r.idEstatusReporte)
                       select new
                       {
                           idReporte = r.idReporte,
                           folio = r.folio,
                           fechaReporte = r.fechaReporte,
                           tipoReporte = r.cTipoReporte.nombreTipoReporte,
                           nomGrupo = r.tERPGrupo.nomGrupo,
                           idSistema = r.idSistema,
                           idArea = r.idArea,
                           asunto = r.asunto,
                           nomSistema = r.cSistemas.nomenglatura.ToUpper(),
                           idEstatusReporte = r.idEstatusReporte,
                           nombreEstadoReporte = r.cEstatusReporte.nombreEstadoReporte,
                           idTipoReporte = r.idTipoReporte,
                           idERPGrupo = r.idERPGrupo
                       };

        var usuarios = (from u in erpRH.view_ValidarTipoUsuarioAreaGrupo where u.idUsuario == idUsuario select u).FirstOrDefault();

        if (reportes.ToList().Count() > 0)
        {
            result.Append("<fieldset>");
            result.Append("<legend>Incidencias Generadas</legend>");
            result.Append("<table id='tblReporteCreados' class='data_grid display'>");
            result.Append("<thead id='grid-head2'>");
            result.Append("<th>Folio</th>");
            result.Append("<th>Tipo Reporte</th>");
            result.Append("<th>Grupo</th>");
            result.Append("<th>Sistema/Área</th>");
            result.Append("<th>Fecha Reporte</th>");
            result.Append("<th>Asunto</th>");
            result.Append("<th>Estatus</th>");
            result.Append("<th>Acciones</th>");
            result.Append("</thead>");
            result.Append("<tbody id='grid-body'>");

            foreach (var tr in reportes)
            {
                result.Append("<tr>");
                result.Append("<td> <span class='hide'>").Append(String.Format("{0:yyyy/MM/dd HH:mm}", tr.fechaReporte)).Append(" </span> ").Append(tr.folio).Append("</td>");
                result.Append("<td>").Append(tr.tipoReporte).Append("</td>");
                result.Append("<td>").Append(tr.nomGrupo).Append("</td>");

                if (tr.idSistema != 0 && tr.idArea == 0)
                {
                    result.Append("<td>").Append(tr.nomSistema).Append("</td>");
                }
                
                if (tr.idSistema == null || tr.idArea == null)
                {
                    result.Append("<td>No Aplica</td>");
                }
                
                if (tr.idSistema.Equals(0) && tr.idArea != null)
                {
                    //var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                    //                     where vValUser.idUsuario == tr.idUsuario
                    //                     select vValUser).FirstOrDefault();

                    result.Append("<td>").Append(usuarios.area).Append("</td>");
                }

                result.Append("<td>").Append(String.Format("{0:yyyy/MM/dd HH:mm}", tr.fechaReporte)).Append("</td>");
                var asunto = tr.asunto.Length <= 128 ? tr.asunto : string.Format("{0}...", tr.asunto.Substring(0, 127));
                result.Append("<td>").Append(asunto).Append("</td>");

                switch (tr.idEstatusReporte)
                {
                    case 2: //Proceso
                        result.Append("<td> <span id='icon-25' class='proceso verde' title='Generado'>").Append(tr.nombreEstadoReporte).Append("</span></td>");
                        break;
                    case 4: //Correcciones
                        result.Append("<td> <span id='icon-25' class='procesos' title='Generado'>").Append(tr.nombreEstadoReporte).Append("</span></td>");
                        break;
                    case 8: //Avance
                        result.Append("<td> <span id='icon-25' class='proceso verde' title='Generado'>").Append(tr.nombreEstadoReporte).Append("</span></td>");
                        break;
                    case 9: //Detenido
                        result.Append("<td> <span id='icon-25' class='proceso rojo' title='Generado'>").Append(tr.nombreEstadoReporte).Append("</span></td>");
                        break;
                }                

                if (tr.idTipoReporte == 3)
                {
                    result.Append("<td> <a onclick='javascript:consultarReporteERP(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                }
                else
                {
                    result.Append("<td> <a onclick='javascript:consultarReporte(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                }

                result.Append("</tr>");
            }

            result.Append("</tbody>");
            result.Append("<tfoot>");
            result.Append("<tr>");
            result.Append("<td class='foot shadow3 round-down2' colspan='8'>&nbsp;</td>");
            result.Append("</tr>");
            result.Append("</tfoot>");
            result.Append("</table>");
            result.Append("</fieldset>");
        }
        else
        {
            result.Append("<fieldset>");
            result.Append("<legend>Incidencias Asignadas</legend>");
            result.Append("<br /> <div class='center bg-alert width60 clear'><span id='icon-47' class='warning blanco'>Ninguna Incidencia ha sido asignada</span></div> <br />");
            result.Append("</fieldset>");
        }

        return result.ToString();
    }

    /// <summary>
    /// Método para generar la Tabla de los reportes que se encuentran en el estatus 3(Por Validar)
    /// </summary>
    /// <returns>Estructura de la tabla de las incidencias por Validar</returns>
    public string getTblReportesValidar(int idUsuario)
    {
        StringBuilder result = new StringBuilder();

        erpRH = new ERPManagementRHDataContext();

        var reportes = from r in erp.tReporte
                       where r.idUsuario == idUsuario && r.idEstatusReporte == 3 //Por Validar
                       select new
                       {
                           idReporte = r.idReporte,
                           folio = r.folio,
                           fechaReporte = r.fechaReporte,
                           tipoReporte = r.cTipoReporte.nombreTipoReporte,
                           nomGrupo = r.tERPGrupo.nomGrupo,
                           idSistema = r.idSistema,
                           idArea = r.idArea,
                           asunto = r.asunto,
                           nomSistema = r.cSistemas.nomenglatura.ToUpper(),
                           idEstatusReporte = r.idEstatusReporte,
                           nombreEstadoReporte = r.cEstatusReporte.nombreEstadoReporte,
                           idTipoReporte = r.idTipoReporte,
                           idERPGrupo = r.idERPGrupo
                       };

        var usuarios = (from u in erpRH.view_ValidarTipoUsuarioAreaGrupo where u.idUsuario == idUsuario select u).FirstOrDefault();

        if (reportes.ToList().Count() > 0)
        {
            result.Append("<fieldset>");
            result.Append("<legend>Incidencias Generadas</legend>");
            result.Append("<table id='tblReporteCreados' class='data_grid display'>");
            result.Append("<thead id='grid-head2'>");
            result.Append("<th>Folio</th>");
            result.Append("<th>Tipo Reporte</th>");
            result.Append("<th>Grupo</th>");
            result.Append("<th>Sistema/Área</th>");
            result.Append("<th>Fecha Reporte</th>");
            result.Append("<th>Asunto</th>");
            result.Append("<th>Estatus</th>");
            result.Append("<th>Acciones</th>");
            result.Append("</thead>");
            result.Append("<tbody id='grid-body'>");

            foreach (var tr in reportes)
            {
                result.Append("<tr>");
                result.Append("<td> <span class='hide'>").Append(String.Format("{0:yyyy/MM/dd H:mm:ss}", tr.fechaReporte)).Append(" </span> ").Append(tr.folio).Append("</td>");
                result.Append("<td>").Append(tr.tipoReporte).Append("</td>");
                result.Append("<td>").Append(tr.nomGrupo).Append("</td>");

                if (tr.idSistema != 0 && tr.idArea == 0)
                {
                    result.Append("<td>").Append(tr.nomSistema).Append("</td>");
                }

                if (tr.idSistema == null || tr.idArea == null)
                {
                    result.Append("<td>No Aplica</td>");
                }

                if (tr.idSistema.Equals(0) && tr.idArea != null)
                {
                    //var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                    //                     where vValUser.idUsuario == tr.idUsuario
                    //                     select vValUser).FirstOrDefault();

                    result.Append("<td>").Append(usuarios.area).Append("</td>");
                }

                result.Append("<td>").Append(String.Format("{0:yyyy/MM/dd HH:mm}", tr.fechaReporte)).Append("</td>");
                var asunto = tr.asunto.Length <= 128 ? tr.asunto : string.Format("{0}...", tr.asunto.Substring(0, 127));
                result.Append("<td>").Append(asunto).Append("</td>");

                result.Append("<td> <span id='icon-25' class='validar' title='Por Validar'>").Append(tr.nombreEstadoReporte).Append("</span></td>");

                if (tr.idTipoReporte == 3)
                {
                    result.Append("<td> <a onclick='javascript:consultarReporteERP(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                }
                else
                {
                    result.Append("<td> <a onclick='javascript:consultarReporte(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                }

                result.Append("</tr>");
            }

            result.Append("</tbody>");
            result.Append("<tfoot>");
            result.Append("<tr>");
            result.Append("<td class='foot shadow3 round-down2' colspan='8'>&nbsp;</td>");
            result.Append("</tr>");
            result.Append("</tfoot>");
            result.Append("</table>");
            result.Append("</fieldset>");
        }
        else
        {
            result.Append("<fieldset>");
            result.Append("<legend>Incidencias Por Validar</legend>");
            result.Append("<br /> <div class='center bg-alert width60 clear'><span id='icon-47' class='warning blanco'>Ninguna Incidencia se encuentra por validar</span></div> <br />");
            result.Append("</fieldset>");
        }

        return result.ToString();
    }

    /// <summary>
    /// Método para generar la Tabla de los reportes que ya fueron terminados y se encuentran en el estatus 5 (Terminado)
    /// </summary>
    /// <returns>Cadena, en la que se encuentra la estructura de la tabla</returns>
    public string getTblReportesTerminados(int idUsuario)
    {
        StringBuilder result = new StringBuilder();

        erpRH = new ERPManagementRHDataContext();

        var statusPermitidos = new Int32[] { 5, 7 }; // Terminado, Rechazado

        var reportes = from r in erp.tReporte
                       where r.idUsuario == idUsuario && statusPermitidos.Contains(r.idEstatusReporte)
                       select new
                       {
                           idReporte = r.idReporte,
                           folio = r.folio,
                           fechaReporte = r.fechaReporte,
                           tipoReporte = r.cTipoReporte.nombreTipoReporte,
                           nomGrupo = r.tERPGrupo.nomGrupo,
                           idSistema = r.idSistema,
                           idArea = r.idArea,
                           asunto = r.asunto,
                           nomSistema = r.cSistemas.nomenglatura.ToUpper(),
                           idEstatusReporte = r.idEstatusReporte,
                           nombreEstadoReporte = r.cEstatusReporte.nombreEstadoReporte,
                           idTipoReporte = r.idTipoReporte,
                           idERPGrupo = r.idERPGrupo
                       };

        var usuarios = (from u in erpRH.view_ValidarTipoUsuarioAreaGrupo where u.idUsuario == idUsuario select u).FirstOrDefault();

        if (reportes.ToList().Count() > 0)
        {
            result.Append("<fieldset>");
            result.Append("<legend>Incidencias Generadas</legend>");
            result.Append("<table id='tblReporteCreados' class='data_grid display'>");
            result.Append("<thead id='grid-head2'>");
            result.Append("<th>Folio</th>");
            result.Append("<th>Tipo Reporte</th>");
            result.Append("<th>Grupo</th>");
            result.Append("<th>Sistema/Área</th>");
            result.Append("<th>Fecha Reporte</th>");
            result.Append("<th>Asunto</th>");
            result.Append("<th>Estatus</th>");
            result.Append("<th>Acciones</th>");
            result.Append("</thead>");
            result.Append("<tbody id='grid-body'>");

            foreach (var tr in reportes)
            {
                result.Append("<tr>");
                result.Append("<td> <span class='hide'>").Append(String.Format("{0:yyyy/MM/dd H:mm:ss}", tr.fechaReporte)).Append(" </span> ").Append(tr.folio).Append("</td>");
                result.Append("<td>").Append(tr.tipoReporte).Append("</td>");
                result.Append("<td>").Append(tr.nomGrupo).Append("</td>");

                if (tr.idSistema != 0 && tr.idArea == 0)
                {
                    result.Append("<td>").Append(tr.nomSistema).Append("</td>");
                }

                if (tr.idSistema == null || tr.idArea == null)
                {
                    result.Append("<td>No Aplica</td>");
                }

                if (tr.idSistema.Equals(0) && tr.idArea != null)
                {
                    //var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                    //                     where vValUser.idUsuario == tr.idUsuario
                    //                     select vValUser).FirstOrDefault();

                    result.Append("<td>").Append(usuarios.area).Append("</td>");
                }

                result.Append("<td>").Append(String.Format("{0:yyyy/MM/dd HH:mm}", tr.fechaReporte)).Append("</td>");
                var asunto = tr.asunto.Length <= 128 ? tr.asunto : string.Format("{0}...", tr.asunto.Substring(0, 127));
                result.Append("<td>").Append(asunto).Append("</td>");

                if (tr.idEstatusReporte == 5)
                {
                    result.Append("<td> <span id='icon-25' class='solicitud_aprobada verde' title='Terminado'>").Append(tr.nombreEstadoReporte).Append("</span></td>");
                }
                else if (tr.idEstatusReporte == 7)
                {
                    result.Append("<td> <span id='icon-25' class='rechazar rojo' title='Rechazado'>").Append(tr.nombreEstadoReporte).Append("</span></td>");
                }                

                if (tr.idTipoReporte == 3)
                {
                    result.Append("<td> <a onclick='javascript:consultarReporteERP(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                }
                else
                {
                    result.Append("<td> <a onclick='javascript:consultarReporte(").Append(tr.idReporte).Append(");'><span id='icon-25' class='consultar' title='Consultar Incidencia'></span></a></td>");
                }

                result.Append("</tr>");
            }

            result.Append("</tbody>");
            result.Append("<tfoot>");
            result.Append("<tr>");
            result.Append("<td class='foot shadow3 round-down2' colspan='8'>&nbsp;</td>");
            result.Append("</tr>");
            result.Append("</tfoot>");
            result.Append("</table>");
            result.Append("</fieldset>");
        }
        else
        {
            result.Append("<fieldset>");
            result.Append("<legend>Incidencias Terminadas</legend>");
            result.Append("<br /> <div class='center bg-alert width60 clear'><span id='icon-47' class='warning blanco'>Ninguna Incidencia ha sido terminada</span></div> <br />");
            result.Append("</fieldset>");
        }

        return result.ToString();
    }


    /// <summary>
    /// Método para generar la Tabla del detalle de cada reporte, para ver el proceso de avance del mismo, es decir, para ver que
    ///  se ha hecho con el mismo, las fechas en las que se ha realizado y el usuario responsable de resolver o atender la petición
    /// </summary>
    /// <returns>Cadena, en la que se encuentra la estructura de la tabla</returns>
    public string getDetalleAccionesReporte(int idReporte, int idUsuario)
    {
        StringBuilder result = new StringBuilder();

        var detallesAccionesRep = (from vdar in erp.view_DetalleLogReporte
                                   where vdar.idReporte == idReporte && vdar.idUsuario == idUsuario && vdar.idTipoResponsable == 1
                                   select new {
                                       idUsuarioResp = vdar.idUsuarioResp,
                                       nombreEstadoReporte = vdar.nombreEstadoReporte,
                                       idRespuestaReporte = vdar.idRespuestaReporte,
                                       fechaRespuesta = vdar.fechaRespuesta,
                                       comentario = vdar.comentario
                                   }).ToList();

        var folioReporte = (from tr in erp.tReporte
                            where tr.idReporte == idReporte
                            select tr.folio).Single();

        if (detallesAccionesRep.Count() > 0)
        {
            result.Append("<fieldset>")
            .Append("<legend>Incidencia: ").Append(folioReporte).Append("</legend>")
            .Append("<table id='tblDetalleAccionesR' class='data_grid display'>")
            .Append("<thead id='grid-head2'>")
            .Append("<th>Fecha</th>")
            .Append("<th>Estatus</th>")
            .Append("<th>Comentario</th>")
            .Append("<th>Responsable</th>")
            .Append("</thead>")
            .Append("<tbody id='grid-body'>");

            foreach (var vdlr in detallesAccionesRep.OrderByDescending(e => e.fechaRespuesta))
            {
                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                var nombreUsuario = cUsuarios.getUsuarioById(vdlr.idUsuarioResp.GetValueOrDefault()).Trim().Split(' ');

                result.Append("<tr>")
                .Append("<td>").Append(String.Format("{0:yyyy/MM/dd HH:mm}", vdlr.fechaRespuesta)).Append("</td>")
                .Append("<td>").Append(vdlr.nombreEstadoReporte).Append("</td>")
                .Append("<td>").Append(vdlr.comentario).Append("</td>")
                .Append("<td> <span class='hide'>").Append(vdlr.idRespuestaReporte).Append(" </span>").Append(nombreUsuario[0]).Append(" ").Append(nombreUsuario[1]).Append("</td>")               
                .Append("</tr>");
            }

            result.Append("</tbody>")
            .Append("<tfoot>")
            .Append("<tr>").Append("<td class='foot shadow3 round-down2' colspan='4'>&nbsp;</td>").Append("</tr>")
            .Append("</tfoot>")
            .Append("</table>")
            .Append("</fieldset>");

        }
        else
        {
            var incidencia = (from tr in erp.tReporte
                              where tr.idReporte == idReporte
                              select tr).Single();

            if (incidencia.idEstatusReporte == 7)
            {
                result.Append("<br /> <div class='bg-alert center width98 clear' style='background-color:#6f7071 !important;'><span id='icon-47' class='informacion blanco'>La Incidencia se encuentra en Lecciones Aprendidas</span></div><br />");
            }
            else
            {
                result.Append("<br /> <div class='center bg-alert width98 clear'><span id='icon-47' class='warning blanco'>La Incidencia aún no ha sido asignada</span></div><br />");
            }
        }
        return result.ToString();
    }


    /// <summary>
    /// Método para generar la Tabla del log en el que se reflejan las acciones que se han realizado
    /// </summary>
    /// <returns>Cadena, en la que se encuentra la estructura de la tabla.</returns>
    public string getTblLogReportes(int idUsuario)

    {


        string result = "";
        var lstLogReportes = (from logreportes in erp.tLogReporte
                              where logreportes.idUsuario == idUsuario
                              select logreportes).ToList();

        if (lstLogReportes.Count > 0)
        {
            ControllerTiposReportes ControllerTRep = new ControllerTiposReportes();


            result += "<fieldset>" +
                        "<legend>Log Incidencias</legend>" +
                      "<table id='tblLogReporte' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Folio Reporte</th>" +
                            "<th>Asunto</th>" +
                            "<th>Tipo Reporte</th>" +
                            "<th>Acción</th>" +
                            "<th>Fecha Acción</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            foreach (tLogReporte tlr in lstLogReportes)
            {



                result += "<tr>";
                result += "<td> <span class='hide'>" + tlr.fecha + " </span>" + tlr.tReporte.folio + "</td>";
                result += "<td>" + tlr.tReporte.asunto + "</td>";
                result += "<td>" + tlr.tReporte.cTipoReporte.nombreTipoReporte + "</td>";
                result += "<td>" + tlr.cAccion.accion + "</td>";
                result += "<td>" + String.Format("{0:MM/dd/yyyy}", tlr.fecha) + "</td>";
                result += "</tr>";
            }

            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='5'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>" +
                      "</fieldset>";

        }

        return result;
    }

    /// <summary>
    /// Método para generar los CheckBox de los sistemas de cada Grupo
    /// </summary>
    /// <returns>Estructura y nombre de los CheckBox</returns>
    /// 
    public string getSistemasERP(int idGrupo)
    {
        string result = "";
        var sistemasERP = new int[] { 1, 2, 3, 4, 10, 11, 18 };//Sin SGI 5

        var query = (from sistemas in erp.cSistemas
                     where sistemasERP.Contains(sistemas.idSistema)
                     select sistemas).ToList();

        if (query.Count > 0)
        {
            foreach (cSistemas cs in query)
            {

                var sistemasGrupo = (from sistem in erp.tERPGrupoSistema
                                     where sistem.idSistema == cs.idSistema && sistem.idERPGrupo == idGrupo && sistem.idEstatusERPGrupoSistema != 2
                                     select sistem).ToList();
                if (sistemasGrupo.Count() > 0)
                {
                    result += "<a href='javascript:;'> <label class='divBlancoSistemas' id=" + cs.idSistema + " style='background-color: #7FC44B !important; color:#FFFFFF !important;'> " + cs.nomenglatura + " </label> </a>";
                }
                else
                {
                    result += "<a href='#'> <label class='divBlancoSistemas'  id=" + cs.idSistema + "><input type='checkbox' id='chk" + cs.idSistema + "' class='sistemas'  onchange='javascript:seleccionado(" + cs.idSistema + ",this);' name='chkSistemas' value=" + cs.idSistema + " /> " + cs.nomenglatura + " </label> </a>";
                }
            }
        }
        else
        {
            result += "Cuenta con todos los sistemas";
        }
        return result;
    }

    /// <summary>
    /// Método para agregar un nuevo grupo a la base de datos
    /// </summary>
    /// <returns>True si se realizan todas las acciones de manera correcta, de lo contrario False</returns>
    public bool agregarGrupo(string nombreGrupo, int idUsuario)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        tLogGrupo tLogGrupo = new tLogGrupo();
        string fechaAccion = DateTime.Now.ToShortDateString();
        DateTime fechaAcc = Convert.ToDateTime(fechaAccion);
        string queryGrupoRH = "";

        var query = (from terpg in erp.tERPGrupo
                     where terpg.nomGrupo == nombreGrupo
                     select terpg.nomGrupo).FirstOrDefault();
        if (query == nombreGrupo)
        {
            return false;
        }
        else
        {
            insertarGrupoRH(nombreGrupo);

            queryGrupoRH = "select MAX(id_grupo) from DBSGRH.dbo.cGrupo";
            var idERPGrupoRH = sp.recuperaValor(queryGrupoRH);

            //Insert en tERPGrupo
            tERPGrupo.nomGrupo = nombreGrupo;
            tERPGrupo.urlERP = "";
            tERPGrupo.idGrupoRH = int.Parse(idERPGrupoRH);
            erp.tERPGrupo.InsertOnSubmit(tERPGrupo);
            erp.SubmitChanges();

            //id del grupo que se acaba de insertar en tERPGrupo
            var idERPGrupo = (from terpg in erp.tERPGrupo
                              select terpg.idERPGrupo).Max();

            //Insert en tERPGrupoSistema
            tERPGrupoSis.idERPGrupo = idERPGrupo;
            tERPGrupoSis.idSistema = 5;
            tERPGrupoSis.appCreada = 0;
            tERPGrupoSis.scriptCreado = 0;
            erp.tERPGrupoSistema.InsertOnSubmit(tERPGrupoSis);
            erp.SubmitChanges();

            //Insert en tLogGrupo
            tLogGrupo.idERPGrupo = idERPGrupo;
            tLogGrupo.idUsuario = idUsuario;
            tLogGrupo.fecha = fechaAcc;
            erp.tLogGrupo.InsertOnSubmit(tLogGrupo);
            erp.SubmitChanges();

            return true;
        }
    }

    /// <summary>
    /// Método para insertar un nuevo grupo en la Base de Datos de Recursos Humanos
    /// </summary>
    public string insertarGrupoRH(string nombreGrupo)
    {
        string resultado = "";
        storedProcedure sp = new storedProcedure();
        resultado = sp.insertarGrupoRH(nombreGrupo);
        return resultado;
    }

    public int obtenerIdGrupo(string nombreGrupo)
    {
        var idGrupo = (from terpg in erp.tERPGrupo
                       where terpg.nomGrupo == nombreGrupo
                       select terpg.idERPGrupo).FirstOrDefault();
        return idGrupo;
    }


    /// <summary>
    /// Método para agregar una ueva petición de ERP
    /// </summary>
    /// <returns>True si se realizan todas las acciones de manera correcta, de lo contrario False</returns>
    public bool agregarPeticion(string asunto, string descripcion, int idTipoReporte, int idPrioridad, string fechaPropuesta, int idERPGrupo, string sistemas, string nombreArchivo, int idUsuario)
    {
        try
        {
            string[] separators = { "," };
            string[] sistema = sistemas.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //Insert en tReporte
            string fechaReporte = DateTime.Now.ToShortDateString();
            DateTime fechaRep = Convert.ToDateTime(fechaReporte);
            DateTime fechaProp = Convert.ToDateTime(fechaPropuesta);
            reporte.asunto = asunto;
            reporte.descripcion = descripcion;
            reporte.idTipoReporte = idTipoReporte;
            reporte.idPrioridad = idPrioridad;
            reporte.idEstatusReporte = 1;
            reporte.fechaReporte = fechaRep;
            reporte.idUsuario = idUsuario;
            reporte.fechaPropuesta = fechaProp;
            reporte.idERPGrupo = idERPGrupo;
            reporte.evidencia = nombreArchivo;

            var incremento = (from trep in erp.tReporte
                              where trep.idTipoReporte == idTipoReporte
                              select trep.idReporte).Count();

            var claveTipo = (from ctr in erp.cTipoReportes
                             where ctr.idTipoReporte == idTipoReporte
                             select ctr).SingleOrDefault();

            reporte.folio = claveTipo.clave + "-" + fechaRep.Year + "-" + (incremento + 1);

            erp.tReporte.InsertOnSubmit(reporte);
            erp.SubmitChanges();

            var idReporte = (from trep in erp.tReporte
                             where trep.idTipoReporte == idTipoReporte
                             select trep.idReporte).Max();

            //Insert en tLogReporte
            string fechaAccion = DateTime.Now.ToShortDateString();
            DateTime fechaAcc = Convert.ToDateTime(fechaAccion);
            tLogReporte.idReporte = idReporte;
            tLogReporte.idUsuario = idUsuario;
            tLogReporte.fecha = fechaAcc;
            tLogReporte.idAccion = 1;

            erp.tLogReporte.InsertOnSubmit(tLogReporte);
            erp.SubmitChanges();


            foreach (string sist in sistema)
            {
                //Insert en tERPGrupoSistema 
                tERPGrupoSis.idERPGrupo = idERPGrupo;
                tERPGrupoSis.idSistema = int.Parse(sist);
                tERPGrupoSis.appCreada = 0;
                tERPGrupoSis.scriptCreado = 0;
                tERPGrupoSis.idReporte = idReporte;
                tERPGrupoSis.idEstatusERPGrupoSistema = 1;

                erp.tERPGrupoSistema.InsertOnSubmit(tERPGrupoSis);
                erp.SubmitChanges();

                tERPGrupoSis = new tERPGrupoSistema();
            }

            if (idTipoReporte == 3)
            {
                tLogEjecucionBD tle = new tLogEjecucionBD();
                tle.idReporte = idReporte;
                tle.idSistema = 0;
                tle.idUsuario = idUsuario;
                tle.idAccion = 7;
                tle.idEstatus = 5;
                tle.fechaEjecución = Convert.ToDateTime("2016-07-29 15:04:24.903");
                tle.fechaTermino = Convert.ToDateTime("2016-07-29 15:04:24.903");
                tle.comentario = "N/A";
                erp.tLogEjecucionBD.InsertOnSubmit(tle);
                erp.SubmitChanges();
            }

            enviarCorreoPeticionERP(idUsuario, idReporte);

            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    /// <summary>
    /// Método para obtener los detalles de una peticion de ERP.
    /// </summary>
    /// <returns>Objeto view_DetalleReporte, con la informacion de la peticion de ERP</returns>
    public view_DetalleReporte consultarReporteERP(int idReporte)
    {
        var detReporte = (from vdr in erp.view_DetalleReporte
                          where vdr.idReporte == idReporte
                          select vdr).SingleOrDefault();

        var sistemas = (from erpgs in erp.tERPGrupoSistema
                        where erpgs.idReporte == idReporte && erpgs.idEstatusERPGrupoSistema != 2
                        select erpgs).ToString();

        return detReporte;
    }

    /// <summary>
    /// Método para generar los CheckBox Seleccionados en un reporte de una peticón de ERP
    /// </summary>
    /// <returns>Estructura de los CheckBox Seleccionados en un Reporte</returns>
    public string getSistemasERPSeleccionados(int idReporte)
    {
        string result = "";
        var query = (from tr in erp.tReporte
                     where tr.idReporte == idReporte
                     select tr).FirstOrDefault();
        if (query.idEstatusReporte == 7)
        {
            var querySistemas = (from sistema in erp.tERPGrupoSistema
                                 where sistema.idReporte == idReporte && sistema.idEstatusERPGrupoSistema == 2
                                 select sistema).ToList();
            foreach (tERPGrupoSistema sist in querySistemas)
            {
                result += "<label class='divBlancoSistemas' id=" + sist.idSistema + " style='background-color: #145A7A !important; color:#FFFFFF !important;'>" + sist.cSistemas.nomenglatura + "</label>";
            }
        }
        else
        {
            var querySistemas = (from sistema in erp.tERPGrupoSistema
                                 where sistema.idReporte == idReporte && sistema.idEstatusERPGrupoSistema != 2
                                 select sistema).ToList();
            foreach (tERPGrupoSistema sist in querySistemas)
            {
                result += "<label class='divBlancoSistemas' id=" + sist.idSistema + " style='background-color: #145A7A !important; color:#FFFFFF !important;'>" + sist.cSistemas.nomenglatura + "</label>";
            }
        }



        return result;
    }

    /// <summary>
    /// Método para generar los CheckBox de los sistemas Seleccionados de cada peticion de ERP, para que puedan ser modificados posteriormente.
    /// </summary>
    /// <returns>Estructura de los CheckBox </returns>
    public string getSistemasERPModificar(int idReporte)
    {
        string result = "<label class='frmReporte'>Sistemas: </label>" +
                "<span id='alertTipoReporte' class='requerido-reporte' style='display: none;'>Seleccione una opción</span><br>";
        var sistemasERP = new int[] { 1, 2, 3, 4, 10, 11 };//Sin SGI 5
        var querySistemas = (from sistema in erp.tERPGrupoSistema
                             where sistema.idReporte == idReporte && sistema.idEstatusERPGrupoSistema != 2
                             select sistema).ToList();

        var grupo = (from sistema in erp.tERPGrupoSistema
                     where sistema.idReporte == idReporte && sistema.idEstatusERPGrupoSistema != 2
                     select sistema.idERPGrupo).First();

        var querySinS = (from cs in erp.cSistemas
                         where sistemasERP.Contains(cs.idSistema) &&
                            !(from terpg in erp.tERPGrupoSistema
                              where terpg.idERPGrupo == grupo && terpg.idEstatusERPGrupoSistema != 2
                              select terpg.idSistema).Contains(cs.idSistema)
                         select cs).ToList();

        foreach (tERPGrupoSistema sist in querySistemas)
        {
            result += "<a href='#'> <label class='divBlancoSistemas'  id=" + sist.idSistema + " style='background-color: #7FC44B !important; color:#FFFFFF !important;'><input type='checkbox' id='chk" + sist.idSistema + "' class='sistemas'  onchange='javascript:seleccionado(" + sist.idSistema + ",this);' name='chkSistemas' value=" + sist.idSistema + " checked='checked'  /> " + sist.cSistemas.nomenglatura + " </label> </a>";
        }

        foreach (cSistemas sistemas in querySinS)
        {
            result += "<a href='#'> <label class='divBlancoSistemas'  id=" + sistemas.idSistema + " ><input type='checkbox' id='chk" + sistemas.idSistema + "' class='sistemas'  onchange='javascript:seleccionado(" + sistemas.idSistema + ",this);' name='chkSistemas' value=" + sistemas.idSistema + " /> " + sistemas.nomenglatura + " </label> </a>";
        }
        return result;
    }

    /// <summary>
    /// Método para modificar una petición de ERP.
    /// </summary>
    /// <returns>True si la petición de ERP es modificada correctamente, de lo contrario False.</returns>
    public bool modificaReporteERP(int idReporte, string asunto, string descripcion, int idTipoReporte, int idPrioridad, int idERPGrupo, string fechaPropuesta, string sistemas, string nombreArchivo, string nomGrupo, int idUsuario)
    {
        try
        {
            string[] separators = { "," };
            string[] sistema = sistemas.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var reporte = (from tr in erp.tReporte
                           where tr.idReporte == idReporte
                           select tr).FirstOrDefault();

            var grupoSis = (from terpg in erp.tERPGrupoSistema
                            where terpg.idReporte == idReporte && terpg.idEstatusERPGrupoSistema != 2
                            select terpg).ToList();

            var grupo = (from terpg in erp.tERPGrupoSistema
                         where terpg.idERPGrupo == idERPGrupo && terpg.scriptCreado != 0 && terpg.idEstatusERPGrupoSistema != 2
                         select terpg).ToList();

            if (grupo.Count() < 1)
            {
                var idGrupoRH = (from terpg in erp.tERPGrupo
                                 where terpg.idERPGrupo == idERPGrupo
                                 select terpg.idGrupoRH).FirstOrDefault();
                //Update de Grupo RH
                modificaGrupoRH(int.Parse(idGrupoRH.ToString()), nomGrupo);
                //Update en tERPGrupo
                tERPGrupo tERPGrupo = erp.tERPGrupo.Single(terpg => terpg.idERPGrupo == idERPGrupo);
                tERPGrupo.nomGrupo = nomGrupo;
                erp.SubmitChanges();
            }

            //Update en tReporte
            DateTime fechaProp = Convert.ToDateTime(fechaPropuesta);
            reporte.asunto = asunto;
            reporte.descripcion = descripcion;
            reporte.idTipoReporte = idTipoReporte;
            reporte.idPrioridad = idPrioridad;
            reporte.fechaPropuesta = fechaProp;
            reporte.idERPGrupo = idERPGrupo;
            reporte.evidencia = nombreArchivo;

            erp.SubmitChanges();

            //Insert en tLogReporte
            string fechaAccion = DateTime.Now.ToShortDateString();
            DateTime fechaAcc = Convert.ToDateTime(fechaAccion);
            tLogReporte.idReporte = idReporte;
            tLogReporte.idUsuario = idUsuario;
            tLogReporte.fecha = fechaAcc;
            tLogReporte.idAccion = 3;

            erp.tLogReporte.InsertOnSubmit(tLogReporte);
            erp.SubmitChanges();

            if (sistema.Count() > grupoSis.Count())
            {
                foreach (string sist in sistema)
                {
                    var query = (from terpgs in erp.tERPGrupoSistema
                                 where terpgs.idSistema == int.Parse(sist) && terpgs.idReporte == idReporte && terpgs.idEstatusERPGrupoSistema != 2
                                 select terpgs).FirstOrDefault();
                    if (query == null)
                    {
                        //Insert en tERPGrupoSistema 
                        tERPGrupoSis.idERPGrupo = idERPGrupo;
                        tERPGrupoSis.idSistema = int.Parse(sist);
                        tERPGrupoSis.appCreada = 0;
                        tERPGrupoSis.scriptCreado = 0;
                        tERPGrupoSis.idReporte = idReporte;
                        tERPGrupoSis.idEstatusERPGrupoSistema = 1;

                        erp.tERPGrupoSistema.InsertOnSubmit(tERPGrupoSis);
                        erp.SubmitChanges();

                        tERPGrupoSis = new tERPGrupoSistema();
                    }
                    //else
                    //{
                    //    tERPGrupoSis.appCreada = 0;
                    //    erp.SubmitChanges();
                    //}
                }
            }
            else
            {
                foreach (tERPGrupoSistema sist in grupoSis)
                {
                    erp.tERPGrupoSistema.DeleteOnSubmit(sist);
                }
                erp.SubmitChanges();

                foreach (string sist in sistema)
                {
                    //Insert en tERPGrupoSistema 
                    tERPGrupoSis.idERPGrupo = idERPGrupo;
                    tERPGrupoSis.idSistema = int.Parse(sist);
                    tERPGrupoSis.appCreada = 0;
                    tERPGrupoSis.scriptCreado = 0;
                    tERPGrupoSis.idReporte = idReporte;
                    tERPGrupoSis.idEstatusERPGrupoSistema = 1;

                    erp.tERPGrupoSistema.InsertOnSubmit(tERPGrupoSis);
                    erp.SubmitChanges();

                    tERPGrupoSis = new tERPGrupoSistema();
                }

            }

            enviarCorreoModificarPeticionERP(idUsuario, idReporte);
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public string modificaGrupoRH(int idGrupo, string nombreGrupo)
    {
        string resultado = "";
        storedProcedure sp = new storedProcedure();
        resultado = sp.modificaGrupoRH(idGrupo, nombreGrupo);
        return resultado;
    }

    /// <summary>
    /// Método para eliminar una petición de ERP
    /// </summary>
    /// <returns>True si las acciones se llevan a cabo de manera exitosa, de lo contrario false.</returns>
    public bool eliminarReporteERP(int idReporte, int idUsuario)
    {
        try
        {
            var reporte = (from tr in erp.tReporte
                           where tr.idReporte == idReporte
                           select tr).FirstOrDefault();

            reporte.idEstatusReporte = 6;

            erp.SubmitChanges();

            var registros = (from terpg in erp.tERPGrupoSistema
                             where terpg.idReporte == idReporte && terpg.idEstatusERPGrupoSistema != 2
                             select terpg).ToList();

            foreach (tERPGrupoSistema sistemas in registros)
            {
                erp.tERPGrupoSistema.DeleteOnSubmit(sistemas);
            }

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
    /// Método para enviar correo electrónico cuando se genera una nueva incidencia
    /// </summary>
    /// <returns>True si el correo se envia de manera exitosa, de lo contrario false</returns>
    public bool enviarCorreo(int idUsuario, int idReporte, int idTipoReporte)
    {
        utileria = new Utileria();
        string asunto = "";
        string cuerpo = "";
        string correo = "";
        // string logoRuta = HttpContext.Current.Server.MapPath("..\\..\\images\\logos\\logoNAD.png");
        string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
        string sArchivoAdjunto = "";

        var query = (from reportes in erp.tReporte
                     where reportes.idReporte == idReporte
                     select reportes).First();
        erpRH = new ERPManagementRHDataContext();
        var validacionUsua = (from vValUserV in erpRH.view_ValidarTipoUsuarioAreaGrupo
                              where vValUserV.idUsuario == idUsuario //&& vValUserV.idTipoUsuario !=null && vValUserV.area !=null && vValUserV.grupo !=null
                              select vValUserV).FirstOrDefault();

        try
        {
            if (validacionUsua.idTipoUsuario == 1)
            {
                string folio = query.folio;
                string fechaPropuesta = String.Format("{0:MM/dd/yyyy}", query.fechaPropuesta);
                string asuntoReporte = query.asunto;
                string descripcion = query.descripcion;
                string prioridad = query.cPrioridadReporte.nombrePrioridad;
                string grupo = query.tERPGrupo.nomGrupo;
                string sistema = query.cSistemas.nomSistema;
                string tipoReporte = query.cTipoReporte.nombreTipoReporte;
                string nombreArchivo = query.evidencia;
                string fechaReporte = String.Format("{0:MM/dd/yyyy}", query.fechaReporte);

                if (nombreArchivo != "")
                {
                    sRutaFinal += "Configuracion\\Reportes\\Evidencia\\" + nombreArchivo; //archivo = HttpContext.Current.Server.MapPath("Evidencia\\" + nombreArchivo);
                   // sRutaFinal = sRutaFinal.Replace(" ", "-");
                    }
                if (nombreArchivo != "")
                {
                    sArchivoAdjunto = "<tr> <td   style='border:none;' colspan='12' > <center><h4>" +
                                                   "<br/><label>Para ver  archivo adjunto haz clic en la liga  --> "
                                                   + "<a  href=\"" + sRutaFinal + "\"  target='_new'>Descargar Archivo</a></label><label></label></h4>" +
                                          "</td> </tr>";

                }

                storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
                //Obtener los idUsuarios que tiene el TipoIncidencia del reporte
                string queryUsuariosSoporte = "select idUsuario from tUsuarioTipoIncidencia where idTipoReporte =" + query.idTipoReporte;
                List<string> lstUsuarioTipo = sp.recuperaRegistros(queryUsuariosSoporte);

                //Obtiene los Usuarios para enviar correo
                var usuariosSoporte = (from soporte in erp.vUsuariosERPMGrupos
                                       where soporte.idRolERPM == 3 && soporte.idTipoCorreoElectronico == 1 && lstUsuarioTipo.Contains(soporte.idEmpleado)
                                       select soporte).ToList();

                //var usuariosSoporte = (from soporte in erp.vUsuariosERPMGrupos
                //                       where soporte.idRolERPM == 3 && soporte.idTipoCorreoElectronico == 1
                //                       select soporte).ToList();

                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                asunto = asuntoReporte;
                #region body
                cuerpo = "<h4 style='color:black'>Una nueva incidencia ha sido generada por: " + cUsuarios.getUsuarioById(idUsuario) + "</h3><br />";

                cuerpo += "<center>" +
                            "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                                "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                    "<td style='border:none;' colspan='6' align='right' >" +
                                    "<font size='4.5em'> <b> Grupo: " + grupo + " </b> </font> " +
                                    "</td>" +
                                "</tr>" +
                                "<tr >" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                    "<td colspan='2' > " + folio + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Prioridad</b></td>" +
                                    "<td colspan='2' > " + prioridad + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Tipo Reporte</b></td>" +
                                    "<td colspan='2' > " + tipoReporte + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Sistema/Área  </b> </td>	" +
                                    "<td colspan='2'> " + sistema + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Reporte </b></td>" +
                                    "<td colspan='2' > " + fechaReporte + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Propuesta </b></td>" +
                                    "<td colspan='2' > " + fechaPropuesta + " </td>" +
                                "</tr>" +
                                "<tr >" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                    "<td colspan='10'> " + asuntoReporte + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Descripción </b> </td>" +
                                    "<td colspan='10'> " + descripcion + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                "</tr>" +
                                   sArchivoAdjunto +
                            //"<tr>" +
                            //    "<td style='border:none;' colspan='12' > <center><h4>" +
                            //        "<br/><label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver mas detalles del reporte</label></h4>" +
                            //    "</td>" +
                            //"</tr>" +
                            "</table>" +
                           
                        "</center>";
                #endregion

                foreach (vUsuariosERPMGrupo vUs in usuariosSoporte)
                {
                    correo += "" + vUs.correoElectronico + ";";
                }
                if (utileria.sendMail(correo, asunto, cuerpo, 2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (validacionUsua.idTipoUsuario == 2)
            {
                string folio = query.folio;
                string fechaPropuesta = String.Format("{0:MM/dd/yyyy}", query.fechaPropuesta);
                string asuntoReporte = query.asunto;
                string descripcion = query.descripcion;
                string prioridad = query.cPrioridadReporte.nombrePrioridad;
                string grupo = query.tERPGrupo.nomGrupo;
                string tipoReporte = query.cTipoReporte.nombreTipoReporte;
                string nombreArchivo = query.evidencia;

                if (nombreArchivo != "")
                {
                    sRutaFinal += "Configuracion\\Reportes\\Evidencia\\" + nombreArchivo; //archivo = HttpContext.Current.Server.MapPath("Evidencia\\" + nombreArchivo);
                   // sRutaFinal = sRutaFinal.Replace(" ", "-");
                }
                if (nombreArchivo != "")
                {
                    sArchivoAdjunto = "<tr> <td   style='border:none;' colspan='12' > <center><h4>" +
                                                "<br/><label>Para ver  archivo adjunto haz clic en la liga  --> "
                                                + "<a  href=\"" + sRutaFinal + "\"  target='_new'>Descargar Archivo</a></label><label></label></h4>" +
                                       "</td> </tr>";

                }
                string fechaReporte = String.Format("{0:MM/dd/yyyy}", query.fechaReporte);

                //var usuariosSoporte = (from soporte in erp.view_correoPersonalExternos
                //                       where soporte.idGrupo == query.idERPGrupo && soporte.idSoporte == idTipoReporte
                //                       select soporte).ToList();

                //var usuariosSoporte = (from soporte in erp.vUsuariosERPMGrupos
                //                       where soporte.idRolERPM == 3 && soporte.idTipoCorreoElectronico == 1
                //                       select soporte).ToList();

                storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
                //Obtener los idUsuarios que tiene el TipoIncidencia del reporte
                string queryUsuariosSoporte = "select idUsuario from tUsuarioTipoIncidencia where idTipoReporte =" + query.idTipoReporte;
                List<string> lstUsuarioTipo = sp.recuperaRegistros(queryUsuariosSoporte);

                //Obtiene los Usuarios para enviar correo
                var usuariosSoporte = (from soporte in erp.vUsuariosERPMGrupos
                                       where soporte.idRolERPM == 3 && soporte.idTipoCorreoElectronico == 1 && lstUsuarioTipo.Contains(soporte.idEmpleado)
                                       select soporte).ToList();

                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                asunto = asuntoReporte;
                cuerpo = "<h4 style='color:black'>Una nueva incidencia ha sido generada por: " + cUsuarios.getUsuarioById(idUsuario) + "</h3><br />";

                cuerpo += "<center>" +
                            "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                                "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                    "<td style='border:none;' colspan='6' align='right' >" +
                                    "<font size='4.5em'> <b> Grupo: " + grupo + " </b> </font> " +
                                    "</td>" +
                                "</tr>" +
                                "<tr >" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                    "<td colspan='2' > " + folio + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Prioridad</b></td>" +
                                    "<td colspan='2' > " + prioridad + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Tipo Reporte</b></td>" +
                                    "<td colspan='2' > " + tipoReporte + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Sistema/Área  </b> </td>	" +
                                    "<td colspan='2'> " + validacionUsua.area + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Reporte </b></td>" +
                                    "<td colspan='2' > " + fechaReporte + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Propuesta </b></td>" +
                                    "<td colspan='2' > " + fechaPropuesta + " </td>" +
                                "</tr>" +
                                "<tr >" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                    "<td colspan='10'> " + asuntoReporte + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Descripción </b> </td>" +
                                    "<td colspan='10'> " + descripcion + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                "</tr>" +
                                sArchivoAdjunto +
                            //"<tr>" +
                            //    "<td style='border:none;' colspan='12' > <center><h4>" +
                            //        "<br/><label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver mas detalles del reporte</label></h4>" +
                            //    "</td>" +
                            //"</tr>" +
                            "</table>" +
                           
                        "</center>";


                foreach (vUsuariosERPMGrupo vUs in usuariosSoporte)
                {
                    correo += "" + vUs.correoElectronico + ";";
                }
                if (utileria.sendMail(correo, asunto, cuerpo, 2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
            throw;
        }
    }

    /// <summary>
    /// Método para enviar correo electrónico cuando se lleva a cabo la modificación de una incidencia
    /// </summary>
    /// <returns>True si es enviado correctamente, por el contrario False</returns>
    public bool enviarCorreoModifica(int idUsuario, int idReporte, int idTipoReporte)
    {
        utileria = new Utileria();
        string asunto = "";
        string cuerpo = "";
        string correo = "";
        //string logoRuta = HttpContext.Current.Server.MapPath("..\\..\\images\\logos\\logoNAD.png");
        string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
        string sArchivoAdjunto = "";

        var query = (from reportes in erp.tReporte
                     where reportes.idReporte == idReporte
                     select reportes).First();
        erpRH = new ERPManagementRHDataContext();
        var validacionUsua = (from vValUserV in erpRH.view_ValidarTipoUsuarioAreaGrupo
                              where vValUserV.idUsuario == idUsuario //&& vValUserV.idTipoUsuario !=null && vValUserV.area !=null && vValUserV.grupo !=null
                              select vValUserV).FirstOrDefault();

        try
        {
            if (validacionUsua.idTipoUsuario == 2)
            {
                string folio = query.folio;
                string fechaPropuesta = String.Format("{0:MM/dd/yyyy}", query.fechaPropuesta);
                string asuntoReporte = query.asunto;
                string descripcion = query.descripcion;
                string prioridad = query.cPrioridadReporte.nombrePrioridad;
                string tipoReporte = query.cTipoReporte.nombreTipoReporte;
                string fechaReporte = String.Format("{0:MM/dd/yyyy}", query.fechaReporte);
                var nombreArchivo = query.evidencia;

             
                if (nombreArchivo != "")
                {
                    sRutaFinal += "Configuracion\\Reportes\\Evidencia\\" + nombreArchivo; //archivo = HttpContext.Current.Server.MapPath("Evidencia\\" + nombreArchivo);
                   // sRutaFinal = sRutaFinal.Replace(" ", "-");
                }
                if (nombreArchivo != "")
                {
                  sArchivoAdjunto= "<td   style='border:none;' colspan='12' > <center><h4>" +
                                              "<br/><label>Para ver  archivo adjunto haz clic en la liga  --> "
                                              + "<a  href=\"" + sRutaFinal + "\"  target='_new'>Descargar Archivo</a></label><label></label></h4>" +
                                         "</td>";

                }

                var usuariosSoporte = (from soporte in erp.view_correoPersonalExternos
                                       where soporte.idGrupo == query.idERPGrupo && soporte.idSoporte == idTipoReporte
                                       select soporte).ToList();

                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                asunto = asuntoReporte;
                cuerpo = "<h4 style='color:black'>Una incidencia ha sido modificada por: " + cUsuarios.getUsuarioById(idUsuario) + "</h3><br />";
                #region 2
                cuerpo += "<center>" +
                            "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                                "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                    "<td style='border:none;' colspan='6' align='right' >" +
                                    "<font size='4.5em'> <b> Grupo: " + validacionUsua.grupo + " </b> </font> " +
                                    "</td>" +
                                "</tr>" +
                                "<tr >" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                    "<td colspan='2' > " + folio + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Prioridad</b></td>" +
                                    "<td colspan='2' > " + prioridad + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Tipo Reporte</b></td>" +
                                    "<td colspan='2' > " + tipoReporte + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Área </b> </td>	" +
                                    "<td colspan='2'> " + validacionUsua.area + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Reporte </b></td>" +
                                    "<td colspan='2' > " + fechaReporte + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Propuesta </b></td>" +
                                    "<td colspan='2' > " + fechaPropuesta + " </td>" +
                                "</tr>" +
                                "<tr >" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                    "<td colspan='10'> " + asuntoReporte + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Descripción </b> </td>" +
                                    "<td colspan='10'> " + descripcion + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                "</tr>" +
                                sArchivoAdjunto+
                            //"<tr>" +
                            //    "<td style='border:none;' colspan='12' > <center><h4>" +
                            //        "<br/><label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver mas detalles del reporte</label></h4>" +
                            //    "</td>" +
                            //"</tr>" +
                            "</table>" +
                        
                        "</center>";
                #endregion
                foreach (view_correoPersonalExternos vUs in usuariosSoporte)
                {
                    correo += "" + vUs.correoElectrONico + ";";
                }

          
     
                if (utileria.sendMail(correo, asunto, cuerpo, 2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (validacionUsua.idTipoUsuario == 1)
            {
                string folio = query.folio;
                string fechaPropuesta = String.Format("{0:MM/dd/yyyy}", query.fechaPropuesta);
                string asuntoReporte = query.asunto;
                string descripcion = query.descripcion;
                string grupo = query.tERPGrupo.nomGrupo;
                string prioridad = query.cPrioridadReporte.nombrePrioridad;
                string sistema = query.cSistemas.nomSistema;
                string tipoReporte = query.cTipoReporte.nombreTipoReporte;
                string fechaReporte = String.Format("{0:MM/dd/yyyy}", query.fechaReporte);
                var nombreArchivo = query.evidencia;

                //var usuariosSoporte = (from soporte in erp.vUsuariosERPMGrupos
                //                       where soporte.idRolERPM == 3 && soporte.idTipoCorreoElectronico == 1
                //                       select soporte).ToList();

                storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
                //Obtener los idUsuarios que tiene el TipoIncidencia del reporte
                string queryUsuariosSoporte = "select idUsuario from tUsuarioTipoIncidencia where idTipoReporte =" + query.idTipoReporte;
                List<string> lstUsuarioTipo = sp.recuperaRegistros(queryUsuariosSoporte);

                //Obtiene los Usuarios para enviar correo
                var usuariosSoporte = (from soporte in erp.vUsuariosERPMGrupos
                                       where soporte.idRolERPM == 3 && soporte.idTipoCorreoElectronico == 1 && lstUsuarioTipo.Contains(soporte.idEmpleado)
                                       select soporte).ToList();

                if (nombreArchivo != "")
                {
                    sRutaFinal += "Configuracion\\Reportes\\Evidencia\\" + nombreArchivo; //archivo = HttpContext.Current.Server.MapPath("Evidencia\\" + nombreArchivo);
                    //sRutaFinal = sRutaFinal.Replace(" ", "-");
                }
                if (nombreArchivo != "")
                {
                    sArchivoAdjunto = "<tr> <td   style='border:none;' colspan='12' > <center><h4>" +
                                                    "<br/><label>Para ver  archivo adjunto haz clic en la liga  --> "
                                                    + "<a  href=\"" + sRutaFinal + "\"  target='_new'>Descargar Archivo</a></label><label></label></h4>" +
                                           "</td> </tr>";

                }
                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                asunto = asuntoReporte;
                cuerpo = "<h4 style='color:black'>Una incidencia ha sido modificada por: " + cUsuarios.getUsuarioById(idUsuario) + "</h3><br />";

                cuerpo += "<center>" +
                            "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                                "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                    "<td style='border:none;' colspan='6' align='right' >" +
                                    "<font size='4.5em'> <b> Grupo: " + grupo + " </b> </font> " +
                                    "</td>" +
                                "</tr>" +
                                "<tr >" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                    "<td colspan='2' > " + folio + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Prioridad</b></td>" +
                                    "<td colspan='2' > " + prioridad + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Tipo Reporte</b></td>" +
                                    "<td colspan='2' > " + tipoReporte + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Sistema/Área </b> </td>	" +
                                    "<td colspan='2'> " + sistema + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Reporte </b></td>" +
                                    "<td colspan='2' > " + fechaReporte + " </td>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Propuesta </b></td>" +
                                    "<td colspan='2' > " + fechaPropuesta + " </td>" +
                                "</tr>" +
                                "<tr >" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                    "<td colspan='10'> " + asuntoReporte + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Descripción </b> </td>" +
                                    "<td colspan='10'> " + descripcion + " </td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                    "<td style='border:none;' ></td>" +
                                "</tr>" +
                                "<tr>"+
                                  sArchivoAdjunto+
                                "</tr>"+
                            //"<tr>" +
                            //    "<td style='border:none;' colspan='12' > <center><h4>" +
                            //        "<br/><label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver mas detalles del reporte</label></h4>" +
                            //    "</td>" +
                            //"</tr>" +
                            "</table>" +
                         
                        "</center>";
                foreach (vUsuariosERPMGrupo vUs in usuariosSoporte)
                {
                    correo += "" + vUs.correoElectronico + ";";
                }
                if (utileria.sendMail(correo, asunto, cuerpo, 2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
            throw;
        }


    }

    /// <summary>
    /// Método para enviar correo electrónico cuando se genera una petición de ERP
    /// </summary>
    /// <returns>True si se envia de manera correcta de los contrario false.</returns>
    public bool enviarCorreoPeticionERP(int idUsuario, int idReporte)
    {
        utileria = new Utileria();
        string asunto = "";
        string cuerpo = "";
        string correo = "";
        //string logoRuta = HttpContext.Current.Server.MapPath("..\\..\\images\\logos\\logoNAD.png");
        string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
        string sArchivoAdjunto = "";


        string sistemas = "";
        var query = (from reportes in erp.tReporte
                     where reportes.idReporte == idReporte
                     select reportes).First();

        string folio = query.folio;
        string fechaPropuesta = String.Format("{0:MM/dd/yyyy}", query.fechaPropuesta);
        string fechaReporte = String.Format("{0:MM/dd/yyyy}", query.fechaReporte);
        string asuntoReporte = query.asunto;
        string descripcion = query.descripcion;
        string prioridad = query.cPrioridadReporte.nombrePrioridad;
        string grupo = query.tERPGrupo.nomGrupo;
        string tipoReporte = query.cTipoReporte.nombreTipoReporte;
        string nombreArchivo = query.evidencia;




        if (nombreArchivo != "")
        {
            sRutaFinal += "Configuracion\\Reportes\\Evidencia\\" + nombreArchivo; //archivo = HttpContext.Current.Server.MapPath("Evidencia\\" + nombreArchivo);
           // sRutaFinal = sRutaFinal.Replace(" ", "-");
        }
        if (nombreArchivo != "")
        {
            sArchivoAdjunto = "<tr> <td   style='border:none;' colspan='12' > <center><h4>" +
                                                     "<br/><label>Para ver  archivo adjunto haz clic en la liga  --> "
                                                     + "<a  href=\"" + sRutaFinal + "\"  target='_new'>Descargar Archivo</a></label><label></label></h4>" +
                                            "</td> </tr>";

        }

        //var usuariosSoporte = (from soporte in erp.vUsuariosERPMGrupos
        //                       where soporte.idRolERPM == 3 && soporte.idTipoCorreoElectronico == 1
        //                       select soporte).ToList();

        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string queryUsuariosSoporte = "select idUsuario from tUsuarioTipoIncidencia where idTipoReporte =" + query.idTipoReporte;
        List<string> lstUsuarioTipo = sp.recuperaRegistros(queryUsuariosSoporte);

        var usuariosSoporte = (from soporte in erp.vUsuariosERPMGrupos
                                   //join tipoUsuario in lstUsuarioTipo on soporte.idEmpleado equals tipoUsuario.idUsuario
                               where soporte.idRolERPM == 3 && soporte.idTipoCorreoElectronico == 1 && lstUsuarioTipo.Contains(soporte.idEmpleado)
                               select soporte).ToList();
        var sist = (from sis in erp.tERPGrupoSistema
                    where sis.idReporte == idReporte && sis.idEstatusERPGrupoSistema != 2
                    select sis).ToList();
        sistemas += "<ul>";
        foreach (tERPGrupoSistema terpgs in sist)
        {
            sistemas += "<li>" + terpgs.cSistemas.nomSistema + "</li>";
        }
        sistemas += "</ul>";

        ControllerUsuarios cUsuarios = new ControllerUsuarios();
        asunto = asuntoReporte;
        cuerpo = "<h4 style='color:black'>Una Petición de ERP ha sido generada por: " + cUsuarios.getUsuarioById(idUsuario) + "</h3><br />";

        cuerpo += "<center>" +
                    "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                        "<tr>" +
                            "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' ></div> </td>" +
                            "<td style='border:none;' colspan='6' align='right' >" +
                            "<font size='4.5em'> <b> Grupo: " + grupo + " </b> </font> " +
                            "</td>" +
                        "</tr>" +
                        "<tr >" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                            "<td colspan='2' > " + folio + " </td>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Prioridad</b></td>" +
                            "<td colspan='2' > " + prioridad + " </td>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Tipo Reporte</b></td>" +
                            "<td colspan='2' > " + tipoReporte + " </td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Sistemas </b> </td>	" +
                            "<td colspan='2'> " + sistemas + " </td>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Reporte </b></td>" +
                            "<td colspan='2' > " + fechaReporte + " </td>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Propuesta </b></td>" +
                            "<td colspan='2' > " + fechaPropuesta + " </td>" +
                        "</tr>" +
                        "<tr >" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                            "<td colspan='10'> " + asuntoReporte + " </td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Descripción </b> </td>" +
                            "<td colspan='10'> " + descripcion + " </td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                        "</tr>" +
                          sArchivoAdjunto +
                    //"<tr>" +
                    //    //"<td style='border:none;' colspan='12' > <center><h4>" +
                    //    //    "<br/><label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver mas detalles del reporte</label></h4>" +
                    //    //"</td>" +
                    //"</tr>" +
                    "</table>" +
                   
                "</center>";


        foreach (vUsuariosERPMGrupo vUs in usuariosSoporte)
        {
            correo += "" + vUs.correoElectronico + ";";
            //correo += "slopez@lcamx.com;";
        }
        correo += "rrodriguez@lcamx.com;";
        if (utileria.sendMail(correo, asunto, cuerpo, 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Método para enviar correo electrónico cuando se modifica una petición de ERP
    /// </summary>
    /// <returns>True si se envia correcto, de lo contrario false.</returns>
    public bool enviarCorreoModificarPeticionERP(int idUsuario, int idReporte)
    {
        utileria = new Utileria();
        string asunto = "";
        string cuerpo = "";
        string correo = "";
        //string logoRuta = HttpContext.Current.Server.MapPath("..\\..\\images\\logos\\logoNAD.png");

        string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
        string sArchivoAdjunto = "";


        string sistemas = "";

        var query = (from reportes in erp.tReporte
                     where reportes.idReporte == idReporte
                     select reportes).First();

        string folio = query.folio;
        string fechaPropuesta = String.Format("{0:MM/dd/yyyy}", query.fechaPropuesta);
        string fechaReporte = String.Format("{0:MM/dd/yyyy}", query.fechaReporte);
        string asuntoReporte = query.asunto;
        string descripcion = query.descripcion;
        string prioridad = query.cPrioridadReporte.nombrePrioridad;
        string grupo = query.tERPGrupo.nomGrupo;
        string tipoReporte = query.cTipoReporte.nombreTipoReporte;
        string nombreArchivo = query.evidencia;

        if (nombreArchivo != "")
        {
            sRutaFinal += "Configuracion\\Reportes\\Evidencia\\" + nombreArchivo; //archivo = HttpContext.Current.Server.MapPath("Evidencia\\" + nombreArchivo);
           // sRutaFinal = sRutaFinal.Replace(" ", "-");
        }
        if (nombreArchivo != "")
        {
            sArchivoAdjunto = "<tr> <td   style='border:none;' colspan='12' > <center><h4>" +
                                                 "<br/><label>Para ver  archivo adjunto haz clic en la liga  --> "
                                                 + "<a  href=\"" + sRutaFinal + "\"  target='_new'>Descargar Archivo</a></label><label></label></h4>" +
                                        "</td> </tr>";

        }


        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string queryUsuariosSoporte = "select idUsuario from tUsuarioTipoIncidencia where idTipoReporte =" + query.idTipoReporte;
        List<string> lstUsuarioTipo = sp.recuperaRegistros(queryUsuariosSoporte);

        var usuariosSoporte = (from soporte in erp.vUsuariosERPMGrupos
                               //join tipoUsuario in lstUsuarioTipo on soporte.idEmpleado equals tipoUsuario.idUsuario
                               where soporte.idRolERPM == 3 && soporte.idTipoCorreoElectronico == 1 && lstUsuarioTipo.Contains(soporte.idEmpleado)
                               select soporte).ToList();

        var sist = (from sis in erp.tERPGrupoSistema
                    where sis.idReporte == idReporte && sis.idEstatusERPGrupoSistema != 2
                    select sis).ToList();
        sistemas += "<ul>";
        foreach (tERPGrupoSistema terpgs in sist)
        {
            sistemas += "<li>" + terpgs.cSistemas.nomSistema + "</li>";
        }
        sistemas += "</ul>";

        ControllerUsuarios cUsuarios = new ControllerUsuarios();
        asunto = asuntoReporte;
        cuerpo = "<h4 style='color:black'>Una Petición de ERP ha sido modificada por: " + cUsuarios.getUsuarioById(idUsuario) + "</h3><br />";

        cuerpo += "<center>" +
                    "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                        "<tr>" +
                            "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                            "<td style='border:none;' colspan='6' align='right' >" +
                            "<font size='4.5em'> <b> Grupo: " + grupo + " </b> </font> " +
                            "</td>" +
                        "</tr>" +
                        "<tr >" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                            "<td colspan='2' > " + folio + " </td>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Prioridad</b></td>" +
                            "<td colspan='2' > " + prioridad + " </td>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Tipo Reporte</b></td>" +
                            "<td colspan='2' > " + tipoReporte + " </td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Sistemas </b> </td>	" +
                            "<td colspan='2'> " + sistemas + " </td>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Reporte </b></td>" +
                            "<td colspan='2' > " + fechaReporte + " </td>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b> Fecha Propuesta </b></td>" +
                            "<td colspan='2' > " + fechaPropuesta + " </td>" +
                        "</tr>" +
                        "<tr >" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                            "<td colspan='10'> " + asuntoReporte + " </td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Descripción </b> </td>" +
                            "<td colspan='10'> " + descripcion + " </td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                            "<td style='border:none;' ></td>" +
                        "</tr>" +
                    // sArchivoAdjunto +
                    //"<tr>" +
                    //    "<td style='border:none;' colspan='12' > <center><h4>" +
                    //        "<br/><label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver mas detalles del reporte</label></h4>" +
                    //    "</td>" +
                    //"</tr>" +
                    "</table>" +
                 
                "</center>";


        foreach (vUsuariosERPMGrupo vUs in usuariosSoporte)
        {
            correo += "" + vUs.correoElectronico + ";";
        }
        correo += "rrodriguez@lcamx.com;";
        if (utileria.sendMail(correo, asunto, cuerpo, 2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Método utilizado cuando se carga la página de Incidencias
    /// </summary>
    public bool iniciar(int idUsuario)
    {
        var query = (from view in erp.vUsuariosERPMGrupos
                     where view.idTipoUsuario == 1 && view.idEmpleado == idUsuario.ToString()
                     select view).ToList();
        var query2 = (from view in erp.vUsuariosERPMGrupos
                      where view.idTipoUsuario == 1 && view.idEmpleado == idUsuario.ToString()
                      select view).ToString();

        //var query2 = (from view in erp.vUsuariosERPMGrupos
        //             where view.idTipoUsuario == 1 && view.idEmpleado == idUsuario.ToString() && view.TipoUsuario.Contains("consultor")
        //             select view).ToString();

        if (query.Count() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Método para obtener el grupo del usuario que se encuentra en sesión
    /// </summary>
    public List<vUsuariosERPMGrupo> getGrupo(int idUsuario)
    {
        var grupo = (from terp in erp.vUsuariosERPMGrupos
                     where terp.idEmpleado.ToString() == idUsuario.ToString()
                     select terp).ToList();

        return grupo;
    }

    /// <summary>
    /// Método para obtener el responsable de un reporte
    /// </summary>
    /// <returns>Nombre del responsable</returns>
    public string obtenerResponsable(int idReporte)
    {
        ControllerUsuarios cUsuario = new ControllerUsuarios();
        string result = "";

        var query = (from trr in erp.tResponsableReporte
                     where trr.idReporte == idReporte && trr.idTipoResponsable == 1
                     select trr).ToList();
        if (query.Count() > 0)
        {
            foreach (tResponsableReporte responsable in query)
            {
                result += "<span id='icon-25' class='usuario_loggeado verde'></span> <label id='frm'>Responsable: </label> <br /> <div id='responsable' class='listadoConsultaResponsable clear'>" + cUsuario.getUsuarioById(responsable.idResponsable) + "<div style='float: right'></div></div>";
            }
        }
        else
        {
            var existeLecciones = (from tr in erp.tReporte
                                   where tr.idReporte == idReporte && tr.idEstatusReporte == 7
                                   select tr).Count();
            if (existeLecciones > 0)
            {
                var leccionesA = (from vla in erp.vLeccionesAprendidas
                                  where vla.idReporte == idReporte && vla.idEstadoReporte == 7
                                  select vla).FirstOrDefault();
                result += "<span id='icon-25' class='usuario_loggeado verde'></span> <label id='frm'>Responsable: </label> <br /> <div id='responsable' class='listadoConsultaResponsable clear'>" + cUsuario.getUsuarioById(leccionesA.idUsuarioRespuesta.GetValueOrDefault()) + "<div style='float: right'></div></div>";
            }
            else
            {
                result += "<span id='icon-47' class='warning rojo'></span> <label>La incidencia aún no ha sido asignada.</label>";
            }
        }
        return result;
    }

    /// <summary>
    /// Método para obtener el nombre de un grupo
    /// </summary>
    public string obtenerNombreGrupo(int idGrupo)
    {
        string result = "";

        var query = (from terpg in erp.tERPGrupo
                     where terpg.idERPGrupo == idGrupo
                     select terpg).FirstOrDefault();
        result = query.nomGrupo;
        return result;

    }

    /// <summary>
    /// Método para obtener todos los sistemas que contiene un ERP
    /// </summary>
    /// <returns>Nombres de los sistemas y estructuras de los checkBox</returns>
    public string getSistemasTodosERP()
    {
        string result = "";
        var sistemasERP = new int[] { 1, 2, 3, 4, 10, 11 };//Sin SGI 5

        var query = (from sistemas in erp.cSistemas
                     where sistemasERP.Contains(sistemas.idSistema)
                     select sistemas).ToList();

        if (query.Count > 0)
        {
            foreach (cSistemas cs in query)
            {

                var sistemasGrupo = (from sistem in erp.tERPGrupoSistema
                                     where sistem.idSistema == cs.idSistema && sistem.idEstatusERPGrupoSistema != 2
                                     select sistem).ToList();
                result += "<a href='#'> <label class='divBlancoSistemas'  id=" + cs.idSistema + "><input type='checkbox' id='chk" + cs.idSistema + "' class='sistemas'  onchange='javascript:seleccionado(" + cs.idSistema + ",this);' name='chkSistemas' value=" + cs.idSistema + " /> " + cs.nomenglatura + " </label> </a>";
            }
        }
        else
        {
            result += "Cuenta con todos los sistemas";
        }
        return result;
    }

    /// <summary>
    /// Método para obtener la tabla de las lecciones aprendidas por un usuario(incidencias en estatus 9)
    /// </summary>
    /// <returns>Estructura de la tabla de las lecciones Aprendidas</returns>
    public string getTblLeccionesAprendidas(int idUsuario)
    {
        string result = "";

        var query = (from tr in erp.tReporte
                     where tr.idEstatusReporte == 7 && tr.idUsuario == idUsuario
                     select tr).ToList();

        var queryTblLecciones = (from tla in erp.vLeccionesAprendidas
                                 where tla.idUsuarioGenerador == idUsuario
                                 select tla).ToList();

        if (queryTblLecciones.Count() > 0)
        {
            result += "<fieldset>" +
                        "<legend> Lecciones Aprendidas </legend>" +
                      "<table id='tblLeccionesAprendidas' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Folio</th>" +
                            "<th>Asunto</th>" +
                            "<th>Detalle</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";
            foreach (vLeccionesAprendidas tla in queryTblLecciones)
            {
                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                result += "<tr>";
                result += "<td> <span class='hide'>" + String.Format("{0:yyyy/MM/dd}", tla.fechaReporte) + " </span>" + tla.folio + "</td>";
                result += "<td>" + tla.asunto + "</td>";
                result += "<td><a href='javascript:;' onclick='javascript:detalleLecciones(" + tla.idLeccionesAprendidas + ");'><span id='icon-25' class='consultar' title='Consultar'></span></a></td>";
                result += "</tr>";
            }
            result += "</tbody>" +
                         "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='3'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>" +
                      "</fieldset>";
        }
        else
        {
            result += "";
        }
        return result;
    }

    /// <summary>
    /// Método para obtener el detalle de un reporte que se encuentra en el estatus de lecciones Aprendidas
    /// </summary>
    /// <returns>Objeto vLeccionesAprendidas, con el detalle de las lecciones aprendidas</returns>
    public vLeccionesAprendidas consultarLeccionesAprendidas(int idLecciones)
    {
        var lecciones = (from vla in erp.vLeccionesAprendidas
                         where vla.idLeccionesAprendidas == idLecciones
                         select vla).FirstOrDefault();

        return lecciones;
    }

    public List<view_ValidarTipoUsuarioAreaGrupo> getEstatusUsuario(int idUsuario)
    {
        erpRH = new ERPManagementRHDataContext();
        var validacionUsua = (from vValUserV in erpRH.view_ValidarTipoUsuarioAreaGrupo
                              where vValUserV.idUsuario == idUsuario //&& vValUserV.idTipoUsuario !=null && vValUserV.area !=null && vValUserV.grupo !=null
                              select vValUserV).ToList();

        return validacionUsua;
    }

    /// <summary>
    /// Método para generar la tabla de reportes
    /// Sin Asignar.
    /// </summary>
    /// <returns>Cadena con estructura de la tabla</returns>
    public string getTblReportesSoportesSinAsignar(int idUsuario)
    {
        string result = "";

        var lstReportes = (from ro in erp.vUsuariosSoporte
                           where ro.idUsuario == idUsuario && ro.idEstatusReporte == 1
                           select ro).ToList();

        erpRH = new ERPManagementRHDataContext();
        //var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
        //                     where vValUser.idUsuario == idUsuario
        //                     select vValUser).FirstOrDefault();
        if (lstReportes.Count > 0)
        {
            ControllerUsuarios cUsuarios = new ControllerUsuarios();
            result += "<fieldset>" +
                        "<legend>Incidencias Sin Asignar</legend>" +
                      "<table id='tblReporteSoporte' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Usuario</th>" +
                            "<th>Tipo Reporte</th>" +
                            "<th>Prioridad</th>" +
                            "<th>Grupo</th>" +
                            "<th>Sistema/Área</th>" +
                            "<th>Fecha</th>" +
                            "<th>Detalle</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            foreach (vUsuariosSoporte t in lstReportes)
            {
                result += "<tr>";
                result += "<td> <span class='hide'>" + String.Format("{0:yyyy/MM/dd}", t.fechaReporte) + " </span>" + cUsuarios.getUsuarioById(t.idUsuarioCreador.GetValueOrDefault()) + "</td>";
                result += "<td>" + t.Tipo_Reporte + "</td>";
                result += "<td>" + t.Prioridad + "</td>";
                result += "<td>" + t.Grupo + "</td>";
                result += "<td>" + t.Area + "</td>";
                result += "<td>" + String.Format("{0:MM/dd/yyyy}", t.fechaReporte) + "</td>";
                result += "<td><a title='Consultar Incidencia' onclick='javascript:getDetalleReporte(" + t.idReporte + ",13);'><span id='icon-25' class='consultar verde'></span></a></td>";
                result += "</tr>";
            }


            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='7'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>" +
                      "</fieldset>";
        }

        return result;
    }

    /// <summary>
    /// Método para generar un excel con las historias de usuario
    /// para puntuarlas.
    /// </summary>
    /// <returns>Cadena con estructura que contiene un excel</returns>
    public string generarExcelHistorias(int idEstatus)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string sResult = "";
        string sQuery = "";

        sQuery = "SELECT idProductBackLog  ID,cs.nomenglatura  Sistema,sEpica  Epica,sHistoria  Historia, ' Descripción :|'+ sDescripcion+' | ' +'Criterios de Aceptación :|'+ sCriteriosAceptacion +'|'+' Riesgos: |'+ sRiesgos  Descipcion,'' storyPoints " +
            " FROM tProductBackLog tp " +
            " INNER JOIN cSistemas  cs ON tp.idSistema = cs.idSistema " +
            " INNER JOIN cPrioridadReporte cp ON tp.idPrioridad = cp.idPrioridadReporte " +
            " WHERE idEstatus =" + idEstatus;

        DataSet ds = sp.getValues(sQuery);

        if (ds.Tables[0].Rows.Count > 0)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                // Crear una nueva hoja de Excel
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reporte");

                // Establecer el estilo de las celdas
                worksheet.Cells.Style.Font.Size = 12;

                // Escribir los encabezados de columna
                for (int col = 0; col < ds.Tables[0].Columns.Count; col++)
                {
                    worksheet.Cells[1, col + 1].Value = ds.Tables[0].Columns[col].ColumnName;
                }

                // Ajustar automáticamente el tamaño de las columnas
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Agregar un tamaño adicional de 5px a las columnas
                for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                {
                    worksheet.Column(col).Width += 5;
                }

                // Establecer estilo a la fila de encabezados
                var headersRow = worksheet.Cells[1, 1, 1, ds.Tables[0].Columns.Count];
                headersRow.Style.Font.Color.SetColor(Color.White);
                headersRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headersRow.Style.Fill.BackgroundColor.SetColor(Color.Blue);
                headersRow.Style.Font.Bold = true;

                // Aplicar filtros a la tabla
                worksheet.Cells[worksheet.Dimension.Address].AutoFilter = true;

                // Escribir los datos en la hoja
                for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                {
                    for (int col = 0; col < ds.Tables[0].Columns.Count; col++)
                    {
                        //worksheet.Cells[row + 2, col + 1].Value = ds.Tables[0].Rows[row][col];

                        // Obtener el valor de la celda
                        string cellValue = ds.Tables[0].Rows[row][col].ToString();

                        // Reemplazar "|" con salto de línea
                        cellValue = cellValue.Replace("|", "\n");

                        // Asignar el valor a la celda
                        worksheet.Cells[row + 2, col + 1].Value = cellValue;
                    }
                }

                // Guardar el archivo Excel en un flujo de bytes
                byte[] fileBytes = package.GetAsByteArray();

                // Convertir el flujo de bytes a una cadena Base64
                sResult = Convert.ToBase64String(fileBytes);
            }
        }
        else
        {
             sResult = "";
        }
        return sResult;


    }

    public bool AsignarHistorias(int idSprint, string historias)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string sQuery = "";
        bool bBandera = true;

        string[] aDatos = historias.Split(',');        

        foreach(string idBackLog in aDatos)
        {
            sQuery = "SELECT tg.idErpGrupo,tp.idSistema,sHistoria, " +
                " 'Descripción: '+char(10)+ sDescripcion + CHAR(10) + " +
                " 'Criterios de Aceptacion: '+char(10)+ sCriteriosAceptacion + CHAR(10) + " +
                " 'Riesgos: '+char(10)+ sRiesgos AS Descripcion,storyProints,idUsuario,idPrioridad " +
                " FROM tProductBackLog tp " +
                " INNER JOIN tERPGrupo tg on tp.sCliente = tg.nomGrupo where idProductBackLog = "+idBackLog;
            List<string> list = sp.recuperaRegistros(sQuery);

            sQuery = "EXEC pa_AgregarHistoriaSprint " + int.Parse(list[0]) + ", " + int.Parse(list[1]) + ", '"+ list[2].ToString() + "', '"+list[3].ToString() + "', "+ int.Parse(list[4]) + ", "+ int.Parse(list[5]) + ", "+ idSprint + ", "+int.Parse(idBackLog)+ ", "+ int.Parse(list[6]);
            bBandera = sp.ejecutaSQL(sQuery);
        }

        return bBandera;
    }

    public bool LiberarHistoria(string newFecha, string historias, int idEstatus)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string sQuery = "";
        bool bBandera = true;

        string[] aDatos = historias.Split(',');

        foreach (string idHistoria in aDatos)
        {
            sQuery = "EXEC pa_ModificacionHistorias '" + newFecha + "', " + idHistoria + ", " + idEstatus;
            bBandera = sp.ejecutaSQL(sQuery);
        }

        return bBandera;
    }

    public bool AsignarHistoria(string newFecha, string historias, int idEstatus)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string sQuery = "";
        bool bBandera = true;

        string[] aDatos = historias.Split(',');

        foreach (string idHistoria in aDatos)
        {
            sQuery = "EXEC pa_ModificacionHistorias '" + newFecha + "', " + idHistoria + ", " + idEstatus;
            bBandera = sp.ejecutaSQL(sQuery);
        }

        return bBandera;
    }

    /// <summary>
    /// Método para leer un excel y actualizar los datos de historias de usuario.
    /// </summary>
    /// <returns>Cadena con resultado obtenido</returns>
    public string leerExcelHistorias(string nombre)
    {
        string sResultado = "";

        // Ruta al archivo Excel
        string filePath = "~/Configuracion/ProductBackLog/ReportesExcel/"+nombre;

        // Resolver la ruta virtual a una ruta física
        string rutaFisica = HttpContext.Current.Server.MapPath(filePath);

        List<List<string>> excelData = new List<List<string>>();
        // Verificar si el archivo existe
        if (File.Exists(rutaFisica))
        {
            using (var package = new ExcelPackage(new FileInfo(rutaFisica)))
            {
                var nombres = "";
                // Iterar a través de las hojas de trabajo
                for (int i = 1; i <= package.Workbook.Worksheets.Count; i++)
                {
                    var worksheet1 = package.Workbook.Worksheets[i];
                    Console.WriteLine("Nombre de la Hoja:" + worksheet1.Name + "Índice: " + i);
                    nombres += "Nombre de la Hoja:" + worksheet1.Name + "Índice: " + i + " ---";
                }
                // Obtener la hoja de trabajo (worksheet) que deseas leer
                var worksheet = package.Workbook.Worksheets[1]; // Puedes cambiar el índice según tus necesidades

                // Obtener el número de filas y columnas en la hoja de trabajo
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                storedProcedure sp = new storedProcedure("DBSGICEConnectionString");

                // Obtener todos los IDs existentes en la base de datos
                string sQuerySelectIds = "SELECT idProductBackLog FROM tProductBackLog WHERE idEstatus = 2 ";
                DataSet dsIds = sp.getValues(sQuerySelectIds);

                // Iterar sobre las filas del archivo Excel
                for (int row = 2; row <= rowCount; row++) // Empezar desde la segunda fila, asumiendo que la primera fila es encabezado
                {
                    // Obtener el valor del ID de la celda
                    string idProductBackLog = worksheet.Cells[row, 1].Text;

                    // Verificar si el ID no existe en la base de datos
                    if (!dsIds.Tables[0].AsEnumerable().Any(r => r.Field<int>("idProductBackLog") == int.Parse( idProductBackLog)))
                    {
                        sResultado = "1"; //
                        break;
                    }
                }

                if(sResultado == "")
                {
                    // Iterar sobre los IDs válidos y actualizar la base de datos
                    for (int row = 2; row <= rowCount; row++)
                    {
                        // Obtener el valor del ID de la celda
                        string idProductBackLog = worksheet.Cells[row, 1].Text;

                        // Obtener el valor de storyPoints desde el archivo Excel (puedes hacer esto dentro del bucle foreach si es necesario)
                        string storyPoints = worksheet.Cells[row, 6].Text;

                        // Crear y ejecutar la consulta SQL para actualizar la base de datos
                        string sQuery = "UPDATE tProductBackLog SET idEstatus = 3, storyProints = " + storyPoints + " WHERE idProductBackLog = " + idProductBackLog;

                        // Ejecutar la consulta SQL

                        bool bres = sp.ejecutaSQL(sQuery);

                        Console.Write(bres);
                    }
                    sResultado = "2";
                }

            }
            // Eliminar el archivo
            File.Delete(rutaFisica);
        }
        else
        {
            sResultado = "0";
        }
        
        return sResultado;
    }
}