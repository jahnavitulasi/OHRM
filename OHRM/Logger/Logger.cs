using System.Threading;

namespace OrangeHRM.Automation.Framework.Logger
{
    public class Logger
    {
        #region Lazy Instance
        private static readonly ThreadLocal<Logger> instance = new ThreadLocal<Logger>(()=>new Logger());
        public static Logger Instance
        {
            get => instance.Value;
            internal set => instance.Value = value;
        }
        public Logger() { }
        #endregion

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Actions
        public void Info(string message)
        {
            log.Info(message);
        }
        public void Warn(string message)
        {
            log.Warn(message);
        }
        public void Error(string message)
        {
            log.Error(message);
        }
        public void Debug(string message)
        {
            log.Debug(message);
        }
        #endregion
    }
}
