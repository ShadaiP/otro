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
            <div id="divBotones" runat="server">
                <fieldset style="width: 95%; margin-left: 11px;">
                    <legend style="width: auto; color: darkblue; font-size: 12px;">Filtro de Búsqueda</legend>
                    <asp:RadioButton Id="BtnGenerarA" runat="server" Text="Generar Acta" AutoPostBack="True" GroupName="GrupActa"/>
                </fieldset>
            </div>

                <div id="DivGeneral" runat="server" visible="true">
                    <div id="FiltroB" style="width: 95%; margin-left: 0px;">
                    <fieldset style="border-color: black; width: 95%; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Filtro Busqueda</legend>
                            <asp:TextBox ID="TxtFiltroB" runat="server" AutoPostBack="true" CssClass="TextBox" Height="22px" Width="183px"></asp:TextBox>
                            <asp:Button  ID="BtnFiltroB" runat="server" CssClass="Boton" Height="28px" Text="Busqueda" Width="100px" OnClientClick="BtnGenerarA_CheckedChanged"/>
  
                          <div>
                              <table style="width: 100%; margin-top: 30px;">
                                  <tr>
                                      <td>
                                          <asp:Label ID="LblNombreR" runat="server" CssClass="LabelGeneral" Text="Nombre del Resguardante: "></asp:Label>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="TxtNombreRes" runat="server" CssClass="TxtGeneral" onkeypress="return soloNumeros(event)" MaxLength="4" Width="410px"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <asp:Label ID="LblCargoR" runat="server" CssClass="LabelGeneral" Text="Cargo del Resguadante"></asp:Label>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="TxTCatgoR" runat="server" CssClass="TxtGeneral" onkeypress="return soloNumeros(event)" MaxLength="4" Width="410px"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <asp:Label ID="LnlAreaAdri"  runat="server" CssClass="LabelGeneral" Text="Area de Adcripción"></asp:Label>
                                      </td>
                                      <td>
                                          <asp:TextBox ID="TxtAreaAdri" runat="server" CssClass="TxtGeneral" onkeypress="return soloNumeros(event)" MaxLength="4" Width="410px"></asp:TextBox>
                                      </td>
                                  </tr>
                              </table>
                          </div>
                            
                    </fieldset>
                </div>
               </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
