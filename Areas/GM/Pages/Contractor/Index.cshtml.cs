using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.GM.Pages.Contractor
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Contractors> Contractors { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty("Login"))
            {
                if (_context.TblContractor != null)
                {
                    Contractors = await _context.TblContractor.ToListAsync();
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
