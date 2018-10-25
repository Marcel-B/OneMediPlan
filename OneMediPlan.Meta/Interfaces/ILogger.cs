using System;
namespace com.b_velop.OneMediPlan.Meta.Interfaces
{
    public interface ILogger
    {
        void Log(string message);
        void Log(string message, string type);
        void Log(string message, Type type);
        void Log(string message, Type type, Exception ex);
    }
}
