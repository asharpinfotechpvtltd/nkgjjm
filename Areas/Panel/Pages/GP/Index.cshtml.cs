using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.GP
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPGramPanchyatByBlock> GramPanchayats { get;set; } = default!;
        public int GpCount { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.TblGramPanchayat != null)
            {
                GramPanchayats = await _context.SPGramPanchyatByBlock.FromSqlRaw("SPGramPanchyatByBlock").ToListAsync();
                GpCount = await _context.TblGramPanchayat.CountAsync();
            }
        }
    }
}
