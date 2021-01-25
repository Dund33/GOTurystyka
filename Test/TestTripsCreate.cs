using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using GOTurystyka.Controllers;
using GOTurystyka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Test
{
    public class TestTripsCreate
    {
        private const string TripName = "RANDOM_NAMED_TRIP";
        private GOTurystykaContext _context;

        private static IServiceProvider CreateServices()
        {
            var server = File.ReadAllText("Properties/SQLServer.txt");

            return new ServiceCollection()
                .AddSingleton<GOTurystykaContext>()
                .AddDbContext<GOTurystykaContext>(options => options.UseSqlServer(server))
                .BuildServiceProvider();
        }

        [SetUp]
        public void Setup()
        {
            _context = CreateServices().GetRequiredService<GOTurystykaContext>();
        }
        
        [Test]
        public void TestTripExistsReturnsView()
        {
            //Arrange
            var systemUnderTest = new TripsController(_context);
            
            //Act
            var res = systemUnderTest.Create();
            
            //Assert
            res.Should().BeOfType<ViewResult>();
        }

        [Test]
        public void TestTripCreatedWhenCalledCreate()
        {
            //Arrange
            
            var systemUnderTest = new TripsController(_context);

            var oldTourist = _context.Tourists.AsNoTracking().First();
            var route = _context.Routes.First();

            var tourist = oldTourist;
            tourist.Id = 0;

            var trip = new Trip()
            {
                Confirmed = false,
                Date = DateTime.Now,
                Ended = false,
                Name = TripName,
                Tourist = tourist,
                TouristId = tourist.Id,
                Route = route
            };
            
            //Act
            var _= systemUnderTest.Create(trip).Result;
            _context.SaveChanges();
            var resTrip = _context.Trips.Find(trip.Id);
            
            //Assert
            resTrip.Should().NotBeNull();
            
            //Cleanup
            var uit = _context.UsersInTrips
                .FirstOrDefault(uit => uit.UserId == 1 && uit.TripId == resTrip.Id);
            _context.Remove(uit);
            _context.Remove(trip);
            _context.Remove(tourist);
            _context.SaveChanges();
        }
    }
}