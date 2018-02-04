/* ****************************************************************
 * SharpSword zhangliang4629@163.com 12/15/2016 3:57:02 PM
 * ****************************************************************/

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 实体需要记录创建信息，修改信息
    /// </summary>
    public interface IFullAudited : ICreationAudited, IModificationAudited
    {
    }
}
