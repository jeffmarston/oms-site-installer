using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Eze.AdminConsole.Model;
using Eze.AdminConsole.Utils;

namespace Eze.AdminConsole
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                //    .AllowAnyOrigin();
                .WithOrigins("http://localhost:8080", "http://localhost:8081");
            }));
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {         
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ServiceMgmtHub>("/ServiceMgmtHub");
            });

            app.UseMvcWithDefaultRoute();

        }
    }
}
