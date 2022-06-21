using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class MaterialIssuance
    {
        [Key]
        public int id { get; set; }
        public string JobWorkId { get; set; }

    }
}
