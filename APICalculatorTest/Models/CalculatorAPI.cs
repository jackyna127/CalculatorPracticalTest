using APICalculatorTest.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace APICalculatorTest.Models
{
    public class CalculatorAPIRequest
    {
        public int LeftNumber;
        public int RightNumber;
        public string Operator;
    }
    public class CalculatorAPIResponse
    {
        public int calculateResult;
    }

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
            
            SetAuthenticationHeader(authToken);           
            SetMethod(Method.POST);
            AddJsonBody(JsonConvert.SerializeObject(requestBody));
            CalculatorAPIResponse response = Execute<CalculatorAPIResponse>();
            //When nothing return
            if(response == null)
                throw new Exception($"No response from server, please check restquest: {requestBody}.");
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
            SetMethod(Method.POST);
            AddJsonBody(JsonConvert.SerializeObject(requestBody));
            var response = ExecuteNonFunctionCheck<CalculatorAPIRequest>();
            //When nothing return
            if (response == null)
                throw new Exception($"No response from server, please check restquest: {requestBody}.");
            return response;
        }

        public IRestResponse ExecuteCalculate<T>(T restRequestBody)
        {
            SetAuthenticationHeader(authToken);
            SetMethod(Method.POST);
            AddJsonBody(JsonConvert.SerializeObject(restRequestBody));
            var response = ExecuteNonFunctionCheck<T>();
            //When nothing return
            if (response == null)
                throw new Exception($"No response data from server, please check restquest: {restRequestBody}.");
            return response;
        }
       
    }


}
