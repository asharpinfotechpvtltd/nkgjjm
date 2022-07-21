namespace Nkgjjm.StoredProcedure
{
    public class SPMaterialReceivedCorrespondenceToPo
    {
        public long ItemCode { get; set; }
        public string ItemName { get; set; }
        public double Qty { get; set; }
        public double TotalReceived { get; set; }
        public int? Warehouse { get; set; }
    }
}
