namespace Nkgjjm.StoredProcedure
{
    public class SPItemList
    {
        
            public int ItemId { get; set; }
            public string ItemName { get; set; }
            public string Unitname { get; set; }
            public string? HsnCode { get; set; }
            public string? Description { get; set; }
            public DateTime AddedDate { get; set; }
            public double Gst { get; set; }
            public double? CGst { get; set; }
            public double? SGst { get; set; }
            public double? IGst { get; set; }
            public double? OpeningStock { get; set; }
            public Int64 ItemCode { get; set; }
        
    }
}
