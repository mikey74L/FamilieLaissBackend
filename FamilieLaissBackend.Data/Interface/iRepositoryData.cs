using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FamilieLaissBackend.Data.Interface
{
    /// <summary>
    /// Contract with the  generic methods for all the Entities
    /// </summary>
    public interface iRepositoryData<TEntity> where TEntity : class
    {
        //Alle Entitites ermitteln
        IQueryable<TEntity> All();

        //Die erste oder eine default Entity anhand des Predicates finden
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        //Die erste Entity anhand des Predicates finden
        TEntity First(Expression<Func<TEntity, bool>> predicate);

        //Die Entity hinzufügen
        void Add(TEntity entity);

        //Die Entity löschen
        void Delete(TEntity entity);

        //Bestehende Entity zum Context hinzufügen
        void Attach(TEntity entity);
    }
}
