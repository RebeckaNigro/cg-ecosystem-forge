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
    public class FaleConoscoSetorConfiguration : IEntityTypeConfiguration<FaleConoscoSetor>
    {
        public void Configure(EntityTypeBuilder<FaleConoscoSetor> builder)
        {
            builder.ToTable("FaleConoscoSetor", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesFaleConoscoSetores)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesFaleConoscoSetores)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}