using SiteOrganiser.Common.Messages;

namespace SiteOrganiser.DataAccess.Messages
{
    public class DBMessage : IBLMessage
    {
        public string Message { get; set; }
        public SaveAttempts Result { get; set; }
    }
}
