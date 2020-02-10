using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpdesk.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Helpdesk.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            StartupExtension.Config = configuration;
        }

        public IConfiguration Configuration { get; }
        public readonly string allowSpecificOrigin = "AllowSpecificOrigin";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors(allowSpecificOrigin);
            services.ConfigureAuthentication();
            services.AddMemoryCache();
            services.AddOptions();

            services.AddControllers();
            services.AddDbContext<AppDBContext>(options =>
            {
                options.UseInMemoryDatabase(Configuration.GetConnectionString("memory"));
            });
            services.ConfigureDependencyInjection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(allowSpecificOrigin);
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
