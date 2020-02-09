using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace TaskMgr.Tests
{
    public class TestDbSet<T>: DbSet<T>, IQueryable, IEnumerable<T>
        where T:class
    {
        ObservableCollection<T> _data;
        IQueryable _query;

        public TestDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public override T Add(T entity)
        {
            _data.Add(entity);
            return entity;
        }

        public override T Remove(T entity)
        {
            _data.Remove(entity);
            return entity;
        }

        public override T Attach(T entity)
        {
            _data.Add(entity);
            return entity;
        }

        public override T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<T> Local => new ObservableCollection<T>(_data);

        Type IQueryable.ElementType => _query.ElementType;

        System.Linq.Expressions.Expression IQueryable.Expression => _query.Expression;

        IQueryProvider IQueryable.Provider => _query.Provider;

        IEnumerator IEnumerable.GetEnumerator() => _query.GetEnumerator();
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return null;
        }
    }
}
