using OpenQA.Selenium;
using OrangeHRM.Automation.Framework.Helpers;
using OrangeHRM.Automation.Framework.WebDriverConfiguration;
using OrangeHRM.Automation.Framework.Extensions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using FluentAssertions;
using WebDriverManager.DriverConfigs.Impl;
using OrangeHRM.Automation.Framework.Extensions;

namespace OrangeHRM.Automation.PageObjects.Pages
{
    public class LoginPage : WebPage
    {
        #region Lazy Instance
        private static readonly ThreadLocal<LoginPage> instance = new ThreadLocal<LoginPage>(()=>new LoginPage());

        public static LoginPage Instance
        {
            get=> instance.Value; 
            protected internal set => instance.Value = value;
        }
        #endregion

        #region Elements
        private By LoginLabel = By.XPath("//div[@class='orangehrm-login-slot']//h5[text()='Login']");
        private IWebElement TxtUsername => FindElementByName("username");
        private IWebElement TxtPassword => FindElementByName("password");
        private IWebElement BtnLogin => FindElementByXPath("//button[contains(@class,'login-button')]");
        private IWebElement ErrorAlert => FindElementByXPath("//p[contains(@class,'alert')]");

        private By OrangeHRMBanner => By.XPath("//div[@class='oxd-brand-banner']");
        #endregion

        #region Actions
        public IWebDriver LaunchBrowser(Browser browser=Browser.Chrome)
        {
            if(browser == Browser.Edge)
            {
                webDriver = new EdgeDriver();
            }
            else
            {
                webDriver = new ChromeDriver(); 
            }
            webDriver.Manage().Window.Maximize();
            IntializePages();
            return webDriver;
        }


        public void Login(string username, string password)
        {
            webDriver.Title.Should().Be("OrangeHRM");
            webDriver.WaitElementIsVisible(LoginLabel);
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
        
        }

        public void ValidateLoggedInUser()
        {
            webDriver.WaitElementIsVisible(OrangeHRMBanner);
          
            IsElementVisible(FindElement(OrangeHRMBanner)).Should().BeTrue("User is logged in successfully");
        }

        public void IntializePages()
        {
            HomePage.Instance.SetDriver(webDriver);
        }

        public void QuiteDriver()
        {
            webDriver.Close();
            webDriver.Quit();
        }

        #endregion
    }
}
