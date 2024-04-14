using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;

public partial class clientes : System.Web.UI.Page
{
    private SqlConnection conn;
    private static string idUsuario = "";

    private void SetConnection(){
        string connx = ConfigurationManager.ConnectionStrings["DBSGICE"].ConnectionString;
        conn = new SqlConnection(connx);
    }

    //Cargar página
    protected void Page_Load(object sender, EventArgs e){ 
        if (!IsPostBack){
            ViewState["sortOrder"] = "";
            gvbind();
            Master.generaHeader("../../");   
        }

        //Si el usuario no se ha logueado no podrá entrar a esta página
       /* if (!(Boolean)Session["logged"]){
             Response.Redirect("index.aspx");
        }*/
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
            SqlCommand cmd = new SqlCommand("SELECT tc.idCuota,tERPG.nomGrupo,cS.nomSistema,tc.idcuota, "+
                "convert(VARCHAR(10),tc.fechaInicio,103) as fechaInicio,convert(VARCHAR(10),tc.fechaVencimiento,103) as fechaVencimiento, "+
                "'$'+Convert(Varchar(max),tc.cuota) as cuota,'<a onclick=\"modificarCuota(' + convert(varchar(max),tc.idCuota) + ');\"><span id=\"icon-25\" class=\"modificar verde\"></span></a>' as Modificar, " +
                "'<a onclick=\"eliminarCuota(' + convert(varchar(max),tc.idCuota) + ');\"><span id=\"icon-25\" class=\"eliminar rojo\"></span></a>' as Eliminar " +
                "from tCuotas tc inner join tERPGrupoSistema tERPGS ON tc.idERPGrupoSistema = tERPGS.idERPGrupoSistema "+
                "inner join tERPGrupo tERPG ON tERPG.idERPGrupo = tERPGS.idERPGrupo inner join cSistemas cS "+
                "ON cS.idSistema = tERPGS.idSistema where tc.idERPGrupoSistema = tERPGS.idERPGrupoSistema "+
                "And tc.idEstatus = 1", conn);
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


    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            GridView1.UseAccessibleHeader = true;
            //This will add the <thead> and <tbody> elements
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            //This adds the <tfoot> element. 
            //Remove if you don't have a footer row
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    

    
}