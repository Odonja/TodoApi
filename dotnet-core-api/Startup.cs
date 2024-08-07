﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoApi.Models;
using Azure.Messaging.ServiceBus;
using TodoApi.Services.Interfaces;
using TodoApi.Services.Implementations;

namespace TodoApi
{
    public class Startup
    {
        // set the environment variable with: setx AZURE_SERVICE_BUS_CONNECTION_STRING "<the connection string>"
        private static readonly string? connectionString = Environment.GetEnvironmentVariable("AZURE_SERVICE_BUS_CONNECTION_STRING");

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            try
            {
                services.AddSingleton<ServiceBusClient>(new ServiceBusClient(connectionString));
                services.AddScoped<IConfigService, ConfigService>();
                services.AddScoped<IQueueEmptyerService, QueueEmptyerService>();

            }
            catch (Exception e)
            {
                // to catch any exceptions on the connection string
                // so atleast the position and todo controller work
            }



            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddDbContext<TodoContext>(options => options.UseInMemoryDatabase("TodoList"));

            services.AddRazorPages();
            services.Configure<PositionOptions>(Configuration.GetSection(PositionOptions.Position));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
