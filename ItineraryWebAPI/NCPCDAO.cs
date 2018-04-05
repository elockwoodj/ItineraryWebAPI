using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace ItineraryWebAPI
{
    public class NCPCDAO
    {
        private cmsmr2Entities _context;
        Auth myAuth = new Auth();
        public NCPCDAO()
        {
            _context = new cmsmr2Entities();
        }

        public AuthResponse GetLogin(Auth myAuth)
        {
            AuthResponse myResponse = new AuthResponse();
            if (myAuth.username == "mark" && myAuth.password == "notsure")
            {
                myResponse.authenticated = true;
                myResponse.role = "Staff";
                return myResponse;
            }
            else if (myAuth.username == "charlie" && myAuth.password == "prettysure")
            {
                myResponse.authenticated = true;
                myResponse.role = "Manager";
                return myResponse;
            }
            else
            {
                myResponse.authenticated = false;
                myResponse.role = "NoRole";
                return myResponse;
            }


        }

        public IList<Route> GetRoute()
        {
            IQueryable<Route> routeList;
            routeList = from route in _context.Route
                          select route;
            if (routeList.Count() != 0)
                return routeList.ToList<Route>();
            else
                return null;
        }

        public IList<Booking> GetBooking()
        { 
            IQueryable<Booking> bookingList;
            bookingList = from booking in _context.Booking
                          select booking;
            if (bookingList.Count() != 0)
                return bookingList.ToList<Booking>();
            else
                return null;
        }

        public IList<Vehicle> GetVehicle()
        {
            IQueryable<Vehicle> vehicleList;
            vehicleList = from vehicle in _context.Vehicle
                          select vehicle;
            if (vehicleList.Count() != 0)
                return vehicleList.ToList<Vehicle>();
            else
                return null;
        }

        public Route GetRoute(int id)
        {
            IQueryable<Route> routeList;
            routeList = from route in _context.Route
                          where route.Id == id
                          select route;
            if (routeList.Count() != 0)
                return routeList.ToList<Route>().First();
            else
                return null;
        }


        public Booking GetBooking(int id)
        {
            IQueryable<Booking> bookingList;
            bookingList = from booking in _context.Booking
                          where booking.Id == id
                          select booking;
            if (bookingList.Count() != 0)
                return bookingList.ToList<Booking>().First();
            else
                return null;
        }

        public Vehicle GetVehicle(int id)
        {
            IQueryable<Vehicle> vehicleList;
            vehicleList = from vehicle in _context.Vehicle
                          where vehicle.Id == id
                          select vehicle;
            if (vehicleList.Count() != 0)
                return vehicleList.ToList<Vehicle>().First();
            else
                return null;
        }

        public bool DeleteRoute(Route _route)
        {
            if (!RouteCheck(_route.Id))
            {
                return false;
            }
            else
            {
                _context.Route.Remove(_route);
                _context.SaveChanges();
                return true;
            }
        }


        public bool DeleteBooking(Booking book)
        {
            if (!BookingCheck(book.Id))
            {
                return false;
            }
            else
            {
                _context.Booking.Remove(book);
                _context.SaveChanges();
                return true;
            }
        }

        public bool DeleteVehicle(Vehicle vehicle)
        {
            if (!VehicleCheck(vehicle.Id))
            {
                return false;
            }
            else
            {
                _context.Vehicle.Remove(vehicle);
                _context.SaveChanges();
                return true;
            }
        }

        public bool RouteCheck(int id)
        {
            IQueryable<int> idList = from _route
                                     in _context.Route
                                     select _route.Id;
            if (idList.ToList<int>().Contains(id))
            { return true; }
            else { return false; }
        }


        public bool BookingCheck(int id)
        {
            IQueryable<int> idList = from _booking
                                     in _context.Booking
                                     select _booking.Id;
            if (idList.ToList<int>().Contains(id))
            { return true; }
            else { return false; }
        }

        public bool VehicleCheck(int id)
        {
            IQueryable<int> idList = from _vehicle
                                     in _context.Vehicle
                                     select _vehicle.Id;
            if (idList.ToList<int>().Contains(id))
            { return true; }
            else { return false; }
        }

        public bool AddRoute(Route _route)
        {
            try
            {
                _context.Route.Add(_route);
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


        public bool AddBooking(Booking book)
        {
            try
            {
                _context.Booking.Add(book);
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

        public bool AddVehicle(Vehicle vehicle)
        {
            try
            {
                _context.Vehicle.Add(vehicle);
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

        public bool EditRoute(Route _route)
        {
            if (!RouteCheck(_route.Id))
            {
                return false;
            }
            else
            {
                Route route = (from rout
                                in _context.Route
                                   where rout.Id == _route.Id
                               select rout).ToList<Route>().First();
                route.RouteStartPoint = _route.RouteStartPoint;
                route.RouteEndPoint = _route.RouteEndPoint;
                _context.SaveChanges();
                return true;
            }
        }


        public bool EditBooking(Booking _booking)
        {
            if (!BookingCheck(_booking.Id))
            {
                return false;
            }
            else
            {
                Booking booking = (from book
                                in _context.Booking
                                       where book.Id == _booking.Id
                                       select book).ToList<Booking>().First();
                booking.PickupLocation = _booking.PickupLocation;
                booking.DropOffLocation = _booking.DropOffLocation;
                booking.VehicleId = _booking.VehicleId;
                booking.CurrentPassenger = _booking.CurrentPassenger;
                _context.SaveChanges();
                return true;
            }
        }

        public bool EditVehicle(Vehicle vehicle)
        {
            if (!VehicleCheck(vehicle.Id))
            {
                return false;
            }
            else
            {
                Vehicle _vehicle = (from vech
                                in _context.Vehicle
                                   where vech.Id == vehicle.Id
                                    select vech).ToList<Vehicle>().First();
                _vehicle.Capacity = vehicle.Capacity;
                _vehicle.Driver = vehicle.Driver;
                _vehicle.Make = vehicle.Make;
                _vehicle.Registration = vehicle.Registration;
                _vehicle.Model = vehicle.Model;
                _context.SaveChanges();
                return true;
            }
        }


    }
}