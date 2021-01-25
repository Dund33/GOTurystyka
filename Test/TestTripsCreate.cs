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
            var trip = new Trip()
            {
                Confirmed = false,
                Date = DateTime.Now,
                Ended = false,
                Name = TripName,
                RouteId = 1,
                TouristId = 1
            };
            
            //Act
            systemUnderTest.Create(trip);
            _context.SaveChanges();
            var resTrip = _context.Trips.SingleOrDefault(trip => trip.Name == TripName);
            
            //Assert
            resTrip.Should().NotBeNull();
            
            //Cleanup
            _context.Remove(trip);
            _context.SaveChanges();
        }
    }
}