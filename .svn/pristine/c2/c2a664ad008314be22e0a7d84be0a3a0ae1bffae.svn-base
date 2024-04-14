using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Configuracion_Reportes_CuotasSistemas : System.Web.UI.Page
{
    private static int idReporte;
    private static ERPManagementDataContext erp;
    private static int idUsuario;
    private static string sistemas;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.generaHeader("../../");
        idUsuario = int.Parse(Session["id"].ToString());
        Utileria utileria = new Utileria();
        idReporte = int.Parse(utileria.DecodeFrom64(Request.QueryString["id"]));
        txtidReporte.Text = idReporte.ToString();
        txtidUsuario.Text = utileria.EncodeTo64(Session["id"].ToString());
        //utileria.DecodeFrom64(idReporte.ToString());

        if (verificarReporte(idReporte, idUsuario))
        {
            lblinfIncidencia.Text = obtenerDetalle(idReporte);
            lblGrupo.Text = obtenerNombreGrupo();
            lblSistemas.Text = obtenerSistemasSolicitados(idReporte);
        }
        else
        {
            Response.Redirect("../../inicio.aspx");
        }


    }

    /// <summary>
    /// Metodo para verificar el usuario y el estado del reporte
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>bool</returns>
    public static bool verificarReporte(int idReporte, int idUsuario)
    {
        ERPManagementDataContext erp = new ERPManagementDataContext();
        var reporte = (from rpt in erp.tReporte
                       where rpt.idReporte == idReporte
                       select new { rpt.idEstatusReporte, rpt.idUsuario }).Single();

        if ((reporte.idEstatusReporte == 3 || reporte.idEstatusReporte == 4) && reporte.idUsuario == idUsuario)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Metodo para obtener los sistemas solicitados.
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>string</returns>
    public static string obtenerSistemasSolicitados(int idReporte)
    {
        ERPManagementDataContext erp = new ERPManagementDataContext();
        ControllerGenerarSistemas generarSistemas = new ControllerGenerarSistemas();
        var query = (from reporte in erp.view_PeticionSistemas
                     where reporte.idReporte == idReporte
                     select reporte).ToList();


        var moneda = (from mon in erp.cMonedaCuota
                      select mon).ToList();

        string res = "<div class='width90 center clear'>";
        int contador = 0;
        foreach (var objeto in query)
        {
            string cboMoneda = "<select id='cbo" + objeto.idSistema + "' class='validarcbo" + objeto.idSistema + " select'><option value'0' disabled selected>Elija una opción</option>";

            foreach (cMonedaCuota cmc in moneda)
            {
                cboMoneda += "<option value=" + cmc.idMoneda + ">" + cmc.nombreMoneda + "</option>";
            }
            cboMoneda += "</select><br>";

            sistemas += objeto.idSistema;
            if (query.Count > 2)
            {
                contador++;

                res += "<div class='left width33'><fieldset class='field3'><div class='card'>" + "" + objeto.imagen + "" + "<p class='h1-card' style='text-align: center;'>" + objeto.nomSistema + "</p><div  id='sist" + objeto.idSistema + "'  class=''></div></div>" +
                    "<div style='background-color:#ffffff;' class='frm borde'><div class='respon'></div><br />" +
                    "<label class='lblResponsable'>Agregar Responsables:</label>" +
                "<div id='autocomplete'  style='padding-top:10px;'><input id='autoC" + objeto.idSistema + "' class='autocomplete transparente validarAuto" + objeto.idSistema + "' name='aut' /></div>" +
                "<br>" +
                    "<label>Cuota:</label><br />" +
                    "<a>$ </a><input type='text' minlength='2' maxlength='8' placeholder='Cuota' class='validarCuota" + objeto.idSistema + "' id='txtCuota" + objeto.idSistema + "' />" + cboMoneda + "" +
                "<br />" +
                    "<label>Fecha Inicio:</label><label style='padding-left:22%;'>Fecha Vencimiento:</label><br />" +
                    "<input type='text' placeholder='Fecha Inicio' readonly='readonly'  class='validar" + objeto.idSistema + " input100' id='txtFechaInicio" + objeto.idSistema + "' /><span id='icon-25' class='calendario'><a style='padding-left:7%;'>&nbsp</a><input type='text' readonly='readonly' class='validar" + objeto.idSistema + " input100' placeholder='Fecha Vencimiento' id='txtFechaFin" + objeto.idSistema + "' /><span id='icon-25' class='calendario'></span><br />" +
                "</div></fieldset></div>";

                if (contador == 3)
                {
                    res += "<div class='clear'><br /></div>";
                }

            }
            else
            {
                res += "<div class='left width50'><fieldset class='field2'><div class='card'>" + "" + objeto.imagen + "" +
                                "<p class='h1-card' style='text-align: center;'>" + objeto.nomSistema + "</p><div id='sist" + objeto.idSistema + "' class=''></div></div>" +
                    "<div style='background-color:#ffffff;' class='frm borde'><br />" +
                    "<label class='lblResponsable'>Agregar Responsables:</label>" +
                    "<div id='autocomplete'  style='padding-top:10px;'><input id='autoC" + objeto.idSistema + "' class='autocomplete transparente " +
                                             "validarAuto" + objeto.idSistema + "' name='aut' /></div>" +
                "<br>" +
                    "<label  for='txtCuota" + objeto.idSistema + "' style='padding-botom:22%;'>Cuota:</label><br><br>" +
                    "<a>$ </a><input minlength='2' maxlength='8' type='text' class='validarCuota" + objeto.idSistema + "' placeholder='Cuota' id='txtCuota" + objeto.idSistema + "' />" + cboMoneda + "" +
                "<br />" +
                    "<label>Fecha Inicio:</label><label style='padding-left:22%;'>Fecha Vencimiento:</label><br />" +
                            "<input type='text' readonly='readonly' class='validar" + objeto.idSistema + " input120' placeholder='Fecha Inicio'" +
                "id='txtFechaInicio" + objeto.idSistema + "' /><span id='icon-25' class='calendario'><a>&nbsp<a style='padding-left:6%'>&nbsp</a><input type='text' " +
                            "readonly='readonly' class='validar" + objeto.idSistema + "' input120' placeholder='Fecha Vencimiento' id='txtFechaFin" + objeto.idSistema + "' />" +
                            "<span id='icon-25' class='calendario'></span><br />" +
                "</div></fieldset></div>";
            }
        }
        return res + "</div><div class='width20 right'><br /><a class='btn verde shadow2 borde-blanco flotante' onclick='javascript:validar();'><span id='icon-25' class='guardar blanco'>Guardar y Validar</span></a><a class='btn blanco shadow2 borde-blanco flotanteRechazar' onclick='javascript:rechazarPeticion();'><span id='icon-25' class='rechazar'>Rechazar</span>  </a></div>";
    }

    /// <summary>
    /// Metodo para obtener el detalle de la petición
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>string</returns>
    public string obtenerDetalle(int idReporte)
    {

        ControllerRespuestaReporte controllerRespuesta = new ControllerRespuestaReporte();
        view_Reporte vReporte = new view_Reporte();
        vReporte = controllerRespuesta.consultaReporteUsuario(idReporte);
        string resultado = "";
        string asunto_url = "";

        if (vReporte.asunto != "")
        {
            asunto_url = "<br /><br /><div class='clear width50'><label class='txt-verde'>Url ERP: </label><u><a title='Dirección URL Sistema' href='" + vReporte.asunto + "'>" + vReporte.asunto + "</a></u></div>";
        }
        resultado += "<div class='width80 center frm form-layer alto'><div class='form-tit round-border3'><h3 class='left'>Petición ERP</h3></div><br />" +
                    "<div class='width98'><div class='width20 left'><label class='txt-verde'>Folio:&nbsp</label><label>" + vReporte.folio + "</label><input type='hidden' id='txtFolio' value='" + vReporte.folio + "' /></div>" +
                    "<div class='width60 left'><label class='txt-verde'>Descripción:&nbsp</label><label style='text-align: justify;'>" + vReporte.descripcion + "<label></div>" +
                    "<div class='width20 left'><a title='" + vReporte.evidencia + "' href='../Reportes/Evidencia/" + vReporte.evidencia + "' target='_new'><span id='icon-25' class='evidencia azul'>Archivo adjunto</a></span></div><br />" +
                    "" + asunto_url + "<br />" +

            "</div></div>";


        return resultado;
    }

    /// <summary>
    /// Metodo poara obtener los id de los sistemas pertecientes a la incidencia
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<string> obtneridSistemas()
    {
        ERPManagementDataContext erp = new ERPManagementDataContext();
        var query = (from reporte in erp.view_PeticionSistemas
                     where reporte.idReporte == idReporte
                     select reporte.idSistema).ToList();
        List<string> res = new List<string>();
        foreach (var obj in query)
        {
            res.Add(obj.ToString());
        }
        return res;
    }

    /// <summary>
    /// Metodo para obtner los responsables de los sistemas
    /// El personal que cuenta con los roles para ser responsables de los sistemas
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<string> obtenerResponsablesSistema(int idReporte)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        string query = "";
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");

        query = "SELECT idEmpleado,nombreCompleto from vUsuariosERPM " +
            "WHERE TipoUsuario!= 'N/A' and TipoUsuario!='Practicante' and nombreCompleto!='Administrador'";

        obtener = sp.recuperaRegistros(query);
        return obtener;
    }

    /// <summary>
    /// Metodo para insertar la configuración de los sistemas solicitados.
    /// </summary>
    /// <param name="datos"></param>
    /// <returns>bool</returns>
    [WebMethod]
    public static bool insertarConfiguracion(string datos, string comentario, string nombreArchivo, int calificacion, string idUsuario)
    {
        ControllerCuotasSistemas ccs = new ControllerCuotasSistemas();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return ccs.insertarConfiguracion(datos, idReporte, idUser, comentario, nombreArchivo, calificacion);
    }

    public string obtenerNombreGrupo()
    {
        ERPManagementDataContext erp = new ERPManagementDataContext();
        var idERP = (from id in erp.tERPGrupoSistema
                     where id.idReporte == idReporte
                     select id.idERPGrupo).First();

        var nombreGrupo = (from nGrupo in erp.tERPGrupo
                           where nGrupo.idERPGrupo == idERP
                           select nGrupo.nomGrupo).Single();
        string res = "<h2 class='tit'>" + nombreGrupo + "<strong>│</strong> SISTEMAS</h2>";
        return res;
    }

    /// <summary>
    /// Metodo para rechazar Petición de ERP
    /// La petición se actualiza en estatus 4
    /// </summary>
    /// <param name="idReporte"></param>
    /// <param name="comentario"></param>
    /// <param name="nombreArchivo"></param>
    /// <param name="fechaPropuesta"></param>
    /// <returns>bool</returns>
    [WebMethod]
    public static bool rechazarERP(int idReporte, string comentario, string nombreArchivo, string idUsuario)
    {
        ControllerCuotasSistemas ccs = new ControllerCuotasSistemas();
        Utileria util = new Utileria();
        int idUser = int.Parse(util.DecodeFrom64(idUsuario));
        return ccs.rechazarERP(idReporte, comentario, nombreArchivo, idUser);
    }
}