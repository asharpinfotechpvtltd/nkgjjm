using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Villages
    {
        [Key]
        public int Id { get; set; }
        public int Block { get; set; }
        public int District { get; set; }
        public string GramPanchayat { get; set; }
        public string Village { get; set; }
        public string VillageCode { get; set; }


    }
}
