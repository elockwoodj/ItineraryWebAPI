using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity.Validation;


namespace ItineraryWebAPI
{
    public class AuctionDAO : AuctionIDAO
    {
        private a9027410Entities _context;
        public AuctionDAO()
        {
            _context = new a9027410Entities();
        }
        public IList<AuctionBEANS> GetListings(int category)
        {
            //Place variables from the tables into the beans
            IQueryable<AuctionBEANS> _listingBEANs = from list in _context.Listings
                                                     from categ in _context.listing_Category
                                                     where list.category == categ.Id
                                                     where categ.Id == category
                                                     select new AuctionBEANS
                                                     {
                                                         Id = list.Id,
                                                         description = list.description,
                                                         image = list.image,
                                                         category = categ.category,
                                                         priceBuy = list.priceBuy,
                                                         startDate = list.startDate,
                                                         accountId = list.accountId,
                                                         categoryId = list.category

                                                     };

            return _listingBEANs.ToList<AuctionBEANS>();

        }

        //Get one particular listing method

        public Listings GetSingularListing(int Id)
        {
            IQueryable<Listings> _listingBEAN = from list in _context.Listings
                                                where list.Id == Id
                                                select list;
            return _listingBEAN.ToList<Listings>().First();
        }

        //Listing check

        public bool ListingCheck(int Id)
        {
            IQueryable<int> idList = from lists
                                     in _context.Listings
                                     select lists.Id;
            if (idList.ToList<int>().Contains(Id))
            { return true; }
            else { return false; }
        }

        //Add new listing funcionality
        public bool AddListing(AuctionBEANS _listingBEAN)
        {
            try
            {
                Listings _newListing = new Listings();
                _newListing.description = _listingBEAN.description;
                _newListing.image = _listingBEAN.image;
                _newListing.priceBuy = _listingBEAN.priceBuy;
                _newListing.category = _listingBEAN.categoryId;
                _newListing.accountId = _listingBEAN.accountId;
                _newListing.startDate = _listingBEAN.startDate;
                _context.Listings.Add(_newListing);
                _context.SaveChanges();
                return true;
            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine
                        ("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                return false;
                throw;
            }
        }

        //Delete Listing Functionality
        public bool DeleteListing(AuctionBEANS _listingBEAN)
        {
            bool check = ListingCheck(_listingBEAN.Id);
            if (check == false)
            {
                return false;
            }
            else
            {
                Listings _doomedList = new Listings();
                _doomedList.description = _listingBEAN.description;
                _doomedList.image = _listingBEAN.image;
                _doomedList.priceBuy = _listingBEAN.priceBuy;
                _doomedList.category = _listingBEAN.categoryId;
                _doomedList.accountId = _listingBEAN.accountId;
                _doomedList.startDate = _listingBEAN.startDate;
                _context.Listings.Remove(_doomedList);
                _context.SaveChanges();
                return true;
            }
        }

        //Edit the Listing function

        public bool EditListing(AuctionBEANS _listingBEAN)
        {
            if (ListingCheck(_listingBEAN.Id) == true)
            {
                Listings update = GetSingularListing(_listingBEAN.Id);
                update.category = _listingBEAN.categoryId;
                update.description = _listingBEAN.description;
                update.image = _listingBEAN.image;
                update.priceBuy = _listingBEAN.priceBuy;
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        //Get list of listing categories method

        public IList<listing_Category> GetCategories()
        {
            IQueryable<listing_Category> _categories;
            _categories = from category
                          in _context.listing_Category
                          select category;

            return _categories.ToList<listing_Category>();
        }

        // Get Bid History 

        public IList<listingBid> GetBidHistory(int accountId)
        {
            IQueryable<listingBid> __bidHistory;
            __bidHistory = from History
                           in _context.listingBid
                           where History.accountId == accountId
                           select History;
            return __bidHistory.ToList<listingBid>();
        }

        // Get Listing History
        public IList<Listings> GetListingHistory(int accountId)
        {
            IQueryable<Listings> _listingHistory;
            _listingHistory = from LHistory
                              in _context.Listings
                              where LHistory.accountId == accountId
                              select LHistory;
            return _listingHistory.ToList<Listings>();

        }

        public bool MakeBid(bidBEANS _newBid)
        {
            try
            {
                listingBid newHighBid = new listingBid();
                newHighBid.bid = _newBid.bid;
                newHighBid.itemId = _newBid.itemId;
                newHighBid.accountId = _newBid.accountId;
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine
                        ("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                return false;
            }
        }

    }
}