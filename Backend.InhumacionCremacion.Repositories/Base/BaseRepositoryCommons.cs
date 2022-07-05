namespace Backend.InhumacionCremacion.Repositories.Base
{
    /// <summary>
    /// BaseRepositoryCommons
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepositoryCommons<T> : BaseRepository<T>, Entities.Interface.Repository.IBaseRepositoryCommons<T> where T : class
    {
        /// <summary>
        /// BaseRepositoryCommons
        /// </summary>
        /// <param name="context"></param>
        public BaseRepositoryCommons(Context.CommonsContext context)
        {
            AppContext = context;
        }
    }
}