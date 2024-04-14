using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ControllerResponsableReporte
/// </summary>
public class ControllerResponsableReporte
{
    /// <summary>
    /// Atributos necesarios para las acciones
    /// dentro del controlador de tResponsableReporte.
    /// </summary>
    private cTipoResponsable tipoResponsable;    
    private tResponsableReporte responsableReporte;
    private ERPManagementDataContext erp;
    private view_Responsables viewApoyo;

    /// <summary>
    /// Método constructor, inicializa las instancias
    /// del data context y un objeto.
    /// </summary>
    public ControllerResponsableReporte()
	{
        erp = new ERPManagementDataContext();
        responsableReporte = new tResponsableReporte();
        tipoResponsable = new cTipoResponsable();
        viewApoyo = new view_Responsables();
	}

    //Obtener a las personas que estan como Apoyo en un reporte y mostrarlas en una tabla.
    public string tblPersonalApoyo(int idReporte)
    {
        string result = "";
        var lstResponsableReporte = (from viewApoyo in erp.view_Responsables
                                     where viewApoyo.idTipoResponsable == 2 && viewApoyo.idReporte == idReporte
                                     select viewApoyo
                                     ).ToList();
        var getEstatus = (from reportes in erp.tReporte where reportes.idReporte== idReporte select reportes).First();
        if (lstResponsableReporte.Count > 0)
        {
            if (getEstatus.idEstatusReporte != 5)
            {
                result += "</br></br></br></br><table id='tblApoyo' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Nombre</th>" +
                            "<th>Eliminar</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";
            }
            else {
                //result += "<table id='tblApoyo' class='data_grid display'>" +
                //       "<thead id='grid-head2'>" +
                //           "<th>Nombre</th>" +
                //       "</thead>" +
                //       "<tbody id='grid-body'>";
                
            }
            foreach (view_Responsables t in lstResponsableReporte)
            {
                if (t.idEstatusReporte != 5)
                {
                    result += "<tr>";
                    result += "<td>" + t.nombreCompleto + "</td>";
                    result += "<td>" +
                          "<a onclick='javascript:eliminarPersonaApoyo(" + t.idReporte + "," + t.idResponsable + ");'>" +
                          "<span id='icon-25' class='eliminar rojo eliminar-apoyo' title='Eliminar'></span>" +
                          "</a>" +
                          "</td>";
                    result += "</tr>";
                    result += "</tbody>" + "</table>";
                }
                else {
                    result += "<p class='txt-verde'>"+t.nombreCompleto+"</p>";
                }
               
            }
           
        }
        else {
            result += "</br></br></br></br><div align='center' style='font-size: 10px !important;' class='bg-alert width50'><div style='width:10%' class='left'><span id='icon-25' class='blanco warning'></span></div><div style='width:90%; text-align: justify; padding-top:10px; padding-bottom:10px'>No hay Personal de Apoyo para este Reporte.</div></div>";
        }
        return result;
    }

    //Obtener a la persona que estan como Responsable en un reporte.
    public string tblPersonalResponsable(int idReporte)
    {
        string result = "";
        var lstResponsableReporte = (from viewApoyo in erp.view_Responsables
                                     where viewApoyo.idTipoResponsable == 1 && viewApoyo.idReporte == idReporte
                                     select viewApoyo
                                     ).ToList();
        var getEstatus = (from reportes in erp.tReporte where reportes.idReporte == idReporte select reportes).First();
        if (lstResponsableReporte.Count > 0)
        {
            if (getEstatus.idEstatusReporte != 5)
            {
                result += "<table id='tblResponsables' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Nombre</th>" +
                            "<th>Eliminar</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";
            }
            else {

                //result += "<table id='tblResponsables' class='data_grid display'>" +
                //        "<thead id='grid-head2'>" +
                //            "<th>Nombre</th>" +
                            
                //        "</thead>" +
                //        "<tbody id='grid-body'>";
            }
            foreach (view_Responsables t in lstResponsableReporte)
            {

                if (t.idEstatusReporte != 5)
                {
                    result += "<tr>";
                    result += "<td>" + t.nombreCompleto + "</td>";
                    result += "<td>" +
                          "<a onclick='javascript:eliminarPersonaResponsable(" + t.idReporte + "," + t.idResponsable + ");'>" +
                          "<span id='icon-25' class='eliminar rojo eliminar-apoyo' title='Eliminar'></span>" +
                          "</a>" +
                          "</td>";
                    result += "</tr>";
                }
                else {
                    result += "<p class='txt-verde'>" + t.nombreCompleto + "</p>";
                }
                
            }
        }
        else
        {
            result += "<div align='center' style='font-size: 10px !important;' class='bg-alert width50'><div style='width:10%' class='left'><span id='icon-25' class='blanco warning'></span></div><div style='width:90%; text-align: justify; padding-top:10px; padding-bottom:10px'>No hay Responsables para este Reporte.</div></div>";
        }
        return result;
    }

    //**************Obtener a la persona que estan como Responsable en un tooltipResponsables.
    public string obtenerResponsableNuevo(int idReporte)
    {
        string result = "";
        var lstResponsableReporte = (from viewApoyo in erp.view_Responsables
                                     where viewApoyo.idTipoResponsable == 1 && viewApoyo.idReporte == idReporte
                                     select viewApoyo
                                     ).ToList();
        var getEstatus = (from reportes in erp.tReporte where reportes.idReporte == idReporte select reportes).First();
        if (lstResponsableReporte.Count > 0)
        {

            foreach (view_Responsables t in lstResponsableReporte)
            {
                if (t.idEstatusReporte==5)
                {
                    result += "<label>"+t.nombreCompleto+"</label>";
                }
                else if (t.idEstatusReporte==1)
                {
                    result += "<label>"+t.nombreCompleto+"</label>";
                }
                else if (t.idEstatusReporte == 3 || t.idEstatusReporte == 2)
                {
                    result += "<div class='listadoConsulta' style='padding-left: 5%'>" + t.nombreCompleto + "<div style='float: right'><a id=''><span id='icon-25' class='verde editar btnModificarRes' title='Modificar'></span></a></div></div>";
                }               
            }

        }
        else
        {
            result += "<label>No existen responsable</label>";
        }
        return result;
    }
    public string obtenerApoyoNuevo(int idReporte)
    {
        string result = "";
        var lstResponsableReporte = (from viewApoyo in erp.view_Responsables
                                     where viewApoyo.idTipoResponsable == 2 && viewApoyo.idReporte == idReporte
                                     select viewApoyo
                                     ).ToList();
        var getEstatus = (from reportes in erp.tReporte where reportes.idReporte == idReporte select reportes).First();
        if (lstResponsableReporte.Count > 0)
        {

            foreach (view_Responsables t in lstResponsableReporte)
            {
                if (t.idEstatusReporte == 5)
                {
                    result += "<label>" + t.nombreCompleto + "</label>";
                }
                else if (t.idEstatusReporte == 1)
                {
                    result += "<label>" + t.nombreCompleto + "</label>";
                }
                else if (t.idEstatusReporte == 3 || t.idEstatusReporte == 2)
                {
                    result += "<div class='listadoConsulta' style='padding-left: 5%'>" + t.nombreCompleto + "<div style='float: right'><a id=''><span  id='icon-25' class='verde eliminar btnEliminarApoyo' title='Eliminar' onclick='eliminarPersonaApoyo(" + t.idReporte + "," + t.idResponsable + ")'></span></a></div></div>";
                }               
                //result += "<div id='divLstresponsable' class='listadoConsultaResponsable' style='padding-left: 2%; font-color='red'>"+t.nombreCompleto+"<div style='float: right'></div></div>";
                //result += "<div class='listadoConsulta' style='padding-left: 5%'>" + t.nombreCompleto + "<div style='float: right'><a id=''><span  id='icon-25' class='verde eliminar btnEliminarApoyo' title='Eliminar' onclick='eliminarPersonaApoyo("+t.idReporte+","+t.idResponsable+")'></span></a></div></div>";
            }

        }
        else
        {
            result += "<label>No existe personal de apoyo <span id='icon-25' class='verde agregar btnAddApoyo' title='Agregar Apoyo'></span></label>";
        }
        return result;
    }

    //Guardar a las personas que van a estar como Apoyo en un Reporte
    public bool guardarPersonaApoyo(int idReporte, int idResponsable)
    {
        try
        {
            responsableReporte = new tResponsableReporte();
            responsableReporte.idReporte = idReporte;
            responsableReporte.idResponsable = idResponsable;
            responsableReporte.idTipoResponsable = 2;
            erp.tResponsableReporte.InsertOnSubmit(responsableReporte);
            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;            
        }
    }

    //Guardar a las personas que van a estar como Apoyo en un Reporte
    public bool guardarPersonaResponsable(int idReporte, int idResponsable)
    {
        try
        {
            responsableReporte = new tResponsableReporte();
            responsableReporte.idReporte = idReporte;
            responsableReporte.idResponsable = idResponsable;
            responsableReporte.idTipoResponsable = 1;
            erp.tResponsableReporte.InsertOnSubmit(responsableReporte);
            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    //Eliminar a las personas que están como Apoyo en un Reporte
    public bool eliminarPersonaApoyo(int idReporte, int idResponsable)
    {
        try
        {            
            var registro = (from responsableReporte in erp.tResponsableReporte
                            where responsableReporte.idResponsable == idResponsable && responsableReporte.idReporte==idReporte
                            select responsableReporte).Single();

            erp.tResponsableReporte.DeleteOnSubmit(registro);
            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    //Eliminar a las personas que están como Responsable en un Reporte
    public bool eliminarPersonaResponsable(int idReporte, int idResponsable)
    {
        try
        {
            var registro = (from responsableReporte in erp.tResponsableReporte
                            where responsableReporte.idResponsable == idResponsable && responsableReporte.idReporte == idReporte
                            select responsableReporte).Single();

            erp.tResponsableReporte.DeleteOnSubmit(registro);
            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    //Modificar a las personas que están como Responsable en un Reporte
    public bool editarPersonaResponsable(int idReporte, int idResponsable)
    {
        try
        {
            var registro = (from responsableReporte in erp.tResponsableReporte
                            where responsableReporte.idReporte == idReporte
                            select responsableReporte).SingleOrDefault();

            registro.idResponsable = idResponsable;

            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    /// <summary>
    /// Métdodo para devolver el nombre del responsable 
    /// por reporte.
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>Nombre del responsable de reporte</returns>
    public string getResponsableByReporte(int idReporte)
    {
        var responsable = (from view_resp in erp.view_Responsables
                          where view_resp.idReporte == idReporte && view_resp.idTipoResponsable == 1
                          select view_resp).SingleOrDefault();

        return responsable.nombreCompleto;
    }
}