using ApiRestFull.Model;
using System.Collections.Generic;

namespace ApiRestFull.Repository
{ 
    //Interface para implementação do serviço pessoa onde definimos o contrato das operações (crud)
    public interface IPessoaRepository
    {
        Pessoa Create(Pessoa pessoa);
        Pessoa FindByYd(long id);
        List<Pessoa> FindAll();
        Pessoa Update(Pessoa pessoa);
        void Delete(long id);
        bool Exists(long id);


    }
}
