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
    public class HistoricoInstituicaoConfiguration : IEntityTypeConfiguration<HistoricoInstituicao>
    {
        public void Configure(EntityTypeBuilder<HistoricoInstituicao> builder)
        {
            builder.ToTable("HistoricoInstituicao", "dbo")
                            .HasKey(x => x.Id);

            builder.HasOne(x => x.Instituicao)
                .WithMany(x => x.HistoricoInstituicoes)
                .HasForeignKey(y => y.InstiuicaoId);

            builder.HasOne(x => x.InstituicaoArea)
                .WithMany(x => x.HistoricoInstituicoes)
                .HasForeignKey(y => y.InstituicaoAreaId);

            builder.HasOne(x => x.InstituicaoClassificacao)
                .WithMany(x => x.HistoricoInstituicoes)
                .HasForeignKey(y => y.InstituicaoClassificacaoId);

            builder.HasOne(x => x.TipoInstituicao)
                .WithMany(x => x.HistoricoInstituicoes)
                .HasForeignKey(y => y.TipoInstituicaoId);

            builder.HasOne(x => x.Aprovacao)
                .WithMany(x => x.HistoricoInstituicoes)
                .HasForeignKey(y => y.AprovacaoId)
                .IsRequired(false);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesHistoricoInstituicoes)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesHistoricoInstituicoes)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}