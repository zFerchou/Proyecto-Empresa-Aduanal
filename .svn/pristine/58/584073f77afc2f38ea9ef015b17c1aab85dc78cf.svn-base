using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
public partial class Configuracion_Reportes_ConsultarReportes : System.Web.UI.Page
{
    private static ControllerGestionReportes controller;
    private static ControllerGestionReportes gestionReportes;
    private static int idUsuario;
    private ERPManagementDataContext erp;
    protected void Page_Load(object sender, EventArgs e)
    {
        idUsuario = int.Parse(Session["id"].ToString());
        Master.generaHeader("../../");
        //Validar el permiso de usuario     
        erp = new ERPManagementDataContext();
        var validacion = (from vUser in erp.view_ValidarUsuario
                          where vUser.idUsuario == idUsuario && vUser.idRolERPM == 2
                          select vUser).Count();
        if (validacion <= 0)
        {
            Response.Redirect("../../inicio.aspx");
        }
    }

    //Traer todos los Reportes
    [WebMethod]
    public static string getTblReportes()
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.getTblReportes(idUsuario);
    }

    // Eliminar Reporte (GENERAL)
    [WebMethod]
    public static bool eliminarReporte(int idReporte)
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.eliminarReporte(idReporte, idUsuario);
    }

    // Eliminar Reporte Sin Asignar
    [WebMethod]
    public static bool eliminarReporteSinAsignar(int idReporte)
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.eliminarReporteSinAsignar(idReporte);
    }

    //Consultar Reporte (GENERAL)
    [WebMethod]
    public static List<string> consultarReporte(int idReporte)
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.consultarReporte(idReporte);
    }
    //Consultar Reporte Sin Asignar
    [WebMethod]
    public static List<string> consultarReporteSinAsignar(int idReporte)
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.consultarReporteSinAsignar(idReporte);
    }

    //Eliminar Reporte Asignado
    [WebMethod]
    public static bool eliminarReporteAsignado(int idReporte)
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.eliminarReporteAsignado(idReporte);
    }

    //Consultar Reporte Asignado
    [WebMethod]
    public static List<string> consultarReporteAsignado(int idReporte)
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.consultarReporteAsignado(idReporte);
    }

    //Obtener a las personas que estan como Apoyo en un reporte y mostrarlas en una tabla.
    [WebMethod]
    public static string tblPersonalApoyo(int idReporte)
    {
        ControllerResponsableReporte rp = new ControllerResponsableReporte();
        return rp.tblPersonalApoyo(idReporte);
    }

    //Obtener a la persona que estan como Responsable en un reporte.
    [WebMethod]
    public static string tblPersonalResponsable(int idReporte)
    {
        ControllerResponsableReporte rp = new ControllerResponsableReporte();
        return rp.tblPersonalResponsable(idReporte);
    }

    //***********Obtener a la persona que estan como Responsable en un tooltip.
    [WebMethod]
    public static string obtenerResponsableNuevo(int idReporte)
    {
        ControllerResponsableReporte rp = new ControllerResponsableReporte();
        return rp.obtenerResponsableNuevo(idReporte);
    }
    [WebMethod]
    public static string obtenerApoyoNuevo(int idReporte)
    {
        ControllerResponsableReporte rp = new ControllerResponsableReporte();
        return rp.obtenerApoyoNuevo(idReporte);
    }

    //Guardar a las personas que van a estar como Apoyo en un Reporte
    [WebMethod]
    public static bool guardarPersonaApoyo(int idReporte, int idResponsable)
    {
        ControllerResponsableReporte rp = new ControllerResponsableReporte();
        return rp.guardarPersonaApoyo(idReporte, idResponsable);
    }

    //Guardar a las personas que van a estar como Responsable en un Reporte
    [WebMethod]
    public static bool guardarPersonaResponsable(int idReporte, int idResponsable)
    {
        ControllerResponsableReporte rp = new ControllerResponsableReporte();
        return rp.guardarPersonaResponsable(idReporte, idResponsable);
    }

    //Eliminar a las personas que vestán como Apoyo en un Reporte
    [WebMethod]
    public static bool eliminarPersonaApoyo(int idReporte, int idResponsable)
    {
        ControllerResponsableReporte rp = new ControllerResponsableReporte();
        return rp.eliminarPersonaApoyo(idReporte, idResponsable);
    }

    //Eliminar a las personas que vestán como Responsable en un Reporte
    [WebMethod]
    public static bool eliminarPersonaResponsable(int idReporte, int idResponsable)
    {
        ControllerResponsableReporte rp = new ControllerResponsableReporte();
        return rp.eliminarPersonaResponsable(idReporte, idResponsable);
    }

    //Modificar a las personas que vestán como Responsable en un Reporte
    [WebMethod]
    public static bool editarPersonaResponsable(int idReporte, int idResponsable)
    {
        ControllerResponsableReporte rp = new ControllerResponsableReporte();
        return rp.editarPersonaResponsable(idReporte, idResponsable);
    }

    //Obtener a las personas que aun no han sido asignadas como apoyo en un reporte para que puedan ser asignados.
    [WebMethod]
    public static List<AutoCompleteResponsables> obtenerPersonalApoyo(string term, int idReporte)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToLower();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");       
        query = "SELECT idEmpleado,nombre+' '+apellidoPaterno+' '+apellidoMaterno as nombreCompleto FROM DBSGRH.dbo.tEmpleado rh JOIN tRolUsuarioERPM tr ON tr.idUsuario=rh.idEmpleado WHERE idRolERPM  IN(SELECT idRolERPM FROM tRolUsuarioERPM where idRolERPM=3) AND rh.idEmpleado not in (select idResponsable from tResponsableReporte where idReporte=" + idReporte + ") and nombre like '%" + term + "%'";
        obtener = sp.recuperaRegistros(query);

        if (obtener != null && obtener.Count > 0)
        {
            for (int i = 0; i < obtener.Count; i += 2)
            {
                ac = new AutoCompleteResponsables();
                ac.Id = obtener[i];
                ac.Nombre = obtener[i + 1];
                resultado.Add(ac);
            }
        }
        else
        {
            resultado.Add(new AutoCompleteResponsables { Id = "", Nombre = "No se encontraron resultados" });
        }
        return resultado;
    }
    //Obtener todos los folios (GENERAL)
    [WebMethod]
    public static List<AutoCompleteResponsables> obtenerFoliosReportes(string term)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToLower();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        query = "SELECT folio, folio FROM tReporte WHERE folio <> '' AND idEstatusReporte <>6 AND folio LIKE '%" + term + "%'";
        obtener = sp.recuperaRegistros(query);

        if (obtener != null && obtener.Count > 0)
        {
            for (int i = 0; i < obtener.Count; i += 2)
            {
                ac = new AutoCompleteResponsables();
                ac.Id = obtener[i];
                ac.Nombre = obtener[i + 1];
                resultado.Add(ac);
            }
        }
        else
        {
            resultado.Add(new AutoCompleteResponsables { Id = "", Nombre = "No se encontraron resultados" });
        }
        return resultado;
    }
    //Obtener los Folios de los Reportes Asignados
    [WebMethod]
    public static List<AutoCompleteResponsables> obtenerFoliosReportesAsignados(string term)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToLower();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        query = "SELECT folio, folio FROM tReporte WHERE idEstatusReporte=2 AND folio <> '' AND folio LIKE '%" + term + "%'";
        obtener = sp.recuperaRegistros(query);

        if (obtener != null && obtener.Count > 0)
        {
            for (int i = 0; i < obtener.Count; i += 2)
            {
                ac = new AutoCompleteResponsables();
                ac.Id = obtener[i];
                ac.Nombre = obtener[i + 1];
                resultado.Add(ac);
            }
        }
        else
        {
            resultado.Add(new AutoCompleteResponsables { Id = "", Nombre = "No se encontraron resultados" });
        }
        return resultado;
    }

    //Obtener los Folios de los Reportes Asignados
    [WebMethod]
    public static List<AutoCompleteResponsables> obtenerFoliosReportesSinAsignar(string term)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToLower();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        query = "SELECT folio, folio FROM tReporte WHERE idEstatusReporte=1 AND folio <> '' AND folio LIKE '%" + term + "%'";
        obtener = sp.recuperaRegistros(query);

        if (obtener != null && obtener.Count > 0)
        {
            for (int i = 0; i < obtener.Count; i += 2)
            {
                ac = new AutoCompleteResponsables();
                ac.Id = obtener[i];
                ac.Nombre = obtener[i + 1];
                resultado.Add(ac);
            }
        }
        else
        {
            resultado.Add(new AutoCompleteResponsables { Id = "", Nombre = "No se encontraron resultados" });
        }
        return resultado;
    }

    //Obtener los Reportes Según Filtros (GEMERAL)
    [WebMethod]
    public static string getTblReportesFiltrados(int idAsignado, int idSinAsignar,int idPorValidar,int idTerminado, string folio, DateTime fechaInicio, DateTime fechaFin, int idFallo, int idFuncionalidad, int idERP, int idGrupo)
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.getTblReportesFiltrados(idAsignado, idSinAsignar, idPorValidar, idTerminado, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP, idGrupo, idUsuario);
    }

    [WebMethod]
    public static List<string> generarGraficas(int idAsignado, int idSinAsignar, int idPorValidar, int idTerminado, string folio, DateTime fechaInicio, DateTime fechaFin, int idFallo, int idFuncionalidad, int idERP, int idERPGrupo)
    {
        List<string> obj = new List<string>();
        gestionReportes = new ControllerGestionReportes();
        obj = gestionReportes.generarGraficas(idAsignado, idSinAsignar, idPorValidar, idTerminado, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP, idERPGrupo);
        return obj;
    }

    [WebMethod]
    public static List<tReporte> generarGraficasDinamicas(int idAsignado, int idSinAsignar, int idPorValidar, int idTerminado, string folio, DateTime fechaInicio, DateTime fechaFin, int idFallo, int idFuncionalidad, int idERP)
    {
        List<tReporte> obj = new List<tReporte>();
        gestionReportes = new ControllerGestionReportes();
        obj = gestionReportes.generarGraficasDinamicas(idAsignado, idSinAsignar, idPorValidar, idTerminado, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP);
        return obj;
    }

    

    //Generar PDF General Según filtros
    [WebMethod]
    public static string generarPdfGeneral(int idAsignadoA, string folioA, DateTime fechaInicioA, DateTime fechaFinA, int idFalloA, int idFuncionalidadA, int idERPA, int idSinAsignarS, string folioS, DateTime fechaInicioS, DateTime fechaFinS, int idFalloS, int idFuncionalidadS, int idERPS)
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.generarPdfGeneral(idAsignadoA, folioA, fechaInicioA, fechaFinA, idFalloA, idFuncionalidadA, idERPA, idSinAsignarS, folioS, fechaInicioS, fechaFinS, idFalloS, idFuncionalidadS, idERPS);
    }

    //WebMethod para generar la tabl en donde se encuentran los detalles de las acciones que se realizaron con cada reporte.
    [WebMethod]
    public static string detalleAccionesReporte(int idReporte)
    {
        controller = new ControllerGestionReportes();
        return controller.detalleAccionesReporte(idReporte);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="justifi"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool guardarJustificacion(int idReporte, string justifi) {
        ControllerGestionReportes gr = new ControllerGestionReportes();
        return gr.guardarJustificacion(idReporte,justifi);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="term"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<AutoCompleteResponsables> obtenerGrupos(string term)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToUpper();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        query = "SELECT idERPGrupo, nomGrupo FROM tERPGrupo WHERE nomGrupo LIKE '%" + term + "%'";
        obtener = sp.recuperaRegistros(query);

        if (obtener != null && obtener.Count > 0)
        {
            for (int i = 0; i < obtener.Count; i += 2)
            {
                ac = new AutoCompleteResponsables();
                ac.Id = obtener[i];
                ac.Nombre = obtener[i + 1];
                resultado.Add(ac);
            }
        }
        else
        {
            resultado.Add(new AutoCompleteResponsables { Id = "", Nombre = "No hay resultados" });
        }
        return resultado;
    }
}