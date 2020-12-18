using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;
using System.Data.SqlClient;
using System.Data;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdActaAdmin
    {

        private static string error;
        public static List<CActaAdmin> MostrarBusqueda(long IdResguardo)
        {

            List<CActaAdmin> lista = new List<CActaAdmin>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());


            try
            {

                cnn.Open();

                SqlCommand cmd = new SqlCommand("SELECT dEmpleado, Nombre, UniAdmin, Cargo, IdResguardo, Actividad, NumInventario, DescripcionBien, Marca, Modelo, Serie, TipoPartida FROM  Vta_buscarNolocalizados = @IdResguardo  ", cnn);

                cmd.Parameters.Add("@IdResguardo", SqlDbType.BigInt).Value = IdResguardo;

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CActaAdmin cActa = new CActaAdmin();

                        cActa.TxtFiltroB = BdConverter.FieldToInt64(rd["IdResguardo"]);
                        cActa.TxtNombreRes = rd["Nombre"].ToString();
                        cActa.UTxtAreaAdri = rd["UniAdmin"].ToString();
                        cActa.TxTCatgoR = rd["Cargo"].ToString();

                        lista.Add(cActa);
                    }
                }
            }
            catch (Exception e)

            {
                error = e.Message;
                cnn.Close();
            }
            return lista;

        }

    }
}
