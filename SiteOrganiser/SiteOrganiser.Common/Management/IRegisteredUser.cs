using System;

namespace SiteOrganiser.Common.Management
{
    public interface IRegisteredUser
    {
        string UserId { get; set; }
        string Email { get; set; }
        DateTime RegistrationDate { get; set; }
        bool Online { get; set; }
        bool IsLocked { get; set; }
    }
}
