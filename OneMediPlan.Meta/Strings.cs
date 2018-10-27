using System.Reflection;
using I18NPortable;
using com.b_velop.OneMediPlan.Meta.Interfaces;
using System.Security.Cryptography;

namespace com.b_velop.OneMediPlan.Meta
{
    public class Strings
    {
        public ILogger Logger { get; set; }
        //public II18N DataStrings { get; set; }

        public Strings(ILogger logger)
        {
            Logger = logger;
            //DataStrings = I18N.Current
                  //.SetNotFoundSymbol("!!") // Optional: when a key is not found, it will appear as $key$ (defaults to "$")
                  //.SetFallbackLocale("de") // Optional but recommended: locale to load in case the system locale is not supported
                  //.SetThrowWhenKeyNotFound(true) // Optional: Throw an exception when keys are not found (recommended only for debugging)
                  //.SetLogger(text => Logger.Log(text.Substring(7), typeof(I18N).Name)) // action to output traces
                  //.SetResourcesFolder("Locales") // Optional: The directory containing the resource files (defaults to "Locales")
                  //.Init(GetType().GetTypeInfo().Assembly); // assembly where locales live
        }

        public static string _AFTER = "after";
        public static string APP_TITLE = "One MediPlan";
        public static string APPOINTMENTS = "Appointments";

        public static string CANCEL = "Cancel".Translate();
        public static string CURRENT_MEDI = "CurrentMedi";

        public static string DATE_CELL = "DateCell";
        public static string DAYS = "Day(s)";
        public static string DEPENDS = "Depends";
        public static string DOSAGE = "Dosage";

        public static string EDIT = "Edit".Translate();

        public static string HOURS = "Hour(s)";

        public static string INTERVALL_TYPE = "IntervallType";

        public static string LABEL_TEXT = "LabelText";

        public static string MEDIS = "Medis".Translate();
        public static string MINUTES = "Minute(s)";
        public static string MONTHS = "Month(s)";

        public static string NAME = "Name";
        public static string NEW = "New";
        public static string NEW_MEDI = "NewMedi";
        public static string NO_JOKER_LEFT = "NoJokerLeft";
        public static string NOT_ENOUGH_JOKER_LEFT = "NotEnoughJokerLeft";
        //public const string DATE_FORMAT = "DateFormat";
        //public const string TIME_FORMAT = "TimeFormat";

        public static string SAVE = "Save".Translate();

        public static string SETTINGS = "Settings".Translate();
        public static string STOCK = "Stock".Translate();
        public static string STOCK_MINIMUM = "StockMinimum";

        public static string TAKE_LAST_JOKER_UNITS = "TakeLastJokerUnits";
        public static string TODAY = "Today";

        public static string WARNING = "Warning".Translate();
        public static string WEEKDAYS = "Weekdays";
        public static string WEEKS = "Week(s)";
    }
}
