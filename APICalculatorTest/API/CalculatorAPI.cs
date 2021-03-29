using APICalculatorTest.Models;
using APICalculatorTest.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WebCalculatorTest.Utilities;

namespace APICalculatorTest.API
{    
    public class CalculatorAPI : APIBase
    {
        string authToken = "";
        
        public CalculatorAPI(string baseUrl,string authToken):base(baseUrl, authToken)
        {
            this.authToken = authToken;
        }
      
        public int ExecuteCalculate(int inputLeftNumber, int inputRightNumber, string selectedOperator)
        {
            CalculatorAPIRequest requestBody = new CalculatorAPIRequest
            {
                LeftNumber = inputLeftNumber,
                RightNumber = inputRightNumber,
                Operator = selectedOperator
            };
            Loggers.Log($"inputLeftNumber:{inputLeftNumber}, selectOperator: {selectedOperator},inputLeftNumber: {inputRightNumber}");
            SetAuthenticationHeader(authToken);           
            SetMethod(Method.POST);
            AddJsonBody(JsonConvert.SerializeObject(requestBody));
            CalculatorAPIResponse response = Execute<CalculatorAPIResponse>();
            //When nothing return
            if (response == null)
            {
                Loggers.Log("No response data from server, please check restquest:" + requestBody.ToString(), "Error");                
            }
            return response.calculateResult;
        }

        
        public IRestResponse ExecuteCalculate(int inputLeftNumber, int inputRightNumber, string selectedOperator, bool authTokenFlag)
        {
            CalculatorAPIRequest requestBody = new CalculatorAPIRequest
            {
                LeftNumber = inputLeftNumber,
                RightNumber = inputRightNumber,
                Operator = selectedOperator
            };
            Loggers.Log($"inputLeftNumber:{inputLeftNumber}, selectOperator: {selectedOperator},inputLeftNumber: {inputRightNumber}");
            SetMethod(Method.POST);
            AddJsonBody(JsonConvert.SerializeObject(requestBody));
            var response = ExecuteNonFunctionCheck<CalculatorAPIRequest>();
            //When nothing return
            if (response == null)
            {
                Loggers.Log("No response data from server, please check restquest:" + requestBody.ToString(), "Error");                
            }
            return response;
        }

        public IRestResponse ExecuteCalculate<T>(T requestBody)
        {
            SetAuthenticationHeader(authToken);
            SetMethod(Method.POST);
            AddJsonBody(JsonConvert.SerializeObject(requestBody));
            Loggers.Log($"Request Body:{requestBody}");
            var response = ExecuteNonFunctionCheck<T>();
            //When nothing return
            if (response == null)
            {
                Loggers.Log("No response data from server, please check restquest:" + requestBody.ToString(), "Error");               
            }
            return response;
        }

    }


}
