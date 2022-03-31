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
    public class DocumentoAreaConfiguration : IEntityTypeConfiguration<DocumentoArea>
    {
        public void Configure(EntityTypeBuilder<DocumentoArea> builder)
        {
            builder.ToTable("DocumentoArea", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesDocumentosAreas)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesDocumentosAreas)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}