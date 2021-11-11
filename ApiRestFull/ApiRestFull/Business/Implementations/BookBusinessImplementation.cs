using ApiRestFull.Data.Converter.Implementations;
using ApiRestFull.Data.VO;
using ApiRestFull.Hypermedia.Utils;
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

        public PagedSearchVO<BookVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            // Montando a query dinamica de acordo com os parametros
            string query = @"select * from books p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $"and p.title like '%{name}%' ";
            query += $"order by p.title {sort} limit {size} offset {offset} ";



            string countQuery = @"select count(1) from books p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $"and p.title like '%{name}%' ";

            var books = _repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchVO<BookVO>
            {
                CurrentPage = page,
                List = _converter.Parse(books),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        }


        public BookVO Update(BookVO bookVO)
        {
            var book = _converter.Parse(bookVO);
           return _converter.Parse(_repository.Update(book));
        }
    }
}
