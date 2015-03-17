using RestSharp;
using SparkAPI.Model;
using SparkAPI.Security;
using System;

namespace SparkAPI
{
    public class Spark
    {
        SparkRest api;
        public string Core { get; set; }
        public string AccessToken { get; private set; }

        //public Spark(string core)
        //{
        //    Core = core;
        //    //var provider = new RegistrySparkAccessTokenProvider();
        //    //if (provider.GetAccessToken() == null)
        //    //    throw new ApplicationException("AccessToken not found! To create AccessToken use Spark.CreateAccessToken(username, password).");
        //    api = new SparkRest(provider.GetAccessToken());
        //}
        public Spark(string core, string accessToken)
        {
            Core = core;

            api = new SparkRest(accessToken);
        }
        public Spark(string core, ISparkAccessTokenProvider accessTokenProvider)
        {
            Core = core;
            api = new SparkRest(accessTokenProvider.GetAccessToken());
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

        public static string CreateAccessToken(string username, string password)
        {
            var api = new SparkRest(null);
            var request = new RestRequest("oauth/token", Method.POST);
            request.AddParameter("grant_type", "password");
            request.AddParameter("client_id", "spark");
            request.AddParameter("client_secret", "spark");
            request.AddParameter("username", username);
            request.AddParameter("password", password);

            var result = api.Execute<AuthResponse>(request, username, password);
            return result.Access_Token;
        }
    }
}
