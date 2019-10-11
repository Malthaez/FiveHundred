using MatchingGame.Api.Clients;
using MatchingGame.Api.Managers;
using MatchingGame.Api.Repositories;
using MatchingGame.Api.ServiceDefinitions;
using UnityEngine;

namespace MatchingGame.Services
{
    public class CardServiceHost : MonoBehaviour
    {
        public static CardService CreateCardService() =>
            new CardService(
                new CardManager(
                    new CardRepository(
                        new SampleServiceClient())));
    }
}
