using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace WebCalculatorTest.Utilities
{
    public static class DriverExtention
    {
        public static IWebDriver OpenBrowser(string BrowserType, string baseUrl, int secondTimeout)
        {
            IWebDriver webDriver = null;           
            switch (BrowserType.ToUpper())
            {
                
                case "CHROME":
                    webDriver = new ChromeDriver();
                    webDriver.Url = baseUrl;
                    break;
                case "IE":
                    var options = new InternetExplorerOptions();
                    options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    options.IgnoreZoomLevel = true;
                    options.InitialBrowserUrl = baseUrl;
                    webDriver = new InternetExplorerDriver(options);
                    break;
                case "FIREFOX":
                    webDriver = new FirefoxDriver();
                    break;
                default:
                    Loggers.Log("Failed initializing driver, browser :" + BrowserType,"FATAL");                   
                    break;
            }
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(secondTimeout);
            return webDriver;
        }
        public static void CloseBrowser(IWebDriver webDriver)
        {
            webDriver.Quit();
        }
    }
}
