using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void generaHeader(string prefixUrl)
    {
        if (!(Boolean)Session["logged"])
        {
            Response.Redirect("~/index.aspx");
        }

            string librerias =  "<link href='" + prefixUrl + "css/Sistema/styles.css' rel='stylesheet' type='text/css' />" +
                                "<link href='" + prefixUrl + "css/Sistema/jquery-ui.css' rel='stylesheet' type='text/css' />" +
                                "<link href='" + prefixUrl + "css/Sistema/dataTables.css' rel='stylesheet' type='text/css' />"+
                                "<link href='" + prefixUrl + "css/Sistema/alertify.core.css' rel='stylesheet' type='text/css' />" +
                                "<link href='" + prefixUrl + "css/Sistema/alertify.default.css' rel='stylesheet' type='text/css' />" +
                                "<script src='" + prefixUrl + "js/Sistema/jquery-1.11.1.min.js' type='text/javascript'></script>" +
                                "<script src='" + prefixUrl + "js/Sistema/jquery-ui.js' type='text/javascript'></script>"+
                                "<script src='" + prefixUrl + "js/Sistema/jquery.dataTables.js' type='text/javascript'></script>"+
                                "<script src='" + prefixUrl + "js/Sistema/jquery.blockUI.js' type='text/javascript'></script>" +
                                "<script src='" + prefixUrl + "js/Sistema/screenBlock.js' type='text/javascript'></script>"+
                                "<script src='" + prefixUrl + "js/Sistema/alertify.min.js' type='text/javascript'></script>"+
                                "<script src='" + prefixUrl + "js/site.js' type='text/javascript'></script>";
            ltHead.Text = librerias;

            lblImgLogo.Text = "<img src='" + prefixUrl + "images/logos/LogoNAD.png' alt='ERPManagement' class='logo-sistema' id='logo-sistema'>";
            generarMenu(prefixUrl);
            generarFoot(prefixUrl);
        
    }

    public void generarMenu(string prefixUrl)
    {
        ERPManagementDataContext erp = new ERPManagementDataContext();
        lblUsuario.Text = Session["username"].ToString();
        int idUsuario = int.Parse(Session["id"].ToString());
        //int idUsuario = 1;
        string menu = "";
        string manuales = "";
        bool administrador = false;
        bool consultor = false;
        bool cliente = false;

        var lstRoles = (from roles in erp.tRolesERPM
                        join rolesUsuario in erp.tRolUsuarioERPM
                          on roles.idRolERPM equals rolesUsuario.idRolERPM
                       where rolesUsuario.idUsuario == idUsuario
                            select roles).ToList();

        menu = "<div class='nav-container'>"+
                                "<ul id='nav'>"+
                                    "<li class='first'><a href='"+prefixUrl+"Inicio.aspx'>Inicio</a></li>";
        menu += "<li><a href='" + prefixUrl + "Configuracion/HistoriasTerminadas/HistoriasTerminadas.aspx" + "'>" + "HistoriasTerminadas" + "</a></li>";

        foreach (tRolesERPM tr in lstRoles)
        {
            if(tr.isMenu == 1){

                if (tr.idRolERPM != 5 && tr.idRolERPM != 7)
                {
                    menu += "<li><a href='" + prefixUrl + tr.pagina + "'>" + tr.descripcion + "</a></li>";
                }
                else
                {
                    if (!cliente)
                    {
                        if (tr.idRolERPM == 7)
                        {
                            menu += "<li><a href='" + prefixUrl + tr.pagina + "'>Clientes</a></li>";
                        }
                        else
                        {
                            menu += "<li><a href='" + prefixUrl + tr.pagina + "'>" + tr.descripcion + "</a></li>";
                        }
                        cliente = true;
                    }
                }
                
            }

            //Generación de submenús de manuales
            if(tr.idRolERPM == 1)
            {
                manuales += "<li><a target='_blank' href='" + prefixUrl + "/Configuracion/Manuales/Manual Usuario Generar Incidencias.pdf'><span id='icon-25' class='pdf'>Generador Incidencias</span></a></li>";
            }

            if(tr.idRolERPM == 3)
            {
                manuales += "<li><a target='_blank' href='" + prefixUrl + "/Configuracion/Manuales/Manual Usuario Soporte Incidencias.pdf'><span id='icon-25' class='pdf'>Soporte Incidencias</span></a></li>";
            }

            if(tr.idRolERPM == 4 || tr.idRolERPM == 5 && !administrador)
            {
                manuales += "<li><a target='_blank' href='" + prefixUrl + "/Configuracion/Manuales/Manual Uso Clientes-Permisos.pdf'><span id='icon-25' class='pdf'>Administrador</span></a></li>";
                administrador = true;
            }

            if(tr.idRolERPM == 2 || tr.idRolERPM == 6 && !consultor)
            {
                manuales += "<li><a target='_blank' href='" + prefixUrl + "/Configuracion/Manuales/Manual de uso Consultar-Accesos.pdf'><span id='icon-25' class='pdf'>Consultor</span></a></li>";
                consultor = true;
            }
                
        }

        menu += "<li class='last'><a>Manuales</a>"+
                    "<ul class='dropdown'>"+
                        manuales+
                    "</ul>"+
                 "</li>";

        menu += "</ul>" +
       "</div>";

        lblMenu.Text = menu;
    }

    public void generarFoot(string prefixUrl)
    {
        string foot = "<div id='footer-layer'>" +
                        "<ul>" +
                            "<li class='rights'>" +
                                "<p>" +
                                    "NAD Global 2013. ©Todos los derechos reservados.. </li>" +
                            "<li class='logos'>" +
                                "<img src='"+prefixUrl+"images/foot/img-logonad.png' alt='logo-nad'></li>" +
                        "</ul>" +
                    "</div>";

        lblFooter.Text = foot;
    }

    protected void linkCerrarSesion_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/index.aspx");
    }
}
