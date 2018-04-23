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
                Listings _newListing = new Listings();


                _newListing.title = _listingBEAN.title;
                _newListing.description = _listingBEAN.description;
                _newListing.priceBuy = _listingBEAN.priceBuy;
                _newListing.category = _listingBEAN.categoryId;
                _newListing.accountId = _listingBEAN.accountId;
                _newListing.endDate = _listingBEAN.endDate;
                _newListing.startPrice = _listingBEAN.startPrice;


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
        public bool DeleteListing(Listings _doomedList)
        {
            bool check = ListingCheck(_doomedList.Id);
            if (check == false)
            {
                return false;
            }
            else
            {
                _context.Listings.Remove(_doomedList);
                _context.SaveChanges();
                return true;
            };


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


        //Method doesn't require input as it simply selects all the information from the listing_Category table and returns this information as a list
        public IList<listing_Category> GetCategories()
        {
            IQueryable<listing_Category> _categories;
            _categories = from category
                          in _context.listing_Category
                          select category;

            return _categories.ToList<listing_Category>();
        }

        //Method requires an accountId as input as it selects information from both the Listing table and the listingBid table
        //It first selects all the bids in the bid table with the specified accountId, it then uses these bids to select
        //records from the Listings table that have the itemId specified in the retrieved records
        //The information it returns is simply the title of the bids and the bids entered, these are returned as a list

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
                               bid = History.bid,
                               itemId = list.Id
                           };
            return __bidHistory.ToList<bidBEANS>();
        }

        //Method accepts an accountId to be able to check all of the listings a specified account has made
        //returns all the information on the listing and hence is required to also access the listing_Category 
        //table to get the name of the category as this is only stored there.
        //Use of BEANS again here as information is required from two tables.
        //Returns a list of auctionBEANS
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

        //Functionality of this method is to compare the current maximum price of any bids on an item, if there are no bids it will consult the Listings
        //table and select the startPrice return that value
        //It requires an input of a specified itemId to be able to select these values from the two tables
        public double GetAuctionPrice(int ItemId)
        {
            //price is assigned depending on which linq statement is used, then returned at the end of the method
            double price;
            listingBid _priceCheck;
            //the object _priceCheck is used to test whether an item has any bids on it, it consults the listingBid table - if the table returns an empty value 
            //it is then assigned a value of null and select the first value - this is because it is returned in a "list" format, hence the need for First()
            _priceCheck = (from listingBid
                           in _context.listingBid
                           where listingBid.itemId == ItemId
                           select listingBid).DefaultIfEmpty(null).First();

            //If the _priceCheck object is null (no bids on selected item), it will then need to select the startPrice from the Listings table
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
            //if _priceCheck is not null, there must be a bid on the selected item, therefore the maximum bid will be within the listingBid table
            //the selected bids are ordered in descending order as this will place the largest bid at the top of the list, using First() to then select this value

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



        //Accepts a _newBid object which contains: accountId, itemId and bid - 
        //some logic applied to make sure the information is valid before it adds the information to the table
        public bool MakeBid(bidBEANS _newBid)
        {
            //Implementing try/catch to stop the programme crashing if incorrect information is entered
            try
            {
                //maximum price of the selected item is found using the GetAuctionPrice method detailed previously
                double price = GetAuctionPrice(_newBid.itemId);

                //The bid will only be added to the table if the value of the bid entered is greater than the current price of the auction
                if (_newBid.bid > price)
                {
                    //New listingBid object generated to be added into the table and saved
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
                //If the bid entered isn't greater than the current price it is rejected
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