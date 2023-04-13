using AutoMapper;
using DeliveryService.Domain.Entities.Categories;
using DeliveryService.Domain.Entities.Compositions;
using DeliveryService.Domain.Entities.Orders;
using DeliveryService.Domain.Entities.Products;
using DeliveryService.Domain.Entities.TimeCategories;
using DeliveryService.Domain.Entities.Users;
using DeliveryService.Service.DTOs.Categories;
using DeliveryService.Service.DTOs.Compositions;
using DeliveryService.Service.DTOs.Orders;
using DeliveryService.Service.DTOs.Products;
using DeliveryService.Service.DTOs.TimeCategories;
using DeliveryService.Service.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Service.Mappers
{
    public  class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForCreationDto>().ReverseMap();
            CreateMap<Order, OrderForCreationDto>().ReverseMap();
            CreateMap<Composition, CompositionForCreationDto>().ReverseMap();
            CreateMap<Product, ProductForCreationDto>().ReverseMap();
            CreateMap<TimeCategory, TimeCategoryForCreationDto>().ReverseMap();
            CreateMap<Category, CategoryForCreationDto>().ReverseMap();
            CreateMap<OrderDetails, OrderDetailsDto>().ReverseMap();

        }

    }
}
