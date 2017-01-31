using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.Management;
using SiteOrganiser.Common.Messages;
using System;

namespace SiteOrganiser.Presentation.Admin
{
    public interface IUserAccountView
    {
        string UserKey { get; }
        string UserEmail { get; set; }
        string OldPassword { get; }
        string NewPassword { get; }
        string SaveMessage { get; set; }
        bool IsSaveSuccessful { get; set; }
        event EventHandler<EventArgs> EditUser;
        event EventHandler<EventArgs> UpdateAccountInfo;
        event EventHandler<EventArgs> SaveNewUser;
        event EventHandler<EventArgs> Loading;
    }
    public class UserAccountPresenter : PresenterBase<IAdminBusiness>
    {
        private IUserAccountView _view;

        public UserAccountPresenter(IUserAccountView view)
        {
            _view = view;
            this.Initialize();
        }

        private void Initialize()
        {
            this._view.EditUser += _view_EditUser;
            this._view.UpdateAccountInfo += _view_UpdateAccountInfo;
            this._view.SaveNewUser += _view_SaveNewUser;
            this._view.Loading += _view_Loading;
        }

        private void _view_EditUser(object sender, EventArgs e)
        {
            try
            {
                IBLMessage message = bl.EditUser(_view.UserEmail, _view.NewPassword, _view.UserKey);
                _view.SaveMessage = message.Message;
                _view.IsSaveSuccessful = message.Result == SaveAttempts.Success;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        private void _view_SaveNewUser(object sender, EventArgs e)
        {
            try
            {
                IBLMessage message = bl.CreateUser(_view.UserEmail, _view.NewPassword);
                _view.SaveMessage = message.Message;
                _view.IsSaveSuccessful = message.Result == SaveAttempts.Success;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        private void _view_UpdateAccountInfo(object sender, EventArgs e)
        {
            try
            {
                IBLMessage message = bl.UpdateAccountInfo(_view.UserEmail, _view.OldPassword, _view.NewPassword, _view.UserKey);
                _view.SaveMessage = message.Message;
                _view.IsSaveSuccessful = message.Result == SaveAttempts.Success;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        private void _view_Loading(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_view.UserKey))
            {
                IRegisteredUser user = bl.GetUser(_view.UserKey);
                if (user != null)
                {
                    _view.UserEmail = user.Email;
                }
            }
        }
    }
}
