using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ACTIVIDAD_FORMULARIO.Models
{
    public class AlumnoContext : DbContext
    {
        public DbSet<Alumno> Alumnos { get; set; }

        public AlumnoContext(DbContextOptions<AlumnoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
