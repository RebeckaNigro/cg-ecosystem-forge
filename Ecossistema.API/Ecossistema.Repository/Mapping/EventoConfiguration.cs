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
    public class EventoConfiguration : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.ToTable("Evento", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.Instituicao)
                .WithMany(x => x.Eventos)
                .HasForeignKey(y => y.InstituicaoId);

            builder.HasOne(x => x.TipoEvento)
                .WithMany(x => x.Eventos)
                .HasForeignKey(y => y.TipoEventoId);

            builder.HasOne(x => x.Endereco)
                .WithMany(x => x.Eventos)
                .HasForeignKey(y => y.EnderecoId)
                .IsRequired(false);

            builder.HasOne(x => x.Aprovacao)
                .WithMany(x => x.Eventos)
                .HasForeignKey(y => y.AprovacaoId)
                .IsRequired(false);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesEventos)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesEventos)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}