using api_filmes_senai.Domains;

namespace api_filmes_senai.Interface
{
    public interface IFilmeRepository
    {
        void Cadastrar(Filme novoFilme);
        List<Filme> Listar();
        void Atualizar(Guid id, Filme filme);
        void Deletar(Guid id);
        Filme BuscarPorId(Guid id);
        //Listar os filmes pelo seu genero - filtro
        List<Filme> ListarPorGenero(Guid IdGenero);
    }
}
