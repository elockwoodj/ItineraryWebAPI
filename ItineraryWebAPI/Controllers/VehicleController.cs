using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItineraryWebAPI.Controllers
{
    public class VehicleController : ApiController
    {
                private NCPCDAO _ncpcDAO;
        public VehicleController()
        {
            _ncpcDAO = new NCPCDAO();
        }

        public HttpResponseMessage GetVehicle()
        {
            IEnumerable<Vehicle> vehicle =
                _ncpcDAO.GetVehicle();
            if (vehicle == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, vehicle);
                return response;
            }
        }

        public HttpResponseMessage GetVehicle(int id)
        {
            Vehicle vehicle =
                _ncpcDAO.GetVehicle(id);
            if (vehicle == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, vehicle);
                return response;
            }
        }

        public HttpResponseMessage DeleteVehicle(int id)
        {
            Vehicle vehicle =
                _ncpcDAO.GetVehicle(id);
            if (vehicle == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound, id);
                return response;
            }
            else
            {
                if (_ncpcDAO.DeleteVehicle(vehicle))
                {
                    HttpResponseMessage response =
                        Request.CreateResponse(HttpStatusCode.OK, vehicle);
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


        public HttpResponseMessage PostVehicle(Vehicle vehicle)
        {
            if (_ncpcDAO.AddVehicle(vehicle) == true)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.Created, vehicle);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/Vehicle/" + vehicle.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotAcceptable, vehicle);
                return response;
            }
        }
        public HttpResponseMessage PutVehicle(Vehicle vehicle)
        {
            if (_ncpcDAO.EditVehicle(vehicle) == true)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Accepted, vehicle);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/Vehicle/" + vehicle.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotAcceptable, vehicle);
                return response;
            }
        }


    }
}
