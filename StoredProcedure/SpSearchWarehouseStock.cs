namespace Nkgjjm.StoredProcedure
{
    public class SpSearchWarehouseStock
    {
        public string ItemName { get; set; }
        public Int64 ItemCode { get; set; }
        public double Inward { get; set; }
        public double Outward { get; set; }
        public double Balance { get; set; }
        public DateTime DATE { get; set; }
        public string WarehouseName { get; set; }
    }
}
