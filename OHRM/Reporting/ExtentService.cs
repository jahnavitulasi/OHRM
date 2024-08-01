using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Internal;
using OrangeHRM.Automation.Framework.Logger;


namespace OrangeHRM.Automation.Framework.Reporting
{
    public class ExtentService
    {
        #region Lazy Instance
        private static readonly ThreadLocal<ExtentService> instance = new ThreadLocal<ExtentService>(()=> new ExtentService());  
        public static ExtentService Instance 
        {
            get => instance.Value; 
            protected internal set => instance.Value = value;
        }
        private ExtentService() { }
        #endregion

        private static ExtentTest Feature {  get; set; }
        private static ExtentTest Scenario { get; set;}
        private static ExtentTest Step { get; set;}
        private static ExtentReports ExtentReports { get; set; }
        public string ReportPath {  get; set; }
        public ExtentSparkReporter Initialize()
        {
            var projectPath = NUnit.Framework.TestContext.CurrentContext.TestDirectory;
            ReportPath = $"{projectPath}\\Reports\\AutomationReport_{DateTime.Now:yyyyMMDDhhmm}";
            if(Directory.Exists(projectPath)==false) 
            {
                Directory.CreateDirectory(projectPath);
            }
            if (Directory.Exists(ReportPath + "\\Img") == false)
            {
                Directory.CreateDirectory(ReportPath + "\\Img");
            }
            string reportFilename = string.Concat(ReportPath, "\\ExtentTestReport.html");
            ExtentSparkReporter reporter = new ExtentSparkReporter(reportFilename);
            //reporter.Config.Theme = Theme.Standard;
            reporter.Config.ReportName = "OrangeHRM Execution Report";
            reporter.Config.DocumentTitle = "Orange HRM";
            ExtentReports = new ExtentReports();
            ExtentReports.AttachReporter(reporter);
            return reporter;
        }

        public ExtentTest CreateFeature(string featureName)
        {
            return Feature = ExtentReports.CreateTest<Feature>(featureName);
        }
        public ExtentTest CreateScenario(string scenarioName,IOrderedDictionary arguments, string[] tags = null)
        {
            string specflowScenarioName = scenarioName;
            if(arguments.Count > 0)
            {
                specflowScenarioName = string.Concat(scenarioName, " - ", arguments[0]);
            }
            Scenario = Feature.CreateNode<Scenario>(specflowScenarioName);
            if(tags != null)
            {
                Scenario.AssignCategory(tags);
            }
            return Scenario;
        }
        public void LogInfo(string infoMessage)
        {
            if (!string.IsNullOrEmpty(infoMessage))
            {
                Logger.Logger.Instance.Info(infoMessage);
            }
            Step = Scenario.CreateNode<Given>(infoMessage);
        }
        public void LogScreenShot(Status status,Media mediaModel)
        {
            switch(status)
            {
                case Status.Pass:
                    Step.Log(status, string.Empty, mediaModel);
                    break;
                case Status.Fail:
                    Step.Log(status,string.Empty, mediaModel);
                    break;
                case Status.Error:
                    break;
                case Status.Warning:
                    Step.Log(status,string.Empty,mediaModel);
                    break;
                case Status.Info:
                    break;
                case Status.Skip:
                    Step.Log(status, string.Empty, mediaModel);
                    break ;
                default:
                    break;
            }
        }
        public void LogError(Status status, string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
            {
                Logger.Logger.Instance.Error(errorMessage);
            }
            Step = Scenario.CreateNode<Given>(errorMessage);
            Step.Log(status, errorMessage);
        }
        public void Flush()
        {
            ExtentReports.Flush();
        }
    }

}
