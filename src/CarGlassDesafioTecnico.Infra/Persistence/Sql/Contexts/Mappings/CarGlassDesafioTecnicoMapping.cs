using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace CarGlassDesafioTecnico.Infra.Persistence.Sql.Contexts.Mappings
{
    [ExcludeFromCodeCoverage]
    public class CarGlassDesafioTecnicoMapping : IEntityTypeConfiguration<Domain.Entities.Usuario>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(c => c.Id);
            

        }
    }
}
