using System.Windows.Input;
using System.Threading.Tasks;

using com.b_velop.OneMediPlan.Models;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.Domain;
using System;

namespace com.b_velop.OneMediPlan.ViewModels
{
    public class AppSettingsViewModel : BaseViewModel
    {
        AppSettings _mediSettings;

        public AppSettings CurrentSettings
        {
            get => _mediSettings;
            set => SetProperty(ref _mediSettings, value);
        }

        public ICommand SaveSettingsCommand { get; }

        public void LoadSettings()
        {
            CurrentSettings = AppStore.Instance.AppSettings;
        }


        public AppSettingsViewModel()
        {
            Title = "Einstellungen";
            SaveSettingsCommand = new Command(async (time) =>
            {
                if (time is DateTime currentTime)
                {
                    CurrentSettings.Hour = currentTime.Hour;
                    CurrentSettings.Minute = currentTime.Minute;
                    CurrentSettings.LastEdit = DateTimeOffset.Now;

                    AppStore.Instance.AppSettings = CurrentSettings;
                    await AppSettingsDataStore.UpdateItemAsync(CurrentSettings);
                }
            });
        }
    }
}
