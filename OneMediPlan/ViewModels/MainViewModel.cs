using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using OneMediPlan.Models;
using Ninject;
using System.Linq;

namespace OneMediPlan.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Medi> Medis { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command AddItemCommand { get; set; }

        public MainViewModel()
        {
            Title = "One Mediplan";
            Medis = new ObservableCollection<Medi>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadMedisCommand());
            AddItemCommand = new Command<Medi>(async (Medi item) => await AddItem(item));
        }

        public async Task<Medi> RemoveMedi(int index)
            => await RemoveMedi(Medis[index]);

        public async Task<Medi> RemoveMedi(Medi medi)
        {
            Medis.Remove(medi);
            var store = App.Container.Get<MediDataStore>();
            await store.DeleteItemAsync(medi.Id);
            return medi;
        }

        async Task ExecuteLoadMedisCommand()
        {
            var dataStore = App.Container.Get<IMediDataStore>();
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Medis.Clear();
                var meds = await dataStore.GetItemsAsync(true);
                var medis = meds.ToList();
                medis.Sort();
                foreach (var medi in medis)
                {
                    if (medi.Id == Guid.Empty) continue;
                    Medis.Add(medi);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task AddItem(Medi medi)
        {
            Medis.Add(medi);
            await DataStore.AddItemAsync(medi);
        }
    }
}
