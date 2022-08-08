using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.VillageIncharge
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPVillageIncharge> VillageIncharge { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                try
                {
                    if (_context.TblVillageCode != null)
                    {
                        VillageIncharge = await _context.SPVillageIncharge.FromSqlRaw("SPVillageIncharge").ToListAsync();
                    }
                   
                }
                catch (Exception ex)
                {

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
