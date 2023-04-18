using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskAssigmentApp.Domain.SeedWork;

namespace TaskAssignmentApp.Infrastructure.ORM.EntityFramework
{
  public abstract class EFBaseRepository<TEntity,TContext> : IRepository<TEntity> 
    where TEntity : class
    where TContext:DbContext
  {
    protected readonly TContext dbContext;
    protected readonly DbSet<TEntity> dbSet;

    public EFBaseRepository(TContext context)
    {
      dbContext = context; // database coneectionlarını yöneten reponun kendisi
      dbSet = dbContext.Set<TEntity>(); // dbSet tablonun kendisi

    }

    public virtual async Task Create(TEntity entity)
    {
      await dbSet.AddAsync(entity);

      await dbContext.SaveChangesAsync();
    }

    public virtual async Task<List<TEntity>> ToListAsync()
    {
      return await dbSet.ToListAsync();
    }

    public virtual async Task<List<TEntity>> WhereAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression = null)
    {
      return await dbSet.Where(expression).ToListAsync();
    }


  }
}
