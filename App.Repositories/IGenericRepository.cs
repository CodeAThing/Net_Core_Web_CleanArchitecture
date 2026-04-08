using System.Linq.Expressions;

namespace App.Repositories
{
    public interface IGenericRepository<T> where T: class // Tüm entity'ler için ortak CRUD işlemlerini tek tek yazmak yerine,
                                                            // generic bir repository sözleşmesi tanımlıyoruz.
    {
        IQueryable<T> GetAll(); //Sorguya sonradan filtreleme, sıralama ve sayfalama ekleyebilmek için IQueryable döndürüyoruz
                                //Dbdeki ilgili entitylerin
                                //tamamını sorgulanabilir bir şekilde döndürmeye yarıyor.

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        ValueTask<T?> GetByIdAsync(int id); //Task da kullanabilirdik

        ValueTask AddAsync(T entity);

        void Update(T entity);
        void Delete(T entity);
        
    }
}
