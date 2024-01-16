namespace clinics_api.Models
{
    public static class SharedMethods
    {
        public static TimeSpan RoundToNearestQuarterHour(TimeSpan time)
        {
            int minutes = time.Minutes;
            int hours = time.Hours;

            // Calculate the remainder when dividing minutes by 15
            int remainder = minutes % 15;

            // Round up to the next quarter-hour if the remainder is greater than 0
            if (remainder > 0)
            {
                minutes = minutes + (15 - remainder);
                if (minutes == 60)
                {
                    minutes = 0;
                    hours++;
                }
            }

            TimeSpan roundedTime = new TimeSpan(hours, minutes, 0);

            return roundedTime;
        }
    }
}
