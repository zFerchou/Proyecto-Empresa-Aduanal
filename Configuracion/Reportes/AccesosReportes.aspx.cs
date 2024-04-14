using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;

public partial class Configuracion_Reportes_AccesosReportes : System.Web.UI.Page
{
    private static int idUsuario;
    private ERPManagementDataContext erp;
    protected void Page_Load(object sender, EventArgs e)
    {
        initComponents();
        //Validar el permiso de usuario   
        idUsuario = int.Parse(Session["id"].ToString());
        erp = new ERPManagementDataContext();
        var validacion = (from vUser in erp.view_ValidarUsuario
                          where vUser.idUsuario == idUsuario && vUser.idRolERPM == 6
                          select vUser).Count();
        if (validacion <= 0)
        {
            Response.Redirect("../../inicio.aspx");
        }
    }

    /// <summary>
    /// Método para inicializar los componentes
    /// </summary>
    public void initComponents()
    {
        Master.generaHeader("../../");
        getDdlGrupo();
        getDdlSistemas();
    }

    /// <summary>
    /// Método para llenar ddl de Grupos
    /// </summary>
    public void getDdlGrupo()
    {
        ControllerERPGrupo controllerERPGrupo = new ControllerERPGrupo();
        List<tERPGrupo> lstERPGrupo = controllerERPGrupo.getERPGrupo();

        ddlGrupo.Items.Add(new ListItem("Todos", "0"));

        foreach (tERPGrupo terp in lstERPGrupo)
        {
            ListItem lstGrupo = new ListItem(terp.nomGrupo, terp.idERPGrupo.ToString());
            ddlGrupo.Items.Add(lstGrupo);
        }
    }

    /// <summary>
    /// Método para llenar ddl de Sistemas
    /// </summary>
    public void getDdlSistemas()
    {
        ControllerSistema cs = new ControllerSistema();
        List<cSistemas> lstSistemas = cs.getSistemas();

        ddlSistema.Items.Add(new ListItem("Todos", "0"));

        foreach (cSistemas sistema in lstSistemas)
        {
            ListItem lstSis = new ListItem(sistema.nomSistema, sistema.idSistema.ToString());
            ddlSistema.Items.Add(lstSis);
        }
    }

    /// <summary>
    /// Método que invoca la generación de la tabla
    /// con los ingresos a los sistemas, según los
    /// filtros especificados.
    /// </summary>
    /// <param name="grupo"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <param name="sistema"></param>
    /// <returns>Código html de tablag generada</returns>
    [WebMethod]
    public static string obtenerReporte(int grupo, string fechaInicio, string fechaFin, int sistema)
    {
        ControllerAccesoReportes car = new ControllerAccesoReportes();
        return car.obtenerReporte(grupo,fechaInicio,fechaFin,sistema);
    }

    /// <summary>
    /// Metodo que invoca la obtención
    /// de la siguiente serie de la gráfica
    /// </summary>
    /// <param name="grupo"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <param name="sistema"></param>
    /// <param name="name"></param>
    /// <returns>String con el json</returns>
    [WebMethod]
    public static string obtenerSerie(int grupo, string fechaInicio, string fechaFin, int sistema, string name)
    {
        ControllerAccesoReportes oCar = new ControllerAccesoReportes();
        return oCar.obtenerSerie(grupo, fechaInicio, fechaFin, sistema, name);
    }

    [WebMethod]
    public static string generarPDF(string grupo, string fechaInicio, string fechaFin, string sistema)
    {
        ControllerAccesoReportes oCAR = new ControllerAccesoReportes();
        return oCAR.generarPDF(HttpContext.Current, grupo, fechaInicio, fechaFin, sistema);
    }

    /// <summary>
    /// Método para gnerar Imagenes y guardarla en el servidor
    /// </summary>
    /// <param name="sNombreGrafica">Nombre de la Imagen</param>
    /// <param name="sBase">Imagen en formato Base64</param>
    /// <returns>Verdadero o Falso en caso de Error</returns>
    [WebMethod]
    public static bool GenerarImagen(string sNombreGrafica, string sBase)
    {
        try
        {
            string sBase64 = sBase;
            byte[] bytes = Convert.FromBase64String(sBase64.Split(',')[1]);
            using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("ImgGraficas/" + sNombreGrafica + ".png"), FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(sBase64.Split(',')[1]);
                    bw.Write(data);
                    bw.Close();
                }
            }
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    /// <summary>
    /// Método para gnerar Imagenes (dos) y guardarla en el servidor
    /// </summary>
    /// <param name="sNombreGrafica">Nombre de la Imagen</param>
    /// <param name="sBase">Imagen en formato Base64</param>
    /// <returns>Verdadero o Falso en caso de Error</returns>
    [WebMethod]
    public static bool GenerarImagenes(string sNombreGrafica, string sBase, string sNombreSegGrafica, string sBaseSeg)
    {
        try
        {
            string sBase64 = sBase;
            byte[] bytes = Convert.FromBase64String(sBase64.Split(',')[1]);
            using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("ImgGraficas/" + sNombreGrafica + ".jpeg"), FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(sBase64.Split(',')[1]);
                    bw.Write(data);
                    bw.Close();
                }
            }
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
        try
        {
            string sBase64 = sBaseSeg;
            byte[] bytes = Convert.FromBase64String(sBase64.Split(',')[1]);
            using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("ImgGraficas/" + sNombreSegGrafica + ".jpeg"), FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(sBase64.Split(',')[1]);
                    bw.Write(data);
                    bw.Close();
                }
            }
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }
}

