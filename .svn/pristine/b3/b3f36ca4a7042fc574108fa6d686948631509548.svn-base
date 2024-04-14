<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="index" %>

<%--<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ERP Management</title>
    <link rel="icon" type="image/png" href="images/logos/favicon-nad.png" />
    <link href="css/Sistema/jquery-ui.css" rel="stylesheet" />
    <link href="css/Sistema/styles.css" rel="stylesheet" type="text/css" />
    <link href="css/Sistema/alertify.core.css" rel="stylesheet" />
    <link href="css/Sistema/alertify.default.css" rel="stylesheet" />

    <script src="js/Sistema/jquery-1.11.1.min.js"></script>
    <script src="js/Sistema/jquery-ui.js"></script>
    <script src="js/Sistema/alertify.min.js"></script>
    <script src="js/Sistema/jquery.blockUI.js"></script>
    <script src="js/Sistema/screenBlock.js"></script>
    <script src="js/index.js"></script>
    <script src='https://www.google.com/recaptcha/api.js'></script>
</head>
<body>
    <div class="width30 center">
        <figure>
            <img src="images/logos/logo.png" alt="NAD Global" class="" />
        </figure>
    </div>
    <div class="alert-layer width30" runat="server">
        <div class="form-tit round-border3">
            <h3 class="">INGRESAR</h3>
        </div>
        <form action="" method="post" id="frm" runat="server">
            <div id="divInicio">
                <label for="txtNomUsuario">Usuario:</label>
                <br />
                <asp:TextBox ID="txtNomUsuario" type="text" runat="server" class="inputP98" placeholder="Nombre del Usuario" />
                <br />
                <label for="txtContrasena">Contraseña:</label><br />
                <asp:TextBox ID="txtContrasena" type="password" runat="server" class="inputP98" placeholder="*********" /><br />
                <%--<asp:Button ID="btnAceptar" runat="server" Text="ACEPTAR" class="btn verde width100 shadow2 borde-blanco center" OnClick="btnAceptar_Click" /><br />--%>
                <input type="button" class="btn verde width100 shadow2 borde-blanco center" id="btnAceptar" value="ENTRAR"/><br />
                <div id="exitoEntro" class="bg-success width98 left hide">
                    <span id="icon-47" class="blanco success">
                        <asp:Label ID="lblExito" runat="server" Text="Autentificacion exitosa."></asp:Label></span>
                </div>
                <div id="errorLogin" class="bg-alert width98 left hide">
                    <span id="icon-47" class="blanco error">
                        <asp:Label ID="lblErrorAutentificacion" runat="server"></asp:Label></span>
                </div>
                <%-- <recaptcha:RecaptchaControl
                ID="recaptcha"
                runat="server"
                PublicKey="6LcSsSUTAAAAAECZQSB0Aa0TaB4H88bzwq0LIsU8"
                PrivateKey="6LcSsSUTAAAAAMlYuWuVgj7PcaTM2y3oxve6FxMl"
            />--%>
            </div>
            <div id="divNewCaptcha">
                <label for="txtUsuario">Usuario:</label><br />
                <asp:TextBox ID="txtUsuario" type="text" runat="server" class="inputP98" />
                <div id="divImagenCaptcha">
                    <img src="GenerateCaptcha.ashx" />
                </div>
                <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div id="divCaptcha">
                            <asp:Label ID="lblNewCaptcha" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div id="divTextCaptcha">                           
                            <asp:TextBox ID="txtCaptchaText" runat="server" CssClass="inputP98" placeholder="Ingresa el código de la imágen"></asp:TextBox>
                        </div>

                        <asp:Button ID="btnReGenerate" runat="server" Text="OTRO CÓDIGO" OnClick="btnReGenerate_Click" CssClass="btn azul"/>
                        <asp:Button ID="btnSubmitCaptcha" runat="server" Text="ACEPTAR" OnClick="btnSubmitCaptcha_Click" CssClass="btn verde"/><br />
                        <asp:Label ID="lblErrorCaptcha" runat="server" Text="" Visible="false"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
            <div class="clear">
                <br />
            </div>

            <asp:Label ID="lblError" runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblWait" runat="server" Text="" Visible="false"></asp:Label>
        </form>
        <a id="btnRecuContraseña" href="javascript:">Olvide mi contraseña</a><a id="btnCancelar" href="javascript:"> Regresar</a>

        <%--<div class="g-recaptcha" data-sitekey="6LcSsSUTAAAAAECZQSB0Aa0TaB4H88bzwq0LIsU8"></div>--%>
    </div>
    <%--<div class="g-recaptcha" data-theme="light" data-sitekey="6LcSsSUTAAAAAECZQSB0Aa0TaB4H88bzwq0LIsU8" style="transform:scale(0.77);transform-origin:0 0"></div>--%>
    
    <%-- DIALOG CAMBIAR PASSWORD --%>
    <div class="hide" id="dlogNewPassword" title="Cambiar Contraseña">
        <p id="frm">Por seguridad y comodidad, te pedimos que cambies la contraseña que te enviamos a tu correo por una personal que incluya letras y números.</p>
        <div class="clear width98 txt-right center">
             <div class="left width80">
                <label id="lblPassword1" for="txtNewPassword1" class="left">Nueva Contraseña:</label> <br />   
                <input type="password" id="txtNewPassword1" class="width90 left"/>
            </div>
            <div id="divIcon1" class="left width20 txt-left">
                <span id="icon-25" class="success verde iconSuccess1 hide"></span>
                <span id="icon-25" class="error rojo hide iconError1"></span>
            </div>
            <br /> 
            
        </div>
        <div class="clear width98 txt-right center">
            <div class="left width80">
                <label id="lblPassword2" for="txtNewPassword2" class="left">Repetir Contraseña:</label> <br />      
                <input type="password" id="txtNewPassword2" class="width90 left"/>
            </div> 
            <div id="divIcon2" class="left width20 txt-left">
                <span id="icon-25" class="success verde iconSuccess2 hide"></span>
                <span id="icon-25" class="error rojo hide iconError2" title="No coincide la contraseña"></span>
            </div>
        </div><br />
        <div id="divError" class="bg-alert width50 left hide">
            <span id="icon-47" class="blanco warning mensajeError"></span>
       </div>
        <div class="clear width98 txt-right center">
            <input type="button" class="btn verde" id="btnSi" value="ACEPTAR"/>
            <input type="button" class="btn blanco" id="btnNo" onclick="javascript: $('#dlogNewPassword').dialog('close');" value="CANCELAR"/>            
        </div>
       
    </div>
    <%-- FIN DIALOG CAMBIAR PASSWORD --%>
</body>
</html>
