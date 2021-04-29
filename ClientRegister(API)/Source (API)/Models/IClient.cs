using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Source__API_.Models
{
    interface IClient
    {
        IList<Client> GetListClient(string path);
        Client GetClient(int id, string path);
        int RegisterClient(Client client, string path);
    }
}
