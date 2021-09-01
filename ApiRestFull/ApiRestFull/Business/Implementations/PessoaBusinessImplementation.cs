using ApiRestFull.Model;
using ApiRestFull.Repository;
using ApiRestFull.Repository.Generic;
using System.Collections.Generic;

namespace ApiRestFull.Business.Implementations
{
    public class PessoaBusinessImplementation : IPessoaBusiness
    {
        // cria a variavel privada _repository para que a mesma seja setada no construtor
        private readonly IRepository<Pessoa> _repository;

        // passa como parametro a classe de context injetada no services
        public PessoaBusinessImplementation(IRepository<Pessoa> repository)
        {
            _repository = repository;
        }

        public List<Pessoa> FindAll()
        {
            return _repository.FindAll();

        }

        public Pessoa FindById(long id)
        {
            return _repository.FindById(id) ;
        }

        public Pessoa Create(Pessoa pessoa)
        {
            
            return _repository.Create(pessoa);
        }

        public Pessoa Update(Pessoa pessoa)
        {
            return _repository.Update(pessoa);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);    
            
        }

    }
}
