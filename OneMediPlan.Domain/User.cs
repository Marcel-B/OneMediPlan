using System;
using System.Collections.Generic;
namespace com.b_velop.OneMediPlan.Domain
{
    public class User : Item
    {
        public User()
        {
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Birthdate { get; set; }


        public IList<Medi> Medis { get; set; }

    }
}
