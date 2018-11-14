using System;
using Redux;

namespace com.b_velop.OneMediPlan.Domain
{
    /*
     * Actions are payloads of information that send data from your application
     * to your store. They only need to implement the markup 
     * interface Redux.IAction.
     */
    public class IncrementAction : IAction { }
    public class DecrementAction : IAction { }
    public class AddMediAction : IAction
    {
        public Medi MyTestMedi { get; set; }
    }
}
