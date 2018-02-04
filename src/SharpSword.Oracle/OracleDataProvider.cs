/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2016 11:36:13 AM
 * ****************************************************************/
using Oracle.ManagedDataAccess.Client;
using SharpSword.Data;
using System;
using System.Data;

namespace SharpSword.Oracle
{
    public class OracleDataProvider : IDataProvider
    {
        public virtual void InitDatabase()
        {
        }

        public virtual bool StoredProceduredSupported
        {
            get { return false; }
        }

        public Type ConnectionType
        {
            get { return typeof(OracleConnection); }
        }

        public virtual IDataParameter CreateParameter()
        {
            return new OracleParameter();
        }
    }
}
