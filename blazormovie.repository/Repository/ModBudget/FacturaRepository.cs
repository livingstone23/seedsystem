using blazormovie.repository.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazormovie.repository.Repository.ModBudget
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly IDbConnection _dbConnection;
        public FacturaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<bool> Insert(Factura factura)
        {
            var sql = @"INSERT INTO Factura (NumeroFactura,IdCliente,Comment,Direccion,LogoId,FechaCreacion,FechaVencimiento,Estado,Descuento,Total)
                        VALUES (@NumeroFactura,@IdCliente,@Comment,@Direccion,@LogoId,@FechaCreacion,@FechaVencimiento,@Estado,@Descuento,@Total)";
            var result = await _dbConnection.ExecuteAsync(sql,
                new { 
                    NumeroFactura = factura.NumeroFactura,
                    IdCliente = factura.IdCliente,
                    Comment = factura.Comment,
                    Direccion = factura.Direccion,
                    LogoId = factura.LogoId,
                    FechaCreacion = factura.FechaCreacion,
                    FechaVencimiento = factura.FechaVencimiento,
                    Estado = factura.Estado,
                    Descuento = factura.Descuento,
                    Total = factura.Total});
            return result > 0;
        }

        public async Task<bool> InsertFacutraLinea(FacturaLinea facturaLineas)
        {
            var sql = @"INSERT INTO Factura (IdFactura,Titulo,Descripcion,Precio,Descuento,Impuestos,Total,FechaCreacion,Cantidad,NumeroFactura)
                        VALUES (@IdFactura,@Titulo,@Descripcion,@Precio,@Descuento,@Impuestos,@Total,@FechaCreacion,@Cantidad,@NumeroFactrua)";

            var result = await _dbConnection.ExecuteAsync(sql, new
            {
                IdFactura = facturaLineas.IdFactura,
                Titulo = facturaLineas.Titulo,
                Descripcion = facturaLineas.Descripcion,
                Precio = facturaLineas.Precio,
                Descuento = facturaLineas.Descuento,
                Impuestos = facturaLineas.Impuestos,
                Total = facturaLineas.Total,
                FechaCreacion = facturaLineas.FechaCreacion,
                Cantidad = facturaLineas.Cantidad,
                NumeroFactura = facturaLineas.NumeroFactura
            });
            
            return result > 0;
        }
    }
}
