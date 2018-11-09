using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using Ninject;

namespace com.b_velop.OneMediPlan.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon",
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_main;

        public ViewPager Pager { get; set; }
        public TabsAdapter Adapter { get; set; }
        private ILogger _logger;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _logger = App.Container.Get<ILogger>();
            Adapter = new TabsAdapter(this, SupportFragmentManager);
            Pager = FindViewById<ViewPager>(Resource.Id.viewpager);
            var tabs = FindViewById<TabLayout>(Resource.Id.tabs);
            Pager.Adapter = Adapter;
            tabs.SetupWithViewPager(Pager);
            Pager.OffscreenPageLimit = 3;
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(false);
        }

        protected override void OnStart()
        {
            base.OnStart();
            Toolbar.MenuItemClick += Toolbar_MenuItemClick;
            Pager.PageSelected += Pager_PageSelected;
        }

        protected override void OnStop()
        {
            base.OnStop();
            Toolbar.MenuItemClick -= Toolbar_MenuItemClick;
            Pager.PageSelected -= Pager_PageSelected;
        }

        void Pager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            var fragment = Adapter.InstantiateItem(Pager, e.Position) as IFragmentVisible;
            fragment?.BecameVisible();
        }

        void Toolbar_MenuItemClick(object sender, Android.Support.V7.Widget.Toolbar.MenuItemClickEventArgs e)
        {
            _logger.Log(GetType().Name);
            var intent = new Intent(this, typeof(AddNewMediActivity));
            _logger.Log($"Add Activity. {intent}");
            StartActivity(intent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }
}
