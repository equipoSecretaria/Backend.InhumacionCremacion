using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Backend.InhumacionCremacion.Repositories.Config;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Context
{
    /// <summary>
    /// InhumacionCremacionContext
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public partial class InhumacionCremacionContext : DbContext
    {
        #region Attributes        
        public virtual DbSet<DatosCementerio> DatosCementerio { get; set; }
        public virtual DbSet<DocumentosSoporte> DocumentosSoporte { get; set; }
        public virtual DbSet<InstitucionCertificaFallecimiento> InstitucionCertificaFallecimiento { get; set; }
        public virtual DbSet<LugarDefuncion> LugarDefuncion { get; set; }
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Solicitud> Solicitud { get; set; }
        public virtual DbSet<UbicacionPersona> UbicacionPersona { get; set; }
        public virtual DbSet<Seguimiento> Seguimiento { get; set; }
		public virtual DbSet<Formatos> Formatos { get; set; }
		public virtual DbSet<ResumenSolicitud> ResumenSolicitud { get; set; }

        public virtual DbSet<EstadoDocumentosSoporte> EstadoDocumentosSoporte { get; set; }
        #endregion

        #region Constructor                        
        /// <summary>
        /// Initializes a new instance of the <see cref="InhumacionCremacionContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public InhumacionCremacionContext(DbContextOptions<InhumacionCremacionContext> options)
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
            modelBuilder.AddDatosCementerio();
            modelBuilder.AddDocumentosSoporte();
            modelBuilder.AddInstitucionCertificaFallecimiento();
            modelBuilder.AddLugarDefuncion();
            modelBuilder.AddPersona();
            modelBuilder.AddSolicitud();
            modelBuilder.AddUbicacionPersona();
            modelBuilder.AddSeguimiento();
            modelBuilder.AddFormatos();
			modelBuilder.AddResumenSolicitud();
            modelBuilder.AddEstadoDocumentosSoporte();
        }
        #endregion
    }
}
