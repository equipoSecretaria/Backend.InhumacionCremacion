namespace Backend.InhumacionCremacion.Repositories.Base
{
    /// <summary>
    /// BaseRepositoryInhumacionCremacion
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Backend.InhumacionCremacion.Repositories.Base.BaseRepository&lt;T&gt;" />
    /// <seealso cref="Backend.InhumacionCremacion.Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion&lt;T&gt;" />
    public class BaseRepositoryInhumacionCremacion<T> : BaseRepository<T>, Entities.Interface.Repository.IBaseRepositoryInhumacionCremacion<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepositoryInhumacionCremacion{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BaseRepositoryInhumacionCremacion(Context.InhumacionCremacionContext context)
        {
            AppContext = context;
        }
    }
}