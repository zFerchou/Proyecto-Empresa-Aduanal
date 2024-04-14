<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Clientes.aspx.cs" Inherits="clientes" %>

<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="Server">
    <script src="../../js/Clientes/clientes.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="Server">
    <div class="datagrid-layer">
        <h1 class="tit">
            <span id="icon-47" class="pagar">CLIENTES</span></h1>
        <center>
                    <asp:ScriptManager ID="ScriptManager2" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanelll" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" OnPreRender="GridView1_PreRender" runat="server" DataKeyNames="idCuota"
                                CssClass="data_grid" AutoGenerateColumns="False">
                                <RowStyle HorizontalAlign="center"></RowStyle>
                                <AlternatingRowStyle CssClass="alt" />
                                <Columns>
                                    <asp:TemplateField HeaderText="CLIENTE" SortExpression="nomGrupo" ControlStyle-CssClass="grid-head1">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblGrupo" runat="server" Text='<%# Eval("nomGrupo") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrupo1" runat="server" Text='<%# Bind("nomGrupo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SISTEMA" SortExpression="nomSistema">
                                        <EditItemTemplate>
                                            <asp:Label ID="lblSistema" runat="server" Text='<%# Eval("nomSistema") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSistema2" runat="server" Text='<%# Bind("nomSistema") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FECHA INICIO" SortExpression="fechaInicio">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtfi" runat="server" Text='<%# Bind("fechaInicio") %>' class="txtFechaInicio"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtfi"
                                                ValidationExpression="(19|20|21)\d\d[-](0[1-9]|1[012])[-](0[1-9]|[12][0-9]|3[01])"
                                                Display="Static" EnableClientScript="false" ErrorMessage="La fecha de inicio no es válida, el formato debe de ser:  yyyy/mm/dd"
                                                runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblfi" runat="server" Text='<%# Bind("fechaInicio") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FECHA VENCIMIENTO" SortExpression="fechaVencimiento">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtff" runat="server" Text='<%# Bind("fechaVencimiento") %>' class="txtFechaFin"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtff"
                                                ValidationExpression="(19|20|21)\d\d[-](0[1-9]|1[012])[-](0[1-9]|[12][0-9]|3[01])"
                                                Display="Static" EnableClientScript="false" ErrorMessage="La fecha de vencimiento no es válida, el formato debe de ser:  yyyy/mm/dd"
                                                runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblff" runat="server" Text='<%# Bind("fechaVencimiento") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CUOTA" SortExpression="cuota">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCuota" runat="server" Text='<%# Bind("cuota") %>'></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtCuota"
                                                ValidationExpression="\d*\.?\d*" Display="Static" EnableClientScript="true" ErrorMessage="La cuota no es válida: sólo número y decimales"
                                                runat="server" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCuota" runat="server" Text='<%# Bind("cuota") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:HyperLinkField HeaderText="Modificar" DataTextField="Modificar" />
                                    <asp:HyperLinkField HeaderText="Eliminar" DataTextField="Eliminar" />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </center>
        <asp:Label ID="lblResult" runat="server" Font-Bold="True" ForeColor="#1d6688"></asp:Label>
    </div>


    <div id="dConfirmar" class="hide" title="Confirmación" style="display: none">
        <div class="clear width98 left">
            <span id="icon-25" class="warning rojo"></span>¿Está seguro de eliminar el registro
            seleccionado?
        </div>
        <div class="clear width98 txt-right">
            <label id="lblAceptarEli">
            </label>
            <a class="btn blanco" onclick="javascript:$('#dConfirmar').dialog('close');">Cancelar</a>
        </div>
    </div>

    <div id="dModificar" class="hide" title="Modificar Cuota" style="display: none">
        <div class="clear width98 left">
            <div class="width10 left">
                <span id="icon-47" class="gastos_fijos"></span>
            </div>
            <div class="width40 left">
                <label>Cuota:</label><br />
                <input id="txtCuota" type="text" class="input150" />
            </div>
        </div>
        <div class="clear">
            <br />
        </div>
        <div class="clear width98 left">
            <div class="width10 left">
                <span id="icon-47" class="calendario"></span>
            </div>
            <div class="width40 left">
                <label>Fecha de Inicio:</label><br />
                <input id="txtFInicio" type="text" class="input150" />
            </div>
            <div class="width40 left">
                <label>Fecha de Vencimiento:</label><br />
                <input id="txtFVencimiento" type="text" class="input150" />
            </div>
        </div>

        <div class="clear width98 txt-right">
            <label id="lblModCuota">
            </label>
            <a class="btn blanco" onclick="javascript:$('#dModificar').dialog('close');">Cancelar</a>
        </div>
        <div class="clear">
            <br />
        </div>
    </div>
    <!--Cajas Ocultas-->
    <asp:TextBox runat="server" ID="txtIdCuotaH" Style="display: none" CssClass=""></asp:TextBox>

</asp:Content>
