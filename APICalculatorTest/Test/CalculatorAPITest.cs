using APICalculatorTest.Models;
using APICalculatorTest.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
#if NETCOREAPP
using System.IO;
using System.Net;
using System.Reflection;
#endif
using System.Text;

namespace APICalculatorTest.Test
{
    [TestClass]
    [TestCategory("API")]
    public class CalculatorAPITest
    {
        private CalculatorAPI calculatorAPI;
        private string authToken = "";
        [TestInitialize]
        public void Setup()
        {
            #if NETCOREAPP
            string configFile = $"{Assembly.GetExecutingAssembly().Location}.config";
            string outputConfigFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
            File.Copy(configFile, outputConfigFile, true);
            #endif
            // string path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
            string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            authToken = ConfigurationManager.AppSettings["AuthToken"];
            calculatorAPI = new CalculatorAPI(baseUrl, authToken);
        }

        [TestMethod]
        //Test positive scenarios
        [DataRow(999, 999, "+", 1998)]// the maximum number where web page can input
        [DataRow(-99, -99, "+", -198)]// the minimum number where web page can input
        [DataRow(-99, 999, "+", 900)]//one positive and one negative number       
        [DataRow(999, -99, "-", 1098)]// the maximum number and mininum number where web page can input
        [DataRow(-99, 999, "-", -1098)]// the maximum number and mininum number where web page can input
        [DataRow(1, 3, "-", 900)]//one positive and one negative number       
        [DataRow(999, 999, "*", 998001)]// the maximum number where web page can input
        [DataRow(-99, -99, "*", 9801)]// the mininum number where web page can input
        [DataRow(1, -3, "*", -3)]//one positive and one negative number       
        [DataRow(999, -99, "/", -999/99)]// the maximum number where web page can input
        [DataRow(-99, 999, "/", -99/999)]// the mininum number where web page can input
        [DataRow(8, -2, "/", -4)]//one positive and one negative number
        [DataRow(-8, -2, "/", 4)]//two negative numbers
        [DataRow(9, 3, "/", 3)]//two positive numbers
        [DataRow(0, 3, "/", 0)]//0 with one positive number
        [DataRow(0, -5, "/", 0)]//0 with one negative number
        public void CalculateAPI_PositiveTests(int leftNumber, int rightNumber, string calculateOperator, int expectedResult)
        {
            var actualResult = calculatorAPI.ExecuteCalculate(leftNumber, rightNumber, calculateOperator);
            Assert.AreEqual(expectedResult, actualResult,
                $"Expected result:{expectedResult}, Calculator API returned actual result: {actualResult}.");
        }

        [TestMethod]
        //Test boundary scenarios
        [DataRow(int.MaxValue, int.MaxValue, "+", int.MaxValue)]
        [DataRow(int.MinValue, int.MinValue, "+", int.MinValue)]
        [DataRow(int.MaxValue, int.MinValue, "-", int.MaxValue)]
        [DataRow(int.MinValue, int.MaxValue, "-", int.MinValue)]
        [DataRow(int.MaxValue, int.MinValue, "*", int.MinValue)]
        [DataRow(int.MinValue, int.MinValue, "*", int.MaxValue)]
        public void CalculateAPI_BoundaryTests(int leftNumber, int rightNumber, string calculateOperator, int expectedResult)
        {
            var actualResult = calculatorAPI.ExecuteCalculate(leftNumber, rightNumber, calculateOperator);
            Assert.AreEqual(expectedResult, actualResult,
                $"Expected result:{expectedResult}, Calculator API returned actual result: {actualResult}.");
        }

        [TestMethod]
        //Test nagetive scenarios
        [DataRow(1, 0, "/", 0)]
        public void CalculateAPI_NegativeTests(int leftNumber, int rightNumber, string calculateOperator, int expectedResult)
        {
            var actualResult = calculatorAPI.ExecuteCalculate(leftNumber, rightNumber, calculateOperator);
            Assert.AreEqual(expectedResult, actualResult,
                $"Expected result:{expectedResult},Calculator API returned actual result:{actualResult}.");
        }
        [TestMethod]
        //Test no auth token 
        [DataRow(1, 1, "/", HttpStatusCode.Unauthorized)]
        public void CalculateAPI_NoAuthToken(int leftNumber, int rightNumber, string calculateOperator, HttpStatusCode expectedResult)
        {
            var actualResponse = calculatorAPI.ExecuteCalculate(leftNumber, rightNumber, calculateOperator,false);
            Assert.AreEqual(expectedResult, actualResponse.StatusCode,
                $"Expected response status code: {expectedResult}, Calculator API returned actual response status code: {actualResponse.StatusCode}.");
        }

        [TestMethod]
        //Input char as left number 
        [DataRow('O', 1, "/", HttpStatusCode.InternalServerError)]
        public void CalculateAPI_InputChar(char leftNumber, int rightNumber, string calculateOperator, HttpStatusCode expectedResult)
        {
            object request = new 
            {
                LeftNumber=leftNumber,
                RightNumber=rightNumber,
                Operator = calculateOperator
            };
            var actualResponse = calculatorAPI.ExecuteCalculate<object>(request);           
            Assert.AreEqual(expectedResult, actualResponse.StatusCode,
                $"Expected response status code: {expectedResult},Calculator API returned actual response status code: {actualResponse.StatusCode}.");
        }

        [TestMethod]
        //Input char as left number 
        [DataRow(2, 1, HttpStatusCode.InternalServerError)]
        public void CalculateAPI_NoOperator(int leftNumber, int rightNumber, HttpStatusCode expectedResult)
        {
            object request = new
            {
                LeftNumber = leftNumber,
                RightNumber = rightNumber
            };
            var actualResponse = calculatorAPI.ExecuteCalculate<object>(request);            
            Assert.AreEqual(expectedResult, actualResponse.StatusCode,
                $"Expected response status code: {expectedResult}, Calculator API returned actual response status code: {actualResponse.StatusCode}.");
        }

        [TestMethod]
        //Input char as left number 
        [DataRow(26.12, 1, HttpStatusCode.InternalServerError)]
        public void CalculateAPI_Double(double leftNumber, int rightNumber, HttpStatusCode expectedResult)
        {
            object request = new
            {
                LeftNumber = leftNumber,
                RightNumber = rightNumber
            };
            var actualResponse = calculatorAPI.ExecuteCalculate<object>(request);            
            Assert.AreEqual(expectedResult, actualResponse.StatusCode,
                $"Expected response status code: {expectedResult}, Calculator API returned actual response status code: {actualResponse.StatusCode}.");
        }

        [TestCleanup]
        public void TearDown()
        {

        }
    }
}
