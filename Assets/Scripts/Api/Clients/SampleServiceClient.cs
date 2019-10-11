using MatchingGame.Api.Clients.Databases;
using MatchingGame.Api.Clients.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace MatchingGame.Api.Clients
{
    public class SampleServiceClient : MonoBehaviour, ISampleServiceClient
    {
        public Dictionary<string, object> Get(int id) => TestCardDatabase.Cards[id];
    }
}
