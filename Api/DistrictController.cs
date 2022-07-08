using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nkgjjm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        ApplicationDbContext _context;
        public DistrictController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        

        // GET api/<DistrictController>/5
        
        public async Task<string> Get(string DistrictName)
        {
            var District =await _context.TblDistrict.SingleOrDefaultAsync(e => e.District == DistrictName);
            if (District!=null)
            {
                return "NA";
            }
            else
            {
                return "ok";
            }
        }

        // POST api/<DistrictController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DistrictController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DistrictController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
