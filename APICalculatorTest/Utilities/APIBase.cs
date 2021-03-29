using log4net;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using WebCalculatorTest.Utilities;

namespace APICalculatorTest.Utilities
{
    public class APIBase
    {
        private string url="";

        public APIBase(string baseUrl, string authToken)
        {
            url = baseUrl;
        }
        private RestRequest restRequest = new RestRequest();   
        protected virtual void AddRequestHeader(string key, string value)
        {
            restRequest.AddOrUpdateParameter(key, value, ParameterType.HttpHeader);
        }

        protected virtual void SetAuthenticationHeader(string token)
        {
            restRequest.AddOrUpdateParameter("x-functions-key", token, ParameterType.HttpHeader);
        }

        protected virtual void AddJsonBody(string json)
        {
            restRequest.AddParameter("Application/Json", json, ParameterType.RequestBody);
        }

        protected virtual void SetMethod(Method method)
        {
            restRequest.Method = method;
        }

        protected virtual IRestResponse ExecuteNonFunctionCheck<T>()
        {
            try
            {
                var restClient = new RestClient(url);
                var response = restClient.Execute(restRequest);               
                Loggers.Log("The response status code: " + response.StatusCode + ", content:" + response.Content);
                return response;
            }
            catch (Exception e)
            {
                Loggers.Log("Send a request failed with error message:" + e.Message, "Error");
                throw new Exception("Send a request failed with error message:" + e.Message);
            }
        }

        protected virtual T Execute<T>()
        {
            try
            {
                var restClient = new RestClient(url);
                var response = restClient.Execute(restRequest);               
                Loggers.Log(" the response status code: " + response.StatusCode + ", content:" + response.Content);
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception e)
            {
                Loggers.Log("Send a request failed with error message:" + e.Message,"Error");
                throw new Exception("Send a request failed with error message:" + e.Message);
            }
        }
    }
}
