using Newtonsoft.Json;
using SourceConsole.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace SourceConsole.Models
{
    class ClientDal : IClient
    {
        public int CpfValidation(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
			string tempCpf;
			string digito;
			int soma;
			int resto;
			cpf = cpf.Trim();
			cpf = cpf.Replace(".", "").Replace("-", "");
			if (cpf.Length != 11)
			   return 0;
			tempCpf = cpf.Substring(0, 9);
			soma = 0;

			for(int i=0; i<9; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
			resto = soma % 11;
			if ( resto < 2 )
				resto = 0;
			else
			   resto = 11 - resto;
			digito = resto.ToString();
			tempCpf = tempCpf + digito;
			soma = 0;
			for(int i=0; i<10; i++)
				soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
			resto = soma % 11;
			if (resto < 2)
			   resto = 0;
			else
			   resto = 11 - resto;
			digito = digito + resto.ToString();
			return cpf.EndsWith(digito) ? 1 : 0;
	    }

        public IList<Client> GetListClientTemp(string path)
        {
            if (File.Exists(path))
            {
				using (StreamReader sr = new StreamReader(path))
                {
					string linha = sr.ReadLine();
                    sr.Close();
					File.WriteAllText(path,"");
					return JsonConvert.DeserializeObject<IList<Client>>(linha);
                }
            }
            else
            {
				return null;
            }
        }

        public int RegisterClient(Client client, string path)
        {
            if (CpfValidation(client.cpf) == 1)
            {
				string jsonClients = "";
				List<Client> listClient = new List<Client>();

                if (!File.Exists(path))
                {
					File.Create(path);
                }

				using (StreamReader st = new StreamReader(path))
				{
					jsonClients += st.ReadLine();
					st.Close();
				}
				listClient = JsonConvert.DeserializeObject<List<Client>>(jsonClients);
				if(listClient == null)
                {
					File.WriteAllText(path, "[]");
					listClient = new List<Client>();
				}
				foreach(Client c in listClient)
                {
					if(c.cpf == client.cpf)
                    {
						return -1;
                    }
                }
				client.id = GetLastId(listClient);
				listClient.Add(client);
				File.WriteAllText(path, JsonConvert.SerializeObject(listClient));
				return 1;
            }
            else
            {
				return 0;
            }
        }

		public int GetLastId(List<Client> list)
        {
			int id = 0;
			foreach (Client c in list)
			{
				if (c.id > id) {
					id = c.id;
				}
			}
			return ++id;
        }
    }
}
