using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Unit
{
    public class DetailsModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DetailsModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public Units Units { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                if (id == null || _context.TblUnits == null)
                {
                    return NotFound();
                }

                var units = await _context.TblUnits.FirstOrDefaultAsync(m => m.UnitId == id);
                if (units == null)
                {
                    return NotFound();
                }
                else
                {
                    Units = units;
                }
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }
    }
}
