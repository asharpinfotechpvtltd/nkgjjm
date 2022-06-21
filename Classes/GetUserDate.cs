namespace Nkgjjm.Classes
{
    public class GetUserDate
    {
        public DateTime ReturnDate()
        {
            DateTime timeUtc = System.DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            return cstTime.Date;
            
        }
    }
}
