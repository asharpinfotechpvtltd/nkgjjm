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
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Units> Units { get; set; } = default!;
        public int TotalUnits { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                if (_context.TblUnits != null)
                {
                    TotalUnits = await _context.TblUnits.CountAsync();
                    Units = await _context.TblUnits.ToListAsync();
                }
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }
        public async Task<IActionResult> OnPost(string Unitname)
        {
            Units = await _context.TblUnits.Where(u => u.UnitName == Unitname).ToListAsync();
            TotalUnits = await _context.TblUnits.CountAsync();
            return Page();
        }
    }
}
