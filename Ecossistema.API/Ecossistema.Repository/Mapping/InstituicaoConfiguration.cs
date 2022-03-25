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
    public class InstituicaoConfiguration : IEntityTypeConfiguration<Instituicao>
    {
        public void Configure(EntityTypeBuilder<Instituicao> builder)
        {
            builder.ToTable("Instituicao", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.InstituicaoArea)
                .WithMany(x => x.Instituicoes)
                .HasForeignKey(y => y.InstituicaoAreaId);

            builder.HasOne(x => x.InstituicaoClassificacao)
                .WithMany(x => x.Insituicoes)
                .HasForeignKey(y => y.InstituicaoClassificacaoId);

            builder.HasOne(x => x.TipoInstituicao)
                .WithMany(x => x.Insituicoes)
                .HasForeignKey(y => y.TipoInstituicaoId);

            builder.HasOne(x => x.Aprovacao)
                .WithMany(x => x.Insituicoes)
                .HasForeignKey(y => y.AprovacaoId)
                .IsRequired(false);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesInstituicoes)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesInstituicoes)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}