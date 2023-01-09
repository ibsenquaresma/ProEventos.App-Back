using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProEventos.Application.Contratos;
using ProEventos.Application;
using ProEventos.Persistence;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;
using System.Text.Json.Serialization;

namespace ProEventos.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Adicionado referencia do DATA CONTEXT - *Curso
            services.AddDbContext<ProEventosContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            //

            services.AddControllers()
                                .AddJsonOptions(options =>
                                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                                )
                                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
                                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
                                );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Service
            services.AddScoped<IEventoService, EventoService>();

            //Persist
            services.AddScoped<IGeralPersist, GeralPersist>();
            services.AddScoped<IEventoPersist, EventoPersist>();

            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(
                x => x.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowAnyOrigin()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}