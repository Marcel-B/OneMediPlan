using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

namespace com.b_velop.OneMediPlan.Droid
{
    public abstract class BaseActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(LayoutResource);
            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            if (Toolbar != null)
            {
                SetSupportActionBar(Toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            Init();
        }
        protected override void OnStop()
        {
            base.OnStop();
            DestroyEvents();
        }
        public void Init()
        {
            GetViews();
            InitViews();
            Localize();
            SetEvents();
        }

        public virtual void GetViews() { }
        public virtual void InitViews() { }
        public virtual void Localize() { }
        public virtual void SetEvents() { }
        public virtual void DestroyEvents() { }

        public Toolbar Toolbar
        {
            get;
            set;
        }

        protected virtual int LayoutResource
        {
            get;
        }

        protected int ActionBarIcon
        {
            set { Toolbar?.SetNavigationIcon(value); }
        }
    }
}
