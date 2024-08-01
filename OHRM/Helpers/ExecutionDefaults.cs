using OrangeHRM.Automation.Framework.Utilites;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrangeHRM.Automation.Framework.Helpers;

namespace OrangeHRM.Automation.Framework.Helpers
{
    public class ExecutionDefaults
    {
        private static readonly Lazy<ExecutionDefaults> instance = new Lazy<ExecutionDefaults>(() => new ExecutionDefaults());

        public static ExecutionDefaults Instance => instance.Value;

        #region OrangeHRM Test Data
        public string URL { get; set; }
        public static ConcurrentBag<LoginAccount> Users = new ConcurrentBag<LoginAccount>();
        public string Browser {  get; set; }
        public string CurrentWorkingDirectory { get; set; }
        public string TestDataDirectory {  get; set; }
        public OrangeHRMUserType OHRMLoggedInUser { get; set; }
        

        #endregion

        public void NunitInitialization()
        {
            DefaultInitialization();
            CurrentWorkingDirectory = NUnit.Framework.TestContext.CurrentContext.TestDirectory;
            TestDataDirectory = Path.Combine(CurrentWorkingDirectory, "TestData");
            Directory.SetCurrentDirectory(NUnit.Framework.TestContext.CurrentContext.TestDirectory);
        }

        public void DefaultInitialization()
        {
            ParserIni.Instance.InitializeExecutionDefaults();
        }


    }
    public class LoginAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public LoginAccount(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }

    public enum OrangeHRMUserType
    {
        Admin,
        NotLoggedIn
    }

    public enum Browser
    {
        [Description("Chrome")]
        Chrome,
        [Description("Edge")]
        Edge
    }
}
