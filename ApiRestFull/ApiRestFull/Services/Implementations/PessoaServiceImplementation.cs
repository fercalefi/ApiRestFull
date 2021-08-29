using ApiRestFull.Controllers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiRestFull.Services.Implementations
{
    public class PessoaServiceImplementation : IPessoaService
    {
        private volatile int count;

        public Pessoa Create(Pessoa pessoa)
        {
            // mock
            return pessoa;
        }

        public void Delete(long id)
        {
            
        }

        public List<Pessoa> FindAll()
        {
            List<Pessoa> pessoas = new List<Pessoa>();

            for (int i = 0; i < 8; i++)
            {
                Pessoa pessoa = MockPessoa(i);
                pessoas.Add(pessoa);
            }


            return pessoas;
        }

        public Pessoa FindByYd(long id)
        {
            return new Pessoa
            {
                Id = IncrementAndGet(),
                Nome = "Fernando" ,
                SobreNome = "Calefi",
                Email = "fercalefi@gmail.com",
                Genero = "Masculino",
                Endereco = "Rua Ida Roncolato Nogueira 180"
            };
        }

        public Pessoa Update(Pessoa pessoa)
        {
            // teoricamente iria ate a base de dados faria o update e retornaria o objeto
            return pessoa;
        }
        private Pessoa MockPessoa(int i)
        {
            return new Pessoa
            {
                Id = IncrementAndGet(),
                Nome = "Fernando " + i,
                SobreNome = "Calefi",
                Email = "fercalefi@gmail.com",
                Genero = "Masculino",
                Endereco = "Rua Ida Roncolato Nogueira 180"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
