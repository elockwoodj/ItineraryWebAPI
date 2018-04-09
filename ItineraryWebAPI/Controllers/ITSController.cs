using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;

namespace ItineraryWebAPI.Controllers
{
    public class ITSController : ApiController
    {
        private ItineraryDAO _itineraryDAO;
        public ITSController()
        {
            _itineraryDAO = new ItineraryDAO();
        }

        public HttpResponseMessage GetItinerary()
        {
            int i = 0;
            System.Threading.Thread.Sleep(100);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound);
            while (i<2){
                IEnumerable<Itinerary> itineraries =
                _itineraryDAO.GetItineraryList();

                //string URL = "http://webteach_net.hallam.shu.ac.uk/cmsmr2/api/its";
                //string urlParameters = "";
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(URL);

                //// Add an Accept header for JSON format.
                //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

                //// List data response.
                //HttpResponseMessage response1 = client.GetAsync(urlParameters).Result;  // Blocking call!
                //if (response1.IsSuccessStatusCode)
                //{
                //    // Parse the response body. Blocking!
                //    var dataObjects = response1.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
                //    foreach (var d in dataObjects)
                //    {
                //        //Console.WriteLine("{0}", d.Name);
                //    }
                //}
                //else
                //{
                //    //Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                //}

            if (itineraries == null)
            {
                response =
                    Request.CreateResponse(HttpStatusCode.NotFound);
                //return response;
            }
            else
            {
                response =
                    Request.CreateResponse(HttpStatusCode.OK, itineraries);
                //return response;
            }
            i++;
            }
            return response;
        }

        public HttpResponseMessage GetItinerary(int id)
        {
            Itinerary itineraries =
                _itineraryDAO.GetItinerary(id);
            if (itineraries == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, itineraries);
                return response;
            }
        }

        public HttpResponseMessage DeleteItinerary(int id)
        {
            Itinerary itinerary =
                _itineraryDAO.GetItinerary(id);
            if (itinerary == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound, id);
                return response;
            }
            else
            {
                if (_itineraryDAO.DeleteItinerary(itinerary))
                {
                    HttpResponseMessage response =
                        Request.CreateResponse(HttpStatusCode.OK, itinerary);
                    return response;
                }
                else
                {
                    HttpResponseMessage response =
                        Request.CreateResponse(HttpStatusCode.InternalServerError, id);
                    return response;
                }
            }
        }


        public HttpResponseMessage PostItinerary(Itinerary itinerary)
        {
            if (_itineraryDAO.AddItinerary(itinerary) == true)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.Created, itinerary);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/ITS/" + itinerary.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotAcceptable, itinerary);
                return response;
            }
        }

        public HttpResponseMessage PutItinerary(Itinerary itinerary)
        {
            if (_itineraryDAO.EditItinerary(itinerary) == true)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted, itinerary);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/ITS/" + itinerary.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotAcceptable, itinerary);
                return response;
            }
        }


        // GET: api/ITS
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/ITS/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/ITS
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/ITS/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ITS/5
        //public void Delete(int id)
        //{
        //}
    }
}
