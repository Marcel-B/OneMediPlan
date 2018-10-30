﻿using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;

using com.b_velop.OneMediPlan.ViewModels;
using OneMediPlan.Droid;
using Ninject;

namespace com.b_velop.OneMediPlan.Droid
{
    [Activity(Label = "AddNewMediActivity")]
    public class AddNewMediActivity : Activity
    {
        FloatingActionButton saveButton;
        EditText title, description;

        public NewMediViewModel ViewModel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = App.Container.Get<NewMediViewModel>();

            // Create your application here
            SetContentView(Resource.Layout.activity_add_item);
            saveButton = FindViewById<FloatingActionButton>(Resource.Id.save_button);
            title = FindViewById<EditText>(Resource.Id.txtTitle);
            description = FindViewById<EditText>(Resource.Id.txtDesc);

            saveButton.Click += SaveButton_Click;
        }

        void SaveButton_Click(object sender, EventArgs e)
        {
            //var item = new Medi
            //{
            //    Name = title.Text,
            //    Description = description.Text
            //};
            //ViewModel.AddItemCommand.Execute(item);

            Finish();
        }
    }
}