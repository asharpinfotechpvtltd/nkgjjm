namespace Nkgjjm.StoredProcedure
{
    public class SPAssignedJobWorkToWareHouse
    {
        public string WorkorderId { get; set; }
        public string Date { get; set; }
        public string District { get; set; }
        public string Block { get; set; }
        public string GramPanchayat { get; set; }
        public string Village { get; set; }
        public Int64 VillageCode { get; set; }
        public string ContractorName { get; set; }
        public Int64 ContractorContact { get; set; }
        public string JobWorkcategory { get; set; }
        public string WarehouseName { get; set; }
    }
}
