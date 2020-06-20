using System;
using System.Collections.Generic;
using System.Text;
using MySqlSugar;

namespace Redoute.Actualsis.Repositonry.DbContext
{
    class DbContext
    {
        private static string _connectionString;

        private SqlSugarClient _db;

        /// <summary>
        /// 连接字符串 
        ///  
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// 数据连接对象 
        ///  
        /// </summary>
        public SqlSugarClient Db
        {
            get { return _db; }
            private set { _db = value; }
        }

        /// <summary>
        /// 数据库上下文实例（自动关闭连接）
        /// 
        /// </summary>
        public static DbContext Context
        {
            get
            {
                return new DbContext();
            }
        }

        /// <summary>
        /// 功能描述:构造函数
        /// </summary>
        private DbContext()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentNullException("数据库连接字符串为空");
            _db = new SqlSugarClient(connectionString: ConnectionString);
        }
    }
}
