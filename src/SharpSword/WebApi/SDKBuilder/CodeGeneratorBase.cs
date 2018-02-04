/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/12 10:28:08
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 代码生成器期基类；基础类命名规则：语言名称+SDKCodeGenerator方式，因为创建实例的工厂方法会搜索实现类名称来创建实例
    /// </summary>
    public abstract class CodeGeneratorBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ActionDocResourceManager ActionDocResourceManager { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDocResourceManager"></param>
        protected CodeGeneratorBase(ActionDocResourceManager actionDocResourceManager)
        {
            this.ActionDocResourceManager = actionDocResourceManager;
        }

        /// <summary>
        /// <![CDATA[是否是集合类类型，如果类型包含下列类型即判断为集合类型：IEnumerable<>,ICollection<>,IList<>,List<>,Array]]>
        /// </summary>
        /// <param name="type">任意数据类型</param>
        /// <returns></returns>
        public bool IsCollection(Type type)
        {
            return new Type[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>) }.Any(t => t.Name == type.Name) || type.IsArray;
        }

        /// <summary>
        /// 是否是基元类型(此方法扩展了C#框架里在的基元类型判断，增加了一些类型，比如：datatime等，还包含了可空值类型)
        /// </summary>
        /// <param name="type">任意数据类型</param>
        /// <returns></returns>
        public bool IsPrimitive(Type type)
        {
            if (type.IsPrimitive || new Type[] {   
                typeof(string),  
                typeof(int?),
                typeof(bool?),
                typeof(long?),
                typeof(float?),
                typeof(short?),
                typeof(double?),
                typeof(Single?),
                typeof(DateTime), 
                typeof(DateTime?), 
                typeof(decimal), 
                typeof(decimal?), 
                typeof(TimeSpan), 
                typeof(TimeSpan?), 
                typeof(DateTimeOffset), 
                typeof(Guid) }
                .Any(t => t == type))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断某个对象是否是复杂类，注意如果类型为集合类型，那么将会判断集合元素类型是否是复杂类型；如果集合元素不是复杂类型，那么返回的也是false
        /// </summary>
        /// <param name="type">任意类型</param>
        /// <returns></returns>
        public bool IsComplexType(Type type)
        {
            if (this.IsPrimitive(type))
            {
                return false;
            }
            else if (typeof(Nullable<>).Name == type.Name)
            {
                return false;
            }
            else if (this.IsCollection(type))
            {
                var genericArgument = type.IsArray ? type.GetElementType() : type.GetGenericArguments()[0];
                if (this.IsPrimitive(genericArgument))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取一个集合里的对象类型（这里的集合必须是泛型集合）
        /// </summary>
        /// <param name="type">
        /// <![CDATA[支持泛型集合类型：IEnumerable<>,ICollection<>,IList<>,List<>,Array]]>
        /// </param>
        /// <returns>如果非泛型集合类型，返回null</returns>
        public Type GetCollectionElementype(Type type)
        {
            if (this.IsCollection(type))
            {
                return type.IsArray ? type.GetElementType() : type.GetGenericArguments()[0];
            }
            return null;
        }

        /// <summary>
        /// 获取自定义复杂类型里的所有复杂类（方便构造出来后进行内部类的输出）
        /// </summary>
        /// <param name="objType">任意对象</param>
        /// <param name="results">返回对象内部所有的复杂类（遍历整个对象树）</param>
        public void GetComplexObjTypes(Type objType, IList<Type> results)
        {
            //复杂对象才去遍历
            if (this.IsComplexType(objType))
            {
                //如果是集合的话，输出特殊处理
                if (this.IsCollection(objType))
                {
                    objType = objType.GetGenericArguments()[0];
                    if (!results.Contains(objType))
                    {
                        results.Add(objType);
                    }
                }

                //遍历对象属性树
                foreach (var p in objType.GetProperties())
                {
                    //基元类型，直接返回
                    if (this.IsPrimitive(p.PropertyType))
                    {
                        continue;
                    }
                    //可空类型
                    else if (typeof(Nullable<>).Name == p.PropertyType.Name)
                    {
                        //基类型
                        var baseType = p.PropertyType.GetGenericArguments()[0].BaseType;

                        //当前类型
                        var genericArgumentType = p.PropertyType.GetGenericArguments()[0];

                        //可空类型是枚举或者是结构体（是枚举需要输出枚举类）
                        if (baseType == typeof(Enum)) //|| baseType == typeof(System.ValueType))
                        {
                            if (!results.Contains(genericArgumentType))
                            {
                                results.Add(genericArgumentType);
                            }
                        }

                        //否则继续下一个属性
                        continue;
                    }
                    //集合类型
                    else if (this.IsCollection(p.PropertyType))
                    {
                        //集合类参数
                        var genericArgument = this.GetCollectionElementype(p.PropertyType);

                        //集合里的对象是否是复杂自定义对象
                        if (!this.IsPrimitive(genericArgument))
                        {
                            //添加到复杂对象集合
                            if (!results.Contains(genericArgument))
                            {
                                //添加到集合
                                results.Add(genericArgument);

                                //继续进行对象树访问
                                this.GetComplexObjTypes(genericArgument, results);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    //复杂自定义对象
                    else
                    {
                        //添加到复杂对象集合
                        if (!results.Contains(p.PropertyType))
                        {
                            results.Add(p.PropertyType);

                            //继续进行对象树访问
                            this.GetComplexObjTypes(p.PropertyType, results);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取复杂类型对象属性描述集合
        /// </summary>
        /// <param name="type">复杂对象类型</param>
        /// <returns></returns>
        public virtual IEnumerable<ComplexObjPropertyTypeDescriptor> GetComplexObjPropertyTypeDescriptors(Type type)
        {
            //基元类型，直接返回空的集合
            if (this.IsPrimitive(type))
            {
                return new List<ComplexObjPropertyTypeDescriptor>();
            }

            //返回属性描述对象集合
            return type.GetPropertiesInfo().Select(property => new ComplexObjPropertyTypeDescriptor()
            {
                MemberName = property.Name,
                MemberType = property.PropertyType,
                Description = this.ActionDocResourceManager.GetDescription("{0}.{1}".With(type.FullName, property.Name)),
                DisplayType = property.PropertyType.Name,
                IsRequire =
                    property.PropertyType.IsPrimitive ||
                    property.GetCustomAttributes(typeof (RequiredAttribute), false).Any()
            });
        }

        /// <summary>
        /// 基元类型数据赋值，判断可空类型
        /// </summary>
        /// <param name="property">对象属性</param>
        /// <param name="obj">对象</param>
        /// <param name="value">属性值</param>
        protected void SetPropertyValue(object obj, PropertyInfo property, object value)
        {
            //待转换成的数据类型
            Type convertType = property.PropertyType;

            //判断下映射实体属性是否是可空类型;是空类型需要特殊处理
            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                NullableConverter nullableConverter = new NullableConverter(property.PropertyType);
                convertType = nullableConverter.UnderlyingType;
            }
            //对异常不处理
            try
            {
                //数据转型
                var propertyValue = Convert.ChangeType(value, convertType);
                //设置属性值,必须为可写属性
                if (property.CanWrite)
                {
                    property.SetValue(obj, propertyValue);
                }
            }
            catch { }
        }

        /// <summary>
        /// 属性转换，此方法会调用CreateTypeMapping()抽象方法
        /// </summary>
        /// <param name="type">数据类型转换</param>
        /// <returns></returns>
        public virtual string ConvertPropertyType(Type type)
        {
            //属性类型名称
            string typeName;

            //基元类型 
            if (this.IsPrimitive(type))
            {
                typeName = type.Name;
            }
            //可空类型
            else if (typeof(Nullable<>).Name == type.Name)
            {
                typeName = type.GetGenericArguments()[0].Name + "?";
            }
            //集合类型
            else if (new Type[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>) }.Any(t => t.Name == type.Name))   //集合类型
            {
                typeName = "{0}<{1}>".With(type.Name.Substring(0, type.Name.Length - 2), type.GetGenericArguments()[0].Name);
            }
            else
            {
                typeName = type.Name;
            }

            //检查映射文件是否存在类型转换
            if (this.CreateTypeMapping().Keys.Contains(typeName))
            {
                return this.CreateTypeMapping()[typeName];
            }

            //不存在类型转换直接输出点类型名称
            return type.Name;
        }

        /// <summary>
        /// 属性是否必须有值
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        protected bool IsRequired(PropertyInfo propertyInfo)
        {
            var x = propertyInfo.GetCustomAttribute<RequiredAttribute>(false);
            return !x.IsNull();
        }

        /// <summary>
        /// 创建类型转换映射表;针对C#属性，转换成对应的客户端语言类型
        /// </summary>
        /// <returns></returns>
        protected abstract Dictionary<string, string> CreateTypeMapping();
    }
}
