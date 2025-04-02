using Microsoft.AspNetCore.Mvc;
using rinha_de_backend_2023.Contracts;
using rinha_de_backend_2023.Entities;

namespace rinha_de_backend_2023.Controllers {
    [ApiController]
    public class PersonController : ControllerBase {
        private readonly IPersonRepository _repository;

        public PersonController(IPersonRepository repository) { _repository = repository; }

        // Endpoint especial para contagem de pessoas cadastradas.
        [HttpGet("contagem-pessoas")]
        public async Task<IActionResult> ContagemPessoas() {
            var count = await _repository.GetPersonCount();
            return Ok(count);
        }

        // Para criar um recurso pessoa.
        [HttpPost("pessoas")]
        public async Task<IActionResult> PostPessoa(Person pessoa) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try {
                var createdPerson = await _repository.AddPerson(pessoa);
                // Console.WriteLine(createdPerson.ToString());

                return CreatedAtAction(nameof(GetPessoa), new { id = createdPerson.Id }, createdPerson);
            }
            catch (Exception) {
                // Console.WriteLine(ex.Message);
                return UnprocessableEntity();
            }
        }

        // Para consultar um recurso criado com a requisição anterior.
        [HttpGet("pessoas/{id}")]
        public async Task<IActionResult> GetPessoa([FromRoute] Guid id) {
            try {
                var person = await _repository.GetPersonById(id);
                return Ok(person);
            }
            catch (Exception) {
                // Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        // Para fazer uma busca por pessoas.
        [HttpGet("pessoas")]
        public async Task<IActionResult> SearchPessoas([FromQuery] string t) {
            return Ok(await _repository.SearchPeople(t));
        }
    }
}
