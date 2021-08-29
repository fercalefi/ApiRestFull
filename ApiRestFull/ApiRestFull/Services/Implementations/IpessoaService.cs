using ApiRestFull.Controllers.Model;
using System.Collections.Generic;

namespace ApiRestFull.Services.Implementations
{
    //Interface para implementação do serviço pessoa onde definimos o contrato das operações (crud)
    public interface IpessoaService
    {
        Pessoa Create(Pessoa pessoa);
        Pessoa FindByYd(long id);
        List<Pessoa> FindAll();
        Pessoa Update(Pessoa pessoa);
        void Delete(long id);


    }
}
