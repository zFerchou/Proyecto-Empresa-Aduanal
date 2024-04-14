using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

public partial class Inicio : System.Web.UI.Page
{
    private static ControllerReportes controller;
    private static int idUsuario;
    private static ControllerRespuestaReporte controllerRespuesta;
    private static Utileria util;

    protected void Page_Load(object sender, EventArgs e)
    {
        iniciar();
    }

    /// <summary>
    /// Método para verificar si existen
    /// pendientes dependiendo el usuario;
    /// </summary>

    public void iniciar()
    {
        Inicio.limpiarVariables();
        Master.generaHeader("");
        controller = new ControllerReportes();
        util = new Utileria();
        //idUsuario = int.Parse(Session["id"].ToString());
        Utileria utileria = new Utileria();
        txtidUser.Text = utileria.EncodeTo64(idUsuario.ToString());//utileria.EncodeTo64(Session["id"].ToString());

    }

    /// <summary>
    /// Metodo para obtener los apartados de los diferenets estados de las incidencias
    /// para mostrarlos si existen.
    /// </summary>
    /// <returns>List<string></returns>
    [WebMethod]
    public static List<string> start()
    {
        Inicio.limpiarVariables();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string query = "";
        List<string> resultado = new List<string>(); ;
        string res = "";
        int rSinAsignar = 0;
        int rAsignados = 0;
        int rPorValidar = 0;
        int rCCreado = 0;
        int rCAsignado = 0;
        int rCValidar = 0;
        int rSoporte = 0;
        int rSprint = 0;
        int rHistorico = 0;
        bool bandera = false;
        controller = new ControllerReportes();
        ERPManagementDataContext erp = new ERPManagementDataContext();

        List<vUsuariosSoporte> lstUsuarioSoporte = (from ru in erp.vUsuariosSoporte
                                                    where ru.idUsuario == idUsuario
                                                    select ru).ToList();

        query = "select idTipoReporte from tUsuarioTipoIncidencia where idUsuario =" + idUsuario;
        List<string> lstUsuarioTipo = sp.recuperaRegistros(query);

        if (lstUsuarioSoporte.Count > 0)
        {
            rSoporte = controller.getCountReportesSoporte(idUsuario, lstUsuarioSoporte,lstUsuarioTipo);
            if (rSoporte > 0)
            {
                res += "<a href='javascript:;' onclick='javascript:getReportesSoportes();' class='divBlanco pdtes'><span id='icon-90' class='configuracion verde'> SOPORTES ADUANALES</span> <span class='divMiniBlanco txt-verde'>" + rSoporte + "</span></a>";
                res += "<div class='clear'></div><br />";
                bandera = true;
            }

        }

        //Se verifica si el usuario en sesion cuenta con el rol 3 para dar soporte a incidencias.
        var queryRolSoporte = (from rol in erp.tRolUsuarioERPM
                               where rol.idUsuario == idUsuario && rol.idRolERPM == 3
                               select rol).Count();

        var lstRolusuario = (from roluser in erp.tUsuarioSoporte
                             where roluser.idUsuario == idUsuario
                             select roluser).Count();

        //si el resultado es mayor a 0, indica que el usuario cuenta con permisos para dar soporte a incidencias      
        if (queryRolSoporte > 0 || lstRolusuario > 0 )
        {
            //se llaman los mettodos para contar el numero de incidencias se encuentran en los estatus 1,2,3,4
            rSinAsignar = controller.getCountReporteSinAsignar(idUsuario, queryRolSoporte,lstUsuarioTipo);
            rAsignados = controller.getCountReportesAsignados(idUsuario, queryRolSoporte, lstUsuarioSoporte);
            rPorValidar = controller.getCountReportesPorValidar(idUsuario, queryRolSoporte, lstUsuarioSoporte);
            rSprint = controller.getCountReporteSinAsignarSprint(idUsuario, queryRolSoporte, lstUsuarioTipo);
            rHistorico = controller.getCountReporteSinAsignarHistorico(idUsuario, queryRolSoporte, lstUsuarioTipo);
            //si la llamada al metodo devuelve mayor a 0 se pinta el div.
            if (rSinAsignar > 0 )
            {
                res += "<a href='javascript:;' onclick='javascript:getReportesSinAsignar();' class='divBlanco pdtes'><label id='icon-25' class='configuracion verde tipo-ticket'>TICKETS SIN ASIGNAR <label class='txt-azul qty-tickets'>" + rSinAsignar + "</label></label></a>";
                bandera = true;
            }

            if (rAsignados > 0 )
            {
                res += "<a href='javascript:;' onclick='javascript:getReportesAsignados();' class='divBlanco pdtes'><label id='icon-25' class='asig_equipo verde tipo-ticket'>TICKETS ASIGNADOS <label class='txt-azul qty-tickets'>" + rAsignados + "</label></label></a>";
                bandera = true;
            }

            if (rPorValidar > 0 ) 
            {
                res += "<a href='javascript:;' onclick='javascript:getReportesPorValidar();' class='divBlanco pdtes'><label id='icon-25' class='validar verde tipo-ticket'>TICKETS POR VALIDAR <label class='txt-azul qty-tickets'>" + rPorValidar + "</label></label></a>";
                bandera = true;
            }

            if (rSprint > 0 )
            {
                res += "<a href='javascript:;' onclick='javascript:getReportesSinAsignarSprint();' class='divBlanco pdtes'><label id='icon-25' class='validar verde tipo-ticket'>SPRINT BACKLOG <label class='txt-azul qty-tickets'>" + rSprint + "</label></label></a>";
                bandera = true;
            }

            if (rHistorico > 0 )
            {
                res += "<a href='javascript:;' onclick='javascript:getReportesSinAsignarHistorico();' class='divBlanco pdtes'><label id='icon-25' class='validar verde tipo-ticket'>TICKETS HISTORICOS <label class='txt-azul qty-tickets'>" + rHistorico + "</label></label></a>";
                bandera = true;
            }
        }
        res += "<div class='clear'></div><br />";


        //consulta para verificar las incidencias en las que se encuentra involucrado el usuario en sesion
        var queryRolGenerar = (from rol in erp.tRolUsuarioERPM
                               where rol.idRolERPM == 1 && rol.idUsuario == idUsuario
                               select rol).Count();
        //si el resultado de la consulta es mayor a 0 se verifica que pendientes tiene el usuario en sesion en los diferenetes estatus de incidencias
        if (queryRolGenerar > 0 )
        {
            //invoca los metodos para obtener la cantidad de incidencias
            //cantidad de incidencias creadas
            rCCreado = controller.getCountReporteCreado(idUsuario);
            //cantidad de incidencias en proceso
            rCAsignado = controller.getCountReportesEnProceso(idUsuario);
            //cantidad de incidencias por validar
            rCValidar = controller.getCountReportesCPorValidar(idUsuario);

            //si la cantidad es mayor a 0 se pinta el div
            if (rCCreado > 0)
            {
                res += "<a href='javascript:;' onclick='javascript:getReportesCreados();' class='divBlanco pdtes'><label id='icon-25' class='boletin verde tipo-ticket'>MIS TICKETS CREADOS <label class='txt-azul qty-tickets'>" + rCCreado + "</label></label></a>";
                bandera = true;
            }

            if (rCAsignado > 0)
            {
                res += "<a href='javascript:;' onclick='javascript:getReportesEnProceso();' class='divBlanco pdtes'><label id='icon-25' class='proceso verde tipo-ticket'>MIS TICKETS EN PROCESO <label class='txt-azul qty-tickets'>" + rCAsignado + "</label></label></a>";
                bandera = true;
            }

            if (rCValidar > 0)
            {
                res += "<a href='javascript:;' onclick='javascript:getReportesCValidar();' class='divBlanco pdtes'><label id='icon-25' class='solicitud_aprobada verde tipo-ticket'>MIS TICKETS POR VALIDAR <label class='txt-azul qty-tickets'>" + rCValidar + "</label></label></a>";
                bandera = true;
            }

        }

        //si la bandera termina en true se dibuja que el usuario en sesion cuenta con pendientes por realizar.
        if (bandera)
        {
            resultado.Add("<div class='bg-success width100 clear mb-4'><span id='icon-47' class='blanco pendientes'>MIS PENDIENTES</span><br />");
        }
        //si la bandera termina en false, el usuario no cuenta con pendientes.
        else
        {
            resultado.Add("<div class='bg-success width100 clear'><span id='icon-47' class='blanco pendientes'>SIN PENDIENTES</span><br />");


        }

        resultado.Add(res);

        return resultado;
    }

    /// <summary>
    /// TABLA SIN ASIGNAR
    /// Método para traer la tabla de
    /// reportes sin asignar construida.
    /// </summary>
    /// <returns>Tabla de reportes si asignar</returns>
    [WebMethod]
    public static string getReportesSinAsignar()
    {
        Inicio.limpiarVariables();
        controller = new ControllerReportes();
        return controller.getTblReportesSinAsignar(idUsuario);
    }
    [WebMethod]
    public static string getReportesSinAsignarSprint()
    {
        Inicio.limpiarVariables();
        controller = new ControllerReportes();
        return controller.getTblReportesSinAsignarSprint(idUsuario);
    }
    [WebMethod]
    public static string getReportesSinAsignarHistorico()
    {
        Inicio.limpiarVariables();
        controller = new ControllerReportes();
        return controller.getTblReportesSinAsignarHistorico(idUsuario);
    }
    /// <summary>
    /// TABLA ASIGNADOS
    /// Método para traer la tabla de
    /// reportes asignados construida.
    /// </summary>
    /// <returns>Tabla de reportes si asignar</returns>
    [WebMethod]
    public static string getReportesAsignados(string idUsuario)
    {
        controller = new ControllerReportes();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getTblReportesAsignados(idUser);
    }

    /// <summary>
    /// TABLA POR VALIDAR
    /// Método para traer la tabla de
    /// reportes por validar construida.
    /// </summary>
    /// <returns>Tabla de reportes por validar</returns>
    [WebMethod]
    public static string getReportesPorValidar(string idUsuario)
    {
        controller = new ControllerReportes();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getTblReportesPorValidar(idUser);
    }
    /// <summary>
    /// Metodo para obtener la tabla de las incidencias creadas, que aun no se han asignada
    /// </summary>
    /// <returns>string (tabla)</returns>
    [WebMethod]
    public static string getReportesCreados(string idUsuario)
    {
        controller = new ControllerReportes();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getReportesCreados(idUser);
    }
    /// <summary>
    /// Metodo para obtener la tabla de las incidencias en proceso
    /// </summary>
    /// <returns>string (tabla)</returns>
    [WebMethod]
    public static string getReportesEnProceso(string idUsuario)
    {
        controller = new ControllerReportes();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getReportesEnProceso(idUser);
    }
    /// <summary>
    /// Metodo para obtener tabla de incidencias por validar
    /// </summary>
    /// <returns>string (tabla)</returns>
    [WebMethod]
    public static string getReportesCValidar(string idUsuario)
    {
        controller = new ControllerReportes();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getReportesCValidar(idUser);
    }

    /// <summary>
    /// Obetener detalle de un reporte en especifico
    /// se obtienen los datos de un reporte.
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>Lista de datos del reporte</returns>
    [WebMethod]
    public static List<string> getDetalleReporte(int idReporte)
    {
        List<string> lstDetalleReporte = new List<string>();
        controller = new ControllerReportes();
        tReporte tr = controller.getReporteById(idReporte);

        lstDetalleReporte.Add(tr.asunto);
        lstDetalleReporte.Add(tr.descripcion);
        lstDetalleReporte.Add(tr.cPrioridadReporte.nombrePrioridad);

        return lstDetalleReporte;

    }
    /// <summary>
    /// Metodo para obtener el detalle de un reporte en especifico
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>Objeto view_RespuestaReporte</returns>
    [WebMethod]
    public static view_RespuestaReporte consultaReporteValidar(int idReporte)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        view_RespuestaReporte vista = new view_RespuestaReporte();

        vista = controllerRespuesta.consultaReporteValidar(idReporte);

        return vista;
    }
    /// <summary>
    /// Metodo para insertar un reporte validado
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="comentario"></param>
    /// <param name="nombreArchivo"></param>
    /// <returns>bool)</returns>
    [WebMethod]
    public static bool insertaComentario(int idReporte, string comentario, string nombreArchivo, int cal, string idUsuario)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controllerRespuesta.insertaComentario(idReporte, comentario, nombreArchivo, idUser, cal);
    }

    /// <summary>
    /// Metodo para insertar comentario de un reporte rechazado
    /// El reporte se mandara a revision, para que sea resuelto correctamente
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="comentario"></param>
    /// <param name="nombreArchivo"></param>
    /// <param name="fechaPropuesta"></param>
    /// <returns>bool</returns>
    [WebMethod]
    public static bool insertaComentarioRechazo(int idReporte, string comentario, string nombreArchivo, string idUsuario)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controllerRespuesta.insertaComentarioRechazo(idReporte, comentario, nombreArchivo, idUser);
    }

    /// <summary>
    /// Metodo para insertar la respuesta de la incidencia del usuario de soporte
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="comentario"></param>
    /// <param name="nombreArchivo"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool insertaComentarioRespuesta(int idReporte, string comentario, string nombreArchivo, string urlERP, string idUsuario)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controllerRespuesta.insertaComentarioRespuesta(idReporte, comentario, nombreArchivo, idUser, urlERP);
    }

    /// <summary>
    /// Metodo para obtener el responsable de una incidencia
    /// </summary>
    /// <returns> Objeto de vUsuariosERPM</returns>
    [WebMethod]
    public static vUsuariosERPM obtenerResponsable()
    {
        Inicio.limpiarVariables();
        controllerRespuesta = new ControllerRespuestaReporte();
        return controllerRespuesta.obtenerResponsable(idUsuario);
    }

    /// <summary>
    /// Metodo para asignar la incidencia a un responsable
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="fechaPropuestaTermino"></param>
    /// <returns>bool</returns>
    [WebMethod]
    public static bool asignarReporte(int idReporte, DateTime fechaPropuestaTermino, string comentario, string personalApoyo, string idUsuario)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controllerRespuesta.asignarReporte(idReporte, fechaPropuestaTermino, idUser, comentario, personalApoyo);
    }

    [WebMethod]
    public static string detalleAccionesReporte(int idReporte, string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getDetalleAccionesReporte(idReporte, idUser);
    }
    /// <summary>
    /// Metodo para consultar la incidencia
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>objeto view_Reporte</returns>
    [WebMethod]
    public static view_Reporte consultaReporteUsuario(int idReporte)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        var resultado = controllerRespuesta.consultaReporteUsuario(idReporte);
        return resultado;
    }
    /// <summary>
    /// Metodo para consultar la incidencia por folio
    /// </summary>
    /// <param name="sFolioV"></param>
    /// <returns>objeto view_Reporte</returns>
    [WebMethod]
    public static view_Reporte consultaReporteUsuarioVinculado( string sFolioV)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        var resultado = controllerRespuesta.consultaReporteUsuario(0, sFolioV);
        return resultado;
    }
    /// <summary>
    /// metodo para obtener Responsable y personal de apoyo
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="tipo"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<string> obtenerReponsableReporte(int idReporte, int tipo)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        return controllerRespuesta.obtenerResponsableReporte(idReporte, tipo);
    }
    /// <summary>
    /// Metodo para obtener el usuario en sesion
    /// </summary>
    /// <returns>string (nombre usuario)</returns>
    [WebMethod]
    public static string obtnerUsuarioSession(string idUsuario)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controllerRespuesta.obtnerUsuarioSession(idUser);
    }
    /// <summary>
    /// metodo para obtener los sistemas solicitados en una peticion de ERP
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns></returns>
    [WebMethod]
    public static string obtenerSistema(int idReporte)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        return controllerRespuesta.obtenerSistema(idReporte);
    }

    /// <summary>
    /// Metodo para cancelar petición
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="comentario"></param>
    /// <param name="nombreArchivo"></param>
    /// <returns>bool</returns>
    [WebMethod]
    public static bool cancelarPeticion(int idReporte, string comentario, string nombreArchivo, string idUsuario)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controllerRespuesta.cancelarPeticion(idReporte, comentario, nombreArchivo, idUser);
    }

    [WebMethod]
    public static List<string> obtenerPersonalApoyo(int idReporte, string idUsuario)
    {
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        string query = "";
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        ERPManagementDataContext erp = new ERPManagementDataContext();

        var tipoReporte = (from r in erp.tReporte
                           where r.idReporte == idReporte
                           select new { r.idTipoReporte, r.idERPGrupo }).FirstOrDefault();
        //el tipo de reporte mayor a 5 es porque soporte Despacho Aduanero,IMMEX, OEA, IVA, otros a 
        //los que ciertos usuarios pueden dar soporte a estos.

        //Se agrega condición  tipoReporte.idTipoReporte <13 para cuando sea idTipoIncidencia= 14 tambien sea considerado 

        if (tipoReporte.idTipoReporte >= 5 && tipoReporte.idTipoReporte <13)
        {
            var idRol = (from rol in erp.tRolSoporte
                         where rol.idSoporte == tipoReporte.idTipoReporte
                         select rol.idRol).FirstOrDefault();

            query = "SELECT distinct idEmpleado, nombreCompleto" +
           " FROM vUsuariosERPM vista JOIN tUsuarioSoporte tr ON tr.idUsuario=vista.idEmpleado " +
           " left join tResponsableReporte trp on trp.idResponsable=tr.idUsuario " +
           " WHERE idEmpleado not in (select idResponsable from tResponsableReporte where idReporte=" + idReporte + ") and idEmpleado!=" + idUser + "" +
           " and idRolERPM =" + idRol + " and idGrupo=" + tipoReporte.idERPGrupo + "";
        }
        else 
        {
            

            

                query = "SELECT distinct idEmpleado, nombreCompleto" +
                   " FROM vUsuariosERPM vista JOIN tRolUsuarioERPM tr ON tr.idUsuario=vista.idEmpleado " +
                   " left join tResponsableReporte trp on trp.idResponsable=tr.idUsuario " +
                   " WHERE idEmpleado not in (select idResponsable from tResponsableReporte where idReporte=" + idReporte + ") and idEmpleado!=" + idUser + "" +
                   " and idRolERPM =3";
       
        }

        obtener = sp.recuperaRegistros(query);
        return obtener;
    }

    [WebMethod]
    public static bool enviarIncidenciaModificar(int idReporte, string comentario, string nombreArchivo, string idUsuario)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controllerRespuesta.enviarIncidenciaModificar(idReporte, comentario, nombreArchivo, idUser);
    }

    /// <summary>
    /// Metodo para obtener la tabla de las soportes sin asignar
    /// </summary>
    /// <returns>string (tabla)</returns>
    [WebMethod]
    public static string getReportesSoportes(string idUsuario)
    {
        controller = new ControllerReportes();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getTblReportesSoportesSinAsignar(idUser);
    }

    [WebMethod]
    public static bool enviarAvance(int idReporte, string comentario, string nombreArchivo, string idUsuario) {

        controllerRespuesta = new ControllerRespuestaReporte();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controllerRespuesta.enviarAvance(idReporte, comentario, nombreArchivo, idUser);
    }

    [WebMethod]
    public static bool PausarReanudar(int idReporte, string idUsuario, string comentario)
    {
        controllerRespuesta = new ControllerRespuestaReporte();
        util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controllerRespuesta.PausarReanudar(idReporte, idUser, comentario);
    }

    [WebMethod(enableSession: true)]
    public static void limpiarVariables()
    {
        idUsuario = 0;

        idUsuario = (string.IsNullOrEmpty(HttpContext.Current.Session["id"].ToString()) ? 0 : int.Parse(HttpContext.Current.Session["id"].ToString()));
    }
}