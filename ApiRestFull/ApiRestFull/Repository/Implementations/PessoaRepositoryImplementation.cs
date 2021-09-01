using ApiRestFull.Model;
using ApiRestFull.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiRestFull.Repository.Implementations
{
    public class PessoaRepositoryImplementation : IPessoaRepository
    {
        // cria a variavel privada _context para que a mesma seja setada no construtor com os dados da conexão
        private MySQLContext _context;

        // passa como parametro a classe de context injetada no services
        public PessoaRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public List<Pessoa> FindAll()
        {
            return _context.Pessoas.ToList();
        }

        public Pessoa FindByYd(long id)
        {
            return _context.Pessoas.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Pessoa Create(Pessoa pessoa)
        {
            try
            {
                _context.Add(pessoa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return pessoa;
        }

        public Pessoa Update(Pessoa pessoa)
        {
            if (!Exists(pessoa.Id)) return null;

            var result = _context.Pessoas.SingleOrDefault(p => p.Id.Equals(pessoa.Id));

            if (result != null)
            {
                try
                {

                    _context.Entry(result).CurrentValues.SetValues(pessoa);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }

            return pessoa;
        }

        public void Delete(long id)
        {
            var result = _context.Pessoas.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                _context.Pessoas.Remove(result);
                _context.SaveChanges();
            }      
            
        }

                       

        public bool Exists(long id)
        {
            return _context.Pessoas.Any(p => p.Id.Equals(id));
        }
    }
}
