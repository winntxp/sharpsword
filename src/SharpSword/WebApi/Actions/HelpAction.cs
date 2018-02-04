/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/18 8:59:14
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi.Actions
{
    /// <summary>
    /// 系统内部使用的帮助接口，用于显示所有合法的Action几个信息
    /// </summary>
    [ActionName("API.Help"), ResultCache(15), DisablePackageSdk, AllowAnonymous]
    [EnableRecordApiLog(false), Serializable, DisableDataSignatureTransmission]
    internal class HelpAction : ActionBase<HelpAction.HelpActionRequestDto, object>
    {
        /// <summary>
        /// 用于搜索
        /// </summary>
        internal class HelpActionRequestDto : RequestDtoBase //,IRequestDtoValidatable,IRequiredUserIdAndUserName
        {
            /// <summary>
            /// 关键词
            /// </summary>
            public string KeyWord { get; set; }
        }

        /// <summary>
        /// Action筛选器
        /// </summary>
        private readonly IActionSelector _actionSelector;
        private readonly ActionDocResourceManager _actionDocResourceManager;
        /// <summary>
        /// 类型查找器
        /// </summary>
        private ITypeFinder _typeFinder;

        /// <summary>
        /// 获取类型名称，方便查看
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <returns>返回指定类型的名称（重新实现）</returns>
        private string GetTypeName(Type type)
        {
            //判断下是否是基元类型
            if (type.IsPrimitive)
            {
                return type.Name;
            }

            //检测是否是集合类型或者可空类型（除去string类型，因为string类型也实现了IEnumerable接口）
            if (typeof(string) != type && new Type[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(Nullable<>) }
                .Any(t => t.Name == type.Name))
            {
                return "{0}<{1}>".With(type.Name, type.GetGenericArguments()[0].Name);
            }

            //不是集合类型或者可空类型直接返回类型名称
            return type.FullName;
        }

        /// <summary>
        /// 初始化帮助Action
        /// </summary>
        private HelpAction()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="actionSelector">Action筛选器</param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="actionDocResourceManager"></param>
        public HelpAction(IActionSelector actionSelector, ITypeFinder typeFinder, ActionDocResourceManager actionDocResourceManager)
            : this()
        {
            actionSelector.CheckNullThrowArgumentNullException(nameof(actionSelector));
            typeFinder.CheckNullThrowArgumentNullException(nameof(typeFinder));
            this._actionSelector = actionSelector;
            this._typeFinder = typeFinder;
            this._actionDocResourceManager = actionDocResourceManager;
        }

        /// <summary>
        /// 返回所有合法的Action集合
        /// </summary>
        /// <returns></returns>
        public override ActionResult<object> Execute()
        {
            //搜索关键词
            string keyWord = string.Empty;

            //给谁使用的
            if (!this.RequestDto.IsNull() && !string.IsNullOrWhiteSpace(this.RequestDto.KeyWord))
            {
                keyWord = this.RequestDto.KeyWord;
            }

            //排除系统框架提供的action信息
            var skipActionTypes = new Type[] { typeof(HelpAction), typeof(ErrorAction) };

            //获取所有合法的Action集合信息；处理掉系统默认的2个Action信息
            var actionTypes = this._actionSelector.GetActionDescriptors().Where(o => !skipActionTypes.Any(t => t == o.ActionType)).ToList();

            //搜索关键词
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                actionTypes = actionTypes.Where(o => o.ActionName.ToUpper().Contains(keyWord.ToUpper())
                    || o.Description.Contains(keyWord)
                    || o.AuthorName.ToUpper().Contains(keyWord.ToUpper())).ToList();
            }

            //搜索结果
            actionTypes = actionTypes.OrderBy(o => o.ActionName).ToList();

            //返回所有合法的Action集合
            return new ActionResult()
            {
                Flag = ActionResultFlag.SUCCESS,
                Info = "API接口系统所有合法的Action接口集合",
                Data = new
                {
                    //API接口总数
                    ApiCount = actionTypes.Count,

                    //服务器信息
                    Server = new
                    {
                        //IIS版本
                        IIS = this.RequestContext.HttpContext.Request.ServerVariables["SERVER_SOFTWARE"].ToString(),
                        //.NET版本信息
                        NetInfo = ".NET版本：.NET CLR {0}.{1}.{2}.{3}".With(Environment.Version.Major, Environment.Version.Minor, Environment.Version.Build, Environment.Version.Revision),
                        //操作系统
                        OS = Environment.OSVersion.ToString(),
                        //CPU个数
                        CPUNumber = Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"),
                        //CPU类型
                        CpuInfo = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"),
                        //IIS服务器地址
                        LocalAddr = this.RequestContext.HttpContext.Request.LocalAddr(),
                        //接口服务运行物理路径
                        PhysicalPath = this.RequestContext.HttpContext.Request.PhysicalApplicationPath
                    },

                    //所引用的程序集
                    RefAssemblys = from item in AppDomain.CurrentDomain.GetAssemblies() orderby item.FullName select item.FullName,

                    //接口集合
                    Actions = from item in actionTypes
                              select new
                              {
                                  //接口名称
                                  ActionName = item.ActionName,
                                  //作者
                                  Author = item.AuthorName,
                                  //类型名称
                                  TypeName = item.ActionType.FullName,
                                  //接口描述
                                  Description = this._actionDocResourceManager.GetDescription(item.ActionType.FullName),
                                  //请求对应的数据包
                                  RequestDTO = new
                                  {
                                      //请求DTO类型
                                      TypeName = item.ActionType.BaseType.GetGenericArguments()[0].FullName,
                                      //请求DTO属性集合
                                      Propertys = item.ActionType.BaseType.GetGenericArguments()[0].GetProperties().Select(o => new
                                      {
                                          //属性名称
                                          PropertyName = o.Name,
                                          //属性类型
                                          TypeName = this.GetTypeName(o.PropertyType),
                                          //描述信息
                                          Description = this._actionDocResourceManager.GetDescription(string.Format("{0}.{1}", item.ActionType.BaseType.GetGenericArguments()[0].FullName, o.Name))

                                      })
                                  },
                                  //输出对应的数据包
                                  ResponseDTO = new
                                  {
                                      //输出DTO类型
                                      TypeName = item.ActionType.BaseType.GetGenericArguments()[1].FullName,
                                      //输出DTO属性集合
                                      Propertys = item.ActionType.BaseType.GetGenericArguments()[1].GetProperties().Select(o => new
                                      {
                                          //属性名称
                                          Name = o.Name,
                                          //属性类型
                                          TypeName = this.GetTypeName(o.PropertyType)
                                      })
                                  },
                                  //HTTP提交方式
                                  HttpMethod = item.HttpMethod.ToString(),
                                  //是否已经被标注为过期
                                  IsObsolete = item.IsObsolete,
                                  //是否需要https请求
                                  RequireHttps = item.RequireHttps,
                                  //是否允许AJAX请求
                                  EnableAjaxRequest = item.EnableAjaxRequest,
                                  //是否需要校验用户名称是否提交
                                  RequiredUserIdAndUserName = item.RequiredUserIdAndUserName,
                                  //是否有缓存
                                  CacheInfo = item.Cache.CacheTime > 0 ? "入口参数请求级缓存，缓存时间:{0}分钟".With(item.Cache.CacheTime) : "无",
                                  //当前版本
                                  Version = item.Version,
                                  //所属程序集
                                  Assembly = item.ActionType.Assembly.FullName
                              }
                }
            };
        }
    }
}
