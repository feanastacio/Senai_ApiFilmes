using api_filmes_senai.Context;
using api_filmes_senai.Domains;
using api_filmes_senai.Interface;

namespace api_filmes_senai.Repositories
{
    /// <summary>
    /// Classe que vai implementar a IGeneroRepository
    /// ou seja, vamos implementar os metodos, toda logica dos metodos 
    /// </summary>
    public class GeneroRepository : IGeneroRepository
    {
        private readonly Filme_Context _context;

        /// <summary>
        /// Construtor do repositorio
        /// Toda ve que o construtor forchamado, 
        /// os dados estarao disoiniveis 
        /// </summary>
        /// <param name="contexto"></param>
        public GeneroRepository(Filme_Context contexto)
        {
            _context = contexto;
        }

        public void Atualizar(Guid id, Genero novoGenero)
        {
            try
            {
                Genero generoBuscado = _context.Genero.Find(id)!;

                if (generoBuscado != null)
                {
                    generoBuscado.Nome = novoGenero.Nome;
                }

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Genero BuscarPorId(Guid id)
        {
            try
            {
                Genero generoBuscado = _context.Genero.Find(id)!;
                return generoBuscado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Cadastrar(Genero novoGenero)
        {
            try
            {
                //Adiciona um novo genêro na tabela Generos (BD)
                _context.Genero.Add(novoGenero);

                //Após o cadastro, salva as alterações(BD)
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Deletar(Guid id)
        {
            try
            {
                Genero generoBuscado = _context.Genero.Find(id)!;
                if (generoBuscado != null)
                {
                    _context.Genero.Remove(generoBuscado);
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Genero> Listar()
        {
            try
            {
                List<Genero> listaGeneros = _context.Genero.ToList();
                return listaGeneros;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
