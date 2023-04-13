using DeliveryService.Domain.Entities.Users;
using DeliveryService.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Service.Interfaces
{
    public  interface IUserService
    {
        Task<User> CreateAsync(UserForCreationDto userDto);
        Task<User> UpdateAsync(Guid Id, UserForCreationDto userDto);
        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        Task<User> GetAsync(Expression<Func<User, bool>> expression);
        Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>> exception = null);
    }
}
