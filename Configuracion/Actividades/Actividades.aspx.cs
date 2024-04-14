using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Configuracion_Actividades_Actividades : System.Web.UI.Page
{
    private static ControllerActividades controller;
    private static int idUsuario;
    private Utileria util;
    private ERPManagementDataContext erp;
    private ERPManagementRHDataContext erpRH;
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
    }

    [WebMethod]
    public static string GetActividades(string idUsuario)
    {
        controller = new ControllerActividades();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return controller.GetActividades(idUser);
    }
}