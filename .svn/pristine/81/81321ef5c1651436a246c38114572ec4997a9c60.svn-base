<%@ WebHandler Language="C#" Class="uploadEvidenciaEnviar" %>

using System;
using System.Web;

public class uploadEvidenciaEnviar : IHttpHandler {

    public void ProcessRequest (HttpContext context) {

        //context.Response.ContentType = "text/plain";


        try
        {

            HttpPostedFile archivo = context.Request.Files["uploadfile"];
            string nombre = "";
            string ruta = "";
            //int idReporte = int.Parse(context.Request["idReporte"].ToString());
            nombre = archivo.FileName.ToString();

            ruta = HttpContext.Current.Server.MapPath("Configuracion/Reportes/EvidenciaRespuesta/" + nombre);
            //ruta = ("C:/Users/sarahi.huerta/Desktop/Repositorios/ERP_PRODUCCION/Configuracion/Reportes/Evidencia");
            archivo.SaveAs(ruta);
            //context.Response.Write("1");
            context.Response.Write(nombre);
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