using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.Ho
{
    public class GalleryModel : PageModel
    {
        ApplicationDbContext _context;
        public GalleryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Closure> Closure { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            try
            {
                Closure = await _context.TblClosure.Where(e => e.Jobworkid == id).ToListAsync();
            }
            catch(Exception ex)
            {

            }
            return Page();
        }
    }
}
