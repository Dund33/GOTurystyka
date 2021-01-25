using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using GOTurystyka.Controllers;
using GOTurystyka.Models;
using GOTurystyka.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Test
{
    public class TestConfirmSegment
    {
        private GOTurystykaContext _context;
        private const string TripName = "RANDOM_NAMED_TRIP";

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
        public void TestConfirmedTripIsConfirmed()
        {
            //Arrange
            var segment = _context.Segments.AsNoTracking().First();
            var systemUnderTest = new SegmentsController(_context, new MessageSender());
            segment.Id = 0;
            segment.Approved = false;
            _context.Segments.Add(segment);
            _context.SaveChanges();
            
            //Act
            var _= systemUnderTest.Approve(segment.Id).Result;

            //Assert
            segment.Approved.Should().BeTrue();
        }

        [Test]
        public void TestTripAlreadyConfirmed()
        {
            var segment = _context.Segments.AsNoTracking().First();
            var systemUnderTest = new SegmentsController(_context, new MessageSender());
            segment.Id = 0;
            segment.Approved = true;
            _context.Segments.Add(segment);
            _context.SaveChanges();
            
            //Act
            var resp= systemUnderTest.Approve(segment.Id).Result;

            //Assert
            resp.Should().BeOfType<OkObjectResult>();
        }
    }
}