using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for ControllerCuotasSistemas
/// </summary>
public class ControllerCuotasSistemas
{
    private ERPManagementDataContext erp;
    private Utileria utileria;
    public ControllerCuotasSistemas()
    {
        erp = new ERPManagementDataContext();
        utileria = new Utileria();
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Metódo para insertar configuración de llos sistemas solicitados en una 
    /// Petición de ERP
    /// </summary>
    /// <param name="Arreglo(datos)"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>bool</returns>
    public bool insertarConfiguracion(string datos, int idReporte, int idUsuario, string comentario, string nombreArchivo, int calificacion)
    {
        string[] sistema = datos.Split('|');
        string[] cuota;
        string[] quitarResp;
        string[] responsable;
        string[] res;
        string[] quitarTipoM;
        string idSistema = "";
        bool banderaRes = false;
        bool banderaCuo = false;
        bool bandera = false;
        string[] ter;
        tResponsableSistema tresponsable;
        tCuotas tCuota;
        try
        {
            for (int i = 0; i < sistema.Length; i++)
            {
                try
                {
                    ter = sistema[i].Split(':');
                    idSistema = ter[3];

                    var update = (from tsis in erp.tERPGrupoSistema
                                  where tsis.idSistema == int.Parse(idSistema) && tsis.idReporte == idReporte
                                  select tsis.idERPGrupoSistema).Single();

                    responsable = sistema[i].Split(',');
                    if (responsable[0] != "")
                        for (int ins = 0; ins < responsable.Length - 1; ins++)
                        {
                            tresponsable = new tResponsableSistema();
                            res = responsable[ins].Split(',');
                            tresponsable.idResponsable = int.Parse(res[0]);
                            tresponsable.idERPGrupoSistema = update;
                            erp.tResponsableSistema.InsertOnSubmit(tresponsable);
                            banderaRes = true;
                        }
                    else
                    {
                        banderaRes = true;
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                    banderaRes = false;
                }
            }
            for (int i = 0; i < sistema.Length; i++)
            {
                try
                {
                    ter = sistema[i].Split(':');
                    idSistema = ter[3];

                    var idERPGrupoSistema = (from id in erp.tERPGrupoSistema
                                             where id.idReporte == idReporte && id.idSistema == int.Parse(idSistema)
                                             select id.idERPGrupoSistema).Single();

                    cuota = sistema[i].Split(':');
                    quitarResp = cuota[0].Split(',');
                    string coma = ",";
                    while (cuota[0].Contains(coma))
                    {
                        quitarResp = cuota[0].Split(',');
                        cuota[0] = cuota[0].Replace(quitarResp[0] + ",", "");
                    }
                    tCuota = new tCuotas();
                    double cantidadCuota = double.Parse(cuota[0]);
                    quitarTipoM = cuota[1].Split('_');
                    DateTime fechaI = Convert.ToDateTime(quitarTipoM[1]);
                    DateTime fechaF = Convert.ToDateTime(cuota[2]);
                    tCuota.fechaInicio = fechaI;
                    tCuota.fechaVencimiento = fechaF;
                    tCuota.cuota = cantidadCuota;
                    tCuota.idMoneda = int.Parse(quitarTipoM[0]);
                    tCuota.idEstatus = 1;
                    tCuota.idERPGrupoSistema = idERPGrupoSistema;
                    erp.tCuotas.InsertOnSubmit(tCuota);
                    banderaCuo = true;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                    banderaCuo = false;
                }
            }
            if (banderaRes == true && banderaCuo == true)
            {

                if (validaPeticionERP(idReporte, idUsuario, comentario, nombreArchivo, calificacion))
                {
                    erp.SubmitChanges();
                    bandera = true;
                }
                else
                {
                    bandera = false;
                }

            }
            return bandera;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            bandera = false;
            return bandera;
        }
    }

    /// <summary>
    /// Metódo para validar la petición de ERP
    /// Se invoca cuando se inserta la configuración de los sistemas
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>bool</returns>
    public bool validaPeticionERP(int idReporte, int idUsuario, string comentario, string nombreArchivo, int calificacion)
    {
        try
        {
            tRespuestaReporte respuesta = new tRespuestaReporte();
            string fechaRespuesta = DateTime.Now.ToShortDateString();
            DateTime fechaR = Convert.ToDateTime(fechaRespuesta);
            //se asignan los parametros para realizar el insert tabla tRespuetaReporte
            respuesta.idReporte = idReporte;
            respuesta.comentario = comentario;
            respuesta.idEstadoReporte = 5;
            respuesta.evidencia = nombreArchivo;
            respuesta.fechaRespuesta = fechaR;
            respuesta.idUsuario = idUsuario;
            erp.tRespuestaReportes.InsertOnSubmit(respuesta);
            erp.SubmitChanges();
            int idRespuestaReporte = respuesta.idRespuestaReporte;
            //se realiza el update en la tabla tReporte.
            var queryUpdateReporte = (from r in erp.tReporte
                                      where r.idReporte == idReporte
                                      select r).First();
            queryUpdateReporte.idEstatusReporte = 5;
            queryUpdateReporte.calificacion = calificacion;
            erp.SubmitChanges();

            byte? estatusCorreo = 1;

            if (enviaCorreoValidaPeticionERP(idUsuario, idReporte, idRespuestaReporte))
            {
                estatusCorreo = 2;
            }

            respuesta.idEstatusCorreo = estatusCorreo;
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
    /// Metodo par enviar correo electronico cuando se insertó la configuración de los sistemas
    /// correctamente
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <param name="idReporte"></param>
    /// <param name="idRespuestaReporte"></param>
    /// <returns>bool</returns>
    public bool enviaCorreoValidaPeticionERP(int idUsuario, int idReporte, int idRespuestaReporte)
    {
        ControllerUsuarios cu = new ControllerUsuarios();
        Utileria utileria = new Utileria();
        //string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
        //string sArchivoAdjunto = "";

        string asunto,
               cuerpo,
               correo;

        string nGrupo;
        var reporte = (from trep in erp.vCorreoUsuarioIncidencias
                       where trep.idReporte == idReporte
                       select trep).SingleOrDefault();

        string nombreGrupo = "";
        asunto = "Configuración de Sistemas ERP";
        cuerpo = "<h3 style='color:black'>" + reporte.nombreUsaurioCreador + " configur&oacute; los sistema(s) " + reporte.folio + "</h3>";


        var nomGrupo = (from ng in erp.view_PeticionSistemas
                        where ng.idReporte == idReporte
                        select new { ng.nomGrupo }).FirstOrDefault();

        nGrupo = nomGrupo.nomGrupo;


        string sistemas = "<table width='99%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='3'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                    "<td style='border:none;' colspan='3' align='right' ><font size='4.5em'> <b> Grupo: " + nGrupo + " </b> </font> </td>" +
                            "</tr>" +
                            "<tr style='background: #145a7a' align=middle>" +
                                "<td width='20%'><h4 style='color:white'>Sistema</h4></td> " +
                                "<td width='35%'><h4 style='color:white'>Responsable(s)</h4></td>" +
                                "<td width='15%'><h4 style='color:white'>Cuota</h4></td>" +
                                "<td width='15%'><h4 style='color:white'>Tipo Moneda</h4></td>" +
                                "<td width='15%'><h4 style='color:white'>Fecha Inicio</h4></td>" +
                                "<td width='15%'><h4 style='color:white'>Fecha Termino</h4></td>";

        var querySistemas = (from sistema in erp.tERPGrupoSistema
                             where sistema.idReporte == idReporte
                             select sistema).ToList();
        int? idERPGrupoSist = 0;

        foreach (tERPGrupoSistema gsistema in querySistemas)
        {
            sistemas += "<tr><td>" + gsistema.cSistemas.nomSistema + "</td><td>";

            var obtenerResp = (from resp in erp.view_ResponsableSistema
                               where resp.idReporte == idReporte && gsistema.idSistema == resp.idSistema
                               select resp).ToList();

            var GrupoSistema = (from gs in erp.view_PeticionSistemas
                                where gs.idReporte == idReporte
                                select new { gs.nomGrupo, gs.nomSistema }).First();

            string grupo = GrupoSistema.nomGrupo;
            string sistema = GrupoSistema.nomSistema;

            string personal = "";
            List<string> user = new List<string>();

            if (obtenerResp.Count == 0)
            {
                personal += "<label>No hay personal responsable para el sistema.</label>";
                idERPGrupoSist = gsistema.idERPGrupoSistema;
            }
            else
            {
                foreach (view_ResponsableSistema view in obtenerResp)
                {
                    personal += "<li>" + view.nombreCompleto + "</li>";
                    idERPGrupoSist = view.idERPGrupoSistema;
                    nombreGrupo = view.nomGrupo;
                    user.Add(view.idEmpleado);
                }
            }
            sistemas += personal;
            sistemas += "</td>";
            var obtenerCuota = (from cuota in erp.tCuotas
                                where cuota.idERPGrupoSistema == idERPGrupoSist
                                select cuota).ToList();
            string fecha = "";
            string fechaT = "";
            foreach (tCuotas tc in obtenerCuota)
            {
                sistemas += "<td align='center'>$" + tc.cuota + "</td><td  align='center'>" + tc.cMonedaCuota.nombreMoneda + "</td><td align='center'>" + String.Format("{0:yyyy/MM/dd}", tc.fechaInicio) + "</td><td align='center'>" + String.Format("{0:yyyy/MM/dd}", tc.fechaVencimiento) + "</td>";
                fecha = String.Format("{0:yyyy/MM/dd}", tc.fechaInicio);
                fechaT = String.Format("{0:yyyy/MM/dd}", tc.fechaVencimiento);
            }

            if (user.Count > 0)
            {
                enviaCorreoResponsableSistema(idReporte, personal, fecha, fechaT, grupo, sistema, user);
            }
        }

        sistemas += "</tr></table>";
        cuerpo += sistemas;
        cuerpo += "<br /><label>Si deseas consultar m&aacute;s informaci&oacute;n sobre la Petici&oacute;n  ingresa a </label>" +
                  "<a href='https://www.nadconsultoria.com/ERPManagement/index.aspx' target='_new'>ERPManagement</a>";
        correo = reporte.correoUsuarioResponsable + ";";
        string cuerpos = cuerpo;
      //  string nombreArchivo = "";
        return utileria.sendMail(correo, asunto, cuerpo, 1);
    }


    /// <summary>
    /// Metodo para rechazar petición de ERP, se cambia de estatus en tabla tReporte
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="comentario"></param>
    /// <param name="nombreArchivo"></param>
    /// <param name="fechaProPuesta"></param>
    /// <returns>bool true si se rechazó la peticón correctamente false de lo contrario</returns>
    public bool rechazarERP(int idReporte, string comentario, string nombreArchivo, int idUsuario)
    {

        tRespuestaReporte respuesta = new tRespuestaReporte();
        try
        {
            var queryUpdateReporte = (from r in erp.tReporte
                                      where r.idReporte == idReporte
                                      select r).First();
            queryUpdateReporte.idEstatusReporte = 4;
            erp.SubmitChanges();

            string fechaRespuesta = DateTime.Now.ToShortDateString();
            DateTime fechaR = Convert.ToDateTime(fechaRespuesta);
            respuesta.idReporte = idReporte;
            respuesta.comentario = comentario;
            respuesta.idEstadoReporte = 4;
            respuesta.evidencia = nombreArchivo;
            respuesta.fechaRespuesta = fechaR;
            respuesta.idUsuario = idUsuario;
            erp.tRespuestaReportes.InsertOnSubmit(respuesta);
            erp.SubmitChanges();

            int idRespuestaReporte = respuesta.idRespuestaReporte;

            byte? estatusCorreo = 1;

            if (enviaCorreoRechazoPeticionERP(idReporte, idUsuario, idRespuestaReporte))
            {
                estatusCorreo = 2;
            }
            respuesta.idEstatusCorreo = estatusCorreo;
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
    /// Metodo para enviar correo, cuando se rechaza una incidencia
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <param name="idRespuestaReporte"></param>
    /// <returns>bool  (true enviado, false no enviado)</returns>
    public bool enviaCorreoRechazoPeticionERP(int idReporte, int idUsuario, int idRespuestaReporte)
    {
        ControllerUsuarios cu = new ControllerUsuarios();
        Utileria utileria = new Utileria();
        string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
        string sArchivoAdjunto = "";

        string asunto,
               correo;

        var correoUsuario = (from trep in erp.vCorreoUsuarioIncidencias
                             where trep.idReporte == idReporte
                             select trep).SingleOrDefault();

        var reporte = (from rp in erp.view_DetalleReporte
                       where rp.idReporte == idReporte
                       select rp).Single();

        var queryidRespuesta = (from tRespuesta in erp.tRespuestaReportes
                                where tRespuesta.idRespuestaReporte == idRespuestaReporte
                                select tRespuesta).Single();

        var querySistemas = (from sistema in erp.tERPGrupoSistema
                             where sistema.idReporte == idReporte
                             select sistema).ToList();
        string sistemas = "";
        foreach (tERPGrupoSistema gsistema in querySistemas)
        {
            sistemas += "<li style='padding-left:2%'>" + gsistema.cSistemas.nomSistema + "</li>";
        }
        string nombreArchivo = queryidRespuesta.evidencia;

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


        asunto = "Rechazo de Petición " + reporte.folio + "";
        string cuerpo = "<h3 style='color:black'>" + correoUsuario.nombreUsuarioResponsable + " rechazo la petici&oacute;n " + reporte.folio + "</h3>" +
                        "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                "<td style='border:none;' colspan='6' align='right' ><font size='4.5em'> <b> Grupo: " + reporte.nomGrupo + " </b></font> </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                "<td colspan='2' > " + reporte.folio + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Tipo Reporte</b></td>" +
                                "<td colspan='2' > " + reporte.nombreTipoReporte + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Prioridad</b></td>" +
                                "<td colspan='3' > " + reporte.nombrePrioridad + " </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='1' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Sistema(s) </b> </td>" +
                                "<td colspan='11'> " + sistemas + " </td>" +
                             "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                "<td colspan='10'> " + reporte.asunto + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b>Comentario Respuesta </b> </td>" +
                                "<td colspan='10'> " + queryidRespuesta.comentario + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                            "</tr>" +
                            sArchivoAdjunto+
                            "<tr align=middle>" +
                                "<td style='border:none;' colspan='10' > <center><h4>" +
                                    "<br/><label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver m&aacute;s detalles de la petici&oacute;n</label></h4>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br/>";

        correo = correoUsuario.correoUsuarioResponsable + ";";

      //  return utileria.sendMail(correo, asunto, cuerpo, queryidRespuesta.evidencia, 1);
       return utileria.sendMail(correo, asunto, cuerpo, 1);
    }

    /// <summary>
    /// Enviar correo electronico a los responsables por sistema.
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="responsa"></param>
    /// <param name="fechaI"></param>
    /// <param name="fechat"></param>
    /// <param name="grupo"></param>
    /// <param name="nomSistema"></param>
    /// <param name="user"></param>
    /// <returns>bool</returns>
    public bool enviaCorreoResponsableSistema(int idReporte, string responsa, string fechaI, string fechat, string grupo, string nomSistema, List<string> user)
    {
        string asunto = "Asignación de Personal Responsable";
        string cuerpo = "<h3 style='margin-left: 4%;'>Asignación de Personal Responsable </h3>" +
                        "<center>" +
                        "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                "<td style='border:none;' colspan='6' align='right' ><font size='4.5em'> <b> Grupo: " + grupo + " </b> </font> </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td rowspan='2' colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Sistema</b></td>" +
                                "<td rowspan='2' colspan='2' > " + nomSistema + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Fecha Inicio</b></td>" +
                                "<td colspan='2' > " + fechaI + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Fecha Termino</b></td>" +
                                "<td colspan='2' > " + fechat + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Responsable(s)</b></td>" +
                                "<td colspan='6' > " + responsa + "</td>" +
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
                            "<tr>" +
                                "<td style='border:none;' colspan='12' > <center><h4>" +
                                    "<label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver m&aacute;s detalles de la petici&oacute;n</label></h4>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br/>" +
                        "</center>";
        int tam = user.Count;
        string correo = "";
        for (int i = 0; i < tam; i++)
        {
            correo += getCorreo(user[i]) + ";";
        }

        return utileria.sendMail(correo, asunto, cuerpo, 1);
    }

    public string getCorreo(string idUsuario)
    {
        var usuario = (from vUsu in erp.vUsuariosERPM
                       where vUsu.idEmpleado.Equals(idUsuario)
                       select vUsu.correoElectronico).SingleOrDefault();
        return usuario;
    }
}

