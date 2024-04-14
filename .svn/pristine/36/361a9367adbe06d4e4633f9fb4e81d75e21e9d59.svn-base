using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

public partial class Configuracion_Implementador_sistema : System.Web.UI.Page
{
    private SqlConnection conn;
    private static ControllerReportes controller;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Master.generaHeader("../../");
        }
    }

    /* 
     * Obtener el numero de las historias en general para pasar a los labels
     */
    [WebMethod]
    public static string ObtenerNumeroHistoriasAgendadas()
    {
        int numeroHistorias = 0;
        string connectionString = ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tHistoriasALiberar WHERE idEstatus = 4", conn);
            numeroHistorias = (int)cmd.ExecuteScalar();
            conn.Close();
        }
        return numeroHistorias.ToString(); // Convertir el número a una cadena antes de devolverlo
    }

    [WebMethod]
    public static string ObtenerNumeroHistoriasNoAgendadas()
    {
        int numeroHistorias = 0;
        string connectionString = ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tHistoriasALiberar WHERE idEstatus = 5", conn);
            numeroHistorias = (int)cmd.ExecuteScalar();
            conn.Close();
        }
        return numeroHistorias.ToString(); // Convertir el número a una cadena antes de devolverlo
    }

    [WebMethod]
    public static string ObtenerNumeroHistoriasLiberadas()
    {
        int numeroHistorias = 0;
        string connectionString = ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tHistoriasALiberar WHERE idEstatus = 6", conn);
            numeroHistorias = (int)cmd.ExecuteScalar();
            conn.Close();
        }
        return numeroHistorias.ToString(); // Convertir el número a una cadena antes de devolverlo
    }

    /* Consulta para obtener el nombre de los sistemas y el numero 
     * de los sistemas que contienen historias (Falta manejar el numero)
     */
    [WebMethod]
    public static string ObtenerNombresHistoriasAgendadas()
    {
        List<string> nombresSistemas = new List<string>();
        string connectionString = ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT s.nomSistema, COUNT(*) AS cantidad_de_historias\r\nFROM cSistemas AS s\r\nINNER JOIN tHistoriasALiberar AS h\r\nON s.idSistema = h.idSistema\r\ninner join tEstatusHistoriaTerminada as e\r\nOn h.idEstatus=e.idEstatus\r\nWhere h.idEstatus=4\r\nGROUP BY s.nomSistema", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                nombresSistemas.Add(reader["nomSistema"].ToString());
            }
            conn.Close();
        }

        // Convertir la lista de nombres de sistemas a un objeto JSON
        return new JavaScriptSerializer().Serialize(nombresSistemas);
    }

    [WebMethod]
    public static string ObtenerNombresHistoriasNoAgendadas()
    {
        List<string> nombresSistemas = new List<string>();
        string connectionString = ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT s.nomSistema, COUNT(*) AS cantidad_de_historias\r\nFROM cSistemas AS s\r\nINNER JOIN tHistoriasALiberar AS h\r\nON s.idSistema = h.idSistema\r\ninner join tEstatusHistoriaTerminada as e\r\nOn h.idEstatus=e.idEstatus\r\nWhere h.idEstatus=5\r\nGROUP BY s.nomSistema", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                nombresSistemas.Add(reader["nomSistema"].ToString());
            }
            conn.Close();
        }

        // Convertir la lista de nombres de sistemas a un objeto JSON
        return new JavaScriptSerializer().Serialize(nombresSistemas);
    }

    [WebMethod]
    public static string ObtenerNombresHistoriasLiberadas()
    {
        List<string> nombresSistemas = new List<string>();
        string connectionString = ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT s.nomSistema, COUNT(*) AS cantidad_de_historias\r\nFROM cSistemas AS s\r\nINNER JOIN tHistoriasALiberar AS h\r\nON s.idSistema = h.idSistema\r\ninner join tEstatusHistoriaTerminada as e\r\nOn h.idEstatus=e.idEstatus\r\nWhere h.idEstatus=6\r\nGROUP BY s.nomSistema", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                nombresSistemas.Add(reader["nomSistema"].ToString());
            }
            conn.Close();
        }

        // Convertir la lista de nombres de sistemas a un objeto JSON
        return new JavaScriptSerializer().Serialize(nombresSistemas);
    }

    /* 
     * Consultas de manera dinamica para el llenado de las tablas
     */
    [WebMethod]
    public static string ObtenerHistoriasAgendadas(string sistema)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT H.idHistoriasALiberar, r.folio, r.descripcion, g.nomGrupo, h.puntosDeHistoria, h.fechaPropuestaOwner " +
                           "FROM tERPGrupo AS g " +
                           "INNER JOIN tReporte AS R ON R.idERPGrupo = g.idERPGrupo " +
                           "INNER JOIN tHistoriasALiberar AS H ON R.idReporte = H.idReporte " +
                           "INNER JOIN cSistemas AS S ON H.idSistema = S.idSistema " +
                           "WHERE h.idEstatus = 4 AND s.nomSistema = @Sistema";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Sistema", sistema);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            // Convertir el DataTable a una lista de objetos JSON
            var jsonData = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col.ColumnName]; // Acceder al valor por nombre de columna
                }
                jsonData.Add(dict);
            }
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(jsonData);
        }
    }

    [WebMethod]
    public static string ObtenerHistoriasNoAgendadas(string sistema)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT H.idHistoriasALiberar, r.folio, r.descripcion, g.nomGrupo, h.puntosDeHistoria, h.fechaPropuestaOwner " +
                           "FROM tERPGrupo AS g " +
                           "INNER JOIN tReporte AS R ON R.idERPGrupo = g.idERPGrupo " +
                           "INNER JOIN tHistoriasALiberar AS H ON R.idReporte = H.idReporte " +
                           "INNER JOIN cSistemas AS S ON H.idSistema = S.idSistema " +
                           "WHERE h.idEstatus = 5 AND s.nomSistema = @Sistema";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Sistema", sistema);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            // Convertir el DataTable a una lista de objetos JSON
            var jsonData = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col.ColumnName]; // Acceder al valor por nombre de columna
                }
                jsonData.Add(dict);
            }
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(jsonData);
        }
    }

    [WebMethod]
    public static string ObtenerHistoriasLibaradas(string sistema)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT r.folio, r.descripcion, g.nomGrupo, h.puntosDeHistoria, h.fechaPropuestaOwner, h.fechaPropuestaImplement " +
                           "FROM tERPGrupo AS g " +
                           "INNER JOIN tReporte AS R ON R.idERPGrupo = g.idERPGrupo " +
                           "INNER JOIN tHistoriasALiberar AS H ON R.idReporte = H.idReporte " +
                           "INNER JOIN cSistemas AS S ON H.idSistema = S.idSistema " +
                           "WHERE h.idEstatus = 6 AND s.nomSistema = @Sistema";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Sistema", sistema);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            // Convertir el DataTable a una lista de objetos JSON
            var jsonData = new List<Dictionary<string, object>>();
            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col.ColumnName]; // Acceder al valor por nombre de columna
                }
                jsonData.Add(dict);
            }
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(jsonData);
        }
    }

    [WebMethod]
    public static bool asignarHistoria(string newFecha, string historias, int idEstatus)
    {
        controller = new ControllerReportes();

        return controller.AsignarHistoria(newFecha, historias, idEstatus);
    }

    [WebMethod]
    public static bool liberarHistoria(string newFecha, int idEstatus, string historias)
    {
        controller = new ControllerReportes();
        return controller.LiberarHistoria(newFecha, historias, idEstatus);
    }
}