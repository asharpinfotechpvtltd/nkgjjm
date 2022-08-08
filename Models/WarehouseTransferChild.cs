namespace Nkgjjm.Models
{
    public class WarehouseTransferChild
    {
        public int Id { get; set; }
        public long Itemcode { get; set; }
        public double TransferQty { get; set; }
        public int TransferMasterid { get; set; }
    }
}
