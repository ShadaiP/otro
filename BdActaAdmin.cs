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
        public static Dictionary<CDatosResguardante, List<CDatosBienesActa>> MostrarBusqueda(long IdResguardo)
        {


            CComparatorResguardante cComparatorResguardante = new CComparatorResguardante();
            Dictionary<CDatosResguardante, List<CDatosBienesActa>> resultadoDatos = new Dictionary<CDatosResguardante, List<CDatosBienesActa>>(cComparatorResguardante);            
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());


            try
            {

                cnn.Open();

                SqlCommand cmd = new SqlCommand("SELECT IdEmpleado, Nombre, UniAdmin, Cargo, IdResguardo, Actividad, NumInventario, DescripcionBien, Marca, Modelo, Serie, TipoPartida FROM Vta_buscarNolocalizados WHERE IdResguardo = @IdResguardo  ", cnn);

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
            return resultadoDatos;

        }

    }
}
