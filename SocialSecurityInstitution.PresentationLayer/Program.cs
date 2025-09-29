using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.Hubs;
using SocialSecurityInstitution.BusinessLogicLayer.MappingLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.SqlDependencyServices;
using SocialSecurityInstitution.BusinessLogicLayer.ValidationServices;
using SocialSecurityInstitution.BusinessObjectLayer;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDatabase;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using SocialSecurityInstitution.PresentationLayer.Middleware;
using SocialSecurityInstitution.PresentationLayer.Services.AbstractPresentationServices;
using SocialSecurityInstitution.PresentationLayer.Services.ConcretePresentationServices;
using System;
using System.Text;

namespace SocialSecurityInstitution.PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true);
                });
            });

            // Configuration setup
            var configuration = builder.Configuration;

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme; // API için cookie tabanlý kimlik doðrulama
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("SignalRJwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/dashboardHub"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/Login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
            });

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddControllersWithViews()
                .AddNToastNotifyToastr(new ToastrOptions()
                {
                    PositionClass = ToastPositions.TopRight,
                    TimeOut = 5000,
                    ProgressBar = true
                })
                .AddRazorRuntimeCompilation();

            builder.Services.AddHttpContextAccessor();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

            // Add all Scoped dataAccess services
            builder.Services.AddScoped<IAtanmaNedenleriDal, AtanmaNedenleriDal>();
            builder.Services.AddScoped<IBankoIslemleriDal, BankoIslemleriDal>();
            builder.Services.AddScoped<IBankolarKullaniciDal, BankolarKullaniciDal>();
            builder.Services.AddScoped<IBankolarDal, BankolarDal>();
            builder.Services.AddScoped<IDepartmanlarDal, DepartmanlarDal>();
            builder.Services.AddScoped<IHizmetBinalariDal, HizmetBinalariDal>();
            builder.Services.AddScoped<IIlcelerDal, IlcelerDal>();
            builder.Services.AddScoped<IIllerDal, IllerDal>();
            builder.Services.AddScoped<IKanallarDal, KanallarDal>();
            builder.Services.AddScoped<IKanallarAltDal, KanallarAltDal>();
            builder.Services.AddScoped<IKanalAltIslemleriDal, KanalAltIslemleriDal>();
            builder.Services.AddScoped<IKanalIslemleriDal, KanalIslemleriDal>();
            builder.Services.AddScoped<IKanalPersonelleriDal, KanalPersonelleriDal>();
            builder.Services.AddScoped<ILoginLogoutLogDal, LoginLogoutLogDal>();
            builder.Services.AddScoped<IPdksCihazlarDal, PdksCihazlarDal>();
            builder.Services.AddScoped<IPersonellerDal, PersonellerDal>();
            builder.Services.AddScoped<IPersonelCocuklariDal, PersonelCocuklariDal>();
            builder.Services.AddScoped<IPersonelYetkileriDal, PersonelYetkileriDal>();
            builder.Services.AddScoped<ISendikalarDal, SendikalarDal>();
            builder.Services.AddScoped<IServislerDal, ServislerDal>();
            builder.Services.AddScoped<IUnvanlarDal, UnvanlarDal>();
            builder.Services.AddScoped<IYetkilerDal, YetkilerDal>();
            builder.Services.AddScoped<IKioskGruplariDal, KioskGruplariDal>();
            builder.Services.AddScoped<IKioskIslemGruplariDal, KioskIslemGruplariDal>();
            builder.Services.AddScoped<ISiralarDal, SiralarDal>();
            builder.Services.AddScoped<IDatabaseLogDal, DatabaseLogDal>();
            builder.Services.AddScoped<IHubConnectionDal, HubConnectionDal>();
            builder.Services.AddScoped<ITvlerDal, TvlerDal>();
            builder.Services.AddScoped<IHubTvConnectionDal, HubTvConnectionDal>();
            builder.Services.AddScoped<ITvBankolariDal, TvBankolariDal>();
            
            // Custom Services
            builder.Services.AddScoped<IUserInfoService, UserInfoService>();
            builder.Services.AddScoped<ILoginControlService, LoginControlService>();
            builder.Services.AddScoped<IPersonelCustomService, PersonelCustomService>();
            builder.Services.AddScoped<IPersonelFacadeService, PersonelFacadeService>();
            builder.Services.AddScoped<IBankolarCustomService, BankolarCustomService>();
            builder.Services.AddScoped<IBankolarKullaniciCustomService, BankolarKullaniciCustomService>();
            builder.Services.AddScoped<IHizmetBinalariCustomService, HizmetBinalariCustomService>();
            builder.Services.AddScoped<IKanallarCustomService, KanallarCustomService>();
            builder.Services.AddScoped<IKanalFacadeService, KanalFacadeService>();
            builder.Services.AddScoped<IKanalPersonelleriCustomService, KanalPersonelleriCustomService>();
            builder.Services.AddScoped<IKioskIslemGruplariCustomService, KioskIslemGruplariCustomService>();
            builder.Services.AddScoped<ISiralarCustomService, SiralarCustomService>();
            builder.Services.AddScoped<IHubConnectionCustomService, HubConnectionCustomService>();
            builder.Services.AddScoped<IHubTvConnectionCustomService, HubTvConnectionCustomService>();
            builder.Services.AddScoped<ITvlerCustomService, TvlerCustomService>();
            builder.Services.AddScoped<IYetkilerCustomService, YetkilerCustomService>();
            builder.Services.AddScoped<IPersonelYetkileriCustomService, PersonelYetkileriCustomService>();
            builder.Services.AddScoped<IIlcelerCustomService, IlcelerCustomService>();
            builder.Services.AddScoped<IServislerCustomService, ServislerCustomService>();

            builder.Services.AddScoped<ICacheService, CacheService>();

            builder.Services.AddScoped<PrintService>();

            // Concrete Services
            builder.Services.AddScoped<IAtanmaNedenleriService, AtanmaNedenleriService>();
            builder.Services.AddScoped<IBankoIslemleriService, BankoIslemleriService>();
            builder.Services.AddScoped<IBankolarKullaniciService, BankolarKullaniciService>();
            builder.Services.AddScoped<IBankolarService, BankolarService>();
            builder.Services.AddScoped<IDepartmanlarService, DepartmanlarService>();
            builder.Services.AddScoped<IHizmetBinalariService, HizmetBinalariService>();
            builder.Services.AddScoped<IIlcelerService, IlcelerService>();
            builder.Services.AddScoped<IIllerService, IllerService>();
            builder.Services.AddScoped<IKanallarService, KanallarService>();
            builder.Services.AddScoped<IKanallarAltService, KanallarAltService>();
            builder.Services.AddScoped<IKanalAltIslemleriService, KanalAltIslemleriService>();
            builder.Services.AddScoped<IKanalIslemleriService, KanalIslemleriService>();
            builder.Services.AddScoped<IKanalPersonelleriService, KanalPersonelleriService>();
            builder.Services.AddScoped<ILoginLogoutLogService, LoginLogoutLogService>();
            builder.Services.AddScoped<IPdksCihazlarService, PdksCihazlarService>();
            builder.Services.AddScoped<IPersonellerService, PersonellerService>();
            builder.Services.AddScoped<IPersonelCocuklariService, PersonelCocuklariService>();
            builder.Services.AddScoped<IPersonelYetkileriService, PersonelYetkileriService>();
            builder.Services.AddScoped<ISendikalarService, SendikalarService>();
            builder.Services.AddScoped<IServislerService, ServislerService>();
            builder.Services.AddScoped<IUnvanlarService, UnvanlarService>();
            builder.Services.AddScoped<IYetkilerService, YetkilerService>();
            builder.Services.AddScoped<IKioskGruplariService, KioskGruplariService>();
            builder.Services.AddScoped<IKioskIslemGruplariService, KioskIslemGruplariService>();
            builder.Services.AddScoped<ISiralarService, SiralarService>();
            builder.Services.AddScoped<IDatabaseLogService, DatabaseLogService>();
            builder.Services.AddScoped<ILogService, LogService>();
            builder.Services.AddScoped<IHubConnectionService, HubConnectionService>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddScoped<IHubTvConnectionService, HubTvConnectionService>();
            builder.Services.AddScoped<ITvlerService, TvlerService>();
            builder.Services.AddScoped<ITvBankolariService, TvBankolariService>();

            //Singleton services
            builder.Services.AddSingleton<IUserContextService, UserContextService>();


            // Validation Services
            builder.Services.AddScoped<IPersonelValidationService, PersonelValidationService>();


            // Add AutoMapper service
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // SignalR
            builder.Services.AddSignalR(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromMinutes(1); // Keep-alive aralýðý
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(2); // Ýstemci zaman aþýmý süresi
            });

            builder.Services.AddTransient<DashboardHub>();
            builder.Services.AddTransient<TvHub>();
            //builder.Services.AddSingleton<ISubscribeTableDependency>(provider =>
            //{
            //    return new SubscribeTableDependency<Departmanlar>(
            //        provider.GetRequiredService<IServiceProvider>(),
            //        provider.GetRequiredService<IHubContext<DashboardHub>>(),
            //        provider.GetRequiredService<IHubContext<TvHub>>(),
            //        provider.GetRequiredService<ILogger<SubscribeTableDependency<Departmanlar>>>(),
            //        provider.GetRequiredService<IMapper>()
            //    );
            //});

            builder.Services.AddSingleton<ISubscribeTableDependency>(provider =>
            {
                return new SubscribeTableDependency<Siralar>(
                    provider.GetRequiredService<IServiceProvider>(),
                    provider.GetRequiredService<IHubContext<DashboardHub>>(),
                    provider.GetRequiredService<IHubContext<TvHub>>(),
                    provider.GetRequiredService<ILogger<SubscribeTableDependency<Siralar>>>(),
                    provider.GetRequiredService<IMapper>()
                );
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialSecurityInstitution API v1");
                });
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseNToastNotify();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();

            app.UseCors("CorsPolicy");

            /* MiddleWare */
            app.UseMiddleware<SessionValidationMiddleware>();//Login Control

            // For scoped services in middleware
            app.UseMiddleware<UserContextMiddleware>();
            /* MiddleWare */

            app.UseAuthorization();

            var serviceProvider = app.Services;
            var tableDependencies = serviceProvider.GetServices<ISubscribeTableDependency>();

            foreach (var tableDependency in tableDependencies)
            {
                tableDependency.SubscribeTablesDependency(connectionString);
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<TvHub>("/TvHub");
                endpoints.MapHub<DashboardHub>("/DashboardHub");
            });


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
