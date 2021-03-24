using FN.Testing.WebApi.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using FN.Testing.Entities;
using Microsoft.Extensions.Logging;
using FN.Testing.DataLayer.DataContext;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Server.IIS;

namespace FN.Testing.WebApi
{
    public class Startup
    {
        readonly IWebHostEnvironment webHostEnvironment;
        public IConfigurationRoot Configuration { get; }
        public Startup(IWebHostEnvironment env)
        {
            webHostEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddOptions();
            services.Configure<CustomConfig>(Configuration.GetSection("CustomConfig"));
            if (Configuration["AppConfig:UseInMemoryDatabase"] == "true")
                services.AddDbContext<ConnectionDataContext>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));
            else 
                services.AddDbContext<ConnectionDataContext>(opt => 
                    opt.UseSqlServer(Configuration.GetConnectionString("StuffDbConnectionString")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        );
            });
            services.AddControllers()
                //.AddNewtonsoftJson(opt =>
                //{
                //    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //})
                ;
            services.AddMvc().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
            services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
            services.AddAuthentication(IISServerDefaults.AuthenticationScheme);
            services
                .AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "FN.Testing.WebApi", Version = "v1" });
                    })
                .AddApplicationServices()
                .AddBusinessServices()
                .AddDataLayer()
                ;
        }
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            //Log.Logger = new LoggerConfiguration()
                    //.WriteTo.RollingFile(pathFormat: "logs\\log-{Date}.log")
                    //.CreateLogger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiFN v1"));
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    public class ClaimsTransformer : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("now", DateTime.Now.ToString()));
            return Task.FromResult(principal);
        }
    }
}
