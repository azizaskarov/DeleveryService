using Abp.Domain.Uow;
using AutoMapper;
using Castle.Core.Configuration;
using DeliveryService.Domain.Commons;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Service.Services
{
    internal class CompositionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;
        public CompositionService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Composition>> CreateAsync(CompositionForCreationDto CompositionDto)
        {
            var response = new BaseResponse<Composition>();

            var mappedComposition = mapper.Map<Composition>(CompositionDto);

            mappedComposition.Create();

            var result = await unitOfWork.Categories.CreateAsync(mappedComposition);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;

        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Composition, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var existComposition = await unitOfWork.Categories.GetAsync(expression);

            if (existComposition is null)
            {
                response.Error = new ErrorResponse(404, "Composition not found");
                return response;
            }

            existComposition.Delete();

            await unitOfWork.Categories.UpdateAsync(existComposition);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;

        }

        public async Task<BaseResponse<IQueryable<Composition>>> GetAllAsync(PaginationParms @parms, Expression<Func<Composition, bool>> exception = null)
        {
            var response = new BaseResponse<IQueryable<Composition>>();

            var Compositions = await unitOfWork.Categories.GetAllAsync(exception);

            response.Data = Compositions;

            return response;
        }

        public async Task<BaseResponse<Composition>> GetAsync(Expression<Func<Composition, bool>> expression)
        {
            var response = new BaseResponse<Composition>();

            var Composition = await unitOfWork.Categories.GetAsync(expression);

            if (Composition is null)
            {
                response.Error = new ErrorResponse(404, "Composition not found");
                response response;
            }
            response.Data = Composition;

            return response;
        }


        public async Task<BaseResponse<Composition>> UpdateAsync(CompositionForCreationDto CompositionDto)
        {
            var existComposition = await repository.GetAsync(p => p.Id == Id && p.State != Domain.Enums.ItemState.Deleted);

            if (existComposition is null)
            {
                response.Error = new ErrorResponse(404, "Composition not found");

            }

            existComposition.NameEn = CompositionDto.NameEn;
            existComposition.NameRu = CompositionDto.NameRu;
            existComposition.NameUz = CompositionDto.NameUz;

            existComposition.Update();

            var Composition = await unitOfWork.Categories.UpdateAsync(existComposition);

            await unitOfWork.SaveChangesAsync();

            response.Data = Composition;

            return response;

        }
    }
}   
