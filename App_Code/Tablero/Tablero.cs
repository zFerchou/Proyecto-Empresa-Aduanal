using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

public class Tablero
{
    public Tablero()
    {
    }

    public string abrirTablero(int iIdUsuario)
    {
        string sRespuesta, sRutaTablero, sRutaAPI, sUsuario, sContrasena, sToken, sQuery;

        try
        {
            storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
            sToken = "";
            sQuery = "select sURL from tsConfiguracionTablero";
            sRutaTablero = sp.recuperaValor(sQuery) + "?ssid=";
            sRutaAPI = "https://api.nadconsultoria.com/NAD_AUTHENTIFIER/api/token";
            sUsuario = "ERPM";
            sContrasena = "jOHV39*8A4ptlHc";
            TokenAcceso oTokenAcceso = new TokenAcceso();

            HttpWebRequest oHttpWebRequest = (HttpWebRequest)WebRequest.Create(sRutaAPI);
            oHttpWebRequest.ContentType = "application/json";
            oHttpWebRequest.Method = "POST";
            oHttpWebRequest.Timeout = 100000;
            string sJsonAccess = "{\"sUsuario\":\"" + sUsuario + "\",\"sContrasena\":\"" + sContrasena + "\",\"iUsuario\":" + iIdUsuario + ",\"iServicio\":1}";
            using (var streamWriter = new StreamWriter(oHttpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(sJsonAccess);
            }
            HttpWebResponse oHttpResponse = (HttpWebResponse)oHttpWebRequest.GetResponse();
            using (var oStreamReader = new StreamReader(oHttpResponse.GetResponseStream()))
            {
                sToken = oStreamReader.ReadToEnd();
            }
            using (var oMemoryStm = new MemoryStream(Encoding.Unicode.GetBytes(sToken)))
            {
                DataContractJsonSerializer oJsonDesz = new DataContractJsonSerializer(typeof(TokenAcceso));
                oTokenAcceso = (TokenAcceso)oJsonDesz.ReadObject(oMemoryStm);
            }

            if (oTokenAcceso.iCodigo == 200)
            {
                sToken = oTokenAcceso.sToken;
            }

            if (!string.IsNullOrEmpty(sToken))
            {
                sRespuesta = sRutaTablero + sToken;
            }
            else
            {
                sRespuesta = "-1";
            }

        }
        catch (Exception ex)
        {
            sRespuesta = "-1";
        }

        return sRespuesta;
    }

}



