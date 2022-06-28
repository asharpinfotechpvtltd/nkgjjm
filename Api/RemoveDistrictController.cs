using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nkgjjm.Models;


namespace Nkgjjm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoveDistrictController : ControllerBase
    {
        ApplicationDbContext _context;
        public RemoveDistrictController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public string GetId(int Id)
        {
            var districts=_context.TblDistrict.Where(x => x.id == Id).FirstOrDefault();
            if(districts != null)
            {
                _context.TblDistrict.RemoveRange(districts); 
                _context.SaveChanges();
            }
            return "Deleted";
        }
    }
}
