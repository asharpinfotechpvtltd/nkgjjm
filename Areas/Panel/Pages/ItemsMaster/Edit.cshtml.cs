using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.ItemsMaster
{
    public class EditModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public EditModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ItemMaster ItemMaster { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> UnitList { get; set; }

        public async Task<IActionResult> OnGetAsync(Int64 id)
        {
            

            var itemmaster =  await _context.TblItemMaster.FirstOrDefaultAsync(m => m.ItemCode == id);
            UnitList = await _context.TblUnits.Select(u => new SelectListItem { Text = u.UnitName, Value = u.UnitId.ToString() }).ToListAsync();
            if (itemmaster == null)
            {
                return NotFound();
            }
            ItemMaster = itemmaster;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ItemMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemMasterExists(ItemMaster.ItemCode))
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

        private bool ItemMasterExists(Int64 id)
        {
          return (_context.TblItemMaster?.Any(e => e.ItemCode == id)).GetValueOrDefault();
        }
    }
}
