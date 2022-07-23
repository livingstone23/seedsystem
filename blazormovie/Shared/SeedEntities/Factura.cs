using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.Shared.SeedEntities
{
    public class Factura
    {
        public bool updateTotal = false;
        public Factura()
        {

        }
        public Factura(Factura factura)
        {
            FacturaLinea = new List<FacturaLinea>(factura.FacturaLinea);
            IdFactura = factura.IdFactura;
            Direccion = factura.Direccion;  
            Comment = factura.Comment;
            LogoId =   factura.LogoId;
            FechaCreacion = factura.FechaCreacion;
            FechaVencimiento = factura.FechaVencimiento;
            Descuento = factura.Descuento;
            Estado = factura.Estado;
            IdCliente = factura.IdCliente;

        }
        public int IdFactura { get; set; }
        public string NumeroFactura { get; set; }
        
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Comment { get; set; }
        public string Direccion { get; set; }
        public string LogoId{ get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int Estado { get; set; }
        public double Descuento { get; set; }
        public double Total => CalculateTotal(FacturaLinea);
        [NotMapped]
        public List<FacturaLinea> FacturaLinea { get; set; } = new List<FacturaLinea>();
        [NotMapped]
        public Client cliente { get; set; }

        double CalculateTotal(List<FacturaLinea> _items)
        {
            try
            {
                double total = Math.Round(_items.Sum(items=>items.Total),2);
                if (Descuento != 0)
                {
                    double discount = total * (1 / Descuento);
                    return Math.Round(total - discount, 2);
                }
                return total;
            }catch(Exception ex)
            {
                return 0;

            }

        }
    }
}
