using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.Messages;
using System;

namespace SiteOrganiser.Presentation.Registration
{
    public interface IResetPasswordView
    {
        bool GuidNotFound { get; set; }
        string ResetGuid { get; }
        string NewPassword { get; }
        string ResetMessage { get; set; }
        bool IsResetSuccessful { get; set; }
        event EventHandler<EventArgs> ResetPassword;
        event EventHandler<EventArgs> Loading;
    }
    public class ResetPasswordPresenter : PresenterBase<IUserManagementBusiness>
    {
        private IResetPasswordView _view;

        public ResetPasswordPresenter(IResetPasswordView view)
        {
            _view = view;
            this.Initialize();
        }

        private void Initialize()
        {
            this._view.ResetPassword += _view_ResetPassword;
            this._view.Loading += _view_Loading;
        }

        private void _view_Loading(object sender, EventArgs e)
        {
            _view.GuidNotFound = bl.CheckResetPasswordGuid(_view.ResetGuid);
        }

        private void _view_ResetPassword(object sender, EventArgs e)
        {
            try
            {
                IBLMessage message = bl.ResetPassword(_view.ResetGuid, _view.NewPassword);
                _view.ResetMessage = message.Message;
                _view.IsResetSuccessful = message.Result == SaveAttempts.Success;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
