using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using TTNH_UDAShop.Data;
using TTNH_UDAShop.Model.Models;

[assembly: OwinStartup(typeof(TTNH_UDAShop.Web.App_Start.Startup))]

namespace TTNH_UDAShop.Web.App_Start
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(TTNH_UDAShopDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // tạo ra 1 PerOwinContext để quản lý User Manager, Owin là viết tắt của Open web interface for .NET - bản chất
            // là 1 midle ware, giảm sự phụ thuộc giữa servar và application (không phụ thuộc vào đăng nhập window hay gì cả mà nó 
            // là một cơ chế riêng
            // cho phép các cơ chế đăng nhập và mã hóa, cho phép đăng nhập từ facebook hay g+...
            app.CreatePerOwinContext<UserManager<ApplicationUser>>(CreateManager);
            // cách cấu hình: sử dụng auto rization server trong phương thức của app
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                // api đăn gnhaapj và validate phải thông qua đường dẫn này
                TokenEndpointPath = new PathString("/oauth/token"),
                // đưa ra 1 authorriserver mà đã định nghĩa ở phần dưới
                Provider = new AuthorizationServerProvider(),
                // 1 token tồn tại 30p
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                // có thể validate qua 1 ứng dụng
                AllowInsecureHttp = true,

            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
        // authorriserver kế thừa từ lớp có sẵn, ghi đè được 2 phương thức
        public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            // phương thức validate token
            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();
            }

            // phương thức gán token
            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

                if (allowedOrigin == null) allowedOrigin = "*";

                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                // tương tác với phần đăng  qua user manager 
                UserManager<ApplicationUser> userManager = context.OwinContext.GetUserManager<UserManager<ApplicationUser>>();
                ApplicationUser user;
                try
                {
                    user = await userManager.FindAsync(context.UserName, context.Password);
                }
                catch
                {
                    // Could not retrieve the user due to error.
                    context.SetError("server_error");
                    context.Rejected();
                    return;
                }
                if (user != null)
                {
                    ClaimsIdentity identity = await userManager.CreateIdentityAsync(
                                                           user,
                                                           DefaultAuthenticationTypes.ExternalBearer);
                    context.Validated(identity);
                }
                else
                {
                    context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không đúng.'");
                    context.Rejected();
                }
            }
        }


        // tạo ra 1 user manager để tương tác với phần đăng nhập
        private static UserManager<ApplicationUser> CreateManager(IdentityFactoryOptions<UserManager<ApplicationUser>> options, IOwinContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context.Get<TTNH_UDAShopDbContext>());
            var owinManager = new UserManager<ApplicationUser>(userStore);
            return owinManager;
        }
    }


}