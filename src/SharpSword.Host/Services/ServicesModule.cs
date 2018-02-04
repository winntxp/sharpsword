/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/28/2016 8:43:31 AM
 * ****************************************************************/
using Autofac;
using Autofac.Core;
using SharpSword.Domain.Services;

namespace SharpSword.Host.Services
{
    /// <summary>
    /// 演示， 用于获取有相同参数合参数名称的类反转
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="holding"></param>
    /// <returns></returns>

    public delegate IShareholding Factory(string symbol, uint holding);

    /// <summary>
    /// 
    /// </summary>
    public interface IShareholding
    {
        string Symbol { get; }

        uint Holding { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Shareholding : IShareholding
    {
        public Shareholding(string symbol, uint holding)
        {
            Symbol = symbol;
            Holding = holding;
        }

        public string Symbol { get; private set; }

        public uint Holding { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Shareholding0 : IShareholding
    {
        public Shareholding0(string symbol1, uint holding)
        {
            Symbol = symbol1;
            Holding = holding;
        }

        public string Symbol { get; private set; }

        public uint Holding { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ServicesModule : Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Shareholding>().AsImplementedInterfaces();
            //builder.RegisterType<Shareholding0>().AsImplementedInterfaces();
            base.Load(builder);
        }

        /// <summary>
        /// 示例同一类，注入不同的缓存（使用多个缓存服务器）
        /// </summary>
        /// <param name="componentRegistry"></param>
        /// <param name="registration"></param>
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            if (typeof(SharpSwordServicesBase).IsAssignableFrom(registration.Activator.LimitType))
            {
                registration.Activating += (c, e) =>
                {
                    var ps = e.Instance.GetType().GetProperties();
                    foreach (var item in ps)
                    {
                        if (item.PropertyType == typeof(ICacheManager) && item.Name == "MemoryCacheManager")
                        {
                            item.SetValue(e.Instance, e.Context.ResolveNamed<ICacheManager>("cache_static"));
                            break;
                        }
                    }
                };
            }
        }
    }
}

