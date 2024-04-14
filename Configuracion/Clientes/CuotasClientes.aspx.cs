using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Configuracion_Clientes_CuotasClientes : System.Web.UI.Page
{
    private static ControllerClientes clientes;
    private ERPManagementDataContext erp;
    private static int idUsuario;
    protected void Page_Load(object sender, EventArgs e)
    {
        idUsuario = int.Parse(Session["id"].ToString());
        Master.generaHeader("../../");
        //Validar el permiso de usuario     
        erp = new ERPManagementDataContext();
        var validacion = (from vUser in erp.view_ValidarUsuario
                          where vUser.idUsuario == idUsuario && (vUser.idRolERPM==5 || vUser.idRolERPM==7)
                          select vUser).Count();
        if (validacion <= 0)
        {         
            Response.Redirect("../../inicio.aspx");
        }

    }

    /// <summary>
    /// Obtener todas ls cuotas de los Grupos y sus sistemas contarados(GENERAL)
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string getCuotas()
    {
        clientes = new ControllerClientes();
        return clientes.getCuotas(idUsuario);
    }

    /* Mostrar todos los grupos existentes  */
    [WebMethod]
    public static string getGrupos()
    {
        clientes = new ControllerClientes();
        return clientes.getGrupos();
    }

    /// <summary>
    /// Obtener la información de la Cuota para que puedan modiciar los datos
    /// </summary>
    /// <param name="idCuota"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<string> obtenerCuota(int idCuota) {
        tCuotas cuota = new tCuotas();
        clientes = new ControllerClientes();
        return clientes.obtenerCuota(idCuota);
    }

    /// <summary>
    /// Modificar la cinformación de la Cuota
    /// </summary>
    /// <param name="idCuota"></param>
    /// <param name="cuota"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaVencimiento"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool modificarCuota(int idCuota, double cuota, string fechaInicio, string fechaVencimiento, string personalResp, int idERPGruposistema, int tMoneda)
    {
        clientes = new ControllerClientes();
        return clientes.modificarCuota(idCuota, cuota, fechaInicio, fechaVencimiento, personalResp, idERPGruposistema, tMoneda);
    }

    /// <summary>
    /// Eliminar una Cuota (Se cambia a estatus 2 de eliminado)
    /// </summary>
    /// <param name="idCuota"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool eliminarCuota(int idCuota)
    {
        clientes = new ControllerClientes();
        return clientes.eliminarCuota(idCuota);
    }

    /// <summary>
    /// Cargar los grupos existentes para el autocomplete
    /// </summary>
    /// <param name="term"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<AutoCompleteResponsables> cargarGrupos(string term)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToLower();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        query = "select idERPGrupo,nomGrupo from tERPGrupo WHERE nomGrupo LIKE '%" + term + "%'";
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

    /// <summary>
    /// Cargar los sistemas existentes para el autocomplete
    /// </summary>
    /// <param name="term"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<AutoCompleteResponsables> cargarSistemas(string term)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToLower();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        query = "select idSistema, nomSistema from cSistemas WHERE nomSistema LIKE '%" + term + "%'";
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

    /// <summary>
    /// Obtener las Cuotas segun los filtros de búsqueda
    /// </summary>
    /// <param name="idGrupo"></param>
    /// <param name="idSistema"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <returns></returns>
    [WebMethod]
    public static string obtenerCuotasFiltros(int idGrupo, int idSistema, DateTime fechaInicio, DateTime fechaFin)
    {
        clientes = new ControllerClientes();
        return clientes.obtenerCuotasFiltros(idGrupo, idSistema, fechaInicio, fechaFin);
    }

    [WebMethod]
    public static List<string> obtenerResponsablesSistema()
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        string query = "";
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");

        query = "SELECT idEmpleado, nombreCompleto FROM vUsuariosERPM vista JOIN tRolPuestoERPM trp on vista.idPuesto=trp.idPuesto" +
                " where vista.idPuesto in (3,4,5)";

        obtener = sp.recuperaRegistros(query);

        return obtener;
    }

    [WebMethod]
    public static List<string> obtenerResponsablesSistemaActivo(int idCuota, int idERPGrupoSistema)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        string query = "";
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");

        query = "select idResponsable, nombreCompleto from tResponsableSistema trs inner join vUsuariosERPM vu on trs.idResponsable=vu.idEmpleado where idERPGrupoSistema='" + idERPGrupoSistema + "'";

        obtener = sp.recuperaRegistros(query);
        return obtener;
    }
    
}