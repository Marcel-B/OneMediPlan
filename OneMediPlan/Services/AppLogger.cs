using System;
using com.b_velop.OneMediPlan.Meta.Interfaces;

namespace com.b_velop.OneMediPlan.Services
{
	public class AppLogger : ILogger
    {
        public AppLogger()
        {
        }

        public void Log(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Log(string message, string type)
        {
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now}] [{type}] - {message}");
        }

        public void Log(string message, Type type)
        {
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now}] [{type.Name}] - {message}");
        }

        public void Log(string message, Type type, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now}] [{type.Name}] - {message + Environment.NewLine}{ex.StackTrace}");
        }
    }
}
