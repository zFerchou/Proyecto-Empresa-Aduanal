<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Servidor.aspx.cs" Inherits="ServidorCS" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="Server">
     
<script src="../../js/Servidor/Servidor.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server">

    <div class="datagrid-layer" >
        <div style="display: flex; justify-content: space-between;">
            <h1 class="tit">
                <span class="">Servidor</span>
            </h1>
    <%-- dialog Generear Reporte --%>
            <h3 id="lblReportePDF">
     <asp:Button ID="btnAgregarServidor" runat="server" Text="Agregar Servidor" OnClientClick="AgregarServidor();" class="btn verde shadow2 borde-blanco" />

            </h3>
        </div>
            <%-- Generar un servidor --%>
        <center>
                    <asp:ScriptManager ID="ScriptManager2" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanelll" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" OnPreRender="GridView1_PreRender" runat="server" 
                                CssClass="data_grid" AutoGenerateColumns="False">
                                <RowStyle HorizontalAlign="center"></RowStyle>
                                <AlternatingRowStyle CssClass="alt" />
                                <Columns>
                                    
                                    <asp:TemplateField  HeaderText="Tamaño" SortExpression="Categoria" ControlStyle-CssClass="grid-head1">
                                       <EditItemTemplate>
                                            <asp:Label ID="lblCategoria" runat="server" Text='<%# Eval("Categoria") %>'></asp:Label>
                                       </EditItemTemplate>
                                            <ItemTemplate>
                                             <asp:Label ID="lblCategoria" runat="server" Text='<%# Bind("Categoria") %>'></asp:Label>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre Servidor" SortExpression="NomInstancia" >
                                        <EditItemTemplate>
                                            <asp:Label ID="lblNomInstancia" runat="server" Text='<%# Eval("NomInstancia") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNomInstancia" runat="server" Text='<%# Bind("NomInstancia") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Tipo de Servidor" SortExpression="TipoInstancia">
                                   <EditItemTemplate>
                                        <asp:Label ID="lblTipoInstancia" runat="server" Text='<%# Eval("TipoInstancia") %>'></asp:Label>
                                   </EditItemTemplate>
                                        <ItemTemplate>
                                         <asp:Label ID="lblTipoInstancia" runat="server" Text='<%# Bind("TipoInstancia") %>'></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Ver detalles">
    <EditItemTemplate>
        <asp:Label ID="lblSistema" runat="server"></asp:Label>
    </EditItemTemplate>
    <ItemTemplate>
        <asp:Label ID="lblSistema2" runat="server"></asp:Label>
         <a href="javascript:;" onclick="InformacionServidor('<%# Eval("idServidor") %>');">
            <span id="icon-25" class="consultar verde" title="Ver Grupo"></span>
        </a>
        
    </ItemTemplate>
   
</asp:TemplateField>   
                                   
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </center>
        <asp:Label ID="lblResult" runat="server" Font-Bold="True" ForeColor="#1d6688"></asp:Label>
    </div>




    <%-- dialog Agregar un servidor--%>
    <div id="dlogServidor" class="hide" title="Agregar un servidor" style="display: none">
    <div class="clear width98 left frm">
        <div class="form-group">
            <label for="txtCategoria">Categoria:</label>
           <asp:TextBox ID="txtCategoria" runat="server"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <label for="txtNomInstancia">NomInstancia:</label>
            <asp:TextBox ID="txtNomInstancia" runat="server"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <label for="txtIpPublica">IpPublica:</label>
            <asp:TextBox ID="txtIpPublica" runat="server"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <label for="txtIpPrivada">IpPrivada:</label>
           <asp:TextBox ID="txtIpPrivada" runat="server"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <label for="txtUrlServidor">UrlServidor:</label>
            <asp:TextBox ID="txtUrlServidor" runat="server"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <label for="txtZonaDisponible">ZonaDisponible:</label>
            <asp:TextBox ID="txtZonaDisponible" runat="server"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <label for="txtEstatus">Estatus:</label>
           <asp:TextBox ID="txtEstatus" runat="server"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <label for="txtIdUsuario">IdUsuario:</label>
            <asp:TextBox ID="txtIdUsuario" runat="server"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <label for="txtIdTipoInstancia">IdTipoInstancia:</label>
             <asp:TextBox ID="txtIdTipoInstancia" runat="server"></asp:TextBox>
        </div>
        <br />
        <div class="form-group">
            <label for="txtIdApp">IdApp:</label>
             <asp:TextBox ID="txtIdApp" runat="server"></asp:TextBox>
        </div>
        
         <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClientClick="agregarDatosServidor();" class="btn verde shadow2 borde-blanco" />

    </div>
</div>

   

<%-- dialog Información del Servidor (Ver detalles) --%>
<div id="dlogInfoServidor" class="hide" title="Detalles" style="display: none">
    <div class="clear width98 left frm">
        <div id="contenedor">
            <div>
                <label for="nombreInstancia"><b>Nombre de la instancia</b></label>
                <input type="text" id="nombreInstancia" name="Nombre" required>
            </div>
            <div>
                <label for="catego"><b>Categoría</b></label>
                <input type="text" id="catego" name="Catego" required>
            </div>
            <div>
                <label for="ipPublica"><b>IP Pública</b></label>
                <input type="text" id="ipPublica" name="IpPublica" required>
            </div>
            <div>
                <label for="ipPrivada"><b>IP Privada</b></label>
                <input type="text" id="ipPrivada" name="IpPrivada" required>
            </div>
            <div>
                <label for="urlServidor"><b>URL del Servidor</b></label>
                <input type="text" id="urlServidor" name="URLServidor" required>
            </div>
            <div>
                <label for="zonaDisponible"><b>Zona Disponible</b></label>
                <input type="text" id="zonaDisponible" name="ZonaDisponible" required>
            </div>
        </div>
    </div>
    <div class="clear width98 txt-right">
        <button class="btn verde" onclick="guardarCambios()">Guardar Cambios</button>
    </div>
</div> <%-- cierra el diálogo dialog Información del Servidor (Ver detalles) --%>

                               
                                
	


   
</asp:Content>