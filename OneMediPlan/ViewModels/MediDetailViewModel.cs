using OneMediPlan.Models;

namespace com.b_velop.OneMediPlan.ViewModels
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
