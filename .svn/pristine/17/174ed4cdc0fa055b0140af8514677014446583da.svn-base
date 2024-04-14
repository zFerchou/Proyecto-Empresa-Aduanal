using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Descripción breve de ControllerGenerarSistemas
/// </summary>
public class ControllerGenerarSistemas
{
    private view_PeticionSistemas peticionSistemas;
    private view_ExisteBDEMP existeBDEMP;
    private view_ExisteBDSGC existeDBSGC;
    private view_ExisteBDSGRO existeDBSGRO;
    private view_ExisteBDSGCE existeDBSGCE;
    private view_HistorialBDEjeciciones verHistorial;
    private ERPManagementDataContext erp;
    private tERPGrupoSistema tERPGrupoSistema;
    private tLogEjecucionBD logEjecucionBD;
    private cSistemas ccSistemas;
    private tERPGrupo tGrupo;
    storedProcedure sp = new storedProcedure("DBSGICEConnectionString");

	public ControllerGenerarSistemas()
	{
        erp = new ERPManagementDataContext();
        peticionSistemas = new view_PeticionSistemas();
        existeBDEMP = new view_ExisteBDEMP();
        existeDBSGC = new view_ExisteBDSGC();
        existeDBSGRO = new view_ExisteBDSGRO();
        verHistorial = new view_HistorialBDEjeciciones();
        tERPGrupoSistema = new tERPGrupoSistema();
        logEjecucionBD = new tLogEjecucionBD();
        ccSistemas = new cSistemas();
        tGrupo = new tERPGrupo();
    }

    /// <summary>
    /// Obtener el Nombre del Grupo  los Sistemas que Solicito
    /// </summary>
    /// <returns>List<view_PeticionSistemas></returns>
    public List<view_PeticionSistemas> obtenerSistemasSolicitados(int idReporte)
    {
        List<view_PeticionSistemas> lstObj = new List<view_PeticionSistemas>();
        var query = (from reporte in erp.view_PeticionSistemas
                     where reporte.idReporte == idReporte
                     select reporte).ToList();
        int contador = 0;
        if (query.Count > 0)
        {            
            foreach (var obj in query)
            {
                contador++;
                view_PeticionSistemas datos = new view_PeticionSistemas();
                datos.idReporte = obj.idReporte;
                if (contador.Equals(1))
                {
                    datos.idERPGrupo = obj.idERPGrupo;
                    datos.nomGrupo = obj.nomGrupo;
                }
                datos.idSistema = obj.idSistema;
                datos.nomSistema = obj.nomSistema;
                datos.imagen = obj.imagen;
                lstObj.Add(obj);
            }            
        }
        return lstObj;
    }

    /// <summary>
    /// Saber solo si existe la BDEMP para el Grupo
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>string</returns>
    public string existeDbEmpresa(int idReporte) {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");

        string resulNomGrupo = "";
        string nombreGrupo = "SELECT tERPGrupo.nomGrupo FROM tReporte INNER JOIN tERPGrupo ON tERPGrupo.idERPGrupo = tReporte.idERPGrupo WHERE tReporte.idReporte="+idReporte;        
        resulNomGrupo = sp.recuperaValor(nombreGrupo);
        string bdemp="DBEMP"+resulNomGrupo;

        string nomEsquema = "SELECT name FROM sys.databases WHERE name='"+bdemp+"'";        
        string res = "";
        res = sp.recuperaValor(nomEsquema);

        return res;
        
    }

    /// <summary>
    /// Generar el .bak de la base de datos de EMPDANA
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    public string generarBDEmpresa(string nomGrupo, int idReporte, int idUsuario, int idSistema) {
        return sp.generarBDEmpresa(nomGrupo, idReporte, idUsuario, idSistema);
    }

    /// <summary>
    /// Cambiar los esquemas de DBEMPDANA a los del nuevo Grupo
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    public string cambiarEsquemasBDEmpresa(string nomGrupo, int idReporte, int idUsuario)
    {
        return sp.cambiarEsquemasBDEmpresa(nomGrupo, idReporte,idUsuario);
    }

    /// <summary>
    /// Limpiar tablas de la nueva base de datos restaurada para su nueva configuración
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    public string limpiarBDEmpresa(string nomGrupo, int idReporte, int idUsuario)
    {
        Thread hilo = new Thread(() => sp.limpiarBDEmpresa(nomGrupo, idReporte, idUsuario));
        hilo.Start();
        //return sp.limpiarBDEmpresa(nomGrupo, idReporte, idUsuario);
        return "Esta tarea se ejecutará en segundo plano, le enviaremos un correo al terminar. °Correcto";
    }

    /// <summary>
    /// Saber si la base de datos de EMP existe y tambien saber en que 
    /// estatus se quedo la ejecucion para generar la BDEMP
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>List<view_ExisteBDEMP></returns>
    public List<view_ExisteBDEMP> existeEstatusDBEMP(int idReporte)
    {
        List<view_ExisteBDEMP> lstObj = new List<view_ExisteBDEMP>();
        var query = (from reporte in erp.view_ExisteBDEMP
                     where reporte.idReporte == idReporte
                     select reporte).ToList();
        if (query.Count > 0)
        {
            foreach (var obj in query)
            {
                view_ExisteBDEMP datos = new view_ExisteBDEMP();
                datos.existe = obj.existe;
                datos.idReporte = obj.idReporte;
                datos.idERPGrupo = obj.idERPGrupo;
                datos.nomGrupo = obj.nomGrupo;
                datos.idAccion = obj.idAccion;
                datos.idEstatus = obj.idEstatus;
                lstObj.Add(obj);
            }
        }
        return lstObj;
    }


    /// <summary>
    /// Saber si la base de datos de SGC existe y tambien saber en que 
    /// estatus se quedo la ejecucion para generar la BDSGC
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>List<view_ExisteBDSGC></returns>
    public List<view_ExisteBDSGC> existeEstatusDBSGC(int idReporte,int idSistema)
    {
        List<view_ExisteBDSGC> lstObj = new List<view_ExisteBDSGC>();
        var query = (from reporte in erp.view_ExisteBDSGC
                     where reporte.idReporte == idReporte && reporte.idSistema == idSistema
                     select reporte).ToList();
        if (query.Count > 0)
        {
            foreach (var obj in query)
            {
                view_ExisteBDSGC datos = new view_ExisteBDSGC();
                datos.existe = obj.existe;
                datos.idReporte = obj.idReporte;
                datos.idERPGrupo = obj.idERPGrupo;
                datos.nomGrupo = obj.nomGrupo;
                datos.idAccion = obj.idAccion;
                datos.idEstatus = obj.idEstatus;
                lstObj.Add(obj);
            }
        }
        return lstObj;
    }

    /// <summary>
    /// Saber si la base de datos de SGRO existe y tambien saber en que 
    /// estatus se quedo la ejecucion para generar la BDSGRO
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>List<view_ExisteBDSGRO></returns>
    public List<view_ExisteBDSGRO> existeEstatusDBSGRO(int idReporte, int idSistema)
    {
        List<view_ExisteBDSGRO> lstObj = new List<view_ExisteBDSGRO>();
        var query = (from reporte in erp.view_ExisteBDSGRO
                     where reporte.idReporte == idReporte && reporte.idSistema == idSistema
                     select reporte).ToList();
        if (query.Count > 0)
        {
            foreach (var obj in query)
            {
                view_ExisteBDSGRO datos = new view_ExisteBDSGRO();
                datos.existe = obj.existe;
                datos.idReporte = obj.idReporte;
                datos.idERPGrupo = obj.idERPGrupo;
                datos.nomGrupo = obj.nomGrupo;
                datos.idAccion = obj.idAccion;
                datos.idEstatus = obj.idEstatus;
                lstObj.Add(obj);
            }
        }
        return lstObj;
    }

    /// <summary>
    /// Saber si la base de datos de SGRO existe y tambien saber en que 
    /// estatus se quedo la ejecucion para generar la BDSGCE
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>List<view_ExisteBDSGCE></returns>
    public List<view_ExisteBDSGCE> existeEstatusDBSGCE(int idReporte, int idSistema)
    {
        List<view_ExisteBDSGCE> lstObj = new List<view_ExisteBDSGCE>();
        var query = (from reporte in erp.view_ExisteBDSGCE
                     where reporte.idReporte == idReporte && reporte.idSistema == idSistema
                     select reporte).ToList();
        if (query.Count > 0)
        {
            foreach (var obj in query)
            {
                view_ExisteBDSGCE datos = new view_ExisteBDSGCE();
                datos.existe = obj.existe;
                datos.idReporte = obj.idReporte;
                datos.idERPGrupo = obj.idERPGrupo;
                datos.nomGrupo = obj.nomGrupo;
                datos.idAccion = obj.idAccion;
                datos.idEstatus = obj.idEstatus;
                lstObj.Add(obj);
            }
        }
        return lstObj;
    }

    /// <summary>
    /// Ver el historial de ejecucución de las bases de datos solicitadas en un reporte de ERP
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>string</returns>
    public string verHistorialBDEjecucion(int idReporte)
    {
        string result = "";
        var lstReportes = (from reportes in erp.view_HistorialBDEjeciciones
                           where reportes.idReporte == idReporte && reportes.nomAccion !="Nueva Petición"
                           select reportes).ToList();

        if (lstReportes.Count > 0)
        {
            ControllerUsuarios cUsuarios = new ControllerUsuarios();
            result +=
                      "<table id='tblHistorial' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Usuario</th>" +
                            "<th>Acción</th>" +
                            "<th>Comentario</th>" +
                            "<th>Inicio Ejecución</th>" +
                            "<th>Termino Ejecución</th>" +
                            "<th>Estatus</th>" +
                            "<th>Tiempo Ejecución</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            foreach (view_HistorialBDEjeciciones t in lstReportes)
            {
                if (t.nomAccion !="Nueva Petición") {
                    result += "<tr id=" + t.idReporte + ">";
                    result += "<td>" + t.usuario + "</td>";
                    result += "<td>" + t.nomAccion + "</td>";
                    result += "<td>" + t.comentario + "</td>";
                    result += "<td>" + t.fechaEjecución + "</td>";
                    if (t.fechaTermino.ToString().Contains("1900"))
                    {
                        result += "<td>En espera</td>";
                    }
                    else
                    {
                        result += "<td>" + t.fechaTermino + "</td>";
                    }

                    if (t.nomEstatus.Equals("Correcto"))
                    {
                        result += "<td><a title='Correcto'><span id='icon-47' class='semaforo_verde'></span></a></td>";
                    }
                    else if (t.nomEstatus.Equals("Error"))
                    {
                        result += "<td><a title='Error'><span id='icon-47' class='semaforo_rojo'></span></a></td>";
                    }
                    else if (t.nomEstatus.Equals("Proceso"))
                    {
                        result += "<td><a title='En Proceso'><span id='icon-47' class='semaforo_amarillo'></span></a></td>";
                    }
                    if (t.tiempo.ToString().Contains("-"))
                    {
                        result += "<td><span class='glyphicon glyphicon-refresh glyphicon-refresh-animate icon-cargando' title='Calculando tiempo...'></td>";
                    }
                    else
                    {
                        result += "<td><a title='Tiempo de ejecución " + t.tiempo + " min'><span id='icon-47' class='historial verde'></span></a>" + t.tiempo + " min</td>";
                    }

                    result += "</tr>";
                }
            }

            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='7'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>";
        }
        else
        {
            result += "<div id=''><div align='center' style='font-size: 10px !important;margin-left: 25%;' class='bg-alert width50'><div style='width:10%' class='left'><span id='icon-25' class='blanco warning'></span></div><div style='width:90%; text-align: justify; padding-top:10px; padding-bottom:10px'>No hay información para mostrar.</div></div></div>°";
        }

        return result;

    }

    /// <summary>
    /// Generar el .bak de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>String</returns>
    public string generarBDSGC(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        return sp.generarBDSGC(nomGrupo, idReporte, idUsuario, idSistema);
    }

    /// <summary>
    /// Cambiar los esquemas de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    public string cambiarEsquemasBDSGC(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        return sp.cambiarEsquemasBDSGC(nomGrupo, idReporte, idUsuario, idSistema);
    }


    /// <summary>
    /// Limpiar tablas de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    public string limpiarBDSGC(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        Thread hilo = new Thread(() => sp.limpiarBDSGC(nomGrupo, idReporte, idUsuario, idSistema));
        hilo.Start();
        return "Esta tarea se ejecutara en segundo plano, le enviaremos un correo al terminar. °Correcto";
       // return sp.limpiarBDSGC(nomGrupo, idReporte, idUsuario, idSistema);
    }

    ////////
    /// <summary>
    /// Generar el .bak de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>String</returns>
    public string generarBDSGRO(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        return sp.generarBDSGRO(nomGrupo, idReporte, idUsuario, idSistema);
    }

    /// <summary>
    /// Cambiar los esquemas de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    public string cambiarEsquemasBDSGRO(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        return sp.cambiarEsquemasBDSGRO(nomGrupo, idReporte, idUsuario, idSistema);
    }


    /// <summary>
    /// Limpiar tablas de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    public string limpiarBDSGRO(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        Thread hilo = new Thread(() => sp.limpiarBDSGRO(nomGrupo, idReporte, idUsuario, idSistema));
        hilo.Start();
        return "Esta tarea se ejecutara en segundo plano, le enviaremos un correo al terminar. °Correcto";
        // return sp.limpiarBDSGC(nomGrupo, idReporte, idUsuario, idSistema);
    }


    ////////
    /// <summary>
    /// Generar el .bak de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>String</returns>
    public string generarBDSGCE(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        return sp.generarBDSGCE(nomGrupo, idReporte, idUsuario, idSistema);
    }

    /// <summary>
    /// Cambiar los esquemas de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    public string cambiarEsquemasBDSGCE(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        return sp.cambiarEsquemasBDSGCE(nomGrupo, idReporte, idUsuario, idSistema);
    }


    /// <summary>
    /// Limpiar tablas de la base de datos SGC
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns>string</returns>
    public string limpiarBDSGCE(string nomGrupo, int idReporte, int idUsuario, int idSistema)
    {
        Thread hilo = new Thread(() => sp.limpiarBDSGCE(nomGrupo, idReporte, idUsuario, idSistema));
        hilo.Start();
        return "Esta tarea se ejecutara en segundo plano, le enviaremos un correo al terminar. °Correcto";
        // return sp.limpiarBDSGC(nomGrupo, idReporte, idUsuario, idSistema);
    }

    /// <summary>
    /// Guardar la URL de cada sistema
    /// </summary>
    /// <param name="idGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idSistema"></param>
    /// <param name="url"></param>
    /// <param name="idUsuario"></param>
    /// <returns>bool</returns>
    public bool guardarURL(int idGrupo, int idReporte, int idSistema, string url, int idUsuario) {
        try {
            var reporte = (from tr in erp.tERPGrupoSistema
                           where tr.idReporte == idReporte && tr.idERPGrupo == idGrupo && tr.idSistema == idSistema
                           select tr).FirstOrDefault();

            var nomSistema = (from tr in erp.cSistemas
                              where  tr.idSistema == idSistema
                              select tr).FirstOrDefault();

            var nomGrupo = (from tr in erp.tERPGrupo
                              where tr.idERPGrupo == idGrupo
                              select tr).FirstOrDefault();

            reporte.URLSistema = url;
            erp.SubmitChanges();

            logEjecucionBD.idReporte = idReporte;
            logEjecucionBD.idSistema = idSistema;
            logEjecucionBD.idUsuario = idUsuario;
            logEjecucionBD.idAccion = 4;
            logEjecucionBD.fechaEjecución = DateTime.Now;
            logEjecucionBD.fechaTermino = DateTime.Now;
            logEjecucionBD.comentario = "Se guardo la URL correctamente para el sistema: "+ nomSistema.nomenglatura+" para el grupo: "+nomGrupo.nomGrupo;
            logEjecucionBD.idEstatus = 2;
            erp.tLogEjecucionBD.InsertOnSubmit(logEjecucionBD);
            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            logEjecucionBD.idReporte = idReporte;
            logEjecucionBD.idUsuario = idUsuario;
            logEjecucionBD.idAccion = 4;
            logEjecucionBD.fechaEjecución = DateTime.Now;
            logEjecucionBD.fechaTermino = DateTime.Now;
            logEjecucionBD.comentario = "Error al guardar la url. Error: "+e.Message;
            logEjecucionBD.idEstatus = 3;
            erp.tLogEjecucionBD.InsertOnSubmit(logEjecucionBD);
            erp.SubmitChanges();
            return false;
        }
    }

    /// <summary>
    /// Guardar URL del SGI
    /// </summary>
    /// <param name="idGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idSistema"></param>
    /// <param name="url"></param>
    /// <param name="idUsuario"></param>
    /// <returns>bool</returns>
    public bool guardarSGI(int idGrupo, int idReporte, string url, int idUsuario) {
        try
        {
            var objeto = (from tr in erp.tERPGrupo
                           where tr.idERPGrupo == idGrupo
                           select tr).FirstOrDefault();
            var nomGrupo = (from tr in erp.tERPGrupo
                            where tr.idERPGrupo == idGrupo
                            select tr).FirstOrDefault();
            objeto.urlERP = url;
            erp.SubmitChanges();

            logEjecucionBD.idReporte = idReporte;
            //logEjecucionBD.idSistema = idSistema;
            logEjecucionBD.idUsuario = idUsuario;
            logEjecucionBD.idAccion = 5;
            logEjecucionBD.fechaEjecución = DateTime.Now;
            logEjecucionBD.fechaTermino = DateTime.Now;
            logEjecucionBD.comentario = "Se genero correctamente el SGI para el grupo: " + nomGrupo.nomGrupo;
            logEjecucionBD.idEstatus = 2;
            erp.tLogEjecucionBD.InsertOnSubmit(logEjecucionBD);
            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            logEjecucionBD.idReporte = idReporte;
            //logEjecucionBD.idSistema = idSistema;
            logEjecucionBD.idUsuario = idUsuario;
            logEjecucionBD.idAccion = 5;
            logEjecucionBD.fechaEjecución = DateTime.Now;
            logEjecucionBD.fechaTermino = DateTime.Now;
            logEjecucionBD.comentario = "Error al generar el SGI: " + e.Message;
            logEjecucionBD.idEstatus = 3;
            erp.tLogEjecucionBD.InsertOnSubmit(logEjecucionBD);
            erp.SubmitChanges();
            return false;
        }
    }


    /// <summary>
    /// Cargar Configuración Inicial para Empresa
    /// </summary>
    /// <param name="nomGrupo"></param>
    /// <param name="idGrupo"></param>
    /// <param name="idReporte"></param>
    /// <param name="idUsuario"></param>
    /// <returns></returns>
    public string configuracionInicial(string nomGrupo, int idGrupo,int idReporte, int idUsuario,string lstSistemas)
    {
        return sp.configuracionInicial(nomGrupo, idGrupo,idReporte, idUsuario, lstSistemas);
    }
}