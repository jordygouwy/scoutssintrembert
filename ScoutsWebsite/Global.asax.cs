using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using ScoutsWebsite.Repositories;
using ScoutsWebsite.IRepositories;
using ScoutsWebsite.Models;

namespace ScoutsWebsite
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("index.aspx", "index.aspx", new { controller = "Home", action = "Index" });
            routes.MapRoute("calendar.aspx", "calendar.aspx", new { controller = "Home", action = "Calendar" });
            routes.MapRoute("getevent.aspx", "getevent.aspx", new { controller = "Home", action = "Event" });
            routes.MapRoute("album.aspx", "album.aspx", new { controller = "Home", action = "Album" });
            routes.MapRoute("leaders.aspx", "leaders.aspx", new { controller = "Home", action = "Leaders" });

            //admin pages
            routes.MapRoute("logon.aspx", "logon.aspx", new { controller = "Account", action = "Logon" });
            routes.MapRoute("aleaders.aspx", "aleaders.aspx", new { controller = "Account", action = "AdminLeaders" });
            routes.MapRoute("aleaderedit.aspx", "aleaderedit.aspx", new { controller = "Account", action = "AdminLeaderEdit" });
            routes.MapRoute("aleaderdelete.aspx", "aleaderdelete.aspx", new { controller = "Account", action = "AdminLeaderDelete" });

            routes.MapRoute("aposts.aspx", "aposts.aspx", new { controller = "Account", action = "AdminPosts" });
            routes.MapRoute("apostedit.aspx", "apostedit.aspx", new { controller = "Account", action = "AdminPostEdit" });
            routes.MapRoute("apostdelete.aspx", "apostdelete.aspx", new { controller = "Account", action = "AdminPostDelete" });

            routes.MapRoute("acalendar.aspx", "acalendar.aspx", new { controller = "Account", action = "AdminCalendar" });
            routes.MapRoute("acalendaredit.aspx", "acalendaredit.aspx", new { controller = "Account", action = "AdminCalendarEdit" });
            routes.MapRoute("acalendardelete.aspx", "acalendardelete.aspx", new { controller = "Account", action = "AdminCalendarDelete" });

            routes.MapRoute("showimage.aspx", "showimage.aspx", new { controller = "Account", action = "ShowImage" });


            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var builder = new ContainerBuilder();           
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<CalendarRepository>().As<ICalendarRepository>();
            builder.RegisterType<PostRepository>().As<IPostRepository>();
            builder.RegisterType<LeaderRepository>().As<ILeaderRepository>();

            builder.RegisterType<ScoutsDataDataContext>();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}