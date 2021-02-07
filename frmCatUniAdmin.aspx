<%@ Page Title="" Language="C#" MasterPageFile="~/Inventarios1.Master" AutoEventWireup="true" CodeBehind="frmCatUniAdmin.aspx.cs" Inherits="InventariosPJEH.frmCatUniAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style5 {
            width: 82%;
            height: 43px;
        }
        .auto-style7 {
            width: 95%;
            height: 136px;
        }
        .auto-style8 {
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
    <asp:Label ID="Lbld" runat="server"></asp:Label>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div id="Contenedor" style="width:auto; height:auto; text-align:center">
               
                <asp:HiddenField ID="Rowindex" runat="server" />

                <div style="width: auto;">
                   
                    <fieldset class="auto-style7" style="border-color: black; margin-left: 11px;">
                        <legend style="width: auto; color: darkblue; font-size: 12px;">Buscar</legend>
                        <div class="auto-style5">
                            <table style="margin-left: 100px" class="auto-style8">
                                <tr>
                                    <td class="auto-style13" style="text-align:right">
                                        <asp:Label ID="LabelTipo" runat="server" CssClass="LabelGeneral" Text="Tipo de área: "></asp:Label>
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="true" CssClass="DropGeneral" Height="20px" Width="420px" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style13" style="text-align:right;">
                                        <asp:Label ID="LabelDistrito" runat="server" CssClass="LabelGeneral" Text="Distrito: " Visible="false"></asp:Label>
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:DropDownList ID="ddlDistrito" runat="server" AutoPostBack="true" CssClass="DropGeneral" Visible="false" Height="20px" Width="420px" OnSelectedIndexChanged="ddlDistrito_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style8">
                                        <asp:Button ID="BtnBuscar" runat="server" CssClass="Boton" Style="margin-top: 15px; align-content: center;" Height="33px" Width="98px" Text="Buscar" OnClick="BtnBuscar_Click"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>

                <div id="DivTabla" runat="server" style="width: 100%;" visible="false">
                    <fieldset style="height:auto">

                        <asp:GridView id="GridBuscarT" runat="server" CssClass="StyleGridV" Height="142px" Width="100%" AutoGenerateColumns="false" 
                             DataKeyNames="IdUniAdmin,UniAdmin,IdSubFondo,SubFondo,Tipo,DescTipo,Telefono,EMail,Clasificacion,DescClasific,IdEmpleado">
                            <Columns>
                                <asp:BoundField DataField="IdUniAdmin" HeaderText="iD"/>
                                <asp:BoundField DataField="UniAdmin" HeaderText="Unidad Administrativa"/>

                            </Columns>
                        </asp:GridView>
                    </fieldset>

                </div>

                <div id="MostrarT" runat="server">
                    <asp:Label ID="MostrarM" runat="server"></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="CPHTitulo">
    <div style="width: auto; text-align: center; background-color: #d8d8d8;">
        <asp:Label ID="LblLema" runat="server" Text="Unidad Administrativa" Font-Size="Large" Font-Bold="True" Font-Italic="True" CssClass="LblLema"></asp:Label>
    </div>
</asp:Content>


