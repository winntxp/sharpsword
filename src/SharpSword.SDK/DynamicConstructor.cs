/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/28/2017 12:29:07 PM
 * ****************************************************************/
using System.Dynamic;

namespace SharpSword.SDK
{
    /// <summary>
    /// 提供动态调用API接口
    /// </summary>
    public sealed class DynamicConstructor : DynamicObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiName"></param>
        /// <param name="apiClient"></param>
        public DynamicConstructor(string apiName, IApiClient apiClient)
        {
            this.ApiName = apiName;
            this.ApiClient = apiClient;
        }

        /// <summary>
        /// 
        /// </summary>
        public IApiClient ApiClient { get; set; }

        /// <summary>
        /// API接口名称
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name;
            result = new DynamicConstructor(this.ApiName + "." + name, this.ApiClient);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var name = binder.Name;
            //result = ((dynamic)this.ApiClient).Execute(new APIServerTimeGetRequest(), null, null);
            //result = new DynamicConstructor(this.ApiName + "." + name, this.ApiClient);
            result = null;
            return true;
        }
    }
}
