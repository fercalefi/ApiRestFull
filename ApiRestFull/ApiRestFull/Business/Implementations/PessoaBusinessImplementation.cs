using ApiRestFull.Data.Converter.Implementations;
using ApiRestFull.Data.VO;
using ApiRestFull.Model;
using ApiRestFull.Repository;
using ApiRestFull.Repository.Generic;
using System.Collections.Generic;

namespace ApiRestFull.Business.Implementations
{
    public class PessoaBusinessImplementation : IPessoaBusiness
    {
        // cria a variavel privada _repository para que a mesma seja setada no construtor
        private readonly IPessoaRepository _repository;
        private readonly PessoaConverter _converter;

        // passa como parametro a classe de context injetada no services
        public PessoaBusinessImplementation(IPessoaRepository repository)
        {
            _repository = repository;
            _converter = new PessoaConverter();
        }

        public List<PessoaVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());

        }

        public PessoaVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id)) ;
        }

        public List<PessoaVO> FindByName(string firstName, string lastName)
        {
            return _converter.Parse(_repository.FindByName(firstName, lastName));
        }

        public PessoaVO Create(PessoaVO pessoaVO)
        {
            var pessoa = _converter.Parse(pessoaVO);
            return _converter.Parse(_repository.Create(pessoa));
        }

        public PessoaVO Update(PessoaVO pessoaVO)
        {
            var pessoa = _converter.Parse(pessoaVO);
            return _converter.Parse(_repository.Update(pessoa));
        }

        public void Delete(long id)
        {
            _repository.Delete(id);    
            
        }

        public PessoaVO Disable(long id)
        {
            var pessoaEntity = _repository.Disable(id);
            return _converter.Parse(pessoaEntity);
        }


    }
}
