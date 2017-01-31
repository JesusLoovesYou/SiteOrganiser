using System;
using System.Collections.Generic;
using SiteOrganiser.Common.System;

namespace SiteOrganiser.Common.Messages
{
    public interface ILoginMessages
    {
        LoginAttempts LoginResult { get; set; }
        ICustomPrincipal Principal { get; set; }
        string Message { get; set; }
    }

    public interface ICredential
    {
        Guid UserId { get; }
        string UserName { get; }
        List<IRole> UserRoles { get; }
        bool Success { get; }
    }

    public enum LoginAttempts
    {
        Success = 1,
        UnknownUser,
        ForbiddenTerminal,
        UnAuthorizedUser
    }
}
