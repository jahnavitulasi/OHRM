using NUnit.Framework;
using OrangeHRM.Automation.PageObjects.Pages;
using TechTalk.SpecFlow;

namespace OrangeHRM.Automation.Tests.StepDefinations
{
    [Binding]
    public sealed class Home
    {

        [Given(@"I am in HomePage\.")]
        public void GivenIAmInHomePage_()
        {
            HomePage.Instance.ValidateUSerInHomePage();
        }
    }
}