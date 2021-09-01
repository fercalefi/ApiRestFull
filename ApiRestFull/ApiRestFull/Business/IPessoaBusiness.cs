using ApiRestFull.Model;
using System.Collections.Generic;

namespace ApiRestFull.Business
{
    //Interface para implementação do serviço pessoa onde definimos o contrato das operações (crud)
    public interface IPessoaBusiness
    {
        Pessoa Create(Pessoa pessoa);
        Pessoa FindById(long id);
        List<Pessoa> FindAll();
        Pessoa Update(Pessoa pessoa);
        void Delete(long id);


    }
}
