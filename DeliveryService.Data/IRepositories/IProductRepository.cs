﻿using DeliveryService.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Data.IRepositories
{
    public  interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Expression<Func<Product, bool>> expression);
        Task<Product> GetAsync(Expression<Func<Product, bool>> expression);
        Task<IQueryable<Product>> GetAllAsync(Expression<Func<Product, bool>> exception = null);
    }
}
