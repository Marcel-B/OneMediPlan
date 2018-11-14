using System;
using System.Collections.ObjectModel;
using System.Linq;

using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Services;
using com.b_velop.OneMediPlan.Domain;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.Redux.States;

namespace com.b_velop.OneMediPlan.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Medi> Medis { get; set; }
        private readonly ILogger _logger;
        public Command LoadItemsCommand { get; set; }
        public Command AddItemCommand { get; set; }
        public ApplicationState ApplicationState;

        public MainViewModel(ILogger logger)
        {
            _logger = logger;
            Title = Strings.APP_TITLE;
            Medis = new ObservableCollection<Medi>();
            LoadItemsCommand = new Command(() => ExecuteLoadMedisCommand());
            AddItemCommand = new Command<Medi>((Medi item) => AddItem(item));
  
        }

        public Medi RemoveMedi(int index)
            => RemoveMedi(Medis[index]);

        public Medi RemoveMedi(Medi medi)
        {
            // Local remove
            Medis.Remove(medi);

            // Store remove
            var medis = AppStore.Instance.User.Medis;
            medis.Remove(medi);
            return medi;
        }

        private void ExecuteLoadMedisCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                var medis = AppStore.Instance.User.Medis.ToList();
                medis.Sort();

                Medis.Clear();
                foreach (var medi in medis)
                {
                    if (medi.Id == Guid.Empty) continue;
                    Medis.Add(medi);
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"Error occured while getting Medis", GetType(), ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void AddItem(Medi medi)
        {
            Medis.Add(medi);
            AppStore.Instance.User.Medis.Add(medi);
        }
    }
}
