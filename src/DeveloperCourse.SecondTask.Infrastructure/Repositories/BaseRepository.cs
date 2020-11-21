using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Dapper;
using DeveloperCourse.SecondLesson.Domain.Entities;
using DeveloperCourse.SecondLesson.Domain.Interfaces;
using DeveloperCourse.SecondTask.Infrastructure.Configs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DeveloperCourse.SecondTask.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields

        protected List<string> IgnoredFieldsWhenUpdate => new List<string>
        {
            "Id", "CreatedDate", "CreatedBy", "IsDeleted"
        };
        
        private readonly DbOptions _dbOptions;

        protected string TableName { get; }
        
        #endregion

        #region Construcstors

        public BaseRepository(IOptions<DbOptions> dbOptions) : this(dbOptions, typeof(TEntity).Name)
        {
        }

        public BaseRepository(IOptions<DbOptions> dbOptions, string tableName)
        {
            _dbOptions = dbOptions.Value;
            TableName = tableName;
        }

        #endregion

        #region Implement Base Repository

        public virtual async Task<TEntity> GetById(Guid id)
        {
            using var db = CreateConnection();

            return await db.QueryFirstOrDefaultAsync<TEntity>($"SELECT * FROM [{TableName}] WHERE [Id] = @id AND [IsDeleted] = 0", new {id});
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            var entityProps = GetEntityProps();

            var valueVars = string.Join(',', entityProps.Select(x => $"@{x}"));
            var valueNames = string.Join(',', entityProps.Select(x => $"[{x}]"));

            using var db = CreateConnection();

            return await db.QueryFirstOrDefaultAsync<TEntity>($"INSERT INTO [{TableName}] ({valueNames}) OUTPUT INSERTED.* VALUES ({valueVars})", entity);
        }

        public virtual async Task<bool> Update(TEntity entity)
        {
            entity.Changed();

            var entityProps = GetEntityProps()
                .Where(x => !IgnoredFieldsWhenUpdate.Any(y => y.Equals(x, StringComparison.OrdinalIgnoreCase)));

            var values = string.Join(',', entityProps.Select(x => $"{x}=@{x}"));

            using var db = CreateConnection();
            
            return await db.ExecuteAsync($"UPDATE [{TableName}] SET {values} WHERE [id] = @id AND [IsDeleted] = 0", entity) != 0;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            using var db = CreateConnection();

            return await db.ExecuteAsync($"UPDATE [{TableName}] SET IsDeleted = 1, LastSavedDate = @date WHERE [id] = @id AND [IsDeleted] = 0", new {id,date = DateTime.UtcNow}) != 0;
        }

        public virtual async Task<bool> Restore(Guid id)
        {
            using var db = CreateConnection();

            return await db.ExecuteAsync($"UPDATE [{TableName}] SET IsDeleted = 0, LastSavedDate = @date WHERE [id] = @id AND [IsDeleted] = 1", new {id,date = DateTime.UtcNow}) != 0;
        }
        
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            using var db = CreateConnection();

            return await db.QueryAsync<TEntity>($"SELECT * FROM [{TableName}] WHERE [IsDeleted] = 0");
        }
        
        public virtual async Task<IEnumerable<TEntity>> CreateMany(IEnumerable<TEntity> entities)
        {
            var entityProps = GetEntityProps();

            var valueVars = string.Join(',', entityProps.Select(x => $"@{x}"));
            var valueNames = string.Join(',', entityProps.Select(x => $"[{x}]"));

            using var db = CreateConnection();

            await db.ExecuteAsync($"INSERT INTO [{TableName}] ({valueNames}) VALUES ({valueVars})", entities);
            
            return await db.QueryAsync<TEntity>($"SELECT * FROM [{TableName}] WHERE [Id] IN ('{string.Join("','", entities.Select(x=>x.Id))}') AND [IsDeleted] = 0");
        }

        public virtual async Task<bool> UpdateMany(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.Changed();
            }
            
            var entityProps = GetEntityProps()
                .Where(x => !IgnoredFieldsWhenUpdate.Any(y => y.Equals(x, StringComparison.OrdinalIgnoreCase)));

            var values = string.Join(',', entityProps.Select(x => $"{x}=@{x}"));

            using var db = CreateConnection();

            return await db.ExecuteAsync($"UPDATE [{TableName}] SET {values} WHERE [id] = @id AND [IsDeleted] = 0", entities) != 0;
        }

        public virtual async Task<bool> DeleteMany(IEnumerable<Guid> id)
        {
            using var db = CreateConnection();

            return await db.ExecuteAsync($"UPDATE [{TableName}] SET IsDeleted = '1', LastSavedDate = @date WHERE [Id] IN ('{string.Join("','", id)}')", new {date = DateTime.UtcNow}) != 0;
        }

        public virtual async Task<bool> RestoreMany(IEnumerable<Guid> id)
        {
            using var db = CreateConnection();

            return await db.ExecuteAsync($"UPDATE [{TableName}] SET IsDeleted = '0', LastSavedDate = @date WHERE [Id] IN ('{string.Join("','", id)}')",new {date = DateTime.UtcNow}) != 0;
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