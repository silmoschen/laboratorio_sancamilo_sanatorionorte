using RestSharp;
using RestSharp.Authenticators;
using System;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.rest
{
    class RestApi
    {
        public String Post(string RestURL, string metodo, Object json)
        {
            var client = new RestClient(RestURL + metodo);
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(json);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public String Post(string RestURL, string metodo, string apikey, string key, Object json)
        {
            var client = new RestClient(RestURL + metodo);
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader(apikey, key);
            request.AddJsonBody(json);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public String Post(string RestURL, string metodo, string apikey, string key, Object json, string userName, string password)
        {
            var client = new RestClient(RestURL + metodo);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader(apikey, key);
            request.AddJsonBody(json);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public String PostBearer(string RestURL, string metodo, string token, Object json)
        {
            var client = new RestClient(RestURL + metodo);
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);

            request.AddJsonBody(json);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }


        public String Get(string RestURL, string metodo, string apikey, string key, Object json, string userName, string password)
        {
            var client = new RestClient(RestURL + metodo);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader(apikey, key);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public string Put(string RestURL, string metodo, string apikey, string key, Object json, string userName, string password)
        {
            var client = new RestClient(RestURL + metodo);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);
            var request = new RestRequest(Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader(apikey, key);
            request.AddJsonBody(json);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

    }
}
