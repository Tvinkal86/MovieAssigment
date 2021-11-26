using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using demo.Helpers;
using demo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;

namespace demo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddAuthentication();


            services.AddScoped<IUserService, UserService>();
        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();

            //Add JWToken to all incoming HTTP Request Header
            //app.Use(async (context, next) =>
            //{
            //    var JWToken = context.Session.GetString("JWToken");
            //    if (!string.IsNullOrEmpty(JWToken))
            //    {
            //        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //    }
            //    await next();
            //});
            //Add JWToken Authentication service
            app.UseAuthentication();

            //app.UseMvc();
        

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
