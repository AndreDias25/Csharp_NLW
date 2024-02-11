using Bogus;
using FluentAssertions;
using Moq;
using RocketseatAuction.API.Communication.Requests;
using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;
using RocketseatAuction.API.Services;
using RocketseatAuction.API.UseCases.Auctions.GetCurrent;
using RocketseatAuction.API.UseCases.Offers.CreateOffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UseCases.Test.Offers.CreateOffer
{
    public class CreateOfferUseCaseTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Success(int itemId)
        {
            //ARRANGE - onde inicializa tudo que precisamos

            var request = new Faker<RequestCreateOfferJson>()
                .RuleFor(request => request.Price, f => f.Random.Decimal(1, 700)).Generate();

            var offerRepository = new Mock<IOfferRepository>();
            var loggedUser = new Mock<ILoggedUser>();
            loggedUser.Setup(i => i.User()).Returns(new User());

            var useCase = new CreateOfferUseCase(loggedUser.Object, offerRepository.Object);

            //ACT - nossa ação, onde utilizamos nossas instancias
            var act = () => useCase.Execute(itemId, request);

            //ASSERT
            act.Should().NotThrow();

        }
    }
}
