using System;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using Ninject;
using UIKit;
using com.b_velop.OneMediPlan.ViewModels;

namespace com.b_velop.OneMediPlan.iOS.ViewControllers
{
    public class BaseViewController : UIViewController
    {
        public BaseViewController(IntPtr handle) : base(handle)
        {
            Logger = App.Container.Get<ILogger>();
        }
        protected ILogger Logger;
    }
}
