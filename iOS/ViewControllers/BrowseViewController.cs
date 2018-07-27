using System;
using System.Collections.Specialized;

//using ChameleonFramework;
using Foundation;
using UIKit;
using OneMediPlan.iOS.CustomCells;

namespace OneMediPlan.iOS
{
    public partial class BrowseViewController : UITableViewController
    {
        UIRefreshControl refreshControl;
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_CELL");

        //public ItemsViewModel Viewmodel { get; set; }
        public MediViewModel ViewModel { get; set; }


        public BrowseViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
       
            var nib = MyMediTableViewCell.Nib;
            var key = MyMediTableViewCell.Key;

            TableView.RegisterNibForCellReuse(nib, key);
            ViewModel = new MediViewModel();
            TableView.RowHeight = 100;

            // Setup UITableView.
            refreshControl = new UIRefreshControl();
            refreshControl.ValueChanged += RefreshControl_ValueChanged;
            TableView.Add(refreshControl);
            TableView.Source = new MedisDataSource(ViewModel);

            Title = ViewModel.Title;

            ViewModel.PropertyChanged += IsBusy_PropertyChanged;
            //ViewModel.Items.CollectionChanged += Items_CollectionChanged;
            ViewModel.Medis.CollectionChanged += Items_CollectionChanged;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            //if (ViewModel.Items.Count == 0)
            if (ViewModel.Medis.Count == 0)
                ViewModel.LoadItemsCommand.Execute(null);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "NavigateToItemDetailSegue")
            {
                var controller = segue.DestinationViewController as BrowseItemDetailViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                //var item = ViewModel.Items[indexPath.Row];
                var item = ViewModel.Medis[indexPath.Row];

                //controller.ViewModel = new ItemDetailViewModel(item);
                controller.ViewModel = new MediDetailViewModel(item);
            }
            else
            {
                var controller = segue.DestinationViewController as ItemNewViewController;
                controller.ViewModel = ViewModel;
                //controller.ViewModel = ViewModel;
            }
        }

        void RefreshControl_ValueChanged(object sender, EventArgs e)
        {
            if (!ViewModel.IsBusy && refreshControl.Refreshing)
                ViewModel.LoadItemsCommand.Execute(null);
        }

        void IsBusy_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var propertyName = e.PropertyName;
            switch (propertyName)
            {
                case nameof(ViewModel.IsBusy):
                    {
                        InvokeOnMainThread(() =>
                        {
                            if (ViewModel.IsBusy && !refreshControl.Refreshing)
                                refreshControl.BeginRefreshing();
                            else if (!ViewModel.IsBusy)
                                refreshControl.EndRefreshing();
                        });
                    }
                    break;
            }
        }

        void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InvokeOnMainThread(() => TableView.ReloadData());
        }
    }

    class MedisDataSource : UITableViewSource
    {
        MediViewModel viewModel;

        public MedisDataSource(MediViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(MyMediTableViewCell.Key, indexPath) as MyMediTableViewCell;
            var medi = viewModel.Medis[indexPath.Row];
            cell.Name = medi.Name;
            cell.Next = medi.NextDate.ToString();
            cell.Last = medi.LastDate.ToString();
            cell.Stock = $"{medi.Stock.ToString("F1")} / {medi.MinimumStock.ToString("F1")}";
            cell.Dosage = medi.Dosage.ToString("F1");
            cell.BackgroundColor = UIColor.LightTextColor;
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

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            => tableView.DeselectRow(indexPath, true);

        public override nint RowsInSection(UITableView tableview, nint section)
            => viewModel.Medis.Count;
    }
}
