using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using OneMediPlan.Models;

namespace OneMediPlan
{
    public class MediViewModel : BaseViewModel{
        public ObservableCollection<Medi> Medis { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command AddItemCommand { get; set; }

        public MediViewModel()
        {
            Title = "One Mediplan";
            Medis = new ObservableCollection<Medi>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadMedisCommand());
            AddItemCommand = new Command<Medi>(async (Medi item) => await AddItem(item));
        }

        async Task ExecuteLoadMedisCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Medis.Clear();
                var medis = await DataStore.GetItemsAsync(true);
                foreach (var medi in medis)
                {
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
