using Core.Infrastructure.Security;
using Core.Request.Payment;
using FakeItEasy;
using FluentAssertions;
using HttpContextMoq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Controllers.Payment;

namespace Web_npm.Tests
{
    public class PaymentControllerTests
    {
        private PaymentController PaymentController;
        public PaymentControllerTests()
        {
            //SUT 
            PaymentController = new PaymentController();
        }

        [Fact]
        public async Task PaymentController_Get_ReturnsNull()
        {
            //Arrange
            var httpContextMock = new HttpContextMock();
            var request = new MaterialRequest();
            var a = A.Fake<UserIdentity>();
            var c = new UserIdentity(new ClaimsIdentity(new List<Claim>() { new Claim("clientId", "1") }));
            var b = A.Fake<PaymentController>();
            A.CallTo(() => b.CurrentUser).Returns(c);
            //Act
            var result = await b.Get(request);

            //Assert
            result.Should().BeOfType<BadRequestResult>();

        }
    }
}