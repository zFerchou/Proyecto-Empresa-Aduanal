<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sistemas.aspx.cs" Inherits="sistemas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="UTF-8">
	<title>Seleccionar Sistemas</title>

    <link href="css/styles.css" rel="stylesheet" type="text/css" />
    <link href="css/csserp/sistemas.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui.js" type="text/javascript"></script>
    <script src="js/sistemas/sistemas.js" type="text/javascript"></script>
    
    <script src="js/screenBlock.js" type="text/javascript"></script>
    <script src="js/jquery.blockUI.js" type="text/javascript"></script>
</head>
<body>
<!---- header ---->
<form id="form" runat="server">
<div id="header-layer">
	<div class="head">
		<img src="images/logos/logo.png" alt="NAD GLOBAL - logo" class="logo-sistema"><asp:LoginName
      ID="LoginName1" runat="server" />
		<ul id="nav-top">
			<li class="username"><span id="icon-25" class="usuario_logueado"></span><asp:Label ID="lblUsuario" runat="server" ></asp:Label></li>
			<li class="logout"><asp:LinkButton runat="server" ID="linkCerrarSesion" onclick="linkCerrarSesion_Click"><span id="icon-25" class="cerrar_sesion animation-ico">Cerrar Sesión</span></asp:LinkButton></li>
		</ul>
	</div>
</div>

<!---- Termina Header ---->

<!---- Panel de opciones de sistemas ---->
<div id="content-layer">
	    <div class="content">
		    <span id="icon-25" class="ayuda" title="Seleccione los módulos requeridos" style="cursor:help">Ayuda</span>
		    <div id="reporte">
			    

                    <div style="padding-left:50px">
                        <label class="bold text20">Grupo:</label><br />
		                <input class="input200" id="txtGrupo" type="text" runat="server"  onkeyup="mayusculas()"/>
                        
                        <!---- input agregado por GDPE---->
                        <input style="visibility:hidden;" id="txtIdGrupo" type="text" />
                        <!---- FIN ---->
                    </div>

				    <div id="divmdls"  align="center" class="grid">
                          <asp:Label runat="server"  align="center" ID="lblModulos"></asp:Label>
				    </div>

				    <input type="button" class="btn verde width20 shadow2 borde-blanco" id="btn-aceptar" value="ACEPTAR" onclick="javascript:confirmar()"/>
			    	
		    </div>

	    </div>
    </div>

    <div id="dConf" title="Confirmar"><p>¿Está seguro?</p>
        <div align="right">
        <input type="button" class="btn verde width30 shadow2 borde-blanco" id="btnCA" value="ACEPTAR" onclick="javascript:insertar()"/>
        <input type="button" class="btn blanco width30 shadow2 borde-blanco" id="btnCV" value="VERIFICAR" onclick="javascript:cerrarDialog(0)"/>
        </div>
    </div>

    <div id="dMsj" title="Notificación"><p id="textoMsj"></p>
        <div align="right">
        <input type="button" class="btn verde width30 shadow2 borde-blanco" id="btnM" value="ACEPTAR" onclick="javascript:cerrarDialog(1)"/>
        </div>
    </div>

    <!----DIALOG---->

        <div id="dNotificacion" class="hide" title="Notificación">
                   <p align="center">
                   
                   Has elegido todos los sistemas, intenta con otro grupo.
                   </p>
                   
                   <div align="center"> 
                        <a  onclick="javascript:cerrarDialogSist();"><label class="btn verde" style="Height:30px; Width:85px;">Aceptar</label></a>
                   </div>
        </div>
         <!---- DIALOG---->

</form>
<!---- Temrina Panel de opciones de sistemas ---->

<!---- Footer ---->
<div id="footer-layer">
	<ul>
		<li class="rights"><p>NAD Global 2013. ©Todos los derechos reservados.</p></li>
		<li class="logos"><img src="images/foot/img-logolca.png" alt="logo-lca"><img src="images/foot/img-logonad.png" alt="logo-nad"></li>
	</ul>
</div>
<!---- Termina Footer ----->


</body>
</html>
