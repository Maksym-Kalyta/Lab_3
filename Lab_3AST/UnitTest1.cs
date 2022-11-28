using RestSharp;
using System.Net;
using RestSharp.Serialization.Json;
using RestSharp.Authenticators;
using Lab3.Models;

namespace Lab_3AST
{
    public class Tests1
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckSeccessfullResponse_WhenGetBookIds()
        {
            //arrange 
            RestClient client = new RestClient("https://restful-booker.herokuapp.com/");
            RestRequest request = new RestRequest("booking", Method.GET);
            //act
            IRestResponse response = client.Execute(request);
            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckSeccessfullResponse_WhenCreateBook()
        {
            //arrange 
            RestClient client = new RestClient("https://restful-booker.herokuapp.com/");
            RestRequest request = new RestRequest("booking", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new Booking()
            {
                firstname = "Brown",
                lastname = "James",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new Bookingdates()
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast"
            });
            //act
            IRestResponse response = client.Execute(request);
            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckSeccessfullResponse_WhenUpdateBook()
        {
            //arrange 
            RestClient client = new RestClient("https://restful-booker.herokuapp.com/");
            RestRequest request = new RestRequest("booking", Method.GET);
            IRestResponse response = client.Execute(request);
            var bookings = new JsonDeserializer().Deserialize<List<Bookingid>>(response);
            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");
            request = new RestRequest($"booking/" + bookings[2].bookingId, Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new Booking()
            {
                firstname = "James",
                lastname = "James",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new Bookingdates()
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast"
            });
            //act
            response = client.Execute(request);
            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void CheckSeccessfullResponse_WhenDeleteBook()
        {
            //arrange 
            RestClient client = new RestClient("https://restful-booker.herokuapp.com/");
            RestRequest request = new RestRequest("booking", Method.GET);
            IRestResponse response = client.Execute(request);
            var bookings = new JsonDeserializer().Deserialize<List<Bookingid>>(response);
            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");
            request = new RestRequest("booking/" + bookings[2].bookingId, Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            //act
            response = client.Execute(request);
            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }
    }
}