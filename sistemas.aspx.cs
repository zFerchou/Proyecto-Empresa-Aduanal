using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;


public partial class sistemas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e){

        //Si el usuario no se ha logueado no podrá entrar a esta página
        if (!(Boolean)Session["logged"])
        {
            Response.Redirect("index.aspx");
        }

         lblUsuario.Enabled = true;

         lblUsuario.Text ="Consultor: "+ Session["username"].ToString();

         cargarModulos();
    }

    protected void linkCerrarSesion_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("index.aspx");
    }

    public void cargarModulos()
    {
        storedProcedure sp = new storedProcedure();

        List<String> lstModulos = new List<string>();
        string qm = "select idSistema, nomSistema, nomenglatura, imagen From cSistemas Where idSistema <= 4";
        lstModulos = sp.recuperaRegistros(qm);
        int contador = 1;
        string mods = "";

        if (lstModulos.Count > 0)
        {
            for (int i = 0; i < lstModulos.Count; i += 4)
            {
                mods += "<div class='g-cont' id='modulo"+contador+"'>" +
                            "<div>" +
                                "" + lstModulos[i + 3] + "" +
                                "<p class='clear'><a href='#'>" + lstModulos[i + 1] + "</a></p>" +
                                "<input type='checkbox' id='check" + lstModulos[i] + "' class='check' value='" + lstModulos[i] + "' /><label id='lblCheck' for='check" + lstModulos[i] + "'>Selección " + lstModulos[i + 2] + "</label>" +
                            "</div>" +
                        "</div>";
                contador++;
            }
        }
        else
        {
            mods = "No hay registros";
        }
        lblModulos.Text = mods;

    }

    // Ejecutar método para bajar aplicaciónes.
    public static void executeAppDownloadRepository() {
        // Ruta donde se encuentre el .exe de la app.
        string urlexe = "C:/inetpub/wwwroot/lca-consultores.mx/ERPManagement/appDownloadRepository/AppDownloadRepository.exe";
        //Ejecuta un CMD y recibe la ruta a ejecutar para hacer la descarga
        System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + urlexe);
        procStartInfo.RedirectStandardOutput = true;
        procStartInfo.UseShellExecute = false;
        //Mostrar o no ventana CMD
        procStartInfo.CreateNoWindow = true;
        //Esconder la ventana
        procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo = procStartInfo;
        proc.Start();        
    }

    //metodo que llena el autocomplete al ingresar
    [WebMethod]
    public static List<AutoComplete> sistemaGrupo(string term)
    {
        List<AutoComplete> resultado = new List<AutoComplete>();
        List<string> obtener = new List<string>();
        AutoComplete ac;
        string query = "";
        term = term.ToLower();
        storedProcedure sp = new storedProcedure();
        query = "SELECT TOP (15) idERPGrupo, nomGrupo FROM tERPGrupo WHERE nomGrupo LIKE '%" + term + "%'";
        obtener = sp.recuperaRegistros(query);

        if (obtener != null && obtener.Count > 0)
        {
            for (int i = 0; i < obtener.Count; i += 2)
            {
                ac = new AutoComplete();
                ac.ID = obtener[i];
                ac.nombre = obtener[i + 1];
                resultado.Add(ac);
            }
        }
        else
        {
            resultado.Add(new AutoComplete { ID = "", nombre = "No se encontraron resultados" });
        }
        return resultado;
    }
    
    [WebMethod]
    public static List<string> deshabilitarCampos(string id)
    {
        List<string> obtener = new List<string>();
        string query = "";
        storedProcedure sp = new storedProcedure();
        query = "SELECT tgs.idSistema "+
                "FROM tERPGrupo tg INNER JOIN tERPGrupoSistema tgs ON tg.idERPGrupo = tgs.idERPGrupo WHERE nomGrupo ='" + id + "' AND tgs.idSistema <= 4";
        obtener = sp.recuperaRegistros(query);
        return obtener;
    }
    

}