using OneMediPlan.Models;
using System.Windows.Input;
using System;
using Ninject;

namespace OneMediPlan.ViewModels
{
    public class NewMediViewModel : BaseViewModel
    {
        public ICommand SaveNameCommand { get; }
        public Medi CurrentMedi { get; set; }
        public string Name { get; set; }

        public NewMediViewModel()
        {
            Title = "Neu";
            CurrentMedi = new Medi
            {
                Id = Guid.Empty,
                Create = DateTimeOffset.Now
            };

            SaveNameCommand = new Command(
                SaveMedi,
                (obj) => obj.ToString().Length > 0);
        }

        public async void SaveMedi(object param)
        {
            var store = App.Container.Get<IDataStore<Medi>>();
            CurrentMedi.Name = Name;
            await store.AddItemAsync(CurrentMedi);
        }
    }
}
