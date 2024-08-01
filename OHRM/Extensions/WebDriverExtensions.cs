using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeHRM.Automation.Framework.Extensions
{
    public static class WebDriverExtensions
    {
        private const int DEFAULT_ELEMENT_TIMEOUt = 30;

        public static void ClickUsingJavaScript(this IWebDriver webDriver, IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor) webDriver;
            executor.ExecuteScript("arguments[0].click();", element);
        }
        public static IWebElement WaitElementIsVisible(this IWebDriver webDriver, By path)
        {
            WebDriverWait wait = new WebDriverWait(webDriver,TimeSpan.FromSeconds(DEFAULT_ELEMENT_TIMEOUt));
            return wait.Until(ExpectedConditions.ElementIsVisible(path));

        }

    }
}
