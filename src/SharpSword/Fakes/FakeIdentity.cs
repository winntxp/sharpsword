using System;
using System.Security.Principal;

namespace SharpSword.Fakes
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeIdentity'
    public class FakeIdentity : IIdentity
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeIdentity'
    {
        private readonly string _name;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeIdentity.FakeIdentity(string)'
        public FakeIdentity(string userName)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeIdentity.FakeIdentity(string)'
        {
            _name = userName;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeIdentity.AuthenticationType'
        public string AuthenticationType
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeIdentity.AuthenticationType'
        {
            get { throw new NotImplementedException(); }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeIdentity.IsAuthenticated'
        public bool IsAuthenticated
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeIdentity.IsAuthenticated'
        {
            get { return !String.IsNullOrEmpty(_name); }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeIdentity.Name'
        public string Name
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeIdentity.Name'
        {
            get { return _name; }
        }

    }
}