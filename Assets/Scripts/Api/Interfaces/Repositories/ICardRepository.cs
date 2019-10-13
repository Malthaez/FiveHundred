using MatchingGame.Api.DTOs;
using MatchingGame.Api.DTOs.Enums;

namespace MatchingGame.Api.Interfaces.Repositories
{
    public interface ICardRepository
    {
        Card Get(CardSuit? cardSuit);
    }
}
