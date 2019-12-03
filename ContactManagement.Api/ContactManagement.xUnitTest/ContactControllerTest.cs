using ContactManagement.Api.Controllers;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using ContactManagement.Repo.Services;
using ContactManagement.Repo.Services.Implementations;
using ContactManagement.xUnitTest.ServiceTest;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ContactManagement.xUnitTest
{
    public class ContactControllerTest
    {
        ContactController _controller;
        IContactService _contactService;

        private readonly Mock<IContactService> _mockService;
        private readonly ContactController _contactController;

        public ContactControllerTest()
        {
            _contactService = new ContactServiceFake();
            _controller = new ContactController(_contactService);

            _mockService = new Mock<IContactService>();
            _mockService.Setup(repo => repo.ListAsync()).Returns(Task.FromResult(Enumerable.Empty<Contact>()));
            _contactController = new ContactController(_mockService.Object);

        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
          
            var okResult = _controller.GetAll();
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(415987);

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
        }

        [Fact]
        public void PostContactFreelance_WithOutVATNumber()
        {
            var dto = new ContactDTO { FirstName = "Laurent", LastName = "FIEVET", GSMNumber = "111515151", IsFreelance = true, VATNumber = "", Address = new AddressDTO { Name = "test", City = "", Country = "", PostalCode = "", Street = "", StreetNumber = "" } };
            var badRequestResult = _contactController.PutOrPost(dto);

            // Assert
            Assert.IsType<BadRequestResult>(badRequestResult.Result);
        }

        [Fact]
        public void PostContactFreelance_WithVATNumber()
        {
           

            var dto = new ContactDTO { FirstName = "Laurent", LastName = "FIEVET", GSMNumber = "111515151", IsFreelance = true, VATNumber = "dfdfdfdfdfd", Address = new AddressDTO { Name = "test", City = "fdsfdsf", Country = "fdsfdsf", PostalCode = "sdfdsfs", Street = "sfdsfdsfds", StreetNumber = "fdsfsdfsd" } };
            var okResult = _contactController.PutOrPost(dto);

            // Assert
            Assert.IsType<OkResult>(okResult.Result);
        }
    }
}
