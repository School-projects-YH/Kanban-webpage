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
        [HttpPut("left")]
        public async Task<IActionResult> MoveLeft(Card card)
        {

            card.ColumnId = (card.ColumnId - 1);
            _context.Entry(card).State = EntityState.Modified;

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

            return NoContent();
        }

        [HttpPut("right")]
        public async Task<IActionResult> MoveRight(Card card)
        {

            card.ColumnId = (card.ColumnId + 1);
            _context.Entry(card).State = EntityState.Modified;

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

            return NoContent();
        }

          private bool CardExists(int id)
        {
            return _context.Card.Any(e => e.Id == id);
        }
    }
}
