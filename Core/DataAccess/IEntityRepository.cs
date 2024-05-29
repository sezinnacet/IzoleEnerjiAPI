using Entities.Abstract;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetList(Expression<Func<T, bool>> filter = null);

        List<T> GetSortedList(Expression<Func<T, bool>> filter = null);

        bool Any(Expression<Func<T, bool>> filter);

        T Get(Expression<Func<T, bool>> filter);

        T Add(T entity);

        T Update(T entity);

        void DBDelete(T entity);

        void DBDeleteMany(List<T> entities);
        bool CheckIfExistsWithCondition(Expression<Func<T, bool>> filter);
        int GetCountWithCondition(Expression<Func<T, bool>> filter = null);
        List<T> AddMany(List<T> entities);
        List<T> GetListWithDeactivated(Expression<Func<T, bool>> filter = null);
        List<T> UpdateMany(List<T> entities);


    }
}
