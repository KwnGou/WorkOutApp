using AutoMapper;
using WorkOutDTOs;
using WorkOutAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyModel = WorkOutAPI.Model;

namespace WorkOutAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        //private readonly DBContext _context;

        private readonly MyModel.DBContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category_GridDTO>>> GetCategories()
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var result = await _context.Categories
                   .ToListAsync();

            var mapped = _mapper.Map<IEnumerable<Category_GridDTO>>(result);

            return Ok(mapped);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category_GridDTO>> GetCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<Category_GridDTO>(category);

            return Ok(mapped);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCategory(int id, Category_GridDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            // Data validation

            if (!(await _context.Categories.AnyAsync(c => c.Id == dto.Id)))
            {
                return BadRequest("Specified category id does not exist");
            }

            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
            }

            var entity = _mapper.Map<Category>(dto);

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"{ex.Message}: {ex?.InnerException?.Message}");
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category_GridDTO>> PostCategory(Category_GridDTO dto)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'DBContext.Categories'  is null.");
            }

            var entity = _mapper.Map<Category>(dto);

            // Data Validation
            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
            }

            _context.Categories.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) 
            {
                return BadRequest($"{ex.Message}:{ex?.InnerException?.Message}");
            }

            var mapped = _mapper.Map<Category_GridDTO>(entity);

            return CreatedAtAction("GetCategory", new { id = mapped.Id }, mapped);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            if (await _context.Exercises.AnyAsync(e => e.CategoryId == id))
            {
                return BadRequest("Category is in use");
            }

            _context.Categories.Remove(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) 
            {
                return BadRequest($"{ex.Message}:{ex?.InnerException?.Message}");
            }

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<(bool result, string message)> ValidateData(Category_GridDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return (false, "Category name is required");
            }

            dto.Name.Trim();

            if (await _context.Categories.AnyAsync(c => c.Name == dto.Name))
            {
                return (false, "Specified category name already exist");
            }

            return (true, string.Empty);
        }
    }
}
