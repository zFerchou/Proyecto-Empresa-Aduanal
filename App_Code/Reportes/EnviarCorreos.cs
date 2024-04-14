using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;




/// <summary>
/// Descripción breve de EnviarCorreos
/// </summary>
public class EnvioCorreos
{

    /*Se crea método estático enviosCorreo para ser invocado o llamado sin tener que crear un objeto de dicha clase.*/
    public static string EnviaCorreos(string snombre, string asunto, string receptor, string cuerpo, int ubicacion)
    {
        string sRespuesta = "";

        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");

        /*Se crea un objeto de tipo Lista EnviarCorreos.*/
        List<string> EnviarCorreos = new List<string>();
        try
        {
            /*Se instancia clase WebService y metodo del mismo llamado Correo Se crear un objeto de tipo EnviarCorreoWS*/
            EnviarCorreoWS.Correo oCorreo = new EnviarCorreoWS.Correo();
     
            string sDestinoCorreo = "";

            /*De sDestinoCorreo quita el ultimo ; que encuentre*/
            sDestinoCorreo = receptor.Trim(new Char[] { ';' });

            /*Arreglo que separa strings por ;*/
            string[] sDatosDestino;
            sDatosDestino = sDestinoCorreo.Split(';');

            /*Se utiliza el objeto oCorreo creado, para poder instanciar propiedades de la clase correo y se asigna el valor esperado*/
            oCorreo.SAsunto = snombre;
            oCorreo.ODestino = sDatosDestino;
            oCorreo.STitulo = asunto;
            oCorreo.ITipoCopia = 0;
            oCorreo.ITipoAdjunto = 2;
            oCorreo.SRutaSistema = "https://www.nadconsultoria.com/ERPManagement/";
            oCorreo.SCuerpoCorreo = cuerpo;

            /*Se hace uso del metodo Credenciales del Web Service , creado nuevo objeto oCredencial.*/
            EnviarCorreoWS.Credenciales oCredencial = new EnviarCorreoWS.Credenciales();

            /*Asignar los valoeres admin y password al objeto creado, para poder accesar al web service*/
            oCredencial.SUsuario = "admin";
            oCredencial.SPassword = "password";

            /*Se crear objeto del metodo EnvioCorreoSoapClient del web service.*/
            EnviarCorreoWS.EnvioCorreoSoapClient oCliente = new EnviarCorreoWS.EnvioCorreoSoapClient();

            /*Se crear objeto del metodo Emisor del web service.*/
            EnviarCorreoWS.Emisor oEmisor = new EnviarCorreoWS.Emisor();
            oEmisor.SNombre = "ALERTAS LCA";
            oEmisor.SMail = "alertas@lcamx.com";

            EnviarCorreoWS.Respuesta oRespuesta = new EnviarCorreoWS.Respuesta();

            oCliente.EnviarCorreoNormal(oCredencial, oCorreo, oEmisor);

            if (oRespuesta.IEstatus == 1)
            {
                sRespuesta = "0";
            }

        }
        catch (Exception ex)
        {

            Console.WriteLine("Error en:" + ex);

        }

        return sRespuesta;

    }
}