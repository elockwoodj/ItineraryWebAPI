using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Net.Http.Headers;


namespace ItineraryWebAPI
{
    public class ListingController : ApiController
    {
        private AuctionDAO _listingService;

        public ListingController()
        {
            _listingService = new AuctionDAO();
        }

        //Accepts the id for a listing and passes the value onto the GetSingularListing method in the DAO, returning a listing from the Listings table with specified Id
        [HttpGet]
        public HttpResponseMessage getSingularListing (int id)
        {
            Listings listing = _listingService.GetSingularListing(id);
            if (listing == null)
            {
                HttpResponseMessage response =
                 Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                Request.CreateResponse(HttpStatusCode.OK, listing);
                return response;
            }
        }
       
        //Accepts an object with the needed values to post to the Listings table - passes to the AddListing method in the DAO
        [HttpPost]
        public HttpResponseMessage postListing(AuctionBEANS newListing)
        {
            if (_listingService.AddListing(newListing) == true)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.Created, newListing);
                response.Headers.Location =
                    new Uri(Request.RequestUri, "/api/Listing/" + newListing.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotAcceptable, newListing);
                return response;
            }
        }
        
        //Accepts an edited listing - passes to the EditListing method in the DAO which will change the values in a listing
        [HttpPut]
        public HttpResponseMessage putListing(AuctionBEANS listingChange)
        {
            if (_listingService.EditListing(listingChange) == true)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.Created, listingChange);
                response.Headers.Location =
                    new Uri(Request.RequestUri, "/api/Listing/" + listingChange.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotAcceptable, listingChange);
                return response;
            }
        }

        //Accepts the id for a listing to be deleted, checks this listing exists using the GetSingularListing method - if it does it will then delete the listing and respond 
        [HttpDelete]
        public HttpResponseMessage deleteListing (int id)
        {
            Listings _doomedListing = _listingService.GetSingularListing(id);
            if(_doomedListing == null)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NotFound, id);
                return response;
            }
            else
            {
                 if (_listingService.DeleteListing(_doomedListing))
                 {
                     HttpResponseMessage response =
                         Request.CreateResponse(HttpStatusCode.OK, _doomedListing);
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

        //// GET: api/Listing
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Listing/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Listing
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Listing/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Listing/5
        //public void Delete(int id)
        //{
        //}
    }
}
