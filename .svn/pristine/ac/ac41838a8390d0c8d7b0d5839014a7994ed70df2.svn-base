using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;


public class AutoCompleteReportes
{
    public int IdReporte { get; set; }
    public int IdSistema { get; set; }
    public string Asunto { get; set; }
    public int IdERPGrupo { get; set; }
}
public partial class Configuracion_HistoriasTerminadas_HistoriasTerminadas : System.Web.UI.Page
{
    private static int idUsuario;
    private ERPManagementDataContext erp;
    private static Utileria utileria;
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            Master.generaHeader("../../");
        }
        utileria = new Utileria();
        idUsuario = int.Parse(Session["id"].ToString());
    }

    [WebMethod]
    public static List<string> obtenerHistoriasTerminadas()
    {
        List<AutoCompleteReportes> resultado = new List<AutoCompleteReportes>();
        
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");

        // por el momento usamos id's 
        string query = "select tr.idReporte, tr.folio ,tr.idSistema, cs.nomSistema ,tr.asunto,te.nomGrupo "
            + "from tReporte as tr "
            + "inner join cSistemas as cs On tr.idSistema=cs.idSistema "
            + "inner join tERPGrupo as te On tr.idERPGrupo=te.idERPGrupo "
            + "LEFT JOIN tHistoriasALiberar AS tl ON tl.idReporte = tr.idReporte "
            + "where tr.idEstatusReporte = 2 AND tl.idReporte IS NULL";

        List<string> obtener = sp.recuperaRegistros(query);

        return obtener;
    }


    [WebMethod]
    public static string InsertarHistoriasOwner(string data)
    {
        //code trycatch
        //Obtener la cadena de conexión desde el archivo de configuración
        string connectionString = ConfigurationManager.ConnectionStrings["DBSGICEConnectionString"].ConnectionString;

        try
        {
            List<Reporte> reportes = JsonConvert.DeserializeObject<List<Reporte>>(data);
            Reporte repo = new Reporte();
            foreach (var reporte in reportes)
            {
                // Aquí puedes acceder a cada objeto 'Reporte' en la lista 'reportes'
                // Por ejemplo:
                int idSistema = reporte.idSistema;
                int idReporte = reporte.idReporte;
                string dateOwner = reporte.dateOwner;
                repo.idReporte = idReporte;
                repo.idSistema = idSistema;
                repo.dateOwner = dateOwner;

                // Realiza las operaciones que necesites con estos datos, como guardarlos en la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("pa_InsertarHistoriasALiberar", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Definir los parámetros del procedimiento almacenado
                command.Parameters.AddWithValue("@idReporte", repo.idReporte);
                command.Parameters.AddWithValue("@idSistema", repo.idSistema);
                command.Parameters.AddWithValue("@fechaPropuestaOwner", repo.dateOwner);
                command.Parameters.AddWithValue("@fechaPropuestaImplement",new DateTime(2000,01,01));
                command.Parameters.AddWithValue("@fechaImplementacionHistoria", repo.dateOwner);
                command.Parameters.AddWithValue("@idEstatus", 1);
                command.Parameters.AddWithValue("@puntosDeHistoria", 0);
                command.Parameters.AddWithValue("@idUsuario", idUsuario);

                // Ejecutar el procedimiento almacenado
                command.ExecuteNonQuery();
            }
            }
            return "Objeto JSON recibido y procesado exitosamente "+repo.idReporte;

        }catch(Exception ex)
        {
            return "Ningun Objeto JSON recibido y : " + ex.Message;
        }
        // Procesar JSON
        //dynamic objeto = JsonConvert.DeserializeObject(reporte);
        // Hacer algo con el objeto JSON, como guardarlo en la base de datos

        // Retornar una respuesta

    }
       
}

public class Reporte
{
    public int idSistema { get; set; }
    public int idReporte { get; set; }
    public string dateOwner { get; set; }
}