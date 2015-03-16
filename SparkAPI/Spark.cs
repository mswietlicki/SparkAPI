using RestSharp;
using SparkAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkAPI
{
    public class Spark
    {
        SparkRest api;
        public string Core { get; set; }
        public Spark(string core)
        {
            Core = core;
            api = new SparkRest("");
        }

        public int GetVariableInt(string name)
        {
            var request = new RestRequest(string.Format("v1/devices/{0}/{1}", Core, name), Method.GET);
            var result = api.Execute<VariableResponse>(request);
            return int.Parse(result.Result);
        }

        public string GetVariableString(string name)
        {
            var request = new RestRequest(string.Format("v1/devices/{0}/{1}", Core, name), Method.GET);
            var result = api.Execute<VariableResponse>(request);
            return result.Result;
        }

        public int CallFunction(string name, string args = "")
        {
            var request = new RestRequest(string.Format("v1/devices/{0}/{1}", Core, name), Method.POST);
            if (string.IsNullOrWhiteSpace(args))
                args = string.Empty;
            request.AddParameter("args", args);
            var result = api.Execute<FunctionResponse>(request);
            return int.Parse(result.Return_Value);
        }
    }

    public class SparkRest
    {
        public string BaseUrl { get; private set; }
        public string AccessToken { get; private set; }
        public SparkRest(string accessToken)
        {
            AccessToken = accessToken;
            BaseUrl = "https://api.spark.io";
        }

        public T Execute<T>(IRestRequest request, string login = null, string password = null) where T : new()
        {
            var client = new RestClient(BaseUrl);
            if (login != null && password != null)
                client.Authenticator = new HttpBasicAuthenticator(login, password);
            else
                request.AddParameter("access_token", AccessToken);

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                throw new ApplicationException(message, response.ErrorException);
            }
            return response.Data;
        }
    }
}
