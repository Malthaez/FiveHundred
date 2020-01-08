using Assets.Scripts.API.DTOs;
using Assets.Scripts.API.DTOs.Enums;

namespace Assets.Scripts.API.Interfaces.Repositories
{
    public interface ICardRepository
    {
        Card Get(CardSuit? cardSuit);
    }
}
