﻿namespace com.b_velop.OneMediPlan.Domain
{
    public class DailyAppointment : Item
    {
        public Medi MediFk { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
