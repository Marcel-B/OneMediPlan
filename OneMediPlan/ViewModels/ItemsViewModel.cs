﻿using System;
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
            AddItemCommand = new Command(async () => await ExecuteAddItemCommand());
        }

        async Task ExecuteLoadMedisCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Medis.Clear();
                var medis = await DataStore.GetMedisAsync(true);
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
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command AddItemCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddItemCommand = new Command<Item>(async (Item item) => await AddItem(item));
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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

        async Task AddItem(Item item)
        {
            Items.Add(item);
            await DataStore.AddItemAsync(item);
        }
    }
}
