using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.GP
{
    public class EditModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public EditModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GramPanchayats GramPanchayats { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> BlockList { get; set; }

        [BindProperty]
        public List<SelectListItem> DistrictList { get; set; }
        public int BlockId { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                try
                {
                    if (id == null || _context.TblGramPanchayat == null)
                    {
                        return NotFound();
                    }

                    DistrictList = await _context.TblDistrict.Select(d => new SelectListItem { Text = d.District, Value = d.id.ToString() }).ToListAsync();
                    var grampanchayats = await _context.TblGramPanchayat.FirstOrDefaultAsync(m => m.Id == id);
                    if (grampanchayats == null)
                    {
                        return NotFound();
                    }
                    GramPanchayats = grampanchayats;
                    BlockList = await _context.TblBlock.Select(b => new SelectListItem { Text = b.Block, Value = b.Id.ToString() }).ToListAsync();
                }
                catch (Exception)
                {

                }
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

            _context.Attach(GramPanchayats).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GramPanchayatsExists(GramPanchayats.Id))
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

        private bool GramPanchayatsExists(int id)
        {
            return (_context.TblGramPanchayat?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
