using System.Security.Principal;

namespace Wey.Domain
{
    public class ValidUserContext
    {
        public UserWithRoles User { get; set; }
        public GenericPrincipal Principal { get; set; }
    }
}