using Backend.InhumacionCremacion.Repositories.Context.Config.Commons;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Context
{
    /// <summary>
    /// InhumacionCremacionContext
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public partial class CommonsContext : DbContext
    {
        #region Attributes        
        public virtual DbSet<Entities.Models.Commons.Dominio> Dominio { get; set; }
        public virtual DbSet<Entities.Models.Commons.TipoDominio> TipoDominio { get; set; }
        #endregion

        #region Constructor  

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public CommonsContext(DbContextOptions<CommonsContext> options)
            : base(options)
        {
        }
        #endregion

        #region Model Creating         
        /// <summary>
        /// OnModel Creating.
        /// </summary>
        /// <param name="modelBuilder">Model Builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddDominio();
            modelBuilder.AddTipoDominio();
            modelBuilder.AddDepartamento();
            modelBuilder.AddMunicipio();
        }
        #endregion
    }
}
