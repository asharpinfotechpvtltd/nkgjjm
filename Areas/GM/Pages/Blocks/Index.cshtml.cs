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

namespace Nkgjjm.Areas.GM.Pages.Blocks
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        
        public int BlockCount { get; set; }

        public List<SPBlockByDistrict> BlockList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                try
                {
                    if (_context.TblBlock != null)
                    {
                        var block = new SqlParameter("@Block", DBNull.Value);
                        BlockList = await _context.SPBlockByDistrict.FromSqlRaw("SPBlockByDistrict @Block", block).ToListAsync();
                        BlockCount = await _context.TblBlock.CountAsync();
                        
                    }
                }
                catch (Exception)
                {

                }
                return Page();
            }
            else
            {
                return Redirect("~/Index");
            }
        }
        public async Task<IActionResult> OnPost(string BlockName)
        {
            try
            {
                var block = new SqlParameter("@Block", BlockName);
                BlockList = await _context.SPBlockByDistrict.FromSqlRaw("SPBlockByDistrict @Block", block).ToListAsync();
                BlockCount = BlockList.Count;
            }
            catch(Exception)
            {

            }
            return Page();
        }
    }
}
