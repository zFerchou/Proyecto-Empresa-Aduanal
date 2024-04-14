using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ControllerTiposReportes
/// </summary>
public class ControllerTiposReportes
{
    private ERPManagementDataContext erp;

	public ControllerTiposReportes()
	{
        erp = new ERPManagementDataContext();
	}

    public List<cTipoReporte> getTipoReporte()
    {
        var lstTipoReporte = (from ctr in erp.cTipoReportes
                              //where ctr.idTipoReporte!=3
                              select ctr).ToList();

        return lstTipoReporte;

    }
}