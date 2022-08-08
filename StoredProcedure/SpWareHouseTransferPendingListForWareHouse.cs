namespace Nkgjjm.StoredProcedure
{
    public class SpWareHouseTransferPendingListForWareHouse
    {
        public int TransferMasterId { get; set; }
        public string ToWareHouse { get; set; }
        public string FromWareHouse { get; set; }
        public string FromWareHouseStatus { get; set; }
        public string HoStatus { get; set; }
        public int Warehouseid { get; set; }
        public string? Challan { get; set; }
    }
}
