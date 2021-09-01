using ApiRestFull.Model;
using ApiRestFull.Model.Base;
using System.Collections.Generic;

namespace ApiRestFull.Repository.Generic
{ 
    //Interface para implementação do serviço pessoa onde definimos o contrato das operações (crud)
    public interface IRepository<T> where T: BaseEntity
    {
        T Create(T item);
        T FindById(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exists(long id);


    }
}
