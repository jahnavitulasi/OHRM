using OrangeHRM.Automation.Framework.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace OrangeHRM.Automation.Framework.WebDriverConfiguration
{
    public class WebPage : WebBase
    {
        #region Constructor
        protected string _windowHandle;
        protected TimeSpan RetryTimeout = TimeSpan.FromSeconds(30);
        public WebPage() { }

        
        #endregion
    }
}
