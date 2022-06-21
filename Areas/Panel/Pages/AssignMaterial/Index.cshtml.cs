using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.AssignMaterial
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SPMaterialIssuance> SPMaterialIssuance { get;set; } = default!;

        public void OnGetAsync()
        {
            
        }
        public async Task<IActionResult> OnPost(string jobworkid)
        {
            var jobwork = new SqlParameter("@JobWorkId", jobworkid);
            SPMaterialIssuance =await _context.SPMaterialIssuance.FromSqlRaw("SPMaterialIssuance @JobWorkId", jobwork).ToListAsync();

            return Page();
        }
    }
}
