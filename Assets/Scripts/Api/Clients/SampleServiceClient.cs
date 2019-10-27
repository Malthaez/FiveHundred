using MatchingGame.Api.Clients.Databases;
using MatchingGame.Api.Clients.Interfaces;
using System;
using System.Collections.Generic;

namespace MatchingGame.Api.Clients
{
    public class SampleServiceClient : ISampleServiceClient, IDisposable
    {
        public Dictionary<string, object> Get(int id) => TestCardDatabase.Cards[id];

        public void Dispose() => throw new NotImplementedException();
    }
}
