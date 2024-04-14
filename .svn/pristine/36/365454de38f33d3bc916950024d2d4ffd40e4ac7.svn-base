using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.util;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Configuracion_Reportes_ModificarReportes : System.Web.UI.Page
{
    private static ControllerGestionReportes controller;
    private static ControllerGestionReportes gestionReportes;
    private static int idUsuario;
    private ERPManagementDataContext erp;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["id"].ToString() == null || Session["id"].ToString() =="")
        {
            Response.Redirect("../../inicio.aspx");
        }
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

    /// <summary>
    /// WebMethod para agregar reporte
    /// </summary>
    /// <returns>True si se agrega correctamente el reporte, de lo contrario False.</returns>
    [WebMethod]
    public static bool modificarReporte(int idReporte, int idTipoReporte, int idPrioridad, int idSistema, int idERPGrupo)
    {
        ControllerReportes  controller = new ControllerReportes();
        return controller.modificarReportes(idReporte, idTipoReporte, idPrioridad, idSistema, idERPGrupo, idUsuario);
    }

    //Traer todos los Reportes
    [WebMethod]
    public static string getTblReportes()
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.getTblReportes(idUsuario, true);
    }

    
    //Consultar Reporte (GENERAL)
    [WebMethod]
    public static List<string> consultarReporte(int idReporte)
    {
        return gestionReportes.consultarReporte(idReporte);
    }
   
    //Consultar Reporte Sin Asignar
    [WebMethod]
    public static List<string> consultarReporteSinAsignar(int idReporte)
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.consultarReporteSinAsignar(idReporte);
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
    public static string getTblReportesFiltrados(int idAsignado, int idSinAsignar, int idPorValidar, int idTerminado, string folio, DateTime fechaInicio, DateTime fechaFin, int idFallo, int idFuncionalidad, int idERP, int idGrupo)
    {
        gestionReportes = new ControllerGestionReportes();
        return gestionReportes.getTblReportesFiltrados(idAsignado, idSinAsignar, idPorValidar, idTerminado, folio, fechaInicio, fechaFin, idFallo, idFuncionalidad, idERP, idGrupo, idUsuario, true);
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
    public static bool guardarJustificacion(int idReporte, string justifi)
    {
        ControllerGestionReportes gr = new ControllerGestionReportes();
        return gr.guardarJustificacion(idReporte, justifi);
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



    /// <summary>
    /// Método para llenar el DropDownList ddlPrioridad,ddlPrioridad,ddlERPGrupo, con los registros que existentes en Base de Datos
    /// </summary>
    [WebMethod]
    public static List<string> getDdlReporte(int idPrioridad, int idTipoIncidencia, int idGrupo, int idSistema)
    {
        List<string> lsResultado = new List<string>();

        string sResultado = "";

        //Obtener las incidencias
        ControllerPrioridad controllerPrioridad = new ControllerPrioridad();
        List<cPrioridadReporte> lstPrioridadReportes = controllerPrioridad.getPrioridad();
        
        sResultado += " <Label for='ddlPrioridad' class='txt-verde frm'> Prioridad: </label> <br>";

        sResultado += " <select id='ddlPrioridad' class='validaCombo select inputP50'> ";

        foreach (cPrioridadReporte cpr in lstPrioridadReportes)
        {
            if (idPrioridad == cpr.idPrioridadReporte) // Seleccionar la opción por valor
            {
                sResultado += "<option value=" + cpr.idPrioridadReporte + " selected >" + cpr.nombrePrioridad + "</option>";
            }
            else
            {
                sResultado += "<option value=" + cpr.idPrioridadReporte + ">" + cpr.nombrePrioridad + "</option>";
            }

        }
        sResultado += "</select><br>";
        //Agregar list al resultado
        lsResultado.Add(sResultado);

        sResultado = "";

        //Obtener los TipoIncidencia 
        ControllerTiposReportes controllerTipoReporte = new ControllerTiposReportes();
        List<cTipoReporte> lstTipoReportes = controllerTipoReporte.getTipoReporte();
        
        sResultado += " <Label for='ddlTipoReporte' class='txt-verde frm'> TipoIncidencia: </label> <br>";
        sResultado += " <select id='ddlTipoReporte' class='validaCombo select inputP50'> ";

        foreach (cTipoReporte cpr in lstTipoReportes)
        {
            if (idTipoIncidencia == cpr.idTipoReporte) // Seleccionar la opción por valor
            {
                sResultado += "<option value=" + cpr.idTipoReporte + " selected >" + cpr.nombreTipoReporte + "</option>";
            }
            else
            {
                sResultado += "<option value=" + cpr.idTipoReporte + "  >" + cpr.nombreTipoReporte + "</option>";
            }

        }
        sResultado += "</select><br>";
        lsResultado.Add(sResultado);

        sResultado = "";

        //Obtener los grupos
        ControllerERPGrupo controllerERPGrupo = new ControllerERPGrupo();
        List<tERPGrupo> lstERPGrupo = controllerERPGrupo.getERPGrupo();

        sResultado += "<Label for='ddlERPGrupo' class='txt-verde frm'>Grupo:</label>" +
                    "<select id='ddlERPGrupo' class='validaCombo select inputP98' onchange='javascript:ddlSistemas();'>";

        foreach (tERPGrupo terpg in lstERPGrupo)
        {
            if(terpg.idERPGrupo == idGrupo)  // Seleccionar la opción por valor
            {
                sResultado += "<option value=" + terpg.idERPGrupo + " selected >" + terpg.nomGrupo.ToUpper() + "</option>";
            }
            else
            {
                sResultado += "<option value=" + terpg.idERPGrupo + ">" + terpg.nomGrupo.ToUpper() + "</option>";
            }
            
        }
        sResultado += "</select><br>";

        lsResultado.Add(sResultado);

        

        sResultado = "";

        ControllerSistema controllerSistema = new ControllerSistema();
        if (idSistema != 0)
        {
            sResultado += controllerSistema.getSistemasIdGrupo(idGrupo, idSistema);
        }
        else
        {
            sResultado += controllerSistema.getSistemasGrupo(idGrupo);
        }

        lsResultado.Add(sResultado);

       
        return lsResultado;
    }


    /// <summary>
    /// WebMethod para llenar el combo de los sistemas de cada grupo
    /// </summary>
    /// <returns>Estructura de la lista desplegable de los sistemas</returns>
    [WebMethod]
    public static string getDdlSistemasGrupo(int idGrupo)
    {
        ControllerSistema controllerSistema = new ControllerSistema();
        return controllerSistema.getSistemasGrupo(idGrupo);
    }

}

