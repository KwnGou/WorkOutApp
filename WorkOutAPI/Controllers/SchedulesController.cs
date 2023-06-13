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
    public class SchedulesController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public SchedulesController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule_DetailsDTO>>> GetSchedules()
        {
            if (_context.Schedules == null)
            {
                return NotFound();
            }
            var result = await _context.Schedules
                .Include(s => s.ScheduleDailyExercis)
                .ThenInclude(e => e.Exercise)
                 .ToListAsync();

            var mapped = _mapper.Map<IEnumerable<Schedule_DetailsDTO>>(result);

            return Ok(mapped);
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule_DetailsDTO>> GetSchedules(int id)
        {
            var entity = await _context.Schedules
                .Where(s => s.Id == id)
                .Include(s => s.ScheduleDailyExercis)
                .ThenInclude(e => e.Exercise)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<Schedule_DetailsDTO>(entity);

            return Ok(mapped);
        }

        // PUT: api/Schedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutSchedules(int id, Schedule_DetailsDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var entity = await _context.Schedules.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            // Data validation
            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
            }

            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            _context.Entry(entity).State = EntityState.Modified;

            // delete existing children
            var existing = await _context.ScheduleDailyExercises.Where(s => s.ScheduleId == dto.Id).ToListAsync();
            _context.ScheduleDailyExercises.RemoveRange(existing);

            // add new children
            foreach (var item in dto.Exercises)
            {
                _context.ScheduleDailyExercises.Add(new ScheduleDailyExercis
                {
                    Date = item.Date,
                    ExerciseId = item.ExerciseId,
                    Schedule = entity
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchedulesExists(id))
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
                return BadRequest($"{ex.Message}:{ex?.InnerException?.Message}");
            }

            return NoContent();
        }

        // POST: api/Schedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Schedule_DetailsDTO>> PostSchedules(Schedule_DetailsDTO dto)
        {
            if (_context.Schedules == null)
            {
                return Problem("Entity set 'DBContext.Schedules'  is null.");
            }

            var entity = _mapper.Map<Schedule>(dto);

            // Data validation
            var (res, msg) = await ValidateData(dto);
            if (!res)
            {
                return BadRequest(msg);
            }

            _context.Schedules.Add(entity);

            // add new children
            foreach (var item in dto.Exercises)
            {
                _context.ScheduleDailyExercises.Add(new ScheduleDailyExercis
                {
                    Date = item.Date,
                    ExerciseId = item.ExerciseId,
                    Schedule = entity
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SchedulesExists(dto.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var mapped = _mapper.Map<Schedule_DetailsDTO>(entity);

            return CreatedAtAction("GetSchedules", new { id = mapped.Id }, mapped);

        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedules(int id)
        {
            if (_context.Schedules == null)
            {
                return NotFound();
            }
            var schedules = await _context.Schedules.FindAsync(id);
            if (schedules == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedules);
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

        private bool SchedulesExists(int id)
        {
            return (_context.Schedules?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<(bool result, string message)> ValidateData(Schedule_DetailsDTO dto)
        {
            foreach (var item in dto.Exercises)
            {

                // check if date is in date range
                if (item.Date < dto.StartDate || item.Date > dto.EndDate)
                {
                    return (false, "Excercise date out of schedule range");
                }
                // exercise id exists
                if (!await _context.Exercises.AnyAsync(e => e.Id == item.ExerciseId))
                {
                    return (false, "Specified excercise does not exist");
                }
            }

            return (true, string.Empty);
        }
    }
}
