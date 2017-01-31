using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.Messages;
using System;

namespace SiteOrganiser.Presentation.Admin
{
    public interface IAjaxUserDeleteView
    {
        string UserEmail { get; }
        string DeleteMessage { get; set; }
        bool IsDeleteSuccessful { set; }
        event EventHandler<EventArgs> Loading;
    }
    public class AjaxUserDeletePresenter : PresenterBase<IAdminBusiness>
    {
        private IAjaxUserDeleteView _view;

        public AjaxUserDeletePresenter(IAjaxUserDeleteView view)
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
                IBLMessage message = bl.DeleteUser(_view.UserEmail);
                _view.DeleteMessage = message.Message;
                _view.IsDeleteSuccessful = message.Result == SaveAttempts.Success;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
