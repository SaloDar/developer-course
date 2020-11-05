using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Shared.Entities;

namespace DeveloperCourse.SecondLesson.Shared.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetById(Guid id);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> Create(TEntity entity);

        Task<IEnumerable<TEntity>> CreateMany(IEnumerable<TEntity> entities);

        Task<bool> Update(TEntity entity);

        Task<bool> UpdateMany(IEnumerable<TEntity> entities);

        Task<bool> Delete(Guid id);

        Task<bool> DeleteMany(IEnumerable<Guid> id);

        Task<bool> Restore(Guid id);

        Task<bool> RestoreMany(IEnumerable<Guid> id);
    }
}