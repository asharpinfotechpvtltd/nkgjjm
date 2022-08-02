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

namespace Nkgjjm.Areas.Panel.Pages.ItemsMaster
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;
        public int TotalItem { get; set; }

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPItemList> ItemMaster { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                if (_context.TblItemMaster != null)
                {
                    var parameter = new SqlParameter("@Searchtext", DBNull.Value);
                    ItemMaster = await _context.SPItemList.FromSqlRaw("SPItemList @Searchtext", parameter).ToListAsync();
                    TotalItem = await _context.TblItemMaster.CountAsync();
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async Task<IActionResult> OnPost(string ItemName)
        {
            try
            {
                var parameter = new SqlParameter("@Searchtext", ItemName);
                ItemMaster = await _context.SPItemList.FromSqlRaw("SPItemList @Searchtext", parameter).ToListAsync();
                TotalItem = await _context.TblItemMaster.Where(i => i.ItemName == ItemName).CountAsync();
            }
            catch(Exception ex)
            {

            }
            return Page();
        }
    }
}
