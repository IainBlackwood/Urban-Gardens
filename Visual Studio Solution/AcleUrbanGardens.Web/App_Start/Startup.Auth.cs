using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using AcleUrbanGardens.Web.Models;
using Microsoft.Owin.Security.Twitter;

namespace AcleUrbanGardens.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            app.UseMicrosoftAccountAuthentication(
                clientId: System.Configuration.ConfigurationManager.AppSettings["MicrosoftOAuthClientID"],
                clientSecret: System.Configuration.ConfigurationManager.AppSettings["MicrosoftOAuthClientSecret"]);

            //TODO: Fix issues with Microsoft Authentication, commented out as not working currently: Investigate
            //app.UseMicrosoftAccountAuthentication(new Microsoft.Owin.Security.MicrosoftAccount.MicrosoftAccountAuthenticationOptions
            //{
            //    ClientId = System.Configuration.ConfigurationManager.AppSettings["MicrosoftOAuthClientID"],
            //    ClientSecret = System.Configuration.ConfigurationManager.AppSettings["MicrosoftOAuthClientSecret"],
            //    CallbackPath = new PathString("/signin-microsoft"),
            //    AuthenticationType = "Microsoft",
            //    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive,
            //    Caption = "This is the caption"
            //});

            // TODO: Twitter and Facebook not currently returning a email address (Was working: why is this happening???)
            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = System.Configuration.ConfigurationManager.AppSettings["TwitterOAuthConsumerKey"],
                ConsumerSecret = System.Configuration.ConfigurationManager.AppSettings["TwitterOAuthConsumerSecret"],
                BackchannelCertificateValidator = null
            });

            app.UseFacebookAuthentication(
               appId: System.Configuration.ConfigurationManager.AppSettings["FacebookOAuthAppID"],
               appSecret: System.Configuration.ConfigurationManager.AppSettings["FacebookOAuthClientSecret"]);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = System.Configuration.ConfigurationManager.AppSettings["GoogleOAuthClientID"],
                ClientSecret = System.Configuration.ConfigurationManager.AppSettings["GoogleOAuthClientSecret"]
            });
        }
    }
}