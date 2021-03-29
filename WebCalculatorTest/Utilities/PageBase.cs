using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebCalculatorTest.Utilities
{
    public class PageBase
    {
        int secondTimeout = 10;
        protected IWebDriver webDriver;
        public PageBase(IWebDriver webDriver, int secondTimeout)
        {            
            this.secondTimeout = secondTimeout;
            this.webDriver = webDriver;
        }

        protected virtual void WaitUntilValueIsPopulated(By element)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(secondTimeout));
            try { 
                wait.Until(d => webDriver.FindElement(@element).GetAttribute("value") != "");
            }
            catch(Exception e)
            {
                Loggers.Log("Wait unitl element value changed failed: " + e.Message, "Error");
            }
        }
       
        protected virtual void EnterInputText(By element, string textValue)
        {
            webDriver.FindElement(element).Clear();
            webDriver.FindElement(element).SendKeys(textValue);
        }

        protected virtual void SwitchToIFrame(By element)
        {
            webDriver.SwitchTo().Frame(webDriver.FindElement(element));
        }
        protected virtual void ClickButton(By element)
        {
            webDriver.FindElement(element).Click();
        }
        protected virtual void SwitchToDefaultContent()
        {
            webDriver.SwitchTo().DefaultContent();
        }
        protected virtual void SelectValueFromDropDown(By element, string selectedValue)
        {
            var dropDownList = webDriver.FindElement(element);
            dropDownList.Click();
            var select = new SelectElement(dropDownList);
            select.SelectByValue(selectedValue);
        }
        protected virtual string GetTextValueFromElement(By element)
        {
            return webDriver.FindElement(element).GetAttribute("value");
        }
    }
}
