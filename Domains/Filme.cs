using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace api_filmes_senai.Domains
{

    [Table("Filme")]
    public class Filme
    {
        [Key]
        public Guid IdFilme { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [Required(ErrorMessage = "O nome do Filme e obrigatório!")]
        public string ? Titulo { get; set; }

        /// <summary>
        /// Refência da tabela Gênero
        /// </summary>
        public Guid IdGenero { get; set; }

        [ForeignKey("IdGenero")]
        public Genero ? Genero { get; set; }

    }
}
