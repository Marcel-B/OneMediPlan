using System.Threading.Tasks;
using com.b_velop.OneMediPlan.Models;

namespace com.b_velop.OneMediPlan.Helpers
{
    public interface ISomeLogic
    {
        Task HandleIntoke(AppMedi medi);
    }
}