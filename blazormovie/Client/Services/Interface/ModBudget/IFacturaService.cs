using blazormovie.Shared.SeedEntities;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.Interface.ModBudget
{
    public interface IFacturaService
    {
        Task Save(Factura factura);
        Task SaveFacutraLinea(FacturaLinea facturaLineas);
    }
}
