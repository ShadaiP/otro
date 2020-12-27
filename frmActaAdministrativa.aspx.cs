using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using InventariosPJEH.CAccesoDatos;
using InventariosPJEH.CNegocios;
using System.Web.UI;

namespace InventariosPJEH
{
    public partial class frmActaAdministrativa : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            BtnFiltroB_Click();
        }

        protected void BtnGenerarA_CheckedChanged(object sender, EventArgs e)
        {
            DivTablaResultadosResguardo.Visible = false;
            RadioButton RadioSeleccionado = sender as RadioButton;

            if (RadioSeleccionado.ID == "BtnGenerarA")
            {
                DivGeneral.Visible = true;

            }

        }

        protected void BtnFiltroB_Click()
        {
            List<string> listaErrores = ValidarDatos();

            if (listaErrores.Count > 0)
            {
                string error = "";
                foreach (var itemError in listaErrores)
                {
                    error += " - " + itemError;
                }
                MostrarMensaje($"DATOS FALTANTES:  {error}", "info", "Intervalo", "Inicio");
                // DivMostrar.Visible = false;

            }
            else
            {
                Dictionary<CDatosResguardante, List<CDatosBienesActa>> resBusqueda = MostrarFiltro();
                DivTablaResultadosResguardo.Visible = true;
                if(resBusqueda.Count > 0)
                {
                    sectionBienes.Visible = true;
                    foreach (KeyValuePair<CDatosResguardante, List<CDatosBienesActa>> entry in resBusqueda)
                    {
                        // do something with entry.Value or entry.Key
                        lblNombreResul.Text = entry.Key.txtONombreResguardante;
                        lblCargoResul.Text = entry.Key.txtOCargoResguardo;
                        lblAreaResul.Text = entry.Key.txtOAreaAdscripcionResguardo;
                        lblIdResguardo.Text = entry.Key.idResguardo.ToString();
                        lblIdUniAdmin.Text = entry.Key.idAreaAdscrip.ToString();
                        gridResultados.DataSource = entry.Value;
                        gridResultados.DataBind();
                        sectionGuardarActa.Visible = true;
                        if (gridResultados.Rows.Count == 0)
                        {
                            MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                        }                        
                    }
                }
                else
                {
                    sectionBienes.Visible = false;
                    MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                }
                
              
            }

        }

        private List<string> ValidarDatos()
        {
            List<string> LErrores = new List<string>();

            if (string.IsNullOrEmpty(TxtFiltroB.Text))
            {
                LErrores.Add("Numero de Inventario");
            }
            return LErrores;
        }

        protected void MostrarMensaje(string Mensaje, string Tipo, string TipoFuncion, string ClaveMsj)
        {
            string Msj = "";
            if (TipoFuncion == "Normal")
                Msj = "MostrarMensaje('" + Mensaje + "', '" + Tipo + "');";

            else if (TipoFuncion == "NotificacionEliminar")
                //    Msj = "prueba2('" + Mensaje + "', '" + Tipo + "');";
                Msj = "confirm();";
            else
                Msj = "MostrarMensajeInterval('" + Mensaje + "', '" + Tipo + "');";


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), ClaveMsj, Msj, true);
        }

        public Dictionary<CDatosResguardante, List<CDatosBienesActa>> MostrarFiltro()
        {
            Dictionary<CDatosResguardante, List<CDatosBienesActa>> lstActaAdmin = BdActaAdmin.MostrarBusqueda(Convert.ToInt64(TxtFiltroB.Text));
            return lstActaAdmin;
        }

        public void limpiarCampos()
        {
            lblNombreResul.Text = "";
            lblCargoResul.Text = "";
            lblAreaResul.Text = "";
            lblIdResguardo.Text = "";
            lblIdUniAdmin.Text = "";
            txtNumActa.Text = "";
            txtFechaAdquisicion.Text = "";
            TxtFiltroB.Text = "";
            gridResultados.DataSource = null;
            gridResultados.DataBind();
            sectionBienes.Visible = false;
        }

        protected void BtnGuardarActa_Click(object sender, EventArgs e)
        {
            //Capturamos los errores con try catch
            try
            {
                if(!BdActaAdmin.InsertarActa(int.Parse(lblIdUniAdmin.Text), int.Parse(lblIdResguardo.Text), txtNumActa.Text, txtFechaAdquisicion.Text))
                {
                    MostrarMensaje("** Error en Base de Datos **", "error", "Normal", "Incorrecto");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            //Capturamos los errores con try catch
            try
            {
                limpiarCampos();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}