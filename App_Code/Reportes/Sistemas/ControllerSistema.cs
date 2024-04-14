using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ControllerSistema
/// </summary>
public class ControllerSistema
{
    ERPManagementDataContext erp;

	public ControllerSistema()
	{
        erp = new ERPManagementDataContext();
	}

    public List<cSistemas> getSistemas()
    {
        var sistemasERP = new int[] { 1, 2, 3, 4, 5, 10, 11, 18 };//ID de sistemas que puede contener un ERP
        var lstSistemas = (from cs in erp.cSistemas
                           where sistemasERP.Contains(cs.idSistema)
                           select cs).ToList();
        return lstSistemas;
    }

    public string getSistemasGrupo(int gpo)
    {
        string result = "<label class='frmReporte'>Sistemas:</label><br/><select id='ddlSistemaG' class='validaCombo select inputP98'>";

        var sistemasERP = new int[] { 1, 2, 3, 4, 10, 11, 18, 20, 22, 21,23,24,25,26,27,28,29,30,31,32,33,34,35,36};//ID de sistemas que puede contener un ERP
        var lstSistemas = (from vsg in erp.vSistemasGrupo
                           where sistemasERP.Contains(vsg.idSistema) && vsg.idERPGrupo==gpo
                           select vsg).ToList();
        result+="<option value='0' disabled selected='true'>Seleccione una opción</option>";
        foreach (vSistemasGrupo sist in lstSistemas)
        {
            result += "<option value="+sist.idSistema+">"+ sist.nomSistema+"</option>";
        }
        result+="</select><br>";

        return result;
    }

    public string getSistemasIdGrupo(int gpo, int sistema)
    {
        string result = "<label class='txt-verde frm'>Sistemas:</label><br/><select id='ddlSistemaG' class='validaCombo select inputP98'>";

        var sistemasERP = new int[] { 1, 2, 3, 4, 10, 11, 18, 20, 22, 21, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36 };//ID de sistemas que puede contener un ERP
        var lstSistemas = (from vsg in erp.vSistemasGrupo
                           where sistemasERP.Contains(vsg.idSistema) && vsg.idERPGrupo == gpo
                           select vsg).ToList();
        
        foreach (vSistemasGrupo sist in lstSistemas)
        {
            if(sistema == sist.idSistema)
            {
                result += "<option value=" + sist.idSistema + " selected>" + sist.nomSistema + "</option>";
            }
            else
            {
                result += "<option value=" + sist.idSistema + ">" + sist.nomSistema + "</option>";
            }
            
        }
        result += "</select><br>";

        return result;
    }
}