using SiteOrganiser.Common.Business;
using SiteOrganiser.Common.Messages;
using System;
using System.Text;

namespace SiteOrganiser.Presentation.Registration
{
    public interface IForgotPasswordView
    {
        string UserEmail { get; }
        string SendMessage { get; set; }
        bool IsSendSuccessful { get; set; }
        event EventHandler<EventArgs> SendResetLink;
    }
    public class ForgotPasswordPresenter : PresenterBase<IUserManagementBusiness>
    {
        private IForgotPasswordView _view;

        public ForgotPasswordPresenter(IForgotPasswordView view)
        {
            _view = view;
            this.Initialize();
        }

        private void Initialize()
        {
            this._view.SendResetLink += _view_SendResetLink;
        }

        private void _view_SendResetLink(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<p>Merhaba,</p>")
                    .Append("<br />")
                    .Append("<p>Bu email size {0} sitesinden gönderildi. Şifrenizi yenileyebilmeniz için lütfen aşağıdaki linke tıklayınız.</p>")
                    .Append("<br />")
                    .Append("<p><a href='{0}/Kayit/SifreYenile.aspx?pwg={1}'>{0}/Kayit/SifreYenile.aspx?pwg={1}</a></p>")
                    .Append("<br />")
                    .Append("<p>Teşekkürler!</p>");
                IBLMessage message = bl.SendResetLink(_view.UserEmail, "<Site Adı> Şifre Yenileme", sb.ToString());
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
