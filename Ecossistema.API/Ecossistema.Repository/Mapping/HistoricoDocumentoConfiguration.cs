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
    public class HistoricoDocumentoConfiguration : IEntityTypeConfiguration<HistoricoDocumento>
    {
        public void Configure(EntityTypeBuilder<HistoricoDocumento> builder)
        {
            builder.ToTable("HistoricoDocumento", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.Documento)
                .WithMany(x => x.HistoricoDocumentos)
                .HasForeignKey(y => y.DocumentoId);


            builder.HasOne(x => x.DocumentoArea)
                .WithMany(x => x.HistoricoDocumentos)
                .HasForeignKey(y => y.DocumentoAreaId);

            builder.HasOne(x => x.Instituicao)
                .WithMany(x => x.HistoricoDocumentos)
                .HasForeignKey(y => y.InstituicaoId);

            builder.HasOne(x => x.Aprovacao)
                .WithMany(x => x.HistoricoDocumentos)
                .HasForeignKey(y => y.AprovacaoId)
                .IsRequired(false);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesHistoricoDocumentos)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesHistoricoDocumentos)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}