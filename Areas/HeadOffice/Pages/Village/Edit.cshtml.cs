using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Village
{
    public class EditModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public EditModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Villages Villages { get; set; } = default!;
        public List<SelectListItem> District { get; set; }
        public List<SelectListItem> Block { get; set; }
        public List<SelectListItem> Grampanchayat { get; set; }

        public async Task<IActionResult> OnGetAsync(Int64? id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                if (id == null || _context.TblVillageCode == null)
                {
                    return NotFound();
                }

                var villages = await _context.TblVillageCode.FirstOrDefaultAsync(m => m.VillageCode == id);
                District = await _context.TblDistrict.Select(d => new SelectListItem { Text = d.District, Value = d.id.ToString() }).ToListAsync();
                Block = await _context.TblBlock.Select(d => new SelectListItem { Text = d.Block, Value = d.Id.ToString() }).ToListAsync();
                Grampanchayat = await _context.TblGramPanchayat.Select(d => new SelectListItem { Text = d.GramPanchayat, Value = d.Id.ToString() }).ToListAsync();
                if (villages == null)
                {
                    return NotFound();
                }
                Villages = villages;
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Villages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VillagesExists(Villages.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VillagesExists(int id)
        {
            return (_context.TblVillageCode?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
