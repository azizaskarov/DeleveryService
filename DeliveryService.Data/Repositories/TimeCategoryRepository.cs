using DeliveryService.Data.Contexts;
using DeliveryService.Data.IRepositories;
using DeliveryService.Domain.Entities.TimeCategories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Data.Repositories
{
    public  class TimeCategoryRepository : ITimeCategoryRepository
    {
        private AppDbContext appDbContext;


        public TimeCategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<TimeCategory> CreateAsync(TimeCategory TimeCategory)
        {
            var result = await appDbContext.TimeCategories.AddAsync(TimeCategory);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> DeleteAsync(Expression<Func<TimeCategory, bool>> expression)
        {
            var TimeCategory = await appDbContext.TimeCategories.FirstOrDefaultAsync(expression);
            if (TimeCategory == null)
                return false;
            appDbContext.TimeCategories.Remove(TimeCategory);
            return true;
        }

        public async Task<IQueryable<TimeCategory>> GetAllAsync(Expression<Func<TimeCategory, bool>> exception = null)
        {
            return exception is null ? appDbContext.TimeCategories.Include(p => p.Products) : appDbContext.TimeCategories.Include(p => p.Products).Where(exception);
        }

        public Task<TimeCategory> GetAsync(Expression<Func<TimeCategory, bool>> expression)
        {
            return appDbContext.TimeCategories.Include(p => p.Products).FirstOrDefaultAsync(expression);
        }

        public async Task<TimeCategory> UpdateAsync(Guid Id, TimeCategory TimeCategory)
        {
            var result = appDbContext.TimeCategories.Update(TimeCategory);
            return result.Entity;
        }
    }
}
