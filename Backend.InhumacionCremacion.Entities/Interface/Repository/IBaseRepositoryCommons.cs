namespace Backend.InhumacionCremacion.Entities.Interface.Repository
{
    /// <summary>
    /// IBaseRepositoryCommons
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepositoryCommons<T> : IBaseRepository<T> where T : class
    {
    }
}
