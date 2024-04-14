using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
/// <summary>
/// Descripción breve de storedProcedure
/// </summary>
public class storedProcedure
{
    private string Conexion;
    private SqlConnection conexion;
    SqlCommand comando = null;
    SqlDataReader resultados = null;
    private string conn;
    private SqlDataAdapter objAdapter;

    public storedProcedure(string conn)
    {
        this.conn = conn;
    }

    #region Default
    /// <summary>
    /// Establecer Conexion para luego utilizarla en algun metodo
    /// </summary>
    public void establecerConexion()
    {
        conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[conn].ConnectionString);
    }
    /// <summary>
    /// Ejecutar una sentencia SQL
    /// </summary>
    /// <param name="query">Sentencia sql</param>
    /// <returns>True/false</returns>
    public bool ejecutaSQL(string query)
    {
        establecerConexion();
        comando = new SqlCommand(query, conexion);
        comando.CommandType = CommandType.Text;

        try
        {
            conexion.Open();
            comando.ExecuteReader();
            conexion.Close();

            return true;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            return false;
        }

    }
    /// <summary>
    /// Recupera una lista de varias columnas
    /// </summary>
    /// <param name="query"></param>
    /// <returns>Lista de string</returns>
    public List<string> recuperaRegistros(string query)
    {
        List<string> resultado = new List<string>();
        establecerConexion();
        comando = new SqlCommand(query, conexion);
        comando.CommandType = CommandType.Text;
        comando.CommandTimeout = 12000;
        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            if (resultados.HasRows)
            {
                while (resultados.Read())
                {
                    for (int i = 0; i < resultados.FieldCount; i++)
                    {
                        resultado.Add(resultados.GetValue(i).ToString());
                    }
                }
            }
            conexion.Close();

            return resultado;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            return null;
        }
        finally
        {
            conexion.Dispose();
            conexion.Close();
        }

    }
    /// <summary>
    /// Recupera un valor de la consulta
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public string recuperaValor(string query)
    {
        establecerConexion();
        comando = new SqlCommand(query, conexion);
        comando.CommandType = CommandType.Text;
        string resultado = "";
        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            if (resultados.HasRows)
            {
                while (resultados.Read())
                {
                    resultado = resultados.GetValue(0).ToString();
                }
            }
            conexion.Close();

            return resultado;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            return "-1";
        }
        finally
        {
            conexion.Dispose();
            conexion.Close();
        }

    }
    public DataSet getValues(string query)
    {
        DataSet ds = new DataSet();
        establecerConexion();
        try
        {
            objAdapter = new SqlDataAdapter(query, this.conexion);
            objAdapter.Fill(ds);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return ds;
    }
    #endregion
    public storedProcedure()
    {
        //Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DBRHConnectionString"].ConnectionString);
        this.conn = "DBSGICE";
        conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[conn].ConnectionString);
    }

    /// <summary>
    /// Función getSha512 que permite cifrar el Password que el usuario
    /// ha ingresado. Esta función cifra el Password de tal forma que se
    /// obtenga más seguridad para la aplicación.
    /// </summary>
    /// <param name="Password">Cadena de caractéres con el Password</param>
    /// <returns>Cadena de caracteres con el password cifrado.</returns>
    public String getSha512(String Password)
    {
        System.Security.Cryptography.SHA512Managed HashTool = new System.Security.Cryptography.SHA512Managed();
        Byte[] PasswordAsByte = System.Text.Encoding.UTF8.GetBytes(Password);
        Byte[] EncryptedBytes = HashTool.ComputeHash(PasswordAsByte);
        HashTool.Clear();
        return Convert.ToBase64String(EncryptedBytes);
    }

    public string insertarGrupoRH(string nombreGrupo)
    {
        string resultado2 = "";
        try
        {
            SqlDataReader read = null;
            comando = new SqlCommand("spInsertarGrupoRH", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.Add("@nombreGrupo", SqlDbType.NVarChar).Value = nombreGrupo;

            conexion.Open();
            read = comando.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    resultado2 = read.GetValue(0).ToString();
                }
                return resultado2;
            }
            else
                return resultado2;

        }
        catch (SqlException ex)
        {
            return resultado2;
        }
        finally
        {
            if (comando != null)
                comando.Dispose();
            conexion.Close();
        }
    }

    //Retorna una lista de objetos
    #region getObjectList
    public List<T> getObjectList<T>(string query) where T : new()
    {
        List<T> Lista = new List<T>();
        establecerConexion();
        try
        {
            using (SqlConnection con = conexion)
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 30000;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Data = new T();
                            PropertyInfo[] Properties = Data.GetType().GetProperties();
                            foreach (var p in Properties)
                            {
                                p.SetValue(Data, reader.GetValue(reader.GetOrdinal(p.Name)), null);
                            }
                            Lista.Add(Data);
                        }
                    }
                }
            }
            //conexion.Close();
        }
        catch (Exception ex)
        {
            string error = ex.Message.ToString();
        }
        return Lista;
    }
    #endregion


    public string modificaGrupoRH(int idGrupo, string nombreGrupo)
    {
        string resultado2 = "";
        try
        {
            SqlDataReader read = null;
            comando = new SqlCommand("sp_actualizaGrupoRH", conexion);
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.Add("@idGrupo", SqlDbType.Int).Value = idGrupo;
            comando.Parameters.Add("@nombreGrupo", SqlDbType.NVarChar).Value = nombreGrupo;

            conexion.Open();
            read = comando.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    resultado2 = read.GetValue(0).ToString();
                }
                return resultado2;
            }
            else
                return resultado2;

        }
        catch (SqlException ex)
        {
            return resultado2;
        }
        finally
        {
            if (comando != null)
                comando.Dispose();
            conexion.Close();
        }
    }

    #region Generar Bases de Datos dinámicas

    /// <summary>
    /// Generar el .bak de la base de datos de EMPDANA
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public string generarBDEmpresa(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_GenerarBDEMP", conexion);
        comando.CommandTimeout = 0;

        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
        comando.Parameters.Add("@idSistema", SqlDbType.Int).Value = idSistema;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    /// <summary>
    /// Cambiar los esquemas de DBEMPDANA a los del nuevo Grupo
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public string cambiarEsquemasBDEmpresa(string nomGrupo, int idReporte, int idUsuario)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_CambiarEsquemaBDEMP", conexion);
        comando.CommandTimeout = 0;
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    /// <summary>
    /// Limpiar tablas de la nueva base de datos restaurada para su nueva configuración
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public string limpiarBDEmpresa(string nomGrupo, int idReporte, int idUsuario)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("spLimpiaDBEMP", conexion);
        comando.CommandTimeout = 0;
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    /// <summary>
    /// Generar el .bak de la base de datos que dio clic
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <param name="idSistema"></param>
    /// <returns>String</returns>
    public string generarBDSGC(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_GenerarBDSGC", conexion);
        comando.CommandTimeout = 0;

        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
        comando.Parameters.Add("@idSistema", SqlDbType.Int).Value = idSistema;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    /// <summary>
    /// Cambiar los esquemas de la base de datos que dio clic
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public string cambiarEsquemasBDSGC(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_CambiarEsquemaBDSGC", conexion);
        comando.CommandTimeout = 0;
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
        comando.Parameters.Add("@idSistema", SqlDbType.Int).Value = idSistema;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    /// <summary>
    /// Limpiar tablas de la base de datos que dio clic
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public string limpiarBDSGC(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_LimpiaDBSGC", conexion);
        comando.CommandTimeout = 0;
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
        comando.Parameters.Add("@idSistema", SqlDbType.Int).Value = idSistema;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    //////
    /// <summary>
    /// Generar el .bak de la base de datos que dio clic
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <param name="idSistema"></param>
    /// <returns>String</returns>
    public string generarBDSGRO(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_GenerarBDSGRO", conexion);
        comando.CommandTimeout = 0;

        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
        comando.Parameters.Add("@idSistema", SqlDbType.Int).Value = idSistema;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    /// <summary>
    /// Cambiar los esquemas de la base de datos que dio clic
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public string cambiarEsquemasBDSGRO(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_CambiarEsquemaBDSGRO", conexion);
        comando.CommandTimeout = 0;
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
        comando.Parameters.Add("@idSistema", SqlDbType.Int).Value = idSistema;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    /// <summary>
    /// Limpiar tablas de la base de datos que dio clic
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public string limpiarBDSGRO(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_LimpiaDBSGRO", conexion);
        comando.CommandTimeout = 0;
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
        comando.Parameters.Add("@idSistema", SqlDbType.Int).Value = idSistema;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    //////
    /// <summary>
    /// Generar el .bak de la base de datos que dio clic
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <param name="idSistema"></param>
    /// <returns>String</returns>
    public string generarBDSGCE(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_GenerarBDSGCE", conexion);
        comando.CommandTimeout = 0;

        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
        comando.Parameters.Add("@idSistema", SqlDbType.Int).Value = idSistema;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    /// <summary>
    /// Cambiar los esquemas de la base de datos que dio clic
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public string cambiarEsquemasBDSGCE(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_CambiarEsquemaBDSGCE", conexion);
        comando.CommandTimeout = 0;
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
        comando.Parameters.Add("@idSistema", SqlDbType.Int).Value = idSistema;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    /// <summary>
    /// Limpiar tablas de la base de datos que dio clic
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public string limpiarBDSGCE(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_LimpiaDBSGCE", conexion);
        comando.CommandTimeout = 0;
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
        comando.Parameters.Add("@idSistema", SqlDbType.Int).Value = idSistema;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    public string configuracionInicial(string nomGrupo, int idGrupo,int idReporte, int idUsuario,string lstSistemas)
    {
        string msg = "";
        establecerConexion();
        comando = new SqlCommand("sp_CargarInfInicialEMP", conexion);
        comando.CommandTimeout = 0;
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.Add("@GNOMGRUPO", SqlDbType.VarChar).Value = nomGrupo;
        comando.Parameters.Add("@idGrupo", SqlDbType.Int).Value = idGrupo;
        comando.Parameters.Add("@idReporte", SqlDbType.Int).Value = idReporte;
        comando.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
        comando.Parameters.Add("@lstSistemas", SqlDbType.VarChar).Value = lstSistemas;

        try
        {
            conexion.Open();
            resultados = comando.ExecuteReader();
            while (resultados.Read())
            {
                msg = resultados.GetValue(0).ToString();
            }
            conexion.Close();

            return msg;
        }
        catch (SqlException ex)
        {
            Console.Write(ex);
            msg = "Error: " + ex.Message;
            return msg;
        }
    }

    #endregion
}