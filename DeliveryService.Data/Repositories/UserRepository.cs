using DeliveryService.Data.Contexts;
using DeliveryService.Data.IRepositories;
using DeliveryService.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext appDbContext;
        

        public UserRepository(AppDbContext appDbContext)
        {
            this .appDbContext = appDbContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            var result = await appDbContext.Users.AddAsync(user);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(expression);
            if(user == null)
                return false;
            appDbContext.Users.Remove(user);
            return true;
        }

        public async Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>> exception = null)
        {
            return exception is null ? appDbContext.Users.Include(p => p.Orders) : appDbContext.Users.Include(p => p.Orders).Where(exception);
        }

        public  Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            return  appDbContext.Users.Include(p => p.Orders).FirstOrDefaultAsync(expression);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var result = appDbContext.Users.Update(user);
            return result.Entity;

        }
    }
}
