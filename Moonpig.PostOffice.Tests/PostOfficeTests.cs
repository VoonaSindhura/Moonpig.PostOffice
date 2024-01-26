namespace Moonpig.PostOffice.Tests
{
    using System;
    using System.Collections.Generic;
    using Api.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Moonpig.PostOffice.Api;
    using Moonpig.PostOffice.Api.Model;
    using Moonpig.PostOffice.Api.Repository;
    using Moonpig.PostOffice.Api.Service;
    using Moonpig.PostOffice.Data;
    using Shouldly;
    using Xunit;

    public class PostOfficeTests
    {
        IDbContext _dbContext;
        IProductRepository _productRepository;
        ISupplierRepository _supplierRepository;
        DispatchDateCalculatorService _dispatchCalculator;
        DespatchDateController _controller;
        ProductService _productService;
        SupplierService _supplierService;
        public PostOfficeTests()
        {
            _dbContext = new DbContext();
            _productRepository = new ProductRepository(_dbContext);
            _supplierRepository = new SupplierRepository(_dbContext);
            _supplierService = new SupplierService(_supplierRepository);
            _productService = new ProductService(_productRepository);
            _dispatchCalculator = new DispatchDateCalculatorService(_productService, _supplierService);
            _controller = new DespatchDateController(_dispatchCalculator);
        }

        [Fact]
        public void OneProductWithLeadTimeOfOneDay()
        {
            // Arrange 
            var orderdDate = new DateTime(2024, 1, 23);
            var expectedDate = orderdDate.AddDays(1);
            var products = new List<int>() { 1 };

            //Action 
            IActionResult actionResult = _controller.Get(products, orderdDate);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            //Assert
            DespatchDate result = Assert.IsType<DespatchDate>(objectResult.Value);
            result.Date.Date.ShouldBe(expectedDate);
        }


        [Fact]
        public void OneProductWithLeadTimeOfTwoDay()
        {
            // Arrange 
            var orderdDate = new DateTime(2024, 1, 23);
            var expectedDate = orderdDate.AddDays(2);
            var products = new List<int>() { 2 };
            //Action 
            IActionResult actionResult = _controller.Get(products, orderdDate);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            //Assert
            DespatchDate result = Assert.IsType<DespatchDate>(objectResult.Value);
            result.Date.Date.ShouldBe(expectedDate);
        }

        [Fact]
        public void oneproductwithleadtimeofthreeday()
        {
            // Arrange 
            var orderdDate = new DateTime(2024, 1, 23);
            var expectedDate = orderdDate.AddDays(3);
            var products = new List<int>() { 3 };
            //Action 
            IActionResult actionResult = _controller.Get(products, orderdDate);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            //Assert
            DespatchDate result = Assert.IsType<DespatchDate>(objectResult.Value);
            result.Date.Date.ShouldBe(expectedDate);
        }

        [Fact]
        public void saturdayhasextratwodays()
        {
            // Arrange 
            var orderdDate = new DateTime(2018, 1, 26);
            var expectedDate = orderdDate.AddDays(3);
            var products = new List<int>() { 1 };
            //Action 
            IActionResult actionResult = _controller.Get(products, orderdDate);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            //Assert
            DespatchDate result = Assert.IsType<DespatchDate>(objectResult.Value);
            result.Date.Date.ShouldBe(expectedDate);
        }

        [Fact]
        public void sundayhasextraday()
        {
            // Arrange 
            var orderdDate = new DateTime(2018, 1, 25);
            var expectedDate = orderdDate.AddDays(5);
            var products = new List<int>() { 3 };
            //Action 
            IActionResult actionResult = _controller.Get(products, orderdDate);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            //Assert
            DespatchDate result = Assert.IsType<DespatchDate>(objectResult.Value);
            result.Date.Date.ShouldBe(expectedDate);
        }

        [Fact]
        public void oneproductwithleadtimeofsixdays()
        {
            // Arrange 
            var orderdDate = new DateTime(2018, 1, 15);
            var expectedDate = orderdDate.AddDays(8);
            var products = new List<int>() { 9 };
            //Action 
            IActionResult actionResult = _controller.Get(products, orderdDate);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            //Assert
            DespatchDate result = Assert.IsType<DespatchDate>(objectResult.Value);
            result.Date.Date.ShouldBe(expectedDate);
        }

        [Fact]
        public void oneproductwithleadtimeofthirteendays()
        {
            // Arrange 
            var orderdDate = new DateTime(2018, 1, 15);
            var expectedDate = orderdDate.AddDays(17);
            var products = new List<int>() { 10 };
            //Action 
            IActionResult actionResult = _controller.Get(products, orderdDate);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            //Assert
            DespatchDate result = Assert.IsType<DespatchDate>(objectResult.Value);
            result.Date.Date.ShouldBe(expectedDate);
        }

        [Fact]
        public void twoproductswithleadtimeofonedayandtwoday()
        {
            // Arrange 
            var orderdDate = new DateTime(2018, 1, 1);
            var expectedDate = orderdDate.AddDays(2);
            var products = new List<int>() { 1, 2 };
            //Action 
            IActionResult actionResult = _controller.Get(products, orderdDate);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            //Assert
            DespatchDate result = Assert.IsType<DespatchDate>(objectResult.Value);
            result.Date.Date.ShouldBe(expectedDate);
        }

    }
}