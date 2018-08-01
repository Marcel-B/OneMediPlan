using OneMediPlan.Models;
using Ninject;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace OneMediPlan.Helpers
{
    public class SomeLogic
    {
        public SomeLogic() { }

        public void HandleIntoke(Medi medi, List<Medi> medis)
        {
            var last = medi.NextDate;
            medi.NextDate = medi.NextDate.AddMinutes(medi.IntervallInMinutes);

            medi.LastDate = last;
            medi.Stock--;

            SetNotification(medi);


            var dep = medis.SingleOrDefault(m => m.DependsOn == medi.Id);

            if (dep == null) return; // fertig

            dep.NextDate = last.AddMinutes(dep.IntervallInMinutes);
            SetNotification(dep);
        }

        private void SetNotification(Medi medi)
        {
        }
    }
}
