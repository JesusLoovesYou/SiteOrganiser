using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.Messages;
using System;

namespace SiteOrganiser.Presentation.Admin
{
    public interface IAjaxUnlockUserView
    {
        string UserEmail { get; }
        string UnlockMessage { get; set; }
        bool IsUnlockSuccessful { set; }
        event EventHandler<EventArgs> Loading;
    }
    public class AjaxUnlockUserPresenter : PresenterBase<IAdminBusiness>
    {
        private IAjaxUnlockUserView _view;

        public AjaxUnlockUserPresenter(IAjaxUnlockUserView view)
        {
            _view = view;
            this.Initialize();
        }

        private void Initialize()
        {
            this._view.Loading += _view_Loading;
        }

        private void _view_Loading(object sender, EventArgs e)
        {
            // delete transport type from dorm as page loads.
            try
            {
                IBLMessage message = bl.ChangeUserLock(_view.UserEmail);
                _view.UnlockMessage = message.Message;
                _view.IsUnlockSuccessful = message.Result == SaveAttempts.Success;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
