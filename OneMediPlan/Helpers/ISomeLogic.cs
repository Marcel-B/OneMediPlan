using System.Threading.Tasks;
using OneMediPlan.Models;

namespace OneMediPlan.Helpers
{
    public interface ISomeLogic
    {
        Task HandleIntoke(Medi medi);
    }
}