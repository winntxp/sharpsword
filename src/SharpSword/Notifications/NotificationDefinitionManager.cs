/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/29/2016 3:03:06 PM
 * *******************************************************/
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SharpSword.Notifications
{
    /// <summary>
    /// 实现了INotificationDefinitionManager接口，消息类型管理器接口实现, 需要注册成 单例 模式
    /// </summary>
    internal class NotificationDefinitionManager : INotificationDefinitionManager, ISingletonDependency
    {
        private readonly INotificationConfiguration _configuration;
        private readonly IIocResolver _iocResolver;
        private readonly IDictionary<string, NotificationDefinition> _notificationDefinitions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iocResolver"></param>
        /// <param name="configuration"></param>
        public NotificationDefinitionManager(IIocResolver iocResolver, INotificationConfiguration configuration)
        {
            this._configuration = configuration;
            this._iocResolver = iocResolver;
            this._notificationDefinitions = new Dictionary<string, NotificationDefinition>();
        }

        /// <summary>
        /// 初始化一下
        /// </summary>
        public void Initialize()
        {
            var context = new NotificationDefinitionContext(this);

            using (var scope = this._iocResolver.Scope("Init"))
            {
                foreach (var providerType in _configuration.Providers)
                {
                    var provider = !this._iocResolver.IsRegistered(providerType, scope)
                        ? (NotificationDefinitionProvider)this._iocResolver.ResolveUnregistered(providerType, scope)
                        : (NotificationDefinitionProvider)this._iocResolver.Resolve(providerType, scope);

                    provider.SetNotificationDefinitions(context);
                }
            }
        }

        /// <summary>
        /// 添加一个消息类型
        /// </summary>
        /// <param name="notificationDefinition"></param>
        public void Add(NotificationDefinition notificationDefinition)
        {
            if (_notificationDefinitions.ContainsKey(notificationDefinition.Name))
            {
                throw new SharpSwordCoreException("已经存在名称为: " + notificationDefinition.Name + " 的消息类型. 消息类型名称必须唯一");
            }
            _notificationDefinitions[notificationDefinition.Name] = notificationDefinition;
        }

        /// <summary>
        /// 根据消息类型名称获取消息类型详细信息，如果不存在，请实现为返回null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public NotificationDefinition Get(string name)
        {
            return _notificationDefinitions.GetValueOrDefault(name);
        }

        /// <summary>
        /// 获取框架注册的所有消息类型
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<NotificationDefinition> GetAll()
        {
            return _notificationDefinitions.Values.ToImmutableList();
        }
    }
}
