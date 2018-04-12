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

        [HttpGet]
        public HttpResponseMessage GetListings(int id)
        {
            IEnumerable<AuctionBEANS> listing =  _listingService.GetListings(id);

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


       

        public HttpResponseMessage GetListingHistory(int accountId)
        {
            IEnumerable<Listings> _listHistory =
                _listingService.GetListingHistory(accountId);
            if (_listHistory == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, _listHistory);
                return response;
            }
        }

       

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
