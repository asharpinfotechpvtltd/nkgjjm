namespace Nkgjjm.StoredProcedure
{
    public class SpWareHouseStockReport
    {
        public double OpeningStock { get; set; }
        public double inward { get; set; }
        public double Outward { get; set; }
        public double closingbalance { get; set; }
        public Int32 ItemCode { get; set; }
        public string ItemName { get; set; }
        public double Freeze { get; set; }
        public double ReturnedFreshStock { get; set; }
        public string UnitName { get; set; }
    }
}
