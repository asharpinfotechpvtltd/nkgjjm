using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nkgjjm.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoCheckController : ControllerBase
    {
        ApplicationDbContext _context;
        public PoCheckController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Get(string Po)
        {
            var District = await _context.TblPoMaster.SingleOrDefaultAsync(e => e.Pono == Po);
            if (District != null)
            {
                return "NA";
            }
            else
            {
                return "ok";
            }
        }

        // POST api/<PoCheckController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PoCheckController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PoCheckController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
