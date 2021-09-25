using ApiRestFull.Data.Converter.Contract;
using ApiRestFull.Data.VO;
using ApiRestFull.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiRestFull.Data.Converter.Implementations
{
    public class PessoaConverter : IParser<PessoaVO, Pessoa>, IParser<Pessoa, PessoaVO>
    {
        public Pessoa Parse(PessoaVO origin)
        {
            if (origin == null) return null;

            return new Pessoa
            {
                Id = origin.Id,
                Nome = origin.Nome,
                SobreNome = origin.SobreNome,
                Endereco = origin.Endereco,
                Genero = origin.Genero,
                Email = origin.Email,
                Enabled = origin.Enabled
                
            };
        }

        public List<Pessoa> Parse(List<PessoaVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public PessoaVO Parse(Pessoa origin)
        {
            if (origin == null) return null;

            return new PessoaVO
            {
                Id = origin.Id,
                Nome = origin.Nome,
                SobreNome = origin.SobreNome,
                Endereco = origin.Endereco,
                Genero = origin.Genero,
                Email = origin.Email,
                Enabled = origin.Enabled
            };
        }

        public List<PessoaVO> Parse(List<Pessoa> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
