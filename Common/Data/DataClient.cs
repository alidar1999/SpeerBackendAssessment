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

        public DataClient()
        {
            Client.Initialize("https://dssxaentuenuzoyuztgl.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImRzc3hhZW50dWVudXpveXV6dGdsIiwicm9sZSI6ImFub24iLCJpYXQiOjE2NjI4MzQ4NzksImV4cCI6MTk3ODQxMDg3OX0.h91D9DeUKokd7-HS4kER0RkSMmGZ3r1rH-uK36sxfRo");
            
        }
    }
}
