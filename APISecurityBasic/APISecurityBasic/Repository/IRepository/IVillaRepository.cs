using APISecurityBasic.Models;
using System.Linq.Expressions;

namespace APISecurityBasic.Repository.IRepository
{
    public interface IVillaRepository
    {
       Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true);
         Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null);
         Task CreateAsync(Villa entity);
        Task RemoveAsync(Villa entity);
        Task UpdateAsync(Villa entity);
    }
}
