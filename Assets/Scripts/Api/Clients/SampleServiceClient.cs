using Assets.Scripts.API.Clients.Databases;
using Assets.Scripts.API.Clients.Interfaces;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.API.Clients
{
    public class SampleServiceClient : ISampleServiceClient, IDisposable
    {
        public Dictionary<string, object> Get(int id) => TestCardDatabase.Cards[id];

        public void Dispose() => throw new NotImplementedException();
    }
}
