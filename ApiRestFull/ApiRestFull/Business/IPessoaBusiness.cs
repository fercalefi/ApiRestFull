using ApiRestFull.Data.VO;
using System.Collections.Generic;

namespace ApiRestFull.Business
{
    //Interface para implementação do serviço pessoa onde definimos o contrato das operações (crud)
    public interface IPessoaBusiness
    {
        PessoaVO Create(PessoaVO pessoa);
        PessoaVO FindById(long id);
        List<PessoaVO> FindAll();
        PessoaVO Update(PessoaVO pessoa);
        void Delete(long id);


    }
}
