/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System.Configuration;

namespace SharpSword.OAuth
{
    public class OAuthConfig : ConfigurationSection
    {
        #region platforms

        [ConfigurationProperty("platforms", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(PlatformElementCollection), AddItemName = "platform")]
        public PlatformElementCollection Platforms
        {
            get { return (PlatformElementCollection)base["platforms"]; }
            set { base["platforms"] = value; }
        }

        #endregion

        #region AuthorizationProviders

        [ConfigurationProperty("authorizationProviders", IsDefaultCollection = false)]
        public AuthorizationProviderElemetCollection AuthorizationProviders
        {
            get { return (AuthorizationProviderElemetCollection)base["authorizationProviders"]; }
            set { base["authorizationProviders"] = value; }
        }

        #endregion      
    }

    #region ElementCollection

    public class PlatformElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PlatformElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PlatformElement)element).Name;
        }

        protected override string ElementName
        {
            get { return "platform"; }
        }

        public new PlatformElement this[string name]
        {
            get { return BaseGet(key: name) as PlatformElement; }
        }

        public PlatformElement this[int index]
        {
            get { return BaseGet(index: index) as PlatformElement; }
        }
    }

    public class AuthorizationProviderElemetCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AuthorizationProviderElemet();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AuthorizationProviderElemet)element).Platform;
        }

        public new AuthorizationProviderElemet this[string name]
        {
            get { return BaseGet(key: name) as AuthorizationProviderElemet; }
        }

        public AuthorizationProviderElemet this[int index]
        {
            get { return BaseGet(index: index) as AuthorizationProviderElemet; }
        }
    }

    public class AppElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AppElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AppElement)element).Appkey;
        }

        public new AppElement this[string name]
        {
            get { return BaseGet(key: name) as AppElement; }
        }

        public AppElement this[int index]
        {
            get { return BaseGet(index: index) as AppElement; }
        }
    }

    #endregion

    #region Element

    public class PlatformElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("fullName", IsKey = true, IsRequired = true)]
        public string FullName
        {
            get { return (string)base["fullName"]; }
            set { base["fullName"] = value; }
        }

        [ConfigurationProperty("authorizationUrl")]
        public string AuthorizationUrl
        {
            get { return (string)base["authorizationUrl"]; }
            set { base["authorizationUrl"] = value; }
        }

        [ConfigurationProperty("tokenUrl")]
        public string TokenUrl
        {
            get { return (string)base["tokenUrl"]; }
            set { base["tokenUrl"] = value; }
        }

        [ConfigurationProperty("apiUrl")]
        public string ApiUrl
        {
            get { return (string)base["apiUrl"]; }
            set { base["apiUrl"] = value; }
        }

        [ConfigurationProperty("apps", IsDefaultCollection = false)]
        public AppElementCollection Apps
        {
            get { return (AppElementCollection)base["apps"]; }
            set { base["apps"] = value; }
        }

    }

    public class AppElement : ConfigurationElement
    {
        [ConfigurationProperty("appkey", IsKey = true, IsRequired = true)]
        public string Appkey
        {
            get { return (string)base["appkey"]; }
            set { base["appkey"] = value; }
        }

        [ConfigurationProperty("secret")]
        public string Secret
        {
            get { return (string)base["secret"]; }
            set { base["secret"] = value; }
        }

        [ConfigurationProperty("redirectUrl")]
        public string RedirectUrl
        {
            get { return (string)base["redirectUrl"]; }
            set { base["redirectUrl"] = value; }
        }
    }

    public class AuthorizationProviderElemet : ConfigurationElement
    {
        [ConfigurationProperty("platform", IsKey = true, IsRequired = true)]
        public string Platform
        {
            get { return (string)base["platform"]; }
            set { base["platform"] = value; }
        }

        [ConfigurationProperty("type")]
        public string Type
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }

    #endregion
}
