using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Configuracion_Configuracion_Configuracion : System.Web.UI.Page
{
    private static ControllerUsuarios cu;
    private static ControllerRoles cr;
    private static int idUsuario;
    private ERPManagementDataContext erp;
    private static Utileria utileria;
    protected void Page_Load(object sender, EventArgs e)
    {
        iniciar();
        utileria = new Utileria();
        idUsuario = int.Parse(Session["id"].ToString());
        txtid.Text = utileria.EncodeTo64(Session["id"].ToString());
        //Validar el permiso de usuario     
        erp = new ERPManagementDataContext();
        var validacion = (from vUser in erp.view_ValidarUsuario
                          where vUser.idUsuario == idUsuario && vUser.idRolERPM == 4
                          select vUser).Count();
        if (validacion <= 0)
        {
            Response.Redirect("../../inicio.aspx");
        }
    }

    public void iniciar()
    {
        Master.generaHeader("../../");
        cu = new ControllerUsuarios();
        //lblTblUsuarios.Text = cu.getTblUsuarios();
        lblTblPuestoUsuarios.Text = cu.getTblPuestoUsuarios();
    }

    [WebMethod]
    public static string getTableUsuarios() {
        cu = new ControllerUsuarios();
        return cu.getTblUsuarios();
    }

    [WebMethod]
    public static string getRolesByUsuario(int idUsuario)
    {
        cr = new ControllerRoles();
        return cr.getCheckRolesUsuario(idUsuario);
    }

    [WebMethod]
    public static string getPermisosByUsuario(int idUsuario)
    {
        cr = new ControllerRoles();
        return cr.getCheckPermisosUsuario(idUsuario);
    }

    [WebMethod]
    public static string getRolesByPuesto(int idPuesto)
    {
        cr = new ControllerRoles();
        return cr.getCheckRolesPuesto(idPuesto);
    }

    [WebMethod]
    public static bool agregarPermisosByUsuario(int idUsuario, string lstPermisos)
    {
        cr = new ControllerRoles();
        return cr.insertaRolesUsuario(idUsuario, lstPermisos);
    }

    [WebMethod]
    public static bool agregarPermisosIncByUsuario(int idUsuario, string lstPermisos)
    {
        cr = new ControllerRoles();
        return cr.insertaPermisosUsuario(idUsuario, lstPermisos);
    }

    [WebMethod]
    public static bool agregarPermisosByPuesto(int idPuesto, string lstPermisos)
    {
        cr = new ControllerRoles();
        return cr.insertaRolesPuesto(idPuesto, lstPermisos);
    }

    [WebMethod]
    public static List<tERPGrupo> obtenerGrupo()
    {
        ControllerRoles cr = new ControllerRoles();
        List<tERPGrupo> tr = new List<tERPGrupo>();
        tr = cr.obtenerGrupo();
        return tr;
    }

    [WebMethod]
    public static List<string> obtenerGrupoActivo(int idUsuario, int idSoporte)
    {
        ControllerRoles cr = new ControllerRoles();
        List<string> tr = new List<string>();
        tr = cr.obtenerGrupoActivo(idUsuario, idSoporte);
        return tr;
    }

    [WebMethod]
    public static List<string> obtenerGrupoPuestoActivo(int idSoporte, int idPuesto)
    {
        ControllerRoles cr = new ControllerRoles();
        List<string> tr = new List<string>();
        tr = cr.obtenerGrupoPuestoActivo(idPuesto, idSoporte);
        return tr;
    }

    //Obtener los Grupos de BDSGRH
    [WebMethod]
    public static List<AutoCompleteResponsables> obtenerGruposRH(string term)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToLower();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        query = "SELECT id_grupo, grupo FROM DBSGRH.DBO.cGrupo WHERE grupo LIKE '%" + term + "%'";
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
            resultado.Add(new AutoCompleteResponsables { Id = "", Nombre = term.ToUpper() });
        }
        return resultado;
    }

    //Obtener las Áreas de BDSGRH
    [WebMethod]
    public static List<AutoCompleteResponsables> obtenerAreasRH(string term)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToLower();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        query = "SELECT idArea, area FROM DBSGRH.DBO.cAreasRH WHERE area LIKE '%" + term + "%'";
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

    //Obtener las Áreas de BDSGRH
    [WebMethod]
    public static bool guardarUsuario(string nombre, string app, string apm, string correo, int idGrupo, int idArea)
    {
        ControllerRoles rp = new ControllerRoles();
        return rp.guardarUsuario(nombre, app, apm, correo, idGrupo, idArea);
    }

    //Saber si existe el Grupo en BDSGRH
    [WebMethod]
    public static bool existeGrupo(string nomGrupo)
    {
        ControllerRoles rp = new ControllerRoles();
        return rp.existeGrupo(nomGrupo);
    }

    [WebMethod]
    public static bool agregarUsuarioConGrupoNuevo(string nombre, string app, string apm, string correo,string nomGrupo, int idArea)
    {
        ControllerRoles rp = new ControllerRoles();
        return rp.agregarUsuarioConGrupoNuevo(nombre, app, apm, correo, nomGrupo, idArea);
    }
}