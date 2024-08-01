using FluentAssertions;
using OpenQA.Selenium;
using OrangeHRM.Automation.Framework.Base;
using OrangeHRM.Automation.Framework.Extensions;
using OrangeHRM.Automation.Framework.WebDriverConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeHRM.Automation.PageObjects.Pages
{
    public class HomePage : WebPage
    {

        #region Lazy Instance
        private static readonly ThreadLocal<HomePage> instance = new ThreadLocal<HomePage>(()=>new HomePage());

        public static HomePage Instance
        {
            get => instance.Value;
            protected internal set => instance.Value = value;
        }

        public void SetDriver(IWebDriver webdriver)
        {
            webDriver = webdriver;
        }
        #endregion
        private IWebElement OrangeHRMLogo => FindElementByXPath("//div[@class='oxd-topbar-header-title']");
        public void ValidateUSerInHomePage()
        {
            OrangeHRMLogo.Text.Should().Be("Dashboard");
        }
    }
}
