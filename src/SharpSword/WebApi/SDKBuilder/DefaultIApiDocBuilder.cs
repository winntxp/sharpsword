/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/20 16:03:59
 * ****************************************************************/
using SharpSword.Timing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口默认提供的仿真数据提供器(具体业务请在外部继承此实现类，重写DefaultValueMapping()方法即可)
    /// </summary>
    public class DefaultIApiDocBuilder : CodeGeneratorBase, IApiDocBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDocResourceManager"></param>
        public DefaultIApiDocBuilder(ActionDocResourceManager actionDocResourceManager)
            : base(actionDocResourceManager)
        {
        }

        /// <summary>
        /// 对外的数据类型显示映射
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, string> CreateTypeMapping()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取测试数据的映射（一般来说，需要进行数据仿真的话，只要重写此方法即可）
        /// </summary>
        /// <returns></returns>
        protected virtual IList<DefaultValueItemConfig> DefaultValueMapping()
        {
            IList<DefaultValueItemConfig> mapping = new List<DefaultValueItemConfig>();

            mapping.Add(new DefaultValueItemConfig(typeof(string), "演示数据", ""));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "九阳豆浆机", "ProductName"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "食品", "CategoryName", "CategoryName1", "CategoryName2", "CategoryName3"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "Administrator", "UserName", "ModifyUserName", "CreateUserName"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "九阳豆浆机", "ProductName"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "695865326954", "BarCode"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "件", "BigUnit"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "http://api.com/imgs/{0}.jpg".With(Guid.NewGuid().ToString("N")), "CategoryImage", "PageHomeImage", "ImageUrlOrg", "ImageUrl400x400", "ImageUrl200x200", "ImageUrl120x120", "ImageUrl60x60"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "湖南省", "ProvinceName"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "长沙市", "CityName"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "长沙县", "RegionName"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "华润凤凰城", "Address"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "201600098756", "OrderID"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "", "SortBy"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), "1", "UserType"));
            mapping.Add(new DefaultValueItemConfig(typeof(string), Guid.NewGuid().ToString("N").ToUpper(), "Token"));

            mapping.Add(new DefaultValueItemConfig(typeof(DateTime), Clock.Now));
            mapping.Add(new DefaultValueItemConfig(typeof(DateTime?), Clock.Now));

            mapping.Add(new DefaultValueItemConfig(typeof(int), new Random().Next(1000, 99999)));
            mapping.Add(new DefaultValueItemConfig(typeof(int), 50, "pageSize"));
            mapping.Add(new DefaultValueItemConfig(typeof(int), 1, "pageIndex", "IsDeleted", "Depth", "DisplaySequence", "IsMaster", "OrderNumber", "IsPrinted", "Status"));
            mapping.Add(new DefaultValueItemConfig(typeof(int), 100, "UserId", "ModifyUserID", "CreateUserID"));

            mapping.Add(new DefaultValueItemConfig(typeof(int?), new Random().Next(1000, 99999)));

            mapping.Add(new DefaultValueItemConfig(typeof(long), new Random().Next(1000, 99999)));
            mapping.Add(new DefaultValueItemConfig(typeof(long?), new Random().Next(1000, 99999)));

            mapping.Add(new DefaultValueItemConfig(typeof(decimal), 100.23m));
            mapping.Add(new DefaultValueItemConfig(typeof(decimal?), 100.23m));

            mapping.Add(new DefaultValueItemConfig(typeof(bool), true));
            mapping.Add(new DefaultValueItemConfig(typeof(bool?), false));

            //返回
            return mapping;
        }

        /// <summary>
        /// 获取映射的默认值
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <param name="type">基元类型</param>
        /// <returns></returns>
        protected object GetDefaultValue(string propertyName, Type type)
        {
            var defaultValueMapping = this.DefaultValueMapping();

            var value = defaultValueMapping.FirstOrDefault(o => o.PropertyType == type && !o.PropertyNames.IsNull() && o.PropertyNames.Contains(propertyName, StringComparer.OrdinalIgnoreCase));
            if (!value.IsNull())
            {
                return value.Value;
            }

            value = defaultValueMapping.FirstOrDefault(o => o.PropertyType == type);
            if (!value.IsNull())
            {
                return value.Value;
            }

            return default(Type);
        }

        /// <summary>
        /// 对指定对象属性进行赋值（测试值）
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="complexObjs">对象属性里的所有复杂对象集合</param>
        /// <returns></returns>
        private object ComplexObjectSetDefaultValue(object obj, Dictionary<Type, object> complexObjs)
        {
            //集合
            if (this.IsCollection(obj.GetType()))
            {
                //集合类参数
                var genericArgument = obj.GetType().IsArray ? obj.GetType().GetElementType() : obj.GetType().GetGenericArguments()[0];
                //复杂类型
                if (!IsPrimitive(genericArgument))
                {
                    this.ComplexObjectSetDefaultValue(((dynamic)obj)[0], complexObjs);
                }
            }
            else
            {
                //获取对象所有属性
                foreach (var property in obj.GetType().GetProperties())
                {
                    //基元类型
                    if (this.IsPrimitive(property.PropertyType))
                    {
                        if (property.CanWrite)
                        {
                            this.SetPropertyValue(obj, property, GetDefaultValue(property.Name, property.PropertyType));
                        }
                    }

                    //复杂类型
                    if (complexObjs.ContainsKey(property.PropertyType))
                    {
                        var value = complexObjs[property.PropertyType];
                        if (property.CanWrite)
                        {
                            property.SetValue(obj, value);
                        }
                        //继续赋上测试值
                        this.ComplexObjectSetDefaultValue(value, complexObjs);
                    }

                    //集合
                    if (this.IsCollection(property.PropertyType))
                    {
                        //集合类参数
                        var genericArgument = property.PropertyType.IsArray ? property.PropertyType.GetElementType() : property.PropertyType.GetGenericArguments()[0];
                        //如果是集合类型
                        dynamic listProperty = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericArgument));
                        //集合里的对象是否是复杂自定义对象
                        if (!this.IsPrimitive(genericArgument))
                        {
                            //属性值
                            dynamic propertyValue = Convert.ChangeType(complexObjs[genericArgument], genericArgument);
                            //随机出现1-5条数据
                            int n = new Random().Next(2, 3);
                            //列表的话就虚拟出3条记录
                            for (int i = 0; i < n; i++)
                            {
                                listProperty.Add(propertyValue);
                            }
                            //对列表数据赋值
                            if (property.CanWrite)
                            {
                                property.SetValue(obj, listProperty);
                            }

                            //赋值类型继续赋值
                            this.ComplexObjectSetDefaultValue(propertyValue, complexObjs);
                        }
                        //基元类型的数据赋值
                        else
                        {
                            //属性值
                            dynamic propertyValue = this.GetDefaultValue(property.Name, genericArgument);
                            //列表的话就虚拟出3条记录
                            for (int i = 0; i < 3; i++)
                            {
                                listProperty.Add(propertyValue);
                            }

                            //对列表数据赋值
                            if (property.CanWrite)
                            {
                                property.SetValue(obj, listProperty);
                            }
                        }
                    }
                }
            }

            //赋值完毕
            return obj;
        }

        /// <summary>
        /// 获取复杂类型实例（注意此类型不能含有任何构造函数，包括其属性里面的复杂属性）
        /// </summary>
        /// <param name="type">任意类型，一般为：RequestDtoType和ResponseDtoType类型</param>
        /// <returns></returns>
        public object CreateInstance(Type type)
        {
            //用于保存复杂类型
            List<Type> complexObjTypes = new List<Type>();

            //获取所有复杂类型
            this.GetComplexObjTypes(type, complexObjTypes);

            //循环创建所有对象
            Dictionary<Type, object> objs = new Dictionary<Type, object>();
            foreach (var item in complexObjTypes)
            {
                objs.Add(item, Activator.CreateInstance(item));
            }

            //基础数据类型无需创建对象
            if (this.IsPrimitive(type))
            {
                return this.GetDefaultValue("", type);
            }

            //集合
            if (this.IsCollection(type))
            {
                //集合类参数
                var genericArgument = type.IsArray ? type.GetElementType() : type.GetGenericArguments()[0];
                //如果是集合类型
                dynamic listProperty = Activator.CreateInstance(typeof(List<>).MakeGenericType(genericArgument));
                //集合里的对象是否是复杂自定义对象
                if (!this.IsPrimitive(genericArgument))
                {
                    //属性值
                    dynamic propertyValue = Convert.ChangeType(objs[genericArgument], genericArgument);
                    //随机出现1-5条数据
                    int n = new Random().Next(2, 3);
                    //列表的话就虚拟出3条记录
                    for (int i = 0; i < n; i++)
                    {
                        listProperty.Add(propertyValue);
                    }
                }
                //基元类型的数据赋值
                else
                {
                    //属性值
                    dynamic propertyValue = this.GetDefaultValue("", genericArgument);
                    //列表的话就虚拟出3条记录
                    for (int i = 0; i < 3; i++)
                    {
                        listProperty.Add(propertyValue);
                    }
                }
                //返回当前类型实例，已经赋值
                return this.ComplexObjectSetDefaultValue(listProperty, objs);
            }

            //返回当前类型实例，已经赋值
            return this.ComplexObjectSetDefaultValue(Activator.CreateInstance(type), objs);
        }
    }
}
