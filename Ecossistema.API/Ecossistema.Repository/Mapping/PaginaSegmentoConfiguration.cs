using Ecossistema.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecossistema.Data.Mapping
{
    public class PaginaSegmentoConfiguration : IEntityTypeConfiguration<PaginaSegmento>
    {
        public void Configure(EntityTypeBuilder<PaginaSegmento> builder)
        {
            builder.ToTable("PaginaSegmento", "dbo")
                     .HasKey(x => x.Id);

            builder.HasOne(x => x.Pagina)
                .WithMany(x => x.PaginasSegmentos)
                .HasForeignKey(y => y.PaginaId);

            builder.HasOne(x => x.TipoSegmento)
                .WithMany(x => x.PaginasSegmentos)
                .HasForeignKey(y => y.TipoSegmentoId);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesPaginasSegmentos)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesPaginasSegmentos)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}