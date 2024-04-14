<%@ WebHandler Language="C#" Class="uploadEvidencia" %>

using System;
using System.Web;

public class uploadEvidencia : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        try
        {
            HttpPostedFile archivo = context.Request.Files["uploadfile"];
            string nombre = "";
            string ruta = "";

            nombre = archivo.FileName.ToString();

            ruta = HttpContext.Current.Server.MapPath("Evidencia/" + nombre);

            archivo.SaveAs(ruta);
            ControllerReportes controller = new ControllerReportes();

            context.Response.Write("1");
        }
        catch (Exception e)
        {
            context.Response.Write("0");
        }


    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    
}