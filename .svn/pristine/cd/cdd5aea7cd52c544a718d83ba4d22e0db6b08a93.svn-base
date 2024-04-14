using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;
/// <summary>
/// Descripción breve de Utileria
/// </summary>
public class Utileria
{
    public Utileria()
    {

    }

    /// <summary>
    /// Método para enviar emails
    /// </summary>
    /// <param name="para"></param>
    /// <param name="asunto"></param>
    /// <param name="cuerpo"></param>
    /// 
    public bool sendMail(string receptor, string asunto, string cuerpo, int ubicacion)
    {
        storedProcedure sp = new storedProcedure();

        string snombre = "";
        snombre = "Alertas LCA";

        EnvioCorreos.EnviaCorreos(snombre, asunto, receptor, cuerpo, ubicacion);

        return true;
    }


    public string EncodeTo64(string text)
    {
        byte[] toEncodeAsBytes
              = System.Text.ASCIIEncoding.ASCII.GetBytes(text);
        string returnValue
              = System.Convert.ToBase64String(toEncodeAsBytes);
        return returnValue;
    }
    public string DecodeFrom64(string text)
    {
        byte[] encodedDataAsBytes
            = System.Convert.FromBase64String(text);
        string returnValue =
           System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
        return returnValue;
    }


}



