using ApiRestFull.Data.Converter.Implementations;
using ApiRestFull.Data.VO;
using ApiRestFull.Model;
using ApiRestFull.Repository;
using ApiRestFull.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFull.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public BookVO Create(BookVO bookVO)
        {
            var book = _converter.Parse(bookVO);
            return _converter.Parse(_repository.Create(book));
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO bookVO)
        {
            var book = _converter.Parse(bookVO);
           return _converter.Parse(_repository.Update(book));
        }
    }
}
