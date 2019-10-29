using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FizzBuzz
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddResponseCompression();

            services.AddMvc().AddControllersAsServices().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressInferBindingSourcesForParameters = true;
            }); 
            
            services.AddSingleton(Configuration.Get<DefaultsConfig>());
            services.AddSingleton<IFizzBuzzService, FizzBuzzService>();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseResponseCaching();
            app.UseResponseCompression();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
