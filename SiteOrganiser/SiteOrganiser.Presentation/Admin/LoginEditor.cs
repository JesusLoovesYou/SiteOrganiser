using Microsoft.Practices.EnterpriseLibrary.Logging;
using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.Messages;
using SiteOrganiser.Common.System;
using System;

namespace SiteOrganiser.Presentation.Admin
{
    public interface ILoginView
    {
        string UserName { get; set; }
        string Password { get; set; }

        event EventHandler<EventArgs> OnLogin;
        bool IsLoginSuccessful { set; }
        string LoginMessage { get; set; }
        ICustomPrincipal Principal { get; set; }
    }

    public class LoginPresenter : PresenterBase<IUserBusiness>
    {
        private ILoginView _view;

        public LoginPresenter(ILoginView view)
        {
            _view = view;
            this.Initialize();
        }

        private void Initialize()
        {
            this._view.OnLogin += _view_Login;
        }

        void _view_Login(object sender, EventArgs e)
        {
            try
            {
                ILoginMessages message = bl.LoginUser(this._view.UserName, this._view.Password);
                _view.LoginMessage = message.Message;
                _view.Principal = message.Principal;
                // IsLoginSuccessful: bu değişken atanmadan önce LoginMessage ve Principal değişkenleri atanmak zorunda.
                _view.IsLoginSuccessful = message.LoginResult == LoginAttempts.Success;
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "PresentationLayer");
                throw;
            }
        }
    }
}
