using ApiRestFull.Model;
using ApiRestFull.Repository.Generic;
using System.Collections.Generic;

namespace ApiRestFull.Repository
{ 
    //Interface para implementação do serviço pessoa onde definimos o contrato das operações (crud)
    public interface IPessoaRepository: IRepository<Pessoa>
    {
        Pessoa Disable(long id);

        List<Pessoa> FindByName(string firstName, string secondName);
    }
}
