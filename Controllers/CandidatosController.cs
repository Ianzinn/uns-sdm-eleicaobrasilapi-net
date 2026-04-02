using EleicaoBrasilApi.Data;
using EleicaoBrasilApi.models;
using Microsoft.AspNetCore.Mvc;

namespace EleicaoBrasilApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CandidatosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var candidatos = _context.Candidatos.ToList();
            return Ok(candidatos);
        }

        [HttpGet("partido/{nomeDoPartido}")]
        public IActionResult GetPorPartido(string nomeDoPartido)
        {
            var candidatos = _context.Candidatos
                .Where(c => c.Partido == nomeDoPartido)
                .ToList();

            return Ok(candidatos);
        }

        [HttpPost]
        public IActionResult Post(Candidato candidato)
        {
            if (_context.Candidatos.Any(c => c.Numero == candidato.Numero))
            {
                return BadRequest("Número já cadastrado");
            }

            _context.Candidatos.Add(candidato);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = candidato.Id }, candidato);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Candidato candidato)
        {
            var candidatoExistente = _context.Candidatos.Find(id);

            if (candidatoExistente == null)
            {
                return NotFound();
            }

            candidatoExistente.Nome = candidato.Nome;
            candidatoExistente.Numero = candidato.Numero;
            candidatoExistente.Partido = candidato.Partido;
            candidatoExistente.ViceNome = candidato.ViceNome;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var candidato = _context.Candidatos.Find(id);

            if (candidato == null)
            {
                return NotFound();
            }

            _context.Candidatos.Remove(candidato);
            _context.SaveChanges();

            return NoContent();
        }
    }
}