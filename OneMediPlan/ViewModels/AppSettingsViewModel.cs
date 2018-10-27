using System.Windows.Input;
using System;

using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Meta;

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

        public int Hour { get; set; }
        public int Minute { get; set; }


        public ICommand SaveSettingsCommand { get; }

        public void LoadSettings()
        {
            CurrentSettings = AppStore.Instance.AppSettings;
        }


        public AppSettingsViewModel()
        {
            Title = Strings.SETTINGS;
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
