using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Services.Models;
using Projeto.DAL.Contracts;
using Projeto.DAL.Entities;
using Microsoft.AspNetCore.Cors;

namespace Projeto.Services.Controllers
{
    [EnableCors("DefaultPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(ClienteCadastroModel model, [FromServices]IClienteRepository repository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Cliente cliente = new Cliente();
                    cliente.Nome = model.Nome;
                    cliente.Email = model.Email;

                    repository.Inserir(cliente);


                    return Ok($"Cliente {cliente.Nome} cadastrado com sucesso");
                }
                catch(Exception e)
                {
                    return StatusCode(500, e.Message);

                }

            }
            else
            {
                return StatusCode(400, "Ocorreram erro de validação");
            }
        }
        
        [HttpPut]
        public IActionResult Put(ClienteEdicaoModel model,[FromServices]IClienteRepository repository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = model.IdCliente;
                    cliente.Nome = model.Nome;
                    cliente.Email = model.Email;

                    repository.Atualizar(cliente);


                    return Ok($"Cliente {cliente.Nome} atualizado com sucesso");
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);

                }

            }
            else
            {
                return StatusCode(400, "Ocorreram erro de validação");
            }
        }
    

        [HttpDelete("{idCliente}")]
        public IActionResult Delete(int idCliente, [FromServices]IClienteRepository repository)
        {
            try
            {
                repository.Deletar(idCliente);
                return Ok("Cliente excluído com sucesso");
            }
            catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll([FromServices]IClienteRepository repository)
        {
            try
            {
                List<ClienteConsultaModel> lista = new List<ClienteConsultaModel>();

                foreach (var item in repository.ObterDados())
                {
                    ClienteConsultaModel model = new ClienteConsultaModel();
                    model.IdCliente = item.IdCliente;
                    model.Nome = item.Nome;
                    model.Email = item.Email;
                    model.DataCriacao = item.DataCriacao;

                    lista.Add(model);
                }

                return Ok(lista);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{idCliente}")]
        public IActionResult GetById(int idCliente, [FromServices]IClienteRepository repository)
        {
            try
            {
                Cliente cliente = repository.ObterPorId(idCliente);

                ClienteConsultaModel model = new ClienteConsultaModel();
                model.IdCliente = cliente.IdCliente;
                model.Nome = cliente.Nome;
                model.Email = cliente.Email;
                model.DataCriacao = cliente.DataCriacao;

                return Ok(model);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }
    }

        
















}