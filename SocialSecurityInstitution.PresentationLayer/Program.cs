using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.MappingLogicService;
using SocialSecurityInstitution.BusinessObjectLayer.CommonDtoEntities;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;
using SocialSecurityInstitution.PresentationLayer.Services.AbstractPresentationServices;
using SocialSecurityInstitution.PresentationLayer.Services.ConcretePresentationServices;

namespace SocialSecurityInstitution.PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddAuthentication(
                Microsoft.AspNetCore.Authentication.Cookies.
                CookieAuthenticationDefaults.AuthenticationScheme    
            ).AddCookie(option => {
                option.LoginPath = "/Login";
                option.LogoutPath = "/Logout";
                option.AccessDeniedPath = "/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
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



            // Add services to the container.
            builder.Services.AddControllersWithViews()
            .AddNToastNotifyToastr(new ToastrOptions()
            {
                PositionClass = ToastPositions.TopRight,
                TimeOut = 5000,
                ProgressBar = true
            })
            .AddRazorRuntimeCompilation();

            builder.Services.AddHttpContextAccessor();
            
            builder.Services.AddScoped<IAtanmaNedenleriDal, AtanmaNedenleriDal>();
            builder.Services.AddScoped<IBankoIslemleriDal, BankoIslemleriDal>();
            builder.Services.AddScoped<IBankolarKullaniciDal,  BankolarKullaniciDal>();
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

            
            /* Custom Servisler */
            builder.Services.AddScoped<IUserInfoService, UserInfoService>();
            builder.Services.AddScoped<ILoginControlService, LoginControlService>();
            builder.Services.AddScoped<IPersonelCustomService, PersonelCustomService>();
            builder.Services.AddScoped<IBankolarCustomService, BankolarCustomService>();
            builder.Services.AddScoped<IBankolarKullaniciCustomService, BankolarKullaniciCustomService>();
            builder.Services.AddScoped<IHizmetBinalariCustomService, HizmetBinalariCustomService>();
            builder.Services.AddScoped<IKanallarCustomService, KanallarCustomService>();
            builder.Services.AddScoped<IKanalPersonelleriCustomService, KanalPersonelleriCustomService>();
            builder.Services.AddScoped<IKioskIslemGruplariCustomService, KioskIslemGruplariCustomService>();

            builder.Services.AddScoped<PrintService>();
            /* Custom Servisler */

            /* Concrete Servisler */
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
            /* Concrete Servisler */

            builder.Services.AddSingleton<IMapper>(AutoMapperConfiguration.Configure());

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
            }

            app.UseNToastNotify();

            app.UseStaticFiles();

            app.UseSession();

            app.UseSessionTimeoutMiddleware();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
