using ExcelDataReader;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ControllerActividades
/// </summary>
public class ControllerActividades
{
    private ERPManagementDataContext erp;
    private ERPManagementRHDataContext erpRH;
    public ControllerActividades()
    {
        erp = new ERPManagementDataContext();
        erpRH = new ERPManagementRHDataContext();
    }

    public string GetActividades(int idUsuario)
    {
        string result = "";
        var lstActividades = (from act in erp.tActividadesTI 
                              where act.iIdUsuario == idUsuario 
                              && !act.bEliminado 
                              select act).OrderByDescending(x => x.dFecha).ToList();
        if (lstActividades.Count > 0)
        {
            result += "<fieldset>" +
                        "<legend>Actividades</legend>" +
                      "<table id='tblActividades' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th width=\"10%\">Fecha</th>" +
                            "<th width=\"15%\">Grupo</th>" +
                            "<th width=\"8%\">Sistema</th>" +
                            "<th width=\"8%\">Horas</th>" +
                            "<th width=\"59%\">Descripción</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            foreach (tActividadesTI a in lstActividades)
            {
                result += "<tr>";
                result += "<td>" + String.Format("{0:yyyy/MM/dd}", a.dFecha) + "</td>";
                result += "<td>" + a.sGrupo.ToUpper() + "</td>";
                result += "<td>" + a.sSistema.ToUpper() + "</td>";
                result += "<td>" + a.iHoras + "</td>";
                result += "<td>" + a.sDescripcion + "</td>";
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


    public List<string> lstSprint()
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        List<string> result = new List<string>();
        string sQuery = "select iNumeroSprint from tSprint where dFechaFin >= GETDATE() AND dFechaInicio <= GETDATE() and sAnio = YEAR(GETDATE())";
        string sPrint = sp.recuperaValor(sQuery);

        sQuery = "select * from vReporteSprint where Sprint = "+sPrint;
        
        result = sp.recuperaRegistros(sQuery);
        return result;
    }

    public List<string> lstSprintByID(string sprint)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        List<string> result = new List<string>();
        
        string sQuery = "select * from vReporteSprint where Sprint = "+sprint;

        result = sp.recuperaRegistros(sQuery);
        return result;
    }

    public string GetSprint(string sprint)
    {
        sprint = (string.IsNullOrEmpty(sprint) ? "0" : sprint);
        string result = "";
        var lstSprint = (from act in erp.vDetalleSprint
                         where act.iNumeroSprint == int.Parse(sprint)
                              select act).OrderByDescending(x => x.folio).ToList();
        if (lstSprint.Count > 0)
        {
            result += "<fieldset>" +
                        "<legend>Sprint "+sprint+"</legend>" +
                      "<table id='tblDetalle' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th width=\"10%\">Folio</th>" +
                            "<th width=\"10%\">Tipo Reporte</th>" +
                            "<th width=\"10%\">Grupo</th>" +
                            "<th width=\"5%\">Sistema</th>" +
                            "<th width=\"20%\">Asunto</th>" +
                            "<th width=\"10%\">Usuario</th>" +
                            "<th width=\"5%\">Puntos</th>" +
                            "<th width=\"10%\">Estatus</th>" +
                            "<th width=\"10%\">Fecha Asignacion</th>" +
                            "<th width=\"10%\">Fecha Pase a Pruebas</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            foreach (vDetalleSprint a in lstSprint)
            {
                result += "<tr>";
                result += "<td style=\"text-align: center;\">" + a.folio+ "</td>";
                result += "<td style=\"text-align: center;\">" + a.tipoReporte+ "</td>";
                result += "<td style=\"text-align: center;\">" + a.nomGrupo + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Sistema + "</td>";
                result += "<td style=\"text-align: center;\">" + a.asunto + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Responsable + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Puntos + "</td>";
                result += "<td style=\"text-align: center;\">" + a.sEstatus + "</td>";
                result += "<td style=\"text-align: center;\">" + a.FechaAsignacion + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Fecha_Pase_A_Pruebas + "</td>";
                result += "</tr>";
            }
            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='10'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>" +
                      "</fieldset>";
        }
        return result;
    }


    public string GetTablaSprint()
    {
        string result = "";
        var lstSprint = (from act in erp.vReporteSprint
                         where act.Sprint != 0
                         select act).OrderByDescending(x => x.Sprint).ToList();
        if (lstSprint.Count > 0)
        {
            result += "<fieldset>" +
                        "<legend>Sprints</legend>" +
                      "<table id='tblSprint' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th width=\"10%\">Sprint</th>" +
                            "<th width=\"8%\">Periodo</th>" +
                            "<th width=\"8%\">Capcidad SP</th>" +
                            "<th width=\"8%\">Total de Historias</th>" +
                            "<th width=\"8%\">Historias Asignadas</th>" +
                            "<th width=\"8%\">Historias en Pruebas</th>" +
                            "<th width=\"8%\">Historias Concluidas</th>" +
                            "<th width=\"8%\">Historias Trasladadas</th>" +
                            "<th width=\"8%\">SP Asignados</th>" +
                            "<th width=\"8%\">SP Cumplidos</th>" +
                            "<th width=\"10%\">% Avance</th>" +
                            "<th width=\"10%\">Estatus</th>" +
                            "<th width=\"10%\">Detalle</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            foreach (vReporteSprint a in lstSprint)
            {
                result += "<tr>";
                result += "<td style=\"text-align: center;\">" + a.Sprint + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Periodo + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Capacidad_SP + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Total_de_Historias + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Historias_Asignadas + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Historias_en_pruebas + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Historias_Concluidas + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Historias_Traladadas + "</td>";
                result += "<td style=\"text-align: center;\">" + a.SP_Asignados + "</td>";
                result += "<td style=\"text-align: center;\">" + a.SP_Cumplidos + "</td>";
                result += "<td style=\"text-align: center;\">" + a.__Avance + "</td>";
                result += "<td style=\"text-align: center;\">" + a.Estatus + "</td>";
                result += "<td><a onclick='javascript:getReportesCreados(" + a.Sprint + ");'><span id='icon-25' class='consultar azul' title='Ver Detalle'></span></a></td>";
                result += "</tr>";
            }
            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='13'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>" +
                      "</fieldset>";
        }
        return result;
    }

    public bool LeerExcel(int idUsuario, string path)
    {
        try
        {
            var usuario = (from u in erp.vUsuariosERPM where u.idEmpleado == idUsuario.ToString() select u).FirstOrDefault();
            DataSet resultDataSet = new DataSet();
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs))
                {
                    resultDataSet = excelDataReader.AsDataSet();
                    excelDataReader.Close();
                }
            }
            bool isHeader = true;

            foreach (DataRow row in resultDataSet.Tables[0].Rows)
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                tActividadesTI actividad = new tActividadesTI();

                actividad.dFechaCarga = DateTime.Now;
                actividad.bEliminado = false;
                actividad.iIdUsuario = idUsuario;
                actividad.sUsuario = usuario.usuario;
                actividad.sNombre = usuario.nombreCompleto;
                actividad.dFecha = row.Field<DateTime>(0);
                actividad.sGrupo = row.Field<string>(1).ToUpper();
                actividad.sSistema = row.Field<string>(2).ToUpper();
                actividad.iHoras = (decimal)row.Field<double>(3);
                actividad.sDescripcion = row.Field<string>(4);

                erp.tActividadesTI.InsertOnSubmit(actividad);                
            }

            erp.SubmitChanges();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
}