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
    public class OrigemConfiguration : IEntityTypeConfiguration<Origem>
    {
        public void Configure(EntityTypeBuilder<Origem> builder)
        {
            builder.ToTable("Origem", "dbo")
                     .HasKey(x => x.Id);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesOrigens)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesOrigens)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}