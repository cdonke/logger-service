using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using Itau.MX4.Logger.Providers.STLog;
using System.Text.Json.Serialization;

namespace Itau.MX4.Logger.Service
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

            services.AddControllers()
                .AddJsonOptions(o => o
                    .JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                );
            

            services.AddLogging(config =>
            {
                config.AddConfiguration(Configuration.GetSection("Logging"));
                config.AddSTLog();
#if DEBUG
                config.AddDebug();
#endif

                if (Process.GetCurrentProcess().SessionId != 0)
                    config.AddConsole();


            });

            services.AddSignalR();
            services.AddSingleton<LogCollection>();
            services.AddSingleton<CancellationTokenSource>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<Hubs.LogHub>("/hub/log");
            });
        }
    }
}
