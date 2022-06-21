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
        public async Task<IActionResult> OnGet()
        {
            District = await _context.TblDistrict.Select(a => new SelectListItem { Text = a.District, Value = a.id.ToString() }).ToListAsync();
            Contractor = await _context.TblContractor.Select(a => new SelectListItem { Text = a.ContractorName, Value = a.ContractorId.ToString() }).ToListAsync();
            JobWorkCategory = await _context.TblJobWorkCategory.Select(a => new SelectListItem { Text = a.JobWorkcategory, Value = a.Id.ToString() }).ToListAsync();
            return Page();
        }

        [BindProperty]
        public JobWork JobWork { get; set; } = default!;

        GetUserDate date = new GetUserDate();
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            

            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@District",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = JobWork.District
                        },
                        new SqlParameter() {
                            ParameterName = "@Block",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Scale = 100,
                            Value = JobWork.Block
                        },


                        new SqlParameter() {
                            ParameterName = "@GramPanchaayat",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = JobWork.GramPanchaayat
                        },
                        new SqlParameter() {
                            ParameterName = "@VillageCode",
                            SqlDbType =  System.Data.SqlDbType.BigInt,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = JobWork.VillageCode
                        },
                        new SqlParameter() {
                            ParameterName = "@ContractorId",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = JobWork.ContractorId
                        },
                        new SqlParameter() {
                            ParameterName = "@JobWorkDesc",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = JobWork.JobWorkDesc
                        },
                        new SqlParameter() {
                            ParameterName = "@JobWorkCategory",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = JobWork.JobWorkCategory
                        },
                         new SqlParameter() {
                            ParameterName = "@Date",
                            SqlDbType =  System.Data.SqlDbType.Date,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = date.ReturnDate()
                        },
                         new SqlParameter() {
                            ParameterName = "@Unit",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = JobWork.Unit
                        },
                          new SqlParameter() {
                            ParameterName = "@Rate",
                            SqlDbType =  System.Data.SqlDbType.Float,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Input,
                             Scale = 100,
                            Value = JobWork.Rate
                        }
            };


            _context.Database.ExecuteSqlRaw("SPJobWork @District,@Block ,@GramPanchaayat,@VillageCode,@ContractorId,@JobWorkDesc," +
                "@JobWorkCategory,@Date,@Unit,@Rate", param);


            return RedirectToPage("./Index");
        }
    }
}
