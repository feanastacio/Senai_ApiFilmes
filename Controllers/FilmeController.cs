using api_filmes_senai.Domains;
using api_filmes_senai.Interface;
using api_filmes_senai.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_filmes_senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeRepository _filmeRepository;
        public FilmeController(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }


        /// <summary>
        /// Endpoint para Listar um Filme 
        /// </summary>
        /// <param name="id">Id do Filme Listado</param>
        /// <returns>Filme Listado</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Filme> listaDeFilmes = _filmeRepository.Listar();
                return Ok(listaDeFilmes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Endpoint para Cadastrar um Filme 
        /// </summary>
        /// <param name="id">Id do Filme Cadastrado</param>
        /// <returns>Filme Cadastrado</returns>
        [HttpPost]
        public IActionResult Post(Filme novoFilme)
        {
            try
            {
                _filmeRepository.Cadastrar(novoFilme);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Endpoint para buscar um Filme pelo seu Id
        /// </summary>
        /// <param name="id">Id do Filme buscado</param>
        /// <returns>FIlme Buscado</returns>
        [HttpGet("BuscarPorId/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Filme filmeBuscado = _filmeRepository.BuscarPorId(id);
                return Ok(filmeBuscado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _filmeRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Filme filme)
        {
            try
            {
                _filmeRepository.Atualizar(id, filme);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("ListarPorGenero/{id}")]
        public IActionResult GetByGenero(Guid id)
        {
            try
            {
                List<Filme> ListaPorGenero = _filmeRepository.ListarPorGenero(id);
                return Ok(ListaPorGenero);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
        