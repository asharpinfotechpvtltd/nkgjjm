using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.MaterialIssuance
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MaterialIssuance> MaterialIssuance { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.MaterialIssuance != null)
            {
                MaterialIssuance = await _context.MaterialIssuance.ToListAsync();
            }
        }
    }
}
