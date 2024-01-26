namespace Moonpig.PostOffice.Tests
{
    using System;
    using System.Collections.Generic;
    using Api.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Moonpig.PostOffice.Api;
    using Moonpig.PostOffice.Api.Model;
    using Moonpig.PostOffice.Data;
    using Shouldly;
    using Xunit;

    public class PostOfficeTests
    {
        DbContext mockDbContext = new DbContext();

        [Fact]
        public void OneProductWithLeadTimeOfOneDay()
        {
            DispatchDateCalculator dispatchCalculator = new DispatchDateCalculator(mockDbContext);
            DespatchDateController controller = new DespatchDateController(dispatchCalculator);
            IActionResult actionResult = controller.Get(new List<int>() { 1 }, DateTime.Now);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            DespatchDate date = Assert.IsType<DespatchDate>(objectResult.Value);
            date.Date.Date.ShouldBe(DateTime.Now.Date.AddDays(1));
        }


        [Fact]
        public void OneProductWithLeadTimeOfTwoDay()
        {
            DispatchDateCalculator dispatchCalculator = new DispatchDateCalculator(mockDbContext);
            DespatchDateController controller = new DespatchDateController(dispatchCalculator);
            IActionResult actionResult = controller.Get(new List<int>() { 2 }, DateTime.Now);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            DespatchDate date = Assert.IsType<DespatchDate>(objectResult.Value);
            date.Date.Date.ShouldBe(DateTime.Now.Date.AddDays(2));
        }

        [Fact]
        public void OneProductWithLeadTimeOfThreeDay()
        {
            DispatchDateCalculator dispatchCalculator = new DispatchDateCalculator(mockDbContext);
            DespatchDateController controller = new DespatchDateController(dispatchCalculator);
            IActionResult actionResult = controller.Get(new List<int>() { 3 }, DateTime.Now);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            DespatchDate date = Assert.IsType<DespatchDate>(objectResult.Value);
            date.Date.Date.ShouldBe(DateTime.Now.Date.AddDays(3));
        }

        [Fact]
        public void SaturdayHasExtraTwoDays()
        {
            DispatchDateCalculator dispatchCalculator = new DispatchDateCalculator(mockDbContext);
            DespatchDateController controller = new DespatchDateController(dispatchCalculator);
            IActionResult actionResult = controller.Get(new List<int>() { 1 }, new DateTime(2018, 1, 26));
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            DespatchDate date = Assert.IsType<DespatchDate>(objectResult.Value);
            date.Date.ShouldBe(new DateTime(2018, 1, 26).Date.AddDays(3));
        }

        [Fact]
        public void SundayHasExtraDay()
        {
            DispatchDateCalculator dispatchCalculator = new DispatchDateCalculator(mockDbContext);
            DespatchDateController controller = new DespatchDateController(dispatchCalculator);
            IActionResult actionResult = controller.Get(new List<int>() { 3 }, new DateTime(2018, 1, 25));
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            DespatchDate date = Assert.IsType<DespatchDate>(objectResult.Value);
            date.Date.ShouldBe(new DateTime(2018, 1, 25).Date.AddDays(5));
        }

        [Fact]
        public void OneProductWithLeadTimeOfSixDays()
        {
            DispatchDateCalculator dispatchCalculator = new DispatchDateCalculator(mockDbContext);
            DespatchDateController controller = new DespatchDateController(dispatchCalculator);
            IActionResult actionResult = controller.Get(new List<int>() { 9 }, new DateTime(2018, 1, 15));
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            DespatchDate date = Assert.IsType<DespatchDate>(objectResult.Value);
            date.Date.ShouldBe(new DateTime(2018, 1, 15).Date.AddDays(8));
        }

        [Fact]
        public void OneProductWithLeadTimeOfThirteenDays()
        {
            DispatchDateCalculator dispatchCalculator = new DispatchDateCalculator(mockDbContext);
            DespatchDateController controller = new DespatchDateController(dispatchCalculator);
            IActionResult actionResult = controller.Get(new List<int>() { 10 }, new DateTime(2018, 1, 15));
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            DespatchDate date = Assert.IsType<DespatchDate>(objectResult.Value);
            date.Date.ShouldBe(new DateTime(2018, 1, 15).Date.AddDays(17));
        }

        [Fact]
        public void TwoProductsWithLeadTimeOfOneDayAndTwoDay()
        {
            DispatchDateCalculator dispatchCalculator = new DispatchDateCalculator(mockDbContext);
            DespatchDateController controller = new DespatchDateController(dispatchCalculator);
            IActionResult actionResult = controller.Get(new List<int>() { 1, 2 }, new DateTime(2018, 1, 1));
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            DespatchDate date = Assert.IsType<DespatchDate>(objectResult.Value);
            date.Date.ShouldBe(new DateTime(2018, 1, 1).Date.AddDays(2));
        }

    }
}
