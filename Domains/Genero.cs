using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_filmes_senai.Domains
{
    [Table("Genero")]
    public class Genero
    {
        [Key]

        public Guid IdGenero { get; set; }

        [Column(TypeName = "VARCHAR(30)")]
        [Required(ErrorMessage = "Nome do Gênero e obrigatório!")]
        public string ? Nome { get; set; }
    }
}
