using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventariosPJEH.CNegocios
{
    public class CUnidadAdmin
    {
        public string Tipo { get; set; }
        public int IdUniAdmin { get; set; }
        public string UnidadAdministrativa { get; set; }
        public int IdClasificacion { get; set; }
        public string Clasificacion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string SubFondo { get; set; }
        public string Descripcion { get; set; }
        public int IdDescClasific { get; set; }
        public string DescClasific { get; set; }
}
}