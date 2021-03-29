using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebCalculatorTest.Utilities;

namespace WebCalculatorTest.Pages
{
    public class CalculatePage: PageBase
    {
       
        private readonly By leftNumberInput = By.Id("leftNumber");
        private readonly By rightNumberInput = By.Id("rightNumber");
        private readonly By resultInput = By.ClassName("result");
        private readonly By operatorSelection = By.Id("operator");
        private readonly By calculateButton = By.Id("calculate");
        private readonly By iFrame = By.TagName("iframe");               

        public CalculatePage(IWebDriver webDriver, int timeOut) : base(webDriver, timeOut)
        {
            
        }

        public void EnterLeftNumber(string leftNumber)
        {
            SwitchToDefaultContent();
            EnterInputText(leftNumberInput,leftNumber);            
        }

        public void EnterRightNumber(string rightNumber)
        {
            SwitchToDefaultContent();
            EnterInputText(rightNumberInput, rightNumber);
        }

        public void SelectOperator(string selectedOperator)
        {
            SwitchToDefaultContent();
            SelectValueFromDropDown(operatorSelection, selectedOperator);
        }

       
        public void ClickCalculateButton()
        {
            SwitchToIFrame(iFrame);            
            ClickButton(calculateButton);
        }

        public int GetCalculateResult()
        {
            SwitchToDefaultContent();
            WaitUntilValueIsPopulated(resultInput);
            return Convert.ToInt32(GetTextValueFromElement(resultInput));
        }
    }
}
