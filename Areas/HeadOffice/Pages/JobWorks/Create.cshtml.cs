using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Classes;
using Nkgjjm.Models;
using System.Data;
using Newtonsoft.Json;

namespace Nkgjjm.Areas.Panel.Pages.JobWorks
{
    public class CreateModel : PageModel
    {
        private readonly Nkgjjm.Models.ApplicationDbContext _context;

        public CreateModel(Nkgjjm.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> District { get; set; }
        public List<SelectListItem> Contractor { get; set; }
        public List<SelectListItem> JobWorkCategory { get; set; }
        public List<SelectListItem> Warehouse { get; set; }
       
        public async Task<IActionResult> OnGet()
        {
            District = await _context.TblDistrict.Select(a => new SelectListItem { Text = a.District, Value = a.id.ToString() }).ToListAsync();
            Contractor = await _context.TblContractor.Select(a => new SelectListItem { Text = a.ContractorName, Value = a.ContractorId.ToString() }).ToListAsync();
            JobWorkCategory = await _context.TblJobWorkCategory.Select(a => new SelectListItem { Text = a.JobWorkcategory, Value = a.Id.ToString() }).ToListAsync();
            Warehouse = await _context.TblWarehouse.Select(a => new SelectListItem { Text = a.WarehouseName, Value = a.Id.ToString() }).ToListAsync();
            return Page();
        }

        [BindProperty]
        public JobWork JobWork { get; set; } = default!;

        GetUserDate date = new GetUserDate();
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            JobWork.Date = date.ReturnDateTime();
            await _context.TblJobWork.AddAsync(JobWork);
            string jobworkJSON = Request.Form["jobworkdesc"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(jobworkJSON);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][2].ToString()))
                {
                    string Particular = dt.Rows[i][0].ToString();
                    string Unit = Convert.ToString(dt.Rows[i][1]);
                    double Rate =Convert.ToDouble(dt.Rows[i][2].ToString());
                    JobDescription desc = new JobDescription();
                    desc.Unit = Unit;
                    desc.Particular = Particular;
                    desc.Rate = Rate;
                    desc.JobWorkid = JobWork.WorkorderId;
                    await _context.TblJobDescription.AddAsync(desc);
                    await _context.SaveChangesAsync();

                }
            }
            return RedirectToPage("./Index");
        }
    }
}
