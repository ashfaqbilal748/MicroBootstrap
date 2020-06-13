using Microsoft.AspNetCore.Authorization;

namespace MicroBootstrap.Authentication
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute(string scheme, string policy = "") : base(policy)
        {
            AuthenticationSchemes = scheme;
        }
    }
}