using System;
using System.Linq;
using System.Text;
using API.Helpers;
using API.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;

using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using Service.Implementation;
using Models.Data;
using System.Collections.Generic;
using Service.Helpers;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Models.EF;
using Microsoft.AspNetCore.Antiforgery;

namespace API
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
            //services.AddAntiforgery(options =>
            //{
            //    options.HeaderName = "X-XSRF-TOKEN";
            //    options.SuppressXFrameOptionsHeader = false;
            //});
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.HttpOnly = HttpOnlyPolicy.None;
                options.Secure = CookieSecurePolicy.None;
                // you can add more options here and they will be applied to all cookies (middleware and manually created cookies)
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);


            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Use the default property (Pascal) casing
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });


            var conn = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(x => x.UseSqlServer(conn));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
                                      {
                                          options.TokenValidationParameters = new TokenValidationParameters
                                          {
                                              ValidateIssuerSigningKey = true,

                                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                                                  .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                                              ValidateIssuer = false,
                                              ValidateAudience = false
                                          };

                                      });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                     {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                    }
                });

            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:4200", "http://10.10.0.34:8070","http://10.4.0.76:1111","https://sarutobi1245.github.io", "http://10.4.4.74:99", "http://10.4.5.132:99", "http://10.10.0.34:8069", "http://10.4.0.76:99","http://10.4.5.132:90","http://10.4.0.76:8069", "http://10.4.5.174:8069", "http://10.4.4.92:4205", "http://10.4.4.224:90", "http://10.4.4.224:98", "http://10.4.4.224:91", "http://10.4.4.92:91", "http://10.4.4.92:90", "http://10.4.0.57:8072", "http://10.4.0.57:8069" , "http://10.4.4.92:8069") //register for client
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            // Code omitted for brevity
            services.AddSignalR();
            // configure DI for application services

            services.AddScoped<IMailHelper, MailHelper>();
            services.AddScoped<IMailExtension, MailExtension>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IActionPlanService, ActionPlanService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryKPILevelService, CategoryKPILevelService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IErrorMessageService, ErrorMessageService>();
            services.AddScoped<IFavouriteService, FavouriteService>();
            services.AddScoped<IKPIService, KPIService>();
            services.AddScoped<IKPILevelService, KPILevelService>();
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IOCCategoryService, OCCategoryService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IHistoryActionPlanService, HistoryActionPlanService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.Use(next => context =>
            //{, IAntiforgery antiforgery
            //    if (context.Request.Path.Value.IndexOf("/api/Home/GetNotifications", StringComparison.OrdinalIgnoreCase) != -1)
            //    {
            //        var tokens = antiforgery.GetAndStoreTokens(context);
            //        context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions() { HttpOnly = true, Path = "/" });
            //    }
            //    return next(context);
            //});
            app.UseStaticFiles();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseCors();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                CheckConsentNeeded = c => false,
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None,
                Secure = CookieSecurePolicy.None,
                MinimumSameSitePolicy = SameSiteMode.None,

            });
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KPI System");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            //context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                // app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            // global cors policy
            //app.UseCors(x => x
            //    .AllowAnyOrigin()
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<HenryHub>("/henry-hub");

            });

        }
    }
}
