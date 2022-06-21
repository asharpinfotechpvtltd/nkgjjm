namespace Nkgjjm.StoredProcedure
{
    public class SPMaterialIssuance
    {
        public int RawMaterialId { get; set; }
        public double Finalqty { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string JobWorkId { get; set; }
        public double Pickedqty { get; set; }
    }
}
