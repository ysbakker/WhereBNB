using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using WhereBNB.API.Model;
using WhereBNB.API.Repositories;

namespace WhereBNB.API
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
            services.AddMemoryCache();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(options =>
                    {
                        Configuration.Bind("AzureAdB2C", options);
                        
                        options.TokenValidationParameters.NameClaimType = "name";
                    },
                    options => { Configuration.Bind("AzureAdB2C", options); });

            services.AddDbContext<WhereBNBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("WhereBNB")));

            services.AddMiniProfiler(options => options.RouteBasePath = "/profiler").AddEntityFramework();
            
            services.AddControllers().AddNewtonsoftJson();

            services.AddScoped<IListingRepository, ListingRepository>();
            services.AddScoped<IRepository<SummaryListing>, Repository<SummaryListing>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(policy =>
                policy.WithOrigins("https://localhost:5000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );

            app.UseMiniProfiler();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}