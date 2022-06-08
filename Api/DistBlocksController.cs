using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistBlocksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DistBlocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DistBlocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistBlock>>> GetTblBlock()
        {
          if (_context.TblBlock == null)
          {
              return NotFound();
          }
            return await _context.TblBlock.ToListAsync();
        }

        // GET: api/DistBlocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DistBlock>> GetDistBlock(int id)
        {
          if (_context.TblBlock == null)
          {
              return NotFound();
          }
            var distBlock = await _context.TblBlock.FindAsync(id);

            if (distBlock == null)
            {
                return NotFound();
            }

            return distBlock;
        }

        // PUT: api/DistBlocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistBlock(int id, DistBlock distBlock)
        {
            if (id != distBlock.Id)
            {
                return BadRequest();
            }

            _context.Entry(distBlock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistBlockExists(id))
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

        // POST: api/DistBlocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DistBlock>> PostDistBlock(DistBlock distBlock)
        {
          if (_context.TblBlock == null)
          {
              return Problem("Entity set 'ApplicationDbContext.TblBlock'  is null.");
          }
            _context.TblBlock.Add(distBlock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDistBlock", new { id = distBlock.Id }, distBlock);
        }

        // DELETE: api/DistBlocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistBlock(int id)
        {
            if (_context.TblBlock == null)
            {
                return NotFound();
            }
            var distBlock = await _context.TblBlock.FindAsync(id);
            if (distBlock == null)
            {
                return NotFound();
            }

            _context.TblBlock.Remove(distBlock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DistBlockExists(int id)
        {
            return (_context.TblBlock?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
