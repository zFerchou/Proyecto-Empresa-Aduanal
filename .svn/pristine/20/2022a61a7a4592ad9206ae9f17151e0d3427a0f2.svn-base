using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.util;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Configuracion_ProductBackLog_ProductBackLog : System.Web.UI.Page
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
        getDdlPrioridad();
        getDdlEstatusHistoria();
        getDdlTipoImpacto();
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
            txtNombreGpo.Text = terg.nomGrupo;
        }
        else if (validacionUsu.idTipoUsuario == 2)
        {
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

    public void getDdlPrioridad()
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string sQuery = "select * from cEstatusProductBackLog";
        DataSet ds = sp.getValues(sQuery);

        ListItem lstPriori = new ListItem("Seleccione una opción", "0");
        lstPriori.Attributes.Add("disabled", "disabled");
        ddlEstatus.Items.Add(lstPriori);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            ListItem lstPrioridades = new ListItem(row[1].ToString(), row[0].ToString());
            ddlEstatus.Items.Add(lstPrioridades);
        }
    }
    public void getDdlTipoImpacto()
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string sQuery = "select * from cTipoImpacto";
        DataSet ds = sp.getValues(sQuery);

        ListItem lstPriori = new ListItem();
        lstPriori.Attributes.Add("disabled", "disabled");
        ddlTipoImpacto.Items.Add(lstPriori);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            ListItem lstPrioridades = new ListItem(row[1].ToString(), row[0].ToString());
            ddlTipoImpacto.Items.Add(lstPrioridades);
        }
    }

    [WebMethod]
    public static string[] TipoImpacto(int id)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string sQuery = "select idTipoImpacto from ProductBackLog_TipoImpacto where idProductBackLog =" + id;
        DataSet ds = sp.getValues(sQuery);

        // Convierte los valores del DataSet en un array de strings
        List<string> valoresList = new List<string>();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            valoresList.Add(row[0].ToString());
        }

        // Convierte la lista en un array
        string[] valoresArray = valoresList.ToArray();

        return valoresArray;
    }

    public void getDdlEstatusHistoria()
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

    [WebMethod]
    public static string getReportesCreados(string idUsuario, int idEstatus)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.getTblProductBackLog(idUser,idEstatus);
    }

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

    [WebMethod]
    public static string getDdlSistemasGrupo(int idGrupo)
    {
        ControllerSistema controllerSistema = new ControllerSistema();
        return controllerSistema.getSistemasGrupo(idGrupo);
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

    [WebMethod]
    public static bool agregarHistoria(string epica, string descripcion, string historia, string criterios, int idPrioridad, int idSistema,  string idERPGrupo, string riesgos, string evidencia,string[] tipoImpacto, string idUsuario)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.agregaHistoria(epica,descripcion,historia,criterios,idPrioridad,idSistema,idERPGrupo,riesgos,evidencia, tipoImpacto, idUser);
    }

    [WebMethod]
    public static List<string> consultarHistoriaCreada(int idHistoria)
    {
        controller = new ControllerReportes();
        return controller.consultarHistoriaCreada(idHistoria);
    }

    [WebMethod]
    public static bool modificarHistoria(string epica, string descripcion, string historia, string criterios, int idPrioridad, int idSistema, string idERPGrupo, string riesgos, string idUsuario, int idHistoria, int estatus, string evidencia, string[] tipoImpacto, int storyPoint)
    {
        controller = new ControllerReportes();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.actualizarHistoria(epica, descripcion, historia, criterios, idPrioridad, idSistema, idERPGrupo, riesgos, idUser,idHistoria,estatus, evidencia,tipoImpacto, storyPoint);
    }

    [WebMethod]
    public static string generarExcelHistorias(int idEstatus)
    {
        controller = new ControllerReportes();
        return controller.generarExcelHistorias(idEstatus);
    }

    [WebMethod]
    public static string leerExcelHistorias(string nombreExcel)
    {
        controller = new ControllerReportes();
        return controller.leerExcelHistorias(nombreExcel);
    }

    [WebMethod]
    public static string getDdlSprint()
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string sQuery = "Declare @fechaSprint Date = GETDATE()\r\nselect idSprint,iNumeroSprint from tSprint where dFechaFin >= @fechaSprint";
        DataSet ds = sp.getValues(sQuery);
        
        string result = "<label class='frmReporte'>Selecciona un Sprint:</label><br/><select id='ddlSeleccionaSprint' class=' select inputP98'>";
        result += "<option value='0' disabled selected='true'>Seleccione una opción</option>";
        
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            result += "<option value=" + row[0].ToString() + "> Número de Sprint " + row[1].ToString() + "</option>";
        }
        result += "</select><br>";

        return result;
    }

    [WebMethod]
    public static bool asignarSprint(int idSprint, string historias) 
    {
        controller = new ControllerReportes();
        return controller.AsignarHistorias(idSprint,historias);
        
    }

    [WebMethod]
    public static List<string> start()
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string query = "";
        List<string> resultado = new List<string>(); ;
        string res = "";
        int rNuevo = 0;
        int rRefinado = 0;
        int rPuntuando = 0;
        int rSprint = 0;


        //se llaman los mettodos para contar el numero de incidencias se encuentran en los estatus 1,2,3,4
        rNuevo = int.Parse(sp.recuperaValor("select count(1)  from tProductBackLog where idEstatus = 1"));
        rRefinado = int.Parse(sp.recuperaValor("select count(1)  from tProductBackLog where idEstatus = 2"));
        rPuntuando = int.Parse(sp.recuperaValor("select count(1)  from tProductBackLog where idEstatus = 3"));
        rSprint = int.Parse(sp.recuperaValor("select count(1)  from tProductBackLog where idEstatus = 4"));
        //si la llamada al metodo devuelve mayor a 0 se pinta el div.
        if (rNuevo > 0)
        {
            res += "<a href='javascript:;' onclick='javascript:getTablaHistorias(1);' class='divBlanco pdtes'><label id='icon-25' class='configuracion verde tipo-ticket'>HISTORIAS NUEVAS <label class='txt-azul qty-tickets'>" + rNuevo + "</label></label></a>";
            
        }

        if (rRefinado > 0)
        {
            res += "<a href='javascript:;' onclick='javascript:getTablaHistorias(2);' class='divBlanco pdtes'><label id='icon-25' class='asig_equipo verde tipo-ticket'>HISTORIAS REFINADAS <label class='txt-azul qty-tickets'>" + rRefinado + "</label></label></a>";
            
        }

        if (rPuntuando > 0)
        {
            res += "<a href='javascript:;' onclick='javascript:getTablaHistorias(3);' class='divBlanco pdtes' id='rPuntuado'><label id='icon-25' class='validar verde tipo-ticket'>HISTORIAS PUNTUADAS <label class='txt-azul qty-tickets'>" + rPuntuando + "</label></label></a>";
           
        }

        if (rSprint > 0)
        {
            res += "<a href='javascript:;' onclick='javascript:getTablaHistorias(4);' class='divBlanco pdtes' id='rSprint'><label id='icon-25' class='validar verde tipo-ticket'>HISTORIAS EN SPRINT <label class='txt-azul qty-tickets'>" + rSprint + "</label></label></a>";
            
        }

        res += "<div class='clear'></div><br />";


        resultado.Add(res);

        return resultado;
    }

}