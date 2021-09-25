using ApiRestFull.Model;
using ApiRestFull.Model.Context;
using ApiRestFull.Repository.Generic;
using System;
using System.Linq;

namespace ApiRestFull.Repository.Implementations
{
    public class PessoaRepositoryImplementation : GenericRepository<Pessoa>, IPessoaRepository
    {
        // construtor pegando o contexto da classe base. Por isso a mudança para protectetd no context
        public PessoaRepositoryImplementation(MySQLContext context) : base(context) { }

        public Pessoa Disable(long id)
        {
           if (!_context.Pessoas.Any(p => p.Id.Equals(id))) return null;

            var user = _context.Pessoas.SingleOrDefault(p => p.Id.Equals(id));

            if (user != null)
            {
                user.Enabled = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return user;
        }
    }
}
