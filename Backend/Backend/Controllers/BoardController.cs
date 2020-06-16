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
    public class BoardController : ControllerBase
    {
        private readonly KanBanContext _context;

        public BoardController(KanBanContext context)
        {
            _context = context;
        }

        // GET: api/Board
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetBoard()
        {
            return await _context.Board.ToListAsync();
        }
        /*
                // GET: api/Board/5
                [HttpGet("{id}")]
                public async Task<ActionResult<Board>> GetBoard(int id)
                {
                    var board = await _context.Board.FindAsync(id);

                    if (board == null)
                    {
                        return NotFound();
                    }

                    return board;
                }
                */

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Board>>> GetUserBoards(int id)
        {

            var userBoards = await (from boards in _context.UserBoards
                                    join user in _context.User on boards.UserId equals user.Id
                                    where user.Id == id
                                    select new UserBoards
                                    {
                                        UserId = boards.UserId,
                                        BoardId = boards.BoardId

                                    }).ToListAsync();
            List<Board> boardsToSend = new List<Board>();

            foreach(var item in userBoards)
            {
                var board = _context.Board.Find(item.BoardId);
                boardsToSend.Add(board);
            }

            

            //foreach (var userBoardsDTO in userBoards)
            //{
            //    foreach (var Board in _context.Board)
            //    {

            //        if (userBoardsDTO.UserId == id)
            //        {
            //            boardsToSend.Add(Board);

            //        }
            //    }

            //}

            Console.WriteLine("Number of boards: "  + boardsToSend.Count());
            //var userBoards = _context.Board.Where(i => i.Id == id);


            if (boardsToSend == null)
            {
                return NotFound();
            }

            return Ok(boardsToSend);
        }

        // PUT: api/Board/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoard(int id, Board board)
        {
            if (id != board.Id)
            {
                return BadRequest();
            }

            _context.Entry(board).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardExists(id))
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

        // POST: api/Board
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Board>> PostBoard(BoardDTO boardDTO)
        {
            var board = new Board()
            {
                Id = boardDTO.Id,
                Title = boardDTO.Title
            };
            _context.Board.Add(board);
            await _context.SaveChangesAsync();

            var userBoard = new UserBoards()
            {
                BoardId = board.Id,
                UserId = boardDTO.UserId
            };
            _context.UserBoards.Add(userBoard);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoard", new { id = board.Id }, board);
        }

        // DELETE: api/Board/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Board>> DeleteBoard(int id)
        {
            var board = await _context.Board.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            _context.Board.Remove(board);
            await _context.SaveChangesAsync();

            return board;
        }

        private bool BoardExists(int id)
        {
            return _context.Board.Any(e => e.Id == id);
        }
    }
}
