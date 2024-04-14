using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
/// Clase para controlar las acciones de los reportes
/// por acceso a los sistemas.
/// </summary>
public class ControllerAccesoReportes
{
    /// <summary>
    /// Declaración de variables a utilizar
    /// </summary>
    private ERPManagementDataContext erp;
    private tLogAcceso tLogAcceso;
    private List<tLogAcceso> lstLogAcceso;

	public ControllerAccesoReportes()
	{
        erp = new ERPManagementDataContext();
        tLogAcceso = new tLogAcceso();
	}

    /// <summary>
    /// Método para generar el código html de la 
    /// tabla de numero de ingreso, dependiendo
    /// de los filtros ingresados.
    /// </summary>
    /// <param name="grupo"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <param name="sistema"></param>
    /// <returns>Tabla construida en código html</returns>
    public string obtenerReporte(int grupo, string fechaInicio, string fechaFin, int sistema) 
    {
        string result = "";
        string titulo = "";
        string contenido = "";
        try
        {
            var vDetalle = (from vd in erp.vDetalleIngresos
                            select vd);

            if (!fechaInicio.Equals(""))
            {
                vDetalle = vDetalle.Where(det => det.fechaIngreso >= DateTime.Parse(fechaInicio));
            }

            if (!fechaFin.Equals(""))
            {
                vDetalle = vDetalle.Where(det => det.fechaIngreso <= DateTime.Parse(fechaFin));
            }

            if (grupo == 0 && sistema == 0)
            {
                var lstDetalle = (from vd in vDetalle
                                  group vd by vd.nomGrupo into g
                                  select new { Grupo = g.Key, Cantidad = g.Count() });

                titulo = "Grupo";
                    foreach (var vd in lstDetalle)
                    {
                        contenido += "<tr>";
                        contenido += "<th>" + vd.Grupo + "</th>" +
                                   "<td>" + vd.Cantidad + "</td>" +
                                   "<td>{}</td>" +
                                   "<td>true</td>";
                        contenido += "</tr>";
                    }
            }
            else if (grupo != 0 && sistema == 0)
            {
                vDetalle = vDetalle.Where(det => det.idERPGrupo == grupo);

                var lstDetalle = (from vd in vDetalle
                                  group vd by vd.nomSistema into g
                                  select new { Sistema = g.Key, Cantidad = g.Count() });

                titulo = "Sistema (Módulo)";

                foreach (var vd in lstDetalle)
                {
                    contenido += "<tr>";
                    contenido += "<th>" + vd.Sistema + "</th>" +
                               "<td>" + vd.Cantidad + "</td>" +
                                   "<td>{}</td>" +
                                   "<td>true</td>";
                    contenido += "</tr>";
                }
            }
            else if (grupo != 0 && sistema != 0)
            {
                vDetalle = vDetalle.Where(det => det.idERPGrupo == grupo && det.idSistema == sistema);

                var lstDetalle = (from vd in vDetalle
                                  group vd by vd.nomUsuario into g
                                  select new { Usuario = g.Key, Cantidad = g.Count() });

                titulo = "Usuarios";

                foreach (var vd in lstDetalle)
                {
                    contenido += "<tr>";
                    contenido += "<th>" + vd.Usuario + "</th>" +
                               "<td>" + vd.Cantidad + "</td>" +
                                   "<td>{}</td>" +
                                   "<td>true</td>";
                    contenido += "</tr>";
                }
            }
            else if (grupo == 0 && sistema != 0)
            {
                vDetalle = vDetalle.Where(det => det.idSistema == sistema);

                var lstDetalle = (from vd in vDetalle
                                  group vd by new { vd.nomSistema, vd.nomGrupo } into g
                                  select new { Sistema = g.Key.nomGrupo +" / "+ g.Key.nomSistema, Cantidad = g.Count() });

                titulo = "Grupo / Sistema";

                foreach (var vd in lstDetalle)
                {
                    contenido += "<tr>";
                    contenido += "<th>" + vd.Sistema + "</th>" +
                               "<td>" + vd.Cantidad + "</td>" +
                                   "<td>{}</td>" +
                                   "<td>true</td>";
                    contenido += "</tr>";
                }

               
            }

            if (!contenido.Equals(""))
            {
                result += " <table id='tblDetalleIngresos' class='data_grid'>" +
                           "<thead id='grid-head2'>" +
                               "<tr>" +
                                   "<th>" + titulo + "</th>" +
                                   "<th>No. Ingresos</th>" +
                                   "<th>D</th>" +
                                   "<th>B</th>" +
                               "</tr>" +
                           "</thead>" +
                           "<tbody id='grid-body'>";
                result += contenido;
                result += "</tbody>" +
               "</table>";
            }

            return result;
        }
        catch (Exception e)
        {
            return result;
        }                
    }


    /// <summary>
    /// Método para generar el código html de la 
    /// tabla de numero de ingreso, dependiendo
    /// de los filtros ingresados.
    /// </summary>
    /// <param name="grupo"></param>
    /// <param name="fechaInicio"></param>
    /// <param name="fechaFin"></param>
    /// <param name="sistema"></param>
    /// <returns>Tabla construida en código html</returns>
    public string obtenerSerie(int grupo, string fechaInicio, string fechaFin, int sistema, string name)
    {
        string result = "";
        try
        {
            var vDetalle = (from vd in erp.vDetalleIngresos
                            select vd);

            if (!fechaInicio.Equals(""))
            {
                vDetalle = vDetalle.Where(det => det.fechaIngreso >= DateTime.Parse(fechaInicio));
            }

            if (!fechaFin.Equals(""))
            {
                vDetalle = vDetalle.Where(det => det.fechaIngreso <= DateTime.Parse(fechaFin));
            }

            if (grupo == 0 && sistema == 0)
            {
                var lstDetalle = (from vd in vDetalle
                                  where vd.nomGrupo.Equals(name)
                                  group vd by vd.nomSistema into g
                                  select new { Sistema = g.Key, Cantidad = g.Count()});
                result = "{\"data\":[";
                int i = 0;
                foreach (var vd in lstDetalle)
                {
                    i++;
                    result += "{\"id\":"+i+",\"name\":\""+vd.Sistema+"\",\"y\":"+vd.Cantidad+",\"drilldown\":true},";
                }
                result = result.TrimEnd(',');
                result += "]}";
            }
            else if (grupo != 0 && sistema == 0)
            {
                vDetalle = vDetalle.Where(det => det.idERPGrupo == grupo);

                var lstDetalle = (from vd in vDetalle
                                  where vd.nomSistema.Equals(name)
                                  group vd by vd.nomUsuario into g
                                  select new { Usuario = g.Key, Cantidad = g.Count() });

                result = "{\"data\":[";
                int i = 0;
                foreach (var vd in lstDetalle)
                {
                    i++;
                    result += "{\"id\":" + i + ",\"name\":\"" + vd.Usuario + "\",\"y\":" + vd.Cantidad + ",\"drilldown\":false},";
                }
                result = result.TrimEnd(',');
                result += "]}";
            }
            else if (grupo == 0 && sistema != 0)
            {
                vDetalle = vDetalle.Where(det => det.idSistema == sistema);

                var lstDetalle = (from vd in vDetalle
                                  where (vd.nomGrupo + " / " + vd.nomSistema).Equals(name)
                                  group vd by vd.nomUsuario into g
                                  select new { Usuario = g.Key, Cantidad = g.Count() });

                result = "{\"data\":[";
                int i = 0;
                foreach (var vd in lstDetalle)
                {
                    i++;
                    result += "{\"id\":" + i + ",\"name\":\"" + vd.Usuario + "\",\"y\":" + vd.Cantidad + ",\"drilldown\":false},";
                }
                result = result.TrimEnd(',');
                result += "]}";
            }
            return result;
        }
        catch (Exception e)
        {
            return result;
        }
    }


    public string generarPDF(HttpContext currentContext, string grupo, string fechaInicio, string fechaFin, string sistema)
    {
        using (Document oDocument = new Document(PageSize.B3.Rotate()))
        {
            string sRuta = currentContext.Server.MapPath("ReporteIngresoERP.pdf");

            if (File.Exists(sRuta))
                File.Delete(sRuta);

            PdfWriter oWriter = PdfWriter.GetInstance(oDocument,new FileStream(sRuta, FileMode.Create, FileAccess.Write, FileShare.None));

            oWriter.PageEvent = new PDFTemplate(grupo, fechaInicio, fechaFin, sistema);
            oDocument.Open();

            string[] arrFiles = Directory.GetFiles(currentContext.Server.MapPath("ImgGraficas/"));
            foreach (string file in arrFiles)
            {
                Bitmap oImageBitmap = new Bitmap(file);
                System.Drawing.Rectangle cloneRect = new System.Drawing.Rectangle(0, 0, 3125, 1205);
                PixelFormat oPxFormat = oImageBitmap.PixelFormat;
                Bitmap cloneBitmap = oImageBitmap.Clone(cloneRect, oPxFormat);
                oImageBitmap.Dispose();
                cloneBitmap.Save(file);
                iTextSharp.text.Image oImagen = iTextSharp.text.Image.GetInstance(file);
                oImagen.Alignment = Element.ALIGN_CENTER;
                oImagen.BorderColor = BaseColor.BLACK;
                oDocument.Add(new Paragraph("    "));
                oDocument.Add(new Paragraph("    "));
                if (arrFiles.Count() != 1)
                {
                    oImagen.ScalePercent(28f);
                }
                else
                { 
                    oImagen.ScalePercent(38f);
                }
                oDocument.Add(oImagen);
            }
            oDocument.Close();
            oDocument.Dispose();
            foreach (string file in arrFiles)
                File.Delete(file);
        }
        return "ReporteIngresoERP.pdf";
    }
}