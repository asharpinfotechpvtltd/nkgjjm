﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Areas.Panel.Pages.AssignItemToWarehouse
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;
        public int TotalCount { get; set; }
        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPItemInWareHouse> ItemToWarehouse { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.TblItemToWarehouse != null)
            {
                ItemToWarehouse = await _context.SPItemInWareHouse.FromSqlRaw("SPItemInWareHouse").ToListAsync();
                TotalCount = await _context.TblItemToWarehouse.CountAsync();

            }
            
        }
    }
}
