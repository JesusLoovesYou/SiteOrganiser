using DependencyResolver;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.DAL;
using SiteOrganiser.Common.Management;
using SiteOrganiser.Common.Messages;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Web.Security;

namespace SiteOrganiser.Business
{
    public class AdminBusiness : IAdminBusiness
    {
        private IUserManagementDAL UserManagementDAL { get; set; }

        public AdminBusiness(object database)
        {
            IKernel kernel = NinjectDependencyResolver.CurrentKernel;
            UserManagementDAL = kernel.Get<IUserManagementDAL>(new IParameter[] { new ConstructorArgument("database", database) });
        }
        public IBLMessage ChangeUserLock(string userEmail)
        {
            try
            {
                MembershipUser user = Membership.GetUser(userEmail);
                if (user != null)
                {
                    if (user.UnlockUser())
                        return new BLMessage { Message = "User activated.", Result = SaveAttempts.Success };
                    return new BLMessage { Message = "User could not be activated.", Result = SaveAttempts.Failure };
                }
                return new BLMessage { Message = "There is no user with this email.", Result = SaveAttempts.Failure };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "AdminBusiness");
            }
            return new BLMessage { Message = "An error occured during changing user lock. Please try again later.", Result = SaveAttempts.Failure };
        }

        public IBLMessage CreateUser(string userEmail, string newPassword)
        {
            try
            {
                MembershipUser newUser = Membership.CreateUser(userEmail, newPassword, userEmail);
                //var roles = Roles.GetAllRoles();
                //Roles.AddUserToRole(userEmail, roles[1]);

                return new BLMessage { Message = "User created.", Result = SaveAttempts.Success };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "AdminBusiness");
                return new BLMessage { Message = "An error occured during user create. Please try again later.", Result = SaveAttempts.Failure };
            }
        }

        public IBLMessage UpdateAccountInfo(string userEmail, string oldPassword, string newPassword, string userKey)
        {
            try
            {
                MembershipUser user = Membership.GetUser(userKey);
                if (user != null)
                {
                    var changeResult = user.ChangePassword(oldPassword, newPassword);
                    if (changeResult)
                    {
                        return new BLMessage { Message = "User info updated.", Result = SaveAttempts.Success };
                    }
                    return new BLMessage { Message = "Wrong password.", Result = SaveAttempts.Failure };
                }
                return new BLMessage { Message = "There is no user with this email.", Result = SaveAttempts.Failure };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "AdminBusiness");
            }
            return new BLMessage { Message = "An error occured during updating user info. Please try again later.", Result = SaveAttempts.Failure };
        }

        public IRegisteredUser GetUser(string userEmail)
        {
            try
            {
                MembershipUser newUser = Membership.GetUser(userEmail);

                if (newUser != null)
                    return new RegisteredUser { Email = newUser.Email, Online = newUser.IsOnline, RegistrationDate = newUser.CreationDate, UserId = newUser.ProviderUserKey.ToString() };
                return null;
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "AdminBusiness");
                throw;
            }
        }

        public IBLMessage EditUser(string userEmail, string newPassword, string userKey)
        {
            try
            {
                MembershipUser user = Membership.GetUser(userKey);
                if (user != null)
                {
                    user.UnlockUser();
                    var changeResult = user.ChangePassword(user.ResetPassword(), newPassword);
                    if (changeResult)
                    {
                        return new BLMessage { Message = "User updated.", Result = SaveAttempts.Success };
                    }
                    return new BLMessage { Message = "Wrong password.", Result = SaveAttempts.Failure };
                }
                return new BLMessage { Message = "There is no user with this email.", Result = SaveAttempts.Failure };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "AdminBusiness");
            }
            return new BLMessage { Message = "An error occured during updating user. Please try again later.", Result = SaveAttempts.Failure };
        }

        public IBLMessage DeleteUser(string userEmail)
        {
            try
            {
                MembershipUser user = Membership.GetUser(userEmail);
                if (user != null)
                {
                    if (Membership.DeleteUser(userEmail))
                    {
                        UserManagementDAL.DeleteApplication(userEmail);
                        UserManagementDAL.DeleteCustomer(userEmail);
                        return new BLMessage { Message = "User deleted.", Result = SaveAttempts.Success };
                    }
                    else
                        return new BLMessage { Message = "User could not be deleted.", Result = SaveAttempts.Failure };
                }
                return new BLMessage { Message = "There is no user with this email.", Result = SaveAttempts.Failure };
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "AdminBusiness");
            }
            return new BLMessage { Message = "An error occured during deleting user. Please try again later.", Result = SaveAttempts.Failure };
        }

        public System.Collections.Generic.List<IRegisteredUser> GetMembers()
        {
            List<IRegisteredUser> registeredUsers = new List<IRegisteredUser>(new List<RegisteredUser>());
            var users = Membership.GetAllUsers();
            foreach (MembershipUser user in users)
            {
                Guid g = new Guid(user.ProviderUserKey.ToString());
                byte[] b = g.ToByteArray();
                string UserID = BitConverter.ToString(b, 0).Replace("-", string.Empty);
                registeredUsers.Add(new RegisteredUser
                {
                    Email = user.Email,
                    Online = user.IsOnline,
                    RegistrationDate = user.CreationDate,
                    UserId = UserID,
                    IsLocked = user.IsLockedOut
                });
            }
            return registeredUsers;
        }
    }
}
