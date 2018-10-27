using Android.App;
using Android.Content;
using Android.OS;

using com.b_velop.OneMediPlan.ViewModels;
using OneMediPlan.Droid;

namespace com.b_velop.OneMediPlan.Droid
{
    [Activity(Label = "Details", ParentActivity = typeof(MainActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
    public class MediDetailActivity : BaseActivity
    {
        /// <summary>
        /// Specify the layout to inflace
        /// </summary>
        protected override int LayoutResource => Resource.Layout.activity_item_details;

        public MediDetailViewModel ViewModel { get; set; 
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var data = Intent.GetStringExtra("data");

            //var item = Newtonsoft.Json.JsonConvert.DeserializeObject<Medi>(data);
            //ViewModel = new MediDetailViewModel(item);

            //FindViewById<TextView>(Resource.Id.description).Text = item.Name;

            //SupportActionBar.Title = item.Name;
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}
