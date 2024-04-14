<%@ WebHandler Language="C#" Class="uploadEvidencia" %>

using System;
using System.Web;

public class uploadEvidencia : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "text/plain";
        string ruta = "";

        try
        {
            HttpPostedFile archivo = context.Request.Files["FileData"];
            string nombre = "";
            int idReporte = int.Parse(context.Request["idReporte"].ToString());
            nombre = archivo.FileName.ToString();

            ruta = HttpContext.Current.Server.MapPath("Configuracion/Reportes/Evidencia/" + nombre);
            archivo.SaveAs(ruta);
            context.Response.Write("1");
        }
        catch (Exception e)
        {
            context.Response.Write("0");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}