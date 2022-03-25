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
    public class HistoricoNoticiaConfiguration : IEntityTypeConfiguration<HistoricoNoticia>
    {
        public void Configure(EntityTypeBuilder<HistoricoNoticia> builder)
        {
            builder.ToTable("HistoricoNoticia", "dbo")
                     .HasKey(x => x.Id);

            builder.HasOne(x => x.Noticia)
                .WithMany(x => x.HistoricoNoticias)
                .HasForeignKey(y => y.NoticiaId);

            builder.HasOne(x => x.Aprovacao)
                .WithMany(x => x.HistoricoNoticias)
                .HasForeignKey(y => y.AprovacaoId)
                .IsRequired(false);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesHistoricoNoticias)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesHistoricoNoticias)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}