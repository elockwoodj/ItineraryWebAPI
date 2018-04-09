using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItineraryWebAPI.Controllers
{
    public class BookingController : ApiController
{
        private NCPCDAO _ncpcDAO;
        public BookingController()
        {
            _ncpcDAO = new NCPCDAO();
        }

        public HttpResponseMessage GetBooking()
        {
            IEnumerable<Booking> booking =
                _ncpcDAO.GetBooking();
            if (booking == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, booking);
                return response;
            }
        }

        public HttpResponseMessage GetBooking(int id)
        {
            Booking booking=
                _ncpcDAO.GetBooking(id);
            if (booking == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, booking);
                return response;
            }
        }

        public HttpResponseMessage DeleteBooking(int id)
        {
            Booking booking =
                _ncpcDAO.GetBooking(id);
            if (booking == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound, id);
                return response;
            }
            else
            {
                if (_ncpcDAO.DeleteBooking(booking))
                {
                    HttpResponseMessage response =
                        Request.CreateResponse(HttpStatusCode.OK, booking);
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


        public HttpResponseMessage PostBooking(Booking booking)
        {
            if (_ncpcDAO.AddBooking(booking) == true)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.Created, booking);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/Booking/" + booking.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotAcceptable, booking);
                return response;
            }
        }
        public HttpResponseMessage PutBooking(Booking booking)
        {
            if (_ncpcDAO.EditBooking(booking) == true)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted, booking);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/Booking/" + booking.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotAcceptable, booking);
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
