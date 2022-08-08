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

namespace Nkgjjm.Areas.Panel.Pages.GP
{
    public class IndexModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public IndexModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SPGramPanchyatByBlock> GramPanchayats { get; set; } = default!;
        public int GpCount { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Login")))
            {
                try
                {
                    if (_context.TblGramPanchayat != null)
                    {
                        var gp = new SqlParameter("@Gp", DBNull.Value);
                        GramPanchayats = await _context.SPGramPanchyatByBlock.FromSqlRaw("SPGramPanchyatByBlock @Gp", gp).ToListAsync();
                        GpCount = await _context.TblGramPanchayat.CountAsync();
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
        public async Task OnPostAsync(string Gp)
        {
            try
            {
                if (_context.TblGramPanchayat != null)
                {
                    var gp = new SqlParameter("@Gp", Gp);
                    GramPanchayats = await _context.SPGramPanchyatByBlock.FromSqlRaw("SPGramPanchyatByBlock @Gp", gp).ToListAsync();
                    GpCount = GramPanchayats.Count;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
