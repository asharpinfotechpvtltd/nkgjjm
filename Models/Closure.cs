using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Closure
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public string Jobworkid { get; set; }
        public DateTime Date { get; set; }
        public string Submittedby { get; set; }
    }
}
