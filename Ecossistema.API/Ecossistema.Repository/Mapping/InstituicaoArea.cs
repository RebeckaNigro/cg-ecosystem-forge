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
    public class InstituicaoAreaConfiguration : IEntityTypeConfiguration<InstituicaoArea>
    {
        public void Configure(EntityTypeBuilder<InstituicaoArea> builder)
        {
            builder.ToTable("InstituicaoArea", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesInstituicoesAreas)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesInstituicoesAreas)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}