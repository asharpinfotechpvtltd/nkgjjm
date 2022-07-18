using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class InwardDocuments
    {
        [Key]
        public int Id { get; set; }
        public string Pono { get; set; }
        public string Filename { get; set; }
        public string challanno { get; set; }
        public DateTime Date { get; set; }
    }
}
