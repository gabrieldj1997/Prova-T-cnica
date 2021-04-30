using Hangfire;
using SourceConsole.Models;
using SourceConsole.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SourceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Registro de clientes");
            IO io = new IO();
            ClientDal cl = new ClientDal();
            while (true)
            {
                IList<Client> temList = cl.GetListClientTemp(io._tempFile);
                if(temList != null)
                {
                    foreach(Client c in temList)
                    {
                        if(cl.CpfValidation(c.cpf) == 1)
                        {
                            int controle = cl.RegisterClient(c, io._finalFile);
                            if (controle == 1)
                            {
                                Console.WriteLine(DateTime.Now.ToString("MM/dd hh:mm:ss") +
                                    $"Client nome={c.nome}; cpf={c.cpf}; cadastrado");
                            }else if(controle == -1)
                            {
                                Console.WriteLine(DateTime.Now.ToString("MM/dd hh:mm:ss") +
                                    $"Client nome={c.nome}; cpf={c.cpf}; ja cadastrado");
                            }
                        }
                        else
                        {
                            Console.WriteLine(DateTime.Now.Date.ToShortDateString() + ": " +
                                $"Cliente \n nome :{c.nome} ; cpf : {c.cpf} \n" +
                                $"Não cadastrado pois o cpf é invalido.");
                            io.Log(DateTime.Now.Date.ToShortDateString() + ": "+
                                $"Cliente \n nome : {c.nome} ; cpf : {c.cpf} \n" +
                                $"Não cadastrado pois o cpf é invalido.") ;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(DateTime.Now.ToString("MM/dd hh:mm:ss") +  " : Sem cadastro");
                    io.Log(DateTime.Now.ToString("MM/dd hh:mm:ss") + " : Sem cadastro");
                }
                System.Threading.Thread.Sleep(30000);
            }
        }
    }
}
