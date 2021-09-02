using System.Text.Json.Serialization;

namespace ApiRestFull.Data.VO
{
    public class PessoaVO
    {
        // altera o nome para exibição
        //[JsonPropertyName("Código")]
        public long Id { get; set; }

        public string Nome { get; set; }
        
        public string SobreNome { get; set; }
        
        public string Endereco { get; set; }

        // descarta a propriedade na serialização do objeto
        //[JsonIgnore]
        public string Genero { get; set; }
        
        public string Email { get; set; }

    }
}
