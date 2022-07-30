using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Supplier
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Suppliers> Suppliers { get;set; } = default!;
        public int TotalCount { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.TblSupplier != null)
            {
                Suppliers = await _context.TblSupplier.ToListAsync();
                TotalCount = await _context.TblSupplier.CountAsync();
            }
        }
    }
}
