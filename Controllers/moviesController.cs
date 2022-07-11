using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_movia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class moviesController : ControllerBase
    {
        private readonly application_db_context _context;
        private new List<string> _allowedextantion = new List<string> { ".jpg", ".PNG " };
        private long _maxallowedpostersize = 11111048576;

        public moviesController(application_db_context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> getallasync()
        {
            var movies = await _context.movies.Include(m => m.genera).ToListAsync();
            return Ok(movies);

        }
        [HttpGet(template:"{id}")]

        public async Task<IActionResult> getbyid (int id )
        {
            var movie = await _context.movies.FindAsync(id);
               if (movie == null) 
                return NotFound();
            var dto = new moviesdetails_dto
            {
                id = movie.id,
                generaid = movie.generaid,
                generaname = movie.genera.Name,
                poster = movie.poster,
                rate = movie.rate,
                Storyline = movie.Storyline,
                title = movie.title,
                year = movie.year
            };

          
             return Ok(movie); 
        
        
        }

        [HttpGet("getbygeneraid") ]
        public async Task<IActionResult> getbygeneraid(byte ggeneraid)

        {
            var movie = await _context.movies.Where(m => m.generaid == ggeneraid).Include(m => m.genera)
                    .Select(m => new moviesdetails_dto
                    {
                        id = m.id ,
                        generaid=m.generaid,
                        generaname=m.genera.Name,
                        poster=m.poster,
                        rate=m.rate,
                        Storyline=m.Storyline,
                        title =m.title,
                        year=m.year


                    })
                    .ToListAsync();
            return Ok (movie);
        
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync( [FromForm]movies_dto dto )
        {
            if (!_allowedextantion.Contains(Path.GetExtension(dto.poster.FileName)))
                return BadRequest(error: "only jpg or png are allowed! ");

            if (dto.poster.Length > _maxallowedpostersize) return BadRequest();

            using var dataStream = new MemoryStream();
            await dto.poster.CopyToAsync(dataStream);
            var movie = new movie
            {
                generaid = dto.generaid,

                title = dto.title,

                poster = dataStream.ToArray(),

                rate = dto.rate,

                Storyline = dto.Storyline,

                year = dto.year,
            };
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return Ok(movie);


        }
        [HttpPut]
        public async Task <IActionResult> update(int id,[FromForm] movies_dto dto )
        {
            var movie = await _context.movies.FindAsync(id);
            if (movie == null)
                return NotFound();
            var isvaledgenera = await _context.generas.AnyAsync(g => g.Id == dto.generaid);
            if (!isvaledgenera) return BadRequest();

            if (dto.poster != null)
            {

                if (!_allowedextantion.Contains(Path.GetExtension(dto.poster.FileName)))
                    return BadRequest(error: "only jpg or png are allowed! ");

                if (dto.poster.Length > _maxallowedpostersize) return BadRequest();

                using var dataStream = new MemoryStream();
                await dto.poster.CopyToAsync(dataStream);
                movie.poster = dataStream.ToArray();
            }
            movie.title = dto.title;
            movie.generaid = dto.generaid;
            movie.Storyline = dto.Storyline;
            movie.rate = dto.rate;
            movie.year = dto.year;
            _context.SaveChanges();
            return Ok(movie);





        }



        [HttpDelete(template:"{id}")]
        public async Task<IActionResult> deletebygettingid(int id)
        {
            var movie = await _context.movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            _context.Remove(movie);
            _context.SaveChanges();
            return Ok(movie);


        }

    }
}
