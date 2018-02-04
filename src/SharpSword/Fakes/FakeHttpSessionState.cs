using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

namespace SharpSword.Fakes
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState'
    public class FakeHttpSessionState : HttpSessionStateBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState'
    {
        private readonly SessionStateItemCollection _sessionItems;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.FakeHttpSessionState(SessionStateItemCollection)'
        public FakeHttpSessionState(SessionStateItemCollection sessionItems)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.FakeHttpSessionState(SessionStateItemCollection)'
        {
            _sessionItems = sessionItems;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.Count'
        public override int Count
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.Count'
        {
            get { return _sessionItems.Count; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.Keys'
        public override NameObjectCollectionBase.KeysCollection Keys
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.Keys'
        {
            get { return _sessionItems.Keys; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.this[string]'
        public override object this[string name]
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.this[string]'
        {
            get { return _sessionItems[name]; }
            set { _sessionItems[name] = value; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.Exists(string)'
        public bool Exists(string key)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.Exists(string)'
        {
            return _sessionItems[key] != null;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.this[int]'
        public override object this[int index]
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.this[int]'
        {
            get { return _sessionItems[index]; }
            set { _sessionItems[index] = value; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.Add(string, object)'
        public override void Add(string name, object value)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.Add(string, object)'
        {
            _sessionItems[name] = value;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.GetEnumerator()'
        public override IEnumerator GetEnumerator()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.GetEnumerator()'
        {
            return _sessionItems.GetEnumerator();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.Remove(string)'
        public override void Remove(string name)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpSessionState.Remove(string)'
        {
            _sessionItems.Remove(name);
        }
    }
}