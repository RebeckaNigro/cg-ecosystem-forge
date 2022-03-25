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
    public class FaleConoscoConfiguration : IEntityTypeConfiguration<FaleConosco>
    {
        public void Configure(EntityTypeBuilder<FaleConosco> builder)
        {
            builder.ToTable("FaleConosco", "dbo")
                .HasKey(x => x.Id);

            builder.HasOne(x => x.FaleConoscoSetor)
                .WithMany(x => x.FaleConoscos)
                .HasForeignKey(y => y.FaleConoscoSetorId);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesFaleConoscos)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesFaleConoscos)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}