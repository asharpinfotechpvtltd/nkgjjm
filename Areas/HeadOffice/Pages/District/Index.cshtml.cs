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

        public IList<Districts> Districts { get; set; } = default!;
        [BindProperty]
        public int TotalCount { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {

                if (_context.TblDistrict != null)
                {
                    Districts = await _context.TblDistrict.ToListAsync();
                    TotalCount = Districts.Count;
                }
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }
        public async Task<IActionResult> OnPost(string districtname)
        {
            try
            {
                Districts = await _context.TblDistrict.Where(d => d.District == districtname).ToListAsync();
                TotalCount = Districts.Count;
            }
            catch (Exception)
            {

            }
            return Page();
        }
    }
}
