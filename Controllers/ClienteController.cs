using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeChip.Models;
using WeChip.Data;

namespace WeChip.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IRepository _repo;

        public ClienteController(IRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repo.GetAllClientesAsysnc();
                foreach (Cliente cliente in result)
                {
                    if (cliente.IdEndereco > 0)
                    {
                        cliente.Endereco = await _repo.GetEnderecoAsysncById(cliente.IdEndereco);
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var cliente = await _repo.GetClienteAsysncById(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                if (cliente.IdEndereco > 0)
                {
                    cliente.Endereco = await _repo.GetEnderecoAsysncById(cliente.IdEndereco);
                }
                cliente.Status = await _repo.GetStatusAsysncById(cliente.IdStatus);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(Cliente model)
        {
            try
            {
                _repo.Add(model);
                await _repo.SaveChangesAsync();

                var listaStatus = gerarListaStatusPadrao(model.Id);
                model.IdStatus = listaStatus[0].Id;
                model.Status = listaStatus[0];
                _repo.Update(model);
                await _repo.SaveChangesAsync();

                return Ok(model);
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("{idCliente}")]
        public async Task<IActionResult> Put(long idCliente, Cliente model)
        {
            try
            {
                var cliente = await _repo.GetClienteAsysncById(idCliente);
                if(cliente == null)
                {
                    return NotFound();
                }

                _repo.Update(model);
                
                if (await _repo.SaveChangesAsync())
                {
                    if (model.IdEndereco == 0 && model.Endereco != null)
                    {
                        model.IdEndereco = model.Endereco.Id;
                        _repo.Update(model);
                        await _repo.SaveChangesAsync();
                    }
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }

            return BadRequest("Erro não esperado ao atualizar cliente!");
        }


        public List<Status> gerarListaStatusPadrao(long idCliente)
        {
            List<Status> listaRetorno = new List<Status> { };
            List<Status> lista = new List<Status>
             {
                 new Status
                 {
                     Id = 0,
                     Descricao = "Nome Livre",
                     FinalizaCliente = false,
                     ContabilizarVenda = false,
                     Codigo = "0001",
                     IdCliente = idCliente
                 },
                 new Status
                 {
                     Id = 0,
                     Descricao = "Não deseja ser contatado",
                     FinalizaCliente = false,
                     ContabilizarVenda = false,
                     Codigo = "0007",
                     IdCliente = idCliente
                 },
                 new Status
                 {
                     Id = 0,
                     Descricao = "Cliente Aceitou Oferta",
                     FinalizaCliente = false,
                     ContabilizarVenda = false,
                     Codigo = "0009",
                     IdCliente = idCliente
                 },new Status
                 {
                     Id = 0,
                     Descricao = "Caiu a ligação",
                     FinalizaCliente = false,
                     ContabilizarVenda = false,
                     Codigo = "0015",
                     IdCliente = idCliente
                 },new Status
                 {
                     Id = 0,
                     Descricao = "Viajou",
                     FinalizaCliente = false,
                     ContabilizarVenda = false,
                     Codigo = "0019",
                     IdCliente = idCliente
                 },new Status
                 {
                     Id = 0,
                     Descricao = "Falecido",
                     FinalizaCliente = false,
                     ContabilizarVenda = false,
                     Codigo = "0021",
                     IdCliente = idCliente
                 },
             };
            foreach (Status status in lista)
            {
                _repo.Add(status);
                _repo.SaveChangesAsync();
                listaRetorno.Add(status);
            }
            return lista;
        }
    }
}
