using Foundation;
using OneMediPlan.Models;
using System;
using System.Collections.Generic;
using UIKit;
using OneMediPlan.iOS.Helper;
using OneMediPlan.ViewModels;
using Ninject;
using OneMediPlan.Helpers;

namespace OneMediPlan.iOS
{
    public partial class DailyViewController : UIViewController
    {
        DailyViewModel ViewModel { get; set; }

        partial void ButtonNext_TouchUpInside(UIButton sender)
        {
            if (TableViewDates.Source is MyDateTableViewSource source)
            {
                ViewModel.NextCommand.Execute(source.Times);
            }
        }

        partial void ButtonAdd_TouchUpInside(UIButton sender)
        {
            var time = PickerTime.Date;
            var dt = time.ToDateTime();
            var hour = dt.Hour;
            var minute = dt.Minute;
            var tpl = new Tuple<Hour, Minute>(new Hour(hour), new Minute(minute));
            if (TableViewDates.Source is MyDateTableViewSource s)
            {
                s.Times.Add(tpl);
                TableViewDates.ReloadData();
                ButtonNext.Hidden = !ViewModel.NextCommand.CanExecute(s.Times);
            }
        }

        public DailyViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<DailyViewModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel.Init();
            ButtonNext.Hidden = true;
            TableViewDates.Source = App.Container.Get<MyDateTableViewSource>();
            PickerTime.Date = NSDate.Now;
            TableViewDates.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            Title = NSBundle.MainBundle.GetLocalizedString(Strings.APPOINTMENTS);
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            if (TableViewDates.Source is MyDateTableViewSource s)
                return s.Times.Count > 0;
            return false;
        }
    }

    internal class MyDateTableViewSource : UITableViewSource
    {
        public IList<Tuple<Hour, Minute>> Times;
        public MyDateTableViewSource()
        {
            Times = new List<Tuple<Hour, Minute>>();
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("DateCell", indexPath) as UITableViewCell;
            var hour = Times[indexPath.Row].Item1.Value;
            var min = Times[indexPath.Row].Item2.Value;
            cell.TextLabel.Text = $"{hour}:{min}";
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
            => Times.Count;
    }
}