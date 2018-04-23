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
    public class ListingHistoryController : ApiController
    {
        private AuctionDAO _listingService;

        public ListingHistoryController()
        {
            _listingService = new AuctionDAO();
        }

        //Accepts the accountId for a specified account, passing this to the GetListingHistory method in the DAO - 
        //This will return all the Listings posted by the specified accountId
        [HttpGet]
        public HttpResponseMessage GetListingHistory(int accountId)
        {
            IEnumerable<AuctionBEANS> _listHistory =
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


        //// GET: ListingHistory
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: ListingHistory/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: ListingHistory/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ListingHistory/Create
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

        //// GET: ListingHistory/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ListingHistory/Edit/5
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

        //// GET: ListingHistory/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ListingHistory/Delete/5
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
