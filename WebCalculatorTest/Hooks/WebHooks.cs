using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using WebCalculatorTest.Utilities;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace WebCalculatorTest.Hooks
{
    [Binding]
    public sealed class WebHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private IWebDriver webDriver;
        private string browserType = "";
        private string baseWebUrl = "";
        private int secondTimeOut = 0;
        private ScenarioContext scenarioContext;
        public WebHooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            baseWebUrl = ConfigurationManager.AppSettings["BaseUrl"]; 
            secondTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOut"]);
            var testContext = scenarioContext.ScenarioContainer.Resolve<TestContext>();
            browserType = testContext.Properties["Browser"]?.ToString();            
            webDriver = DriverExtention.OpenBrowser(browserType, baseWebUrl, secondTimeOut);
            scenarioContext["WebDriver"] = webDriver;
            scenarioContext["secondTimeOut"] = secondTimeOut;
        }
       
        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                if (scenarioContext.TestError != null)
                {
                    Screenshot screenShot = ((ITakesScreenshot)webDriver).GetScreenshot();
                    string title = scenarioContext.ScenarioInfo.Title;
                    string Runname = title + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
                    string currentDirectory = System.IO.Directory.GetCurrentDirectory();
                    string path = currentDirectory.Split(new string[] { "\\bin" }, StringSplitOptions.None)[0];
                    string screenshotfilename = path + "\\Reporting\\Screenshots\\" + Runname + ".png";
                    screenShot.SaveAsFile(screenshotfilename, ScreenshotImageFormat.Png);
                        
                  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Save screen shots failed:{e.Message}");   
            }
            finally
            {
                DriverExtention.CloseBrowser(webDriver);
                if (scenarioContext.ContainsKey("WebDriver"))
                {
                    scenarioContext.Remove("WebDriver");

                };
            }          
           
        }



    }
}

