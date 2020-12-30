using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventariosPJEH.CNegocios;
using System.Data.SqlClient;

namespace InventariosPJEH.CAccesoDatos
{
    public class BdHistoricoPersonal
    {
        private static string error;
        public static List<CHistoricoPersonal> MostrarHistorialPersonal(string Nombre, string APaterno, string AMaterno)
        {
            List<CHistoricoPersonal> lista = new List<CHistoricoPersonal>();
            SqlConnection cnn = new SqlConnection(CConexion.Obtener());
            try
            {
                cnn.Open();

                
                String query = "SELECT ClaveEmpleado, Nombre+APaterno+AMaterno as NombreCompleto, Cargo, UniAdmin, Fecha FROM Vta_HistoricoPersonal WHERE ";
                if (Nombre.Length > 0)
                {
                    query += " (Nombre LIKE %@Nombre%) OR";
                }
                if (APaterno.Length > 0)
                {
                    query += "(APaterno LIKE %@APaterno%) OR";
                }
                if (AMaterno.Length > 0)
                {
                    query += "(AMaterno LIKE %@AMaterno%) OR";
                }
                if (query.EndsWith("R"))
                {
                    query.Substring(0, query.Length - 3);
                }

                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                cmd.Parameters.Add(new SqlParameter("@APaterno", APaterno));
                cmd.Parameters.Add(new SqlParameter("@AMaterno", AMaterno));
               /* cmd.Parameters.Add("@Nombre",System.Data.SqlDbType.Char).Value = Nombre;
                cmd.Parameters.Add("@APaterno", System.Data.SqlDbType.Char).Value = APaterno;
                cmd.Parameters.Add("@AMaterno", System.Data.SqlDbType.Char).Value = AMaterno;*/
               

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        CHistoricoPersonal cHistorico = new CHistoricoPersonal();
                        cHistorico.NombreCompleto = rd["NombreCompleto"].ToString();
                        cHistorico.Cargo = rd["Cargo"].ToString();
                        cHistorico.UniAdmin = rd["UniAdmin"].ToString();
                        cHistorico.Fecha = BdConverter.FieldToDate(rd["Fecha"]);
                        cHistorico.ClaveEmpleado = int.Parse(rd["ClaveEmpleado"].ToString());
                         /*cHistorico.NombreCompleto = Nombre + " " + APaterno + " " + AMaterno;*/
                         lista.Add(cHistorico);
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