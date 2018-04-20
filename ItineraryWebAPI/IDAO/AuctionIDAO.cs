using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ItineraryWebAPI
{
    public interface AuctionIDAO
    {
        IList<AuctionBEANS> GetListings(int category);
        IList<listing_Category> GetCategories();
        IList<bidBEANS> GetBidHistory(int accountId);
        IList<AuctionBEANS> GetListingHistory(int accountId);
        Listings GetSingularListing(int Id);
        bool ListingCheck(int id);
        bool AddListing(AuctionBEANS _newListing);
        bool DeleteListing(Listings _listing);
        double GetAuctionPrice(int itemId);
    }
}
