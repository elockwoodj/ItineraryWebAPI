using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;


namespace ItineraryWebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private AuctionDAO _listingService;

        public CategoryController()
        {
            _listingService = new AuctionDAO();
        }


        [HttpGet]
        public HttpResponseMessage GetListingCategories()
        {
            IEnumerable<listing_Category> _Category =
                _listingService.GetCategories();
            if (_Category == null)
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.NotFound);
                return response;
            }
            else
            {
                HttpResponseMessage response =
                    Request.CreateResponse(HttpStatusCode.OK, _Category);
                return response;
            }
        }


        public HttpResponseMessage GetListings(int id)
        {
            IEnumerable<AuctionBEANS> listing = _listingService.GetListings(id);

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


        //        // GET: Category
        //        public ActionResult Index()
        //        {
        //            return View();
        //        }

        //        // GET: Category/Details/5
        //        public ActionResult Details(int id)
        //        {
        //            return View();
        //        }

        //        // GET: Category/Create
        //        public ActionResult Create()
        //        {
        //            return View();
        //        }

        //        // POST: Category/Create

        //        public ActionResult Create(FormCollection collection)
        //        {
        //            try
        //            {
        //                // TODO: Add insert logic here

        //                return RedirectToAction("Index");
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }

        //        // GET: Category/Edit/5
        //        public ActionResult Edit(int id)
        //        {
        //            return View();
        //        }

        //        // POST: Category/Edit/5
        //        public ActionResult Edit(int id, FormCollection collection)
        //        {
        //            try
        //            {
        //                // TODO: Add update logic here

        //                return RedirectToAction("Index");
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }

        //        // GET: Category/Delete/5
        //        public ActionResult Delete(int id)
        //        {
        //            return View();
        //        }

        //        // POST: Category/Delete/5
        //        public ActionResult Delete(int id, FormCollection collection)
        //        {
        //            try
        //            {
        //                // TODO: Add delete logic here

        //                return RedirectToAction("Index");
        //            }
        //            catch
        //            {
        //                return View();
        //            }
        //        }
        //    }
    }
}
