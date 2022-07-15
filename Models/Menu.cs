using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class Menu
    {
        [Key]
        public int M_ID { get; set; }
        public int? M_P_ID { get; set; }
        public string M_NAME { get; set; }
        public string? CONTROLLER_NAME { get; set; }
        public string? ACTION_NAME { get; set; }
    }
}
