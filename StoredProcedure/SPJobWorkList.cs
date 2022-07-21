namespace Nkgjjm.StoredProcedure
{
    public class SPJobWorkList
    {
        public string District { get; set; }
        public string Block { get; set; }
        public string GramPanchayat { get; set; }
        public string Village { get; set; }
        public Int64 VillageCode { get; set; }
        public string CompanyName { get; set; }
        public string ContractorName { get; set; }
        public string JobWorkcategory { get; set; }
        public string JobWorkDesc { get; set; }
        public DateTime Date { get; set; }
        public string WorkorderId { get; set; }
        public bool Iscompleted { get; set; }
    }
}
