using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItineraryWebAPI
{
    public class AuctionBEANS
    {
        public AuctionBEANS() { }

        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public Nullable<double> priceBuy { get; set; }
        public string category { get; set; }
        public int categoryId { get; set; }
        public int accountId { get; set; }
        public DateTime startDate { get; set; }
    }
}
