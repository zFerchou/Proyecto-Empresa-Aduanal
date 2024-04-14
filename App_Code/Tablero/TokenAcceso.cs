using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;

public class TokenAcceso
{
    public TokenAcceso()
    {
    }

    public int iCodigo { get; set; }
    public string sMensaje { get; set; }
    public string sToken { get; set; }

}



