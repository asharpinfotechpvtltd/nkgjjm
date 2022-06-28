using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.Blocks
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DistBlock> DistBlock { get;set; } = default!;
        public int BlockCount { get; set; }

        public List<SPBlockByDistrict> BlockList { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.TblBlock != null)
            {
                BlockList = await _context.SPBlockByDistrict.FromSqlRaw("SPBlockByDistrict").ToListAsync();
                BlockCount = await _context.TblBlock.CountAsync();
            }
        }
        public async Task<IActionResult> OnPost(string BlockName)
        {
            DistBlock = await _context.TblBlock.Where(d => d.Block == BlockName).ToListAsync();
            BlockCount = DistBlock.Count;
            return Page();
        }
    }
}
