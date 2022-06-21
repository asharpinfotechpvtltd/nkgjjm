﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Areas.Panel.Pages.JobWorkCategory
{
    public class DeleteModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public DeleteModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public JobWorkcategories JobWorkcategories { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblJobWorkCategory == null)
            {
                return NotFound();
            }

            var jobworkcategories = await _context.TblJobWorkCategory.FirstOrDefaultAsync(m => m.Id == id);

            if (jobworkcategories == null)
            {
                return NotFound();
            }
            else 
            {
                JobWorkcategories = jobworkcategories;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblJobWorkCategory == null)
            {
                return NotFound();
            }
            var jobworkcategories = await _context.TblJobWorkCategory.FindAsync(id);

            if (jobworkcategories != null)
            {
                JobWorkcategories = jobworkcategories;
                _context.TblJobWorkCategory.Remove(JobWorkcategories);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
