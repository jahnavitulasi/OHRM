using TechTalk.SpecFlow;
using NUnit.Framework;
using System;
using OrangeHRM.Automation.Framework.Reporting;
using OrangeHRM.Automation.Framework.Utilites;
using OrangeHRM.Automation.Framework.Helpers;
using OrangeHRM.Automation.PageObjects.Pages;
using System.Collections.Specialized;
using AventStack.ExtentReports;
using System.Buffers.Text;
using log4net.Repository.Hierarchy;
using OrangeHRM.Automation.Framework.Logger;
using Logger = OrangeHRM.Automation.Framework.Logger.Logger;
using AventStack.ExtentReports.Model;

namespace OrangeHRM.Automation.Tests.Helper
{
    [Binding]
    public sealed class Hooks
    {
        static List<TestScenario> TestScenarios;
        static TestScenario CurrentTestScenario;
        private readonly ScenarioContext _scenarioContext;
        static TestRunInfo CurrentTestRunInfo;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void TestInitialize()
        { 
            TestScenarios = new List<TestScenario>();
            CurrentTestRunInfo = new TestRunInfo();
            CurrentTestRunInfo.ProjectName = "OrangeHRM";
            CurrentTestRunInfo.StartTime = DateTime.Now;
            ProcessUtil.Instance.KillChromeDriver();
            string dir = Path.GetDirectoryName(typeof(Hooks).Assembly.Location);
            Environment.CurrentDirectory = dir;
            ExecutionDefaults.Instance.NunitInitialization();
            ExtentService.Instance.Initialize();
            string UserName = TestContext.Parameters.Get("UserName");

            if (!string.IsNullOrEmpty(UserName))
            {
                ExecutionDefaults.Users.ToArray()[0].UserName = UserName;
            }
            string Password = TestContext.Parameters.Get("Password");
            if (!string.IsNullOrEmpty(Password))
            {
                ExecutionDefaults.Users.ToArray()[0].Password = Password;
            }
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext) 
        {
            ExtentService.Instance.CreateFeature(TestContext.CurrentContext.Test.ClassName);
            if (featureContext.FeatureInfo.Tags.Contains("OrangeHRM"))
            {
                CurrentTestRunInfo.ProjectName = "OrangeHRM";
            }
        }

        [BeforeScenario(Order = 1)]
        public void BeforeScenario() 
        {
            ExtentService.Instance.CreateScenario(_scenarioContext.ScenarioInfo.Title, _scenarioContext.ScenarioInfo.Arguments, _scenarioContext.ScenarioInfo.Tags);
        }

        [BeforeScenario("LoginUser")]
        public void LaunchAndLoginOrangeHRMUser()
        {
            string userName = UserHelper.Instance.GetUserName();
            string password = UserHelper.Instance.GetPassword();
            LoginPage.Instance.LaunchBrowser();
            LoginPage.Instance.NavigateToURL(ExecutionDefaults.Instance.URL);
            LoginPage.Instance.Login(userName, password);
            LoginPage.Instance.ValidateLoggedInUser();
            ExtentService.Instance.LogInfo($"I login into OrangeHRM Application using username '{userName}'");
        }
        [BeforeScenario]
        
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            CurrentTestScenario = new TestScenario();
            CurrentTestScenario.FeatureName = FeatureContext.Current.FeatureInfo.Title;
            CurrentTestScenario.ScenarioName = scenarioContext.ScenarioInfo.Title;
            CurrentTestScenario.TestParams =(OrderedDictionary) scenarioContext.ScenarioInfo.Arguments;
            CurrentTestScenario.ScenarioTags = scenarioContext.ScenarioInfo.Tags;
            CurrentTestScenario.StartTime = DateTime.Now;
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            if(_scenarioContext.TestError != null)
            {
                string ScreenshotFilePath = Path.Combine(ExtentService.Instance.ReportPath + "\\Img", Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".png");
                Media mediaModel = MediaEntityBuilder.CreateScreenCaptureFromBase64String(ScreenshotFilePath).Build();
                LoginPage.Instance.TakeScreenshotandSave(ScreenshotFilePath);
                try
                {
                    ExtentService.Instance.LogScreenShot(Status.Fail, mediaModel);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Warn($"Expection Occured : {ex.StackTrace}");
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            ExtentService.Instance.Flush();
        }
        [AfterTestRun]
        public static void TearDownReport()
        {
            ExtentService.Instance.Flush();
            LoginPage.Instance.QuiteDriver();
        }
       
    }
}