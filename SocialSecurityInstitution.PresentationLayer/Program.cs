using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialSecurityInstitution.BusinessLogicLayer.AbstractLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.ConcreteLogicServices;
using SocialSecurityInstitution.BusinessLogicLayer.CustomAbstractLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.CustomConcreteLogicService;
using SocialSecurityInstitution.BusinessLogicLayer.MappingLogicServices;
using SocialSecurityInstitution.DataAccessLayer.AbstractDataServices;
using SocialSecurityInstitution.DataAccessLayer.ConcreteDataServices;

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
            ).AddCookie(
                x =>
                {
                    x.LoginPath = "/Login";
                    x.LogoutPath = "/Logout";
                    x.AccessDeniedPath = "/Login";
                }    
            );

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<IAtanmaNedenleriService, AtanmaNedenleriService>();
            builder.Services.AddSingleton<IAtanmaNedenleriDal, AtanmaNedenleriDal>();
            builder.Services.AddSingleton<IPersonelRequestService, PersonelRequestService>();
            builder.Services.AddSingleton<ILoginControlService, LoginControlService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

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
