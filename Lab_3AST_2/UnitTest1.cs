using RestSharp;
using System.Net;
using RestSharp.Serialization.Json;
using RestSharp.Authenticators;

namespace Lab_3AST_2
{
    public class Tests
    {
        private string matchId = "585962936";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckSeccessfullResponse_WhenGetRequestHeroes()
        {
            // arrange
            RestClient client = new RestClient("https://api.opendota.com/api/");
            RestRequest request = new RestRequest("/heroes", Method.GET);
            // act
            IRestResponse response = client.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckSeccessfullResponse_WhenExecutePlayerRefreshPostRequest()
        {
            // arrange
            RestClient client = new RestClient("https://api.opendota.com/api/");
            RestRequest request = new RestRequest($"request/{matchId}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            // act
            IRestResponse response = client.Execute(request);
            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}