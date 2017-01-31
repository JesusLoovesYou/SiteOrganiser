using SiteOrganiser.Common.Messages;
using SiteOrganiser.Common.System;

namespace SiteOrganiser.Business
{
    public class LoginMessages : ILoginMessages
    {
        private LoginAttempts _loginResult = LoginAttempts.UnknownUser;
        private string _message = string.Empty;
        private ICustomPrincipal _principal;

        public LoginAttempts LoginResult
        {
            get { return _loginResult; }
            set { _loginResult = value; }
        }

        public ICustomPrincipal Principal
        {
            get { return _principal; }
            set { _principal = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
