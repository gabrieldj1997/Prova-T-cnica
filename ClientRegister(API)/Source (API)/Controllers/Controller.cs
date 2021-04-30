using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Source__API_.Models;
using Source__API_.Utils;

namespace Source__API_.Controllers
{
    [ApiController]
    [Route("api")]
    public class Controller : ControllerBase
    {
        IO io = new IO();
        ClientOp cl = new ClientOp();
        [HttpGet]
        [Route("client")]
        public ActionResult GetClients() {
            try
            {
                IList<Client> clients = cl.GetListClient(io._finalFile);
                return Ok(clients);
            }catch(Exception)
            {
                return Ok("Sem clientes cadastrados no momento;");
            }
        }

        [HttpGet]
        [Route("client/{id}")]
        public ActionResult GetClient(int id)
        {
            try
            {
                Client client = cl.GetClient(id, io._finalFile);
                if (client != null)
                {
                    return Ok(client);
                }
                else
                {
                    return Ok("Sem clientes com esse id, por favor consulte os clientes cadastrados para" +
                                        " inserir um id valido");
                }
            }catch(Exception)
            {
                return Ok("Sem clientes com esse id, por favor consulte os clientes cadastrados para" +
                    " inserir um id valido");
            }

        }

        [HttpPost]
        [Route("register/client")]
        public ActionResult RegisterClient([FromBody] Client client)
        {
            try
            {
                int i = cl.RegisterClient(client, io._tempFile);
                if(i == 1)
                {
                    return Ok("Cliente cadastrado");
                }
                else
                {
                    return Ok("Cliente não cadastrado, por favor insira somente nome e cpf, os 2 em formato string e json");
                }
            }catch(Exception)
            {
                return Ok("cliente não cadastrado, por favor utilize o seguinte modelo json: {'nome':'Gabriel','cpf':'06442151190'}'");
            }
        }
        [HttpGet]
        [Route("test")]
        public ActionResult Test()
        {
            return Ok("Aplicação no ar!!!");
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public ActionResult DeleteRegister(int id)
        {
            Client client = cl.DeleteClient(id, io._finalFile);
            if(client != null)
            {
                return Ok(client);
            }
            else
            {
                return Ok(@"Nenhum client com id {id} encontrado, por favor verifique a tabela de clientes.");
            }
        }
    }
}
