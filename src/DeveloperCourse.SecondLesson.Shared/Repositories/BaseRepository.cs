using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DeveloperCourse.SecondLesson.Shared.Configs;
using DeveloperCourse.SecondLesson.Shared.Entities;
using DeveloperCourse.SecondLesson.Shared.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DeveloperCourse.SecondLesson.Shared.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields
        
        private readonly DbOptions _dbOptions;

        private readonly string _tableName;
        
        #endregion

        #region Construcstors

        public BaseRepository(IOptions<DbOptions> dbOptions) : this(dbOptions, typeof(TEntity).Name)
        {
        }

        public BaseRepository(IOptions<DbOptions> dbOptions, string tableName)
        {
            _dbOptions = dbOptions.Value;
            _tableName = tableName;
        }

        #endregion

        #region Implement Base Repository

        public virtual async Task<TEntity> GetById(Guid id)
        {
            using var db = CreateConnection();

            return await db.QueryFirstOrDefaultAsync<TEntity>($"SELECT * FROM [{_tableName}] WHERE [Id] = @id AND [IsDeleted] = 0", new {id});
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            var entityProps = GetEntityProps();

            var valueVars = string.Join(',', entityProps.Select(x => $"@{x}"));
            var valueNames = string.Join(',', entityProps.Select(x => $"[{x}]"));

            using var db = CreateConnection();

            return await db.QueryFirstOrDefaultAsync<TEntity>($"INSERT INTO [{_tableName}] ({valueNames}) OUTPUT INSERTED.* VALUES ({valueVars})", entity);
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            var entityProps = GetEntityProps();

            var values = string.Join(',', entityProps.Select(x => $"{x}=@{x}"));

            using var db = CreateConnection();

            await db.ExecuteAsync($"UPDATE [{_tableName}] SET {values} WHERE [id] = @id AND [IsDeleted] = 0", entity);

            return true;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            using var db = CreateConnection();

            return await db.ExecuteAsync($"UPDATE [{_tableName}] SET IsDeleted = 1 WHERE [id] = @id AND [IsDeleted] = 0", new {id}) != 0;
        }

        public virtual async Task<bool> Restore(Guid id)
        {
            using var db = CreateConnection();

            return await db.ExecuteAsync($"UPDATE [{_tableName}] SET IsDeleted = 0 WHERE [id] = @id AND [IsDeleted] = 1", new {id}) != 0;
        }
        
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            using var db = CreateConnection();

            return await db.QueryAsync<TEntity>($"SELECT * FROM [{_tableName}] WHERE [IsDeleted] = 0");
        }
        
        public virtual async Task<IEnumerable<TEntity>> CreateMany(IEnumerable<TEntity> entities)
        {
            var entityProps = GetEntityProps();

            var valueVars = string.Join(',', entityProps.Select(x => $"@{x}"));
            var valueNames = string.Join(',', entityProps.Select(x => $"[{x}]"));

            using var db = CreateConnection();

            await db.ExecuteAsync($"INSERT INTO [{_tableName}] ({valueNames}) VALUES ({valueVars})", entities);
            
            return await db.QueryAsync<TEntity>($"SELECT * FROM [{_tableName}] WHERE [Id] IN ('{string.Join("','", entities.Select(x=>x.Id))}') AND [IsDeleted] = 0");
        }

        public virtual async Task<bool> UpdateMany(IEnumerable<TEntity> entities)
        {
            var entityProps = GetEntityProps();

            var values = string.Join(',', entityProps.Select(x => $"{x}=@{x}"));

            using var db = CreateConnection();

            return await db.ExecuteAsync($"UPDATE [{_tableName}] SET {values} WHERE [id] = @id AND [IsDeleted] = 0", entities) != 0;
        }

        public virtual async Task<bool> DeleteMany(IEnumerable<Guid> id)
        {
            using var db = CreateConnection();

            return await db.ExecuteAsync($"UPDATE [{_tableName}] SET IsDeleted = '1' WHERE [Id] IN ('{string.Join("','", id)}')") != 0;
        }

        public virtual async Task<bool> RestoreMany(IEnumerable<Guid> id)
        {
            using var db = CreateConnection();

            return await db.ExecuteAsync($"UPDATE [{_tableName}] SET IsDeleted = '0' WHERE [Id] IN ('{string.Join("','", id)}')") != 0;
        }

        #endregion

        #region Protected Methods

        protected virtual List<string> GetEntityProps()
        {
            return typeof(TEntity).GetProperties()
                .Where(x => x.CustomAttributes.All(y =>
                    y.AttributeType != typeof(NonSerializedAttribute) &&
                    y.AttributeType != typeof(IgnoreDataMemberAttribute)))
                .Select(x =>
                {
                    var attributeData = (DataMemberAttribute) Attribute.GetCustomAttribute(x, typeof(DataMemberAttribute));

                    return attributeData?.Name ?? x.Name;
                }).ToList();
        }

        protected virtual IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_dbOptions.ConnectionString);

            connection.Open();

            return connection;
        }

        #endregion
    }
}