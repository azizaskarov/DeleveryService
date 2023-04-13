using DeliveryService.Domain.Entities.Users;
using DeliveryService.Service.DTOs.Users;
using DeliveryService.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DeliveryService.Api.Controllers
{
    [ApiController]
    [Route("api/users")]

    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<User>> Create(UserForCreationDto userDto)
        {
            var user = await  userService.CreateAsync(userDto);

            if(user == null)
            {
                return BadRequest();   
            }
            return Ok(user);

        }
        [HttpPost]

        public async Task<ActionResult<User>> GetAll()
        {
            var users = await userService.GetAllAsync(null);
            if(users == null)
            {
                return BadRequest();
            }
            return Ok(users);
        }

        [HttpDelete]
        public async Task<ActionResult<User>> Delete(Guid Id)
        {
            var result = await userService.DeleteAsync(p => p.Id == Id);

            if (result)
                return Ok();
            return BadRequest();
        }
        [HttpPut]
        public async Task<ActionResult<User>> Update(Guid Id, [FromForm] UserForCreationDto userDto)
        {
            var user = await userService.UpdateAsync(Id, userDto);

            if(user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

    }
}
