using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ControllerUsuarios
/// </summary>
public class ControllerUsuarios
{
    private ERPManagementDataContext erp;

	public ControllerUsuarios()
	{
        erp = new ERPManagementDataContext();
	}

    /// <summary>
    /// Método para devolver el nombre del usuario 
    /// por ID.
    /// </summary>
    /// <param name="idReporte"></param>
    /// <returns>Nombre del Usuario</returns>
    public string getUsuarioById(int idUsuario)
    {
        var usuario = (from vUsu in erp.vUsuariosERPM
                           where vUsu.idEmpleado.Equals(idUsuario.ToString())
                           select vUsu.nombreCompleto).FirstOrDefault();

        return usuario;
    }

    /// <summary>
    /// Método para generar tabla de Usuarios
    /// en ERP Management
    /// </summary>
    /// <returns>Tabla de Usuarios construida</returns>
    public string getTblUsuarios()
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        List<string> lstusuarios = new List<string>();
        string query = "Select nombreCompleto, idEmpleado, tipoUsuario from vUsuariosERPM";
        lstusuarios = sp.recuperaRegistros(query);
        int cantidad = lstusuarios.Count;

        string result = "";
        char c = '"';
        if (cantidad > 0)
        {
            result += "<fieldset>" +
                        "<legend><span id='icon-25' class='usuarios blanco'>Permisos por usuario</span></legend>" +
                      "<table id='tblUsuarios' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Usuario</th>" +
                            "<th>Puesto</th>" +
                            "<th>Permisos</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            for (int i = 0; i < cantidad; i+=3 )
            {
                result += "<tr>";
                result += "<td>" + lstusuarios[i] + "</td>";
                if (lstusuarios[i + 2] != "")
                {
                    result += "<td>" + lstusuarios[i + 2] + "</td>";
                }
                else {
                    result += "<td> N/A </td>";
                }
                
                result += "<td><a title='Roles' onclick='javascript:getRolesByUsuario(" + lstusuarios[i + 1] + ", " + c + "" + lstusuarios[i] + "" + c + ",2);'><span id='icon-25' class='permisos verde'></span></a></td>";
                result += "</tr>";
            }


            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='3'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>" +
                      "</fieldset>";
        }

        return result;
    }

    /// <summary>
    /// Método para generar tabla de Puestos de Usuarios
    /// en ERP Management
    /// </summary>
    /// <returns>Tabla de Puestos de Usuarios construida</returns>
    public string getTblPuestoUsuarios()
    {
        storedProcedure sp=new storedProcedure("DBSGICEConnectionString");
        List<string> lstpuestos =new List<string>();
        string query = "select Distinct(idPuesto),TipoUsuario from vusuariosERPM";

        lstpuestos=sp.recuperaRegistros(query);

        int cantidad=lstpuestos.Count;
        string result = "";

        char c = '"';
        if (cantidad > 0)
        {
            result += "<fieldset>" +
                        "<legend><span id='icon-25' class='roles blanco'>Permisos por puesto</span></legend>" +
                      "<table id='tblPuestoUsuarios' class='data_grid display'>" +
                        "<thead id='grid-head2'>" +
                            "<th>Puesto</th>" +
                            "<th>Permisos</th>" +
                        "</thead>" +
                        "<tbody id='grid-body'>";

            for (int i = 0; i < cantidad; i+=2)
            {
                if (lstpuestos[i] != "" || lstpuestos[i + 1] != "")
                {
                    result += "<tr>";
                    result += "<td>" + lstpuestos[i+1] + "</td>";
                    result += "<td><a title='Roles' onclick='javascript:getRolesByPuesto(" + lstpuestos[i] + ", " + c + "" + lstpuestos[i+1] + "" + c + ",1);'><span id='icon-25' class='permisos verde'></span></a></td>";
                    result += "</tr>";
                }
            }


            result += "</tbody>" +
                        "<tfoot>" +
                            "<tr>" +
                                "<td class='foot shadow3 round-down2' colspan='2'>&nbsp;</td>" +
                            "</tr>" +
                        "</tfoot>" +
                        "</table>" +
                      "</fieldset>";
        }

        return result;
    }
}