using DependencyResolver;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.DAL;
using SiteOrganiser.Common.Messages;
using SiteOrganiser.Common.UserManagement;
using SiteOrganiser.DataAccess.Helpers;
using Ninject;
using Ninject.Parameters;
using System;
using System.Web.Security;

namespace SiteOrganiser.Business
{
    public class UserManagementBusiness : IUserManagementBusiness
    {
        private IUserManagementDAL UserManagementDAL { get; set; }
        public UserManagementBusiness(object database)
        {
            IKernel kernel = NinjectDependencyResolver.CurrentKernel;
            UserManagementDAL = kernel.Get<IUserManagementDAL>(new IParameter[] { new ConstructorArgument("database", database) });
        }
        public IBLMessage SendApplication(string userEmail, string userPassword, string title, string content)
        {
            var appResult = UserManagementDAL.SaveApplication(userEmail, userPassword);
            try
            {
                IUserApplication app = UserManagementDAL.GetApplicationByEmail(userEmail);
                if (app != null)
                {
                    EmailHelper.Send("email@domain.com", "p@ssw0rd", userEmail, title, String.Format(content, "website url", app.VerifyGuid));
                }
                else
                {
                    return new BLMessage { Message = "Application could not be found.", Result = SaveAttempts.Failure };
                }
            }
            catch (Exception exception)
            {
                // delete entry...
                UserManagementDAL.DeleteApplication(userEmail);
                // log exception...
                Logger.Write(exception.Message, "UserManagementBusiness");
                return new BLMessage { Message = "An error occured during sending application. Please try again later.", Result = SaveAttempts.Failure };
            }
            return appResult;
        }

        public IBLMessage VerifyApplication(string verifyGuid)
        {
            /*
             * Verification steps:
             * 1- Get application.
             * 2- Create user in membership with application credentials.
             * 3- Delete application
             */

            try
            {
                IUserApplication app = UserManagementDAL.GetApplicationByVerifyGuid(verifyGuid);
                if (app != null)
                {
                    var oldUser = Membership.GetUser(app.UserEmail);
                    if (oldUser == null)
                    {
                        MembershipUser newUser = Membership.CreateUser(app.UserEmail, app.UserPassword, app.UserEmail);
                        //var roles = Roles.GetAllRoles();
                        //Roles.AddUserToRole(app.UserEmail, roles[1]);

                        return new BLMessage { Message = "Application verified.", Result = SaveAttempts.Success };
                    }
                    else
                    {
                        return new BLMessage { Message = "Account already activated.", Result = SaveAttempts.Failure };
                    }
                }
                else
                {
                    return new BLMessage { Message = "Application could not be found.", Result = SaveAttempts.Failure };
                }
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementBusiness");
                return new BLMessage { Message = "An error occured during verifying application. Please try again later.", Result = SaveAttempts.Failure };
            }
        }

        public IBLMessage SendResetLink(string userEmail, string title, string content)
        {
            /*
             * Send reset link steps:
             * 1- Save reset request via user email and generated guid.
             * 2- Send reset link via user email.
             */

            var appResult = UserManagementDAL.SaveResetRequest(userEmail);
            try
            {
                IResetRequest resetRequest = UserManagementDAL.GetResetRequestByEmail(userEmail);
                if (resetRequest != null)
                {
                    EmailHelper.Send("email@domain.com", "p@ssw0rd", userEmail, title, String.Format(content, "website url", resetRequest.ResetGuid));
                }
                else
                {
                    return new BLMessage { Message = "Reset request could not be found.", Result = SaveAttempts.Failure };
                }
            }
            catch (Exception exception)
            {
                // delete entry...
                UserManagementDAL.DeleteApplication(userEmail);
                // log exception...
                Logger.Write(exception.Message, "UserManagementBusiness");
                return new BLMessage { Message = "An error occured during sending reset request. Please try again later.", Result = SaveAttempts.Failure };
            }
            return appResult;
        }

        public bool CheckResetPasswordGuid(string resetGuid)
        {
            IResetRequest resetRequest = UserManagementDAL.GetResetRequestByResetGuid(resetGuid);
            return resetRequest == null;
        }

        public IBLMessage ResetPassword(string resetGuid, string newPassword)
        {
            /*
             * Password reset steps:
             * 1- Get reset request via reset guid.
             * 2- Get Membershipuser via email from reset request.
             * 3- Update user password
             * 4- Delete reset request from db.
             */

            try
            {
                IResetRequest resetRequest = UserManagementDAL.GetResetRequestByResetGuid(resetGuid);
                if (resetRequest != null)
                {
                    MembershipUser user = Membership.GetUser(resetRequest.UserEmail);
                    if (user != null)
                    {
                        //var oldPAssword = user.GetPassword();
                        var changeResult = user.ChangePassword(user.ResetPassword(), newPassword);
                        if (changeResult)
                        {
                            return UserManagementDAL.DeleteResetRequest(resetGuid);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                // log exception...
                Logger.Write(exception.Message, "UserManagementBusiness");
            }
            return new BLMessage { Message = "An error occured during reseting password. Please try again later.", Result = SaveAttempts.Failure };
        }
    }
}
