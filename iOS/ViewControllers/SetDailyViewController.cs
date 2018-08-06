using Foundation;
using OneMediPlan.Models;
using System;
using System.Collections.Generic;
using UIKit;
using OneMediPlan.iOS.Helper;
using System.Collections.ObjectModel;
using System.Linq;

namespace OneMediPlan.iOS
{
    public partial class SetDailyViewController : UIViewController
    {
        partial void UIButton44379_TouchUpInside(UIButton sender)
        {
            var time = PickerTime.Date;
            var dt = time.NSDateToDateTime();
            var hour = dt.Hour;
            var minute = dt.Minute;
            var tpl = new Tuple<Hour, Minute>(new Hour(hour), new Minute(minute));
            if (TableViewDates.Source is MyDateTableViewSource s)
            {
                s.Times.Add(tpl);
                TableViewDates.ReloadData();
            }
        }

        public Medi CurrentMedi { get; set; }

        public SetDailyViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TableViewDates.Source = new MyDateTableViewSource();
            CurrentMedi.DailyAppointments = null;
            PickerTime.Date = NSDate.Now;
            TableViewDates.SeparatorStyle = UITableViewCellSeparatorStyle.None;
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            if (TableViewDates.Source is MyDateTableViewSource s)
                return s.Times.Count > 0;
            return false;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.DestinationViewController is SaveMediViewController saveMediViewController)
            {
                if(TableViewDates.Source is MyDateTableViewSource s){
                    CurrentMedi.DailyAppointments = s.Times;
                    saveMediViewController.CurrentMedi = CurrentMedi;
                }
            }
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