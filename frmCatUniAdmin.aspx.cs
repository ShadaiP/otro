using InventariosPJEH.CVista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using InventariosPJEH.CAccesoDatos;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Threading.Tasks;
using InventariosPJEH.CNegocios;
using System.Data.Odbc;

namespace InventariosPJEH
{
    public partial class frmCatUniAdmin : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;
            }
            if(!this.IsPostBack)
            {
                IniciarLlenadoDropTipo();
            }
          
        }
        //Llenado inicial
        private void IniciarLlenadoDropTipo()
        {
            DataTable Datos = new DataTable();
            Datos = BdUnidadAdmin.ClasificaciónUA();
            ddlTipo.DataSource = Datos;
            ddlTipo.DataTextField = "Descripcion";
            ddlTipo.DataValueField = "IdAbreviatura";
            ddlTipo.DataBind();
            ddlTipo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlDistrito.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlTipo.SelectedItem.Text == "PRIMERA INSTANCIA")
            {
                this.LabelDistrito.Visible = true;
                this.ddlDistrito.Visible = true;
                DataTable Datoos = new DataTable();
                Datoos = BdUnidadAdmin.ObtenerDistritos();
                ddlDistrito.DataSource = Datoos;
                ddlDistrito.DataTextField = "Distrito";
                ddlDistrito.DataValueField = "IdDistrito";
                ddlDistrito.DataBind();
                ddlDistrito.Items.Insert(0, new ListItem("Seleccionar", "0"));

            }
            else 
            {
                this.LabelDistrito.Visible = false;
                this.ddlDistrito.Visible = false;
            }
        }

       
        protected void ddlDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdDistrito = Convert.ToInt32(ddlDistrito.SelectedValue);
           
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            /*try
            {*/
                if (ddlTipo.SelectedIndex != 0)
                {
                    //Parametros de busqueda
                    String DescClasific = Convert.ToString(ddlTipo.SelectedItem);

                

                MostrarT.Visible = true;
                MostrarM.Text = DescClasific;
                   GridBuscarT.DataSource = BdUnidadAdmin.ConsultarGbUnidad(DescClasific);
                    GridBuscarT.DataBind();

                    DivTabla.Visible = true;
                    if (GridBuscarT.Rows.Count == 0)
                    {
                        MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                    }

                }
                else
                {
                    MostrarMensaje("** Campos Vacíos **", "error", "Normal", "Incorrecto");
                }
            /* }
           catch(Exception ex)
            {
                MostrarMensaje("** Error en Base de Datos **", "error", "Normal", "Incorrecto");
                Console.WriteLine(ex.ToString());
                throw ex;
            }*/
        }

        /// <summary>
        ///  Función para buscar Unidad Administrativa
        /// </summary>




        /// <summary>
        /// Mensajes de error o confirmacion
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="Tipo"></param>
        /// <param name="TipoFuncion"></param>
        /// <param name="ClaveMsj"></param>

        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion, string ClaveMsj)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";
            else if (TipoFuncion == "NotificacionEliminar")
                Msj = "confirm();";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }


    }

   

}