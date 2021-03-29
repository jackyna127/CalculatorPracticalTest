using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using WebCalculatorTest.Pages;

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
        }
        [Given(@"he chooses the operator (.*)")]
        public void GivenHeChoosesTheOperator(string selectOperator)
        {
            if (selectOperator != "")            
            {
                calculatePage.SelectOperator(selectOperator);
            }
        }

        [Given(@"he enters the right number (.*)")]
        public void GivenHeEntersTheRightNumber(string rightNumber)
        {
            calculatePage.EnterRightNumber(rightNumber);
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
            Assert.AreEqual(expectedResult, actualResult, 
                $"Expected result: {expectedResult}, Web calculator returns actual result: {actualResult}.");
        }
        
        [Then(@"he will see the error (.*) on the web page")]
        public void ThenHeWillSeeTheErrorOnTheWebPage(string expectedMessage)
        {
            //todo, need the requirement when invalid input for calculator, for now, just assuming it would return an error message.
            string actualMessage = "";
            Assert.AreEqual(expectedMessage, actualMessage,
               $"Expected result {expectedMessage}, eb calculator returns actual result: {actualMessage}.");
        }
    }
}
