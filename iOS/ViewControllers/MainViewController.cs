using System;
using System.Collections.Specialized;
using OneMediPlan.iOS.CustomCells;

using ChameleonFramework;
using Foundation;
using UIKit;
using OneMediPlan.Helpers;
using OneMediPlan.ViewModels;
using Ninject;
using Ninject.Parameters;
using System.Runtime.CompilerServices;
using OneMediPlan.Models;
using System.Threading.Tasks;
using Realms;

namespace OneMediPlan.iOS
{
    public partial class MainViewController : UITableViewController
    {
        //UIRefreshControl refreshControl;
        public MainViewModel ViewModel { get; set; }

        public MainViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<MainViewModel>();
            //ViewModel.PropertyChanged += IsBusy_PropertyChanged;
            ViewModel.Medis.CollectionChanged += Items_CollectionChanged;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView.RegisterNibForCellReuse(MyMediTableViewCell.Nib, MyMediTableViewCell.Key);

            TableView.RowHeight = 100;

            // Setup UITableView.
            //refreshControl = new UIRefreshControl();
            //refreshControl.ValueChanged += RefreshControl_ValueChanged;
            //TableView.Add(refreshControl);
            var source = App.Container.Get<MedisDataSource>();
            source.parent = this;
            TableView.Source = source;
            Title = ViewModel.Title;


            //ViewModel.PropertyChanged += IsBusy_PropertyChanged;
            //ViewModel.Medis.CollectionChanged += Items_CollectionChanged;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            //var rows = TableView.NumberOfRowsInSection(0);
            //if (ViewModel.Medis.Count == 0)
            ViewModel.LoadItemsCommand.Execute(null);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "NavigateToItemDetailSegue")
            {
                var controller = segue.DestinationViewController as BrowseItemDetailViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                var item = ViewModel.Medis[indexPath.Row];
                controller.ViewModel = App.Container.Get<MediDetailViewModel>(new ConstructorArgument("item", item));
            }
            //else
            //{
            //    var controller = segue.DestinationViewController as NewMediViewController;
            //    controller.ViewModel = new NewMediViewModel();
            //}
        }

        //void RefreshControl_ValueChanged(object sender, EventArgs e)
        //{
        //    if (!ViewModel.IsBusy && refreshControl.Refreshing)
        //        ViewModel.LoadItemsCommand.Execute(null);
        //}

        //void IsBusy_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    var propertyName = e.PropertyName;
        //    switch (propertyName)
        //    {
        //        case nameof(ViewModel.IsBusy):
        //            {
        //                InvokeOnMainThread(() =>
        //                {
        //                    if (ViewModel.IsBusy && !refreshControl.Refreshing)
        //                        refreshControl.BeginRefreshing();
        //                    else if (!ViewModel.IsBusy)
        //                        refreshControl.EndRefreshing();
        //                });
        //            }
        //            break;
        //    }
        //}

        void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InvokeOnMainThread(() => TableView.ReloadData());
        }
    }

    internal class MedisDataSource : UITableViewSource
    {
        MainViewModel viewModel;
        public MainViewController parent;

        public MedisDataSource(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(MyMediTableViewCell.Key, indexPath) as MyMediTableViewCell;
            var medi = viewModel.Medis[indexPath.Row];
            cell.Background = UIColor.FromRGB(223, 249, 251);
            cell.Name = medi.Name;
            cell.Next = medi.GetNextDate();
            cell.Last = medi.GetLastDate();
            cell.Stock = medi.GetStockInfo();
            cell.Dosage = medi.GetDosage();
            cell.BackgroundColor = UIColor.LightTextColor;
            switch (medi.IntervallType)
            {
                case Models.IntervallType.Depend:
                    cell.ImageColor = UIColor.FromRGB(248, 194, 145);
                    break;
                case Models.IntervallType.IfNedded:
                    cell.ImageColor = UIColor.FromRGB(184, 233, 148);
                    break;
                case Models.IntervallType.Intervall:
                    cell.ImageColor = UIColor.FromRGB(229, 142, 38);
                    break;
                case Models.IntervallType.Nothing:
                    cell.ImageColor = UIColor.FromRGB(235, 47, 6);
                    break;
                case Models.IntervallType.Weekdays:
                    cell.ImageColor = UIColor.FromRGB(7, 153, 146);
                    break;
                case Models.IntervallType.DailyAppointment:
                    cell.ImageColor = UIColor.FromRGB(30, 55, 153);
                    break;
                default:
                    break;
            }
            if (medi.Stock <= medi.MinimumStock)
                cell.ImageColor = UIColor.FromRGB(235, 47, 6);
            return cell;
        }

        public UIContextualAction ContextualFlagAction(int row)
        {
            var action = UIContextualAction.FromContextualActionStyle
                            (UIContextualActionStyle.Normal,
                                "Foo",
                                (FlagAction, view, success) =>
                                {
                                    success(true);
                                });

            //action.Image = UIImage.FromFile("feedback.png");
            action.BackgroundColor = UIColor.Blue;

            return action;
        }

        public UIContextualAction ContextualDefinitionAction(int row)
        {

            var action = UIContextualAction.FromContextualActionStyle(UIContextualActionStyle.Normal,
                "Bar",
                (ReadLaterAction, view, success) =>
                {
                });
            action.BackgroundColor = UIColor.Green;
            return action;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            viewModel.Medis.RemoveAt(indexPath.Row);
            //base.CommitEditingStyle(tableView, editingStyle, indexPath);
        }

        //public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        //{   // Optional - default text is 'Delete'
        //    return "Trash (" + tableItems[indexPath.Row].SubHeading + ")";
        //}

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
            => true;

        public override UISwipeActionsConfiguration GetLeadingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            //UIContextualActions
            var definitionAction = ContextualDefinitionAction(indexPath.Row);
            var flagAction = ContextualFlagAction(indexPath.Row);

            //UISwipeActionsConfiguration
            var leadingSwipe = UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { flagAction, definitionAction });

            leadingSwipe.PerformsFirstActionWithFullSwipe = false;
            return leadingSwipe;
        }

        public async override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            var medi = viewModel.Medis[indexPath.Row];

            if (medi.Stock <= 0)
            {
                //Create Alert
                var okAlertController = UIAlertController.Create("Waring", $"No '{medi.Name}' left.", UIAlertControllerStyle.Alert);

                //Add Action
                okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                // Present Alert
                parent.PresentViewController(okAlertController, true, null);
                return;
            }
            else if (medi.Stock - medi.Dosage < 0)
            {
                // TODO - Fire warn message
                //Create Alert
                var okCancelAlertController = UIAlertController.Create("Waring", $"Not enough '{medi.Name}' left.", UIAlertControllerStyle.Alert);

                //Add Actions
                okCancelAlertController.AddAction(UIAlertAction.Create($"Take last {medi.Stock} unit(s)", UIAlertActionStyle.Default, async alert =>
                {
                    await UpdateList(medi);
                }));

                okCancelAlertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));

                // Present Alert
                parent.PresentViewController(okCancelAlertController, true, null);
                return;
            }
            await UpdateList(medi);
        }

        public async Task UpdateList(Medi medi)
        {
            var s = App.Container.Get<ISomeLogic>();
            await s.HandleIntoke(medi);
            viewModel.LoadItemsCommand.Execute(null);
            return;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
            => viewModel.Medis.Count;
    }
}
