//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ItineraryWebAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Listings
    {
        public int Id { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public Nullable<double> priceBuy { get; set; }
        public int category { get; set; }
        public int accountId { get; set; }
        public System.DateTime startDate { get; set; }
        public string title { get; set; }
        public double startPrice { get; set; }
    }
}
