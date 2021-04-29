using System;
using System.Collections.Generic;
using System.Text;

namespace SourceConsole.Models
{
    interface IClient
    {
        IList<Client> GetListClientTemp(string path);
        int CpfValidation(string cpf);
        int RegisterClient(Client client, string path);
        int GetLastId(List<Client> list);
    }
}
