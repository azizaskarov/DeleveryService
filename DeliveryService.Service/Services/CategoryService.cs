
using Abp.Domain.Uow;
using AutoMapper;
using DeliveryService.Domain.Commons;
using DeliveryService.Domain.Entities.Categories;
using DeliveryService.Service.DTOs.Categories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Service.Services
{
    internal class CategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Category>> CreateAsync(CategoryForCreationDto CategoryDto)
        {
            var response = new BaseResponse<Category>();    
            
            var mappedCategory = mapper.Map<Category>(CategoryDto);

            mappedCategory.Create();

            var result = await unitOfWork.Categories.CreateAsync(mappedCategory);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;

        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Category, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var existCategory = await unitOfWork.Categories.GetAsync(expression);

            if (existCategory is null)
            {
                response.Error = new ErrorResponse(404, "Category not found");
                return response;
            }

            existCategory.Delete();

            await unitOfWork.Categories.UpdateAsync(existCategory);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;

        }

        public async Task<BaseResponse<IQueryable<Category>>> GetAllAsync(PaginationParms @parms, Expression<Func<Category, bool>> exception = null)
        {
            var response = new BaseResponse<IQueryable<Category>>();

            var Categorys = await unitOfWork.Categories.GetAllAsync(exception);

            response.Data = Categorys;

            return response;
        }

        public async Task<BaseResponse<Category>> GetAsync(Expression<Func<Category, bool>> expression)
        {
            var response = new BaseResponse<Category>();

            var Category = await unitOfWork.Categories.GetAsync(expression);

            if(Category is null)
            {
                response.Error = new ErrorResponse(404, "Category not found");
                response response;
            }
            response.Data = Category;

            return response;
        }

        
        public async Task<BaseResponse<Category>> UpdateAsync(CategoryForCreationDto CategoryDto)
        {
            var existCategory = await repository.GetAsync(p => p.Id == Id && p.State != Domain.Enums.ItemState.Deleted);

            if (existCategory is null)
            {
                response.Error = new ErrorResponse(404, "Category not found");
              
            }

            existCategory.NameEn = CategoryDto.NameEn;
            existCategory.NameRu = CategoryDto.NameRu;
            existCategory.NameUz = CategoryDto.NameUz;

            existCategory.Update();

            var Category = await unitOfWork.Categories.UpdateAsync(existCategory);

            await unitOfWork.SaveChangesAsync();

            response.Data = Category;

            return response;

        }
    }
        
}
