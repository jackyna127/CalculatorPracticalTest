using log4net;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

                log.Info(DateTime.Now + ":send a request to endpoint " + url + ", and paramenters:" + restRequest.Parameters.ToString());
                log.Info(DateTime.Now + " the response status code: " + response.StatusCode + ", content:" + response.Content);

                return response;
            }
            catch (Exception e)
            {
                throw new Exception("Send a request failed with error message:" + e.Message);
            }
        }

        protected virtual T Execute<T>()
        {
            try
            {
                var restClient = new RestClient(url);
                var response = restClient.Execute(restRequest);

                log.Info(DateTime.Now + ":send a request to endpoint " + url + ", and paramenters:" + restRequest.Parameters.ToString());
                log.Info(DateTime.Now + " the response status code: " + response.StatusCode + ", content:" + response.Content);

                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception e)
            {
                throw new Exception("Send a request failed with error message:" + e.Message);
            }
        }
    }
}
