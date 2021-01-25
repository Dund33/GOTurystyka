using System;
using System.IO;
using FluentAssertions;
using GOTurystyka.Controllers;
using GOTurystyka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Test
{
    public class TestTouristDetails
    {
        private const string MemoryConnString = "Database=:memory:";


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
        public void TestTouristsThatDontExistReturn404()
        {
            //arrange
            var invalidId = 10000;
            var systemUnderTest = new TouristsController(_context);

            //act
            var res = systemUnderTest.Details(invalidId).Result;

            //assert
            res.Should().BeOfType<NotFoundResult>();
        }
        
        [Test]
        public void TestTouristsThatExistReturnView(){
            
            //Arrange
            var existingTourist = 1;
            var systemUnderTest = new TouristsController(_context);
            
            //Act
            var res = systemUnderTest.Details(existingTourist).Result;
            
            //Assert
            res.Should().BeOfType<ViewResult>();
        }
    }
}