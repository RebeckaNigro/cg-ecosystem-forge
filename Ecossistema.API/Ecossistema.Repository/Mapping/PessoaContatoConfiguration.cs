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
    public class PessoaContatoConfiguration : IEntityTypeConfiguration<PessoaContato>
    {
        public void Configure(EntityTypeBuilder<PessoaContato> builder)
        {
            builder.ToTable("PessoaContato", "dbo")
                     .HasKey(x => x.Id);

            builder.HasOne(x => x.Pessoa)
                .WithMany(x => x.PessoasContatos)
                .HasForeignKey(y => y.PessoaId);

            builder.HasOne(x => x.Contato)
                .WithMany(x => x.PessoasContatos)
                .HasForeignKey(y => y.ContatoId);

            builder.HasOne(x => x.TipoContato)
                .WithMany(x => x.PessoasContatos)
                .HasForeignKey(y => y.TipoContatoId);

            builder.HasOne(x => x.UsuarioCriacao)
                .WithMany(x => x.UsuariosCriacoesPessoasContatos)
                .HasForeignKey(y => y.UsuarioCriacaoId);

            builder.HasOne(x => x.UsuarioOperacao)
                .WithMany(x => x.UsuariosOperacoesPessoasContatos)
                .HasForeignKey(y => y.UsuarioOperacaoId);
        }
    }
}