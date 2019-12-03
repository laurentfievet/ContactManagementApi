using ContactManagement.Api.Controllers;
using ContactManagement.Api.Validators;
using ContactManagement.Repo.Services;
using ContactManagement.xUnitTest.ServiceTest;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.TestHelper;
using Xunit;
using ContactManagement.Repo.Models;

namespace ContactManagement.xUnitTest
{
    public class ContactControllerTest
    {
        ContactController _controller;
        IContactService _contactService;
        private ContactValidator validator;

        public ContactControllerTest()
        {
            _contactService = new ContactServiceFake();
            _controller = new ContactController(_contactService);

            validator = new ContactValidator();
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
            var person = new ContactDTO { Id = 1, FirstName = "Laurent", LastName = "FIEVET", GSMNumber = "111515151", IsFreelance = true, VATNumber = "2145245545", Address = new AddressDTO { Name = "test", City = "gfdfd", Country = "fdfdfd", PostalCode = "dfdfd", Street = "fdfdfd", StreetNumber = "fdfdfdfd" } };

            validator.ShouldNotHaveValidationErrorFor(x => x.VATNumber, person);
        }

        [Fact]
        public void PostContactFreelance_WithVATNumber()
        {
            var person = new ContactDTO { Id = 1, FirstName = "Laurent", LastName = "FIEVET", GSMNumber = "111515151", IsFreelance = true, VATNumber = "", Address = new AddressDTO { Name = "test", City = "gfdfd", Country = "fdfdfd", PostalCode = "dfdfd", Street = "fdfdfd", StreetNumber = "fdfdfdfd" } };

            validator.ShouldHaveValidationErrorFor(x => x.VATNumber, person);

        }

    }
}
