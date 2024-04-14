using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Configuracion_Reportes_Sistemas : System.Web.UI.Page
{
    private static int idUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
        idUsuario = int.Parse(Session["id"].ToString());
        string idRep = Request.QueryString["id"];
        Utileria util = new Utileria();
        int idReporte= int.Parse(util.DecodeFrom64(idRep));
        Master.generaHeader("../../");
        
        txtIdReporte.Text = util.DecodeFrom64(idRep);
        txtIdUsuario.Text = idUsuario.ToString();
        existeDbEmpresa(idReporte);
    }

    /// <summary>
    /// Obtener el Nombre del Grupo  los Sistemas que Solicito
    /// </summary>
    /// <returns>List<view_PeticionSistemas></returns>
    [WebMethod]
    public static List<view_PeticionSistemas> obtenerSistemasSolicitados(int idReporte)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.obtenerSistemasSolicitados(idReporte);
    }

    /// <summary>
    /// Saber solo si existe la BDEMP para el Grupo
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string existeDbEmpresa(int idReporte) {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.existeDbEmpresa(idReporte);
    }

    /// <summary>
    /// Generar el .bak de la base de datos de EMPDANA
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string generarBDEmpresa(string nomGrupo, int idReporte, int idUsuario, int idSistema) {    
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.generarBDEmpresa(nomGrupo,idReporte,idUsuario,idSistema);
    }


    /// <summary>
    /// Cambiar los esquemas de DBEMPDANA a los del nuevo Grupo
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string cambiarEsquemasBDEmpresa(string nomGrupo, int idReporte, int idUsuario)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.cambiarEsquemasBDEmpresa(nomGrupo, idReporte,idUsuario);
    }

    /// <summary>
    /// Limpiar tablas de la nueva base de datos restaurada para su nueva configuración
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string limpiarBDEmpresa(string nomGrupo, int idReporte, int idUsuario)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.limpiarBDEmpresa(nomGrupo, idReporte,idUsuario);
    }

    /// <summary>
    /// Saber si la base de datos de EMP existe y tambien saber en que 
    /// estatus se quedo la ejecucion para generar la BDEMP
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>List<view_ExisteBDEMP></returns>
    [WebMethod]
    public static List<view_ExisteBDEMP> existeEstatusDBEMP(int idReporte) { 
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.existeEstatusDBEMP(idReporte);
    }

    /// <summary>
    /// Saber si la base de datos de EMP existe y tambien saber en que 
    /// estatus se quedo la ejecucion para generar la BDSGC
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>List<view_ExisteBDSGC></returns>
    [WebMethod]
    public static List<view_ExisteBDSGC> existeEstatusDBSGC(int idReporte, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.existeEstatusDBSGC(idReporte,idSistema);
    }

    /// <summary>
    /// Saber si la base de datos de EMP existe y tambien saber en que 
    /// estatus se quedo la ejecucion para generar la BDSGRO
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static List<view_ExisteBDSGRO> existeEstatusDBSGRO(int idReporte, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.existeEstatusDBSGRO(idReporte, idSistema);
    }

    /// <summary>
    /// Saber si la base de datos de EMP existe y tambien saber en que 
    /// estatus se quedo la ejecucion para generar la BDSGCE
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static List<view_ExisteBDSGCE> existeEstatusDBSGCE(int idReporte, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.existeEstatusDBSGCE(idReporte, idSistema);
    }

    /// <summary>
    /// Ver el historial de ejecucución de las bases de datos solicitadas en un reporte de ERP
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string verHistorialBDEjecucion(int idReporte)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.verHistorialBDEjecucion(idReporte);
    }

    /// <summary>
    /// Generar el .bak de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <param name="idSistema"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string generarBDSGC(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.generarBDSGC(nomGrupo, idReporte, idUsuario, idSistema);
    }

    /// <summary>
    /// Cambiar los esquemas de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string cambiarEsquemasBDSGC(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.cambiarEsquemasBDSGC(nomGrupo, idReporte, idUsuario, idSistema);
    }

    /// <summary>
    /// Limpiar tablas de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string limpiarBDSGC(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.limpiarBDSGC(nomGrupo, idReporte, idUsuario, idSistema);
    }

    ////////////
    /// <summary>
    /// Generar el .bak de la base de datos SGRO
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <param name="idSistema"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string generarBDSGRO(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.generarBDSGRO(nomGrupo, idReporte, idUsuario, idSistema); 
    }

    /// <summary>
    /// Cambiar los esquemas de la base de datos SGRO
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string cambiarEsquemasBDSGRO(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.cambiarEsquemasBDSGRO(nomGrupo, idReporte, idUsuario, idSistema);
    }

    /// <summary>
    /// Limpiar tablas de la base de datos SGRO
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string limpiarBDSGRO(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.limpiarBDSGRO(nomGrupo, idReporte, idUsuario, idSistema);
    }

    ////////////
    /// <summary>
    /// Generar el .bak de la base de datos SGRO
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <param name="idSistema"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string generarBDSGCE(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.generarBDSGCE(nomGrupo, idReporte, idUsuario, idSistema);
    }

    /// <summary>
    /// Cambiar los esquemas de la base de datos SGRO
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string cambiarEsquemasBDSGCE(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.cambiarEsquemasBDSGCE(nomGrupo, idReporte, idUsuario, idSistema);
    }

    /// <summary>
    /// Limpiar tablas de la base de datos SGRO
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string limpiarBDSGCE(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.limpiarBDSGCE(nomGrupo, idReporte, idUsuario, idSistema);
    }

    /// <summary>
    /// Guardar la URL de cada sistema
    /// </summary>
    /// <param name="idGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idSistema"></param>
    /// <param name="url"></param>
    /// <param name="idUsuario"></param>
    /// <returns>bool</returns>
    [WebMethod]
    public static bool guardarURL(int idGrupo, int idReporte, int idSistema, string url, int idUsuario) {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.guardarURL(idGrupo, idReporte, idSistema, url, idUsuario);
    }

    /// <summary>
    /// Guardar URL del SGI
    /// </summary>
    /// <param name="idGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idSistema"></param>
    /// <param name="url"></param>
    /// <param name="idUsuario"></param>
    /// <returns>bool</returns>
    [WebMethod]
    public static bool guardarSGI(int idGrupo, int idReporte, string url, int idUsuario)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.guardarSGI(idGrupo, idReporte, url, idUsuario);
    }

    /// <summary>
    /// Cargar Configuración Inicial para Empresa
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    [WebMethod]
    public static string configuracionInicial(string nomGrupo, int idGrupo, int idReporte, int idUsuario,string lstSistemas)
    {
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        return generarSistemas.configuracionInicial(nomGrupo ,idGrupo, idReporte, idUsuario, lstSistemas);
    }
}