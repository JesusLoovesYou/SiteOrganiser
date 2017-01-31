using SiteOrganiser.Common.Messages;

namespace SiteOrganiser.Business
{
    public class BLMessage : IBLMessage
    {
        public string Message { get; set; }
        public SaveAttempts Result { get; set; }
    }
}
