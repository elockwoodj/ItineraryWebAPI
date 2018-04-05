using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace ItineraryWebAPI
{
    public class ItineraryDAO
    {
        private ItineraryEntities _context;

        public ItineraryDAO()
        {
            _context = new ItineraryEntities();
        }

        public IList<Itinerary> GetItineraryList()
        {
            IQueryable<Itinerary> itineraries;
            itineraries = from iti in _context.Itinerary
                         select iti;
            if (itineraries.Count() != 0)
                return itineraries.ToList<Itinerary>();
            else
                return null;
        }

        public Itinerary GetItinerary(int id)
        {
            IQueryable<Itinerary> itineraries;
            itineraries = from iti in _context.Itinerary
                          where iti.Id==id
                          select iti;
            if (itineraries.Count() != 0)
                return itineraries.ToList<Itinerary>().First();
            else
                return null;
        }

        public bool DeleteItinerary(Itinerary itinerary)
        {
            if (!ItineraryCheck(itinerary.Id))
            {
                return false;
            }
            else
            {
                _context.Itinerary.Remove(itinerary);
                _context.SaveChanges();
                return true;
            }
        }


        public bool ItineraryCheck(int id)
        {
            IQueryable<int> idList = from ity
                                     in _context.Itinerary
                                     select ity.Id;
            if (idList.ToList<int>().Contains(id))
            { return true; }
            else { return false; }
        }

        public bool AddItinerary(Itinerary itinerary)
        {
            try
            {
                _context.Itinerary.Add(itinerary);
                _context.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
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

        public bool EditItinerary(Itinerary _itinerary)
        {
            if (!ItineraryCheck(_itinerary.Id))
            {
                return false;
            }
            else
            {
                Itinerary itinerary = (from iti
                                in _context.Itinerary
                                          where iti.Id == _itinerary.Id
                                    select iti).ToList<Itinerary>().First();
                itinerary.itiName = _itinerary.itiName;
                itinerary.destination = _itinerary.destination;
                itinerary.purpose = _itinerary.purpose;
                itinerary.startDate = _itinerary.startDate;
                itinerary.endDate = _itinerary.endDate;
                _context.SaveChanges();
                return true;
            }
        }


    }
}