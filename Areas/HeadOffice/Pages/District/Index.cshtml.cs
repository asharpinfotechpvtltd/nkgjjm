using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.District
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Districts> Districts { get;set; } = default!;
        [BindProperty]
        public int TotalCount { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.TblDistrict != null)
            {
                Districts = await _context.TblDistrict.ToListAsync();
                TotalCount= Districts.Count;
            }
        }
        public async Task<IActionResult> OnPost(string districtname)
        {
            Districts = await _context.TblDistrict.Where(d=>d.District== districtname).ToListAsync();
            TotalCount = Districts.Count;
            return Page();
        }
    }
}
