using System;
using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.Messages;

namespace SiteOrganiser.Presentation
{
    public interface IServerEditorView
    {
        string ServerUserName { get; set; }
        string Password { get; set; }
        string ServerName { get; set; }
        string DatabaseName { get; set; }
        int DatabaseType { get; set; }
        string TempFolder { get; set; }

        string SaveMessage { get; set; }
        bool IsSaveSuccessful { set; }
        event EventHandler<EventArgs> Save;
        event EventHandler<EventArgs> Loading;
    }
    public class ServerEditorPresenter : PresenterBase<IServerBusiness>
    {
        private IServerEditorView _view;

        public ServerEditorPresenter(IServerEditorView view)
        {
            _view = view;
            this.Initialize();
        }

        private void Initialize()
        {
            this._view.Save += _view_Save;
            this._view.Loading += _view_Loading;
        }

        private void _view_Loading(object sender, EventArgs e)
        {
        }

        private void _view_Save(object sender, EventArgs e)
        {
            try
            {
                IBLMessage message = bl.SaveServerInfo(_view.ServerUserName, _view.Password,
                                                       _view.ServerName, _view.DatabaseName,
                                                       _view.DatabaseType, _view.TempFolder);
                _view.SaveMessage = message.Message;
                _view.IsSaveSuccessful = message.Result == SaveAttempts.Success;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public IBLMessage TestServerConnection()
        {
            return bl.TestServerConnection(_view.TempFolder, _view.DatabaseName, _view.ServerName, _view.ServerUserName, _view.Password);
        }
    }
}
