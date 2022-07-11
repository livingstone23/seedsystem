using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class FacturaLinea
    {
        public int IdFacturaLinea{ get; set; }
        public int IdFactura{ get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public double Descuento { get; set; }
        public double Impuestos { get; set;}
        public double Total { get; set; }
        public DateTime FechaCreacion { get; set; }
        public double Cantidad { get; set; }
        public string NumeroFactura { get; set; }
    }
}
