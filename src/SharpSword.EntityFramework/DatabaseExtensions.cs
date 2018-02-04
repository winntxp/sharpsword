/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/14/2016 8:31:45 AM
 * ****************************************************************/
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// Database 动态查询扩展方法
    /// </summary>
    internal static class DatabaseExtensions
    {
        /// <summary>
        /// 用于缓存动态类型和SQL查询语句主键的关系
        /// </summary>
        private static readonly ConcurrentDictionary<string, Type> DynamicTypeCache =
            new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// 根据SQL查询语句，生成动态类型，方便比如我们代码生成器生成DTO类
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="isColsed"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static Type CreateDynamicType(this Database db, string sql, bool isColsed, params object[] parameters)
        {
            if (sql.IsNullOrEmpty())
            {
                throw new SharpSwordCoreException("sql is empty");
            }

            //缓存类型，缓存里存在直接从缓存里读取，这里我们将SQL语句转换成MD5摘要；
            string cacheKey = "DynamicSql_{0}".With(MD5.Encrypt(sql).ToUpper());
            if (DynamicTypeCache.ContainsKey(cacheKey))
            {
                return DynamicTypeCache[cacheKey];
            }

            //数据连接是否打开，不打开先打开
            if (db.Connection.State != ConnectionState.Open)
            {
                db.Connection.Open();
            }

            DbCommand dbCommand = db.Connection.CreateCommand();
            dbCommand.CommandText = sql;

            //设置参数
            PrepareCommandParameters(dbCommand, parameters);

            //读取一行数据，在没有数据情况下，直接返回null
            IDataReader dataReader = dbCommand.ExecuteReader();

            //校验是否为null，为null抛出异常
            dataReader.CheckNullThrowArgumentNullException(nameof(dataReader));

            //创建一个动态类型
            TypeBuilder builder = CreateTypeBuilder("DynamicModelAssembly",
                "DynamicModule",
                "DynamicTypes_{0}".With(Guid.NewGuid().ToString("N").ToUpper()));

            //可数据类型映射需要做特殊处理
            var types = new Type[]
            {
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(bool),
                typeof(DateTime)
            };

            //创建类型属性
            // ReSharper disable once PossibleNullReferenceException
            foreach (DataRow row in dataReader.GetSchemaTable().Rows)
            {
                //属性名称
                var propertyName = row["ColumnName"].ToString();
                //字段是否允许为null
                var allowDbNull = (bool)row["AllowDBNull"];
                //属性类型
                Type propertyType = (Type)row["DataType"];
                //空类型特殊处理
                if (allowDbNull && types.Any(o => o == propertyType))
                {
                    propertyType = typeof(Nullable<>).MakeGenericType(propertyType);
                }
                //动态创建属性
                CreateAutoImplementedProperty(builder, propertyName, propertyType);
            }

            dataReader.Close();
            dataReader.Dispose();
            //清空下参数
            dbCommand.Parameters.Clear();
            dbCommand.Dispose();

            //返回创建的动态类型
            Type returnType = builder.CreateType();

            //添加到缓存
            if (!DynamicTypeCache.ContainsKey(cacheKey))
            {
                DynamicTypeCache.TryAdd(cacheKey, returnType);
            }

            if (isColsed)
            {
                db.Connection.Close();
                db.Connection.Dispose();
            }

            return returnType;
        }

        /// <summary>
        /// 根据SQL查询语句，生成动态类型，方便比如我们代码生成器生成DTO类
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Type CreateDynamicType(this Database db, string sql, params object[] parameters)
        {
            return db.CreateDynamicType(sql, true, parameters);
        }

        /// <summary>
        /// 根据查询，获取动态的查询对象，注意：如果没有数据，返回空集合
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable DynamicSqlQuery(this Database db, string sql, params object[] parameters)
        {
            //返回动态类型
            var returnType = CreateDynamicType(db, sql, false, parameters);

            //查询
            if (parameters != null && parameters.Length > 0)
            {
                return db.SqlQuery(returnType, sql, parameters);
            }
            else
            {
                return db.SqlQuery(returnType, sql);
            }
        }

        /// <summary>
        /// 为查询准备参数
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <param name="parameters"></param>

        internal static void PrepareCommandParameters(this DbCommand dbCommand, params object[] parameters)
        {
            //构造参数，参数可以用2种方式
            if (parameters != null && parameters.Length > 0)
            {
                DbParameter[] array = new DbParameter[parameters.Length];
                if (parameters.All((object p) => p is DbParameter))
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        array[i] = (DbParameter)parameters[i];
                    }
                }
                else
                {
                    if (parameters.Any((object p) => p is DbParameter))
                    {
                        throw new Exception("参数集合不能包含任何DbParameter对象");
                    }
                    var array2 = new string[parameters.Length];
                    var array3 = new string[parameters.Length];
                    for (int j = 0; j < parameters.Length; j++)
                    {
                        array2[j] = string.Format(CultureInfo.InvariantCulture, "p{0}", j);

                        array[j] = dbCommand.CreateParameter();
                        array[j].ParameterName = array2[j];
                        array[j].Value = (parameters[j] ?? DBNull.Value);

                        array3[j] = "@" + array2[j];
                    }

                    dbCommand.CommandText = string.Format(CultureInfo.InvariantCulture, dbCommand.CommandText, array3);
                }

                dbCommand.Parameters.AddRange(array);
            }

        }

        /// <summary>
        /// 创建动态类型创建器
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="moduleName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private static TypeBuilder CreateTypeBuilder(string assemblyName, string moduleName, string typeName)
        {
            TypeBuilder typeBuilder = AppDomain.CurrentDomain
                                               .DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run)
                                               .DefineDynamicModule(moduleName)
                                               .DefineType(typeName, TypeAttributes.Public);
            //参加默认的构造函数
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);

            return typeBuilder;
        }

        /// <summary>
        /// 为动态类型创建属性
        /// </summary>
        /// <param name="typeBuilder"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyType"></param>
        private static void CreateAutoImplementedProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            const string privateFieldPrefix = "m_";
            const string getterPrefix = "get_";
            const string setterPrefix = "set_";

            // Generate the field.
            FieldBuilder fieldBuilder = typeBuilder.DefineField(
              string.Concat(privateFieldPrefix, propertyName),
              propertyType,
              FieldAttributes.Private);

            // Generate the property
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(
              propertyName,
              System.Reflection.PropertyAttributes.HasDefault,
              propertyType, null);

            // Property getter and setter attributes.
            MethodAttributes propertyMethodAttributes = MethodAttributes.Public
              | MethodAttributes.SpecialName
              | MethodAttributes.HideBySig;

            // Define the getter method.
            MethodBuilder getterMethod = typeBuilder.DefineMethod(
                string.Concat(getterPrefix, propertyName),
                propertyMethodAttributes,
                propertyType,
                Type.EmptyTypes);

            // Emit the IL code.
            // ldarg.0
            // ldfld,_field
            // ret
            ILGenerator getterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            // Define the setter method.
            MethodBuilder setterMethod = typeBuilder.DefineMethod(
              string.Concat(setterPrefix, propertyName),
              propertyMethodAttributes,
              null,
              new Type[] { propertyType });

            // Emit the IL code.
            // ldarg.0
            // ldarg.1
            // stfld,_field
            // ret
            ILGenerator setterILCode = setterMethod.GetILGenerator();
            setterILCode.Emit(OpCodes.Ldarg_0);
            setterILCode.Emit(OpCodes.Ldarg_1);
            setterILCode.Emit(OpCodes.Stfld, fieldBuilder);
            setterILCode.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getterMethod);
            propertyBuilder.SetSetMethod(setterMethod);
        }
    }
}
