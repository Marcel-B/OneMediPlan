using System.Collections.Generic;
namespace com.b_velop.OneMediPlan.Domain
{
    public class MediUser : Item
    {
        public MediUser(){}

        public string Name { get; set; }
        public string Email { get; set; }
        public string Birthdate { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }

        public IList<Medi> Medis { get; set; }
    }
}
