using OrangeHRM.Automation.Framework.Helpers;
using OrangeHRM.Automation.Framework.Reporting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeHRM.Automation.Tests.Helper
{
    internal class UserHelper
    {
        private static readonly ThreadLocal<UserHelper> instance = new ThreadLocal<UserHelper>(()=> new UserHelper());
        public static UserHelper Instance
        {
            get => instance.Value;
            protected internal set => instance.Value = value;
        }
        public string GetUserName()
        {
            ConcurrentBag<LoginAccount> userID = ExecutionDefaults.Users;
            string userName = userID.ToList()[0].UserName;
            return userName;
        }

        public string GetPassword()
        {
            ConcurrentBag<LoginAccount> userIDs =ExecutionDefaults.Users;
            string password = userIDs.ToList()[0].Password;
            return password;
        }
    }
}
