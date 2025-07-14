using AutoMapper;
using Foxy.Core.Dtos.RequestDtos;
using Foxy.Core.Dtos.ResultDtos;
using Foxy.DataLayer.Models.Support;

using Foxy.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foxy.WebApi.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketService _service;
        private readonly IMapper _mapper;

        public TicketController(TicketService service, IMapper mapper)
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

            var result = _mapper.Map<TicketResDto>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketReqDto reqentity)
        {
            var userid = User.Claims.FirstOrDefault(c => c.Type == "userid")?.Value;
            var entity = _mapper.Map<Ticket>(reqentity);
            entity.CreatedBy = Guid.Parse(userid);
            entity.CreatedAt = DateTime.Now.ToUniversalTime();
            var created = await _service.CreateAsync(entity);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TicketReqDto reqentity)
        {
            //var entity = await _service.GetByIdAsync(id);
            //entity.Title = reqentity.Title;
            var entity = _mapper.Map<Ticket>(reqentity);
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
