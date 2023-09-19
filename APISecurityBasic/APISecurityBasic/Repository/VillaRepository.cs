using APISecurityBasic.Models;
using APISecurityBasic.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace APISecurityBasic.Repository
{
    public class VillaRepository: IVillaRepository
    {
        private readonly ApplicationDBContext _db;
        public VillaRepository( ApplicationDBContext db)
        {
            _db = db;   
        }
        public async Task CreateAsync(Villa entity)
        {
            await _db.Villas.AddAsync(entity);   
            await _db.SaveChangesAsync();   
        }

        public async Task UpdateAsync(Villa entity)
        {
             _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
        }
        public async Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null , bool tracked = true)
        {
            IQueryable<Villa> query = _db.Villas;
            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = query.Where(filter);    
            }
            return await query.FirstOrDefaultAsync();  
        }
        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
        {
            IQueryable<Villa> query = _db.Villas;
            
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }
        public async Task RemoveAsync(Villa entity)
        {
            _db.Villas.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
