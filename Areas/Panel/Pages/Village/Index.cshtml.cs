using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.Village
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPVillageList> VillageList { get;set; } = default!;
        public int TotalVillageCount { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.TblVillageCode != null)
            {
                VillageList = await _context.SPVillageList.FromSqlRaw("SPVillageList").ToListAsync();
                TotalVillageCount = await _context.TblVillageCode.CountAsync();
            }
        }
    }
}
