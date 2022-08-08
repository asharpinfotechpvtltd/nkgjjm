using System.ComponentModel.DataAnnotations;

namespace Nkgjjm.Models
{
    public class WarehouseTransfer
    {
        [Key]
        public int TransferMasterid { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseID { get; set; }
        public DateTime AddedDate { get; set; }
        public string FromWareHouseStatus { get; set; }
        public string ToWareHouseStatus { get; set; }
        public string HoStatus { get; set; }
        public string DriverName { get; set; }
        public string MobileNo { get; set; }
        public string VehicleNo { get; set; }
        public string BiltyNo { get; set; }
        public string Challan { get; set; }
    }
}
