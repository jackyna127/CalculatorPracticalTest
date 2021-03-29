using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using WebCalculatorTest.Pages;
using WebCalculatorTest.Utilities;

namespace WebCalculatorTest.StepDefinitions
{
    [Binding]
    public class WebCalculatorTestScenariosSteps
    {
        CalculatePage calculatePage;
        public WebCalculatorTestScenariosSteps(ScenarioContext scenarioContext)
        {
            int secondTimeOut = (int)scenarioContext["secondTimeOut"];
            IWebDriver webDriver = (IWebDriver)scenarioContext["WebDriver"];
            calculatePage = new CalculatePage(webDriver, secondTimeOut);
        }

        [Given(@"the user wants to see Scenario (.*)")]
        public void GivenTheUserWantsToSeeScenario(string scenarioName)
        {
           
        }

        [Given(@"he enters the left number (.*)")]
        public void GivenHeEntersTheLeftNumber(string leftNumber)
        {
            calculatePage.EnterLeftNumber(leftNumber);
            Loggers.Log("Enter Left number:" + leftNumber);
        }
        [Given(@"he chooses the operator (.*)")]
        public void GivenHeChoosesTheOperator(string selectOperator)
        {
            if (selectOperator != "")            
            {
                calculatePage.SelectOperator(selectOperator);
            }
            Loggers.Log("Select operator: " + selectOperator);
        }

        [Given(@"he enters the right number (.*)")]
        public void GivenHeEntersTheRightNumber(string rightNumber)
        {
            calculatePage.EnterRightNumber(rightNumber);
            Loggers.Log("Enter Right Number: " + rightNumber);
        }
        
        [Given(@"the user accidentally triggers with Scenario (.*)")]
        public void GivenTheUserAccidentallyTriggersWithScenario(string scenarioName)
        {
    
        }
        
        [When(@"he clicks the Calculate button")]
        public void WhenHeClicksTheCalculateButton()
        {
            calculatePage.ClickCalculateButton();
        }
        
        [Then(@"he will see the calculate (.*) on the web page")]
        public void ThenHeWillSeeTheCalculateOnTheWebPage(int expectedResult)
        {
            int actualResult = calculatePage.GetCalculateResult();
            Loggers.Log("Expected result:" + expectedResult + ", actual result: " +  actualResult);
            Assert.AreEqual(expectedResult, actualResult,"Expected result:" + expectedResult +" , actual result: " + actualResult + ". More infomation please check the logs");
        }
        
        [Then(@"he will see the error (.*) on the web page")]
        public void ThenHeWillSeeTheErrorOnTheWebPage(string expectedMessage)
        {
            //todo, need the requirement when invalid input for calculator, for now, just assuming it would return an error message.
            string actualMessage = "";
            Assert.AreEqual(expectedMessage, actualMessage,
               $"Expected result {expectedMessage}, actual result: {actualMessage}. More infomation please check the logs");
        }
    }
}
