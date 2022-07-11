using api_movia.Migrations.Dtos;
using api_movia.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_movia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class generascontroller : ControllerBase
    {
        private readonly application_db_context _context;

        public generascontroller(application_db_context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult>GetAllAsync()
        {
            var generas = await _context.generas.ToListAsync() ;
            return Ok(generas);

        }
        [HttpPost]
        public async Task<IActionResult> createasync  (create_generas_dto dto)
        {
            var genera = new genera { Name = dto.Name };
            await _context.AddAsync(genera);
            _context.SaveChanges();
            return Ok(genera);
        }
        [HttpPut(template:"{id}" )]
        public async Task<IActionResult> Updateasync(int id, [FromBody] create_generas_dto dto)
        {
            var genera = await _context.generas.SingleOrDefaultAsync(g => g.Id == id);
            if(genera == null) { return BadRequest(); }

            genera.Name = dto.Name;
            _context.SaveChanges();
            return Ok(genera);

        }
        [HttpDelete(template:"{id}" )]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genera = await _context.generas.SingleOrDefaultAsync(g => g.Id == id);
            if (genera == null) { return BadRequest(); }

            _context.Remove(genera);
            _context.SaveChanges();
            return Ok(genera);





        }


    }
}
