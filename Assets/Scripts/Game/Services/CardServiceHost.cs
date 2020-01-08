using Assets.Scripts.API.Clients;
using Assets.Scripts.API.Managers;
using Assets.Scripts.API.Repositories;
using Assets.Scripts.API.ServiceDefinitions;
using UnityEngine;

namespace Assets.Scripts.Game.Services
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
