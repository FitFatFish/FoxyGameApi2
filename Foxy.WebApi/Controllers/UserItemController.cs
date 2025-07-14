using AutoMapper;
using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.DataLayer.Models.Users;
using Foxy.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foxy.WebApi.Controllers
{
    public class UserItemController : Controller
    {
        private readonly UserItemService _service;
        private readonly IMapper _mapper;

        public UserItemController(UserItemService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<UserItemResDto>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserItemReqDto reqentity)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == "userid")?.Value;
            var entity = _mapper.Map<UserItem>(reqentity);
            //entity.CreatedBy = Guid.Parse(userid);
            //entity.CreatedAt = DateTime.Now.ToUniversalTime();
            var created = await _service.CreateAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserItemReqDto reqentity)
        {
            //var entity = await _service.GetByIdAsync(id);
            //entity.Title = reqentity.Title;
            var entity = _mapper.Map<UserItem>(reqentity);
            if (id != entity.Id) return BadRequest();
            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
