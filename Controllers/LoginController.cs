using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api_filmes_senai.Domains;
using api_filmes_senai.DTO;
using api_filmes_senai.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api_filmes_senai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
    private readonly IUsuarioRepository _usuarioRepository;
    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDTO.Email!, loginDTO.Senha!);
                if (usuarioBuscado == null)
                {
                    return NotFound("Usuario não encontrado, email ou senha inválidos");
                }
                // Caso o usuario seja encontrado, prossegue para a criação do token

                //1 Passo 1: Definir asClaims() que serão fornecidos no token(Payload)
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti,usuarioBuscado.IdUsuario.ToString()),
                    new Claim (JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),
                    new Claim (JwtRegisteredClaimNames.Name, usuarioBuscado.Nome!),
                    //Podemos definir uma claim personalizada 
                    new Claim("Nome da Claim", "Valor da Claim")
                };

                //Passo 2: Definir a chave de acesso do token 
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autentificacao-webapi-dev"));

                //Passo 3: Definir credenciais do token(Header)
                var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                //Passo 4: Gerar o token 
                var token = new JwtSecurityToken
                    (
                        //emissor do token
                         issuer: "api_filmes_senai",

                         //destinatario do token
                         audience: "api_filmes_senai",

                         //dados definidos nas claims
                         claims: claims,

                         //tempo de expiração no token
                         expires: DateTime.Now.AddMinutes(5),

                         //credencias token
                         signingCredentials: creds
                    );
                return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token)});
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
    
    
}
