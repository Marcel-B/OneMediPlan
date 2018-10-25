using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using com.b_velop.OneMediPlan.Helpers;
using com.b_velop.OneMediPlan.iOS.CustomCells;
using com.b_velop.OneMediPlan.Meta;
using com.b_velop.OneMediPlan.Models;
using com.b_velop.OneMediPlan.ViewModels;
using Foundation;
using Ninject;
using Ninject.Parameters;
using UIKit;

namespace com.b_velop.OneMediPlan.iOS
{
    public partial class MainViewController : UITableViewController
    {
        public MainViewModel ViewModel { get; set; }

        public MainViewController(IntPtr handle) : base(handle)
        {
            ViewModel = App.Container.Get<MainViewModel>();
            ViewModel.Medis.CollectionChanged += Items_CollectionChanged;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView.RegisterNibForCellReuse(MyMediTableViewCell.Nib, MyMediTableViewCell.Key);

            TableView.RowHeight = 100;

            var source = App.Container.Get<MedisDataSource>();
            source.parent = this;
            TableView.Source = source;
            Title = ViewModel.Title;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
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
        }

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
                             NSBundle.MainBundle.GetLocalizedString(Strings.EDIT),
                                (FlagAction, view, success) =>
                                {
                                    success(true);
                                });

            //action.Image = UIImage.FromFile("feedback.png");
            action.BackgroundColor = UIColor.FromRGB(84, 160, 255);

            return action;
        }

        public UIContextualAction ContextualDefinitionAction(int row)
        {
            var medi = viewModel.Medis[row];
            var action = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                NSBundle.MainBundle.GetLocalizedString(Strings.STOCK),
                (ReadLaterAction, view, success) =>
                {
                    var foo = UIAlertController.Create("HeyHo", "Let's go", UIAlertControllerStyle.Alert);
                    foo.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, alert =>
                    {

                    }));

                    foo.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, alert =>
                    {

                    }));
                    foo.ShowViewController(foo, this);
                    var alertView = new UIAlertView
                    {
                        Title = NSBundle.MainBundle.GetLocalizedString(Strings.STOCK),
                        Message = $"Enter new stock for {medi.Name}, dude!",
                        AlertViewStyle = UIAlertViewStyle.PlainTextInput
                    };
                    alertView.GetTextField(0).Text = medi.Stock.ToString();
                    alertView.GetTextField(0).KeyboardType = UIKeyboardType.DecimalPad;

                    alertView.AddButton("OK");
                    alertView.AddButton(NSBundle.MainBundle.GetLocalizedString(Strings.CANCEL));
                    alertView.Clicked += async (object sender, UIButtonEventArgs e) =>
                    {
                        if (e.ButtonIndex == 0)
                        {
                            var stock = alertView.GetTextField(0).Text;
                            if (double.TryParse(stock, out var newStock))
                            {
                                medi.Stock = newStock;
                                success(true);
                                await UpdateList(medi);
                            }
                        }
                    };
                    alertView.Show();
                });
            action.BackgroundColor = UIColor.FromRGB(16, 172, 132);
            return action;
        }

        public override async void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
            => await viewModel.RemoveMedi(indexPath.Row);

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

            var waring = NSBundle.MainBundle.GetLocalizedString(Strings.WARNING);
            var cancel = NSBundle.MainBundle.GetLocalizedString(Strings.CANCEL);
            var noLeft = NSBundle.MainBundle.GetLocalizedString(Strings.NO_JOKER_LEFT);
            var notEnough = NSBundle.MainBundle.GetLocalizedString(Strings.NOT_ENOUGH_JOKER_LEFT);
            var takeLast = NSBundle.MainBundle.GetLocalizedString(Strings.TAKE_LAST_JOKER_UNITS);

            if (medi.Stock <= 0)
            {
                //Create Alert
                var okAlertController = UIAlertController.Create(waring, string.Format(noLeft, medi.Name), UIAlertControllerStyle.Alert);

                //Add Action
                okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                // Present Alert
                parent.PresentViewController(okAlertController, true, null);
                return;
            }
            else if (medi.Stock - medi.Dosage < 0)
            {
                //Create Alert
                var okCancelAlertController = UIAlertController.Create(waring, string.Format(notEnough, medi.Name), UIAlertControllerStyle.Alert);

                //Add Actions
                okCancelAlertController.AddAction(UIAlertAction.Create(string.Format(takeLast, medi.Name), UIAlertActionStyle.Default, async alert =>
                {
                    await UpdateList(medi);
                }));

                okCancelAlertController.AddAction(UIAlertAction.Create(cancel, UIAlertActionStyle.Cancel, null));

                // Present Alert
                parent.PresentViewController(okCancelAlertController, true, null);
                return;
            }
            await UpdateList(medi);
        }

        public async Task UpdateList(AppMedi medi)
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
