using DeliveryService.Data.Contexts;
using DeliveryService.Data.IRepositories;
using DeliveryService.Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Data.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {

        private AppDbContext appDbContext;


        public CategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            var result = await appDbContext.Categories.AddAsync(category);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> DeleteAsync(Expression<Func<Category, bool>> expression)
        {
            var Category = await appDbContext.Categories.FirstOrDefaultAsync(expression);
            if (Category == null)
                return false;
            appDbContext.Categories.Remove(Category);
            return true;
        }

        public async Task<IQueryable<Category>> GetAllAsync(Expression<Func<Category, bool>> exception = null)
        {
            return exception is null ? appDbContext.Categories.Include(p => p.Products) : appDbContext.Categories.Include(p => p.Products).Where(exception);
        }

        public Task<Category> GetAsync(Expression<Func<Category, bool>> expression)
        {
            return appDbContext.Categories.Include(p => p.Products).FirstOrDefaultAsync(expression);
        }

        public async Task<Category> UpdateAsync(Guid Id, Category Category)
        {
            var result = appDbContext.Categories.Update(Category);
            return result.Entity;

        }

    }
}
