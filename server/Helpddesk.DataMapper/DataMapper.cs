using Mapster;
using System.Collections.Generic;

namespace Helpdesk.DataMapper
{
    //For more information regarding Mapster see: https://github.com/eswann/Mapster
    public class DataMapper<TModel, TEntity> : IDataMapper<TModel, TEntity> where TModel : class where TEntity : class
    {
        /// <summary>
        /// Mapper method to convert model to entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TEntity ModelToEntity(TModel model)
        {
            return TypeAdapter.Adapt<TModel, TEntity>(model);
        }
        /// <summary>
        /// default mapper method to convert entity to model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TModel EntityToModel(TEntity entity)
        {
            return TypeAdapter.Adapt<TEntity, TModel>(entity);
        }
        /// <summary>
        /// method to convert list of model to list of entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> ModelToEntity(IEnumerable<TModel> model)
        {
            return TypeAdapter.Adapt<IEnumerable<TModel>, IEnumerable<TEntity>>(model);
        }
        /// <summary>
        /// method to convert list of entity to list of model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IEnumerable<TModel> EntityToModel(IEnumerable<TEntity> entity)
        {
            return TypeAdapter.Adapt<IEnumerable<TEntity>, IEnumerable<TModel>>(entity);
        }
    }
}
