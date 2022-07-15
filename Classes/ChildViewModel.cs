using Microsoft.EntityFrameworkCore;
using Nkgjjm.StoredProcedure;

namespace Nkgjjm.Models
{
    public class ChildViewModel
    {
        ApplicationDbContext Context;

        public ChildViewModel(ApplicationDbContext context)
        {
            Context = context;
        }

        public List<SpUserMenu>? Menu { get; set; }
        public List<SpUserChildMenu>? ChildMenu { get; set; }

        


       

    }
}
