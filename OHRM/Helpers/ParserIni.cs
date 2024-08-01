using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;

namespace OrangeHRM.Automation.Framework.Helpers
{
    public class ParserIni
    {
        private static readonly Lazy<ParserIni> instance = new Lazy<ParserIni>(() => new ParserIni());

        public static ParserIni Instance => instance.Value;

        private readonly IniData _testExecutionParser;
        private readonly IniData _orangeHRMTestDataParser;

        private string OrangeHRMTestDataFileName { get; set; }

        public ParserIni()
        {
            FileIniDataParser parser = new FileIniDataParser
            {
                Parser =
                {
                    Configuration =
                    {
                        CommentString = "#",
                        AssigmentSpacer = "",
                        ThrowExceptionsOnError =true
                    }

                }
            };
            try
            {
                string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string dir = Path.Combine(currentDirectory, @"Configuration\TestExecution.ini");
                _testExecutionParser = parser.ReadFile(dir);
                OrangeHRMTestDataFileName = GetTestExecutionValues("TESTDATAFILES", "OrangeHRM");
                if(OrangeHRMTestDataFileName != null)
                {
                    string OrangeHRMTestDataFilePath = Path.Combine(currentDirectory, @"Configuration\" + OrangeHRMTestDataFileName + ".ini");
                    if (File.Exists(OrangeHRMTestDataFilePath))
                    {
                        _orangeHRMTestDataParser = parser.ReadFile(OrangeHRMTestDataFilePath);
                    }

                }

            }
            catch(Exception ex)
            {
                throw new Exception($"Exception while accessing the test data .ini file {ex.Message}");
            }
        }

        public string GetTestExecutionValues(string section, string key)
        {
            try
            {
                return _testExecutionParser[section][key]?.Trim();
            }
            catch
            {
                return null;
            }
        }
        public string GetOrangeHRMTestDataValues(string section, string key)
        {
            string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            try
            {
                if (OrangeHRMTestDataFileName != null)
                {
                    string OrangeHRMTestDataFilePath = Path.Combine(currentDirectory, @"Configuration\" + OrangeHRMTestDataFileName + ".ini");
                    if (File.Exists(OrangeHRMTestDataFilePath))
                    {
                        return _orangeHRMTestDataParser[section][key].Trim();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception) 
            {
                return null;
            }
        }
        public void LoadOrangeHRMDetails()
        {
            try
            {
                ExecutionDefaults.Instance.URL = GetOrangeHRMTestDataValues("GENERAL", "URL");
                KeyDataCollection keydataCollection = _orangeHRMTestDataParser["CREDENTIALS"];
                foreach (KeyData keyValue in keydataCollection)
                {
                    ExecutionDefaults.Users.Add(new LoginAccount(keyValue.KeyName, keyValue.Value));
                }
            }
            catch(Exception) { }

        }
        internal void InitializeExecutionDefaults()
        {
            string currentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (GetTestExecutionValues("EXECUTION", "Browser") != null)
            {
                ExecutionDefaults.Instance.Browser = GetTestExecutionValues("EXECUTION", "Browser");
            }
            else
            {
                ExecutionDefaults.Instance.Browser = "Chrome";
            }
            if (OrangeHRMTestDataFileName != null)
            {
                string OrangeHRMTestDataFilePath = Path.Combine(currentDirectory, @"Configuration\" + OrangeHRMTestDataFileName + ".ini");
                if (File.Exists(OrangeHRMTestDataFilePath))
                {
                    LoadOrangeHRMDetails();
                }
            }
        }
    }
}
