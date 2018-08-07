using System.Windows.Input;
using System;
using Ninject;
using OneMediPlan.Models;
using System.Threading.Tasks;
using System.Linq;

namespace OneMediPlan
{
    public class AppSettingsViewModel : BaseViewModel
    {
        public DateTimeOffset StandardReminderTime { get; set; }
        MediSettings _mediSettings;
        public MediSettings CurrentSettings
        {
            get => _mediSettings; 
            set => SetProperty(ref _mediSettings, value);
        }
        public ICommand SaveSettingsCommand { get; }

        public async Task LoadSettings()
        {
            var stored = App.Container.Get<IDataStore<MediSettings>>();
            var settingsList = await stored.GetItemsAsync();
            CurrentSettings = settingsList.First();
            return;
        }


        public AppSettingsViewModel()
        {
            Title = "Einstellungen";

            LoadSettings();

            SaveSettingsCommand = new Command(async (time) =>
            {
                if (time is DateTime currentTime)
                {
                    var store = App.Container.Get<IDataStore<MediSettings>>();
                    var item = new MediSettings();
                    item.Hour = currentTime.Hour;
                    item.Minute = currentTime.Minute;
                    await store.UpdateItemAsync(item);
                }
            });
        }
    }
}
