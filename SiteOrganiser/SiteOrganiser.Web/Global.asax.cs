using DependencyResolver;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using SiteOrganiser.Common.System;
using SiteOrganiser.Web.Admin.Membership;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace SiteOrganiser.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Creating Log object.
            try
            {
                LogWriterFactory logwriterFactory = new LogWriterFactory();
                LogWriter logWriter = logwriterFactory.Create();
                Logger.SetLogWriter(logWriter);
            }
            catch (Exception exc)
            {
                throw exc.InnerException ?? exc;
            }

            // Create IoC
            IKernel kernel = new StandardKernel();
            kernel.Load(new SiteOrganiser.Business.BusinessBootstrapper().GetModules());
            kernel.Load(new SiteOrganiser.DataAccess.DataAccessBootstrapper().GetModules());
            NinjectDependencyResolver.SetKernel(kernel);

            // Checking database connection.
            //var serverMessage = SitePresenter.TestServerConnection();
            //if (serverMessage.Result != SaveAttempts.Success)
            //{
            //    throw new Exception("Site installation error. Contact to administrator.", new Exception(serverMessage.Message));
            //}
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                if (!String.IsNullOrEmpty(authTicket.UserData))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);
                    List<IRole> roles = new List<IRole>();
                    serializeModel.UserRoles.ForEach(r => roles.Add(new Role { RoleId = r.RoleId, RoleName = r.RoleName }));
                    Admin.Membership.CustomPrincipal newUser = new Admin.Membership.CustomPrincipal(serializeModel.UserId, authTicket.Name, roles);
                    HttpContext.Current.User = newUser;
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}