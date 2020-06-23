using Microsoft.EntityFrameworkCore.ChangeTracking;
using Redoute.Actualsis.Basic.Common;
using Redoute.Actualsis.IRepositonry;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Redoute.Actualsis.Repositonry
{
    public class BasicRepository<TEntity> : IBasicRepository<TEntity> where TEntity : class, new()
    {

        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<TEntity> entityDB;

        public DbContext Context
        {
            get { return context; }
            set { context = value; }
        }
        internal SqlSugarClient Db
        {
            get { return db; }
            private set { db = value; }
        }
        internal SimpleClient<TEntity> EntityDB
        {
            get { return entityDB; }
            private set { entityDB = value; }
        }

        public BasicRepository()
        {
            DbContext.Init(ConfigHelper.connectionStrings.MySqlConnString, DbType.MySql);
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<TEntity>(db);
        }


        public async Task<bool> AddAsync(TEntity entity)
        {
            var i = await Task.Run(() => db.Insertable(entity).ExecuteReturnBigIdentity());
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return (int)i > 0;
        }

        public Task AddRangeAsync(IEnumerable<TEntity> list)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> ListAsync()
        {
            return Task.Run(() => entityDB.GetList());
        }

        public Task<IQueryable<TEntity>> QueryAsync()
        {
            return null;
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity, params Expression<Func<TEntity, object>>[] expressions)
        {
            throw new NotImplementedException();
        }

        IQueryable<TEntity> IBasicRepository<TEntity>.QueryAsync()
        {
            throw new NotImplementedException();
        }
    }
}
