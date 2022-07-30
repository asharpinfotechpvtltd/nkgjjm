namespace Nkgjjm.StoredProcedure
{
    public class SPValidateByHo
    {
        public Int64 RawMaterialId { get; set; }

        public string ItemName { get; set; }

        public double Pickedqty { get; set; }
        public double BomQty { get; set; }
        public double Demand { get; set; }
        public double Instock { get; set; }
        public double BalanceQty { get; set; }

    }
}
