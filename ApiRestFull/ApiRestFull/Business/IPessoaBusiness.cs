using ApiRestFull.Data.VO;
using ApiRestFull.Hypermedia.Utils;
using System.Collections.Generic;

namespace ApiRestFull.Business
{
    //Interface para implementação do serviço pessoa onde definimos o contrato das operações (crud)
    public interface IPessoaBusiness
    {
        PessoaVO Create(PessoaVO pessoa);
        PessoaVO FindById(long id);
        List<PessoaVO> FindByName(string firstName, string lastName);
        List<PessoaVO> FindAll();
        PessoaVO Update(PessoaVO pessoa);
        void Delete(long id);
        PessoaVO Disable(long id);
        PagedSearchVO<PessoaVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page);


    }
}
