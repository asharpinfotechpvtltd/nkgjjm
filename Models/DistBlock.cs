using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Nkgjjm.Models;

namespace Nkgjjm.Models
{
    public class DistBlock
    {
        [Key]
        public int Id { get; set; }
        public string Block { get; set; }
        public int District { get; set; }
    }
}
