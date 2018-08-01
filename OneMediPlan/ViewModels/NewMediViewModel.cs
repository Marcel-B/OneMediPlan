using OneMediPlan.Models;

namespace OneMediPlan.ViewModels
{
    public class NewMediViewModel : BaseViewModel
    {
        public NewMediViewModel()
        {
            Title = "Set Name";
        }
        public Medi CurrentMedi { get; set; }
    }
}
