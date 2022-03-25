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
    public class SituacaoAprovacaoConfiguration : IEntityTypeConfiguration<SituacaoAprovacao>
    {
        public void Configure(EntityTypeBuilder<SituacaoAprovacao> builder)
        {
            builder.ToTable("SituacaoAprovacao", "dbo")
                     .HasKey(x => x.Id);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesSituacoesAprovacoes)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesSituacoesAprovacoes)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}