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
                                                         title = list.title,
                                                         description = list.description,
                                                         image = list.image,
                                                         category = categ.category,
                                                         priceBuy = list.priceBuy,
                                                         startDate = list.startDate,
                                                         accountId = list.accountId,
                                                         categoryId = list.category,
                                                         startPrice = list.startPrice

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
                _newListing.title = _listingBEAN.title;
                _newListing.description = _listingBEAN.description;
                _newListing.priceBuy = _listingBEAN.priceBuy;
                _newListing.category = _listingBEAN.categoryId;
                _newListing.accountId = _listingBEAN.accountId;
                _newListing.startDate = _listingBEAN.startDate;
                _newListing.startPrice = _listingBEAN.startPrice;

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

        //Delete Listing Functionality
        public bool DeleteListing(Listings _listingBEAN)
        {
            bool check = ListingCheck(_listingBEAN.Id);
            if (check == false)
            {
                return false;
            }
            else
            {
                Listings _doomedList = new Listings();
                _doomedList.title = _listingBEAN.title;
                _doomedList.description = _listingBEAN.description;
                _doomedList.image = _listingBEAN.image;
                _doomedList.priceBuy = _listingBEAN.priceBuy;
                _doomedList.category = _listingBEAN.category;
                _doomedList.accountId = _listingBEAN.accountId;
                _doomedList.startDate = _listingBEAN.startDate;
                _doomedList.startPrice = _listingBEAN.startPrice;
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
                update.title = _listingBEAN.title;
                update.category = _listingBEAN.categoryId;
                update.description = _listingBEAN.description;
                update.priceBuy = _listingBEAN.priceBuy;
                update.startPrice = _listingBEAN.startPrice;
                update.startDate = _listingBEAN.startDate;

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

        public double GetAuctionPrice(int ItemId)
        {
            //Two if statements, one as per in MakeBid one as per below - below comes first to check if null, then do makebid version.
            double price=0;
            listingBid _priceCheck;
            _priceCheck = (from listingBid
                          in _context.listingBid
                          where listingBid.itemId == ItemId
                          orderby listingBid.bid descending
                          select listingBid).DefaultIfEmpty(null).First();
            if (_priceCheck != null) { 
                //listingBid bidPrice = _priceCheck.ToList<listingBid>().DefaultIfEmpty(null).First();
                price = _priceCheck.bid;
            }
            else
            {
                
                IQueryable<Listings> _checker;
                _checker = from Listings
                           in _context.Listings
                           where Listings.Id == ItemId
                           select Listings;
                Listings checker = _checker.ToList<Listings>().First();

                price = checker.startPrice;
            }
            return price;

        }
        public bool MakeBid(bidBEANS _newBid)
        {
            try
            {
                IQueryable<listingBid> _IDCheck;
                _IDCheck = from listingBid
                           in _context.listingBid
                           where listingBid.itemId == _newBid.itemId
                           orderby listingBid.bid descending
                           select listingBid;
                listingBid myBid = _IDCheck.ToList<listingBid>().First();

                if (myBid == null)
                {
                    IQueryable<Listings> _startCheck;
                    _startCheck = from listing
                                  in _context.Listings
                                  where listing.Id == _newBid.itemId
                                  select listing;
                    Listings myListing = _startCheck.ToList<Listings>().First();
                    

                    if (_newBid.bid > myListing.startPrice)
                    {
                        listingBid newHighBid = new listingBid();
                        newHighBid.bid = _newBid.bid;
                        newHighBid.itemId = _newBid.itemId;
                        newHighBid.accountId = _newBid.accountId;
                        _context.listingBid.Add(newHighBid);
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    if (_newBid.bid > myBid.bid)
                    {
                        listingBid newHighBid = new listingBid();
                        newHighBid.bid = _newBid.bid;
                        newHighBid.itemId = _newBid.itemId;
                        newHighBid.accountId = _newBid.accountId;
                        _context.listingBid.Add(newHighBid);
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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