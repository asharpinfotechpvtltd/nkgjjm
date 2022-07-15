﻿using System;
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
    public class GramPanchayatsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GramPanchayatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<GramPanchayats> Grampanchayat { get; set; }

        [HttpGet]
        public async Task<string> GetGrampanchayat(int Districtid,int Blockid,string GramPanchayatName)
        {
            var Gp = await _context.TblGramPanchayat.SingleOrDefaultAsync(e => e.District == Districtid && e.Block==Blockid && e.GramPanchayat==GramPanchayatName);
            if (Gp != null)
            {
                return "NA";
            }
            else
            {
                return "ok";
            }
        }

       
        
        // PUT: api/GramPanchayats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGramPanchayats(int id, GramPanchayats gramPanchayats)
        {
            if (id != gramPanchayats.Id)
            {
                return BadRequest();
            }

            _context.Entry(gramPanchayats).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GramPanchayatsExists(id))
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

        // POST: api/GramPanchayats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GramPanchayats>> PostGramPanchayats(GramPanchayats gramPanchayats)
        {
          if (_context.TblGramPanchayat == null)
          {
              return Problem("Entity set 'ApplicationDbContext.TblGramPanchayat'  is null.");
          }
            _context.TblGramPanchayat.Add(gramPanchayats);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGramPanchayats", new { id = gramPanchayats.Id }, gramPanchayats);
        }

        // DELETE: api/GramPanchayats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGramPanchayats(int id)
        {
            if (_context.TblGramPanchayat == null)
            {
                return NotFound();
            }
            var gramPanchayats = await _context.TblGramPanchayat.FindAsync(id);
            if (gramPanchayats == null)
            {
                return NotFound();
            }

            _context.TblGramPanchayat.Remove(gramPanchayats);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GramPanchayatsExists(int id)
        {
            return (_context.TblGramPanchayat?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
