using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ControllerERPGrupo
/// </summary>
public class ControllerERPGrupo
{
    ERPManagementDataContext erp;

    public ControllerERPGrupo()
    {
        erp = new ERPManagementDataContext();
    }

    /// <summary>
    /// Método para asignar items al DropDownList ddlERPGrupo
    /// </summary>
    /// <returns>Lista de objetos tERPGrupo </returns>
    public List<tERPGrupo> getERPGrupo()
    {
        var lstERPGrupo = (from terp in erp.tERPGrupo select terp).OrderBy(o => o.nomGrupo).ToList();
        //var lstERPGrupo = (from terp in erp.vGrupos
        //                   select terp).ToList();

        return lstERPGrupo;
    }
}