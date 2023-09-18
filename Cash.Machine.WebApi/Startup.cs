using Autofac;
using AutoMapper;
using Cash.Machine.Cross.Cutting;
using Cash.Machine.Data.Context;
using Cash.Machine.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cash.Machine.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string connectionString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("Warren_DbConnection");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseMySql(connectionString));
            services.AddAutoMapper(typeof(AutoMapperSetup));
            services.AddControllersWithViews();
        }

        public void ConfigureContainer(ContainerBuilder Builder)
        {
            Builder.RegisterModule(new ModuleIOC());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=CashMachine}/{action=Index}/{id?}");
            });
        }
    }
}