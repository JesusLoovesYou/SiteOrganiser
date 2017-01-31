using DependencyResolver;
using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.DAL;
using SiteOrganiser.Common.Messages;
using SiteOrganiser.Common.System;
using Ninject;
using Ninject.Parameters;
using System.Security.Principal;

namespace SiteOrganiser.Business
{
    public class UserBusiness : IUserBusiness
    {
        private IUserDAL UserDAL { get; set; }
        public UserBusiness(object database)
        {
            IKernel kernel = NinjectDependencyResolver.CurrentKernel;
            UserDAL = kernel.Get<IUserDAL>(new IParameter[] { new ConstructorArgument("database", database) });
        }

        public ILoginMessages LoginUser(string userName, string password)
        {
            ICredential result = UserDAL.GetUserCredentials(userName);
            if (result.Success)
            {
                IIdentity identity = new CustomIdentity(true, userName);
                ICustomPrincipal principal = new CustomPrincipal(identity, result.UserId, result.UserName, result.UserRoles);
                var message = new LoginMessages { LoginResult = LoginAttempts.Success, Message = string.Empty, Principal = principal };
                return message;
            }
            else
            {
                return new LoginMessages { LoginResult = LoginAttempts.UnknownUser, Message = "User could not be found." };
            }
        }
    }
}
