using API.Data;
using API.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration) // pull this info from appsettings.dev / appsettings.json
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) // aka dependency injection container
        { // makes these services available to be used in the app

            services.AddControllers();
            services.AddSwaggerGen(c => // creates documentation for whatever api's there are
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });

            // starts the dbcontext session
            //                    storecontext: name of the context from migrations/storecontext.cs
            services.AddDbContext<StoreContext>(opt => 
            {
                // this is where the options are passed in
                // comes from app configuration / appsettings.development.json
                opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // this is for middleware
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage(); // creates exceptions for errors
                app.UseSwagger(); // middleware for swagger
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1")); // generates swagger page
            }

            // app.UseHttpsRedirection();

            app.UseRouting(); //routing
            app.UseCors(opt => 
            {
                opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
            });
            app.UseAuthorization(); // authorization

            app.UseEndpoints(endpoints => // maps endpoints from controllers
            {
                endpoints.MapControllers();
            });
        }
    }
}
