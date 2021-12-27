using Imagination.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Imagination
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOpenTelemetryTracing(builder => builder
                .SetResourceBuilder(ResourceBuilder
                    .CreateDefault()
                    .AddEnvironmentVariableDetector()
                    .AddTelemetrySdk()
                    .AddService("Imagination"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddJaegerExporter()
                .AddSource(Program.Telemetry.Name));

            services.AddControllers();

            services.Configure<KestrelServerOptions>(options =>
            {
                // Required by SkiaSharp library as it's reading the stream synchronously
                options.AllowSynchronousIO = true;
            });

            services.AddTransient<IImageConversionService, ImageConversionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
