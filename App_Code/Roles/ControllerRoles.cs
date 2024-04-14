using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ControllerRoles
/// </summary>
public class ControllerRoles
{
    private ERPManagementDataContext erp;
    private ERPManagementRHDataContext erpRH;
    private tRolUsuarioERPM tru;
    private tRolPuestoERPM trp;
    private tEmpleado templeado;
    private tCorreoElectronicoEmpleado tcorreo;
    private storedProcedure sp;
    private tUsuarios tusuarios;
    private cEmpresas cEmpresas;
    private cGrupo cGrupo;
    private tERPGrupo tERPGrup;
   
    public ControllerRoles()
    {
        erp = new ERPManagementDataContext();
        erpRH = new ERPManagementRHDataContext();
        sp = new storedProcedure();
        cEmpresas = new cEmpresas();
        cGrupo = new cGrupo();
        tERPGrup = new tERPGrupo();
	}

    public string getCheckRolesUsuario(int idUsuario)
    {
        string result = "<div class='width98 center'><fieldset><legend>Permisos</legend>";
        string mostrar = "";
        string espacio = "";
        var lstRoles = (from roles in erp.tRolesERPM
                        select roles).ToList();

        var lstRolesCheck = (from rolUsuario in erp.tRolUsuarioERPM
                             where rolUsuario.idUsuario == idUsuario
                             select rolUsuario).ToList();

        int cantidad = lstRoles.Count;
        int contador = 0;
        int var = 0;
        if (lstRoles.Count > 0)
        {
            foreach (tRolesERPM tr in lstRoles)
            {
                bool rolIsChecked = false;
                int tamanio = 30;
                if (lstRolesCheck.Count > 0)
                {
                    foreach (tRolUsuarioERPM tru in lstRolesCheck)
                    {
                        if (tr.idRolERPM == tru.idRolERPM)
                        {
                            rolIsChecked = true;
                        }
                    }
                }
                if (contador > 6 && contador < 16 && var == 0)
                {
                    result += "<div class='clear width98'>";
                    espacio = "clear";
                    var++;
                }
                if (rolIsChecked)
                {
                    result += "<div class='left width30 " + espacio + "' style='1em; font-weight: bold;'><input onChange='javascript:mostrar(" + tr.idRolERPM + ")' id='" + tr.idRolERPM + "' type='checkbox' class='check' value='" + tr.idRolERPM + "' checked/>" +
                              "<label id='lbl" + tr.idRolERPM + "' for='" + tr.idRolERPM + "'>" + tr.descripcion + "</label></div>";
                }
                else
                {
                    result += "<div class='left width30 " + espacio + "' style='1em; font-weight: bold;'><input onChange='javascript:mostrar(" + tr.idRolERPM + ");' id='" + tr.idRolERPM + "' type='checkbox' class='check' value='" + tr.idRolERPM + "' />" +
                              "<label id='lbl" + tr.idRolERPM + "' for='" + tr.idRolERPM + "'>" + tr.descripcion + "</label></div>";

                }
                if (contador > 6 && contador < 16)
                {
                    result += "<div  id='autocomplete" + tr.idRolERPM + "' class='clear left width98 hide'  style='padding-top:10px;'><label style='1.2em; font-weight: bold;'> Grupo: </label><input id='autoC" + tr.idRolERPM + "' class='autocomplete transparente " +
                             "validarAuto" + tr.idRolERPM + "' name='aut' /><br /></div>";
                }
                contador++;
            }
        }

        return result + "</fieldset></div>";
    }

    public string getCheckPermisosUsuario(int idUsuario)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string query = "select idUsuario,idTipoReporte from tUsuarioTipoIncidencia";
        List<tUsuarioIncidencia> lstUsuarioInc = sp.getObjectList<tUsuarioIncidencia>(query);
        string result = "<div class='width98 center'><fieldset><legend>Permisos Incidencias</legend>";
        string mostrar = "";
        string espacio = "";
        var lstRoles = (from roles in erp.cTipoReportes
                        select roles).ToList();

        var lstRolesCheck = (from rolUsuario in lstUsuarioInc
                             where rolUsuario.idUsuario == idUsuario
                             select rolUsuario).ToList();

        int cantidad = lstRoles.Count;
        int contador = 0;
        int var = 0;
        if (lstRoles.Count > 0)
        {
            foreach (cTipoReporte tr in lstRoles)
            {
                bool rolIsChecked = false;
                int tamanio = 30;
                if (lstRolesCheck.Count > 0)
                {
                    foreach (tUsuarioIncidencia tru in lstRolesCheck)
                    {
                        if (tr.idTipoReporte == tru.idTipoReporte)
                        {
                            rolIsChecked = true;
                        }
                    }
                }
                if (contador > 6 && contador < 16 && var == 0)
                {
                    result += "<div class='clear width98'>";
                    espacio = "clear";
                    var++;
                }
                if (rolIsChecked)
                {
                    result += "<div class='left width30 " + espacio + "' style='1em; font-weight: bold;'><input onChange='javascript:mostrar(" + tr.idTipoReporte + ")' id='" + tr.idTipoReporte + "' type='checkbox' class='check' value='" + tr.idTipoReporte + "' checked/>" +
                              "<label id='lbl" + tr.idTipoReporte + "' for='" + tr.idTipoReporte + "'>" + tr.nombreTipoReporte + "</label></div>";
                }
                else
                {
                    result += "<div class='left width30 " + espacio + "' style='1em; font-weight: bold;'><input onChange='javascript:mostrar(" + tr.idTipoReporte + ");' id='" + tr.idTipoReporte + "' type='checkbox' class='check' value='" + tr.idTipoReporte + "' />" +
                              "<label id='lbl" + tr.idTipoReporte + "' for='" + tr.idTipoReporte + "'>" + tr.nombreTipoReporte + "</label></div>";

                }
                if (contador > 6 && contador < 16)
                {
                    result += "<div  id='autocomplete" + tr.idTipoReporte + "' class='clear left width98 hide'  style='padding-top:10px;'><label style='1.2em; font-weight: bold;'> Grupo: </label><input id='autoCI" + tr.idTipoReporte + "' class='autocomplete transparente " +
                             "validarAuto" + tr.idTipoReporte + "' name='aut' /><br /></div>";
                }
                contador++;
            }
        }

        return result + "</fieldset></div>";
    }

    public string getCheckRolesPuesto(int idPuesto)
    {
        string result = "<div class='clear width98 center' style='1em; font-weight: bold;'><fieldset><legend>Permisos</legend>";
        int tmanaio = 50;
        int contador = 0;
        int variable = 0;
        int cantidad;
        string espacio = ""; ;
        var lstRoles = (from roles in erp.tRolesERPM
                        select roles).ToList();

        var lstRolesCheck = (from rolPuesto in erp.tRolPuestoERPM
                             where rolPuesto.idPuesto == idPuesto
                             select rolPuesto).ToList();

        cantidad = lstRoles.Count;
        int var = 0;
        if (lstRoles.Count > 0)
        {
            foreach (tRolesERPM tr in lstRoles)
            {
                bool rolIsChecked = false;
                int tamanio = 30;
                if (lstRolesCheck.Count > 0)
                {
                    foreach (tRolPuestoERPM tru in lstRolesCheck)
                    {
                        if (tr.idRolERPM == tru.idRolERPM)
                        {
                            rolIsChecked = true;
                        }
                    }
                }
                if (contador > 6 && contador < 16 && var == 0)
                {
                    result += "<div class='clear width98'>";
                    espacio = "clear";
                    var++;
                }
                if (rolIsChecked)
                {
                    result += "<div class='left width30 " + espacio + "' style='1em; font-weight: bold;'><input onChange='javascript:mostrar(" + tr.idRolERPM + ")' id='" + tr.idRolERPM + "' type='checkbox' class='check' value='" + tr.idRolERPM + "' checked/>" +
                              "<label id='lbl" + tr.idRolERPM + "' for='" + tr.idRolERPM + "'>" + tr.descripcion + "</label></div>";
                }
                else
                {
                    result += "<div class='left width30 " + espacio + "' style='1em; font-weight: bold;'><input onChange='javascript:mostrar(" + tr.idRolERPM + ");' id='" + tr.idRolERPM + "' type='checkbox' class='check' value='" + tr.idRolERPM + "' />" +
                              "<label id='lbl" + tr.idRolERPM + "' for='" + tr.idRolERPM + "'>" + tr.descripcion + "</label></div>";

                }
                if (contador > 6 && contador < 16)
                {
                    result += "<div  id='autocomplete" + tr.idRolERPM + "' class='clear left width98 hide'  style='padding-top:10px;'><label style='1.2em; font-weight: bold;'> Grupo: </label><input id='autoC" + tr.idRolERPM + "' class='autocomplete transparente " +
                             "validarAuto" + tr.idRolERPM + "' name='aut' /><br /></div>";
                }
                contador++;
            }
        }

        return result + "</fieldset></div>";
    }

    public bool insertaRolesUsuario(int idUsuario, string permisos)
    {
        List<tRolUsuarioERPM> lstRolUsuario = new List<tRolUsuarioERPM>();
        string[] rol = permisos.Split('|');
        string[] grupoRol = rol;
        string[] grol;
        int tam = rol.Length;
        int idSoporte;
        try
        {
            var lstRolUsuarioDelete = (from rolUsuario in erp.tRolUsuarioERPM
                                       where rolUsuario.idUsuario == idUsuario
                                       select rolUsuario).ToList();

            erp.tRolUsuarioERPM.DeleteAllOnSubmit(lstRolUsuarioDelete);
            erp.SubmitChanges();

            var lstRolGrupo = (from rolesGrupo in erp.tUsuarioSoporte
                               where rolesGrupo.idUsuario == idUsuario
                               select rolesGrupo).ToList();

            erp.tUsuarioSoporte.DeleteAllOnSubmit(lstRolGrupo);
            erp.SubmitChanges();

            for (int i = 0; i < tam; i++)
            {
                if (rol[i] != "")
                {
                    grupoRol = rol[i].Split('.');
                    int t = grupoRol.Length;
                    if (t == 1)
                    {

                        tRolUsuarioERPM trolU = new tRolUsuarioERPM();
                        trolU.idRolERPM = int.Parse(grupoRol[0]);
                        trolU.idUsuario = idUsuario;
                        erp.tRolUsuarioERPM.InsertOnSubmit(trolU);
                    }
                    else
                    {
                        tRolUsuarioERPM trolU = new tRolUsuarioERPM();
                        trolU.idRolERPM = int.Parse(grupoRol[0]);
                        trolU.idUsuario = idUsuario;
                        erp.tRolUsuarioERPM.InsertOnSubmit(trolU);

                        idSoporte = int.Parse(grupoRol[0]);
                        grol = grupoRol[1].Split(',');
                        int tamaño = grol.Length;
                        for (int c = 0; c < tamaño; c++)
                        {
                            tUsuarioSoporte tuser = new tUsuarioSoporte();
                            tuser.idGrupo = int.Parse(grol[c]);
                            tuser.idRolERPM = idSoporte;
                            tuser.idUsuario = idUsuario;
                            erp.tUsuarioSoporte.InsertOnSubmit(tuser);
                        }
                    }

                }
            }
            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public bool insertaPermisosUsuario(int idUsuario, string permisos)
    {
        storedProcedure sp = new storedProcedure("DBSGICEConnectionString");
        string query = "";
        List<tRolUsuarioERPM> lstRolUsuario = new List<tRolUsuarioERPM>();
        string[] rol = permisos.Split('|');
        string[] grupoRol = rol;
        string[] grol;
        int tam = rol.Length;
        int idSoporte;
        try
        {
            query = "delete from tUsuarioTipoIncidencia where idUsuario = "+idUsuario;
            bool bBandera = sp.ejecutaSQL(query);
            if (bBandera)
            {
                for (int i = 0; i < tam; i++)
                {
                    if (rol[i] != "")
                    {
                        grupoRol = rol[i].Split('.');
                        int t = grupoRol.Length;
                        if (t == 1)
                        {
                            query = "INSERT INTO tUsuarioTipoIncidencia VALUES("+idUsuario+","+ grupoRol[0] + ")";
                            sp.ejecutaSQL(query);
                        }
                        else
                        {
                            query = "INSERT INTO tUsuarioTipoIncidencia VALUES(" + idUsuario + "," + grupoRol[0] + ")";
                            sp.ejecutaSQL(query);

                            idSoporte = int.Parse(grupoRol[0]);
                            grol = grupoRol[1].Split(',');
                            int tamaño = grol.Length;
                            for (int c = 0; c < tamaño; c++)
                            {
                                query = "INSERT INTO tUsuarioTipoIncidencia VALUES(" + idUsuario + "," + idSoporte + ")";
                                sp.ejecutaSQL(query);
                            }
                        }

                    }
                }
                
            }
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public bool insertaRolesPuesto(int idPuesto, string permisos)
    {
        string[] rol = permisos.Split('|');
        string[] grupoRol = rol;
        string[] grol;

        int tam = rol.Length;
        int idSoporte;
        try
        {
            var lstRolUsuarioDelete = (from rolPuesto in erp.tRolPuestoERPM
                                       where rolPuesto.idPuesto == idPuesto
                                       select rolPuesto).ToList();
            if (lstRolUsuarioDelete.Count > 0)
            {
                erp.tRolPuestoERPM.DeleteAllOnSubmit(lstRolUsuarioDelete);
                erp.SubmitChanges();
            }

            var lstUsuarios = (from usuarios in erp.vUsuariosERPM
                               where usuarios.idPuesto == idPuesto.ToString()
                                       select usuarios).ToList();

            List<tRolUsuarioERPM> lstUsuariosERP = new List<tRolUsuarioERPM>();
            if (lstUsuarios.Count > 0)
            {

                foreach (vUsuariosERPM v in lstUsuarios)
                {

                    var lstRolUsuariosDelete = (from rolUsuario in erp.tUsuarioSoporte
                                                where rolUsuario.idUsuario == int.Parse(v.idEmpleado)
                                                select rolUsuario).ToList();

                    
                    int tamanio = grupoRol.Length;
                    for (int x = 0; x < tamanio; x++)
                    {
                        if (grupoRol[x] != "")
                        {
                            tRolUsuarioERPM trolu = new tRolUsuarioERPM();
                            trolu.idUsuario = int.Parse(v.idEmpleado);
                            string[] tipoSoporte = grupoRol[x].Split('.');
                            trolu.idRolERPM = int.Parse(tipoSoporte[0]);
                            lstUsuariosERP.Add(trolu);
                        }
                    }

                    if (lstRolUsuariosDelete.Count > 0)
                    {
                        erp.tUsuarioSoporte.DeleteAllOnSubmit(lstRolUsuariosDelete);
                        erp.SubmitChanges();
                    }

                    var lstRolUsDelete = (from rolUsuario in erp.tRolUsuarioERPM
                                          where rolUsuario.idUsuario == int.Parse(v.idEmpleado)
                                          select rolUsuario).ToList();

                    if (lstRolUsDelete.Count > 0)
                    {
                        erp.tRolUsuarioERPM.DeleteAllOnSubmit(lstRolUsDelete);
                        erp.SubmitChanges();
                    }
                }
            }

            var lstRolGrupo = (from rolesGrupo in erp.tPuestoSoporte
                               where rolesGrupo.idPuesto == idPuesto
                               select rolesGrupo).ToList();

            erp.tPuestoSoporte.DeleteAllOnSubmit(lstRolGrupo);
            erp.SubmitChanges();

            if (permisos != "")
            {
                for (int i = 0; i < tam; i++)
                {
                    if (rol[i] != "")
                    {
                        grupoRol = rol[i].Split('.');
                        int t = grupoRol.Length;
                        if (t == 1)
                        {

                            tRolPuestoERPM trol = new tRolPuestoERPM();
                            trol.idRolERPM = int.Parse(grupoRol[0]);
                            trol.idPuesto = idPuesto;
                            erp.tRolPuestoERPM.InsertOnSubmit(trol);
                            if (lstUsuarios.Count > 0)
                            {
                                erp.tRolUsuarioERPM.InsertAllOnSubmit(lstUsuariosERP);
                            }
                        }
                        else
                        {
                            tRolPuestoERPM trolP = new tRolPuestoERPM();
                            trolP.idRolERPM = int.Parse(grupoRol[0]);
                            trolP.idPuesto = idPuesto;
                            erp.tRolPuestoERPM.InsertOnSubmit(trolP);

                            idSoporte = int.Parse(grupoRol[0]);
                            grol = grupoRol[1].Split(',');
                            int tamaño = grol.Length;
                            for (int c = 0; c < tamaño; c++)
                            {
                                tPuestoSoporte tPuesto = new tPuestoSoporte();
                                tPuesto.idGrupo = int.Parse(grol[c]);

                                var lstusuariosRolPuesto = (from uPuesto in erp.vUsuariosERPM
                                                            where uPuesto.idPuesto == idPuesto.ToString()
                                                            select uPuesto.idEmpleado).ToList();

                                List<tRolUsuarioERPM> tRolUsuarioERP = new List<tRolUsuarioERPM>();
                                List<tUsuarioSoporte> lstUS = new List<tUsuarioSoporte>();
                                foreach (var objeto in lstusuariosRolPuesto)
                                {

                                    tUsuarioSoporte tUsuariosSop = new tUsuarioSoporte();
                                    tUsuariosSop.idGrupo = int.Parse(grol[c]);
                                    tUsuariosSop.idUsuario = int.Parse(objeto);
                                    tUsuariosSop.idRolERPM = idSoporte;
                                    lstUS.Add(tUsuariosSop);

                                    tRolUsuarioERPM tRolUsuario = new tRolUsuarioERPM();
                                    tRolUsuario.idRolERPM = int.Parse(grupoRol[0]);
                                    tRolUsuario.idUsuario = int.Parse(objeto);
                                    tRolUsuarioERP.Add(tRolUsuario);
                                }

                                erp.tUsuarioSoporte.InsertAllOnSubmit(lstUS);
                                erp.tRolUsuarioERPM.InsertAllOnSubmit(tRolUsuarioERP);

                                tPuesto.idRolERPM = idSoporte;
                                tPuesto.idPuesto = idPuesto;
                                erp.tPuestoSoporte.InsertOnSubmit(tPuesto);
                            }
                        }


                    }
                }
            }
            erp.SubmitChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public List<tERPGrupo> obtenerGrupo()
    {

        var grupos = (from grupo in erp.tERPGrupo
                      select grupo).ToList();

        List<tERPGrupo> resultado = new List<tERPGrupo>();

        foreach (tERPGrupo ter in grupos)
        {
            tERPGrupo terg = new tERPGrupo();
            terg.idERPGrupo = ter.idERPGrupo;
            terg.nomGrupo = ter.nomGrupo;
            resultado.Add(terg);
        }

        return resultado;
    }

    public List<string> obtenerGrupoActivo(int idUsuario, int idSoporte)
    {

        var grupos = (from grupo in erp.tUsuarioSoporte
                      where grupo.idUsuario == idUsuario && grupo.idRolERPM == idSoporte
                      select grupo.idGrupo).ToList();

        List<string> resultado = new List<string>();

        foreach (int objeto in grupos)
        {
            resultado.Add(objeto.ToString());
            
            resultado.Add(getNomSistema(objeto).ToString());
        }

        return resultado;
    }

    public string getNomSistema(int idGrupo)
    {
        var nomSistema = (from ns in erp.tERPGrupo
                          where ns.idERPGrupo == idGrupo
                          select ns.nomGrupo).FirstOrDefault();

        return nomSistema;
    }


    public List<string> obtenerGrupoPuestoActivo(int idPuesto, int idSoporte)
    {

        var grupos = (from grupo in erp.tPuestoSoporte
                      where grupo.idPuesto == idPuesto && grupo.idRolERPM == idSoporte
                      select grupo.idGrupo).ToList();

        List<string> resultado = new List<string>();

        foreach (int objeto in grupos)
        {
            resultado.Add(objeto.ToString());
            resultado.Add(getNomSistema(objeto).ToString());
        }

        return resultado;
    }

    //Guardar Usuario
    public bool guardarUsuario(string nombre, string app, string apm, string correo, int idGrupo, int idArea)
    {
        try
        {
            #region crearEmpledo
            var getIdEmpresa = (from cEmpresa in erpRH.cEmpresas
                                where cEmpresa.id_grupo == idGrupo
                                select cEmpresa.idEmpresa).FirstOrDefault();
            //Insertar en tEmpleado RH
            templeado = new tEmpleado();
            templeado.nombre = nombre;
            templeado.apellidoPaterno = app;
            templeado.apellidoMaterno = apm;
            templeado.idEmpresa = getIdEmpresa;
            templeado.idArea = idArea;
            templeado.idEstadoCivil = 1;
            erpRH.tEmpleado.InsertOnSubmit(templeado);
            erpRH.SubmitChanges();
            #endregion

            #region ultimoEmpleadoCreado
            //Obtener el último empleado registrado
            var q = (from tempelado in erpRH.tEmpleado
                     select templeado.idEmpleado).Max();
            #endregion

            #region guardarCorreo
            ////Guardar el idUsuario y el correo en tCorreoElectronicoEmpleado RH
            tcorreo = new tCorreoElectronicoEmpleado();
            tcorreo.idEmpleado = q;
            tcorreo.correoElectronico = correo;
            tcorreo.idTipoCorreoElectronico=1;
            erpRH.tCorreoElectronicoEmpleado.InsertOnSubmit(tcorreo);
            erpRH.SubmitChanges();
            #endregion

            #region agregarRolGeneradorIncidencias
            ////Agregar rol de "Soporte de Incidencias" al nuevo Usuario
            tru = new tRolUsuarioERPM();
            tru.idUsuario = q;
            tru.idRolERPM=1;
            erp.tRolUsuarioERPM.InsertOnSubmit(tru);
            erp.SubmitChanges();
            #endregion

            #region configUsuario
            ////Poner al usuario como externo, generar su usuario y contraseña
            string randomPassword;
            var getUsuario = (from tr in erpRH.tUsuarios
                              where tr.idUsuario == q
                              select tr).FirstOrDefault();
            var getCorreo = (from trCorreo in erpRH.tCorreoElectronicoEmpleado
                             where trCorreo.idEmpleado == q
                             select trCorreo).FirstOrDefault();

            //Generar una contraseña.
            randomPassword = createRandomPassword(10);
            //Encriptar la contraseña.
            string newPassword = sp.getSha512(randomPassword);
            string numUsuarioAl = createRandomPassword(3);
            string newUser = nombre.Trim().ToLower()+app.Trim().ToLower();
           
            var existsUser = (from tr in erpRH.tUsuarios
                              where tr.usuario == newUser
                              select tr).Count();
            if (existsUser >=1 && app !="")
            {
                newUser = newUser + app.Trim() + numUsuarioAl;
            }
            else if (existsUser >=1 && app =="")
            {
                newUser = newUser + createRandomPassword(2).ToLower();
            }
            {

            }
            //Crear un Usuario y COntraseña y guardarlo en tUsuarios RH.
            tusuarios = new tUsuarios();
            tusuarios.idUsuario = q;
            tusuarios.usuario = newUser;
           tusuarios.contrasena = newPassword;
           tusuarios.idTipoUsuario = 2;
           tusuarios.idEstatusUsuario = 1;
           // //Insertar en "tUsuarios" en RH.
           erpRH.tUsuarios.InsertOnSubmit(tusuarios);
           erpRH.SubmitChanges();

           enviarCorreo(newUser, randomPassword);
           #endregion
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }

    public bool existeGrupo(string nomGrupo) {
        var getNomGrupo = (from nombreGrupo in erpRH.cGrupo
                            where nombreGrupo.grupo == nomGrupo
                            select nombreGrupo.grupo).Count();
        if (getNomGrupo <= 0)
        {            
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool agregarUsuarioConGrupoNuevo(string nombre, string app, string apm, string correo, string nomGrupo, int idArea)
    {
        try
        {
            #region crearEmpledo            
            //Insertar en cGrupo RH
            cGrupo = new cGrupo();
            cGrupo.grupo = nomGrupo;
            erpRH.cGrupo.InsertOnSubmit(cGrupo);
            erpRH.SubmitChanges();


            var getIdGrupoByName = (from cGru in erpRH.cGrupo
                                    where cGru.grupo == nomGrupo
                                    select cGru.id_grupo).FirstOrDefault();

            //Insertar en cEmpresas el nuevo Grupo
            cEmpresas = new cEmpresas();
            cEmpresas.descripcion = nomGrupo;
            cEmpresas.id_grupo = getIdGrupoByName;            
            erpRH.cEmpresas.InsertOnSubmit(cEmpresas);
            erpRH.SubmitChanges();

            var getIdEmpresa = (from cEmpresa in erpRH.cEmpresas
                                where cEmpresa.id_grupo == getIdGrupoByName
                                select cEmpresa.idEmpresa).FirstOrDefault();

            //Insertar en tERPGRupo el nuevo Grupo
            tERPGrup = new tERPGrupo();
            tERPGrup.nomGrupo = nomGrupo;
            tERPGrup.idGrupoRH = getIdGrupoByName;
            tERPGrup.urlERP = "";
            erp.tERPGrupo.InsertOnSubmit(tERPGrup);
            erp.SubmitChanges();

            //Insertar en tEmpleado RH
            templeado = new tEmpleado();
            templeado.nombre = nombre;
            templeado.apellidoPaterno = app;
            templeado.apellidoMaterno = apm;
            templeado.idEmpresa = getIdEmpresa;
            templeado.idArea = idArea;
            templeado.idEstadoCivil = 1;
            erpRH.tEmpleado.InsertOnSubmit(templeado);
            erpRH.SubmitChanges();
            #endregion

            #region ultimoEmpleadoCreado
            //Obtener el último empleado registrado
            var q = (from tempelado in erpRH.tEmpleado
                     select templeado.idEmpleado).Max();
            #endregion

            #region guardarCorreo
            //Guardar el idUsuario y el correo en tCorreoElectronicoEmpleado RH
            tcorreo = new tCorreoElectronicoEmpleado();
            tcorreo.idEmpleado = q;
            tcorreo.correoElectronico = correo;
            tcorreo.idTipoCorreoElectronico = 2;
            erpRH.tCorreoElectronicoEmpleado.InsertOnSubmit(tcorreo);
            erpRH.SubmitChanges();
            #endregion

            #region agregarRolGeneradorIncidencias
            //Agregar rol de "Soporte de Incidencias" al nuevo Usuario
            tru = new tRolUsuarioERPM();
            tru.idUsuario = q;
            tru.idRolERPM = 1;
            erp.tRolUsuarioERPM.InsertOnSubmit(tru);
            erp.SubmitChanges();
            #endregion

            #region configUsuario
            //Poner al usuario como externo, generar su usuario y contraseña
            string randomPassword;
            var getUsuario = (from tr in erpRH.tUsuarios
                              where tr.idUsuario == q
                              select tr).FirstOrDefault();
            var getCorreo = (from trCorreo in erpRH.tCorreoElectronicoEmpleado
                             where trCorreo.idEmpleado == q
                             select trCorreo).FirstOrDefault();

            //Generar una contraseña.
            randomPassword = createRandomPassword(10);
            //Encriptar la contraseña.
            string newPassword = sp.getSha512(randomPassword);
            string newUser = nombre.Substring(0, 1).ToLower();

            var existsUser = (from tr in erpRH.tUsuarios
                              where tr.usuario == newUser
                              select tr).Count();
            if (existsUser >= 1)
            {
                newUser = newUser + app.Trim() + apm.Substring(0, 1).ToLower();
            }
            else
            {
                newUser = newUser + app.Trim().ToLower();
            }
            //Crear un Usuario y COntraseña y guardarlo en tUsuarios RH.
            tusuarios = new tUsuarios();
            tusuarios.idUsuario = q;
            tusuarios.usuario = newUser;
            tusuarios.contrasena = newPassword;
            tusuarios.idTipoUsuario = 2;
            tusuarios.idEstatusUsuario = 1;
            //Insertar en "tUsuarios" en RH.
            erpRH.tUsuarios.InsertOnSubmit(tusuarios);
            erpRH.SubmitChanges();

            enviarCorreo(newUser, randomPassword);
            #endregion
            return true;
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
            return false;
        }
    }
    /// <summary>
    /// Método para generar contraseñas alfanumericamente
    /// </summary>
    /// <param name="passwordLength"></param>
    /// <returns>String</returns>
    private static string createRandomPassword(int passwordLength)
    {
        string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
        char[] chars = new char[passwordLength];
        Random rd = new Random();

        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }

        return new string(chars);
    }

    /// <summary>
    /// Método para enviar correo electrónico al recuperar contraseña
    /// </summary>
    /// <returns>True si el correo se envia de manera exitosa, de lo contrario false</returns>
    public bool enviarCorreo(string usuario, string contrasenia)
    {
        var getUsuario = (from tr in erpRH.tUsuarios
                          where tr.usuario == usuario
                          select tr).FirstOrDefault();
        var getCorreo = (from tr in erpRH.tCorreoElectronicoEmpleado
                         where tr.idEmpleado == getUsuario.idUsuario
                         select tr).FirstOrDefault();
        string receptor = getCorreo.correoElectronico;
        string cuerpo = "<h3>La siguiente información es para poder ingresar al sistema:  <a href='https://www.nadconsultoria.com/ERPManagement/'>ERP Management</a></h3><table border='0' cellpadding='0' cellspacing='0' align='center' width='100%'><tbody><tr bgcolor='#85c555'><td colspan='5' height='20'>&nbsp;</td></tr><tr bgcolor='#85c555'><td width='20'>&nbsp;</td><td width='20'>&nbsp;</td><td align='center' style='font-size:29px;color:#ffffff;font-weight:normal;letter-spacing:1px;line-height:1;font-family:Arial,Helvetica,sans-serif'> Tu Usuario es: " + usuario + " y tu nueva contraseña es: " + contrasenia + "</td><td width='20'>&nbsp;</td><td width='20'>&nbsp;</td></tr><tr bgcolor='#85c555'><td colspan='5' height='20'>&nbsp;</td></tr></tbody></table>";
        string snombre = "Alertas LCA";
        string asunto = "Nueva Usuario y Contraseña";
        int ubicacion = 0;

        sp = new storedProcedure("DBSGICEConnectionString");
        EnvioCorreos.EnviaCorreos(snombre, asunto, receptor, cuerpo, ubicacion);

        return true;
    }
}