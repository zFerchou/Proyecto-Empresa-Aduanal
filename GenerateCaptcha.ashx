<%@ WebHandler Language="C#" Class="GenerateCaptcha" %>

using System;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Imaging;


public class GenerateCaptcha : IHttpHandler, IReadOnlySessionState {

    public void ProcessRequest (HttpContext context) {
        MemoryStream memStream = new MemoryStream();
        //2)Obtiene el código de la sessiónpara pasarlo a String y generar la imágen.
        string phrase = Convert.ToString(context.Session["captcha"]);

        //Generar una imagen con los numeros obtenidos de Session["captcha"].
        Bitmap imgCapthca = GenerateImage(210, 30, phrase);
        imgCapthca.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        byte[] imgBytes = memStream.GetBuffer();

        imgCapthca.Dispose();
        memStream.Close();

        //Genera la imagen y la muestra en pantalla
        context.Response.ContentType = "image/jpeg";
        context.Response.BinaryWrite(imgBytes);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

    public Bitmap GenerateImage(int Width, int Height, string Phrase)
    {
        Bitmap CaptchaImg = new Bitmap(Width, Height);
        Random Randomizer = new Random();
        Graphics Graphic = Graphics.FromImage(CaptchaImg);
        Graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        Graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        //Set height and width of captcha image
        Graphic.FillRectangle(new SolidBrush(Color.Black), 0, 0, Width, Height);
        //Rotate text a little bit
        Graphic.RotateTransform(0);
        Graphic.DrawString(Phrase, new Font("Verdana", 20),new SolidBrush(Color.White), 15, 0);
        Graphic.Flush();
        return CaptchaImg;
    }

} 