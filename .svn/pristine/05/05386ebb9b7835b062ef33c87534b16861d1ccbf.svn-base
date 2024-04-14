using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.ComponentModel;
using System.Web;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web.Services;

/// <summary>
/// Descripción breve de ControllerGenerarSistemas
/// </summary>
public class ControllerIndex
{
    private ERPManagementRHDataContext erprh;
    private tCorreoElectronicoEmpleado correoEmpleado;
    private storedProcedure sp;
    private Utileria utileria;
    public string user;
    public string pass;
    public string mensaje = "";
    public const string CONEXION = "DBCE_EMPRESAConnectionString";
    public const string CONEXION2 = "DBSGICEConnectionString";
    public ControllerIndex()
    {
        erprh = new ERPManagementRHDataContext();
        sp = new storedProcedure();
        utileria = new Utileria();
        correoEmpleado = new tCorreoElectronicoEmpleado();
    }

    public void recuperarContrasenia(string usuario) {
        string randomPassword;
        var getUsuario = (from tr in erprh.tUsuarios
                        where tr.usuario == usuario
                        select tr).FirstOrDefault();
        //Obtener una contraseña.
        randomPassword = CreateRandomPassword(10);
        enviarCorreo(usuario, randomPassword);
        //Encriptar la contraseña.
        string newPassword = sp.getSha512(randomPassword);
        //Setear la nueva contraseña encriptada al campo "constrasena" para actualizarla.
        getUsuario.contrasena = newPassword;
        getUsuario.idEstatusUsuario = 1;
        //Actualizar la contraseña.
        erprh.SubmitChanges();        
    }

    /// <summary>
    /// Método para generar contraseñas alfanumericamente
    /// </summary>
    /// <param name="passwordLength"></param>
    /// <returns>String</returns>
    private static string CreateRandomPassword(int passwordLength)
    {
        string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
        char[] chars = new char[passwordLength];
        Random rd = new Random();

        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }

        return new string(chars);
    }

    /// <summary>
    /// Método para enviar correo electrónico c
    /// </summary>
    /// <returns>True si el correo se envia de manera exitosa, de lo contrario false</returns>
    /// 

  
    public bool enviarCorreo(string usuario, string contrasenia)
    {
        var getUsuario = (from tr in erprh.tUsuarios
                          where tr.usuario == usuario
                          select tr).FirstOrDefault();
        var getCorreo = (from tr in erprh.tCorreoElectronicoEmpleado
                          where tr.idEmpleado == getUsuario.idUsuario && tr.idTipoCorreoElectronico==1
                          select tr).FirstOrDefault();
        string receptor = getCorreo.correoElectronico;
        string cuerpo ="<table border='0' cellpadding='0' cellspacing='0' align='center' width='100%'><tbody><tr bgcolor='#85c555'><td colspan='5' height='20'>&nbsp;</td></tr><tr bgcolor='#85c555'><td width='20'>&nbsp;</td><td width='20'>&nbsp;</td><td align='center' style='font-size:29px;color:#ffffff;font-weight:normal;letter-spacing:1px;line-height:1;font-family:Arial,Helvetica,sans-serif'>" + usuario + ", tu nueva contraseña es: " + contrasenia + "</td><td width='20'>&nbsp;</td><td width='20'>&nbsp;</td></tr><tr bgcolor='#85c555'><td colspan='5' height='20'>&nbsp;</td></tr></tbody></table>";
        string snombre = "Alertas LCA";
        string asunto = "Nueva Usuario y Contraseña";
        int ubicacion = 0;

        sp = new storedProcedure("DBSGICEConnectionString");
        EnvioCorreos.EnviaCorreos(snombre, asunto, receptor, cuerpo, ubicacion);
        
        return true;
    }

    [WebMethod]
    public static string cambiarContrasenia(int idUsuario, string contrasenia)
    {
        ERPManagementRHDataContext rh = new ERPManagementRHDataContext();
        storedProcedure util = new storedProcedure(CONEXION2);
        try
        {
            //Encriptar la contraseña.
            string newPassword = util.getSha512(contrasenia);
            var getUsuario = (from tr in rh.tUsuarios
                              where tr.idUsuario == idUsuario
                              select tr).FirstOrDefault();
            //Setear la nueva contraseña encriptada al campo "constrasena" para actualizarla.
            getUsuario.contrasena = newPassword;
            getUsuario.idEstatusUsuario = 2;
            //Actualizar la contraseña.
            rh.SubmitChanges();
            return "Cotraseña actualizada correctamente. °Correcto";
        }
        catch (Exception ex)
        {
            return "Error al cambiar tu contraseña. °Error";
        }
    }
}