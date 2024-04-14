using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ControllerPrioridad
/// </summary>
public class ControllerPrioridad
{
    private ERPManagementDataContext erp;

	public ControllerPrioridad()
	{
        erp = new ERPManagementDataContext();
	}

    public List<cPrioridadReporte> getPrioridad()
    {
        var lstPrioridad = (from cpr in erp.cPrioridadReporte
                            select cpr).ToList();
        return lstPrioridad;
    }
}