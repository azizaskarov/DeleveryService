using AutoMapper;
using DeliveryService.Data.IRepositories;
using DeliveryService.Domain.Entities.Users;
using DeliveryService.Domain.Enums;
using DeliveryService.Service.DTOs.Users;
using DeliveryService.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Service.Services
{
    public class UserService : IUserService, IFileService
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;
        public UserService(IUserRepository repository, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<User> CreateAsync(UserForCreationDto userDto)
        {
            var existUser = await repository.GetAsync(p => p.PhoneNumber == userDto.PhoneNumber);
            if (existUser is not null)
            {
                return null;
            }
            var mappedUser = mapper.Map<User>(existUser);

            mappedUser.Image = await SaveFileAsync(userDto.Image.OpenReadStream(), userDto.Image.FileName);

            mappedUser.Create();

            var user = await repository.CreateAsync(mappedUser);

            user.Image = config.GetSection("FileUrl:ImageUrl").Value + mappedUser.Image;

            return user;

        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var existUser = await repository.GetAsync(expression);

            if(existUser is not null)
                return false;

            existUser.Delete();

            await repository.UpdateAsync(existUser);

            return true;
        }

        public Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>> exception = null)
        {
            return repository.GetAllAsync(p => p.State != ItemState.Deleted);    
        }

        public Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            return repository.GetAsync(expression);
        }

        public async Task<string> SaveFileAsync(Stream file, string fileName)
        {
            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;
            string storagePath = config.GetSection("Storage:ImageUrl").Value;
            string filePath = Path.Combine(env.WebRootPath , $"{storagePath}/{fileName}");
            FileStream mainFile = File.Create(filePath);
            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return fileName;
        }

        public async Task<User> UpdateAsync(Guid Id, UserForCreationDto userDto)
        {
            var existUser = await repository.GetAsync(p => p.Id == Id && p.State != Domain.Enums.ItemState.Deleted);

            if (existUser is null)
                return null;

            existUser.FirstName = userDto.FirstName;
            existUser.LastName = userDto.LastName;
            existUser.PhoneNumber =userDto.PhoneNumber;

            existUser.Image = await SaveFileAsync(userDto.Image.OpenReadStream(), userDto.Image.FileName);

            existUser.Update();

            var user = await repository.UpdateAsync(existUser);
            user.Image = config.GetSection("FileUrl:ImageUrl").Value + user.Image;

            return user;
        }
    }
}
