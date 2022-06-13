using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Villages
    {
        [Key]
        public int Id { get; set; }
        public int Block { get; set; }
        public int District { get; set; }
        public int? GramPanchayat { get; set; }
        public string? Village { get; set; }
        public Int64? VillageCode { get; set; }


    }
}
