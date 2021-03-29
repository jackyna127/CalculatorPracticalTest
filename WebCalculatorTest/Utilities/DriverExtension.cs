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
            if (string.IsNullOrEmpty(BrowserType))
            { 
                throw new Exception("Driver type is not provided. Please check test setting file.");
            }
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("URL is not provided. Please check test setting file.");
            }
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
                    throw new Exception($"Failed initializing driver. " +  $"Given driver type {BrowserType} is not supported.");

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
