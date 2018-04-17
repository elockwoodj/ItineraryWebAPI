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
        public HttpResponseMessage auctionPrice(int itemId)
        {
            double auctionPrice = _listingService.GetAuctionPrice(itemId);
            HttpResponseMessage response =
                Request.CreateResponse(HttpStatusCode.OK, auctionPrice);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage postListing(bidBEANS newBi_d)
        {
            if (_listingService.MakeBid(newBi_d) == true)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.Created, newBi_d);
                response.Headers.Location =
                    new Uri(Request.RequestUri, "/api/Bid/" + newBi_d.Id.ToString());
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotAcceptable, newBi_d);
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
