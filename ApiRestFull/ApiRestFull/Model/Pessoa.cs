using ApiRestFull.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFull.Model
{
    // a nottation Table define o bind entre o nome da classe e o nome da tabela no banco de dados
    [Table("pessoa")]
    public class Pessoa: BaseEntity
    {
        // a nottation column faz o bind entre o nome da propriedade e a coluna no banco de dados
        [Column("nome")]
        public string Nome { get; set; }
        
        [Column("sobrenome")]
        public string SobreNome { get; set; }
        
        [Column("endereco")]
        public string Endereco { get; set; }

        [Column("genero")] 
        public string Genero { get; set; }
        
        [Column("email")]
        public string Email { get; set; }

    }
}
