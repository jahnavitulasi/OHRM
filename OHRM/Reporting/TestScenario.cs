using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeHRM.Automation.Framework.Reporting
{
    public class TestScenario
    {
        #region Properties
        public string FeatureName { get; set; }
        public string ScenarioName { get; set; }
        public OrderedDictionary TestParams { get; set; }
        public string[] ScenarioTags { get; set; }
        public string ScenarioStatus { get; set; }
        public DateTime StartTime { get; set; }
        public string Duration { get; set; }
        #endregion
    }

    public class TestRunInfo
    {
        #region Properties
        public string ProjectName {  get; set; }
        public string ProjectVersion { get; set; }
        public string OSBuildNumber { get; set; }
        public string HostMachine { get; set; }
        public string WindowsUserName { get; set; }
        public string User {  get; set; }
        public string Url { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsSmokeSuite { get; set; }
        #endregion
    }

}
