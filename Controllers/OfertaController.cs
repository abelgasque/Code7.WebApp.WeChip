using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeChip.Data;
using WeChip.Models;

namespace WeChip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaController : ControllerBase
    {
        private readonly IRepository _repo;

        public OfertaController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("por-cliente/{id}")]
        public async Task<IActionResult> GetOfertasPorCliente(long id)
        {
            try
            {
                var lista = await _repo.GetOfertasPorClienteIdAsysnc(id);
                List<Oferta> listaRetorno = new List<Oferta> { };

                foreach (Oferta oferta in lista)
                {
                    if (oferta.IdStatus > 0)
                    {
                        oferta.Status = await _repo.GetStatusAsysncById(oferta.IdStatus);
                    }
                    if (oferta.IdProduto > 0)
                    {
                        oferta.Produto = await _repo.GetProdutoAsysncById(oferta.IdProduto);
                    }
                    listaRetorno.Add(oferta);
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(Oferta model)
        {
            try
            {
                
                if(model.Cliente != null && model.Cliente.Id > 0)
                {
                    var status = new Status
                    {
                        Id = 0,
                        Descricao = "Nome Livre",
                        FinalizaCliente = false,
                        ContabilizarVenda = false,
                        Codigo = "0001",
                        IdCliente = model.Cliente.Id
                    };
                    model.IdCliente = model.Cliente.Id;
                    model.Status = status;
                    model.Cliente = null;
                }
                if (model.Produto != null && model.Produto.Id > 0)
                {
                    model.IdProduto = model.Produto.Id;
                    model.Produto = null;
                }
                _repo.Add(model);
                await _repo.SaveChangesAsync();

                if(model.IdProduto == 0)
                {
                    model.IdProduto = model.Produto.Id;
                }
                model.IdStatus = model.Status.Id;

                _repo.Update(model);

                await _repo.SaveChangesAsync();

                return Ok(model);
                
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
    }
}
