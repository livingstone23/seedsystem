using blazormovie.Client.Auth;
using blazormovie.Client.Helpers;
using blazormovie.Client.Repository;
using blazormovie.Client.Services.Interface.ModBudget;
using blazormovie.Client.Services.ModBudget;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace blazormovie.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //Servicios
            //builder.Services.AddScoped<IBudgetService, BudgetService>();
            //builder.Services.AddScoped<ICategoryService, CategoryService>();
            //builder.Services.AddScoped<IInitiativeService, InitiativeService>();
            //builder.Services.AddScoped<IPOSPayService, POSPayService>();
            //builder.Services.AddScoped<IProjectService, ProjectService>();

            ConfigureServices(builder.Services);

            builder.Services.AddMudServices();

            await builder.Build().RunAsync();

        }

        private static void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IRepositorio, Repositorio>();
            services.AddScoped<IMostrarMensajes, MostrarMensajes>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBudgetService, BudgetService>();
            services.AddScoped<IInitiativeService, InitiativeService>();
            services.AddScoped<IPOSPayService, POSPayService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectCostService, ProjectCostService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IGroupsService, GroupsService>();
            services.AddScoped<ICostService, CostService>();

            services.AddAuthorizationCore();

            services.AddScoped<ProveedorAutenticacionJWT>();

            services.AddScoped<AuthenticationStateProvider, ProveedorAutenticacionJWT>(
                provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());

            services.AddScoped<ILoginService, ProveedorAutenticacionJWT>(
                provider => provider.GetRequiredService<ProveedorAutenticacionJWT>());

            services.AddScoped<RenovadorToken>();

        }
    }
}
