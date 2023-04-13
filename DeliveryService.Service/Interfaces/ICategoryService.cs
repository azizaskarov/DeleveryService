using DeliveryService.Domain.Entities.Categories;
using DeliveryService.Service.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Service.Interfaces
{
    public  interface ICategoryService
    {
        Task<Category> CreateAsync(CategoryForCreationDto CategoryDto);
        Task<Category> UpdateAsync(Guid Id, CategoryForCreationDto CategoryDto);
        Task<bool> DeleteAsync(Expression<Func<Category, bool>> expression);
        Task<Category> GetAsync(Expression<Func<Category, bool>> expression);
        Task<IQueryable<Category>> GetAllAsync( Expression<Func<Category, bool>> exception = null);
    }
}
