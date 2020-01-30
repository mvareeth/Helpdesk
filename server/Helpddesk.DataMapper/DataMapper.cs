using Mapster;
using System.Collections.Generic;

namespace Helpdesk.DataMapper
{
    //For more information regarding Mapster see: https://github.com/eswann/Mapster
    public class DataMapper<TModel, TEntity> : IDataMapper<TModel, TEntity> where TModel : class where TEntity : class
    {
        public TEntity ModelToEntity(TModel model)
        {
            return TypeAdapter.Adapt<TModel, TEntity>(model);
        }

        public TModel EntityToModel(TEntity entity)
        {
            return TypeAdapter.Adapt<TEntity, TModel>(entity);
        }

        public IEnumerable<TEntity> ModelToEntity(IEnumerable<TModel> model)
        {
            return TypeAdapter.Adapt<IEnumerable<TModel>, IEnumerable<TEntity>>(model);
        }

        public IEnumerable<TModel> EntityToModel(IEnumerable<TEntity> entity)
        {
            return TypeAdapter.Adapt<IEnumerable<TEntity>, IEnumerable<TModel>>(entity);
        }
    }
}
