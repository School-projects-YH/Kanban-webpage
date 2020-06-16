using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly KanBanContext _context;

        public ColumnController(KanBanContext context)
        {
            _context = context;
        }

        // GET: api/Column
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Column>>> GetColumn()
        {
            return await _context.Column.ToListAsync();
        }

        // GET: api/Column/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Column>> GetColumn(int id)
        {
            var column = await _context.Column.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            return Ok(column);
        }

        // GET: api/Column/Board/5
        [HttpGet("Board/{id}")]
        public async Task<ActionResult<List<CardDTO>>> GetColumnByBoardId(int id)
        {

            var dtoQuery = await (from column in _context.Column
                                  where column.BoardId == id
                                  select new Column
                                  {
                                      Id = column.Id,
                                      Title = column.Title

                                  }).ToListAsync();

            if (dtoQuery == null)
            {
                return NotFound();
            }
            return Ok(dtoQuery);


        }

        // PUT: api/Column/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColumn(int id, Column column)
        {
            if (id != column.Id)
            {
                return BadRequest();
            }

            _context.Entry(column).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColumnExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Column
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Column>> PostColumn(Column column)
        {
            _context.Column.Add(column);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColumn", new { id = column.Id }, column);
        }

        // DELETE: api/Column/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Column>> DeleteColumn(int id)
        {
            var column = await _context.Column.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }

            _context.Column.Remove(column);
            await _context.SaveChangesAsync();

            return column;
        }

        private bool ColumnExists(int id)
        {
            return _context.Column.Any(e => e.Id == id);
        }
    }
}
