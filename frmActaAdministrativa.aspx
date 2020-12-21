<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmActaAdministrativa.aspx.cs" Inherits="InventariosPJEH.frmActaAdministrativa" %>



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
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
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
                        <fieldset style="border-color: black; width: 95%; margin-left: 11px;">                        
                            <asp:Label ID="lblNumActa" runat="server" CssClass="LabelGeneral" Text="Número de Acta: "></asp:Label>
                            <asp:TextBox ID="txtNumActa" runat="server" AutoPostBack="true" CssClass="TextBox" Height="22px" Width="183px"></asp:TextBox>
                            <asp:Label ID="lblFechaActa" runat="server" CssClass="LabelGeneral" Text="Fecha Acta: "></asp:Label>
                            <asp:TextBox ID="dateFechaActa" runat="server" AutoPostBack="true" CssClass="TextBox" Height="22px" Width="183px"></asp:TextBox>                            
                            <asp:Button  ID="Button1" runat="server" CssClass="Boton" Height="28px" Text="Busqueda" Width="100px" OnClientClick="BtnGenerarA_CheckedChanged"/>
                        </fieldset>
                    </fieldset>
                </div>
               </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
