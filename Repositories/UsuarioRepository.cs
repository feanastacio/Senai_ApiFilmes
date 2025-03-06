using api_filmes_senai.Context;
using api_filmes_senai.Domains;
using api_filmes_senai.Interface;
using api_filmes_senai.Uteis;

namespace api_filmes_senai.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Filme_Context _context;
        public UsuarioRepository(Filme_Context context)
        {
            _context = context;
        }
        public Usuario BuscarPorEmailESenha(string email, string senha)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuario.FirstOrDefault(u => u.Email == email)!;
                if (usuarioBuscado != null)
                {
                    bool confere = Criptografia.CompararHash(senha, usuarioBuscado.Senha!);
                    if (confere)
                    {
                        return usuarioBuscado;
                    }
                }
                return null!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Usuario BuscarPorId(Guid id)
        {
            Usuario usuarioBuscado = _context.Usuario.Find(id)!;

            if (usuarioBuscado != null)
            {
                return usuarioBuscado;
            }
            return null!;
        }

        public void Cadastrar(Usuario novoUsuario)
        {
            try
            {
                novoUsuario.Senha = Criptografia.GerarHash(novoUsuario.Senha!);
                _context.Usuario.Add(novoUsuario);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
