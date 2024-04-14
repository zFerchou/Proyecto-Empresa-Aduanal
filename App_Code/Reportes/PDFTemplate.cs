using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf.draw;

/// <summary>
/// Clase utilizada para generar encabezado y pie de pagina de PDF
/// </summary>
public class PDFTemplate : PdfPageEventHelper 
{
    #region Variables

    PdfContentByte oPdfContentByte;
    PdfTemplate oHeaderTemplate;
    PdfTemplate oFooterTemplate;
    BaseFont oBaseFont;
    string sGrupo;
    string sFechaInicio;
    string sFechaFin;
    string sSistema;

    #endregion

    public PDFTemplate(string sGrupo, string sFechaInicio, string sFechaFin, string sSistema) {
        this.sGrupo = sGrupo;
        this.sFechaInicio = sFechaInicio;
        this.sFechaFin = sFechaFin;
        this.sSistema = sSistema;
    }

    /// <summary>
    /// Metodo para agregar el encabezado
    /// </summary>
    /// <param name="oWriter"></param>
    /// <param name="oDocument"></param>
    public override void OnStartPage(PdfWriter oWriter, Document oDocument)
    {
        oBaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        oPdfContentByte = oWriter.DirectContent;

        oHeaderTemplate = oPdfContentByte.CreateTemplate(100, 100);
        oFooterTemplate = oPdfContentByte.CreateTemplate(50, 50);

        string sPathImg =  System.Web.Hosting.HostingEnvironment.MapPath("~/images/logos/logo.jpg");
        Image oImagen = Image.GetInstance(sPathImg);
        oImagen.Alignment = Element.ALIGN_LEFT;
        oImagen.ScaleAbsolute(130,75);
        oImagen.SetAbsolutePosition(45, 883);

        oPdfContentByte = oWriter.DirectContent;
        PdfPTable oTable = new PdfPTable(3);
        oTable.WidthPercentage = 100;
        oTable.SetTotalWidth(new float[] { 45, 280, 72 });

        PdfPCell oCellPrincipal = new PdfPCell();
        PdfPCell oCellLogo = new PdfPCell();
        oCellPrincipal.BackgroundColor = WebColors.GetRGBColor("#1d6688");
        Paragraph oTextoCellPrincipal = new Paragraph("REPORTE DE INGRESOS AL SISTEMA DE GESTIÓN INTEGRAL");
        oTextoCellPrincipal.Font.SetStyle(Font.BOLD);
        oTextoCellPrincipal.Alignment = Element.ALIGN_CENTER;
        oTextoCellPrincipal.Font.Size = 17;
        oTextoCellPrincipal.Font.Color = BaseColor.WHITE;
        oCellPrincipal.AddElement(oTextoCellPrincipal);

        PdfPTable oTableFiltros = new PdfPTable(4);
        oTableFiltros.SetTotalWidth(new float[] { 15, 25, 15, 45 });
        oTableFiltros.WidthPercentage = 98;

        PdfPCell oCellGrupot = new PdfPCell();
        Paragraph oPGrupot = new Paragraph("Grupo:");
        oPGrupot.Font.Size = 15;
        oPGrupot.Font.SetStyle(Font.BOLD);
        oPGrupot.Font.Color = BaseColor.WHITE;
        oCellGrupot.AddElement(oPGrupot);
        oCellGrupot.BackgroundColor = WebColors.GetRGBColor("#163746"); ;
        oTableFiltros.AddCell(oCellGrupot);

        PdfPCell oCellGrupo = new PdfPCell();
        Paragraph oPGrupo = new Paragraph(sGrupo);
        oPGrupo.Font.Size = 15;
        oCellGrupo.AddElement(oPGrupo);
        oCellGrupo.BackgroundColor = BaseColor.WHITE;
        oTableFiltros.AddCell(oCellGrupo);

        PdfPCell oCellSistemat = new PdfPCell();
        Paragraph oPSistemat = new Paragraph("Sistema:");
        oPSistemat.Font.Size = 15;
        oPSistemat.Font.SetStyle(Font.BOLD);
        oPSistemat.Font.Color = BaseColor.WHITE;
        oCellSistemat.AddElement(oPSistemat);
        oCellSistemat.BackgroundColor = WebColors.GetRGBColor("#163746"); ;
        oTableFiltros.AddCell(oCellSistemat);

        PdfPCell oCellSistema = new PdfPCell();
        Paragraph oPSistema = new Paragraph(sSistema);
        oPSistema.Font.Size = 15;
        oCellSistema.AddElement(oPSistema);
        oCellSistema.BackgroundColor = BaseColor.WHITE;
        oTableFiltros.AddCell(oCellSistema);

        PdfPCell oCellFinit = new PdfPCell();
        Paragraph oPFinit = new Paragraph("Fecha inicio:");
        oPFinit.Font.Size = 15;
        oPFinit.Font.SetStyle(Font.BOLD);
        oPFinit.Font.Color = BaseColor.WHITE;
        oCellFinit.AddElement(oPFinit);
        oCellFinit.BackgroundColor = WebColors.GetRGBColor("#163746"); ;
        oTableFiltros.AddCell(oCellFinit);

        PdfPCell oCellFini = new PdfPCell();
        Paragraph oPFini = new Paragraph(sFechaInicio == "" ? "-" : sFechaInicio);
        oPFini.Font.Size = 15;
        oCellFini.AddElement(oPFini);
        oCellFini.BackgroundColor = BaseColor.WHITE;
        oTableFiltros.AddCell(oCellFini);

        PdfPCell oCellFfint = new PdfPCell();
        Paragraph oPFfint = new Paragraph("Fecha fin:");
        oPFfint.Font.Size = 15;
        oPFfint.Font.SetStyle(Font.BOLD);
        oPFfint.Font.Color = BaseColor.WHITE;
        oCellFfint.AddElement(oPFfint);
        oCellFfint.BackgroundColor = WebColors.GetRGBColor("#163746"); ;
        oTableFiltros.AddCell(oCellFfint);

        PdfPCell oCellFfin = new PdfPCell();
        Paragraph oPFfin = new Paragraph(sFechaFin == "" ? "-" : sFechaFin);
        oPFfin.Font.Size = 15;
        oCellFfin.AddElement(oPFfin);
        oCellFfin.BackgroundColor = BaseColor.WHITE;
        oTableFiltros.AddCell(oCellFfin);

        oCellPrincipal.AddElement(oTableFiltros);

        Paragraph oTextoCellFechaTitulo = new Paragraph("Fecha de consulta:");
        oTextoCellFechaTitulo.Alignment = Element.ALIGN_CENTER;
        oTextoCellFechaTitulo.Font.Size = 15;
        PdfPCell oCellDate = new PdfPCell();
        Paragraph oTextoCellFecha = new Paragraph(DateTime.Now.ToString());
        oTextoCellFecha.Alignment = Element.ALIGN_CENTER;
        oTextoCellFecha.Font.Size = 15;
        
        oCellDate.AddElement(oTextoCellFechaTitulo);
        oCellDate.AddElement(oTextoCellFecha);

        oTable.AddCell(oCellLogo);
        oTable.AddCell(oCellPrincipal);
        oTable.AddCell(oCellDate);

        oDocument.Add(oTable);
        oPdfContentByte.AddImage(oImagen);
    }

    /// <summary>
    /// Método  para colocar pie de pagina.
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="document"></param>
    public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
    {
        try
        {
            PdfContentByte cbPie = new PdfContentByte(writer);
            cbPie = writer.DirectContent;
            cbPie.BeginText();
            cbPie.SetFontAndSize(FontFactory.GetFont(FontFactory.HELVETICA, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL).BaseFont, 12);
            cbPie.SetColorFill(iTextSharp.text.BaseColor.BLACK);
            cbPie.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Página: " + writer.PageNumber, 1350, 25, 0);
            cbPie.EndText();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);

        }
    }
}