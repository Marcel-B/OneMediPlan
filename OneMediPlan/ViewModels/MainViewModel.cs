using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Ninject;
using System.Linq;
using com.b_velop.OneMediPlan.Models;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.Services;

namespace com.b_velop.OneMediPlan.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<AppMedi> Medis { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command AddItemCommand { get; set; }

        public MainViewModel()
        {
            Title = "One Mediplan";
            Medis = new ObservableCollection<AppMedi>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadMedisCommand());
            AddItemCommand = new Command<AppMedi>(async (AppMedi item) => await AddItem(item));
        }

        public async Task<AppMedi> RemoveMedi(int index)
            => await RemoveMedi(Medis[index]);

        public async Task<AppMedi> RemoveMedi(AppMedi medi)
        {
            Medis.Remove(medi);
            //var store = App.Container.Get<MediDataStore>();
            //await store.DeleteItemAsync(medi.Id);
            return medi;
        }

        async Task ExecuteLoadMedisCommand()
        {
            //var dataStore = App.Container.Get<IMediDataStore>();
            //if (IsBusy)
                //return;

            IsBusy = true;

            try
            {
                Medis.Clear();
                //var meds = await dataStore.GetItemsAsync(true);
                //var medis = meds.ToList();
                //medis.Sort();
                //foreach (var medi in medis)
                //{
                //    if (medi.Id == Guid.Empty) continue;
                //    Medis.Add(medi);
                //}
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

        async Task AddItem(AppMedi medi)
        {
            //Medis.Add(medi);
            //await DataStore.AddItemAsync(medi);
        }
    }
}
