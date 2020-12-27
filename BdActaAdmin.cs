using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;
using System.Data.SqlClient;
using System.Data;
using InventariosPJEH.CAccesoDatos;


namespace InventariosPJEH.CAccesoDatos
{
    public class BdActaAdmin
    {

        private static string error;

        public static bool InsertarActa(int idAreaResguardante, int idResguardo, String NumActa, String fechaActa)
        {
            SqlTransaction lTransaccion = null;
            SqlConnection Conn = new SqlConnection(CConexion.Obtener());
            int Valor_Retornado = 0;
            bool success = false;
            try
            {
                Conn.Open();
                
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);
                //Especificamos el comando, en este caso el nombre del Procedimiento Almacenado, lTransaccion
                SqlCommand cmd = new SqlCommand("SP_Insertar_Acta", Conn, lTransaccion);
                //SqlCommand cmd = new SqlCommand("SP_Insertar_Personal1", Conn);

                //Se indica al tipo de comando que es de tipo Procedimiento Alamcenado
                cmd.CommandType = CommandType.StoredProcedure;
                //Se limpian los parametros
                cmd.Parameters.Clear();
                //Comienza a mandar a cada uno de los parametros, deben de enviarse en el tipo
                //de datos que coincida es sql server 
                cmd.Parameters.Add(new SqlParameter("@NumActa", NumActa));
                cmd.Parameters.Add(new SqlParameter("@FechaAlta", fechaActa));
                cmd.Parameters.Add(new SqlParameter("@IdUniAdmin", idAreaResguardante));
                cmd.Parameters.Add(new SqlParameter("@IdResguardo", idResguardo));
                //declaramos el valor de retorno
                SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
                //asigamos el valor de retorno
                ValorRetorno.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(ValorRetorno);
                //Se executa la consulta
                cmd.ExecuteNonQuery();
                Valor_Retornado = Convert.ToInt32(ValorRetorno.Value);
                //Dependiendo del valor de retorno la variable success si el procedimiento retorna un 1 la operación se realizó
                //con exito de no ser así s emantiene en false y por lo tanto falló la operación
                if (Valor_Retornado == 1)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
                return success;
            }
            catch(Exception e)
            {
                error = e.Message;
                Console.WriteLine(e.ToString());
                Conn.Close();
                return false;
            }
            finally
            {
                if (success)
                {
                    lTransaccion.Commit();
                    Conn.Close();
                }
                else
                {
                    lTransaccion.Rollback();
                    Conn.Close();
                }
            }
        }

        public static Dictionary<CDatosResguardante, List<CDatosBienesActa>> MostrarBusqueda(long IdResguardo)
        {


            CComparatorResguardante cComparatorResguardante = new CComparatorResguardante();
            Dictionary<CDatosResguardante, List<CDatosBienesActa>> resultadoDatos = new Dictionary<CDatosResguardante, List<CDatosBienesActa>>(cComparatorResguardante);            
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());


            try
            {

                cnn.Open();

                SqlCommand cmd = new SqlCommand("SELECT IdEmpleado, Nombre, IdUniAdmin, UniAdmin, Cargo, IdResguardo, Actividad, NumInventario, DescripcionBien, Marca, Modelo, Serie, TipoPartida FROM Vta_buscarNolocalizados WHERE IdResguardo = @IdResguardo  ", cnn);

                cmd.Parameters.Add("@IdResguardo", SqlDbType.BigInt).Value = IdResguardo;
                
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CDatosResguardante datosResguardante = new CDatosResguardante();
                        CDatosBienesActa cDatosBienActa = new CDatosBienesActa();                        

                        datosResguardante.txtONombreResguardante = rd["Nombre"].ToString();
                        datosResguardante.txtOAreaAdscripcionResguardo = rd["UniAdmin"].ToString();
                        datosResguardante.txtOCargoResguardo = rd["Cargo"].ToString();
                        datosResguardante.idResguardo = int.Parse(rd["IdResguardo"].ToString());
                        datosResguardante.idAreaAdscrip = int.Parse(rd["IdUniAdmin"].ToString());

                        cDatosBienActa.txtOInventarioResguardo = rd["NumInventario"].ToString();
                        cDatosBienActa.txtONombreBienResguardo = rd["DescripcionBien"].ToString();
                        cDatosBienActa.txtOMarcaResguardo = rd["Marca"].ToString();
                        cDatosBienActa.txtOModeloResguardo= rd["Modelo"].ToString();
                        cDatosBienActa.txtOSerieResguardo = rd["Serie"].ToString();

                        List<CDatosBienesActa> listaBienesResult = new List<CDatosBienesActa>();

                        if (resultadoDatos.TryGetValue(datosResguardante, out listaBienesResult))
                        {
                            listaBienesResult.Add(cDatosBienActa);
                        }
                        else
                        {
                            List<CDatosBienesActa> listaBienes = new List<CDatosBienesActa>();
                            listaBienes.Add(cDatosBienActa);
                            resultadoDatos.Add(datosResguardante, listaBienes);
                        }
                                                
                    }
                }
            }
            catch (Exception e)

            {
                error = e.Message;
                Console.WriteLine(e.ToString());
                cnn.Close();
            }
            finally
            {
                cnn.Close();
            }
            return resultadoDatos;

        }

    }
}
