﻿using System;
namespace com.b_velop.OneMediPlan.Domain
{
    public class DailyAppointment : Item
    {
        public Medi Medi { get; set; }

        // TODO - Hack, Pls look for a better solution
        public Guid MediFk { get => Medi.Id; set => Medi.Id = value; }

        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
