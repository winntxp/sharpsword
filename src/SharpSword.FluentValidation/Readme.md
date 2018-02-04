
此项目为RequestDto验证组件IRequestDtoValidator的FluentValidation实现，如果想使用FluentValidation进行上送参数校验，HOST项目直接用于此项目即可

然后新建验证类，如我们需要验证一个下面的上送RequestDto参数正确性：
========================================================================
/// <summary>
/// 上送RequestDto
/// </summary>
[Serializable]
public class IdsGetActionRequest : RequestDtoBase
{
	/// <summary>
    /// 上送时间
    /// </summary>
    public DateTime Date { get; set; }
}
========================================================================
//验证配置类
public class IdsGetActionRequestValidator : RequestDtoFluentValidationBase<IdsGetActionRequest>
{
   /// <summary>
   /// 
   /// </summary>
   public IdsGetActionRequestValidator()
   {
       this.RuleFor(o => o.Date).GreaterThan(new DateTime(2011, 1, 1)).WithMessage("时间必须大于2011-1-1");
   }
}

查看更加详细的关于FluentValidation组件使用，可以查看：https://github.com/JeremySkinner/FluentValidation/wiki