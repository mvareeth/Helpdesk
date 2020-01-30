using System.Collections.Generic;

namespace Helpdesk.DataMapper
{
    public interface IDisposableWrapper { }

    public interface IDataMapper<TModel, TEntity> : IDisposableWrapper where TModel : class where TEntity : class
    {
        TEntity ModelToEntity(TModel model);

        TModel EntityToModel(TEntity entity);

        IEnumerable<TEntity> ModelToEntity(IEnumerable<TModel> model);

        IEnumerable<TModel> EntityToModel(IEnumerable<TEntity> entity);

    }
}
