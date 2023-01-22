using Autofac;
using Microsoft.OpenApi.Models;
using OpenXrm.Core.Database;
using OpenXrm.Core.Database.Infrastructure;
using Newtonsoft;

namespace OpenXrm.Core.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OpenXRM", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CORS",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:4200", "localhost/", "localhost:4200/").AllowAnyHeader().AllowAnyMethod();
                    });
            });
        }

        // The new method
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called after ConfigureServices so things
            // registered here OVERRIDE things registered in ConfigureServices.
            builder.RegisterModule(new DatabaseModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenXRM v1"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //app.UseStaticFiles();
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var pDbContext = serviceScope.ServiceProvider.GetRequiredService<CoreContext>();
                pDbContext.Database.EnsureCreated();
            }
            app.UseRouting();
            app.UseCors("CORS");//Name of cors policy
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute(
                //    name: "DefaultApi",
                //pattern: "api/{controller}/{id}",
                //defaults: new { id = RouteParameter.Optional });
            });
        }
    }
}