namespace Nkgjjm.StoredProcedure
{
    public class SpItemBasedOnBom
    {
        public string JobWorkId { get; set; }
        public Int64 Rawmaterialid { get; set; }
        public string ItemName { get; set; }
        public double TotalQty { get; set; }
        public int TotalBom { get; set; }
        public double MaterialPickedqty { get; set; }
        public double TotalStock { get; set; }
    }
}
