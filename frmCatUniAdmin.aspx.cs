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
        CUnidadAdmin obJN = new CUnidadAdmin();
        BdUnidadAdmin obJE = new BdUnidadAdmin();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["__EVENTTARGET"] == "AccionVacio")
            {
                Rowindex.Value = string.Empty;
            }
            if(!this.IsPostBack)
            {
                IniciarLlenadoDropTipo();
                IniciarLlenadoUniNuevo();
                IniciarLlenadoSub();
                IniciarLlenadoTipo();
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


        /// <summary>
        /// llenado unidad administrativa nuevo
        /// </summary>
        public void IniciarLlenadoUniNuevo()
        {
            DataTable Datos = new DataTable();
            Datos = BdUnidadAdmin.ClasificaciónUA();
            ddlTipoNuevo.DataSource = Datos;
            ddlTipoNuevo.DataTextField = "Descripcion";
            ddlTipoNuevo.DataValueField = "IdAbreviatura";
            ddlTipoNuevo.DataBind();
            ddlTipoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlDistritoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlSubFondoN.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public void IniciarLlenadoSub()
        {
            DataTable Datos = new DataTable();
            Datos = BdUnidadAdmin.ObtenerSubFondo();
            ddlSubFondoN.DataSource = Datos;
            ddlSubFondoN.DataTextField = "SubFondo";
            ddlSubFondoN.DataValueField = "IdSubFondo";
            ddlSubFondoN.DataBind();
            ddlDistritoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            ddlSubFondoN.Items.Insert(0, new ListItem("Seleccionar", "0"));
        }

        public void IniciarLlenadoTipo()
        {
            DataTable Datos = new DataTable();
            Datos = BdUnidadAdmin.ObtenerTipo();
            ddlTipoSub.DataSource = Datos;
            ddlTipoSub.DataTextField = "Tipo";
            ddlTipoSub.DataValueField = "IdTipo";
            ddlTipoSub.DataBind();
            ddlTipoSub.Items.Insert(0, new ListItem("Seleccionar", "0"));
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
            BuscarTipo();
        }

        /// <summary>
        ///  Función para buscar Unidad Administrativa
        /// </summary>
        public void BuscarTipo()
        {
            try
            {
                string DescClasific = Convert.ToString(ddlTipo.SelectedItem);
                string Distrito = Convert.ToString(ddlDistrito.SelectedItem);

                DivResultados.Visible = true;

                if (ddlTipo.SelectedIndex != 0 & ddlDistrito.SelectedIndex != 0)
                {
                    GridTabla.DataSource = BdUnidadAdmin.ConsultarGbUD(DescClasific, Distrito);
                    GridTabla.DataBind();
                    
                }
                else
                {
                    GridTabla.DataSource = BdUnidadAdmin.ConsultarGbUnidad(DescClasific);
                    GridTabla.DataBind();

                    if (GridTabla.Rows.Count == 0)
                    {
                        MostrarMensaje("** No existen datos con la búsqueda solicitada **", "error", "Normal", "Incorrecto");
                        DivResultados.Visible = false;
                        LimpiarRegistros();
                    }
                }
               
            }
            catch (Exception ex)
            {
                MostrarMensaje("** Error en Base de Datos **", "error", "Normal", "Incorrecto");
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }



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

        public void LimpiarRegistros()
        {
            ddlTipo.SelectedIndex = 0;
            ddlDistrito.SelectedIndex = 0;
        }

        public void LimpiarNuevoR()
        {
            ddlTipoNuevo.SelectedIndex = 0;
            ddlDistritoNuevo.SelectedIndex = 0;
            ddlSubFondoN.SelectedIndex = 0;
            ddlTipoSub.SelectedIndex = 0;
            TxTCorreoN.Text = string.Empty;
            TxTNombreUniN.Text = string.Empty;
            TxtTelefonoNue.Text = string.Empty;
        }

        protected void ddlTipoNuevo_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(ddlTipoNuevo.SelectedItem.Text == "PRIMERA INSTANCIA")
            {
                this.ddlDistritoNuevo.Visible = true;
                this.LblDistritoN.Visible = true;
                DataTable Datos = new DataTable();
                Datos = BdUnidadAdmin.ObtenerDistritos();
                ddlDistritoNuevo.DataSource = Datos;
                ddlDistritoNuevo.DataTextField = "Distrito";
                ddlDistritoNuevo.DataValueField = "IdDistrito";
                ddlDistritoNuevo.DataBind();
                ddlDistritoNuevo.Items.Insert(0, new ListItem("Seleccionar", "0"));
            }
           else
            {
                this.ddlDistritoNuevo.Visible = false;
                this.LblDistritoN.Visible = false;
            }
        }

        protected void BtnNuevoR_Click(object sender, EventArgs e)
        {
            if (DivNuevoReg.Visible == false)
            {
                LimpiarRegistros();
                DivResultados.Visible = false;
                DivNuevoReg.Visible = true;
                BtnGuardar.Visible = true;
                BtnCancelar.Visible = true;
            }
            else if(DivNuevoReg.Visible == true)
            {
                DivNuevoReg.Visible = false;
                DivResultados.Visible = false;
                BtnGuardar.Visible = false;
                BtnCancelar.Visible = false;
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

            if (ddlTipoNuevo.SelectedIndex !=0)
            {
               /* if(ddlDistritoNuevo.SelectedIndex !=0)
                {*/
                    if(ddlSubFondoN.SelectedIndex !=0)
                    {
                        if(ddlTipoSub.SelectedIndex !=0)
                        {
                            if(TxTNombreUniN.Text != "")
                            {
                                if(TxtTelefonoNue.Text != "")
                                {
                                    if(TxTCorreoN.Text != "")
                                    {
                                    string IdSubFondo = Convert.ToString(ddlSubFondoN.SelectedValue);
                                    string IdAbreviatura = Convert.ToString(ddlTipoNuevo.SelectedValue);
                                    string Clasificacion = "";
                                    int Distrito = Convert.ToInt32(ddlDistritoNuevo.SelectedValue);
                                    string IdTipo = Convert.ToString(ddlTipoSub.SelectedValue);
                                    int IdEmpleado = 0;


                                    if (IdAbreviatura != "")
                                    {
                                        SqlConnection cnn3 = new SqlConnection(CConexion.Obtener());
                                        cnn3.Open();
                                        string consultar = "select IdAbreviatura, Abreviatura from Abreviaturas  where IdAbreviatura =" + IdAbreviatura;
                                        SqlCommand cmd2 = new SqlCommand(consultar, cnn3);
                                        using (var rd = cmd2.ExecuteReader())
                                        {
                                            while (rd.Read())
                                            {
                                                Clasificacion = rd["Abreviatura"].ToString();

                                            }
                                            cnn3.Close();
                                        }

                                    }

                                    SqlConnection cnn = new SqlConnection(CConexion.Obtener());
                                    bool sucess = false;
                                    SqlTransaction lTransaccion = null;
                                    int Valor_Retorno = 0;
                                    int Valor_Retorno_Insertar_Uni = 0;

                                    try
                                    {
                                       
                                        cnn.Open();
                                        lTransaccion = cnn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                                        SqlCommand cmd = new SqlCommand("SP_Insertar_UniAdmn", cnn, lTransaccion);

                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.Clear();

                                        cmd.Parameters.Add(new SqlParameter("@IdSubFondo", IdSubFondo));
                                        cmd.Parameters.Add(new SqlParameter("@UniAdmin", TxTNombreUniN.Text.ToUpper()));
                                        cmd.Parameters.Add(new SqlParameter("@Tipo", IdTipo));
                                        cmd.Parameters.Add(new SqlParameter("@Telefono", TxtTelefonoNue.Text.ToUpper()));
                                        cmd.Parameters.Add(new SqlParameter("@EMail", TxTCorreoN.Text));
                                        cmd.Parameters.Add(new SqlParameter("@Clasificacion", Clasificacion));
                                        cmd.Parameters.Add(new SqlParameter("@IdAbreviatura", IdAbreviatura));
                                        cmd.Parameters.Add(new SqlParameter("@IdEmpleado", IdEmpleado));

                                        SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                                        ValorRetorno.Direction = ParameterDirection.Output;
                                        cmd.Parameters.Add(ValorRetorno);

                                        SqlParameter IdUniParam = new SqlParameter("@IdUniInsertada", SqlDbType.Int);
                                        IdUniParam.Direction = ParameterDirection.Output;
                     
                                        cmd.ExecuteNonQuery();

                                        Valor_Retorno_Insertar_Uni = Convert.ToInt32(ValorRetorno.Value);
                                        int IdUni = Convert.ToInt32(IdUniParam.Value);

                                        if (Distrito != 0 & IdUni != 0)
                                        {
                                            

                                            cmd = new SqlCommand("SP_Insertar_Uni_UADistrito", cnn, lTransaccion);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.Clear();

                                            cmd.Parameters.Add(new SqlParameter("@IdUniAdmin", IdUni));
                                            cmd.Parameters.Add(new SqlParameter("@IdDistrito", Distrito));

                                            ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                                            ValorRetorno.Direction = ParameterDirection.Output;
                                            cmd.ExecuteNonQuery();
                                            cmd.Parameters.Add(ValorRetorno);
                                            Valor_Retorno = Convert.ToInt32(ValorRetorno.Value);

                                            if (Valor_Retorno == 1 & Valor_Retorno_Insertar_Uni ==1)
                                            {
                                                sucess = true;
                                            }
                                            else
                                            {
                                                sucess = false;
                                            }

                                        }



                                        //cmd.ExecuteNonQuery();


                                        Valor_Retorno = Convert.ToInt32(ValorRetorno.Value);

                                        if(Valor_Retorno == 1)
                                        {
                                            sucess = true;
                                        }
                                        else 
                                        {
                                            sucess = false;
                                        }
             

                                    }
                                    catch (Exception ex)
                                        {
                                            MostrarMensaje(ex.Message, "error", "Normal", "Incorrecto");
                                        }
                                        finally
                                        {
                                            if(sucess)
                                            {
                                                lTransaccion.Commit();
                                                
                                                MostrarMensaje("** Unidad Administrativa guardada correctamente **", "error", "Normal", "Incorrecto");
                                                DivNuevoReg.Visible = false;
                                                BtnGuardar.Visible = false;
                                                BtnCancelar.Visible = false;
                                               cnn.Close();
                                            LimpiarNuevoR();
                                                
                                            }
                                            else 
                                            {
                                                lTransaccion.Rollback();
                                            cnn.Close();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MostrarMensaje("** Seleccione el campo nómina **", "error", "Normal", "Incorrecto");
                                    }
                                }
                                else 
                                {
                                    MostrarMensaje("** Seleccione el campo nómina **", "error", "Normal", "Incorrecto");
                                }
                            }
                            else 
                            {
                                MostrarMensaje("** Seleccione el campo nómina **", "error", "Normal", "Incorrecto");
                            }
                        }
                        else
                        {
                            MostrarMensaje("** Seleccione el campo nómina **", "error", "Normal", "Incorrecto");
                        }
                    }
                    else 
                    {
                        MostrarMensaje("** Seleccione el campo nómina **", "error", "Normal", "Incorrecto");
                    }
                /*}
                else 
                {
                    MostrarMensaje("** Seleccione el campo nómina **", "error", "Normal", "Incorrecto");
                }*/
            }
            else
            {
                MostrarMensaje("** Seleccione el campo nómina **", "error", "Normal", "Incorrecto");
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
           LimpiarNuevoR();
            DivNuevoReg.Visible = false;
            BtnGuardar.Visible = false;
            BtnCancelar.Visible = false;
        }

        public void EliminarUnidad()
        {
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand("SP_Eliminar_UniAdmin", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@IdUniAdmin", obJE.IDUniAdmin);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

    }

   

}