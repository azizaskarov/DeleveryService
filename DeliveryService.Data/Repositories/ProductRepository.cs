using DeliveryService.Data.Contexts;
using DeliveryService.Data.IRepositories;
using DeliveryService.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Data.Repositories
{ 
    public class ProductRepository : IProductRepository
    {
        private AppDbContext appDbContext;


        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            var result = await appDbContext.Products.AddAsync(product);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression)
        {
            var Product = await appDbContext.Products.FirstOrDefaultAsync(expression);
            if (Product == null)
                return false;
            appDbContext.Products.Remove(Product);
            return true;
        }

        public async Task<IQueryable<Product>> GetAllAsync(Expression<Func<Product, bool>> exception = null)
        {
            return exception is null ? appDbContext.Products.Include(p => p.Compositions) : appDbContext.Products.Include(p => p.Compositions).Where(exception);
        }

        public Task<Product> GetAsync(Expression<Func<Product, bool>> expression)
        {
            return appDbContext.Products.Include(p => p.Compositions).FirstOrDefaultAsync(expression);
        }

        public async Task<Product> UpdateAsync(Guid Id, Product Product)
        {
            var result = appDbContext.Products.Update(Product);
            return result.Entity;
        }
    }
}
