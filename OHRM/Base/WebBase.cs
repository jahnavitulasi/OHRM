using OrangeHRM.Automation.Framework.WebDriverConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualBasic;


namespace OrangeHRM.Automation.Framework.Base
{
     public class WebBase
     {
        public IWebDriver webDriver;
        public IJavaScriptExecutor javaScriptExecutor;
        public Actions actions;
        public WebDriverWait wait;
        internal int wait_seconds = 100;
        public string parentWindow = null;

        public void SetWebDriverImplicitTimeout(int seconds)
        {
            webDriver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(seconds);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }
        public void NavigateToURL(string url)
        {
            webDriver.Navigate().GoToUrl(url);
        }
        protected void ClickElement(IWebElement element)
        {
            element.Click();
        }
        protected IWebElement FindElement(By by)
        {
            return webDriver.FindElement(by);
        }
        protected IWebElement FindElementByName(string name)
        {
           return webDriver.FindElement(By.Name(name));
        }
        protected IWebElement FindElementByXPath(string xpath)
        {
            return webDriver.FindElement(By.XPath(xpath));
        }
        public bool IsElementVisible(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch(NoSuchElementException) { return false; }
            catch(StaleElementReferenceException) { return false; }
            catch(Exception) { return false; }
        }
        public void TakeScreenshotandSave(string saveLocation)
        {
            try
            {
                ITakesScreenshot ssdriver = webDriver as ITakesScreenshot;
                webDriver.Manage().Window.Maximize();
                Screenshot screenshot = ssdriver.GetScreenshot();
                screenshot.SaveAsFile(saveLocation);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
