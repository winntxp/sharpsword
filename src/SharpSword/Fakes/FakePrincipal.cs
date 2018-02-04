using System.Linq;
using System.Security.Principal;

namespace SharpSword.Fakes
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakePrincipal'
    public class FakePrincipal : IPrincipal
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakePrincipal'
    {
        private readonly IIdentity _identity;
        private readonly string[] _roles;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakePrincipal.FakePrincipal(IIdentity, string[])'
        public FakePrincipal(IIdentity identity, string[] roles)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakePrincipal.FakePrincipal(IIdentity, string[])'
        {
            _identity = identity;
            _roles = roles;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakePrincipal.Identity'
        public IIdentity Identity
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakePrincipal.Identity'
        {
            get { return _identity; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakePrincipal.IsInRole(string)'
        public bool IsInRole(string role)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakePrincipal.IsInRole(string)'
        {
            return _roles != null && _roles.Contains(role);
        }
    }
}