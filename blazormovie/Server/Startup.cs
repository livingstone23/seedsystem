using blazormovie.repository.Interface.ModBudget;
using blazormovie.repository.Repository.ModBudget;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Text;
using blazormovie.Client.Services.Interface.ModBudget;

namespace blazormovie.Server
{
    public class Startup
    {
        public IConfiguration configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Habilitamos el contexto para el uso del sql server
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            //Codigo de configuracion 
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Codigo de configuracion del jsonwtoken valida si es un tokenvalido
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["jwt:key"])),
                     ClockSkew = TimeSpan.Zero
                 });


            //Habilito la utilizacion de autoMapper
            services.AddAutoMapper(typeof(Startup));

            // Para manejar referencias circulares
            services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



            //Leemos la cadena de conexion
            string dbConnectionString = this.configuration.GetConnectionString("DefaultConnection");
            //Pasamos la cadena para habilitar la conexion
            services.AddSingleton<IDbConnection>((sp) => new SqlConnection(dbConnectionString));


            //Repositorios
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IGroupsRepository, GroupsRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IInitiaiveRepository, InitiativeRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IPOSPayRepository, POSPayRepository>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            //1.1 Habilitar Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //1.2 Habilitar Swagger
            app.UseSwaggerUI();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //1.3 Habilitar Swagger
            app.UseSwagger();

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
