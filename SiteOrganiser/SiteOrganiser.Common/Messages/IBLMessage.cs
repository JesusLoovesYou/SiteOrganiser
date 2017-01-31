namespace SiteOrganiser.Common.Messages
{
    public interface IBLMessage
    {
        string Message { get; set; }
        SaveAttempts Result { get; set; }
    }

    public enum SaveAttempts
    {
        Success = 1,
        Failure,
        UnknownReason
    }
}
