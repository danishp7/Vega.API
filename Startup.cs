using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Vega.API.Data;
using Vega.API.Helpers;

namespace Vega.API
{
    public class Startup
    {
        public IConfiguration _config { get; }

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddCors();
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(_config.GetConnectionString("VegaConnectionString")));
            
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IVegaSharedRepository, VegaSharedRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey
                        (System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // to add global exception handler so that in production mode we are not sending developer exception
                // page back to user
                app.UseExceptionHandler(builder => {
                    builder.Run(async ctx => {
                        ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = ctx.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            // here we also need to add info in header so we use helper in which we
                            // set the error message in the appropriate header like
                            // 1: Application-Header
                            // 2: Access-Control-Expose-Headers
                            // 3: Access-Control-Allow-Origin
                            ctx.Response.AddApplicationError(error.Error.Message);
                            await ctx.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                // app.UseHsts();
            }
            app.UseCors(c => c.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default", // this is routing name
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
