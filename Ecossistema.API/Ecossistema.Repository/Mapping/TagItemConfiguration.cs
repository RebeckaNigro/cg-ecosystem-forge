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
    public class TagItemConfiguration : IEntityTypeConfiguration<TagItem>
    {
        public void Configure(EntityTypeBuilder<TagItem> builder)
        {
            builder.ToTable("TagItem", "dbo")
                     .HasKey(x => x.Id);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesTagItens)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesTagItens)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}