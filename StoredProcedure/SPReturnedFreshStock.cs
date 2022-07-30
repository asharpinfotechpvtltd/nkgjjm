namespace Nkgjjm.StoredProcedure
{
    public class SpReturnedFreshStock
    {
        public long ItemCode { get; set; }
        public string ItemName { get; set; }
        public DateTime Date { get; set; }
        public double ReturnedFreshStock { get; set; }
    }
}
