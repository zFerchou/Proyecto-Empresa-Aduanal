using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ControllerClientes
/// </summary>
public class ControllerClientes
{
    private ERPManagementDataContext erp;
    private view_CuotasGrupSi vCuotas;
    private tCuotas cuotas;
    private Utileria utileria;
    public ControllerClientes()
    {
        vCuotas = new view_CuotasGrupSi();
        erp = new ERPManagementDataContext();
        cuotas = new tCuotas();
        utileria = new Utileria();
    }

    /// <summary>
    /// Obtener todas lss cuotas de los Grupos y sus sistemas contratados(GENERAL)
    /// </summary>
    /// <returns></returns>
    public string getCuotas(int idUsuario)
    {
        string result = "";
        var lstCuotas = (from vc in erp.view_CuotasGrupSis
                         select vc).ToList();

        var cliente = (from tru in erp.tRolUsuarioERPM
                       where tru.idUsuario == idUsuario && tru.idRolERPM == 5
                       select tru).Count();

        var calidad = (from tru in erp.tRolUsuarioERPM
                       where tru.idUsuario == idUsuario && tru.idRolERPM == 7
                       select tru).Count();
        if (cliente == 1 || (cliente == 1 && calidad == 1))
        {
            if (lstCuotas.Count > 0)
            {
                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                result +=
                          "<table id='tblCuotas' class='data_grid display'>" +
                            "<thead id='grid-head2'>" +
                                "<th>Grupo</th>" +
                                "<th>Sistema</th>" +
                                "<th>Fecha Inicio</th>" +
                                "<th>Fecha vencimiento</th>" +
                                "<th>Cuota</th>" +
                                "<th>Acciones</th>" +
                            "</thead>" +
                            "<tbody id='grid-body'>";

                foreach (var t in lstCuotas)
                {
                    result += "<tr id=" + t.idCuota + ">";
                    result += "<td>" + t.nomGrupo + "</td>";
                    result += "<td>" + t.nomSistema + "</td>";
                    result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaInicio) + "</span>" + String.Format("{0:dd/MM/yyyy}", t.fechaInicio) + "</td>";
                    result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaVencimiento) + "</span>" + String.Format("{0:dd/MM/yyyy}", t.fechaVencimiento) + "</td>";
                    result += "<td>" + decimal.Round((decimal)t.cuota, 2, MidpointRounding.AwayFromZero) + "</td>";
                    result += "<td><a onclick='javascript:eliminarCuota(" + t.idCuota + ");'><span id='icon-25' class='eliminar azul' title='Eliminar'></span></a> |  <a onclick='javascript:obtenerCuota(" + t.idCuota + ", " + t.idERPGrupoSistema + ");'><span id='icon-25' class='modificar azul' title='Modificar'></span></a></td>";
                    result += "</tr>";
                }

                result += "</tbody>" +
                            "<tfoot>" +
                                "<tr>" +
                                    "<td class='foot shadow3 round-down2' colspan='6'>&nbsp;</td>" +
                                "</tr>" +
                            "</tfoot>" +
                            "</table>";
            }

        }
        else
        {
            if (lstCuotas.Count > 0)
            {
                ControllerUsuarios cUsuarios = new ControllerUsuarios();
                result +=
                          "<table id='tblCuotas' class='data_grid display'>" +
                            "<thead id='grid-head2'>" +
                                "<th>Grupo</th>" +
                                "<th>Sistema</th>" +
                                "<th>Fecha Inicio</th>" +
                                "<th>Fecha vencimiento</th>" +
                                "<th>Cuota</th>" +
                            "</thead>" +
                            "<tbody id='grid-body'>";

                foreach (var t in lstCuotas)
                {
                    result += "<tr id=" + t.idCuota + ">";
                    result += "<td>" + t.nomGrupo + "</td>";
                    result += "<td>" + t.nomSistema + "</td>";
                    result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaInicio) + "</span>" + String.Format("{0:dd/MM/yyyy}", t.fechaInicio) + "</td>";
                    result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaVencimiento) + "</span>" + String.Format("{0:dd/MM/yyyy}", t.fechaVencimiento) + "</td>";
                    result += "<td>" + decimal.Round((decimal)t.cuota, 2, MidpointRounding.AwayFromZero) + "</td>";
                    result += "</tr>";
                }

                result += "</tbody>" +
                            "<tfoot>" +
                                "<tr>" +
                                    "<td class='foot shadow3 round-down2' colspan='5'>&nbsp;</td>" +
                                "</tr>" +
                            "</tfoot>" +
                            "</table>";
            }

        }
        return result;

    }

    /* Mostrar los grupos  */
    public string getGrupos()
    {
        string result = "";
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string query = "";

        query = "select idERPGG, isnull(grupo,''),isnull(urlSgi,''),isnull(urlRepo,''),isnull(bdEmpresa,''),isnull(idERPGrupo,'') from tERPGrupoGen";

        List<string> grupos = sp.recuperaRegistros(query);


        if (grupos.Count > 0)
        {
            ControllerUsuarios cUsuarios = new ControllerUsuarios();
            result +=
                      "<table id='tblCuotas' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Grupo</th>" +
                            "<th>URL SGI</th>" +
                            "<th>URL Repositorio</th>" +
                            "<th>BD Empresa</th>" +
                            "<th>Sistemas</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            for (int i = 0; i < grupos.Count; i = i + 6)
            {
                result += "<tr id=" + grupos[i] + ">";
                result += "<td>" + grupos[i + 1] + "</td>";
                result += "<td>" + grupos[i + 2] + "</td>";
                result += "<td>" + grupos[i + 3] + "</td>";
                result += "<td>" + grupos[i + 4] + "</td>";
                result += "<td><a onclick='javascript:verGrupo(" + grupos[i + 5] + ");'><span id='icon-25' class='consultar azul' title='Ver Grupo'></span></a></td>";
                result += "</tr>";
            }

            result += "</tbody>" +
                            "<tfoot>" +
                                "<tr>" +
                                    "<td class='foot shadow3 round-down2' colspan='5'>&nbsp;</td>" +
                                "</tr>" +
                            "</tfoot>" +
                            "</table>";

        }/*  */

        return result;

    }

    /// <summary>
    /// Obtener la información de la Cuota para que puedan modificar los datos
    /// </summary>
    /// <param name="idCuota"></param>
    /// <returns></returns>
    public List<string> obtenerCuota(int idCuota)
    {
        double cuota = 0;
        string fechaI = "";
        string fechaF = "";
        string cboMoneda = "<select id='cbo" + idCuota + "'  class='select'><option value='0' disabled selected>Elija una opción</option>";
        var lstCuota = (from vc in erp.view_CuotasGrupSis
                        where vc.idCuota == idCuota
                        select vc).ToList();
        List<string> lst = new List<string>();

        foreach (var t in lstCuota)
        {
            cuota = t.cuota.GetValueOrDefault();
            fechaI = String.Format("{0:yyyy-mm-dd}", t.fechaInicio);
            fechaF = String.Format("{0:yyyy-mm-dd}", t.fechaVencimiento);
            lst.Add(Convert.ToString(decimal.Round((decimal)cuota, 2, MidpointRounding.AwayFromZero)));
            lst.Add(fechaI);
            lst.Add(fechaF);

        }

        var cbo = (from mon in erp.cMonedaCuota
                   select mon).ToList();

        foreach (cMonedaCuota mc in cbo)
        {
            cboMoneda += "<option value=" + mc.idMoneda + ">" + mc.nombreMoneda + "</option>";
        }

        lst.Add(cboMoneda);

        var moneda = (from tipoM in erp.tCuotas
                      where tipoM.idCuota == idCuota
                      select new { tipoM.idMoneda }).Single();

        lst.Add(moneda.idMoneda.ToString());

        return lst;

    }

    /// <summary>
    /// Modificar la cinformación de la Cuota
    /// </summary>
    /// <param name="idCuota"></param>
    /// <param name="cuota"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaVencimiento"></param>
    /// <returns></returns>
    public bool modificarCuota(int idCuota, double cuota, string fechaInicio, string fechaVencimiento, string personalResp, int idERPGruposistema, int tMoneda)
    {
        string[] presp = personalResp.Split(','); ;
        int tam = presp.Length;
        try
        {
            var responsables = (from tresp in erp.tResponsableSistema
                                where tresp.idERPGrupoSistema == idERPGruposistema
                                select tresp).ToList();

            erp.tResponsableSistema.DeleteAllOnSubmit(responsables);
            erp.SubmitChanges();

            var lstCuota = (from vc in erp.tCuotas
                            where vc.idCuota == idCuota
                            select vc).First();
            lstCuota.cuota = cuota;

            DateTime fechaI = Convert.ToDateTime(fechaInicio);
            DateTime fechaV = Convert.ToDateTime(fechaVencimiento);
            lstCuota.fechaInicio = fechaI;
            lstCuota.fechaVencimiento = fechaV;
            lstCuota.idMoneda = tMoneda;
            if (personalResp != "")
            {
                for (int i = 0; i < tam; i++)
                {
                    tResponsableSistema tresp = new tResponsableSistema();
                    tresp.idResponsable = int.Parse(presp[i]);
                    tresp.idERPGrupoSistema = idERPGruposistema;
                    erp.tResponsableSistema.InsertOnSubmit(tresp);
                }
            }
            erp.SubmitChanges();

            if (personalResp != "")
            {
                enviarCorreoModificacionCuota(idCuota, idERPGruposistema);
            }

            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    /// <summary>
    /// Eliminar una Cuota (Se cambia a estatus 2 de eliminado)
    /// </summary>
    /// <param name="idCuota"></param>
    /// <returns></returns>
    public bool eliminarCuota(int idCuota)
    {
        try
        {
            var reporte = (from tr in erp.tCuotas
                           where tr.idCuota == idCuota
                           select tr).First();
            reporte.idEstatus = 2;

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
    /// Obtener las Cuotas segun los filtros de búsqueda
    /// </summary>
    /// <param name="idGrupo"></param>
    /// <param name="idSistema"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <returns></returns>
    public string obtenerCuotasFiltros(int idGrupo, int idSistema, DateTime fechaInicio, DateTime fechaFin)
    {
        string fechaI = String.Format("{0:yyyy-MM-dd}", fechaInicio);
        string fechaF = String.Format("{0:yyyy-MM-dd}", fechaFin);
        string result = "";
        var lstCuotas = from tc in erp.view_CuotasGrupSis
                        select tc;
        if (idGrupo != 0)
        {
            lstCuotas = lstCuotas.Where(tc => (tc.idERPGrupo == idGrupo));
        }
        if (idSistema != 0)
        {
            lstCuotas = lstCuotas.Where(tc => (tc.idSistema == idSistema));
        }
        if (fechaInicio != DateTime.MinValue && fechaFin == DateTime.MinValue)
        {
            lstCuotas = lstCuotas.Where(tc => tc.fechaInicio == fechaI);
        }
        if (fechaFin != DateTime.MinValue && fechaInicio == DateTime.MinValue)
        {
            lstCuotas = lstCuotas.Where(tc => tc.fechaVencimiento == fechaF);
        }
        if (fechaInicio != DateTime.MinValue && fechaFin != DateTime.MinValue)
        {
            lstCuotas = lstCuotas.Where(tc => Convert.ToDateTime(tc.fechaInicio) >= Convert.ToDateTime(fechaI) && Convert.ToDateTime(tc.fechaVencimiento) <= Convert.ToDateTime(fechaF));
        }


        if (lstCuotas.ToList().Count > 0)
        {
            ControllerUsuarios cUsuarios = new ControllerUsuarios();
            result +=
                      "<table id='tblCuotas' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Grupo</th>" +
                            "<th>Sistema</th>" +
                            "<th>Fecha Inicio</th>" +
                            "<th>Fecha vencimiento</th>" +
                            "<th>Cuota</th>" +
                            "<th>Acciones</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";


            foreach (var t in lstCuotas)
            {
                result += "<tr id=" + t.idCuota + ">";
                result += "<td>" + t.nomGrupo + "</td>";
                result += "<td>" + t.nomSistema + "</td>";
                result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaInicio) + "</span>" + String.Format("{0:dd/MM/yyyy}", t.fechaInicio) + "</td>";
                result += "<td>" + "<span class='hide'>" + String.Format("{0:yyyy-MM-dd}", t.fechaVencimiento) + "</span>" + String.Format("{0:dd/MM/yyyy}", t.fechaVencimiento) + "</td>";
                result += "<td>" + t.cuota + "</td>";
                result += "<td><a onclick='javascript:eliminarCuota(" + t.idCuota + ");'><span id='icon-25' class='eliminar azul' title='Eliminar'></span></a> | <a onclick='javascript:obtenerCuota(" + t.idCuota + ");'><span id='icon-25' class='modificar azul' title='Modificar'></span></a></td>";
                result += "</tr>";
            }

            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='6'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>";
        }
        else
        {
            result += "<div id='divTblApoyo123'><div align='center' style='font-size: 10px !important;margin-left: 25%;' class='bg-alert width50'><div style='width:10%' class='left'><span id='icon-25' class='blanco warning'></span></div><div style='width:90%; text-align: justify; padding-top:10px; padding-bottom:10px'>No se encontraron resultados.</div></div></div>°";
        }

        return result;
    }


    /// <summary>
    /// Metodo para envio de correo electronico, cuando se ha modificado el responsable de un sistema
    /// </summary>
    /// <param name="idCuota"></param>
    /// <param name="idERPGrupoSistema"></param>
    /// <returns>bool</returns>
    public bool enviarCorreoModificacionCuota(int idCuota, int idERPGrupoSistema)
    {
        ControllerCuotasSistemas cs = new ControllerCuotasSistemas();

        var query = (from cuota in erp.view_CuotasGrupSis
                     where cuota.idCuota == idCuota
                     select cuota).Single();

        var responsables = (from resp in erp.view_ResponsableSistema
                            where resp.idERPGrupoSistema == idERPGrupoSistema
                            select resp).ToList();

        string responsa = "";
        List<string> user = new List<string>();
        if (responsables.Count != 0)
        {
            foreach (view_ResponsableSistema tr in responsables)
            {
                responsa += "<li>" + tr.nombreCompleto + "</li>";
                user.Add(tr.idEmpleado);
            }
        }
        else
        {
            responsa += "No hay personal responsable para este sistema.";
        }
        string asunto = "Asignación de Personal Responsable";
        string rutaLogo = HttpContext.Current.Server.MapPath("..\\..\\images\\logos\\logoNAD.png");
        string cuerpo = "<h3 style='margin-left: 4%;'>Asignación de Personal Responsable </h3>" +
                        "<center>" +
                        "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='" + rutaLogo + "' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                "<td style='border:none;' colspan='6' align='right' ><font size='4.5em'> <b> Grupo: " + query.nomGrupo + " </b> </font> </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td rowspan='2' colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Sistema</b></td>" +
                                "<td rowspan='2' colspan='2' > " + query.nomSistema + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Fecha Inicio</b></td>" +
                                "<td colspan='2' > " + query.fechaInicio + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Fecha Termino</b></td>" +
                                "<td colspan='2' > " + query.fechaVencimiento + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Responsable(s)</b></td>" +
                                "<td colspan='6' > " + responsa + "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br/>" +
                        "</center>";
        int tam = user.Count;
        string correo = "";
        for (int i = 0; i < tam; i++)
        {
            correo += cs.getCorreo(user[i]) + ";";
        }
        return utileria.sendMail("seraro8@gmail.com;", asunto, cuerpo, 1);
    }
}