﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ItineraryWebAPI.Controllers
{
    public class LoginController : ApiController
    {

        public LoginController()
        {

        }
        public HttpResponseMessage PostLogin(Auth myAuth)
        {

            if (myAuth.username == "mark201" && myAuth.password == "notsure")
            {
                myAuth.authenticated = true;
                myAuth.role = 1;
                myAuth.name = "Mark Godson";

                HttpResponseMessage response =
                Request.CreateResponse(HttpStatusCode.Accepted, myAuth);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/login/" + myAuth.role.ToString());
                return response;

            }
            else if (myAuth.username == "emma103" && myAuth.password == "prettysure")
            {
                myAuth.authenticated = true;
                myAuth.role = 2;
                myAuth.name = "Emma Smith";
                HttpResponseMessage response =
                Request.CreateResponse(HttpStatusCode.Accepted, myAuth);
                response.Headers.Location = new Uri(Request.RequestUri, "/api/login/" + myAuth.role.ToString());
                return response;

                //                return 2;
            }
            //else
            //{
            //    //myResponse.authenticated = false;
            //myResponse.role = "NoRole";
            //myAuth.role = 0;
            //HttpResponseMessage response =
            //Request.CreateResponse(HttpStatusCode.Created, myAuth);
            ////response.Headers.Location = new Uri(Request.RequestUri, "/api/ITS/" + itinerary.Id.ToString());
            //return response;
            //                return 0;
            else
            {
                myAuth.authenticated = false;
                myAuth.role = 0;
                myAuth.name = "";
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, myAuth);
                return response;
            }
        }

    }
    public class Auth
    {
        public string username;
        public string password;
        public string name;
        public bool authenticated;
        public int role;
    }
    }
//}