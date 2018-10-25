using com.b_velop.OneMediPlan.Models;

namespace com.b_velop.OneMediPlan.ViewModels
{
    public class MediDetailViewModel : BaseViewModel
    {
        public AppMedi Medi { get; set; }
        public MediDetailViewModel(AppMedi item = null)
        {
            if (item != null)
            {
                Title = item.Name;
                Medi = item;
            }
        }
    }
}
