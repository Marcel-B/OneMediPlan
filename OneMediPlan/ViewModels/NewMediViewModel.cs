using System;
using OneMediPlan.Models;

namespace OneMediPlan.ViewModels
{
    public class NewMediViewModel : BaseViewModel
    {
        public NewMediViewModel()
        {
            Title = "Name";
        }

        public Medi CurrentMedi { get; set; }

    }
}
