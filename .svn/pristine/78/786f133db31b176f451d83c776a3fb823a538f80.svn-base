using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Configuracion_Reportes_Reportes : System.Web.UI.Page
{
    private static ControllerReportes controller;
    private static ControllerResponsableReporte controllerResponsableReporte;
    private static int idUsuario;
    private ERPManagementDataContext erp;
    private string gpo;
    private Utileria util;
    private ERPManagementRHDataContext erpRH;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.generaHeader("../../");
        idUsuario = int.Parse(Session["id"].ToString());
        gpo = txtidGrupo.Text;
        getDdlPrioridad();
        //getDdlTipoReporte();

        //getDdlERPGrupo();


        util = new Utileria();
        txtidUser.Text = util.EncodeTo64(Session["id"].ToString());

        //Validar si el usuario es interno o externo, su area y grupo
        erpRH = new ERPManagementRHDataContext();

        var validacionUsu = (from vValUser in erpRH.view_ValidarTipoUsuarioAreaGrupo
                             where vValUser.idUsuario == idUsuario
                             select vValUser).FirstOrDefault();

        erp = new ERPManagementDataContext();
        tERPGrupo terg = new tERPGrupo();
        int tergs = 0;
        if (validacionUsu.idGrupo != "N/A" && validacionUsu.idTipoUsuario == 2)
        {

            terg = (from grupo in erp.tERPGrupo
                    where grupo.idGrupoRH == int.Parse(validacionUsu.idGrupo)
                    select grupo).FirstOrDefault();
            if (terg != null)
            {
                tergs = (from gs in erp.tERPGrupoSistema
                         where gs.idERPGrupo == terg.idERPGrupo
                         select gs).Count();
            }
        }

        if (tergs > 0)
        {
            txtTipoUsuario.Text = "3";
            txtNombreGrupo.Text = "<Label for='txtNombreGrupo' class='frmReporte'>Grupo:</Label><br /><br />" + terg.nomGrupo + "<br/><br/>";
            txtNombreArea.Text = "<Label for='txtNombreArea' class='frmReporte'>Area:</Label><br /><br />" + validacionUsu.area;
            txtIdArea.Text = validacionUsu.idArea;
            txtidGrupo.Text = terg.idERPGrupo.ToString();
            txtNombreGpo.Text = terg.nomGrupo;
        }
        else if (validacionUsu.idTipoUsuario == 2)
        {
            txtidGrupo.Text = terg.idERPGrupo.ToString();
            txtTipoUsuario.Text = validacionUsu.idTipoUsuario.ToString();
            txtNombreGrupo.Text = "<Label for='txtNombreGrupo' class='frmReporte'>Grupo:</Label><br /><br />" + validacionUsu.grupo + "<br/><br/>";
            txtNombreArea.Text = "<Label for='txtNombreArea' class='frmReporte'>Area:</Label><br /><br />" + validacionUsu.area;
            txtIdArea.Text = validacionUsu.idArea;
        }
        else if (validacionUsu.idTipoUsuario == 1)
        {
            txtTipoUsuario.Text = validacionUsu.idTipoUsuario.ToString();
        }


        //if (validacionUsu.idTipoUsuario==2)
        //{            
        //    ddlERPGrupo.Visible = true;
        //    txtIdArea.Text = validacionUsu.idArea;
        //    txtidGrupo.Text = validacionUsu.idGrupo;
        //    lblGrupoExterno.Text = validacionUsu.grupo;
        //    lblAreaExterno.Text = validacionUsu.area;
        //    txtIdUsuario.Text = validacionUsu.idUsuario.ToString();
        //}
        //else if (validacionUsu.idTipoUsuario == 1)
        //{
        //    getLblGrupo();
        //}



        //Validar el permiso de usuario     
        erp = new ERPManagementDataContext();
        var validacion = (from vUser in erp.view_ValidarUsuario
                          where vUser.idUsuario == idUsuario && vUser.idRolERPM == 1
                          select vUser).Count();
        if (validacion <= 0)
        {
            Response.Redirect("../../inicio.aspx");
        }
    }

    /// <summary>
    /// WebMethod por medio del cual se visualizan los detalles al cargar la pantalla de Incidencias
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static bool iniciar()
    {
        controller = new ControllerReportes();
        return controller.iniciar(idUsuario);
    }

    /// <summary>
    /// Método para asignar el grupo correspondiente a cada usuario.
    /// </summary>
    public void getLblGrupo()
    {
        controller = new ControllerReportes();
        List<vUsuariosERPMGrupo> grupo = controller.getGrupo(idUsuario);
        string res = "";
        string id = "";
        foreach (vUsuariosERPMGrupo vusg in grupo)
        {
            res = vusg.grupo;
            id = vusg.id_grupo.ToString();
        }

        lblGrupoUsuario.Text = res;
        txtidGrupo.Text = id;
    }

    /// <summary>
    /// WebMethod para agregar reporte
    /// </summary>
    /// <returns>True si se agrega correctamente el reporte, de lo contrario False.</returns>
    [WebMethod]
    public static bool agregarReporte(string asunto, string descripcion, int idTipoReporte, int idPrioridad, int idSistema, string fechaPropuesta, int idERPGrupo, string nombreArchivo, string idUsuario, int idArea, string ticketVinculado, int sprint, int puntos)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.agregaReporte(asunto, descripcion, idTipoReporte, idPrioridad, idSistema, fechaPropuesta, idUser, idERPGrupo, nombreArchivo, idArea, ticketVinculado,sprint, puntos);
    }

    /// <summary>
    /// Método para llenar el DropDownList ddlTipoReporte con los tipos de reportes que existen en la Base de Datos
    /// </summary>
    //public void getDdlTipoReporte()
    //{
    //    //Validar si el usuario es interno o externo, su area y grupo
    //    erpRH = new ERPManagementRHDataContext();
    //    var validacionUsua = (from vValUserV in erpRH.view_ValidarTipoUsuarioAreaGrupo
    //                          where vValUserV.idUsuario == idUsuario //&& vValUserV.idTipoUsuario !=null && vValUserV.area !=null && vValUserV.grupo !=null
    //                          select vValUserV).FirstOrDefault();
    //    if (validacionUsua.idTipoUsuario == 2)
    //    {
    //        ControllerTiposReportes controllerTipoReportes = new ControllerTiposReportes();
    //        List<cTipoReporte> lstTipoReporte = controllerTipoReportes.getTipoReporte().Where(a => a.idTipoReporte > 4).ToList();//Se crea una Lista de Tipos de Reportes

    //        ListItem lstTiposR = new ListItem("Seleccione una opción", "0");
    //        lstTiposR.Attributes.Add("disabled", "disabled");
    //        ddlTipoReporte.Items.Add(lstTiposR);
    //        foreach (cTipoReporte ctr in lstTipoReporte)
    //        {
    //            ListItem lstTipos = new ListItem(ctr.nombreTipoReporte, ctr.idTipoReporte.ToString());//Se crea un nuevo item por cada elemento de la lista
    //            ddlTipoReporte.Items.Add(lstTipos); //Se agrega el item al combo
    //        }
    //    }
    //    else if (validacionUsua.idTipoUsuario == 1)
    //    {
    //        ControllerTiposReportes controllerTipoReportes = new ControllerTiposReportes();
    //        List<cTipoReporte> lstTipoReporte = controllerTipoReportes.getTipoReporte().Where(a => a.idTipoReporte < 5).ToList();//Se crea una Lista de Tipos de Reportes

    //        ListItem lstTiposR = new ListItem("Seleccione una opción", "0");
    //        lstTiposR.Attributes.Add("disabled", "disabled");
    //        ddlTipoReporte.Items.Add(lstTiposR);
    //        foreach (cTipoReporte ctr in lstTipoReporte)
    //        {
    //            ListItem lstTipos = new ListItem(ctr.nombreTipoReporte, ctr.idTipoReporte.ToString());//Se crea un nuevo item por cada elemento de la lista
    //            ddlTipoReporte.Items.Add(lstTipos); //Se agrega el item al combo
    //        }
    //    }

    //}

    /// <summary>
    /// Método para llenar el DropDownList ddlPrioridad con las prioridades de los reportes que existentes en Base de Datos
    /// </summary>
    public void getDdlPrioridad()
    {
        ControllerPrioridad controllerPrioridad = new ControllerPrioridad();
        List<cPrioridadReporte> lstPrioridadReportes = controllerPrioridad.getPrioridad();

        ListItem lstPriori = new ListItem("Seleccione una opción", "0");
        lstPriori.Attributes.Add("disabled", "disabled");
        ddlPrioridad.Items.Add(lstPriori);
        foreach (cPrioridadReporte cpr in lstPrioridadReportes)
        {
            ListItem lstPrioridades = new ListItem(cpr.nombrePrioridad, cpr.idPrioridadReporte.ToString());
            ddlPrioridad.Items.Add(lstPrioridades);
        }
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

    ///// <summary>
    ///// Método para llenar el DropDownList ddlERPGrupo con los Grupos que existen en Base de Datos
    ///// </summary>
    //[WebMethod]
    //public void getDdlERPGrupo()
    //{
    //    ControllerERPGrupo controllerERPGrupo = new ControllerERPGrupo();
    //    erpRH = new ERPManagementRHDataContext();
    //    var validacionUsua = (from vValUserV in erpRH.view_ValidarTipoUsuarioAreaGrupo
    //                          where vValUserV.idUsuario == idUsuario //&& vValUserV.idTipoUsuario !=null && vValUserV.area !=null && vValUserV.grupo !=null
    //                          select vValUserV).FirstOrDefault();
    //    if (validacionUsua.idTipoUsuario == 2)
    //    {
    //        EnableViewState = true;
    //        List<tERPGrupo> lstERPGrupoExt = controllerERPGrupo.getERPGrupo().Where(a => a.idGrupoRH.Equals(validacionUsua.idGrupo)).ToList();//Se crea una Lista de Tipos de Reportes
    //        ListItem lstERPGExt = new ListItem(validacionUsua.grupo, validacionUsua.idGrupo.ToString());
    //        //lstERPGExt.Attributes.Add("disabled", "disabled");
    //        ddlERPGrupo.Items.Add(lstERPGExt);
    //    }
    //    else
    //    {
    //        List<tERPGrupo> lstERPGrupo = controllerERPGrupo.getERPGrupo();
    //        ListItem lstERPG = new ListItem("Seleccione una opción", "0");
    //        lstERPG.Attributes.Add("disabled", "disabled");
    //        ddlERPGrupo.Items.Add(lstERPG);
    //        foreach (tERPGrupo terp in lstERPGrupo)
    //        {
    //            ListItem lstERP = new ListItem(terp.nomGrupo, terp.idERPGrupo.ToString());
    //            ddlERPGrupo.Items.Add(lstERP);
    //        }
    //    }
    //}

    [WebMethod]
    public static List<view_ValidarTipoUsuarioAreaGrupo> getEstatusUsuario()
    {
        controller = new ControllerReportes();
        return controller.getEstatusUsuario(idUsuario);
    }

    /// <summary>
    /// WebMethod para generar la tabla de los reportes que han sido generados, 
    /// pero que contengan el estatus 1, 2, 3 o 5 (Generado/Asignado/Por Validar/Terminado)
    /// </summary>
    /// <returns>Estructura de la tabla de los reportes generados</returns>
    [WebMethod]
    public static string getReportesCreados(string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getTblReportesCreados(idUser);
    }

    /// <summary>
    /// WebMethod para obtener el detalle de los reportes que han sido generados
    /// </summary>
    /// <returns>Lista con los datos de un reporte</returns>
    [WebMethod]
    public static List<string> getDetalleReporte(int idReporte)
    {
        List<string> lstDetalleReporte = new List<string>();
        controller = new ControllerReportes();
        tReporte tr = controller.getReporteGeneradoById(idReporte);

        lstDetalleReporte.Add(tr.asunto);
        lstDetalleReporte.Add(tr.descripcion);
        lstDetalleReporte.Add(tr.cPrioridadReporte.nombrePrioridad);

        return lstDetalleReporte;

    }

    /// <summary>
    /// WebMethod para obtener los detalles de los reportes generados, mostrando todos sus detalles
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>Objeto de tipo view_DetalleReporte</returns>
    [WebMethod]
    public static view_DetalleReporte consultarReporteCreado(int idReporte)
    {
        controller = new ControllerReportes();
        return controller.consultarReporteCreado(idReporte);
    }

    /// <summary>
    /// WebMethod para eliminar un reporte que aun no ha sido asignado. 
    /// </summary>
    /// <returns>True si se elimina de manera correcta, de lo contrario false</returns>
    [WebMethod]
    public static bool eliminarReporteCreado(int idReporte, string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.eliminarReporteCreado(idReporte, idUser);
    }

    /// <summary>
    /// WebMethod para modificar un reporte que aun no ha sido generado 
    /// </summary>
    /// <returns>True si el reporte es modificado de manera correcta, de lo contrario False</returns>
    [WebMethod]
    public static bool modificarReporteCreado(int idReporte, string asunto, string descripcion, int idTipoReporte, int idPrioridad, int idSistema, int idERPGrupo, string fechaPropuesta, string nombreArchivo, string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        //int idUser = int.Parse(idUsuario);
        return controller.modificaReporte(idReporte, asunto, descripcion, idTipoReporte, idPrioridad, idSistema, idERPGrupo, fechaPropuesta, nombreArchivo, idUser);
    }

    /// <summary>
    /// WebMethod para generar la tabla en donde se encuentra el log de las acciones realizadas con los reportes.
    /// </summary>
    /// <returns>Estructura de la tabla para ale log con las incidencias</returns>
    [WebMethod]
    public static string getLogReportes(string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getTblLogReportes(idUser);
    }

    /// <summary>
    /// WebMethod para crear la tabla en donde solo se encuentran los reportes que no han sido asignados y 
    /// se encuentran en el estatus 1 (Generado)
    /// </summary>
    /// <returns>Estructura de la tabla</returns>
    [WebMethod]
    public static string getReportesGenerados(string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getTblReportesGenerados(idUser);
    }

    /// <summary>
    /// WebMethod para generar la tabla en donde solamente los reportes que se encuentran en el estatus 2(Asignados)
    /// </summary>
    /// <returns>Estructura de la tabla</returns>
    [WebMethod]
    public static string getReportesEnSoporte(string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getTblReportesEnSoporte(idUser);
    }

    /// <summary>
    /// WebMethod para generar la tabla en donde solamente los reportes que se encuentran en el estatus 3(Por Validar)
    /// </summary>
    /// <returns>Estructura de la tabla</returns>
    [WebMethod]
    public static string getReportesValidar(string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getTblReportesValidar(idUser);
    }

    /// <summary>
    /// WebMethod para hacer la tabla en la que estan los reportes que han sido concluidos o con el estatus 5
    /// </summary>
    /// <returns>Estructura de la tabla</returns>
    [WebMethod]
    public static string getReportesTerminados(string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getTblReportesTerminados(idUser);
    }

    /// <summary>
    /// WebMethod para generar la tabla en donde se encuentran los detalles de las acciones que 
    /// se realizaron con cada reporte.
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>Estructura de la tabla</returns>
    [WebMethod]
    public static string detalleAccionesReporte(int idReporte, string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getDetalleAccionesReporte(idReporte, idUser);
    }

    /// <summary>
    /// WebMethod para obtener los sistemas de un grupo de un ERP
    /// </summary>
    /// <param name="idGrupo"></param>
    /// <returns>Cadena en la que se encuentran los sistemas.</returns>
    [WebMethod]
    public static string getSistemasERP(int idGrupo)
    {
        controller = new ControllerReportes();
        return controller.getSistemasERP(idGrupo);
    }

    /// <summary>
    /// WebMethod para el autocomplete de los grupos
    /// </summary>
    /// <returns>Lista de las conicidencias ingresadas por el usuario</returns>
    [WebMethod]
    public static List<AutoCompleteResponsables> obtenerGrupos(string term)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToUpper();
        term = term.Replace(" ", "");
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        query = "SELECT idERPGrupo, UPPER(nomGrupo) AS nomGrupo FROM tERPGrupo WHERE nomGrupo LIKE '%" + term + "%' ORDER BY nomGrupo ASC";
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
            resultado.Add(new AutoCompleteResponsables { Id = "", Nombre = term });
        }
        return resultado;
    }

    /// <summary>
    /// WebMethod para cargar responsables al inicio.
    /// </summary>
    /// <returns>Lista de coincidencias encontradas de a cuerdo a los datos introducidos por el usuario</returns>
    [WebMethod]
    public static List<AutoCompleteResponsables> obtenerGruposInicio(string term)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToUpper();
        term.Replace(" ", "");
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        query = "SELECT idERPGrupo, UPPER(nomGrupo) AS nomGrupo FROM tERPGrupo WHERE nomGrupo LIKE '%" + term + "%' ORDER BY nomGrupo ASC";

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
    /// WebMethod por medio del cual se lleva a cabo el registro  de un nuevo grupo en la Base de Datos
    /// </summary>
    /// <returns>True si se agrega correctamente, de lo contrario, false,</returns>
    [WebMethod]
    public static bool agregarGrupo(string nombreGrupo, string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.agregarGrupo(nombreGrupo, idUser);
    }

    /// <summary>
    /// WebMethod por medio del cual se obtiene el ID de un grupo ingresado por el usuario
    /// </summary>
    /// <returns>True si se registra de manera exitosa, por el contrario sería False.</returns>
    [WebMethod]
    public static int obtenerIdGrupo(string nombreGrupo)
    {
        controller = new ControllerReportes();
        return controller.obtenerIdGrupo(nombreGrupo);
    }

    /// <summary>
    /// WebMethod con el cual se agrega una nueva peticion de ERP
    /// </summary>
    /// <returns>True si se registra de manera exitosa, por el contrario sería False</returns>
    [WebMethod]
    public static bool agregarPeticion(string asunto, string descripcion, int idTipoReporte, int idPrioridad, string fechaPropuesta, int idERPGrupo, string sistemas, string nombreArchivo, string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.agregarPeticion(asunto, descripcion, idTipoReporte, idPrioridad, fechaPropuesta, idERPGrupo, sistemas, nombreArchivo, idUser);
    }

    /// <summary>
    /// WebMethod para visualizar el detalle de una petición de ERP
    /// </summary>
    /// <returns>Objeto de tipo view_DetalleReporte</returns>
    [WebMethod]
    public static view_DetalleReporte consultarReporteERP(int idReporte)
    {
        controller = new ControllerReportes();
        return controller.consultarReporteERP(idReporte);
    }

    /// <summary>
    /// WebMethod por medio del cual se obtienen los sistemas seleccionados cuando se realiza la petición de un ERP.
    /// </summary>
    /// <returns>Sistemas correspondientes al reporte</returns>
    [WebMethod]
    public static string getSistemasERPSeleccionados(int idReporte)
    {
        controller = new ControllerReportes();
        return controller.getSistemasERPSeleccionados(idReporte);
    }

    /// <summary>
    /// WebMethod para obtener los sistemas para modificar una peticion de ERP
    /// </summary>
    /// <returns>Estructura de los sistemas para modificar</returns>
    [WebMethod]
    public static string getSistemasERPModificar(int idReporte)
    {
        controller = new ControllerReportes();
        return controller.getSistemasERPModificar(idReporte);
    }

    /// <summary>
    /// WebMethod para modificar una peticion de ERP
    /// </summary>
    /// <returns>True si se modifica de manera exitosa, de lo contrario false.</returns>
    [WebMethod]
    public static bool modificaReporteERP(int idReporte, string asunto, string descripcion, int idTipoReporte, int idPrioridad, int idERPGrupo, string fechaPropuesta, string sistemas, string nombreArchivo, string nomGrupo, string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.modificaReporteERP(idReporte, asunto, descripcion, idTipoReporte, idPrioridad, idERPGrupo, fechaPropuesta, sistemas, nombreArchivo, nomGrupo, idUser);
    }

    /// <summary>
    /// WebMethod para eliminar una petición de ERP
    /// </summary>
    /// <returns>True si se elimina de manera correcta, de los contrario false.</returns>
    [WebMethod]
    public static bool eliminarReporteERP(int idReporte, string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.eliminarReporteERP(idReporte, idUser);
    }

    /// <summary>
    /// WebMethod para guardar la justificacion de cuando se elimina una incidencia
    /// </summary>
    /// <returns>True si se guarda correctamente, de los contrario false.</returns>
    [WebMethod]
    public static bool guardarJustificacion(int idReporte, string justifi)
    {
        ControllerGestionReportes gr = new ControllerGestionReportes();
        return gr.guardarJustificacion(idReporte, justifi);
    }

    /// <summary>
    /// WebMethod por medio del cual se obtiene el responsable de un reporte
    /// </summary>
    /// <returns>Responsable de un reporte</returns>
    [WebMethod]
    public static string obtenerResponsable(int idReporte)
    {
        controller = new ControllerReportes();
        return controller.obtenerResponsable(idReporte);
    }

    /// <summary>
    /// WebMethod para obtener el nombre de un grupo
    /// </summary>
    /// <returns>Nombre del grupo</returns>
    [WebMethod]
    public static string obtenerNombreGrupo(int idGrupo)
    {
        controller = new ControllerReportes();
        return controller.obtenerNombreGrupo(idGrupo);
    }

    /// <summary>
    /// WebMethod para obtener todos los sistemas que puede contener un ERP
    /// </summary>
    /// <returns>Sistemas de un ERP</returns>
    [WebMethod]
    public static string getSistemasTodosERP()
    {
        controller = new ControllerReportes();
        return controller.getSistemasTodosERP();
    }

    /// <summary>
    /// WebMethod para obtener la tabla de las lecciones aprendidas de cada usuario
    /// </summary>
    /// <returns>Estructura de la tabla de las lecciones aprendidas</returns>
    [WebMethod]
    public static string getLeccionesAprendidas(string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getTblLeccionesAprendidas(idUser);
    }

    /// <summary>
    /// WebMethod para obtener la informacion de casa una de las lecciones aprendidas
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>Lista con los datos de la leccion aprendida</returns>
    [WebMethod]
    public static vLeccionesAprendidas consultarLeccionesAprendidas(int idLecciones)
    {
        controller = new ControllerReportes();
        return controller.consultarLeccionesAprendidas(idLecciones);
    }

    /// <summary>
    /// Eliminar Reportes (GENERAL)
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>true si es eliminado, false de lo contrario</returns>
    [WebMethod]
    public static bool eliminarReporte(int idReporte, string idUsuario)
    {
        ERPManagementDataContext erp = new ERPManagementDataContext();
        tLogReporte tlreporte = new tLogReporte();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        try
        {
            var reporte = (from tr in erp.tReporte
                           where tr.idReporte == idReporte
                           select tr).FirstOrDefault();

            reporte.idEstatusReporte = 6;

            erp.SubmitChanges();

            string fechaAccion = DateTime.Now.ToShortDateString();
            DateTime fechaAcc = Convert.ToDateTime(fechaAccion);
            tlreporte.idReporte = idReporte;
            tlreporte.idUsuario = idUser;
            tlreporte.fecha = fechaAcc;
            tlreporte.idAccion = 2;

            erp.tLogReporte.InsertOnSubmit(tlreporte);
            erp.SubmitChanges();

            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    [WebMethod]
    public static List<string> cboTipoReporte(int tipoReporte, int idGrupo)
    {
        ERPManagementDataContext erp = new ERPManagementDataContext();
        List<cTipoReporte> lstct = new List<cTipoReporte>();

        List<string> resultado = new List<string>();
        string cboTipoReporte = "";
        string res = "";
        if (tipoReporte < 5 || tipoReporte == 14 || tipoReporte == 15 || tipoReporte == 16 || tipoReporte == 17)
        {
            lstct = (from ctr in erp.cTipoReportes
                     where ctr.idTipoReporte < 5 || ctr.idTipoReporte == 14 || ctr.idTipoReporte == 15 || ctr.idTipoReporte == 16 || ctr.idTipoReporte == 17 || ctr.idTipoReporte == 18 || ctr.idTipoReporte == 19 || ctr.idTipoReporte == 20 || ctr.idTipoReporte == 21 || ctr.idTipoReporte == 22
                     select ctr).ToList();
            cboTipoReporte += "<Label for='cboTR' class='frmReporte'>Tipo Incidencia:<label><br/>";

            ControllerSistema controllerSistema = new ControllerSistema();
            res = controllerSistema.getSistemasGrupo(idGrupo);

        }
        else
        {
            lstct = (from ctr in erp.cTipoReportes
                     where ctr.idTipoReporte > 4
                     select ctr).ToList();
            cboTipoReporte += "<Label for='ddlTipoReporte' class='frmReporte'>Tipo Consulta:<label><br/>";
        }

        cboTipoReporte += "<select id='ddlTipoReporte' class='validaCombo select inputP50' onchange='javascript:tipoReporte();'><option value='0' disabled='disabled' selected>Seleccione una opción</option>";
        foreach (cTipoReporte cr in lstct)
        {
            cboTipoReporte += "<option value=" + cr.idTipoReporte + ">" + cr.nombreTipoReporte + "</option>";
        }
        cboTipoReporte += "</select><br>";

        resultado.Add(cboTipoReporte);
        resultado.Add(res);

        return resultado;
    }
    [WebMethod]
    public static List<string> cboTipoReporteGrupo()
    {
        ERPManagementDataContext erp = new ERPManagementDataContext();
        List<cTipoReporte> lstct = new List<cTipoReporte>();
        List<string> resultado = new List<string>();

        string res = "<Label for='cboTR' class='frmReporte'>Tipo Incidencia:<label><br/>" +
                    "<select id='ddlTipoReporte' class='validaCombo select inputP50' onchange='javascript:tipoReporte();'><option value='0' disabled='disabled' selected>Seleccione una opción</option>";

        lstct = (from ctr in erp.cTipoReportes
                 where ctr.idTipoReporte < 5 || ctr.idTipoReporte == 14 || ctr.idTipoReporte == 15 || ctr.idTipoReporte == 16 || ctr.idTipoReporte == 17 || ctr.idTipoReporte == 18 || ctr.idTipoReporte == 19 || ctr.idTipoReporte == 20 || ctr.idTipoReporte == 21 || ctr.idTipoReporte == 22 || ctr.idTipoReporte == 25
                 select ctr).ToList();
        foreach (cTipoReporte ct in lstct)
        {
            res += "<option value=" + ct.idTipoReporte + ">" + ct.nombreTipoReporte + "</option>";
        }

        res += "</select><br>";
        ControllerERPGrupo controllerERPGrupo = new ControllerERPGrupo();
        List<tERPGrupo> lstERPGrupo = controllerERPGrupo.getERPGrupo();

        string cboGrupo = "<Label for='ddlERPGrupo' class='frmReporte'>Grupo:</label>" +
                    "<select id='ddlERPGrupo' class='validaCombo select inputP98' onchange='javascript:ddlSistemas();'><option value'0' disabled selected>Seleccione una opción</option>";

        foreach (tERPGrupo terpg in lstERPGrupo)
        {
            cboGrupo += "<option value=" + terpg.idERPGrupo + ">" + terpg.nomGrupo.ToUpper() + "</option>";
        }
        cboGrupo += "</select><br>";

        resultado.Add(res);
        resultado.Add(cboGrupo);

        return resultado;
    }
}