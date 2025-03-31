using Microsoft.AspNetCore.Mvc;

namespace rinha_de_backend_2023.Controllers
{
    [ApiController]
    public class PersonController : ControllerBase
    {
        // Endpoint especial para contagem de pessoas cadastradas.
        [HttpGet("contagem-pessoas")]
        public IActionResult ContagemPessoas() {
            return Ok(1);
        }

        // Para criar um recurso pessoa.
        [HttpPost("pessoas")]
        public IActionResult PostPessoa() {
            return Created();
        }

        // Para consultar um recurso criado com a requisição anterior.
        [HttpGet("pessoas/{id}")]
        public IActionResult GetPessoa([FromRoute] Guid id) {
            return Ok(id);
        }

        // Para fazer uma busca por pessoas.
        [HttpGet("pessoas")]
        public IActionResult SearchPessoas([FromQuery] String t) {
            return Ok(t);
        }
    }
}
