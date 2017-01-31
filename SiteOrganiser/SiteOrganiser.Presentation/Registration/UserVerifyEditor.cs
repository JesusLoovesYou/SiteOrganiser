using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.Messages;
using System;

namespace SiteOrganiser.Presentation.Registration
{
    public interface IUserVerifyView
    {
        string VerifyGuid { get; }
        string VerifyMessage { get; set; }
        bool IsVerifySuccessful { get; set; }
        event EventHandler<EventArgs> Verifying;
    }
    public class UserVerifyPresenter : PresenterBase<IUserManagementBusiness>
    {
        private IUserVerifyView _view;

        public UserVerifyPresenter(IUserVerifyView view)
        {
            _view = view;
            this.Initialize();
        }

        private void Initialize()
        {
            this._view.Verifying += _view_Verifying;
        }

        private void _view_Verifying(object sender, EventArgs e)
        {
            try
            {
                IBLMessage message = bl.VerifyApplication(_view.VerifyGuid);
                _view.VerifyMessage = message.Message;
                _view.IsVerifySuccessful = message.Result == SaveAttempts.Success;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
