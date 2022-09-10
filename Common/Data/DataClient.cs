using Microsoft.Extensions.Configuration;
using Supabase;

namespace Common.Data
{
    public class DataClient : IClient
    {
        public Client DatabaseClient { get
            {
                return Client.Instance;
            }
        }
    }
}
