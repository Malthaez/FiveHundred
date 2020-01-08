using System.Collections.Generic;

namespace Assets.Scripts.API.Clients.Interfaces
{
    public interface ISampleServiceClient
    {
        //int Post(Dictionary<string, object> sample);

        Dictionary<string, object> Get(int id);

        //bool Put(int id, Dictionary<string, object> sample);

        //bool Delete(int id);
    }
}
