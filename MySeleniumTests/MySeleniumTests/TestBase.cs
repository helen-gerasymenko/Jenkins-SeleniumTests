using System;
using OpenQA.Selenium.Remote;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;

namespace MySeleniumTests
{
    public abstract class TestBase
    {
        protected RemoteWebDriver Driver;

        protected TestBase()
        {
            Driver = CreateChromeDriver();
        }

        [OneTimeSetUp]
        public virtual void BeforeAll() { }

        [SetUp]
        public virtual void BeforeEach() { }


        [TearDown]
        public virtual void AfterEach()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                
            }
        }

        [OneTimeTearDown]
        public virtual void AfterAll()
        {
            Driver.Quit();
        }

        public RemoteWebDriver CreateRemoteWebDriver()
        {
            var uri = new Uri(ConfigurationManager.AppSettings["SeleniumHub"]).ToString();
            var options = new ChromeOptions();
            options.AddUserProfilePreference("password_manager_enabled", false);
            var capabilities = options.ToCapabilities();
            var driver = new RemoteWebDriver(new Uri(uri), capabilities)
            {
                Url = CreateBaseWebsiteUrl().ToString()
            };
            driver.Manage().Window.Size = new Size(1800, 900);
            return driver;
        }

        private static RemoteWebDriver CreateChromeDriver()
        {
            var pathToChromeExecutable = new FileInfo(Path.Combine(AssemblyLocalPath, @"..\..\..\packages\Selenium.WebDriver.ChromeDriver.75.0.3770.8\driver\win32")).FullName;

            //set an option to disable 'Save Password' prompt in the browser
            var options = new ChromeOptions();
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("password_manager_enabled", false);

            var driver = new ChromeDriver(pathToChromeExecutable, options)
            {
                Url = CreateBaseWebsiteUrl().ToString()
            };

            driver.Manage().Window.Size = new Size(1800, 900);
            return driver;
        }

        private static Uri CreateBaseWebsiteUrl()
        {
            return new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
        }

        public static string AssemblyLocalPath
        {
            get
            {
                var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                return assemblyPath == null ? string.Empty : new Uri(assemblyPath).LocalPath;
            }
            
        }
    }
}
