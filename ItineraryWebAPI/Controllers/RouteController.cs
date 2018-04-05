using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItineraryWebAPI.Controllers
{
    public class RouteController : ApiController
    {
        private NCPCDAO _ncpcDAO;
        public RouteController()
        {
            _ncpcDAO = new NCPCDAO();
        }

        public HttpResponseMessage GetRoute()
        {
            IEnumerable<Route> route =
                _ncpcDAO.GetRoute();
            if (route == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, route);
                return response;
            }
        }

        public HttpResponseMessage GetRoute(int id)
        {
            Route route =
                _ncpcDAO.GetRoute(id);
            if (route == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, route);
                return response;
            }
        }

        public HttpResponseMessage DeleteRoute(int id)
        {
            Route route =
                _ncpcDAO.GetRoute(id);
            if (route == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound, id);
                return response;
            }
            else
            {
                if (_ncpcDAO.DeleteRoute(route))
                {
                    HttpResponseMessage response =
                        Request.CreateResponse(HttpStatusCode.OK, route);
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


        public HttpResponseMessage PostRoute(Route route)
        {
            if (_ncpcDAO.AddRoute(route) == true)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.Created, route);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/Vehicle/" + route.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotAcceptable, route);
                return response;
            }
        }
        public HttpResponseMessage PutRoute(Route route)
        {
            if (_ncpcDAO.EditRoute(route) == true)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted, route);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/Route/" + route.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotAcceptable, route);
                return response;
            }
        }



    }
}
