using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Agitur.Utilities;
using Agitur.EFDataAccess;
using Agitur.DataAccess.Abstractions;
using Agitur.ApplicationLogic;
using Microsoft.AspNetCore.Http.Features;
using Agitur.SignalR;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace Agitur
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

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            string allowedChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890 ";
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddControllers();
            services.AddDbContext<AuthenticationDbContext>(options =>
                 options.UseSqlServer(
                     Configuration.GetConnectionString("Identity")));
            services.AddDefaultIdentity<AgiturUser>(options => {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters=allowedChars; }).
                AddEntityFrameworkStores<AuthenticationDbContext>();
            services.AddDbContext<AgiturDbContext>(options => options. UseSqlServer(Configuration.GetConnectionString("Agitur")));

            services.AddSignalR().AddHubOptions<ChatHub>(options => options.EnableDetailedErrors = true)
                 .AddJsonProtocol(options =>
                 {
                     options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                 });

            //JWT Authentication
            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<IUserContactsRepository, EFUserContactsRepository>();
            services.AddScoped<IUserMessageRepository, EFUserMessageRepository>();
            services.AddScoped<IGroupRepository, EFGroupRepository>();
            services.AddScoped<IUserGroupRepository, EFUserGroupRepository>();
            services.AddScoped<IGroupMessageRepository, EFGroupMessageRepository>();
            services.AddScoped<IVocalMessageRepository, EFVocalMessageRepository>();
            services.AddScoped<GroupMessageServices>();
            services.AddScoped<UserGroupServices>();
            services.AddScoped<GroupServices>();
            services.AddScoped<UserServices>();
            services.AddScoped<UserContactsServices>();
            services.AddScoped<UserMessageServices>();
            services.AddScoped<VocalMessageServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //FIX CORS BEFORE PUBLISHING
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");

            });
        }
    }
}
