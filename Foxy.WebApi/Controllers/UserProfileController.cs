using AutoMapper;
using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.DataLayer.Models.Games;
using Foxy.DataLayer.Models.Users;
using Foxy.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foxy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController(UserProfileService service, IMapper mapper) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await service.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var result = mapper.Map<UserProfileResDto>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserProfileReqDto reqentity)
        {
           
            var entity = mapper.Map<UserProfile>(reqentity);
          
            var created = await service.CreateAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
    }
}
