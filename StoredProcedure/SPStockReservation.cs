namespace Nkgjjm.StoredProcedure
{
    public class SPStockReservation
    {
        public string WorkorderId { get; set; }
        public Int64 RawMaterialId { get; set; }
        public string ItemName { get; set; }
        public double OpeningStock { get; set; }
        public double BomQty { get; set; }
        public double MaterialPickedqty { get; set; }
        public double Freeze { get; set; }
        public double BalanceToIssue { get; set; }
        public double ClosingStock { get; set; }
        public int Warehouseid { get; set; }
        public string WarehouseName { get; set; }
    }
}
