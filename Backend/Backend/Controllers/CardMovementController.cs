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
    public class CardMovementController : ControllerBase
    {
        private readonly KanBanContext _context;

        public CardMovementController(KanBanContext context)
        {
            _context = context;
        }


        // PUT: api/cardmovement/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("left/{id}")]
        public async Task<IActionResult> MoveLeft(int id)
        {
            var card = _context.Card.Find(id);
            Console.WriteLine(card.Id);

            if (card.ColumnId != 1)
            {
                card.ColumnId = (card.ColumnId - 1);
                _context.Entry(card).State = EntityState.Modified;
            }
            else
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(card.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpPut("right/{id}")]

        public async Task<IActionResult> MoveRight(int id)

        {
            var card = _context.Card.Find(id);

            if (card.ColumnId != 4)
            {
                card.ColumnId = (card.ColumnId + 1);
                _context.Entry(card).State = EntityState.Modified;
            }
            else
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(card.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

          private bool CardExists(int id)
        {
            return _context.Card.Any(e => e.Id == id);
        }
    }
}
