using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServidorCS : System.Web.UI.Page
{

    private SqlConnection conn;
    private static string idUsuario = "";

    private void SetConnection()
    {
        string connx = ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString;
        conn = new SqlConnection(connx);
    }

    //Cargar página
    protected void Page_Load(object sender, EventArgs e)
    {



        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            gvbind();
            Master.generaHeader("../../");
        }


        idUsuario = Session["id"].ToString();




    }


    //LLENAR EL GRIDVIEW, PARA CADA ACCIÓN
    protected void gvbind()
    {
        try
        {
            lblResult.Text = "";
            SetConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Categoria, NomInstancia, TipoInstancia, idServidor FROM tServidores as tS INNER JOIN cTipoInstancia as cTi ON cTi.idTipoInstancia = tS.idServidor", conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            //error
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                int cantrows = ds.Tables[0].Rows.Count;
                // lblTotal.Text = "MOSTRANDO TOTAL DE REGISTROS: " + " " + " " + cantrows.ToString();
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Cells[0].Text = "SIN REGISTROS";

            }

        }
        catch (Exception ex)
        {
            Response.Write("<div id='dDialogo' title='ERP Managment'><span id='icon-25' class='rojo rechazar'> NO FUE POSIBLE MOSTRAR LOS REGISTROS</span></div>");
        }



    }
    // Boton para agregar un servidor
    [WebMethod]
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            // Establecer la conexión con la base de datos
            SetConnection();
            conn.Open();

            // Crear el comando para llamar al procedimiento almacenado
            SqlCommand cmd = new SqlCommand("paInsertarDatosServidor", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Pasar los parámetros al procedimiento almacenado
            cmd.Parameters.AddWithValue("@Categoria", txtCategoria.Text);
            cmd.Parameters.AddWithValue("@NomInstancia", txtNomInstancia.Text);
            cmd.Parameters.AddWithValue("@IpPublica", txtIpPublica.Text);
            cmd.Parameters.AddWithValue("@IpPrivada", txtIpPrivada.Text);
            cmd.Parameters.AddWithValue("@UrlServidor", txtUrlServidor.Text);
            cmd.Parameters.AddWithValue("@ZonaDisponible", txtZonaDisponible.Text);
            cmd.Parameters.AddWithValue("@Estatus", txtEstatus.Text);
            cmd.Parameters.AddWithValue("@idUsuario", int.Parse(idUsuario)); 
            cmd.Parameters.AddWithValue("@idTipoInstancia", int.Parse(txtIdTipoInstancia.Text));
            cmd.Parameters.AddWithValue("@idApp", int.Parse(txtIdApp.Text));

            // Ejecutar el comando
            cmd.ExecuteNonQuery();

            // Refrescar los datos en el GridView después de la inserción
            gvbind();

            // Mostrar mensaje de éxito
            lblResult.Text = "Datos insertados correctamente";
        }
        catch (Exception ex)
        {
            // Mostrar mensaje de error si ocurre una excepción
            lblResult.Text = "Error al insertar datos: " + ex.Message;
        }
        finally
        {
            conn.Close();
        }
    }

    


    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {

            GridView1.UseAccessibleHeader = true;

            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    
    // Metodo para obtener los datos de Información
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)] // Esto indica que la respuesta será en formato JSON
    public static object ObtenerInformacion(int idServidor)
    {

        
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Categoria, NomInstancia, IpPublica, IpPrivada, UrlServidor, ZonaDisponible FROM tServidores WHERE idServidor = @idServidor", conn);
            cmd.Parameters.AddWithValue("@idServidor", idServidor);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                // Retornar los datos como un objeto JSON
                return new
                {
                    Categoria = dt.Rows[0]["Categoria"],
                    NomInstancia = dt.Rows[0]["NomInstancia"],
                    IpPublica = dt.Rows[0]["IpPublica"],
                    IpPrivada = dt.Rows[0]["IpPrivada"],
                    UrlServidor = dt.Rows[0]["UrlServidor"],
                    ZonaDisponible = dt.Rows[0]["ZonaDisponible"]
                };
            }
            else
            {
                return null; // Si no se encuentra ningún registro
            }
        }
        catch (Exception ex)
        {
            // Manejar la excepción

            return null;
        }
        finally
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString);
            conn.Close();
        }
    }
}