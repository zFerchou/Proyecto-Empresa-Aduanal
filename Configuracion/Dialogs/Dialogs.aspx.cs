using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Dialogs : System.Web.UI.Page
{
    private static ControllerResponsableReporte controllerResponsableReporte;
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.generaHeader("../../");
    }
    //Obtener a las personas que estan como Apoyo en un reporte y mostrarlas en una tabla.
    [WebMethod]
    public static string tblPersonalApoyo(int idReporte)
    {
        controllerResponsableReporte = new ControllerResponsableReporte();
        return controllerResponsableReporte.tblPersonalApoyo(idReporte);
    }

    //Obtener a la persona que estan como Responsable en un reporte.
    [WebMethod]
    public static string tblPersonalResponsable(int idReporte)
    {
        controllerResponsableReporte = new ControllerResponsableReporte();
        return controllerResponsableReporte.tblPersonalResponsable(idReporte);
    }

    //Obtener a las personas que aun no han sido asignadas como apoyo en un reporte para que puedan ser asignados.
    [WebMethod]
    public static List<AutoCompleteResponsables> obtenerPersonalApoyo(string term)
    {
        List<AutoCompleteResponsables> resultado = new List<AutoCompleteResponsables>();
        List<string> obtener = new List<string>();
        AutoCompleteResponsables ac;
        string query = "";
        term = term.ToLower();
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        query = "SELECT idEmpleado,nombre+' '+apellidoPaterno+' '+apellidoMaterno FROM DBSGRH.dbo.tEmpleado LEFT JOIN tResponsableReporte ON tResponsableReporte.idResponsable=DBSGRH.dbo.tEmpleado.idEmpleado WHERE idEmpleado NOT IN(SELECT idResponsable FROM tResponsableReporte)AND nombre LIKE '%" + term + "%'";
        obtener = sp.recuperaRegistros(query);

        if (obtener != null && obtener.Count > 0)
        {
            for (int i = 0; i < obtener.Count; i += 2)
            {
                ac = new AutoCompleteResponsables();
                ac.Id= obtener[i];
                ac.Nombre = obtener[i + 1];
                resultado.Add(ac);
            }
        }
        else
        {
            resultado.Add(new AutoCompleteResponsables { Id = "", Nombre = "No se encontraron resultados" });
        }
        return resultado;
    }
    
    //Guardar a las personas que van a estar como Apoyo en un Reporte
    [WebMethod]
    public static bool guardarPersonaApoyo(int idReporte, int idResponsable)
    {
        controllerResponsableReporte = new ControllerResponsableReporte();
        return controllerResponsableReporte.guardarPersonaApoyo(idReporte, idResponsable);
    }

    //Eliminar a las personas que vestán como Apoyo en un Reporte
    [WebMethod]
    public static bool eliminarPersonaApoyo(int idReporte, int idResponsable)
    {
        controllerResponsableReporte = new ControllerResponsableReporte();
        return controllerResponsableReporte.eliminarPersonaApoyo(idReporte, idResponsable);
    }

}