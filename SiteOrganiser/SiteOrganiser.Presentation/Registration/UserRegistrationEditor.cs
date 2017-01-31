using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.Messages;
using System;
using System.Text;

namespace SiteOrganiser.Presentation.Registration
{
    public interface IUserRegistrationView
    {
        string CustomerName { get; set; }
        string CustomerSurname { get; set; }
        string CustomerFirmname { get; set; }
        string UserEmail { get; set; }
        string CustomerPhone { get; set; }
        string CustomerAddress { get; set; }
        string UserPassword { get; set; }
        string SendMessage { get; set; }
        bool IsSendSuccessful { get; set; }
        event EventHandler<EventArgs> SendApplication;
        event EventHandler<EventArgs> Loading;
    }
    public class UserRegistrationPresenter : PresenterBase<IUserManagementBusiness>
    {
        private IUserRegistrationView _view;

        public UserRegistrationPresenter(IUserRegistrationView view)
        {
            _view = view;
            this.Initialize();
        }

        private void Initialize()
        {
            this._view.SendApplication += _view_SendApplication;
            this._view.Loading += _view_Loading;
        }

        private void _view_Loading(object sender, EventArgs e)
        {
        }

        private void _view_SendApplication(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<p>Hello,</p>")
                    .Append("<br />")
                    .Append("<p>This email has been send to you by {0}. To activate your account click the link below.</p>")
                    .Append("<br />")
                    .Append("<p><a href='{0}/Kayit/KayitOnay.aspx?verify={1}'>{0}/Kayit/KayitOnay.aspx?verify={1}</a></p>")
                    .Append("<br />")
                    .Append("<p>Thanks!</p>");
                IBLMessage message = bl.SendApplication(_view.UserEmail, _view.UserPassword, "<site name> Verification", sb.ToString());
                _view.SendMessage = message.Message;
                _view.IsSendSuccessful = message.Result == SaveAttempts.Success;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
