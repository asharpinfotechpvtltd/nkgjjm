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
        public string ReturnDateTime()
        {
            DateTime timeUtc = System.DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            string date = cstTime.ToString("dd-MM-yyyy");
            string time = cstTime.ToString("HH:mm");
            return date + " " + time;

        }
    }
}
