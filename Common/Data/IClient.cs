using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data
{
    public interface IClient
    {
        public Supabase.Client DatabaseClient { get;}
    }
}
