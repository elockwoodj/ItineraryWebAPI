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
        //Accepts the id for a category from the listings table, then using a linq statement selects information from both the Listings and the listing_Category table
        //These are formatted into a BEAN as this provides the ability to access information from both tables in the same object - returning the desired information
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
                                                         title = list.title,
                                                         description = list.description,
                                                         image = list.image,
                                                         category = categ.category,
                                                         priceBuy = list.priceBuy,
                                                         endDate = list.endDate,
                                                         accountId = list.accountId,
                                                         categoryId = list.category,
                                                         startPrice = list.startPrice

                                                     };

            return _listingBEANs.ToList<AuctionBEANS>();

        }

        //Accepts the id for a listing, using this selects the first listing from the Listings table with that specified Id - these Id's are a primary key hence each id will refer to a single listing

        public Listings GetSingularListing(int Id)
        {
            IQueryable<Listings> _listingBEAN = from list in _context.Listings
                                                where list.Id == Id
                                                select list;
            return _listingBEAN.ToList<Listings>().First();
        }

        //Accepts an Id for a listing, checking if this listing exists within the Listings table - USED IN XYZ

        public bool ListingCheck(int Id)
        {
            IQueryable<int> idList = from lists
                                     in _context.Listings
                                     select lists.Id;
            if (idList.ToList<int>().Contains(Id))
            { return true; }
            else { return false; }
        }

        //Accepts an AuctionBEANS object, which will be converted into a Listings object to be input into the Listings table
        //Returns true if the object is successfully added into the Listings table
        public bool AddListing(AuctionBEANS _listingBEAN)
        {
            try
            {
                Listings _newListing = new Listings
                {
                    title = _listingBEAN.title,
                    description = _listingBEAN.description,
                    priceBuy = _listingBEAN.priceBuy,
                    category = _listingBEAN.categoryId,
                    accountId = _listingBEAN.accountId,
                    endDate = _listingBEAN.endDate,
                    startPrice = _listingBEAN.startPrice
                };

                //Currently it isn't possible to upload personalised images, each category has a set image - this switch case sets the image URL which is used within the HTML page
                switch (_listingBEAN.categoryId)
                {
                    case 1:
                        _newListing.image = "Content/Toys and Games.jpg";
                        break;
                    case 2:
                        _newListing.image = "Content/furniture.jpg";
                        break;
                    case 3:
                        _newListing.image = "Content/appliances.jpg";
                        break;
                    case 4:
                        _newListing.image = "Content/electronics.jpg";
                        break;
                    case 5:
                        _newListing.image = "Content/mClothes.jpg";
                        break;
                    case 6:
                        _newListing.image = "Content/wClothes.jpg";
                        break;
                    case 7:
                        _newListing.image = "Content/kClothes.jpg";
                        break;
                    case 8:
                        _newListing.image = "Content/gaming.jpg";
                        break;
                    case 9:
                        _newListing.image = "Content/kids.jpg";
                        break;
                    case 10:
                        _newListing.image = "Content/garden.jpg";
                        break;
                    case 11:
                        _newListing.image = "Content/tools.jpg";
                        break;
                    case 12:
                        _newListing.image = "Content/books-movies-music.jpg";
                        break;
                }

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

        //accepts a Listings object, using the ListingCheck method detailed earlier to confirm the listing exists before deleting it
        //if the listing does exist all the required information is placed into an object (_doomedList) and removed from the table - these changes are then saved
        public bool DeleteListing(Listings _listingBEAN)
        {
            bool check = ListingCheck(_listingBEAN.Id);
            if (check == false)
            {
                return false;
            }
            else
            {
                Listings _doomedList = new Listings
                {
                    title = _listingBEAN.title,
                    description = _listingBEAN.description,
                    image = _listingBEAN.image,
                    priceBuy = _listingBEAN.priceBuy,
                    category = _listingBEAN.category,
                    accountId = _listingBEAN.accountId,
                    endDate = _listingBEAN.endDate,
                    startPrice = _listingBEAN.startPrice
                };
                _context.Listings.Remove(_doomedList);
                _context.SaveChanges();
                return true;
            }
        }

        //accepts a Listings object, using the ListingCheck method detailed earlier to confirm the listing exists before editing it
        //if the listing does exist all the required information is placed into an object (update) and changed within the table - these changes are then saved
        public bool EditListing(AuctionBEANS _listingBEAN)
        {
            if (ListingCheck(_listingBEAN.Id) == true)
            {
                Listings update = GetSingularListing(_listingBEAN.Id);
                update.title = _listingBEAN.title;
                update.category = _listingBEAN.categoryId;
                update.description = _listingBEAN.description;
                update.priceBuy = _listingBEAN.priceBuy;
                update.startPrice = _listingBEAN.startPrice;
                update.endDate = _listingBEAN.endDate;

                switch (_listingBEAN.categoryId)
                {
                    case 1:
                        update.image = "Content/Toys and Games.jpg";
                        break;
                    case 2:
                        update.image = "Content/furniture.jpg";
                        break;
                    case 3:
                        update.image = "Content/appliances.jpg";
                        break;
                    case 4:
                        update.image = "Content/electronics.jpg";
                        break;
                    case 5:
                        update.image = "Content/mClothes.jpg";
                        break;
                    case 6:
                        update.image = "Content/wClothes.jpg";
                        break;
                    case 7:
                        update.image = "Content/kClothes.jpg";
                        break;
                    case 8:
                        update.image = "Content/gaming.jpg";
                        break;
                    case 9:
                        update.image = "Content/kids.jpg";
                        break;
                    case 10:
                        update.image = "Content/garden.jpg";
                        break;
                    case 11:
                        update.image = "Content/tools.jpg";
                        break;
                    case 12:
                        update.image = "Content/books-movies-music.jpg";
                        break;
                }

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

        public IList<bidBEANS> GetBidHistory(int accountId)
        {
            IQueryable<bidBEANS> __bidHistory;
            __bidHistory = from History in _context.listingBid
                           from list in _context.Listings
                           where History.accountId == accountId
                           where History.itemId == list.Id
                           select new bidBEANS
                           {
                               title = list.title,
                               bid = History.bid
                           };
            return __bidHistory.ToList<bidBEANS>();
        }

        // Get Listing History
        public IList<AuctionBEANS> GetListingHistory(int accountId)
        {
            IQueryable<AuctionBEANS> _listingHistory;
            _listingHistory = from LHistory in _context.Listings
                              from categ in _context.listing_Category
                              where LHistory.accountId == accountId
                              where LHistory.category == categ.Id
                              select new AuctionBEANS
                              {
                                  category = categ.category,
                                  Id = LHistory.Id,
                                  title = LHistory.title,
                                  description = LHistory.description,
                                  image = LHistory.image,
                                  priceBuy = LHistory.priceBuy,
                                  endDate = LHistory.endDate
                              };
            return _listingHistory.ToList<AuctionBEANS>();

        }

        public double GetAuctionPrice(int ItemId)
        {

            double price;
            listingBid _priceCheck;
            _priceCheck = (from listingBid
                          in _context.listingBid
                           where listingBid.itemId == ItemId
                           orderby listingBid.bid descending
                           select listingBid).DefaultIfEmpty(null).First();

            if (_priceCheck == null)
            {
                IQueryable<Listings> _checker;
                _checker = from Listings
                           in _context.Listings
                           where Listings.Id == ItemId
                           select Listings;
                Listings checker = _checker.ToList<Listings>().First();

                price = checker.startPrice;

            }
            else
            {
                IQueryable<listingBid> _IDCheck;
                _IDCheck = from listingBid
                           in _context.listingBid
                           where listingBid.itemId == ItemId
                           orderby listingBid.bid descending
                           select listingBid;
                listingBid myBid = _IDCheck.ToList<listingBid>().First();

                price = myBid.bid;
            }
            return price;

        }



        
        public bool MakeBid(bidBEANS _newBid)
        {
            try
            {
                double price = GetAuctionPrice(_newBid.itemId);
                if (_newBid.bid > price)
                {
                    listingBid newHighBid = new listingBid
                    {
                        bid = _newBid.bid,
                        itemId = _newBid.itemId,
                        accountId = _newBid.accountId
                    };
                    _context.listingBid.Add(newHighBid);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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