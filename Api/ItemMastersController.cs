using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemMastersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemMastersController(ApplicationDbContext context)
        {
            _context = context;
        }

    
        [HttpGet]
        public async Task<List<SPItemMaster>> GetItemMaster(Int64 Productid)
        {
            var itemcode = new SqlParameter("@ItemCode", Productid);
            var itemMaster =await  _context.SPItemMaster.FromSqlRaw("SPItemMaster @ItemCode",itemcode).ToListAsync();
            return itemMaster;
        }

        // PUT: api/ItemMasters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemMaster(int id, ItemMaster itemMaster)
        {
            if (id != itemMaster.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(itemMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemMasterExists(id))
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

        // POST: api/ItemMasters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemMaster>> PostItemMaster(ItemMaster itemMaster)
        {
          if (_context.TblItemMaster == null)
          {
              return Problem("Entity set 'ApplicationDbContext.TblItemMaster'  is null.");
          }
            _context.TblItemMaster.Add(itemMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemMaster", new { id = itemMaster.ItemId }, itemMaster);
        }

        // DELETE: api/ItemMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemMaster(int id)
        {
            if (_context.TblItemMaster == null)
            {
                return NotFound();
            }
            var itemMaster = await _context.TblItemMaster.FindAsync(id);
            if (itemMaster == null)
            {
                return NotFound();
            }

            _context.TblItemMaster.Remove(itemMaster);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemMasterExists(int id)
        {
            return (_context.TblItemMaster?.Any(e => e.ItemId == id)).GetValueOrDefault();
        }
    }
}
