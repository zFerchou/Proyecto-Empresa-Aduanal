using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Configuracion_Sprint_Sprint : System.Web.UI.Page
{
    private static ControllerActividades controller;
    private static int idUsuario;
    private Utileria util;
    private ERPManagementDataContext erp;
    private ERPManagementRHDataContext erpRH;
    string sprint;
    string estatus;
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.generaHeader("../../");
        idUsuario = int.Parse(Session["id"].ToString());

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
        }
        else if (validacionUsu.idTipoUsuario == 2)
        {
            txtTipoUsuario.Text = validacionUsu.idTipoUsuario.ToString();
        }
        else if (validacionUsu.idTipoUsuario == 1)
        {
            txtTipoUsuario.Text = validacionUsu.idTipoUsuario.ToString();
        }

        //Validar el permiso de usuario     
        erp = new ERPManagementDataContext();
        var validacion = (from vUser in erp.view_ValidarUsuario
                          where vUser.idUsuario == idUsuario && vUser.idRolERPM == 1
                          select vUser).Count();
        if (validacion <= 0)
        {
            Response.Redirect("../../inicio.aspx");
        }
        //comboSprint();
        //comboEstatus();
        //lblSprint.Text = sprint;
        //lblEstatus.Text = estatus;
    }

    [WebMethod]
    public static string GetSprint(string sprint)
    {
        controller = new ControllerActividades();
        Utileria util = new Utileria();
        
        return controller.GetSprint(sprint);
    }

    [WebMethod]
    public static string GetTablaSprint()
    {
        controller = new ControllerActividades();
        Utileria util = new Utileria();

        return controller.GetTablaSprint();
    }

    [WebMethod]
    public static List<string> GetSprintByID(string idUsuario)
    {
        controller = new ControllerActividades();
        return controller.lstSprint();
    }

    [WebMethod]
    public static List<string> GetSprintBySprint(string sprint)
    {
        controller = new ControllerActividades();
        return controller.lstSprintByID(sprint);
    }

    #region comboSprint
    public string comboSprint()
    {
        string queryM = "select Sprint,'Sprint '+convert(varchar,Sprint)  from vReporteSprint";
        DataSet dtSprint = new DataSet();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        dtSprint = sp.getValues(queryM);
        sprint += "<select class=\"validaCombo select inputP25\"  name=\"sp\" id=\"sp\">";
        sprint += "<option value= 0 >Elige un sprint</option>";
        foreach (DataRow row in dtSprint.Tables[0].Rows)
        {

            sprint += "<option value=" + row[0].ToString() + ">" + row[1].ToString() + "</option>";
        }
        sprint += "</select>";

        return sprint;

    }
    #endregion
    #region comboEstatus
    public string comboEstatus()
    {
        string queryM = "select * from cEstatusSprint";
        DataSet dtSprint = new DataSet();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        dtSprint = sp.getValues(queryM);
        estatus += "<select class=\"validaCombo select inputP25\"  name=\"es\" id=\"es\">";
        estatus += "<option value= 0 >Estatus Sprint</option>";
        foreach (DataRow row in dtSprint.Tables[0].Rows)
        {

            estatus += "<option value=" + row[1].ToString() + ">" + row[1].ToString() + "</option>";
        }
        estatus += "</select>";

        return estatus;

    }
    #endregion
}