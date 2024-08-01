using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OrangeHRM.Automation.Framework.Utilites
{
    public class ProcessUtil
    {
        #region Lazy Instance
        private static readonly ThreadLocal<ProcessUtil> instance = new ThreadLocal<ProcessUtil>(()=> new ProcessUtil());

        public static ProcessUtil Instance
        {
            get { return instance.Value; }
            protected internal set { instance.Value = value; }
        }
        private ProcessUtil() { }
        #endregion

        public void KillProcess(string processName)
        {

            IEnumerable<Process> runningProcess = Process.GetProcesses().Where<Process>(p => p.ProcessName.Contains(processName));

            if (runningProcess.ToArray().Length > 0)
            {
                foreach (var process in runningProcess)
                {
                    if (process.HasExited == false)
                    {
                        process.Kill();
                    }
                }
            }
        }
        public void KillChromeDriver()
        {
            ProcessUtil.Instance.KillProcess("chromedriver");
        }
    }
}
