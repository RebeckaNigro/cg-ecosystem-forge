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
    public class HistoricoEventoConfiguration : IEntityTypeConfiguration<HistoricoEvento>
    {
        public void Configure(EntityTypeBuilder<HistoricoEvento> builder)
        {
            builder.ToTable("HistoricoEvento", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.Evento)
                .WithMany(x => x.HistoricoEventos)
                .HasForeignKey(y => y.EventoId);

            builder.HasOne(x => x.Instituicao)
                .WithMany(x => x.HistoricoEventos)
                .HasForeignKey(y => y.InstituicaoId);

            builder.HasOne(x => x.TipoEvento)
                .WithMany(x => x.HistoricoEventos)
                .HasForeignKey(y => y.TipoEventoId);

            builder.HasOne(x => x.Endereco)
                .WithMany(x => x.HistoricoEventos)
                .HasForeignKey(y => y.EnderecoId)
                .IsRequired(false);

            builder.HasOne(x => x.Aprovacao)
                .WithMany(x => x.HistoricoEventos)
                .HasForeignKey(y => y.AprovacaoId)
                .IsRequired(false);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesHistoricoEventos)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesHistoricoEventos)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}