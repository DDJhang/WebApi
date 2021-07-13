using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWebApi.Context;
using MyWebApi.Repository;
using MyWebApi.Repository.Interface;
using MyWebApi.Service;
using MyWebApi.Service.Interface;
using System;

namespace MyWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IPunchClockRepository, PunchClockRepository>();

            services.AddScoped<IPunchClockService, PunchClockService>();

            services.AddControllers();

            services.AddDbContextPool<AccountContext>(opt =>
                                               opt.UseMySQL(Configuration.GetConnectionString("AccountContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
