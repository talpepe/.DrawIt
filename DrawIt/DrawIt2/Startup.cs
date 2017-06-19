using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using DrawIt.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;



[assembly: OwinStartup(typeof(DrawIt.Startup))]

namespace DrawIt
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888



            app.CreatePerOwinContext<ApplicationDbContext>(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            app.MapSignalR();


        }


    }
}
