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
    public class FaleConoscoSetorContatoConfiguration : IEntityTypeConfiguration<FaleConoscoSetorContato>
    {
        public void Configure(EntityTypeBuilder<FaleConoscoSetorContato> builder)
        {
            builder.ToTable("FaleConoscoSetorContato", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.FaleConoscoSetor)
                .WithMany(x => x.FaleConoscoSetoresContatos)
                .HasForeignKey(y => y.FaleConoscoSetorId);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesFaleConoscoSetoresContatos)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesFaleConoscoSetoresContatos)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}