using api_filmes_senai.Interface;
using api_filmes_senai.Domains;
using api_filmes_senai.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_filmes_senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Endpoint para Cadastrar um Usuario 
        /// </summary>
        /// <param name="id">Id do Gênero Usuario</param>
        /// <returns>Usuario Cadastrado</returns>
        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            try
            {
                _usuarioRepository.Cadastrar(usuario);
                return StatusCode(201, usuario);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        /// <summary>
        /// Endpoint para buscar um Usuario pelo seu Id
        /// </summary>
        /// <param name="id">Id do Usuario buscado</param>
        /// <returns>Usuario Buscado</returns>
        [HttpGet("BuscarPorId/{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorId(id);
                if (usuarioBuscado != null)
                {
                    return Ok(usuarioBuscado);
                }
                return null;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
