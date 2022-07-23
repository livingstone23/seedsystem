using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Shared.SeedEntities;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace blazormovie.Client.Services.ModBudget
{
    public class FacturaService : IFacturaService
    {
        private readonly HttpClient _httpClient;

        public FacturaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Save(Factura factura)
        {
            if (factura.IdFactura == 0)
                await _httpClient.PostAsJsonAsync<Factura>($"api/Factura", factura);
        }

        public Task SaveFacutraLinea(FacturaLinea facturaLineas)
        {
            throw new System.NotImplementedException();
        }
    }
}
