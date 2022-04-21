using EFCUTY_HFT_2021221.Data;
using EFCUTY_HFT_2021221.Logic;
using EFCUTY_HFT_2021221.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EFCUTY_HFT_2021221.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<ICitizenLogic, CitizenLogic>();
            services.AddTransient<ISettlementLogic, SettlementLogic>();
            services.AddTransient<ICountryLogic, CountryLogic>();

            services.AddTransient<ICitizenRepository, CitizenRepository>();
            services.AddTransient<ISettlementRepository, SettlementRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();

            services.AddTransient<WorldDbContext, WorldDbContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EFCUTY_HFT_2021221.Endpoint", Version = "v1" });
            });
            services.AddSignalR();
            //services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFCUTY_HFT_2021221.Endpoint v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
