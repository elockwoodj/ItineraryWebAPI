using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Net.Http.Headers;


namespace ItineraryWebAPI.Controllers
{
    public class BidController : ApiController
    {
        private AuctionDAO _listingService;

        public BidController()
        {
            _listingService = new AuctionDAO();
        }

        [HttpGet]
        public HttpResponseMessage auctionPrice(int Id)
        {
            double Price = _listingService.GetAuctionPrice(Id);
            if (Price > 0)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, Price);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotAcceptable, Price);
                return response;
            }
        }

        [HttpPost]
        public HttpResponseMessage postBidding(bidBEANS newBidObject)
        {
            if (_listingService.MakeBid(newBidObject) == true)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.Created, newBidObject);
                response.Headers.Location =
                    new Uri(Request.RequestUri, "/api/Bid/" + newBidObject.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotAcceptable, newBidObject);
                return response;
            }
        }

        //// GET: Bid/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Bid/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Bid/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Bid/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
