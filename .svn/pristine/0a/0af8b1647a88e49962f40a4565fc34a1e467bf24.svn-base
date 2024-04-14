<%@ WebHandler Language="C#" Class="uploadReporte" %>

using System;
using System.Web;

public class uploadReporte : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        try
        {
            HttpPostedFile archivo = context.Request.Files["uploadfile"];
            string nombre = "";
            string ruta = "";

            nombre = archivo.FileName.ToString();

            ruta = HttpContext.Current.Server.MapPath("ReportesExcel/" + nombre);

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