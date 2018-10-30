using Android.Content;
using Android.Support.V4.App;

using com.b_velop.OneMediPlan.Meta;
using OneMediPlan.Droid.Fragments;

namespace com.b_velop.OneMediPlan.Droid
{
    public class TabsAdapter : FragmentStatePagerAdapter
    {
        string[] titles;

        public override int Count => titles.Length;

        public TabsAdapter(Context context, Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
            titles = new[]
            {
                Strings.MEDIS,
                Strings.SETTINGS,
            };
            //context.Resources.GetTextArray(Resource.Array.sections);
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position) =>
                            new Java.Lang.String(titles[position]);

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0:
                    return OutterMediBrowserFragment.NewInstance();
                case 1:
                    return SettingsFragment.NewInstance();
            }
            return null;
        }

        public override int GetItemPosition(Java.Lang.Object frag)
            => PositionNone;
    }
}
