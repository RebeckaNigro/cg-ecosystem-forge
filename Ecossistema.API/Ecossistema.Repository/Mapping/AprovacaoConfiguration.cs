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
    public class AprovacaoConfiguration : IEntityTypeConfiguration<Aprovacao>
    {
        public void Configure(EntityTypeBuilder<Aprovacao> builder)
        {
            builder.ToTable("Aprovacao", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.Origem)
                .WithMany(x => x.Aprovacoes)
                .HasForeignKey(y => y.OrigemId);

            builder.HasOne(x => x.UsuarioAprovacao)
                .WithMany(x => x.UsuariosAprovacoes)
                .HasForeignKey(y => y.UsuarioAprovacaoId)
                .IsRequired(false);

            builder.HasOne(x => x.Insituicao)
                .WithMany(x => x.Aprovacoes)
                .HasForeignKey(y => y.InstituicaoId)
                .IsRequired(false);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Aprovacoes)
                .HasForeignKey(y => y.UsuarioId)
                .IsRequired(false);

            builder.HasOne(x => x.Evento)
                .WithMany(x => x.Aprovacoes)
                .HasForeignKey(y => y.EventoId)
                .IsRequired(false);

            builder.HasOne(x => x.Noticia)
                .WithMany(x => x.Aprovacoes)
                .HasForeignKey(y => y.NoticiaId)
                .IsRequired(false);

            builder.HasOne(x => x.Documento)
                .WithMany(x => x.Aprovacoes)
                .HasForeignKey(y => y.DocumentoId)
                .IsRequired(false);

            builder.HasOne(x => x.SituacaoAprovacao)
                .WithMany(x => x.Aprovacoes)
                .HasForeignKey(y => y.SituacaoAprovacaoId);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesAprovacoes)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesAprovacoes)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}