using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskAssigmentApp.Domain.SeedWork
{
  // veriye erişim için bu Interface kullanacağız
  public interface IRepository<TEntity> where TEntity:class
  {

    // ticket where emploeeId=1 and startDate = 10.10.2023 and endDate = 15.10.2023 arasındaki ticketları getir

    /// <summary>
    /// Veri tabanından sorgulamak için
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> expression);

    Task<List<TEntity>> ToListAsync();

    /// <summary>
    /// Veri tabanına kayıt atma işlemi
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task Create(TEntity entity);
   

  }
}
