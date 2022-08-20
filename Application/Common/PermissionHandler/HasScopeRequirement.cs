using Microsoft.AspNetCore.Authorization;

namespace Application.Common.PermissionHandler
{
    public  class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }

        public HasScopeRequirement (string scope, string issuer)
        {
            this.Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            this.Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
    }
}
