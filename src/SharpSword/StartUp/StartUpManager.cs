/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/19 8:54:06
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// 系统启动项管理器
    /// </summary>
    internal class StartUpManager
    {
        /// <summary>
        /// 启动实现了IStartUp接口的所有类
        /// </summary>
        /// <param name="typeFinder">类型查找器</param>
        public static void Start(ITypeFinder typeFinder)
        {
            typeFinder.CheckNullThrowArgumentNullException(nameof(typeFinder));
            using (var scope = ServicesContainer.Current.Scope())
            {
                IList<IStartUp> startUps = new List<IStartUp>();

                //创建出所有待启动的对象
                typeFinder.FindClassesOfType<IStartUp>().ToList().ForEach(type =>
                {
                    var startUp = ((IStartUp)ServicesContainer.Current.ResolveUnregistered(type, scope));
                    startUps.Add(startUp);
                });

                //排序后进行启动
                startUps.OrderByDescending(o => o.Priority).ToList().ForEach(startup =>
                {
                    startup.Init();

                    //清理下资源(如果StartUp实现了IDisposable接口)
                    if (startup is IDisposable)
                    {
                        ((IDisposable)startup).Dispose();
                    }
                });
            }
        }
    }
}
