namespace Geta.Bring.Pickup.Model
{
    public struct Time
    {
        public readonly int Hour;
        public readonly int Minute;

        public Time(int totalminutes) : this(totalminutes / 60, totalminutes % 60) {}

        public Time(int hour, int minute)
        {
            if (hour > 23) hour %= 24;
            if (minute > 59) minute %= 60;

            Hour = hour;
            Minute = minute;
        }

        public override string ToString()
        {
            // Documentation of bring clearly states HH:mm.
            // The API does not accept that format.
            // Don't use ':'
            return $"{Hour:00}{Minute:00}";
        }
    }
}