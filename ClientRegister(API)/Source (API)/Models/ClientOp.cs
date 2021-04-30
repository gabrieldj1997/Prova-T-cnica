using Newtonsoft.Json;
using Source__API_.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Source__API_.Models
{
    public class ClientOp : IClient
    {
        public Client GetClient(int id, string path)
        {
            IList<Client> list = GetListClient(path);
            foreach(Client c in list)
            {
                if (c.id == id) return c;
            }
            return null; 
        }

        public IList<Client> GetListClient(string path)
        {
            string list;
            IList<Client> clients;
            using (StreamReader sr = new StreamReader(path))
            {
                list = sr.ReadToEnd();
                clients = JsonConvert.DeserializeObject<List<Client>>(list);
                sr.Close();
            }
            if(clients == null)
            {
                clients = new List<Client>();
            }
            return clients;
        }

        public int RegisterClient(Client client, string path)
        {
            try {
                if (File.Exists(path))
                {
                    IList<Client> clients;
                    string row;
                    using (StreamReader sr = new StreamReader(path))
                    {
                        row = sr.ReadToEnd();
                        sr.Close();
                    }
                    clients = JsonConvert.DeserializeObject<IList<Client>>(row);
                    if (clients == null)
                    {
                        clients = new List<Client>();
                    }
                    clients.Add(client);
                    File.WriteAllText(path, JsonConvert.SerializeObject(clients));
                    return 1;
                }
                else
                {
                    var file = File.Create(path);
                    file.Close();
                    RegisterClient(client, path);
                }
                return 0;
            }catch(Exception e)
            {
                IO io = new IO();
                io.Log(DateTime.Now.ToString("MM/dd hh:mm:ss : "+e.Message));
                return 0;
            }
        }
        public Client DeleteClient(int id, string path)
        {
            try
            {
                Client client = null;
                string json_list;
                using (StreamReader sr = new StreamReader(path))
                {
                    json_list = sr.ReadToEnd();
                    sr.Close();
                }
                if(json_list != "")
                {
                    IList<Client> list_client = JsonConvert.DeserializeObject<IList<Client>>(json_list);
                    for(int i = 0; i < list_client.Count; i++)
                    {
                        if(list_client[i].id == id)
                        {
                            client = list_client[i];
                            list_client.Remove(list_client[i]);
                        }
                    }
                    File.WriteAllText(path, JsonConvert.SerializeObject(list_client));
                    return client;
                }
                else
                {
                    return null;
                }
            }catch(Exception e)
            {
                return null;
            }
        }
    }
}
