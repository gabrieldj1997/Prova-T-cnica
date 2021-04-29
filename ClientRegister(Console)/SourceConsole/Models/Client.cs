using System;
using System.Collections.Generic;
using System.Text;

namespace SourceConsole.Models
{
    class Client
    {
        public Client(int id, string nome, string cpf)
        {
            this.id = id;
            this.nome = nome;
            this.cpf = cpf;
        }
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }

    }
}
