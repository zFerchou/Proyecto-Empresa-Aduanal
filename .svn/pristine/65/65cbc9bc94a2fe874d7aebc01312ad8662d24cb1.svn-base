using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Configuracion_Tablero_Tablero : System.Web.UI.Page
{
    private static int idUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.generaHeader("../../");
        Configuracion_Tablero_Tablero.limpiarVariables();
        //idUsuario = int.Parse(Session["id"].ToString());
    }

    #region ABRIR TABLERO
    [WebMethod]
    public static string abrirTablero()
    {
        Configuracion_Tablero_Tablero.limpiarVariables();
        string sRespuesta;
        Tablero oUtil = new Tablero();
        sRespuesta = oUtil.abrirTablero(idUsuario);
        return sRespuesta;
    }
    #endregion

    [WebMethod(enableSession: true)]
    public static void limpiarVariables()
    {
        idUsuario = 0;

        idUsuario = (string.IsNullOrEmpty(HttpContext.Current.Session["id"].ToString()) ? 0 : int.Parse(HttpContext.Current.Session["id"].ToString()));
    }

}