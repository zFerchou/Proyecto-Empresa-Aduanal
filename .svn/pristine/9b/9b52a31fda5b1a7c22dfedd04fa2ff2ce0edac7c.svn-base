<%@ WebHandler Language="C#" Class="UploadActividades" %>

using System;
using System.Web;
using System.IO;
using System.Data;
using ExcelDataReader;
using OfficeOpenXml;
using OfficeOpenXml.Table;

public class UploadActividades : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        try
        {
            HttpPostedFile archivo = context.Request.Files["uploadfile"];
            Utileria util = new Utileria();
            var idUsuarioHeader = util.DecodeFrom64(context.Request.Headers["idUsuario"]);

            int idUsuario = 0;
            int.TryParse(idUsuarioHeader, out idUsuario);

            string directorio = HttpContext.Current.Server.MapPath("ArchivosUpload/");

            string ruta = Path.Combine(directorio, archivo.FileName.ToString());

            archivo.SaveAs(ruta);

            ControllerActividades controller = new ControllerActividades();

            var resultado = controller.LeerExcel(idUsuario, ruta);            

            if (File.Exists(ruta))
            {
                File.Delete(ruta);
            }

            context.Response.Write("1");   

            if (!resultado)
            { 
                context.Response.Write("0");    
            }
        }
        catch (Exception ex)
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