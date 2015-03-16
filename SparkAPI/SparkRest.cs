using RestSharp;
using System;

namespace SparkAPI
{
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
