using RocketseatAuction.API.Contracts;
using RocketseatAuction.API.Entities;

namespace RocketseatAuction.API.UseCases.Auctions.GetCurrent
{
    public class GetCurrentAuctionUseCase
    {
        private readonly IAuctionRepository repository;

        public GetCurrentAuctionUseCase()
        {
        }

        public GetCurrentAuctionUseCase(IAuctionRepository repository) => this.repository = repository;
        public Auction? Execute()
        {
            return repository.GetCurrenty();
        }
    }
}
