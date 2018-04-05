using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace ItineraryWebAPI.Controllers
{
    public class BidHistoryController : ApiController
    {
        private AuctionDAO _listingService;

        public BidHistoryController()
        {
            _listingService = new AuctionDAO();
        }

        public HttpResponseMessage GetBidHistory(int accountId)
        {
            IEnumerable<listingBid> _bids =
                _listingService.GetBidHistory(accountId);
            if (_bids == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, _bids);
                return response;
            }
        }




        //// GET: BidHistory
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: BidHistory/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: BidHistory/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: BidHistory/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: BidHistory/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: BidHistory/Edit/5
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

        //// GET: BidHistory/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: BidHistory/Delete/5
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
