using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using DrawIt3.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
[assembly: OwinStartupAttribute(typeof(DrawIt3.Startup))]
namespace DrawIt3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
