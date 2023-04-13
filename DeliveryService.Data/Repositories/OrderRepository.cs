using DeliveryService.Data.Contexts;
using DeliveryService.Data.IRepositories;
using DeliveryService.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private AppDbContext appDbContext;


        public OrderRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<Order> CreateAsync(Order order)
        {
            var result = await appDbContext.Orders.AddAsync(order);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression)
        {
            var Order = await appDbContext.Orders.FirstOrDefaultAsync(expression);
            if (Order == null)
                return false;
            appDbContext.Orders.Remove(Order);
            return true;
        }

        public async Task<IQueryable<Order>> GetAllAsync(Expression<Func<Order, bool>> exception = null)
        {
            return exception is null ? appDbContext.Orders.Include(p => p.User) : appDbContext.Orders.Include(p => p.User).Where(exception);
        }

        public Task<Order> GetAsync(Expression<Func<Order, bool>> expression)
        {
            return appDbContext.Orders.Include(p => p.User).FirstOrDefaultAsync(expression);
        }

        public async Task<Order> UpdateAsync(Guid Id, Order Order)
        {
            var result = appDbContext.Orders.Update(Order);
            return result.Entity;
        }
    }
}    
