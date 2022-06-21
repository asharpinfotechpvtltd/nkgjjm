using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class JobWorkcategories
    {
        [Key]
        public int Id { get; set; }
        public string JobWorkcategory { get; set; }
    }
}
