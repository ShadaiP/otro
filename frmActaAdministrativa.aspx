<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmActaAdministrativa.aspx.cs" Inherits="InventariosPJEH.frmActaAdministrativa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style19 {
            width: 826px;
        }

        .auto-style20 {
            text-transform: uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTitulo" runat="server">
       <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblTituloActa" runat="server" Text="Generación de actas administrativas" Font-Size="Large" Font-Bold="True" Font-Italic="True" ForeColor="#6D252B"></asp:Label>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
   <asp:ScriptManager ID="ScriptManagerGenerarActa" runat="server"></asp:ScriptManager> 
    
    <asp:UpdatePanel ID="GAdministrativa" runat="server">
        <ContentTemplate>
            <div id="ContenerTodo">            

                <div id="DivGeneral" runat="server" visible="true">
                    <div id="FiltroB" style="width: 95%; margin-left: 0px;">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Filtro Busqueda</legend>
                            <asp:Label ID="lblNoResguardo" runat="server" CssClass="LabelGeneral" Text="Número de Resguardo: "></asp:Label>
                            <asp:TextBox ID="TxtFiltroB" runat="server" AutoPostBack="true" CssClass="TextBox" Height="22px" Width="183px"></asp:TextBox>
                            <asp:Button  ID="BtnFiltroB" runat="server" CssClass="Boton" Height="28px" Text="Busqueda" Width="100px" OnClientClick="BtnGenerarA_CheckedChanged"/>                            
                    </fieldset>
                    <fieldset id="sectionBienes" style="border-color: black; width: 95%; margin-left: 11px;" visible ="false" runat="server">
                           <legend style="width: auto; color: darkblue; font-size: 12px;">Bienes no Localizados</legend>
                          <div>
                              <table style="width: 100%; margin-top: 30px;">
                                  <tr>
                                      <td>
                                          <asp:Label ID="lblNombreR" Font-Bold="true" runat="server" CssClass="LabelGeneral" Text="Nombre del Resguardante: "></asp:Label>
                                      </td>
                                      <td>
                                          <asp:Label ID="lblNombreResul" runat="server" CssClass="LabelGeneral" Text=""></asp:Label>                                          
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <asp:Label ID="lblCargoR" Font-Bold="true" runat="server" CssClass="LabelGeneral" Text="Cargo del Resguadante"></asp:Label>
                                      </td>
                                      <td>
                                          <asp:Label ID="lblCargoResul" runat="server" CssClass="LabelGeneral" Text=""></asp:Label>                                          
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <asp:Label ID="lblAreaAdri" Font-Bold="true" runat="server" CssClass="LabelGeneral" Text="Area de Adcripción"></asp:Label>
                                      </td>
                                      <td>
                                          <asp:Label ID="lblAreaResul"  runat="server" CssClass="LabelGeneral" Text=""></asp:Label>
                                          <asp:Label ID="lblIdResguardo"  runat="server" CssClass="LabelGeneral" Text="" Visible="false"></asp:Label>
                                          <asp:Label ID="lblIdUniAdmin"  runat="server" CssClass="LabelGeneral" Text="" Visible="false"></asp:Label>
                                      </td>
                                  </tr>
                              </table>
                          </div>

                           <div runat="server" id="DivTablaResultadosResguardo" style="width: 100%;" visible="false">

                            <fieldset style="height: auto">

                                <asp:GridView ID="gridResultados" CssClass="StyleGridV" runat="server" Height="142px" Width="100%"
                                    AutoGenerateColumns="False"
                                    DataKeyNames="txtOInventarioResguardo,txtONombreBienResguardo,txtOMarcaResguardo,txtOModeloResguardo,txtOSerieResguardo"                                    >
                                    <Columns>                                        
                                        <asp:BoundField DataField="txtOInventarioResguardo" HeaderText="Inventario" />
                                        <asp:BoundField DataField="txtONombreBienResguardo" HeaderText="Nombre del Bien" />
                                        <asp:BoundField DataField="txtOMarcaResguardo" HeaderText="Marca" />
                                        <asp:BoundField DataField="txtOModeloResguardo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="txtOSerieResguardo" HeaderText="Serie" />

                                        
                                    </Columns>
                                </asp:GridView>
                            </fieldset>
                        </div>
                        <fieldset id="sectionGuardarActa" runat="server" visible="false" style="border-color: black; width: 95%; margin-left: 11px;">
                        <table style="margin-left: 100px" class="auto-style8">
                            <tr>
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="lblNumActa" runat="server" CssClass="LabelGeneral" Text="Número de Acta: "></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumActa" runat="server" AutoPostBack="true" CssClass="TextBox" Height="22px" Width="183px"></asp:TextBox>
                                </td>                        
                                <td style="text-align: right;" class="auto-style6">
                                    <asp:Label ID="LablFechaAdquisicion" runat="server" Text="Fecha adquisición:" Width="190px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaAdquisicion" runat="server" CssClass="TextBox" Enabled="false"></asp:TextBox>
                                    <asp:Image ID="ImgFechaAdquisicion" runat="server" Height="40px" ImageUrl="~/Imagenes/Generales/Calendario.png" Width="50px" />                                    
                                        <cc1:CalendarExtender ID="FechaAdquisicion" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImgFechaAdquisicion" TargetControlID="txtFechaAdquisicion" />
                                </td>
                            </tr>                            
                        </table>
                        </fieldset>                                                        
                        <table style="width: 50%; margin: 0 auto; text-align: center;">
                            <tr>
                                <td>
                                    <asp:Button ID="BtnGuardarActa" CssClass="Boton" runat="server" Text="Guardar Acta" OnClick="BtnGuardarActa_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnCancelarr" CssClass="Boton" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click" />
                                </td>
                            </tr>
                        </table>                            
                    </fieldset>
                </div>
               </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
