using RestSharp;
using SparkAPI.Model;
using SparkAPI.Security;

namespace SparkAPI
{
    public class Spark
    {
        SparkRest api;
        public string Core { get; set; }
        public string AccessToken { get; private set; }

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
    }
}
