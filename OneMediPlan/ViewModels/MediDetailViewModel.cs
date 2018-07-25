using OneMediPlan.Models;

namespace OneMediPlan
{
    public class MediDetailViewModel : BaseViewModel
    {
        public Medi Medi { get; set; }
        public MediDetailViewModel(Medi item = null)
        {
            if (item != null)
            {
                Title = item.Name;
                Medi = item;
            }
        }
    }
}
