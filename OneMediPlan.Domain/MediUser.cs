using System.Collections.Generic;
using System;
namespace com.b_velop.OneMediPlan.Domain
{
    public class MediUser : Item
    {
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }

        public IList<Medi> Medis { get; set; }
    }
}
