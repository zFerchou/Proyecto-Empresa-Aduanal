$(document).ready(function () {
    $('#divNewCaptcha').hide();
    $('#divCaptcha').hide();
    $('#btnCancelar').hide();
    $("#dlogRecuContraseña").dialog({
        modal: true,
        autoOpen: false,
        height: 400,
        width: 500,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
        },
        open: function (event, ui) {
        }
    });

    $('#btnRecuContraseña').click(function (e) {
        e.preventDefault();
        $('#divInicio').hide('slow');
        $('#divNewCaptcha').show('slow'); 
        $('#btnAceptar').show();         
        $('#btnCancelar').show();
        $('#errorLogin').addClass('hide');
        $(this).hide();
    });

    //Cerrar Dialog del Detalle del Reporte
    $('#btnNo').click(function (e) {
        e.preventDefault();
        $('#dlogRecuContraseña').dialog("close");
    });
    $('#btnReGenerate').click(function () {
        $('#divImagenCaptcha').remove();
        $('#divCaptcha').show();
        $('#divImagenCaptcha').hide('slow');
        $('#divSuccess').addClass('hide');
        $('#divError').addClass('hide');
    });
    $('#btnReGenerate').click(function () {
        $('#divImagenCaptcha').remove();
    });
    $('#btnSubmitCaptcha').click(function () {
        if ($.trim($('txtCaptchaText').val() =='')) {
            $('#divImagenCaptcha').remove();
            $('#divSuccess').removeClass('hide');
            $('#divError').removeClass('hide');
        }
    });

    $('#btnCancelar').click(function () {
        $('#divNewCaptcha').hide('slow');
        $('#divInicio').show('slow');
        $('#btnCancelar').hide();
        $('#btnRecuContraseña').show();
        $('#divSuccess').addClass('hide');
        $('#divError').addClass('hide');
    });

    $("#dlogNewPassword").dialog({
        modal: true,
        autoOpen: false,
        height: 255,
        width: 600,
        resizable: false,
        draggable: false,
        show: "clip",
        hide: "clip",
        close: function (event, ui) {
            $('.iconSuccess1').addClass('hide');
            $('.iconSuccess2').addClass('hide');

            $('.iconError1').addClass('hide');
            $('.iconError2').addClass('hide');
        },
        open: function (event, ui) {
            //VALIDAR SI LOS TXT DE CONTRASEÑAS SON IGUALES
            $('#txtNewPassword2,#txtNewPassword1').each(function () {
                var elem = $(this);
                // Save current value of element
                elem.data('oldVal', elem.val());
                // Look for changes in the value 
                elem.bind("propertychange keyup input paste", function (event) {
                    var txtContrasenia1 = $('#txtNewPassword1');
                    var txtContrasenia2 = $('#txtNewPassword2');
                    if (txtContrasenia2.val() == txtContrasenia1.val()) {
                        $('.iconSuccess1').removeClass('hide');
                        $('.iconSuccess2').removeClass('hide');
                        $('.iconError2').addClass('hide');
                    } else {
                        $('.txtNewPassword2').focus();
                        $('.iconError2').removeClass('hide');
                        $('.iconSuccess2').addClass('hide');
                        txtContrasenia1.attr('placeholder', 'Ingresa una contraseña');
                    }
                    if (txtContrasenia1.val() == '') {
                        $('.iconError1').removeClass('hide');
                        $('.iconSuccess1').addClass('hide');
                    }
                    if (txtContrasenia2.val() == '') {
                        $('.iconError2').removeClass('hide');
                        $('.iconSuccess2').addClass('hide');
                        txtContrasenia2.attr('placeholder', 'Ingresa una contraseña');
                    }
                });
            });

        }
    });
    //Fin elementos para captcha
    $("#btnAceptar").click(function (e) {
        e.preventDefault();
        entrar();
    });
});


function entrar() {
    block();
    
    if ($("#txtNomUsuario").val() == "" || $("#txtContrasena").val() == "") {
        unBlock();
        usuarioIncorrecto("No se permiten datos vacíos");
    } else {
        $.ajax({
            url: "index.aspx/iniciaSessionString",
            data: "{pass:'" + $("#txtContrasena").val() + "',user:'" + $("#txtNomUsuario").val() + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                unBlock();
                var user, id, status, coinciden, existe, activo, idioma;
                user = response.d[0]; id = response.d[1];
                status = response.d[2]; coinciden = response.d[3];
                existe = response.d[4]; activo = response.d[5];
                idioma = response.d[6];
                if (status == '1') {//Si el usuario cambió su contraseña
                    $('#dlogNewPassword').dialog("open");
                    $('#btnSi').click(function () {
                        var txtContrasenia1 = $('#txtNewPassword1').val();
                        var txtContrasenia2 = $('#txtNewPassword2').val();

                        if (txtContrasenia1 != '' && txtContrasenia2 != '') {
                            if (txtContrasenia1 == txtContrasenia2) {
                                cambiarContrasenia(id, txtContrasenia2);
                            } else {
                                $('#txtNewPassword1').val('');
                                $('#txtNewPassword2').val('');
                                //$('#txtNewPassword1').focus();
                                $('.mensajeError').text('Las contraseñas no son iguales.');
                                $("#divError").slideDown("slow");
                                setTimeout(function () { $("#divError").slideUp("slow"); }, 3000);
                            }

                        } else {
                            $('.mensajeError').text('Llena el formulario.');
                            $("#divError").slideDown("slow");
                            setTimeout(function () { $("#divError").slideUp("slow"); }, 3000);
                            //$('#txtNewPassword1').focus();
                        }
                    });
                    $('#btnNo').click(function () {
                        $('#dlogNewPassword').dialog("close");
                    });
                }
                iniciaSession(response.d);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    }
}

function cambiarContrasenia(idUsuario, contrasenia) {
    block();
    $.ajax({
        url: "index.aspx/cambiarContrasenia",
        type: "POST",
        data: "{idUsuario:" + idUsuario + ",contrasenia:'" + contrasenia + "'}",
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        success: function (data) {
            unBlock();
            var mensaje = data.d.indexOf('°Correcto');
            if (mensaje > 0) {
                alertify.success(data.d.substring(0, mensaje - 1));
                $('#dlogNewPassword').dialog("close");
                document.location.href = "Inicio.aspx";
            } else if (data.d.indexOf('°Error')) {
                alertify.error(data.d.substring(0, mensaje - 1));
            }

        },
        error: function (xhr, status, error) {
            unBlock();
            alert('error');
        }
    });
}

function iniciaSession(session) {
    var user, id, status, coinciden, existe, activo, idioma;

    user = session[0];
    id = session[1];
    status = session[2];
    coinciden = session[3];
    existe = session[4];
    activo = session[5];
    idioma = session[6];

    /******************Validando***********************/
    if (activo == ":'(") {
        usuarioIncorrecto("");
        //usuarioIncorrecto("Posible error en el sistema");
    } else if (activo == ":(" && existe == ":(") {
        usuarioIncorrecto("El usuario ingresado no existe");
    } else if (activo == ":(" && existe == ":)" && coinciden == ":)") {
        //usuarioIncorrecto("El usuario se encuentra inactivo");
        usuarioIncorrecto("Usuario/Contraseña incorrectos");
    } else if (activo == ":)" && existe == ":)" && coinciden == ":(") {
        usuarioIncorrecto("Los datos del usuario no coinciden");
    } else if (activo == ":)" && existe == ":)" && coinciden == ":)") {
        if (status == 1) {
            $("#txtidUs").val(id);
            idUsuario = id;
            
        } else {
            $("#exitoEntro").removeClass("hide");
            //Redireccionamos al Home
            document.location.href = "inicio.aspx";
        }
    } else if (status==0) {
        usuarioIncorrecto("No tienes permisos para ingresar");
    }

}

function usuarioIncorrecto(mensaje) {
    $("#lblErrorAutentificacion").html("");
    $("#lblErrorAutentificacion").html(mensaje + " favor de verificar la información.");
    $("#errorLogin").slideDown("slow");
    setTimeout(function () { $("#errorLogin").slideUp("slow"); }, 3000);
    $("#txtUser").val("");
    $("#txtPass").val("");
    $("#txtUser").focus();
}