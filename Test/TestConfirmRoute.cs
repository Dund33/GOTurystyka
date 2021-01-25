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
    public class TestConfirmRoute
    {
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
        public void ConfirmedRouteIsConfirmedTest()
        {
            var systemUnderTest = new RoutesController(_context);
            var route = _context.Routes.AsNoTracking().First();
            route.Id = 0;
            route.Approved = false;
            route.AlreadyTravelled = true;
            route.WaitingForApproval = true;
            _context.Routes.Add(route);
            _context.SaveChanges();
            var _ = systemUnderTest.Approve(route.Id).Result;
            route.Approved.Should().BeTrue();
            _context.Remove(route);
            _context.SaveChanges();
            
        }

        [Test]
        public void NonexistetntRouteIsNotFound()
        {
            var invalidId = 150594948;
            var systemUnderTest = new RoutesController(_context);
            var res = systemUnderTest.Approve(invalidId).Result;
            res.Should().BeOfType<NotFoundResult>();
        }
    }
}