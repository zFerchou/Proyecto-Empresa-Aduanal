using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
/// <summary>
/// Summary description for ControllerRespuestaReporte
/// </summary>
public class ControllerRespuestaReporte
{
    //se crean las instancias de los objetos para acceder a ellos 
    private ERPManagementDataContext erp;
    private view_RespuestaReporte viewRespuesta;
    private tRespuestaReporte respuesta;
    private tReporte reporte;
    private tResponsableReporte trp;
    private Utileria utileria;
    public ControllerRespuestaReporte()
    {
        //
        // TODO: Add constructor logic here
        //
        //se inicializan
        erp = new ERPManagementDataContext();
        respuesta = new tRespuestaReporte();
        reporte = new tReporte();
        trp = new tResponsableReporte();
        utileria = new Utileria();
    }

    /// <summary>
    /// Metodo para consultar el detalle de un reporte
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>Objeto view_RespuestaReporte</returns>
    public view_RespuestaReporte consultaReporteValidar(int idReporte)
    {
        view_RespuestaReporte queryConsultaReportes = new view_RespuestaReporte();

        var queryMax = (from maximo in erp.view_RespuestaReportes
                        where maximo.idReporte == idReporte
                        select maximo.idRespuestaReporte).Max();

        queryConsultaReportes = (from viewRespuesta in erp.view_RespuestaReportes
                                 where viewRespuesta.idReporte == idReporte && viewRespuesta.idRespuestaReporte == queryMax
                                 select viewRespuesta).SingleOrDefault();

        return queryConsultaReportes;
    }

    /// <summary>
    /// Metodo para insertar comentario de un reporte validado
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="comentario"></param>
    /// <param name="nombreArchivo"></param>
    /// <returns>bool</returns>
    public bool insertaComentario(int idReporte, string comentario, string nombreArchivo, int idUsuario, int cal)
    {
        try
        {
            storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
            string query = "";
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
            queryUpdateReporte.calificacion = cal;
            erp.SubmitChanges();

            var LogTicketSprint = (from t in erp.tTicketSprint
                                   where t.idReporte == idReporte
                                   select t).FirstOrDefault();

            if (LogTicketSprint != null)
            {
                query = "UPDATE tTicketSprint SET iEstatus = 5 where idReporte =" + idReporte;
                sp.ejecutaSQL(query);
            }

            byte? estatusCorreo = 1;

            if (enviarCorreoRespuestaValidado(idReporte, idUsuario, idRespuestaReporte))
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
    /// Metodo para insertar reporte rechazado, se cambia de estatus en tabla tReporte
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="comentario"></param>
    /// <param name="nombreArchivo"></param>
    /// <param name="fechaProPuesta"></param>
    /// <returns>bool</returns>
    public bool insertaComentarioRechazo(int idReporte, string comentario, string nombreArchivo, int idUsuario)
    {
        try
        {
            storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
            string query = "";
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

            var LogTicketSprint = (from t in erp.tTicketSprint
                                   where t.idReporte == idReporte
                                   select t).FirstOrDefault();

            if (LogTicketSprint != null)
            {
                query = "UPDATE tTicketSprint SET iEstatus = 2 where idReporte =" + idReporte;
                sp.ejecutaSQL(query);
            }

            if (enviarCorreoRespuestaRechazo(idReporte, idUsuario, idRespuestaReporte))
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

    public tReporte obtenerDetalleReporte(int idReporte)
    {
        reporte = new tReporte();
        erp = new ERPManagementDataContext();

        var queryConsulta = (from t in erp.tReporte
                             where t.idReporte == idReporte
                             select t).First();
        return queryConsulta;
    }

    /// <summary>
    /// Metodo para insertar la respuesta de la incidencia
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="comentario"></param>
    /// <param name="nombreArchivo"></param>
    /// <param name="idUsuario"></param>
    /// <param name="urlERP"></param>
    /// <returns>bool</returns>
    public bool insertaComentarioRespuesta(int idReporte, string comentario, string nombreArchivo, int idUsuario, string urlERP)
    {
        try
        {
            storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
            string query = "";
            //se asignan los parametros para realizar el insert tabla tRespuetaReporte
            string fechaRespuesta = DateTime.Now.ToShortDateString();
            DateTime fechaR = DateTime.Now;// Convert.ToDateTime(fechaRespuesta);
            respuesta.idReporte = idReporte;
            respuesta.comentario = comentario;
            respuesta.idEstadoReporte = 3;
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
            queryUpdateReporte.idEstatusReporte = 3;
            erp.SubmitChanges();

            var LogTicketSprint = (from t in erp.tTicketSprint
                                   where t.idReporte == idReporte
                                   select t).FirstOrDefault();

            if (LogTicketSprint != null)
            {
                query = "UPDATE tTicketSprint SET iEstatus = 3 where idReporte =" + idReporte;
                sp.ejecutaSQL(query);
            }

            if (queryUpdateReporte.idTipoReporte == 3 && urlERP != "")
            {
                var queryUpdate = (from url in erp.tERPGrupoSistema
                                   where url.idReporte == idReporte
                                   select url.idERPGrupo
                                   ).First();

                var queryUpdateUrl = (from terp in erp.tERPGrupo
                                      where terp.idERPGrupo == queryUpdate
                                      select terp).First();

                queryUpdateUrl.urlERP = urlERP;
                erp.SubmitChanges();
            }
            byte? estatusCorreo = 1;
            if (queryUpdateReporte.idTipoReporte == 3)
            {
                if (enviarCorreoRespuestaERP(idUsuario, idReporte, idRespuestaReporte))
                {
                    estatusCorreo = 2;
                }
            }
            if (queryUpdateReporte.idTipoReporte != 3)
            {
                if (enviarCorreoRespuesta(idReporte, idUsuario, idRespuestaReporte))
                {
                    estatusCorreo = 2;
                }
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

    public bool asignarReporte(int idReporte, DateTime fechaPropuestaTermino, int idUsuario, string comentario, string personalApoyo)
    {
        string[] personalA;
        int cantidad;
        try
        {
            trp.idReporte = idReporte;

            trp.idResponsable = idUsuario;
            trp.idTipoResponsable = 1;
            erp.tResponsableReporte.InsertOnSubmit(trp);

            storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
            string query = "";

            var queryUpdateReporte = (from r in erp.tReporte
                                      where r.idReporte == idReporte
                                      select r).First();

            //verificamos si existe en tablas lñog de sprint

            var LogTicketSprint = (from t in erp.tTicketSprint
                                   where t.idReporte == idReporte
                                   select t).FirstOrDefault();

            if(LogTicketSprint != null)
            {
                query = "UPDATE tTicketSprint SET iEstatus = 2, idResponsable ="+idUsuario+" where idReporte =" + idReporte;
                sp.ejecutaSQL(query);
            }

            DateTime fechaPopuestaT = Convert.ToDateTime(fechaPropuestaTermino);
            queryUpdateReporte.idEstatusReporte = 2;
            queryUpdateReporte.fechaPropuestaTermino = fechaPopuestaT;

            tRespuestaReporte tRespuestaR = new tRespuestaReporte();
            //string fechaRespuesta = DateTime.Now.ToShortDateString();
            DateTime fechaR = DateTime.Now;
            tRespuestaR.idReporte = idReporte;
            tRespuestaR.fechaRespuesta = fechaR;
            tRespuestaR.idEstadoReporte = 2;
            tRespuestaR.idUsuario = idUsuario;
            //tRespuestaR.idEstatusCorreo = estatusCorreo;
            tRespuestaR.comentario = comentario;
            tRespuestaR.evidencia = "";
            erp.tRespuestaReportes.InsertOnSubmit(tRespuestaR);
            if (personalApoyo != "")
            {
                personalA = personalApoyo.Split(',');
                cantidad = personalA.Length;
                for (int i = 0; i < cantidad; i++)
                {
                    tResponsableReporte tresp = new tResponsableReporte();
                    tresp.idReporte = idReporte;
                    tresp.idTipoResponsable = 2;
                    tresp.idResponsable = int.Parse(personalA[i]);
                    erp.tResponsableReporte.InsertOnSubmit(tresp);
                }
            }

            erp.SubmitChanges();

            byte? estatusCorreo = 1;

            if (queryUpdateReporte.idTipoReporte == 3)
            {
                if (enviarCorreoAsignacionERP(idUsuario, idReporte, tRespuestaR.idRespuestaReporte))
                {
                    estatusCorreo = 2;
                }

                tRespuestaR.idEstatusCorreo = estatusCorreo; ;
            }

            if (queryUpdateReporte.idTipoReporte != 3)
            {
                if (enviarCorreo(idUsuario, idReporte, tRespuestaR.idRespuestaReporte))
                {
                    estatusCorreo = 2;
                }

                tRespuestaR.idEstatusCorreo = estatusCorreo;

            }

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
    /// Metodo para obtener Responsable
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <returns>un objeto de vUsuariosERPM</returns>
    public vUsuariosERPM obtenerResponsable(int idUsuario)
    {
        string id = idUsuario.ToString();

        erp = new ERPManagementDataContext();
        vUsuariosERPM vistaUsuario = new vUsuariosERPM();

        var resultado = (from vu in erp.vUsuariosERPM
                         where vu.idEmpleado == id
                         select vu).First();

        return resultado;
    }

    /// <summary>
    /// Metodo para consultar el detalle de una incidencia
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>Objeto view_Reporte</returns>
    public view_Reporte consultaReporteUsuario(int idReporte, string sFolioV = "")
    {
        view_Reporte queryConsulta = new view_Reporte();

        if (idReporte == 0)
        {
            var queryidReporte = (from exists in erp.tReporte
                               where exists.folio== sFolioV
                               select exists.idReporte).First();
            idReporte = queryidReporte;
        }
        //Consulta para verificar si existe en tRespuestaReporte la incidencia con estatus de 4 de recahazado
        var queryExists = (from exists in erp.view_RespuestaReportes
                           where exists.idReporte == idReporte && exists.idEstadoReporte != 2
                           select exists).Count();

        var queryExistsEv = (from reporte in erp.tRespuestaReportes
                             where reporte.idReporte == idReporte
                             select reporte).Count();


        //Si el resultado de la consulta es mayor es a 0
        if (queryExists > 0)
        {
            //se realiza una consulta para obtener el maximo de tRespuestaReporte donde el estatus del reporte sea 4
            var queryMax = (from max in erp.view_RespuestaReportes
                            where max.idReporte == idReporte && max.comentario != ""
                            select max.idRespuestaReporte).Max();

            //se obtiene el comentario donde el estatus sea iagual a 4
            var queryConsultaM = (from consultam in erp.view_RespuestaReportes
                                  where consultam.idRespuestaReporte == queryMax
                                  select consultam).FirstOrDefault();

            //se crea un objeto para poder igualar el resultado la consulta (comentario)
            queryConsulta = (from consulta in erp.view_Reporte
                             where consulta.idReporte == idReporte
                             select consulta).FirstOrDefault();

            //se igual el resultado de la consulta para poder devolver el ultimo comentario
            if (queryConsultaM.comentario != "")
            {
                queryConsulta.descripcion = queryConsultaM.comentario;
            }
            if (queryExistsEv > 0)
            {
                var queryArchivo = (from archivo in erp.tRespuestaReportes
                                    where archivo.evidencia != "" && archivo.idReporte == idReporte
                                    select archivo).Count();

                if (queryArchivo != 0)
                {
                    var idRespuesta = (from ev in erp.tRespuestaReportes
                                       where ev.idReporte == idReporte && ev.evidencia != ""
                                       select ev.idRespuestaReporte).Max();

                    var queryExistsEvidencia = (from ev in erp.tRespuestaReportes
                                                where ev.idRespuestaReporte == idRespuesta
                                                select ev.evidencia).Max();

                    if (queryExistsEvidencia != "")
                    {
                        queryConsulta.evidencia = queryExistsEvidencia;
                    }
                }
            }

            if (queryConsulta.idTipoReporte == 3)
            {
                var queryUpdate = (from url in erp.tERPGrupoSistema
                                   where url.idReporte == idReporte
                                   select url.idERPGrupo
                                       ).First();

                var queryUpdateUrl = (from terp in erp.tERPGrupo
                                      where terp.idERPGrupo == queryUpdate && terp.urlERP != null
                                      select terp).First();
                queryConsulta.asunto = queryUpdateUrl.urlERP;
            }
            //se retorna el objeto de tipo view_Reporte
            return queryConsulta;
        }
        else
        {
            //si no existe se realiza la consulta y se retorna el objeto
            queryConsulta = (from consulta in erp.view_Reporte
                             where consulta.idReporte == idReporte
                             select consulta).First();

            if (queryConsulta.idTipoReporte == 3)
            {
                var queryUpdate = (from url in erp.tERPGrupoSistema
                                   where url.idReporte == idReporte
                                   select url.idERPGrupo
                                       ).First();

                var queryUpdateUrl = (from terp in erp.tERPGrupo
                                      where terp.urlERP != null && terp.idERPGrupo == queryUpdate
                                      select terp).FirstOrDefault();

                if (queryUpdateUrl != null)
                {
                    queryConsulta.asunto = queryUpdateUrl.urlERP;
                }
                else
                {
                    queryConsulta.asunto = "";
                }
            }
            if (queryExistsEv > 0)
            {
                var queryExistsEvidencia = (from ev in erp.tRespuestaReportes
                                            where (ev.evidencia != "" || ev.evidencia != null) && ev.idReporte == idReporte
                                            select ev.evidencia).Max();

                if (queryExistsEvidencia != "")
                {
                    queryConsulta.evidencia = queryExistsEvidencia;
                }
            }
            return queryConsulta;
        }
    }

    /// <summary>
    /// Metodo para obtener responsable y personal de apoyo de una incidencia
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="tipoUsuario"></param>
    /// <returns>List<String></returns>
    public List<string> obtenerResponsableReporte(int idReporte, int tipoUsuario)
    {
        ControllerUsuarios cUsuarios = new ControllerUsuarios();
        List<string> apoyo = new List<string>();
        var queryResponsable = (from resp in erp.tResponsableReporte
                                where resp.idReporte == idReporte && resp.idTipoResponsable == 1
                                select resp).First();

        apoyo.Add(cUsuarios.getUsuarioById(queryResponsable.idResponsable));
        //Valida el tipo de usario que esta consultando la incidencia
        //si tipoUsuario==1 solo regresa el responsable directo de la incidencia
        if (tipoUsuario == 1)
        {
            return apoyo;
        }
        //si tipoUsuario!=2 retorna ademas del responsable el personal de apoyo
        else
        {
            var queryApoyo = (from resp in erp.tResponsableReporte
                              where resp.idReporte == idReporte && resp.idTipoResponsable == 2
                              select resp).ToList();

            foreach (tResponsableReporte tResponsable in queryApoyo)
            {
                apoyo.Add(cUsuarios.getUsuarioById(tResponsable.idResponsable));
            }
            return apoyo;
        }
    }

    /// <summary>
    /// Metodo para obtener el nombre de usuario en sesion
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <returns>string (Nombre Usuario)</returns>
    public string obtnerUsuarioSession(int idUsuario)
    {
        string id = idUsuario.ToString();
        string nombre;

        var resultado = (from vu in erp.vUsuariosERPM
                         where vu.idEmpleado == id
                         select vu).First();

        nombre = resultado.nombreCompleto;

        return nombre;
    }

    /// <summary>
    /// Metodo para enviar correo electronico, cuando se ha asignado una incidencia
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <param name="idReporte"></param>
    /// <returns>bool  (true enviado, false no enviado)</returns>
    public bool enviarCorreo(int idUsuario, int idReporte, int idRespuestaReporte)
    {
        ControllerUsuarios cu = new ControllerUsuarios();
        utileria = new Utileria();
        string asunto,
               correo;
        string sistema = "";
        string comentarioAsignacion = "";
        string nombreArchivo = "";
        string tipoSoporte = "la incidencia: ";
        var reporte = (from trep in erp.vCorreoUsuarioIncidencias
                       where trep.idReporte == idReporte
                       select trep).SingleOrDefault();

        var infReporte = (from rp in erp.tReporte
                          where rp.idReporte == idReporte
                          select rp).FirstOrDefault();

        var nombreGrupo = (from ng in erp.view_DetalleReporte
                           where ng.idReporte == idReporte
                           select new { ng.nomGrupo, ng.nomSistema }).FirstOrDefault();

        /*Datos de los responsables */
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string query = "";

        query = "select distinct correoElectronico, nombreCompleto from tResponsableReporte tr " +
                    " join vUsuariosERPMGrupos tc on tc.idEmpleado=tr.idResponsable " +
                    " where idReporte=" + reporte.idReporte + " and idTipoCorreoElectronico=1";

        List<string> interesados = sp.recuperaRegistros(query);

        /* Asignados */
        string asignados = "";
        string emailAsig = "";

        if (interesados.Count > 0)
        {
            asignados = "<h4>";
            for (int i = 0; i < interesados.Count; i = i + 2)
            {
                emailAsig += "" + interesados[i] + ";";
                asignados += "" + interesados[i + 1] + " <br/>";
            }
            asignados += "</h4>";
        }
        else
        {
            emailAsig = reporte.correoUsuarioResponsable + ";";
            asignados = "<h4>" + reporte.nombreUsuarioResponsable + " </h4>"; 
        }
        /* */


        if (infReporte.idArea != 0)
        {
            ERPManagementRHDataContext erpRH = new ERPManagementRHDataContext();

            var area = (from nombreArea in erpRH.view_ValidarTipoUsuarioAreaGrupo
                        where nombreArea.idUsuario == infReporte.idUsuario
                        select nombreArea.area).FirstOrDefault();

            sistema = area;
            tipoSoporte = "el soporte: ";
        }
        else
        {
            sistema = nombreGrupo.nomSistema;
        }
        var respuestar = (from resp in erp.tRespuestaReportes
                          where resp.idReporte == idReporte && resp.idEstadoReporte == 2 && resp.idRespuestaReporte == idRespuestaReporte
                          select new { resp.comentario }).Single();

        if (respuestar.comentario != "")
        {
            comentarioAsignacion = "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Comentario Asignaci&oacute;n</b> </td>" +
                                "<td colspan='10'> " + respuestar.comentario + " </td>";
        }

        asunto = "Asignación de "+tipoSoporte+" " + reporte.folio + "";
        string cuerpo = "<h3>Se ha asignado " + tipoSoporte + "" + infReporte.folio + " a: </h3>" + asignados +
                        //"<h3>" + reporte.nombreUsuarioResponsable + " se ha asignado " + tipoSoporte + "" + infReporte.folio + "</h3>" +
                        "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                "<td style='border:none;' colspan='6' align='right' ><font size='4.5em'> <b> Grupo: " + nombreGrupo.nomGrupo + " </b></font> </td>" +
                            "</tr>" +
                            "<tr >" +

                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                "<td colspan='2' > " + infReporte.folio + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Prioridad</b></td>" +
                                "<td colspan='2' > " + infReporte.cPrioridadReporte.nombrePrioridad + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Tipo Reporte</b></td>" +
                                "<td colspan='2' > " + infReporte.cTipoReporte.nombreTipoReporte + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Sistema </b> </td>" +
                                "<td colspan='2'> " + sistema + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Fecha Propuesta </b> </td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuesta) + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b> Fecha Termino </b></td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuestaTermino) + " </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                "<td colspan='10'> " + infReporte.asunto + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                   "" + comentarioAsignacion + "" +
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
                                    "<label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver m&aacute;s detalles de la incidencia</label></h4>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br/>";

        correo = reporte.correoUsarioCreador + ";";

        bool verEmail = emailAsig.Contains(correo);

        if (verEmail)
        {/* Ya contiene el correo  */
            correo = emailAsig;
        }
        else
        {
            correo += emailAsig;
        }

        return utileria.sendMail(correo, asunto, cuerpo, 1);
    }
    /// <summary>
    /// Metod para enviar correo electronico de respuesta de una incidencia
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <param name="idRespuestaReporte"></param>
    /// <returns> bool  (true enviado, false no enviado)</returns>
    public bool enviarCorreoRespuesta(int idReporte, int idUsuario, int idRespuestaReporte)
    {
        ControllerUsuarios cu = new ControllerUsuarios();
        utileria = new Utileria();
        string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
        string sArchivoAdjunto = "";

        //bool resultado = false;
        string asunto,
               correo;
        string sistema = "";
        string tipoSoporte = "la incidencia: ";
        string comentarioRespuesta = "";
        var reporte = (from trep in erp.vCorreoUsuarioIncidencias
                       where trep.idReporte == idReporte
                       select trep).SingleOrDefault();

        var queryidRespuesta = (from tRespuesta in erp.tRespuestaReportes
                                where tRespuesta.idRespuestaReporte == idRespuestaReporte
                                select tRespuesta).Single();

        var nombreGrupo = (from ng in erp.view_DetalleReporte
                           where ng.idReporte == idReporte
                           select new { ng.nomGrupo, ng.nomSistema }).Single();

        var infReporte = (from rp in erp.tReporte
                          where rp.idReporte == idReporte
                          select rp).Single();

        if (infReporte.idArea != 0)
        {
            ERPManagementRHDataContext erpRH = new ERPManagementRHDataContext();

            var area = (from nombreArea in erpRH.view_ValidarTipoUsuarioAreaGrupo
                        where nombreArea.idUsuario == infReporte.idUsuario
                        select nombreArea.area).FirstOrDefault();

            sistema = area;
            tipoSoporte = "el soporte: ";
        }
        else
        {
            sistema = nombreGrupo.nomSistema;
        }

        if (queryidRespuesta.comentario != "")
        {
            comentarioRespuesta = "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b>Comentario Respuesta </b> </td>" +
                                "<td colspan='10'> " + queryidRespuesta.comentario + " </td>";
        }
        string nombreArchivo = queryidRespuesta.evidencia;

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



        asunto = "Respuesta de "+tipoSoporte+" " + infReporte.folio + "";
        string cuerpo = "<h3>" + reporte.nombreUsuarioResponsable + " ha respondido  " + tipoSoporte + "" + infReporte.folio + "</h3><br />" +
                        "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                "<td style='border:none;' colspan='6' align='right' ><font size='4.5em'> <b> Grupo: " + nombreGrupo.nomGrupo + " </b></font> </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                "<td colspan='2' > " + infReporte.folio + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Prioridad</b></td>" +
                                "<td colspan='2' > " + infReporte.cPrioridadReporte.nombrePrioridad + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Tipo Reporte</b></td>" +
                                "<td colspan='2' > " + infReporte.cTipoReporte.nombreTipoReporte + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Sistema </b> </td>" +
                                "<td colspan='2'> " + sistema + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Fecha Propuesta </b> </td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuesta) + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b> Fecha Termino </b></td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuestaTermino) + " </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                "<td colspan='10'> " + infReporte.asunto + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                    "" + comentarioRespuesta + "" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                            "</tr>" +

                            sArchivoAdjunto +
                            "<tr>" +
                                "<td style='border:none;' colspan='12' > <center><h4>" +
                                    "<label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver m&aacute;s detalles de la incidencia</label></h4>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br/>";

        correo = reporte.correoUsarioCreador + ";";

        return utileria.sendMail(correo, asunto, cuerpo, 1);
    }
    /// <summary>
    /// Metodo para enviar correo cuando se ha validado la incidencia.
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <param name="idRespuestaReporte"></param>
    /// <returns>bool (true enviado, false no enviado)</returns>
    public bool enviarCorreoRespuestaValidado(int idReporte, int idUsuario, int idRespuestaReporte)
    {
        ControllerUsuarios cu = new ControllerUsuarios();
        utileria = new Utileria();
        string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
        string sArchivoAdjunto = "";

        string asunto,
               correo;
        string sistema = "";
        string tipoSoporte = "de la incidencia: ";
        string tipoSoporteAsunto = "Incidencia";
        string comentarioValidacion = "";
        var reporte = (from trep in erp.vCorreoUsuarioIncidencias
                       where trep.idReporte == idReporte
                       select trep).SingleOrDefault();

        var queryidRespuesta = (from tRespuesta in erp.tRespuestaReportes
                                where tRespuesta.idRespuestaReporte == idRespuestaReporte
                                select tRespuesta).Single();

        var nombreGrupo = (from ng in erp.view_DetalleReporte
                           where ng.idReporte == idReporte
                           select new { ng.nomGrupo, ng.nomSistema }).Single();

        var infReporte = (from rp in erp.tReporte
                          where rp.idReporte == idReporte
                          select rp).Single();

        /*Correo a los responsables */
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string query = "";

        query = "select distinct correoElectronico from tResponsableReporte tr "+
                    " join vUsuariosERPMGrupos tc on tc.idEmpleado=tr.idResponsable "+
                    " where idReporte=" + reporte .idReporte+ " and idTipoCorreoElectronico=1";

        List<string> interesados = sp.recuperaRegistros(query);
        



        if (infReporte.idArea != 0)
        {
            ERPManagementRHDataContext erpRH = new ERPManagementRHDataContext();

            var area = (from nombreArea in erpRH.view_ValidarTipoUsuarioAreaGrupo
                        where nombreArea.idUsuario == infReporte.idUsuario
                        select nombreArea.area).FirstOrDefault();

            sistema = area;
            tipoSoporte = "del soporte: ";
            tipoSoporteAsunto = "Soporte";
        }
        else
        {
            sistema = nombreGrupo.nomSistema;
        }

        if (queryidRespuesta.comentario != "")
        {
            comentarioValidacion = "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b>Comentario Respuesta </b> </td>" +
                                "<td colspan='10'> " + queryidRespuesta.comentario + " </td>";
        }
        string nombreArchivo=queryidRespuesta.evidencia;

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

        asunto = "Validación "+tipoSoporteAsunto+" " + reporte.folio;
        string cuerpo = "<h3>" + reporte.nombreUsaurioCreador + " ha validado la respuesta " + tipoSoporte + "" + reporte.folio + "</h3><br />" +
                        "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                "<td style='border:none;' colspan='6' align='right' ><font size='4.5em'> <b> Grupo: " + nombreGrupo.nomGrupo + " </b></font> </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                "<td colspan='2' > " + infReporte.folio + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Prioridad</b></td>" +
                                "<td colspan='2' > " + infReporte.cPrioridadReporte.nombrePrioridad + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Tipo Reporte</b></td>" +
                                "<td colspan='2' > " + infReporte.cTipoReporte.nombreTipoReporte + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Sistema </b> </td>" +
                                "<td colspan='2'> " + sistema + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Fecha Propuesta </b> </td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuesta) + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b> Fecha Termino </b></td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuestaTermino) + " </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                "<td colspan='10'> " + infReporte.asunto + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                    " " + comentarioValidacion + " " +
                            "</tr>" +
                            "<tr>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                            "</tr>" +
                            sArchivoAdjunto +
                            "<tr>" +
                                "<td style='border:none;' colspan='12' > <center><h4>" +
                                    "<label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver m&aacute;s detalles de la incidencia</label></h4>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br/>";
        //correo = reporte.correoUsuarioResponsable + ";";
        correo = "";
        if (interesados.Count > 0)
        {
            for (int i = 0; i < interesados.Count; i = i + 1)
            {
                correo += "" + interesados[i] + ";";
            }
        }
        else
        {
            correo = reporte.correoUsuarioResponsable + ";";
        }

        return utileria.sendMail(correo, asunto, cuerpo, 1);
    }

    /// <summary>
    /// Metodo para enviar correo, cuando se rechaza una incidencia
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <param name="idRespuestaReporte"></param>
    /// <returns>bool  (true enviado, false no enviado)</returns>
    public bool enviarCorreoRespuestaRechazo(int idReporte, int idUsuario, int idRespuestaReporte)
    {
        ControllerUsuarios cu = new ControllerUsuarios();
        utileria = new Utileria();
        string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
        string sArchivoAdjunto = "";
        string asunto,
               correo;
        string sistema = "";
        string tiposoporte = "de la incidencia: ";
        string tipoSoporteAsunto = "Incidencia";
        string comentarioRechazo = "";
        var reporte = (from trep in erp.vCorreoUsuarioIncidencias
                       where trep.idReporte == idReporte
                       select trep).SingleOrDefault();

        var queryidRespuesta = (from tRespuesta in erp.tRespuestaReportes
                                where tRespuesta.idRespuestaReporte == idRespuestaReporte
                                select tRespuesta).Single();

        var nombreGrupo = (from ng in erp.view_DetalleReporte
                           where ng.idReporte == idReporte
                           select new { ng.nomGrupo, ng.nomSistema }).Single();

        var infReporte = (from rp in erp.tReporte
                          where rp.idReporte == idReporte
                          select rp).Single();

        /*Correo a los responsables */
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string query = "";

        query = "select distinct correoElectronico from tResponsableReporte tr " +
                    " join vUsuariosERPMGrupos tc on tc.idEmpleado=tr.idResponsable " +
                    " where idReporte=" + reporte.idReporte + " and idTipoCorreoElectronico=1";

        List<string> interesados = sp.recuperaRegistros(query);

        if (infReporte.idArea != 0)
        {
            ERPManagementRHDataContext erpRH = new ERPManagementRHDataContext();

            var area = (from nombreArea in erpRH.view_ValidarTipoUsuarioAreaGrupo
                        where nombreArea.idUsuario == infReporte.idUsuario
                        select nombreArea.area).FirstOrDefault();

            sistema = area;
            tiposoporte = "del soporte: ";
            tipoSoporteAsunto = "Soporte";
        }
        else
        {
            sistema = nombreGrupo.nomSistema;
        }

        if (queryidRespuesta.comentario != "")
        {
            comentarioRechazo = "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b>Comentario Respuesta </b> </td>" +
                                "<td colspan='10'> " + queryidRespuesta.comentario + " </td>";
        }
        string nombreArchivo=queryidRespuesta.evidencia;

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

        asunto = "Rechazo de "+tipoSoporteAsunto+" " + reporte.folio;
        string cuerpo = "<h3>" + reporte.nombreUsaurioCreador + " ha rechazado la respuesta " + tiposoporte + "" + reporte.folio + "</h3><br />" +
                        "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                "<td style='border:none;' colspan='6' align='right' ><font size='4.5em'> <b> Grupo: " + nombreGrupo.nomGrupo + " </b></font> </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                "<td colspan='2' > " + infReporte.folio + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Prioridad</b></td>" +
                                "<td colspan='2' > " + infReporte.cPrioridadReporte.nombrePrioridad + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Tipo Reporte</b></td>" +
                                "<td colspan='2' > " + infReporte.cTipoReporte.nombreTipoReporte + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Sistema </b> </td>" +
                                "<td colspan='2'> " + sistema + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Fecha Propuesta </b> </td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuesta) + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b> Fecha Termino </b></td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuestaTermino) + " </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                "<td colspan='10'> " + infReporte.asunto + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                    " " + comentarioRechazo + " " +
                            "</tr>" +
                            "<tr>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                            "</tr>" +
                            sArchivoAdjunto +
                            "<tr>" +
                                "<td style='border:none;' colspan='12' > <center><h4>" +
                                    "<label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver m&aacute;s detalles de la incidencia</label></h4>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br/>";

        //correo = reporte.correoUsuarioResponsable + ";";

        correo = "";
        if (interesados.Count > 0)
        {
            for (int i = 0; i < interesados.Count; i = i + 1)
            {
                correo += "" + interesados[i] + ";";
            }
        }
        else
        {
            correo = reporte.correoUsuarioResponsable + ";";
        }

        return utileria.sendMail(correo, asunto, cuerpo, 1);
    }

    /// <summary>
    /// Metodo para para obtener los sistemas solicitados en una peticion de ERP
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>String (Sistemas Solicitados.)</returns>
    public string obtenerSistema(int idReporte)
    {
        string resultado = "";
        string encrIdReporte = utileria.EncodeTo64(idReporte.ToString());
        var querySistemas = (from sistema in erp.tERPGrupoSistema
                             where sistema.idReporte == idReporte
                             select sistema).ToList();

        var Cantidad = (from can in erp.tERPGrupoSistema
                        where can.scriptCreado != 0 && can.idReporte == idReporte
                        select can.idReporte).ToList();

        if (querySistemas.Count == Cantidad.Count)
        {
            resultado += "<input type='hidden' id='txtvalidar' value=1 /><input type='hidden' id='txtEncrIdReporte' value='" + encrIdReporte + "'>";
        }
        else
        {
            resultado += "<input type='hidden' id='txtvalidar' value=0 /><input type='hidden' id='txtEncrIdReporte' value='" + encrIdReporte + "'>";
        }

        foreach (tERPGrupoSistema gsistema in querySistemas)
        {
            resultado += "<label class='divBlancoSistemasI'  title='" + gsistema.cSistemas.nomSistema + "' id=" + gsistema.idSistema + " style='background-color: #145A7A !important; color:#FFFFFF !important;'>" + gsistema.cSistemas.nomenglatura + "</label>";
        }

        return resultado;
    }

    /// <summary>
    /// Metodo para enviar correo electronico, cuando se asigna una peticion de ERP.
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <param name="idReporte"></param>
    /// <returns>bool  (true enviado, false no enviado)</returns>
    public bool enviarCorreoAsignacionERP(int idUsuario, int idReporte, int idRespuestaReporte)
    {
        ControllerUsuarios cu = new ControllerUsuarios();
        utileria = new Utileria();
  //      string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
     

        string asunto,
               correo;

    

        var infReporte = (from rp in erp.tReporte
                          where rp.idReporte == idReporte
                          select rp).Single();

        var nombreGrupo = (from ng in erp.view_DetalleReporte
                           where ng.idReporte == idReporte
                           select new { ng.nomGrupo, ng.nomSistema }).Single();

        var reporte = (from trep in erp.vCorreoUsuarioIncidencias
                       where trep.idReporte == idReporte
                       select trep).SingleOrDefault();

        var comentario = (from com in erp.tRespuestaReportes
                          where com.idReporte == idReporte && com.idEstadoReporte == 2 && com.idRespuestaReporte == idRespuestaReporte
                          select com.comentario).Single();

        var querySistemas = (from sistema in erp.tERPGrupoSistema
                             where sistema.idReporte == idReporte
                             select sistema).ToList();

        string sistemas = "";

        foreach (tERPGrupoSistema gsistema in querySistemas)
        {
            sistemas += "<li style='padding-left:2%'>" + gsistema.cSistemas.nomSistema + "</li>";
        }

        asunto = "Asignacíón de Petición " + infReporte.folio + "";
        string cuerpo = "<h3> Se asigno la petici&oacute;n a " + reporte.nombreUsuarioResponsable + "</h3>" +
                        "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                "<td style='border:none;' colspan='6' align='right' ><font size='4.5em'> <b> Grupo: " + nombreGrupo.nomGrupo + " </b></font> </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                "<td colspan='2' > " + infReporte.folio + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Prioridad</b></td>" +
                                "<td colspan='2' > " + infReporte.cPrioridadReporte.nombrePrioridad + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Tipo Reporte</b></td>" +
                                "<td colspan='2' > " + infReporte.cTipoReporte.nombreTipoReporte + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Sistema </b> </td>" +
                                "<td colspan='2'> " + sistemas + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Fecha Propuesta </b> </td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuesta) + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b> Fecha Termino </b></td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuestaTermino) + " </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                "<td colspan='10'> " + infReporte.asunto + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                    "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b>Comentario Respuesta </b> </td>" +
                                "<td colspan='10'> " + comentario + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='border:none;' colspan='12' > <center><h4>" +
                                    "<label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver m&aacute;s detalles de la petici&oacute;n</label></h4>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br/>";

        correo = reporte.correoUsarioCreador + ";";

        return utileria.sendMail(correo, asunto, cuerpo, 1);
    }

    /// <summary>
    /// Metodo para enviar correo electronico, cuando se responde una peticion de ERP.
    /// </summary>
    /// <param name="idUsuario"></param>
    /// <param name="idReporte"></param>
    /// <returns>bool  (true enviado, false no enviado)</returns>
    public bool enviarCorreoRespuestaERP(int idUsuario, int idReporte, int idRespuestaReporte)
    {
        ControllerUsuarios cu = new ControllerUsuarios();
        utileria = new Utileria();
        string asunto,
               correo;

       // string nombreArchivo = "";
        var reporte = (from trep in erp.tReporte
                       where trep.idReporte == idReporte
                       select trep).SingleOrDefault();

        var comentario = (from tRespuesta in erp.tRespuestaReportes
                          where tRespuesta.idRespuestaReporte == idRespuestaReporte
                          select tRespuesta.comentario).Single();

        var querySistemas = (from sistema in erp.tERPGrupoSistema
                             where sistema.idReporte == idReporte
                             select sistema).ToList();

        string sistemas = "";

        foreach (tERPGrupoSistema gsistema in querySistemas)
        {
            sistemas += "<li style='padding-left:1%'>" + gsistema.cSistemas.nomSistema + "</li>";
        }

        var idERP = (from id in erp.tERPGrupoSistema
                     where id.idReporte == idReporte
                     select id.idERPGrupo).First();

        var nombreGrupo = (from nGrupo in erp.tERPGrupo
                           where nGrupo.idERPGrupo == idERP
                           select nGrupo.nomGrupo).Single();

        var correoUs = (from cor in erp.vCorreoUsuarioIncidencias
                        where cor.idReporte == idReporte
                        select new { cor.correoUsarioCreador, cor.nombreUsuarioResponsable }).Single();

        asunto = "Respuesta de Petición de ERP " + reporte.folio + "";
        string cuerpo = correoUs.nombreUsuarioResponsable + " ha respondido a la petición " + reporte.folio + "" +
                        "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                "<td style='border:none;' colspan='6' align='right' ><font size='4.5em'> <b> Grupo: " + nombreGrupo + " </b></font> </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='1' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                "<td colspan='2' > " + reporte.folio + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Tipo Reporte</b></td>" +
                                "<td colspan='2' > " + reporte.cTipoReporte.nombreTipoReporte + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Prioridad</b></td>" +
                                "<td colspan='3' > " + reporte.cPrioridadReporte.nombrePrioridad + " </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='1' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Sistema(s) </b> </td>" +
                                "<td colspan='11'> " + sistemas + " </td>" +
                             "</tr>" +
                            "<tr >" +
                                "<td colspan='1' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                "<td colspan='11'> " + reporte.asunto + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                    "<td colspan='1' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b>Comentario Respuesta </b> </td>" +
                                "<td colspan='11'> " + comentario + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                            "</tr>" +
                            "<tr align=middle>" +
                                "<td style='border:none;' colspan='12' > <center><h4>" +
                                    "<label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver m&aacute;s detalles de la petición</label></h4>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br/>";

        correo = correoUs.correoUsarioCreador + ";";

        return utileria.sendMail(correo, asunto, cuerpo, 1);
    }

    /// <summary>
    /// Metodo para cancelar la incidencia
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="comentario"></param>
    /// <param name="nombreArchivo"></param>
    /// <param name="idUsuario"></param>
    /// <returns>bool(true canceldado correctamente, false de lo contrario)</returns>
    public bool cancelarPeticion(int idReporte, string comentario, string nombreArchivo, int idUsuario)
    {
        try
        {
            string query = "";
            storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
            tLeccionesAprendidas tLecciones = new tLeccionesAprendidas();
            string fechaRespuesta = DateTime.Now.ToShortDateString();
            DateTime fechaR = Convert.ToDateTime(fechaRespuesta);
            respuesta.idReporte = idReporte;
            respuesta.comentario = comentario;
            respuesta.idEstadoReporte = 7;
            respuesta.evidencia = nombreArchivo;
            respuesta.fechaRespuesta = fechaR;
            respuesta.idUsuario = idUsuario;
            erp.tRespuestaReportes.InsertOnSubmit(respuesta);
            erp.SubmitChanges();
            int idRespuestaReporte = respuesta.idRespuestaReporte;
            var queryUpdateReporte = (from r in erp.tReporte
                                      where r.idReporte == idReporte
                                      select r).First();
            queryUpdateReporte.idEstatusReporte = 7;
            erp.SubmitChanges();

            var LogTicketSprint = (from t in erp.tTicketSprint
                                   where t.idReporte == idReporte
                                   select t).FirstOrDefault();

            if (LogTicketSprint != null)
            {
                query = "UPDATE tTicketSprint SET iEstatus = 7 where idReporte =" + idReporte;
                sp.ejecutaSQL(query);
            }

            //Insert en tLeccionesAprendidas
            tLecciones.idReporte = idReporte;
            tLecciones.idUsuario = idUsuario;
            tLecciones.idRespuestaReporte = respuesta.idRespuestaReporte;
            erp.tLeccionesAprendidas.InsertOnSubmit(tLecciones);
            erp.SubmitChanges();

            var idTipoReporte = (from tr in erp.tReporte
                                 where tr.idReporte == idReporte
                                 select tr.idTipoReporte).FirstOrDefault();
            if (idTipoReporte == 3)
            {
                var sistemas = (from terpg in erp.tERPGrupoSistema
                                where terpg.idReporte == idReporte
                                select terpg).ToList();

                foreach (tERPGrupoSistema sistema in sistemas)
                {
                    //erp.tERPGrupoSistema.DeleteOnSubmit(sistema);
                    sistema.idEstatusERPGrupoSistema = 2;
                }
                erp.SubmitChanges();
            }

            byte? estatusCorreo = 1;


            if (enviarCorreoCancelacion(idReporte, idUsuario, idRespuestaReporte, 1))
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
    /// Metodo para enviar correo, cuando se cancela una incidencia
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <param name="idRespuestaReporte"></param>
    /// <returns>bool  (true enviado, false no enviado)</returns>
    public bool enviarCorreoCancelacion(int idReporte, int idUsuario, int idRespuestaReporte, int tipoCorreo)
    {
        ControllerUsuarios cu = new ControllerUsuarios();
        string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
        string sArchivoAdjunto = "";

        utileria = new Utileria();
        string asunto,
               correo;

        string nSistemanGrupo = "";
        string tipoSoporte = " la incidencia ";
        string tipoSoporteAsunto = "Incidencia";
        var reporte = (from trep in erp.tReporte
                       where trep.idReporte == idReporte
                       select trep).Single();

        var queryidRespuesta = (from tRespuesta in erp.tRespuestaReportes
                                where tRespuesta.idRespuestaReporte == idRespuestaReporte
                                select tRespuesta).Single();

        var grupo = (from sistema in erp.view_DetalleReporte
                     where sistema.idReporte == idReporte
                     select new { sistema.nomGrupo }).Single();

        if (reporte.idTipoReporte == 3)
        {
            var idSistema = (from sistema in erp.view_DetalleReporte
                             where sistema.idReporte == idReporte
                             select new { sistema.nomGrupo }).Single();

            var querySistemas = (from sistema in erp.tERPGrupoSistema
                                 where sistema.idReporte == idReporte
                                 select sistema).ToList();

            string sistemas = "";

            foreach (tERPGrupoSistema gsistema in querySistemas)
            {
                sistemas += "<li style='padding-left:2%'>" + gsistema.cSistemas.nomSistema + "</li>";
            }
            nSistemanGrupo = sistemas;
        }
        else
        {
            var idSistema = (from sistema in erp.view_DetalleReporte
                             where sistema.idReporte == idReporte
                             select new { sistema.nomSistema }).Single();

            if (reporte.idArea != 0)
            {
                ERPManagementRHDataContext erpRH = new ERPManagementRHDataContext();

                var area = (from nombreArea in erpRH.view_ValidarTipoUsuarioAreaGrupo
                            where nombreArea.idUsuario == reporte.idUsuario
                            select nombreArea.area).FirstOrDefault();

                nSistemanGrupo += "<label>" + area + "</label>";
                tipoSoporte = "el soporte ";
                tipoSoporteAsunto = "Soporte";
            }
            else
            {
                nSistemanGrupo += "<label>" + idSistema.nomSistema + "</label>";
            }
        }

        var correoUs = (from cor in erp.vUsuariosERPM
                        where cor.idEmpleado == reporte.idUsuario.ToString()
                        select new { cor.correoElectronico }).Single();

        if (tipoCorreo == 1)
        {
            asunto = "Cancelación de "+tipoSoporteAsunto+": " + reporte.folio;
        }
        else
        {
            asunto = "Enviar Incidencia a Modificar: " + reporte.folio;
        }
        string nombreArchivo = queryidRespuesta.evidencia;

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
        string cuerpo = "<h3 style='color:black'>Se ha cancelado " + tipoSoporte + "" + reporte.folio + "</h3>" +
                        "<center> <table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                "<td style='border:none;' colspan='6' align='right' ><font size='4.5em'> <b> Grupo: " + grupo.nomGrupo + " </b></font> </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td  style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                "<td colspan='2' > " + reporte.folio + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Tipo Reporte</b></td>" +
                                "<td colspan='2' > " + reporte.cTipoReporte.nombreTipoReporte + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Prioridad</b></td>" +
                                "<td colspan='3' > " + reporte.cPrioridadReporte.nombrePrioridad + " </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='1' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Sistema(s) </b> </td>" +
                                "<td colspan='11'> " + nSistemanGrupo + " </td>" +
                             "</tr>" +
                            "<tr >" +
                                "<td colspan='1' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                "<td colspan='11'> " + reporte.asunto + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                    "<td colspan='1' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b>Comentario Respuesta </b> </td>" +
                                "<td colspan='11'> " + queryidRespuesta.comentario + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                            "</tr>" +
                             sArchivoAdjunto +
                            "<tr align=middle>" +
                                "<td style='border:none;' colspan='10' > <center><h4>" +
                                    "<label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver m&aacute;s detalles de la incidencia</label></h4>" +
                                "</td>" +
                            "</tr>" +
                        "</table> </center>" +
                        "<br/>";

        correo = correoUs.correoElectronico + ";";

        return utileria.sendMail(correo, asunto, cuerpo, 1);
    }

    public bool enviarIncidenciaModificar(int idReporte, string comentario, string nombreArchivo, int idUsuario)
    {
        tLeccionesAprendidas tLecciones = new tLeccionesAprendidas();
        //Update estatus en tReporte
        var reporte = (from tr in erp.tReporte
                       where tr.idReporte == idReporte
                       select tr).FirstOrDefault();
        reporte.idEstatusReporte = 1;
        erp.SubmitChanges();

        //Borrar responsables de reporte
        var responsables = (from trr in erp.tResponsableReporte
                            where trr.idReporte == idReporte
                            select trr).ToList();
        erp.tResponsableReporte.DeleteAllOnSubmit(responsables);

        //Insert en tRespuestaReporte
        string fechaRespuesta = DateTime.Now.ToShortDateString();
        DateTime fechaR = Convert.ToDateTime(fechaRespuesta);
        respuesta.idReporte = idReporte;
        respuesta.comentario = comentario;
        respuesta.idEstadoReporte = 1;
        respuesta.evidencia = nombreArchivo;
        respuesta.fechaRespuesta = fechaR;
        respuesta.idUsuario = idUsuario;
        erp.tRespuestaReportes.InsertOnSubmit(respuesta);
        erp.SubmitChanges();


        //Insert en tLeccionesAprendidas
        tLecciones.idReporte = idReporte;
        tLecciones.idRespuestaReporte = respuesta.idRespuestaReporte;
        tLecciones.idUsuario = idUsuario;
        erp.tLeccionesAprendidas.InsertOnSubmit(tLecciones);
        erp.SubmitChanges();

        enviarCorreoCancelacion(idReporte, idUsuario, respuesta.idRespuestaReporte, 2);

        return true;
    }

    public bool enviarAvance(int idReporte, string comentario, string nombreArchivo, int idUsuario)
    {
        try
        {
            var tr = (from reporte in erp.tReporte
                      where reporte.idReporte == idReporte
                      select reporte).FirstOrDefault();

            tr.idEstatusReporte = 8;

            tRespuestaReporte trr = new tRespuestaReporte();
            trr.idReporte = idReporte;
            trr.idEstadoReporte = 8;
            trr.evidencia = nombreArchivo;
            trr.comentario = comentario;

            string fechaRespuesta = DateTime.Now.ToShortDateString();
            DateTime fechaR = Convert.ToDateTime(fechaRespuesta);
            trr.fechaRespuesta = fechaR;
            trr.idUsuario = idUsuario;
            erp.tRespuestaReportes.InsertOnSubmit(trr);
            erp.SubmitChanges();
            int idRespuestaReporte = trr.idRespuestaReporte;
            byte? estatusCorreo = 1;
            if (enviaCorreoAvance(idReporte, idRespuestaReporte, idUsuario))
            {
                estatusCorreo = 2;
            }
            trr.idEstatusCorreo = estatusCorreo;
            erp.SubmitChanges();

            return true;
        }
        catch (Exception e)
        {
            return false;

        }
    }
    public bool enviaCorreoAvance(int idReporte, int idRespuestaReporte, int idUsuario)
    {
        string sRutaFinal = ConfigurationManager.AppSettings["keyUrl"];
        string sArchivoAdjunto = "";

        ControllerUsuarios cu = new ControllerUsuarios();
        utileria = new Utileria();
        //bool resultado = false;
        string asunto,
               correo;
        string sistema = "";
        string tipoSoporte = "la incidencia: ";
        string comentarioRespuesta = "";
        var reporte = (from trep in erp.vCorreoUsuarioIncidencias
                       where trep.idReporte == idReporte
                       select trep).SingleOrDefault();

        var queryidRespuesta = (from tRespuesta in erp.tRespuestaReportes
                                where tRespuesta.idRespuestaReporte == idRespuestaReporte
                                select tRespuesta).Single();

        var nombreGrupo = (from ng in erp.view_DetalleReporte
                           where ng.idReporte == idReporte
                           select new { ng.nomGrupo, ng.nomSistema }).Single();

        var infReporte = (from rp in erp.tReporte
                          where rp.idReporte == idReporte
                          select rp).Single();

        if (infReporte.idArea != 0)
        {
            ERPManagementRHDataContext erpRH = new ERPManagementRHDataContext();

            var area = (from nombreArea in erpRH.view_ValidarTipoUsuarioAreaGrupo
                        where nombreArea.idUsuario == infReporte.idUsuario
                        select nombreArea.area).FirstOrDefault();

            sistema = area;
            tipoSoporte = "el soporte: ";
        }
        else
        {
            sistema = nombreGrupo.nomSistema;
        }

        if (queryidRespuesta.comentario != "")
        {
            comentarioRespuesta = "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b>Comentario Respuesta </b> </td>" +
                                "<td colspan='10'> " + queryidRespuesta.comentario + " </td>";
        }

        asunto = "Avance Consulta " + infReporte.folio + "";
        string nombreArchivo = queryidRespuesta.evidencia;
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

        string cuerpo = "<h3>" + reporte.nombreUsuarioResponsable + " ha enviado un avance de " + tipoSoporte + "" + infReporte.folio + "</h3><br />" +
                        "<table width='98%' border=1 cellspacing=0 cellpadding=2 bordercolor='#145a7a'>" +
                            "<tr>" +
                                    "<td style='border:none;' colspan='6'><div style='float:left;' WIDTH=80%><IMG SRC='https://www.nadconsultoria.com/ERPManagement/images/logos/logoNAD.png' WIDTH='140' HEIGHT='70' ></div> </td>" +
                                "<td style='border:none;' colspan='6' align='right' ><font size='4.5em'> <b> Grupo: " + nombreGrupo.nomGrupo + " </b></font> </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;' ><b>Folio</b></td>" +
                                "<td colspan='2' > " + infReporte.folio + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Prioridad</b></td>" +
                                "<td colspan='2' > " + infReporte.cPrioridadReporte.nombrePrioridad + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Tipo Reporte</b></td>" +
                                "<td colspan='2' > " + infReporte.cTipoReporte.nombreTipoReporte + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Sistema </b> </td>" +
                                "<td colspan='2'> " + sistema + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'> <b> Fecha Propuesta </b> </td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuesta) + " </td>" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b> Fecha Termino </b></td>" +
                                "<td colspan='2'> " + String.Format("{0:yyyy/MM/dd}", infReporte.fechaPropuestaTermino) + " </td>" +
                            "</tr>" +
                            "<tr >" +
                                "<td colspan='2' style='background: #145a7a; color:white; border: #FFFFFF 1px solid;'><b>Asunto</b></td>" +
                                "<td colspan='10'> " + infReporte.asunto + " </td>" +
                            "</tr>" +
                            "<tr>" +
                                    "" + comentarioRespuesta + "" +
                            "</tr>" +
                            "<tr>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                                "<td style='border:none;'></td>" +
                            "</tr>" +
                              sArchivoAdjunto +
                            "<tr>" +
                                "<td style='border:none;' colspan='12' > <center><h4>" +
                                    "<label>Por favor ingresa a <a href='https://www.nadconsultoria.com/ERPManagement/' target='_new'>ERPManagement</a></label><label>, para ver m&aacute;s detalles de la consulta.</label></h4>" +
                                "</td>" +
                            "</tr>" +
                        "</table>" +
                        "<br/>";

        correo = reporte.correoUsarioCreador + ";";

        return utileria.sendMail(correo, asunto, cuerpo, 1);
    }

    public bool PausarReanudar(int idReporte, int idUsuario, string comentario)
    {
        try
        {
            string query = "";
            storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
            var queryUpdateReporte = (from r in erp.tReporte
                                      where r.idReporte == idReporte
                                      select r).First();

            int idEstadoReporte = queryUpdateReporte.idEstatusReporte;

            if (idEstadoReporte == 9)
            {
                queryUpdateReporte.idEstatusReporte = 2;
            }
            if (idEstadoReporte == 2 || idEstadoReporte == 4)
            {
                queryUpdateReporte.idEstatusReporte = 9;
            }

            tRespuestaReporte tRespuestaR = new tRespuestaReporte();

            tRespuestaR.idReporte = idReporte;
            tRespuestaR.idEstadoReporte = queryUpdateReporte.idEstatusReporte;
            tRespuestaR.evidencia = "";
            tRespuestaR.comentario = comentario;
            tRespuestaR.fechaRespuesta = DateTime.Now;
            tRespuestaR.idEstatusCorreo = 2;
            tRespuestaR.idUsuario = idUsuario;

            var LogTicketSprint = (from t in erp.tTicketSprint
                                   where t.idReporte == idReporte
                                   select t).FirstOrDefault();

            if (LogTicketSprint != null)
            {
                query = "UPDATE tTicketSprint SET iEstatus = "+ queryUpdateReporte.idEstatusReporte + " where idReporte =" + idReporte;
                sp.ejecutaSQL(query);
            }

            erp.tRespuestaReportes.InsertOnSubmit(tRespuestaR);

            erp.SubmitChanges();

            //byte? estatusCorreo = 1;

            //if (enviarCorreo(idUsuario, idReporte, tRespuestaR.idRespuestaReporte))
            //{
            //    estatusCorreo = 2;
            //}

            //tRespuestaR.idEstatusCorreo = estatusCorreo;

            //erp.SubmitChanges();

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}