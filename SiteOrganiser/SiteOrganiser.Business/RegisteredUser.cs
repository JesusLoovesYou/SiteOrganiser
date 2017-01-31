using System;
using SiteOrganiser.Common.Management;

namespace SiteOrganiser.Business
{
    public class RegisteredUser : IRegisteredUser
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Online { get; set; }
        public bool IsLocked { get; set; }
    }
}
