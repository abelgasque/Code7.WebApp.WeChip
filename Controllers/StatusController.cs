using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeChip.Data;

namespace WeChip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IRepository _repo;

        public StatusController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("por-cliente/{id}")]
        public async Task<IActionResult> GetOfertasPorCliente(long id)
        {
            try
            {
                var lista = await _repo.GetStatusPorClienteIdAsysnc(id);
                
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }
    }
}
