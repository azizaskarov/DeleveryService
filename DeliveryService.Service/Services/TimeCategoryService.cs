using Abp.Domain.Uow;
using AutoMapper;
using Castle.Core.Configuration;
using DeliveryService.Domain.Commons;
using DeliveryService.Domain.Entities.TimeCategories;
using DeliveryService.Service.DTOs.TimeCategories;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Service.Services
{
    public  class TimeCategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;
        public TimeCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<TimeCategory>> CreateAsync(TimeCategoryForCreationDto TimeCategoryDto)
        {
            var response = new BaseResponse<TimeCategory>();

            var mappedTimeCategory = mapper.Map<TimeCategory>(TimeCategoryDto);

            mappedTimeCategory.Create();

            var result = await unitOfWork.Categories.CreateAsync(mappedTimeCategory);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;

        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<TimeCategory, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var existTimeCategory = await unitOfWork.Categories.GetAsync(expression);

            if (existTimeCategory is null)
            {
                response.Error = new ErrorResponse(404, "TimeCategory not found");
                return response;
            }

            existTimeCategory.Delete();

            await unitOfWork.Categories.UpdateAsync(existTimeCategory);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;

        }

        public async Task<BaseResponse<IQueryable<TimeCategory>>> GetAllAsync(PaginationParms @parms, Expression<Func<TimeCategory, bool>> exception = null)
        {
            var response = new BaseResponse<IQueryable<TimeCategory>>();

            var TimeCategorys = await unitOfWork.Categories.GetAllAsync(exception);

            response.Data = TimeCategorys;

            return response;
        }

        public async Task<BaseResponse<TimeCategory>> GetAsync(Expression<Func<TimeCategory, bool>> expression)
        {
            var response = new BaseResponse<TimeCategory>();

            var TimeCategory = await unitOfWork.Categories.GetAsync(expression);

            if (TimeCategory is null)
            {
                response.Error = new ErrorResponse(404, "TimeCategory not found");
                response response;
            }
            response.Data = TimeCategory;

            return response;
        }


        public async Task<BaseResponse<TimeCategory>> UpdateAsync(TimeCategoryForCreationDto TimeCategoryDto)
        {
            var existTimeCategory = await repository.GetAsync(p => p.Id == Id && p.State != Domain.Enums.ItemState.Deleted);

            if (existTimeCategory is null)
            {
                response.Error = new ErrorResponse(404, "TimeCategory not found");

            }

            existTimeCategory.NameEn = TimeCategoryDto.NameEn;
            existTimeCategory.NameRu = TimeCategoryDto.NameRu;
            existTimeCategory.NameUz = TimeCategoryDto.NameUz;

            existTimeCategory.Update();

            var TimeCategory = await unitOfWork.Categories.UpdateAsync(existTimeCategory);

            await unitOfWork.SaveChangesAsync();

            response.Data = TimeCategory;

            return response;

        }
    }
}
