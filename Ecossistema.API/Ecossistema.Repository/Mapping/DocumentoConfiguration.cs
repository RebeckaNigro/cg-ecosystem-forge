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
    public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.ToTable("Documento", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.TipoDocumento)
                .WithMany(x => x.Documentos)
                .HasForeignKey(y => y.TipoDocumentoId);

            builder.HasOne(x => x.DocumentoArea)
                .WithMany(x => x.Documentos)
                .HasForeignKey(y => y.DocumentoAreaId);

            builder.HasOne(x => x.Instituicao)
                .WithMany(x => x.Documentos)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.Aprovacao)
                .WithMany(x => x.Documentos)
                .HasForeignKey(y => y.AprovacaoId);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesDocumentos)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesDocumentos)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}