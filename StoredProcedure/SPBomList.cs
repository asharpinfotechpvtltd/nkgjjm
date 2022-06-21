namespace Nkgjjm.StoredProcedure
{
    public class SPBomList
    {
        public string ItemName { get; set; }
        public int RawMaterialId { get; set; }
        
        public double Qty { get; set; }
        public double Tolerance { get; set; }
        public double Finalqty { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
