using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using GOTurystyka.Controllers;
using GOTurystyka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace Test
{
    public class Tests
    {

        private const string MemoryConnString = "Database=:memory:";
        private static IServiceProvider CreateServices()
        {
            var server = File.ReadAllText("Properties/SQLServer.txt");

            return new ServiceCollection()
                .AddSingleton<GOTurystykaContext>()
                .AddDbContext<GOTurystykaContext>(options => options.UseSqlServer(server))
                .BuildServiceProvider();

        }


        private GOTurystykaContext _context;

        [SetUp]
        public void Setup()
        {
            _context = CreateServices().GetRequiredService<GOTurystykaContext>();
        }

        [Test]
        public void Test1()
        {
            //Arrange
            var systemUnderTest = new TouristsController(_context);

            var res = systemUnderTest.Index().Result;
            var view = res as ViewResult;

            //Assert
            view.Should().NotBeNull();
        }

        [Test]
        public void TestPointsThatDontExistReturn404()
        {
        }
    }
}