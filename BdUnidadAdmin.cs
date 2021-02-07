using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using InventariosPJEH.CNegocios;
using System.Configuration;
using System.Windows;
using System.Web.UI;
using InventariosPJEH.CVista;
using InventariosPJEH.CAccesoDatos;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdUnidadAdmin
    {
        
        public SqlConnection conexion;
        private static string error;
        protected SqlDataAdapter adaptador;
        protected SqlDataReader reader;
        protected DataSet data;
        
        /*public BdUnidadAdmin()
        {
            this.conexion = ConexionBD.getConexion();
        }*/

        /// <summary>
        /// Selecciona los Tipos de Unidades Administrativas
        /// </summary>
        /// <returns></returns>
        /// 
        public static DataTable ClasificaciónUA()
        {
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter adp = new SqlDataAdapter();
            cmd.CommandText = "select DISTINCT idAbreviatura, Descripcion from Abreviaturas where tipo = 'ClasificaciónUA' ORDER BY Descripcion ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;
        }
        /// <summary>
        /// Selecciona Distritos
        /// </summary>
        /// <returns></returns>
        /// 

        public static DataTable ObtenerDistritos()
        {
            SqlCommand cmd = new SqlCommand();
            DataTable Tabla = new DataTable("Resultado");
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            SqlDataAdapter adp = new SqlDataAdapter();

            cmd.CommandText = "select IdDistrito, Distrito from Cat_Distritos ORDER BY Distrito ASC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            adp.SelectCommand = cmd;
            cnn.Open();
            adp.Fill(Tabla);
            cnn.Close();
            return Tabla;

        }
        
       
        public static List<CUnidadAdmin> ConsultarGbUnidad(string DescClasific)
        {
            List<CUnidadAdmin> Uni = new List<CUnidadAdmin>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            //SqlCommand cmd = new SqlCommand();
            //SqlDataAdapter adp = new SqlDataAdapter();

             try
             {

             cnn.Open();
             SqlCommand cmd = new SqlCommand(string.Format("SELECT IdUniAdmin, UniAdmin, IdSubFondo, SubFondo, Tipo, DescTipo, Telefono, EMail, Clasificacion, DescClasific, IdEmpleado FROM Vta_UniAdmin where DescClasific like '%{0}%' ORDER BY UniAdmin ASC ", DescClasific), cnn);
                 using (var rd = cmd.ExecuteReader()) 
                  {
                    while(rd.Read())
                    {
                        CUnidadAdmin UniA = new CUnidadAdmin();
                        UniA.IdClasificacion = int.Parse(rd["IdUniAdmin"].ToString());
                        UniA.UnidadAdministrativa = rd["UniAdmin"].ToString();

                        Uni.Add(UniA);
                    
                    
                    }
            
                  }
             }
              catch (Exception e)

              {
                  error = e.Message;
                  Console.WriteLine(e.ToString());
                  cnn.Close();
                  throw new Exception();
              }
              finally
              {
                  cnn.Close();
              }

            return Uni;
        }

    }
}